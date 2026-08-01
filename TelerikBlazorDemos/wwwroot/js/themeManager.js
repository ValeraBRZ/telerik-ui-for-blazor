window.theme = {
    appThemeLsKey: "telerik-blazor-dark-1",
    blazorSwatchLsKey: "telerik-blazor-swatch-1",

    changeCss: function (cssFileUrl, themeState) {
        var oldLink = document.getElementById("kendoCss");

        if (cssFileUrl === oldLink.getAttribute("href")) {
            return;
        }

        var newLink = document.createElement("link");
        newLink.setAttribute("id", "kendoCss");
        newLink.setAttribute("rel", "stylesheet");
        newLink.setAttribute("type", "text/css");
        newLink.setAttribute("href", cssFileUrl);
        newLink.onload = () => {
            oldLink.parentElement.removeChild(oldLink);
            localStorage.setItem(theme.blazorSwatchLsKey, JSON.stringify(themeState));

            var componentElements = document.querySelectorAll([
                ".k-chart",
                ".k-scheduler-layout",
                ".k-gauge",
                ".k-signature",
                ".k-pivotgrid",
                ".k-gantt"
            ].join(","));

            var instances = TelerikBlazor._instances;

            for (var i = 0; i < componentElements.length; i++) {
                var id = componentElements[i].getAttribute("data-id");
                var instance = instances[id];

                if (instance && instance.refresh) {
                    instance.refresh();
                }
            }
        };

        document.getElementsByTagName("head")[0].appendChild(newLink);
    },

    restoreCss: function () {
        return JSON.parse(localStorage.getItem(theme.blazorSwatchLsKey));
    },

    appTheme: function () {
        var isDark = false;
        var domElement = document.querySelector(".kd-mode");

        function setTheme() {
            domElement.setAttribute('data-is-dark', isDark);
            localStorage.setItem(theme.appThemeLsKey, isDark);
        }

        function switchTheme() {
            isDark = !isDark;
            setTheme();
            return isDark;
        }

        function init() {
            // delayed set due to appTheme self invokation
            isDark = JSON.parse(localStorage.getItem(theme.appThemeLsKey)) === true;

            setTheme();
        }

        function disableDark() {
            domElement.setAttribute('data-is-dark', false);
        }

        function restoreDark() {
            domElement.setAttribute('data-is-dark', isDark);
        }

        return {
            init: init,
            switchTheme: switchTheme,
            disableDark: disableDark,
            restoreDark: restoreDark
        }
    }()
};
