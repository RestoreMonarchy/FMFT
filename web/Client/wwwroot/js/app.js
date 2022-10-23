function BuildSeatsCanvas(canvasId) {
    const canvas = document.getElementById(canvasId);
    const ctx = canvas.getContext("2d");

    const rows = 11;

    const columnsPerRow = [
        18,
        21,
        23,
        24,
        25,
        25,
        23,
        20,
        15,
        13,
        6
    ];

    const marginX = 30;
    const marginY = 30;

    const maxColumns = Math.max(...columnsPerRow);
    const maxWidthPixels = marginX * maxColumns;
    const maxHeightPixels = marginY * rows;

    canvas.height = maxHeightPixels;
    canvas.width = maxWidthPixels;

    function RowOffset(columns) {
        const maxColumns = Math.max(...columnsPerRow);
        const maxWidthPixels = 30 * maxColumns;
        const widthPixels = 30 * columns;

        return (maxWidthPixels - widthPixels) / 2;
    }

    function DrawSeat(row, column, color) {
        const offset = RowOffset(columnsPerRow[row]);

        let columnX = offset + column * marginX;
        let columnY = row * marginY;

        ctx.beginPath();
        ctx.fillStyle = color;
        ctx.rect(columnX, columnY, 25, 25);
        ctx.fill();

        ctx.fillStyle = "white";
        ctx.font = "bold 12px Arial";
        ctx.textBaseline = "middle";
        ctx.textAlign = "center";
        ctx.fillText(column + 1, columnX + 12.5, columnY + 12.5);
    }

    for (let i = 0; i < rows; i++) {
        const columns = columnsPerRow[i];

        for (let j = 0; j < columns; j++) {
            
            DrawSeat(i, j, "#009578");
        }
    }

    canvas.addEventListener("click", HandleClick);

    function HandleClick(e) {

        const rect = canvas.getBoundingClientRect();

        console.log("RECT: ", rect);
        console.log("EVENT ARGS: ", e);

        const zoom = maxHeightPixels / rect.height;

        console.log("ZOOM: ", zoom);

        const x = (e.clientX - rect.left) * zoom;
        const y = (e.clientY - rect.top) * zoom;

        console.log("x: ", x, " y: ", y);

        

        let column = -1;
        const row = y / 30;
        const rowFloor = Math.floor(row);

        console.log("row: ", row);
        const bottomMargin = 5 / 30;
        const rowBottomMargin = (row - rowFloor);


        if (rowBottomMargin >= 1 - bottomMargin) {
            console.log("clicked row margin");
            return;
        }

        const columns = columnsPerRow[rowFloor];
        const rowOffset = RowOffset(columns);

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
            return;
        }

        DrawSeat(rowFloor, column, "red");
        console.log("row: ", rowFloor + 1, "column: ", column + 1);
    }


}