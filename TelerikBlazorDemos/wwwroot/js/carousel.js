(function () {
    window.carousel = window.carousel || {};

    window.carousel = {
        init: function (options) {
            this.options = options;

            this.bindEvents();

            return this.getGroupsSettings();
        },
        bindEvents: function () {
            this.resizeHandler = this.resize.bind(this);
            window.addEventListener('resize', this.resizeHandler);
        },
        unbindEvents: function () {
            window.removeEventListener('resize', this.resizeHandler);
        },
        destroy: function () {
            this.unbindEvents();
        },
        resize() {
            const that = this;

            clearTimeout(this.activateResizeCarousel);

            this.activateResizeCarousel = setTimeout(function () {
                that.invokeResizeAsync();
            }, 100);
        },
        invokeResizeAsync() {
            if (this.options.dotNetInstance) {
                const groupSettings = this.getGroupsSettings();

                if (groupSettings) {
                    this.options.dotNetInstance.invokeMethodAsync(this.options.resizeEvent, groupSettings);
                }
            }
        },
        getGroupsSettings() {
            const slidesCount = document.querySelectorAll(".kd-carousel-slide").length;
            const container = document.querySelector(".kd-carousel-slides");
            const activeSlide = document.querySelector(".kd-carousel-slide");
            const visibleSlidesCount = parseInt(container.offsetWidth / activeSlide.offsetWidth, 10);

            const groupsCount = Math.ceil(slidesCount / visibleSlidesCount);

            if (groupsCount && visibleSlidesCount) {
                return {
                    groupsCount: groupsCount,
                    itemsInGroup: visibleSlidesCount
                };
            }

            return null;
        }
    }
})();
