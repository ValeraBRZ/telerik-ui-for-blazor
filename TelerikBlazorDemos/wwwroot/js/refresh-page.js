(function() {

    document.addEventListener("click", function (e) {
        var reconnectModal = document.getElementById("components-reconnect-modal");

        if (reconnectModal && e.target.parentElement === reconnectModal && e.target.tagName === "BUTTON") {
            refreshPage();
        }
    })

    function refreshPage() {
        window.location.reload();
    }

})();
