window.codeManager = {
    renderFileContent: function (url) {
        var xhr = new XMLHttpRequest();
        xhr.open('GET', url);
        var snippetContainer = document.getElementById("codecontainer");
        xhr.onload = function () {
            if (xhr.status === 200) {
                if (snippetContainer) {
                    snippetContainer.innerHTML = xhr.responseText;
                    prettyPrint();
                }
            }
            else {
                if (snippetContainer) {
                    snippetContainer.innerHTML = 'Request for code snippet failed.  Returned status of ' + xhr.status;
                }
            }
        };
        xhr.send();
    },

    renderCode: function (url) {
        var filePath = '/blazor-ui/source/index?path=Pages' + url.replace(/-/g, '_');
        window.codeManager.renderFileContent(filePath);
    },

    renderDemoFileContent: function (url) {
        window.codeManager.renderFileContent("/blazor-ui/source/read?path=" + url);
    }
}
