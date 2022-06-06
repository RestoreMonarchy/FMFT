
function DoPanzoom() {
    const elem = document.getElementById('panzoom-element')
    const panzoom = Panzoom(elem, {
        maxScale: 5
    })
    /*panzoom.pan(1000, 10)*/
    panzoom.zoom(0.5, { animate: true })

    const zoomInButton = document.getElementById('panzoom-zoomin');
    const zoomOutButton = document.getElementById('panzoom-zoomout');
    const resetButton = document.getElementById('panzoom-reset');

    zoomInButton.addEventListener('click', panzoom.zoomIn)
    zoomOutButton.addEventListener('click', panzoom.zoomOut)
    resetButton.addEventListener('click', panzoom.reset)

    // Panning and pinch zooming are bound automatically (unless disablePan is true).
    // There are several available methods for zooming
    // that can be bound on button clicks or mousewheel.
    //button.addEventListener('click', panzoom.zoomIn)
    //elem.parentElement.addEventListener('wheel', panzoom.zoomWithWheel)
}
