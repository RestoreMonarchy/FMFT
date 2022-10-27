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
                    integrity: integrity,
                    headers: { 'MyCustomHeader': 'My custom value' }
                });

                resourcesToLoad.push(fetchResources);

                fetchResources.then((r) => {
                    loadedCount += 1;

                    const totalCount = resourcesToLoad.length;
                    const percentLoaded = 10 + parseInt((loadedCount * 90.0) / totalCount);
                    const progressbar = document.getElementById('progressbar');
                    progressbar.style.width = percentLoaded + '%';
                    const progressLabel = document.getElementById('progressLabel');
                    progressLabel.innerText = `Downloading ${loadedCount}/${totalCount}: ${filename}`;
                });

                return fetchResources;
            }
    });
}

StartBlazor();