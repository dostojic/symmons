// BEGIN: Extend jQuery
var myData = "1";
var strCounter = 1;
var __window = jQuery(window);
var screenSize = __window.width();

jQuery.extend({
    isDefined: function (obj) {
        return typeof obj !== 'undefined';
    },
    exists: function (obj) {
        return jQuery(obj).length !== 0 ? true : false;
    }
});

function addExpandedDesignText(triggerSelector, objClassName, showText, hideText) {

    jQuery(document).click('click touchend', triggerSelector, function (event) {
        event.preventDefault();
        //var $this = jQuery(triggerSelector),
        var $thisParent = jQuery(this).parent().parent();

        if ($thisParent.hasClass(objClassName)) {
            $thisParent.removeClass(objClassName);

            if (showText != "") {
                jQuery(this).html(showText);
            }
        } else {
            //jQuery("." + objClassName).removeClass(objClassName);
            $thisParent.addClass(objClassName);

            if (hideText != "") {
                jQuery(this).html(hideText);
            }

        }
    });
}

function getParameterName(name) {
    var self = this;
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

function addExpanded(objVal, objClassName, boolHideCurrent, hideAll, isToggle) {
    jQuery(document).on('click touchend', objVal, function (event) {
        var $this = jQuery(this);

        if (!isToggle) {
            if ($this.hasClass(objClassName)) {
                $this.removeClass(objClassName);
            } else {
                if (hideAll) {
                    jQuery("." + objClassName).removeClass(objClassName);
                } else {
                    $this.removeClass(objClassName);
                }
                $this.addClass(objClassName);
                if (boolHideCurrent) {
                    $this.hide();
                }
            }
        } else {
            $this.addClass(objClassName);
        }
    });
}



function addExpandedText(objVal, objClick, objClassName, showText, hideText, isSlideToggle, isDataAttr) {

    jQuery(objClick).click(function (event) {
        event.preventDefault();
        var $thisContainer = jQuery(objVal),
            $this = jQuery(objClick);
        if ($this.hasClass(objClassName)) {
            $this.removeClass(objClassName);
            if (isSlideToggle) {
                $thisContainer.slideToggle("fast");
            }
            if (showText != "") {
                $this.html(showText);
            }
            if (isDataAttr) {
                $thisContainer.attr("data-attr-showhide", "false");
            }




        } else {
            jQuery("." + objClassName).removeClass(objClassName);
            $this.addClass(objClassName);
            if (isSlideToggle) {
                $thisContainer.slideToggle("fast");
            }
            if (hideText != "") {
                $this.html(hideText);
            }
            if (isDataAttr) {
                $thisContainer.attr("data-attr-showhide", "true");
            }
        }
    });
}


jQuery.noConflict();

var slider = jQuery('.design-studio-slider').bxSlider();

jQuery(document).ready(function ($) {

    jQuery(".header__rightcols").showHide({
        triggerSelector: ".show-subitem",
        contentSelector: ".header__util-item__subitem.support-subitem",
        startClosed: true,
        openedClass: "support-open",
        closedClass: "support-closed"
    });

    jQuery(".header__util-content").showHide({
        triggerSelector: ".nav-support",
        contentSelector: ".support-navigation",
        startClosed: true,
        openedClass: "is-supportopen",
        closedClass: "is-supportclosed"
    });

    jQuery(".header__util-content").showHide({
        triggerSelector: ".nav-menu",
        contentSelector: ".menu-navigation",
        startClosed: true,
        openedClass: "is-menuopen",
        closedClass: "is-menuclosed"
    });

    jQuery(".header__rightcols").showHide({
        triggerSelector: ".search-item",
        contentSelector: ".header__searchbox",
        startClosed: true,
        openedClass: "is-searchopen",
        closedClass: "is-searchclosed"
    });

    jQuery(".search-item").on("click", function() {
        jQuery(".header__searchfield").focus();
    });

    jQuery(".primary-navigation__item").showHide({
        triggerSelector: ".primary-navigation__link",
        contentSelector: ".primary-navigation__subitem",
        startClosed: true,
        openedClass: "is-primaryNavopen",
        closedClass: "is-primaryNavClosed"
    });


    jQuery(".faq-listing__item").showHide({
        triggerSelector: ".faq-listing__group",
        contentSelector: ".faq-listing__wrap",
        startClosed: true,
        openedClass: "is-faqOpen",
        closedClass: "is-faqClosed",
        bodyClicked: false
    });

    jQuery(".browse-products__tabs").tabSelection({
        triggerSelector: ".browse-products__item",
        contentSelector: ".browse-products__segment",
        contentTabSelector: ".browse-products__tabscontent",
        backButton: ".back-btn",
        activeClass: 'active',
        triggeredClass: 'triggered',
        urlParamName: 'tab',
        isMobileEnabled: true
    });


    jQuery(".where-buy__tabs").tabSelection({
        triggerSelector: ".where-buy__item",
        contentSelector: ".where-buy__segment",
        contentTabSelector: ".where-buy__tabscontent",
        backButton: ".back-btn",
        activeClass: 'active',
        triggeredClass: 'triggered',
        urlParamName: 'tab',
        isMobileEnabled: false
    });

    // truncate text
    $('.browse-products__segment--teaser, .hover-content__teaser').each(function(index, e) {
      var element = $(e);
      var text = $.trim(element.text());
      var limitedText = text.substr(0, 150);
      var words = limitedText.split(' ');
      var limitedWords = words.splice(0, words.length - 1);
      var newLimitedText = limitedWords.join(' ');

      element.text(newLimitedText + '...');
    });

    if (jQuery.exists(".product-section")) {
        jQuery(".product-section").parent().addClass("product-container")
    }

    if (jQuery.exists(".gallery-thumbnail__listing")) {
        jQuery(".gallery-thumbnail__listing").galleryListing();
    }

    if (jQuery.exists(".product-comparelist")) {
        jQuery(".product-comparelist").compareProducts();
    }

    if (jQuery.exists(".product-comparelist__table")) {
        jQuery(".product-comparelist__table").compareProducts({
            isCompareTable: true
        });
    }

    jQuery(".product-comparelist").showHide({
        triggerSelector: ".product-comparelist__title",
        contentSelector: ".product-comparelist__listing",
        startClosed: true,
        openedClass: "open",
        closedClass: "closed",
        bodyClicked: false
    });


    if (jQuery.exists(".generic-listing")) {
        jQuery(".generic-listing").genericListing({
            isPaginationEnabled: true,
            pageLimit: 10
        });
    }

    if (jQuery.exists(".sales-reps__listing")) {
        jQuery(".sales-reps__listing").salesRepListing({
            isPaginationEnabled: true,
            pageLimit: 12
        });
    }

    if (jQuery.exists(".search-results__contentlisting")) {
        jQuery(".search-results__contentlisting").contentSearchListing({
            isPaginationEnabled: true,
            pageLimit: 10
        });
    }

    if (jQuery.exists("#productBrowseListing")) {
        jQuery("#productBrowseListing").productBrowse({
            isPaginationEnabled: true,
            pageLimit: 10,
            itemCount: ".product-browse__itemcount"
        });
    }

    if (!jQuery.exists(".hero-slider") && !jQuery.exists(".page-banner")) {
        jQuery("body").removeClass("header-static");
    }

    if (jQuery.exists("#txtLocationSearch")) {
        //jQuery("#txtLocationSearch").geocomplete();
    }
    //Slider

    if (jQuery.exists(".product-listing")) {
        jQuery(".product-listing").productSlider({
            isMobile: true,
            killSlider: true
        });

    }

    jQuery(".compare-back").click(function (event) {
        event.preventDefault();
        history.back(1);
    });

    jQuery(".browse-products__segment--item").swipe({
        excludedElements: "button, input, select, textarea, .noSwipe",
        swipeLeft: function (event, direction, distance, duration, fingerCount, fingerData) {
            jQuery(this).addClass("swiped-left").removeClass("swiped-right");
        },
        swipeRight: function (event, direction, distance, duration, fingerCount, fingerData) {
            jQuery(this).addClass("swiped-right").removeClass("swiped-left");

        }

    });


    jQuery("#txtWhereBuy").keydown(function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if (jQuery.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
            // Allow: Ctrl+A
            (e.keyCode == 65 && e.ctrlKey === true) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });


    if (jQuery.exists(".product-gallery")) {
        jQuery(".product-gallery").productSlider({
            isMobile: false,
            isPagerCustom: true,
            adaptiveHeight: false
        });
    }

    if (jQuery.exists(".collection-listing")) {
        if (jQuery(".section-content-wrapper").length === 0) {
            jQuery(".collection-listing__heading").hide();
        }
    }

    if (jQuery.exists(".hero-slider__container")) {
        jQuery(".hero-slider__container").heroSlider({
            swapBg: true
        });
    }

    if (jQuery.exists(".collection-slider")) {
        jQuery(".collection-slider").heroSlider({
            swapBg: false,
            duration: jQuery('.collection-slider[data-slider]').attr('data-duration'),
            startRandom: jQuery('.collection-slider[data-slider]').attr('data-start-random')
        });
    }


    if (jQuery.exists(".product-features")) {
        jQuery(".product-features").toggleContent({
            toggleMode: 'height',
            showMoreObject: jQuery('.show-features'),
            showContainerHeight: 190
        });
    }

    if (jQuery.exists(".product-links-wrapper")) {
        jQuery(".product-links-wrapper").toggleContent({
            toggleMode: 'list',
            showMoreObject: jQuery('.show-downloads')
        });
    }

    if (jQuery.exists(".support-links-wrapper")) {
        jQuery(".support-links-wrapper").toggleContent({
            toggleMode: 'list',
            showMoreObject: jQuery('.show-support-links')
        });
    }

    if (jQuery.exists(".frm__contactus")) {
        jQuery(".frm__contactus").contactUs();
    }


    if (jQuery.exists(".show-diagram")) {
        addExpanded(".show-diagram", "expanded", true, true, false);
    }

    if (jQuery.exists(".shop-store__help")) {
        addExpanded(".shop-store__help", "expanded", false, false, true);

        jQuery(document).on('click', ".shop-store__close", function (event) {
            event.preventDefault();
            jQuery(this).parent().parent().find(".shop-store__help").removeClass("expanded");
        });

    }

    if (jQuery.exists(".shop-store")) {
        jQuery(".shop-store").storeShopping();
    }

    if (jQuery.exists(".product-details__pricing")) {
        jQuery("body").productDetails();
    }

    if (jQuery.exists(".product-smart-listing")) {

        if (jQuery('.product-smart-listing > a').length == 1) {
            jQuery('.product-smart-listing > a').addClass("disabled");
        } else {
            jQuery('.product-smart-listing > a').removeClass("disabled");
        }

        jQuery('.product-smart-listing > a').click(function () {
            if (jQuery('.product-smart-listing > a').length > 1) {
                var tab_id = jQuery(this).attr('data-tab');

                jQuery('.product-smart-listing > a').removeClass('active');
                jQuery('.tab-content').removeClass('active');

                jQuery(this).addClass('active');
                jQuery(".tab-content-wrapper").find("[data-tab='" + tab_id + "']").addClass("active");
            }

        });
    }



    if (jQuery.exists(".catalog-finishes")) {
        /*
            if (screenSize <= 768) {
              jQuery('.catalog-finishes').on('click', function () {
                jQuery(this).next().addClass("active");
                setTimeout(function () {
                  jQuery(".design-option__list-finishes").removeClass("active");
                }, 1000);
              });
            } else {

              jQuery(".catalog-finishes").hover(function () {
                  jQuery(this).next().addClass("active");
                },
                function () {
                  setTimeout(function () {
                    jQuery(".design-option__list-finishes").removeClass("active");
                  }, 300);

                }
              );
            }*/
    }


    //equalize the row height for icon listing
    $('.icon-listing__item .icon-listing__link').matchHeight();



    // equalize design studio row heights
    $('.design-option__row-zero').matchHeight();
    $('.design-option__row-one').matchHeight();
    $('.design-option__row-two').matchHeight();
    $('.design-option__row-three').matchHeight();
    $('.design-option__row-four').matchHeight();
    $('.design-option__row-five').matchHeight();

    // launch videos in a modal
    $('.video-modal').magnificPopup({
        disableOn: 700,
        type: 'iframe',
        mainClass: 'mfp-fade',
        removalDelay: 160,
        preloader: false,
        fixedContentPos: false
    });



    if (jQuery.exists(".design-option__heading--mobile")) {
        var strShowTxt = jQuery(".design-option__heading--mobile").data("show-text"),
            strHideTxt = jQuery(".design-option__heading--mobile").data("hide-text");
        addExpandedDesignText(".design-option__heading--mobile", "expanded", strShowTxt, strHideTxt);
    }




    //Swapping the two image sources

    var imageSourceSwap = function () {
        var $this = jQuery(this).find("img");
        var newSource = $this.data('hover-src');
        if (newSource !== "" && newSource !== undefined) {
            $this.data('hover-src', $this.attr('src'));
            $this.attr('src', newSource);
        }
    };

    jQuery('.icon-listing__link').hover(imageSourceSwap, imageSourceSwap);
    jQuery('.footer__socialmedia--links > a').hover(imageSourceSwap, imageSourceSwap);



    /*  if (jQuery.exists(".scfForm")) {

        //SKU - Design studio
        var strSKUval = getParameterName("sku");
        if (strSKUval != "") {
          jQuery(".sku-panel").addClass("active");
          jQuery('.sku-panel .text-box.single-line').val("#" + strSKUval).attr("readonly", "true");
        } else {
          jQuery(".sku-panel").removeClass("active");
        }

        //File upload

        jQuery('.field-optional.field-title').html(jQuery('.field-optional.field-title').html() + " (Optional)");
        jQuery('.field-attach-file.field-title').html(jQuery('.field-attach-file.field-title').html() + " (Optional)");
        jQuery('.field-attach-file.field-border').first().addClass("show");
        jQuery('.label-add-another-file.field-title').click(function (event) {
          event.preventDefault();
          jQuery('.field-attach-file.field-border:eq(' + eval(strCounter + 1) + ')').addClass("show");
          strCounter = strCounter + 1;

        });

      }*/
});

jQuery(window).resize(function () {

    if ($('.studio-slider-wrap').length > 0) {
        slider.reloadSlider();
    }

});