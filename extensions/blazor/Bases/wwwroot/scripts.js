document.onkeypress = function (e) {
    e = e || window.event;

    const params =
    {
        KeyCode: e.keyCode,
        Key: e.key
    };

    DotNet.invokeMethodAsync("FMFT.Extensions.Blazor.Bases", "TriggerOnKeyPressAsync", params);
};

function ShowModal(modalElement) {
    const modal = new bootstrap.Modal(modalElement);
    modal.show();
}

function ShowModalStatic(modalElement) {
    const modal = new bootstrap.Modal(modalElement, {
        backdrop: 'static'
    });
    modal.show();
}

function HideModal(modalElement) {
    const modal = bootstrap.Modal.getInstance(modalElement);
    modal.hide();
}

function StartCarousel(myCarousel) {
    if (typeof (myCarousel) == 'undefined' || myCarousel == null) {
        return;
    }

    const carousel = new bootstrap.Carousel(myCarousel);
    carousel.cycle();
}

function SelectInput(inputElement) {
    inputElement.select();
}