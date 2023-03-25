async function InitializeSeatsCanvas(options, dotnetHelper) {

    const canvasId = options.canvasId;
    const seatsMap = options.seatsMap;
    const marginY = options.marginY;
    const defaultColor = options.defaultColor;

    const canvas = document.getElementById(canvasId);

    const ctx = canvas.getContext("2d");
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    let totalRows = 0;

    for (let i = 0; i < seatsMap.length; i++) {
        totalRows += seatsMap[i].length;
    }

    let maxWidthPixels = GetMaxWidth(options);
    let maxHeightPixels = marginY * totalRows;

    const stageWidth = options.stageWidth;
    const stageHeight = options.stageHeight;
    const stageOffset = options.stageOffset;
    const sectorSpace = options.sectorSpace;

    canvas.height = maxHeightPixels + stageOffset + (seatsMap.length - 1) * sectorSpace;
    canvas.width = maxWidthPixels;

    DrawStage(options, stageHeight, stageWidth);

    const sectors = seatsMap.length;
    for (let h = 0; h < sectors; h++) {
        const rows = seatsMap[h].length;

        const sector = h + 1;

        for (let i = 0; i < rows; i++) {
            const columns = seatsMap[h][i];

            const row = i + 1;

            for (let j = 0; j < columns; j++) {
                const column = j + 1;

                DrawSeat(options, row, column, sector, defaultColor);
            }
        }
    }    

    function HandleClick(e) {
        const rect = canvas.getBoundingClientRect();
        const zoom = canvas.height / rect.height;

        const x = (e.clientX - rect.left) * zoom;
        const y = (e.clientY - rect.top) * zoom;

        const seat = FindSeatByCoordinates(options, x, y);

        if (seat == null) {
            return;
        }

        //console.log("row: ", seat.row, "column: ", seat.column);
        dotnetHelper.invokeMethodAsync("HandleSeatClickAsync", seat.row, seat.column, seat.sector);
    }

    canvas.addEventListener("click", HandleClick);
}

function DrawStage(options, height, width) {
    const canvasId = options.canvasId;
    const font = options.stageFont;
    const color = options.stageColor;

    const canvas = document.getElementById(canvasId);
    const ctx = canvas.getContext("2d");

    const x = canvas.width / 2 - width / 2;
    const y = 0;

    ctx.beginPath();
    ctx.fillStyle = color;
    ctx.rect(x, y, width, height);
    ctx.fill();

    const posX = x + width / 2;
    const posY = y + height / 2;

    ctx.fillStyle = "white";
    ctx.font = font;
    ctx.textBaseline = "middle";
    ctx.textAlign = "center";
    ctx.fillText("Scena", posX, posY);
}

function DrawSeat(options, row, column, sector, color) { 
    const canvasId = options.canvasId;
    
    const sizeX = options.sizeX;
    const sizeY = options.sizeY;
    const font = options.font;    

    const canvas = document.getElementById(canvasId);
    const ctx = canvas.getContext("2d");

    const pos = GetPosition(options, row, column, sector);

    ctx.beginPath();
    ctx.fillStyle = color;
    ctx.rect(pos.columnX, pos.columnY, sizeX, sizeY);
    ctx.fill();

    ctx.fillStyle = "white";
    ctx.font = font;
    ctx.textBaseline = "middle";
    ctx.textAlign = "center";
    ctx.fillText(column, pos.x, pos.y);
}

function GetPosition(options, row, column, sector) {
    const rowIndex = row - 1;
    const columnIndex = column - 1;
    const sectorIndex = sector - 1;
    const canvasWidth = GetMaxWidth(options);

    const stageOffset = options.stageOffset;
    const sectorOffset = GetSectorOffsetY(options, sector);

    const marginX = options.marginX;
    const marginY = options.marginY;

    const sizeX = options.sizeX;
    const sizeY = options.sizeY;

    const breakpointSpace = options.breakpointSpace;
    const breakpoints = options.breakpoints;
    const breakpoint = breakpoints[sectorIndex][rowIndex];

    let offset = CalculateRowOffset(options, row, sector);

    let columnX = canvasWidth - (offset + column * marginX);
    if (breakpoint != undefined) {
        if (breakpoint < column) {
            columnX -= breakpointSpace;
        } else {
            columnX += offset;
        }
    }

    const columnY = rowIndex * marginY + stageOffset + sectorOffset;

    const posX = columnX + sizeX / 2;
    const posY = columnY + sizeY / 2;

    const pos = {
        x: posX,
        y: posY,
        columnX: columnX,
        columnY: columnY
    };

    return pos;
}

function GetMaxWidth(options) {
    const seatsMap = options.seatsMap;
    const breakpoints = options.breakpoints;
    const breakpointSpace = options.breakpointSpace;
    const marginX = options.marginX;

    let maxWidth = 0;

    for (let i = 0; i < seatsMap.length; i++) {
        for (let j = 0; j < seatsMap[i].length; j++) {
            const columns = seatsMap[i][j];

            let width = columns * marginX;

            const breakpoint = breakpoints[i][j];
            if (breakpoint != undefined) {
                width += breakpointSpace;
            }

            if (width > maxWidth) {
                maxWidth = width;
            }
        }
    }

    return maxWidth;
}

function GetMaxColumns(seatsMap) {
    let maxColumns = 0;

    for (let i = 0; i < seatsMap.length; i++) {
        const sectorMaxColumns = Math.max(...seatsMap[i]);
        if (sectorMaxColumns > maxColumns) {
            maxColumns = sectorMaxColumns;
        }
    }

    return maxColumns;
}

function GetSectorOffsetY(options, sector) {
    const seatsMap = options.seatsMap;
    const sectorSpace = options.sectorSpace;
    let offset = 0;

    for (let i = 0; i < seatsMap.length; i++) {
        if (i + 1 < sector) {
            offset += options.marginY * seatsMap[i].length + sectorSpace;
        }
    }

    return offset;
}

function GetSectorHeight(options, sector) {
    const seatsMap = options.seatsMap;
    const marginY = options.marginY;

    return seatsMap[sector - 1].length * marginY;
}

function CalculateRowOffset(options, row, sector) {
    const seatsMap = options.seatsMap;
    const marginX = options.marginX;
    const breakpoints = options.breakpoints;
    const breakpointSpace = options.breakpointSpace;
    const sectorIndex = sector - 1;
    const rowIndex = row - 1;

    const sectorMap = seatsMap[sectorIndex];

    const maxWidthPixels = GetMaxWidth(options);

    const columns = sectorMap[rowIndex];
    let widthPixels = marginX * columns;

    if (breakpoints[sectorIndex][rowIndex] != undefined) {
        widthPixels += breakpointSpace;
    }

    return (maxWidthPixels - widthPixels) / 2;
}

function FindSectorByCoordinates(options, x, y) {
    const stageOffset = options.stageOffset;
    const seatsMap = options.seatsMap;

    for (let i = 0; i < seatsMap.length; i++) {
        const sector = i + 1;
        const sectorOffset = GetSectorOffsetY(options, sector);

        const startX = stageOffset + sectorOffset;
        const endX = startX + GetSectorHeight(options, sector);

        if (y >= startX && y <= endX)
        {
            return sector;
        }
    }

    return null;
    
}

function FindSeatByCoordinates(options, x, y) {
    const seatsMap = options.seatsMap;
    const marginX = options.marginX;
    const marginY = options.marginY;
    const sizeX = options.sizeX;
    const sizeY = options.sizeY;
    const stageOffset = options.stageOffset;

    const sector = FindSectorByCoordinates(options, x, y);
    if (sector == null) {
        return;
    }

    const sectorOffset = GetSectorOffsetY(options, sector);
    const sectorMap = seatsMap[sector - 1];

    const rowExact = (y - stageOffset - sectorOffset) / marginY;
    const rowIndex = Math.floor(rowExact);
    const row = rowIndex + 1;
    const columns = sectorMap[rowIndex];

    for (let i = 0; i < columns; i++) {
        const column = i + 1;
        const pos = GetPosition(options, row, column, sector);        

        const endX = pos.columnX + sizeX;
        const endY = pos.columnY + sizeY;

        if (pos.columnX <= x && endX >= x && pos.columnY <= y && endY >= y) {
            return {
                row: row,
                column: column,
                sector: sector
            };
        }
    }

    return null;
}