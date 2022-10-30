function ClearModalBackdrop() {
    const elements = document.getElementsByClassName("modal-backdrop");
    while (elements.length > 0) {
        elements[0].parentNode.removeChild(elements[0]);
    }
}