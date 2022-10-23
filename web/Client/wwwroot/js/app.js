class SeatsCanvas {

    constructor(canvas, ctx, seatsMap, options) {
        this.canvas = canvas;
        this.ctx = ctx;
        this.seatsMap = seatsMap;
        this.options = options;

        this.maxColumns = Math.max(...seatsMap)
        this.maxWidthPixels = this.options.marginX * this.maxColumns;
        this.maxHeightPixels = this.options.marginY * this.seatsMap.length;

        this.canvas.height = this.maxHeightPixels;
        this.canvas.width = this.maxWidthPixels;
    }

    RowOffset(columns) {
        const widthPixels = this.options.marginX * columns;

        return (this.maxWidthPixels - widthPixels) / 2;
    }

    DrawSeat(row, column, color) {
        const columns = this.seatsMap[row];
        const offset = this.RowOffset(columns);

        const columnX = offset + column * this.options.marginX;
        const columnY = row * this.options.marginY;

        this.ctx.beginPath();
        this.ctx.fillStyle = color;
        this.ctx.rect(columnX, columnY, 25, 25);
        this.ctx.fill();

        this.ctx.fillStyle = "white";
        this.ctx.font = "bold 12px Arial";
        this.ctx.textBaseline = "middle";
        this.ctx.textAlign = "center";
        this.ctx.fillText(column + 1, columnX + 12.5, columnY + 12.5);
    }

    FindSeatByCoordinates(x, y) {
        let column = -1;
        const row = y / 30;
        const rowFloor = Math.floor(row);

        console.log("row: ", row);
        const bottomMargin = 5 / 30;
        const rowBottomMargin = (row - rowFloor);

        if (rowBottomMargin >= 1 - bottomMargin) {
            console.log("clicked row margin");
            return null;
        }

        const columns = this.seatsMap[rowFloor];
        const rowOffset = this.RowOffset(columns);

        for (let k = 0; k < columns; k++) {
            const columnX = rowOffset + k * 30;

            //console.log("columnX: ", columnX, "rowOffset: ", rowOffset, "x: ", x);

            if (columnX <= x && x < columnX + 25) {
                column = k;
                break;
            }
        }

        if (column == -1) {
            console.log("clicked on column margin");
            return null;
        }

        return {
            row: row,
            column: column
        };
    }
}

function BuildSeatsCanvas(canvasId, seatsMap, options, dotnetHelper) {

    const canvas = document.getElementById(canvasId);
    const ctx = canvas.getContext("2d");


    const seatsCanvas = new SeatsCanvas(canvas, ctx, seatsMap, options);


    for (let i = 0; i < seatsMap.length; i++) {
        const columns = seatsMap[i];

        for (let j = 0; j < columns; j++) {
            
            seatsCanvas.DrawSeat(i, j, options.defaultColor);
        }
    }

    canvas.addEventListener("click", HandleClick);

    function HandleClick(e) {

        const rect = canvas.getBoundingClientRect();

        console.log("RECT: ", rect);
        console.log("EVENT ARGS: ", e);

        const zoom = seatsCanvas.maxHeightPixels / rect.height;

        console.log("ZOOM: ", zoom);

        const x = (e.clientX - rect.left) * zoom;
        const y = (e.clientY - rect.top) * zoom;

        console.log("x: ", x, " y: ", y);

        const seat = seatsCanvas.FindSeatByCoordinates(x, y);

        if (seat == null) {
            return;
        }

        DrawSeat(seat.row, seat.column, "red");

        const actualRow = seat.row + 1;
        const actualColumn = seat.column + 1;

        console.log("row: ", actualRow, "column: ", actualColumn);
        dotnetHelper.invokeMethodAsync('HandleSeatClickAsync', actualRow, actualColumn);
    }
}