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

    function RowOffset(columnsPerRow, columns) {
        const maxColumns = Math.max(...columnsPerRow);
        const maxWidthPixels = 30 * maxColumns;
        const widthPixels = 30 * columns;

        return (maxWidthPixels - widthPixels) / 2;
    }


    let y = 0;

    for (let i = 0; i < rows; i++) {
        const columns = columnsPerRow[i];

        let x = RowOffset(columnsPerRow, columns);

        for (let j = 0; j < columns; j++) {
            let columnX = x;
            let columnY = y;

            ctx.beginPath();
            ctx.fillStyle = "#009578";
            ctx.rect(columnX, columnY, 25, 25);
            ctx.fill();


            ctx.fillStyle = "white";
            ctx.font = "bold 12px Arial";
            ctx.textBaseline = "middle";
            ctx.textAlign = "center";
            ctx.fillText(j + 1, columnX + 12.5, columnY + 12.5);

            x += marginX;
        }
        y += marginY;
    }


}