(function() {
    window.scrollNavLinkIntoView = (href) => {
        const sidebar = document.querySelector(".kd-sidebar");
        const anchor = sidebar ? sidebar.querySelector("a[href='" + href + "']") : null;

        if (anchor) {
            anchor.scrollIntoView({
                block: "center"
            });
        }
    };

    window.updateUrlHash = (hashValue) => {
        let urlWithoutHash = window.location.href.split("#")[0];
        history.replaceState(null, "", urlWithoutHash + "#" + hashValue);
    }

    window.openHomePageTab = () => {
        const hashValue = window.location.hash;
        if (hashValue == "") {
            return;
        }

        const anchor = document.querySelector("[data-id='" + hashValue.substring(1) + "']");
        if (!anchor) {
            return;
        }

        anchor.click();

        const sectionHeading = anchor.closest(".kd-main-wrapper")?.querySelector("h2");
        if (sectionHeading) {
            let headerHeight = document.getElementById("webinar-banner").offsetHeight +
                60 + // telerik.com header
                document.querySelector(".kd-sub-nav").offsetHeight;
            window.scrollTo(0, sectionHeading.offsetTop - headerHeight);
        }
    };

    window.initFeedbackForm = () => {
        const feedbackForm = document.querySelector("feedback-form");
        const isHidden = feedbackForm && feedbackForm.shadowRoot.querySelector(".p-ff-hiden");

        if (isHidden && feedbackForm.reinit instanceof Function) {
            feedbackForm.reinit();
        }
    }
})();
