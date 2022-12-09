function ClearModalBackdrop() {
    const elements = document.getElementsByClassName("modal-backdrop");
    while (elements.length > 0) {
        elements[0].parentNode.removeChild(elements[0]);
    }
}

function HideNavbarCollapse(navbarContent) {
    const bsCollapse = new bootstrap.Collapse(navbarContent,
        {
            toggle: false
        })

    bsCollapse.hide();
}

function downloadFromUrl(url, fileName) {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}

function DownloadFromByteArray(byteArray, fileName, contentType) {

    // Convert numbers array to Uint8Array object.
    const uint8Array = new Uint8Array(byteArray);

    // Wrap it by Blob object.
    const blob = new Blob([uint8Array], { type: contentType });

    // Create "object URL" that is linked to the Blob object.
    const url = URL.createObjectURL(blob);

    // Invoke download helper function that implemented in 
    // the earlier section of this article.
    downloadFromUrl(url, fileName);

    // At last, release unused resources.
    URL.revokeObjectURL(url);
}