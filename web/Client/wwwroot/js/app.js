﻿function ClearModalBackdrop() {
    const elements = document.getElementsByClassName("modal-backdrop");
    while (elements.length > 0) {
        elements[0].parentNode.removeChild(elements[0]);
    }
}

function HideNavbarCollapse(navbarContent) {
    const bsCollapse = new bootstrap.Collapse(navbarContent)
    bsCollapse.hide();
}