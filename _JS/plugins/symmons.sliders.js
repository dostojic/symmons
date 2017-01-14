var Symmons = Symmons || {};
var mySlider = "";
! function ($) {
    "use strict";

    jQuery.fn.productSlider = function (options) {
        return this.each(function () {
            var productSlider = Object.create(Symmons.productSlider);
            productSlider.init(this, options);
        });
    };

    jQuery.fn.productSlider.options = {
        isMobile: false,
        adaptiveHeight: false,
        autoRotate: false,
        duration: "0",
        startRandom: jQuery('.product-listing[data-slider]').attr('data-start-random'),
        isPagerCustom: "",
        pagerCustom: "",
        pagerCustomObj: "#bx-pager",
        slideCount: '',
        killSlider: false,
        showcontrols: true

    };

    Symmons.productSlider = {

        init: function (elem, options) {
            var self = this;
            self.$sliderRendered = false;
            self.$container = jQuery(elem);
            self.options = jQuery.extend({}, jQuery.fn.productSlider.options, options);
            self.objSlider = null;


            self.bindElements();
            self.bindHandlers();

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

        bindElements: function () {
            var self = this;
            self.$currentSlider = self.$container.find(self.options.slider);
        },

        bindHandlers: function () {
            var self = this;




            jQuery(window).resize(function () {

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

                        if (self.options.killSlider) {
                            //self.killSlider();
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
                self.options.showcontrols = true;
            } else {
                self.options.slideCount = false;
                self.options.showcontrols = false;
            }

            mySlider = self.$container.bxSlider({
                mode: 'horizontal',
                adaptiveHeight: self.options.adaptiveHeight,
                controls: self.options.showcontrols,
                pagerCustom: self.options.pagerCustom,
                auto: self.options.autoRotate,
                pause: self.options.duration,
                autoHover: true,
                pager: self.options.slideCount,
                randomStart: (self.options.startRandom == '') ? true : false,
                onSliderLoad: function () {
                    var bLazy = new Blazy({
                        selector: '.b-lazy' 
                    });
                }
            });


        },

        killSlider: function () {
            var self = this;
            mySlider.destroySlider();

            setTimeout(function () {
                self.removeInineStyles();
            }, 100);


        },

        removeInineStyles: function () {
            var self = this;
            jQuery(self.$container).removeAttr("style")
            jQuery(self.$container).children().removeAttr("style");
        },

        reloadSlider: function () {
            var self = this;
            setTimeout(function () {
                mySlider.reloadSlider();
            }, 50);
        }
    };
}(jQuery);