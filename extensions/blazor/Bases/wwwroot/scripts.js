
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
