function StartBlazor() {
    let loadedCount = 0;
    const resourcesToLoad = [];
    Blazor.start({
        loadBootResource:
            function (type, filename, defaultUri, integrity) {
                if (type == "dotnetjs")
                    return defaultUri;

                const fetchResources = fetch(defaultUri, {
                    cache: 'no-cache',
                    integrity: integrity
                });

                resourcesToLoad.push(fetchResources);

                fetchResources.then((r) => {
                    loadedCount += 1;

                    if (filename == "blazor.boot.json") {
                        const progressbar = document.getElementById('progressbar');
                        progressbar.style.width = 90 + '%';
                        return;
                    }                        

                    const totalCount = resourcesToLoad.length;
                    const percentLoaded = 10 + parseInt((loadedCount * 90.0) / totalCount);
                    const progressbar = document.getElementById('progressbar');
                    progressbar.style.width = percentLoaded + '%';
                    const progressLabel = document.getElementById('progressLabel');
                    progressLabel.innerText = `Pobieranie ${loadedCount}/${totalCount}: ${filename}`;
                });

                return fetchResources;
            }
    });
}

StartBlazor();