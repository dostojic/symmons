var Symmons = Symmons || {};

! function ($) {
    "use strict";

    jQuery.fn.heroSlider = function (options) {
        return this.each(function () {
            var heroSlider = Object.create(Symmons.heroSlider);
            heroSlider.init(this, options);
        });
    };

    jQuery.fn.heroSlider.options = {
        isMobile: false,
        adaptiveHeight: false,
        autoRotate: true,
        duration: jQuery('.hero-slider__container[data-slider]').attr('data-duration'),
        startRandom: jQuery('.hero-slider__container[data-slider]').attr('data-start-random'),
        isPagerCustom: "",
        pagerCustom: "",
        pagerCustomObj: "#bx-pager",
        slideCount: '',
        bgImageURL: ".hero-slider__image",
        swapBg: false

    };

    Symmons.heroSlider = {

        init: function (elem, options) {
            var self = this;
            self.$sliderRendered = false;
            self.$container = jQuery(elem);
            self.options = jQuery.extend({}, jQuery.fn.heroSlider.options, options);
            self.objSlider = null;


            self.bindElements();
            self.bindHandlers();

            if (self.options.swapBg) {
                self.bindBackgroundImage();
            }


            if (self.options.isPagerCustom) {
                if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {
                    self.options.pagerCustom = false;
                } else {
                    self.options.pagerCustom = self.options.pagerCustomObj;
                }
            }

            if (self.options.isMobile) {
                if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {
                    self.bindSlider();
                    self.$sliderRendered = true;
                }
            } else {
                self.bindSlider();
                self.$sliderRendered = true;
            }

        },




        bindBackgroundImage: function () {
            var self = this;

            jQuery(self.options.bgImageURL).each(function (i, itemVal) {
                if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {
                    jQuery(itemVal).attr("style", "background-image:url('" + jQuery(itemVal).data('attr-mobileimage') + "')");
                } else {
                    jQuery(itemVal).attr("style", "background-image:url('" + jQuery(itemVal).data('attr-desktopimage') + "')");
                }
            });
        },

        bindElements: function () {
            var self = this;
            self.$currentSlider = self.$container.find(self.options.slider);
        },

        bindHandlers: function () {
            var self = this;
            jQuery(window).resize(function () {

                if (self.options.swapBg) {
                    self.bindBackgroundImage();
                }

                if (self.options.isMobile) {
                    if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {
                        if (self.options.isPagerCustom) {
                            self.options.pagerCustom = false;
                        }

                        if (!self.$sliderRendered) {
                            self.bindSlider();
                            self.$sliderRendered = true;
                        }

                    } else {
                        if (self.options.isPagerCustom) {
                            self.options.pagerCustom = self.options.pagerCustomObj;
                        }

                        self.$sliderRendered = false;


                    }
                }


            });
        },

        bindSlider: function () {
            var self = this;

            self.options.duration = self.options.duration * 1000;
            // Auto Rotate only if the duration is not 0
            if (self.options.duration == 0) {
                self.options.autoRotate = false;
            } else {
                self.options.autoRotate = true;
            }

            var strslideCount = jQuery(self.$container).find("li").length;
            if (strslideCount > 1) {
                self.options.slideCount = true;
            } else {
                self.options.slideCount = false;
            }

            self.$container.bxSlider({
                mode: 'horizontal',
                adaptiveHeight: self.options.adaptiveHeight,
                controls: true,
                pagerCustom: self.options.pagerCustom,
                auto: self.options.autoRotate,
                pause: self.options.duration,
                autoHover: true,
                touchEnabled: true,
                pager: self.options.slideCount,
                randomStart: (self.options.startRandom === '') ? true : false,
                onSlideAfter: function () {
                    self.$container.stopAuto();
                    self.$container.startAuto();
                }
            });

        }
    };
}(jQuery);