(function () {
    window.googleAnalytics = window.googleAnalytics || {};

    window.googleAnalytics = {
        submit: function (vote) {
            const dataLayer = window.dataLayer || [];
            dataLayer.push({
                'event': 'virtualEvent',
                'eventCategory': 'feedback',
                'eventAction': vote,
                'eventLabel': window.location.href
            });
        },
        trackSpaNavigation: function () { // GA tracking for SPA
            if (window.location.host == "demos.telerik.com") {
                window.dataLayer = window.dataLayer || [];
                window.dataLayer.push({
                    'event': 'spaPageview',
                });
            }
        }
    }
})();
