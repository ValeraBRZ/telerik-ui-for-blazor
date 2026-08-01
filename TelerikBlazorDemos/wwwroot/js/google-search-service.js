(function () {
    window.googleSearchService = window.googleSearchService || {};

    window.googleSearchService = {
        searchAsync: function(requestOptions) {
            const request = new Request(requestOptions.url);

            return fetch(request)
                .then(response => {
                    if (response.status === 200) {
                        return response.json();
                    } else {
                        return null;
                    }
                })
                .then(data => {
                    return data;
                }).catch(() => {
                    return null;
                });
        }
    }
})();
