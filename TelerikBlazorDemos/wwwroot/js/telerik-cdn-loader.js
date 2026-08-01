window.telerikNav = {
    footerSelector: "#telerik-global-footer-container",
    headerHtml: "",
    footerHtml: "",
    addHeaderToDom: function () {
        var appElem = document.getElementsByTagName('app')[0];
        var headerPlaceholder = document.getElementById('header-placeholder');
        var HtmlContent = document.createRange().createContextualFragment(this.headerHtml);
        appElem.insertBefore(HtmlContent, headerPlaceholder);

        setTimeout(function() {
            var header = document.getElementById("js-tlrk-nav");
            header.style.display = 'block';
            headerPlaceholder.style.display = 'none';
            telerikNav.adjustSidebarHeight();
            telerikNav.attachSidebarEvents();
        }, 200);
    },
    adjustSidebarHeight: function () {
        var getElementHeight = (element) => element ? element.getBoundingClientRect().height : 0;
        var header = document.getElementById("js-tlrk-nav");
        var headerHeight = getElementHeight(header);

        var searchbar = document.querySelector(".kd-sub-nav");
        var searchbarHeight = getElementHeight(searchbar);

        var webinarBanner = document.getElementById("webinar-banner");
        var webinarBannerHeight = getElementHeight(webinarBanner);

        var sidebar = document.querySelector(".kd-sidebar");

        if (sidebar) {
            sidebar.style.height = "100%";
            var sidebarHeight = getElementHeight(sidebar);
            telerikNav.defaultSideBarHeight = sidebarHeight - headerHeight - searchbarHeight - webinarBannerHeight;
            sidebar.style.height = telerikNav.defaultSideBarHeight + "px";
        }
    },
    sideBarElement: null,
    ensureSideBarElement: function () {
        if (!telerikNav.sideBarElement || !telerikNav.sideBarElement.parentElement) {
            telerikNav.sideBarElement = document.querySelector(".kd-sidebar");
        }
    },
    defaultSideBarHeight: 0,
    currentSideBarHeight: 0,
    ensureSideBarHeight: function () {
        if (telerikNav.defaultSideBarHeight == 0) {
            telerikNav.defaultSideBarHeight = telerikNav.sideBarElement.offsetHeight;
        }
    },
    webinarElement: null,
    ensureWebinarElement: function () {
        if (!telerikNav.webinarElement || !telerikNav.webinarElement.parentElement) {
            telerikNav.webinarElement = document.getElementById("webinar-banner");
        }
    },
    attachSidebarEvents: function() {
        telerikNav.ensureWebinarElement();
        if (telerikNav.webinarElement && telerikNav.webinarElement.offsetHeight > 0) {
            document.addEventListener("scroll", telerikNav.resizeSideBarOnScroll);
        }
        window.addEventListener("resize", telerikNav.resizeSideBarOnWindowResize);

        window.addEventListener("beforeunload", telerikNav.dispose);
    },
    resizeSideBarOnWindowResize: function () {
        telerikNav.adjustSidebarHeight();
        if (window.scrollY > 0) {
            telerikNav.resizeSideBarOnScroll();
        }
    },
    resizeSideBarOnScroll: function () {
        telerikNav.ensureSideBarElement();
        telerikNav.ensureWebinarElement();
        var scrollTop = window.scrollY;
        var bannerHeight = telerikNav.webinarElement.offsetHeight;
        if (telerikNav.sideBarElement) {
            telerikNav.ensureSideBarHeight();

            var adjustHeight = Math.min(scrollTop, bannerHeight);

            if (adjustHeight != telerikNav.currentSideBarHeight) {
                telerikNav.currentSideBarHeight = adjustHeight;
                telerikNav.sideBarElement.style.marginTop = -Math.min(scrollTop, adjustHeight) + "px";
                telerikNav.sideBarElement.style.height = (telerikNav.defaultSideBarHeight + adjustHeight) + "px";
            }
        }
    },
    addFooterToDom: function () {
        var containerElem = document.querySelector(this.footerSelector);
        containerElem.innerHTML = this.footerHtml;
    },
    displayNavigation: function () {
        var header = document.getElementById("js-tlrk-nav");
        if (!header) {
            var intervalLimit = 0;
            var intervalHeader = setInterval(function () {
                intervalLimit++;
                if (telerikNav.headerHtml !== '' || intervalLimit >= 50) {
                    telerikNav.addHeaderToDom();
                    clearInterval(intervalHeader);
                }
            }, 20);
        } else {
            var banner = document.getElementById("webinar-banner");
            var headerPlaceholder = document.getElementById('header-placeholder');

            // need to reposition header as the framework will always display the banner after the injected header.
            if (banner) {
                headerPlaceholder.after(header);
            }

            headerPlaceholder.style.display = 'none';
        }

        if (!document.querySelector(this.footerSelector).hasChildNodes()) {
            var intervalFooterLimit = 0;
            var intervalFooter = setInterval(function () {
                intervalFooterLimit++;
                if (telerikNav.footerHtml !== '' || intervalFooterLimit >= 50) {
                    telerikNav.addFooterToDom();
                    clearInterval(intervalFooter);
                }
            }, 20);
        }
    },
    initialize: function () {
        var currentUrl = location.href;
        var cdnEnvironment = "";
        var telerikNavigationVersion = "stable";
        var productName = "blazor-ui";
        var headerUrl = "";
        var footerUrl = "";

        if (currentUrl.indexOf("kendobuild") !== -1) {
            cdnEnvironment = "uat";
        }

        headerUrl = `https://${cdnEnvironment}cdn.telerik-web-assets.com/telerik-navigation/${telerikNavigationVersion}/nav-${productName}-csa-abs-component.html`;
        footerUrl = `https://${cdnEnvironment}cdn.telerik-web-assets.com/telerik-navigation/${telerikNavigationVersion}/footer-big-abs-markup.html`;

        fetch(headerUrl)
            .then(function (response) {
                return response.text();
            })
            .then(function (html) {
                window.telerikNav.headerHtml = html;
            })
            .catch(function () { });

        fetch(footerUrl)
            .then(function (response) {
                return response.text();
            })
            .then(function (html) {
                window.telerikNav.footerHtml = html;
            })
            .catch(function () { });

        window.telerikNav.displayNavigation();
    },
    dispose: function () {
        window.removeEventListener("resize", telerikNav.resizeSideBarOnWindowResize);
        document.removeEventListener("scroll", telerikNav.resizeSideBarOnScroll);
    }
};
