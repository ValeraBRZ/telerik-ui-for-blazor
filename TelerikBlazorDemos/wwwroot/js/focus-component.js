window.focusFirstComponent = {
    focusComponent: function (evt) {
        if (focusFirstComponent.selector && evt.altKey === true && evt.keyCode === 87) {
            // Alt + W, only for demos that provide a selector to focus
            var elementToFocus = document.querySelector('#demo-runner ' + focusFirstComponent.selector);
            if (elementToFocus) {
                elementToFocus.focus();
            }
        }
    },
    selector: "",
    setSelector: function (currSelector) {
        focusFirstComponent.selector = currSelector;
    }
};

document.addEventListener("keydown", focusFirstComponent.focusComponent);
