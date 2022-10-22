function BuildSeatsCanvas(canvasId) {
    const canvas = document.getElementById(canvasId);
    const ctx = canvas.getContext("2d");

    //ctx.beginPath();
    //ctx.moveTo(100, 100);
    //ctx.lineTo(200, 100);
    //ctx.lineTo(200, 150);
    //ctx.closePath();
    //ctx.stroke();

    ctx.strokeColor = "red";
    ctx.beginPath();
    ctx.moveTo(200, 200);
    ctx.lineTo(400, 200);
    ctx.lineTo(400, 300);
    /*ctx.closePath();*/
    ctx.stroke();


}