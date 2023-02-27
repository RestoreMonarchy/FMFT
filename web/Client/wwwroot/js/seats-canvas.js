async function InitializeSeatsCanvas(options, dotnetHelper) {

    const canvasId = options.canvasId;
    const seatsMap = options.seatsMap;
    const marginX = options.marginX;
    const marginY = options.marginY;
    const defaultColor = options.defaultColor;

    const canvas = document.getElementById(canvasId);

    const ctx = canvas.getContext("2d");

    ctx.clearRect(0, 0, canvas.width, canvas.height);

    //await new Promise(r => setTimeout(r, 1000));

    const maxColumns = Math.max(...seatsMap);
    const maxWidthPixels = marginX * maxColumns;
    const maxHeightPixels = marginY * seatsMap.length;

    const stageWidth = options.stageWidth;
    const stageHeight = options.stageHeight;
    const stageOffset = options.stageOffset;

    canvas.height = maxHeightPixels + stageOffset;
    canvas.width = maxWidthPixels;

    DrawStage(options, stageHeight, stageWidth);

    const rows = seatsMap.length;

    for (let i = 0; i < rows; i++) {
        const columns = seatsMap[i];

        const row = i + 1;

        for (let j = 0; j < columns; j++) {            
            const column = j + 1;

            DrawSeat(options, row, column, defaultColor);
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
        dotnetHelper.invokeMethodAsync("HandleSeatClickAsync", seat.row, seat.column);
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

//function DrawRow(options, row) {
//    const rowIndex = row - 1;

//    const canvasId = options.canvasId;
//    const font = options.font;

//    const canvas = document.getElementById(canvasId);
//    const ctx = canvas.getContext("2d");

//    ctx.fillStyle = "white";
//    ctx.font = font;
//    ctx.textBaseline = "middle";
//    ctx.textAlign = "center";
//    ctx.fillText(row, 0, 0);
//}

function DrawSeat(options, row, column, color) { 
    const stageOffset = options.stageOffset;

    const rowIndex = row - 1;
    const columnIndex = column - 1;

    const canvasId = options.canvasId;
    const marginX = options.marginX;
    const marginY = options.marginY;
    const sizeX = options.sizeX;
    const sizeY = options.sizeY;
    const font = options.font;

    const canvas = document.getElementById(canvasId);
    const ctx = canvas.getContext("2d");

    const offset = CalculateRowOffset(options, row);

    const columnX = offset + columnIndex * marginX;
    const columnY = rowIndex * marginY + stageOffset;
    const posX = columnX + sizeX / 2;
    const posY = columnY + sizeY / 2;
    
    ctx.beginPath();
    ctx.fillStyle = color;
    ctx.rect(columnX, columnY, sizeX, sizeY);
    ctx.fill();

    ctx.fillStyle = "white";
    ctx.font = font;
    ctx.textBaseline = "middle";
    ctx.textAlign = "center";
    ctx.fillText(column, posX, posY);
}

function CalculateRowOffset(options, row) {

    const rowIndex = row - 1;

    const seatsMap = options.seatsMap;
    const marginX = options.marginX;

    const maxColumns = Math.max(...seatsMap);
    const maxWidthPixels = marginX * maxColumns;

    const columns = seatsMap[rowIndex];
    const widthPixels = marginX * columns;

    return (maxWidthPixels - widthPixels) / 2;
}

function FindSeatByCoordinates(options, x, y) {

    const seatsMap = options.seatsMap;
    const marginX = options.marginX;
    const marginY = options.marginY;
    const sizeX = options.sizeX;
    const sizeY = options.sizeY;
    const stageOffset = options.stageOffset;

    const rowExact = (y - stageOffset) / marginY;
    const rowIndex = Math.floor(rowExact);

    //console.log("row exact: ", rowExact, " row floor: ", rowIndex);

    const marginYSize = marginY - sizeY;
    const bottomMargin = marginYSize / marginY;
    const rowBottomMargin = rowExact - rowIndex;

    if (rowBottomMargin >= 1 - bottomMargin) {
        //console.log("clicked row margin");
        return null;
    }

    const columns = seatsMap[rowIndex];

    const row = rowIndex + 1;
    const rowOffset = CalculateRowOffset(options, row);

    let columnIndex = -1;

    for (let k = 0; k < columns; k++) {

        const columnX = rowOffset + k * marginX;

        //console.log("columnX: ", columnX, "rowOffset: ", rowOffset, " x: ", x, " columnX ", columnX, " sizeX: ", sizeX);

        if (columnX <= x && x < columnX + sizeX) {
            columnIndex = k;
            break;
        }
    }

    if (columnIndex == -1) {
        //console.log("clicked column margin");
        return null;
    }

    return {
        row: row,
        column: columnIndex + 1
    };
}