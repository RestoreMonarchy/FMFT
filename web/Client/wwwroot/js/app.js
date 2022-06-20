
function InitializePanzoom(containerElement) {

    const elem = containerElement.getElementsByClassName('panzoom-element')[0];

    const panzoom = Panzoom(elem, {
        maxScale: 5,
        canvas: true
    })
    /*panzoom.pan(1000, 10)*/
    panzoom.zoom(1, { animate: false })

    //var height = containerElement.height;
    //var width = containerElement.offsetWidth - 100;

    //var elemHeight = elem.height;
    //var elemWidth = elem.offsetWidth - 100;
    //console.log(width);
    //console.log(elemWidth);

    //var zoom = width / elemWidth;
    //console.log(zoom);
    //panzoom.zoom(zoom);


    const toolbarElem = containerElement.getElementsByClassName("panzoom-toolbar")[0];

    const zoomInButton = toolbarElem.getElementsByClassName('panzoom-zoomin')[0];
    const zoomOutButton = toolbarElem.getElementsByClassName('panzoom-zoomout')[0];
    const resetButton = toolbarElem.getElementsByClassName('panzoom-reset')[0];

    zoomInButton.addEventListener('click', panzoom.zoomIn)
    zoomOutButton.addEventListener('click', panzoom.zoomOut)
    resetButton.addEventListener('click', panzoom.reset)

    // Panning and pinch zooming are bound automatically (unless disablePan is true).
    // There are several available methods for zooming
    // that can be bound on button clicks or mousewheel.
    //button.addEventListener('click', panzoom.zoomIn)

    /*containerElement.addEventListener('wheel', panzoom.zoomWithWheel)*/
}
