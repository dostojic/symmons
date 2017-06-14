var Symmons = Symmons || {};

! function ($) {
    "use strict";

    jQuery.fn.productDetails = function (options) {
        return this.each(function () {
            var productDetails = Object.create(Symmons.productDetails);
            productDetails.init(this, options);
        });
    };

    jQuery.fn.productDetails.options = {
            $currency: "$",
            $grandTotal: ".ttl-price-list",
            $productPlumbingFlowRateObj: "rdlFlowRate",
            $productPlumbingOthersObj: "rdlOthers",
            $productPlumbingLabel: ".plumbing-addl-price",

            $productAvailValesObj: ".product-valves",
            $productAvailValesLabel: ".product-variant-additional-price",

            $productFinishObj: "rdlfinishDetails",

            $productSKUObj: ".product-sku",
            $templateSelector: "#template-pricingInformation",
            $pricingObj: ".product-details__pricing",
            $urlSelector: jQuery(".product-details__pricing").attr('data-ajax-url')
                // $productFinishUrl = jQuery(self.options.$pricingObj).data("ajax-url")

        },
        Symmons.productDetails = {
            init: function (elem, options) {
                var self = this;
                self.$container = jQuery(elem);
                self.options = jQuery.extend({}, jQuery.fn.productDetails.options, options);

                self.getDetailsbyAjax();
                self.bindHandlers();

                setTimeout(function () {
                    jQuery(".gallery-content").removeClass("slide-effect")
                }, 1500);

            },

            bindHandlers: function (event) {
                var self = this;

                jQuery(".gallery-content").addClass("slide-effect");

                jQuery(document).on("change", self.options.$productAvailValesObj, function (event) {
                    event.preventDefault();
                    self.updateValves();
                });

                jQuery(document).on("click", "input[name=" + self.options.$productPlumbingFlowRateObj + "]", function (event) {
                    self.updatePlumbing('flow-rate');
                    self.updateFinish();
                });

                jQuery(document).on("click", "input[name=" + self.options.$productPlumbingOthersObj + "]", function (event) {
                    self.updatePlumbing('others');
                    self.updateFinish();
                });

                jQuery(document).on("click", "input[name=" + self.options.$productFinishObj + "]", function (event) {
                    self.getDetailsbyAjax('onclick', event);
                });
                
                self.handleWTBClick();

                jQuery(document).on("click", ".ps-lightbox-close", function () {
                    self.handleWTBClick();
                });
            },

            handleWTBClick: function(event) {
                var interval = setInterval(function () {
                    var isClicked = jQuery(".ps-widget").hasClass("ps-open");

                    if (isClicked) {
                        ga('send', 'event', 'Product Detail Page', 'WTB Button Click');

                        clearInterval(interval);
                    }
                }, 1000);
            },

            getDetailsbyAjax: function (action, event) {
                var self = this;

                jQuery(".product-loader").addClass("show");
                var selectedFinish = self.getRadioBtnVal(self.options.$productFinishObj, "", true);
                var strShowPlumbingOption = "";
                if (jQuery(".product-plumbing-content").attr("data-attr-showhide") !== undefined) {

                    strShowPlumbingOption = jQuery(".product-plumbing-content").attr("data-attr-showhide");
                } else {
                    strShowPlumbingOption = "false";
                }




                // Make the AJAX call
                jQuery.ajax({
                    url: self.options.$urlSelector + "?finishType=" + selectedFinish + "&showPlumbingOption=" + strShowPlumbingOption,
                    type: "GET",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "JSON",
                    async: true,
                    error: function (msg) {
                        console.log("Error" + msg.responseText);
                    },
                    success: function (data) {
                        self.bindPricingValues(data);

                        if (strShowPlumbingOption == "true") {
                            jQuery(".product-plumbing-content").show();
                            jQuery(".plumbing-head").html(jQuery(".plumbing-head").data("hide-text"));
                            jQuery(".plumbing-head").addClass("expanded");
                        } else {
                            jQuery(".product-plumbing-content").hide();
                            jQuery(".plumbing-head").html(jQuery(".plumbing-head").data("show-text"));
                            jQuery(".plumbing-head").removeClass("expanded");
                        }
                        if (jQuery.exists(".product-details__plumbing")) {
                            var strShowTxt = jQuery(".plumbing-head").data("show-text"),
                                strHideTxt = jQuery(".plumbing-head").data("hide-text");
                            addExpandedText(".product-plumbing-content", ".plumbing-head", "expanded", strShowTxt, strHideTxt, true, true);

                        }

                        jQuery(self.options.$productAvailValesLabel).html(self.options.$currency + (+self.getElementValue(self.options.$productAvailValesObj)).toFixed(2));

                        //if (action == "onclick") {
                        self.updateProductSlider(data);
                        jQuery(".gallery-content").addClass("slide-effect");
                        // }
                        //

                        var updatedSKU = self.updateSKU();

                        $('.product-details__model').text('Model #' + updatedSKU);
                        $('.ps-widget').attr('ps-sku', updatedSKU);
                    }
                });


            },

            bindPricingValues: function (data) {
                var self = this;

                var $container = jQuery(self.options.$templateSelector),
                    $containerHtml = $container.html();

                if (typeof $containerHtml !== 'undefined' && $containerHtml.length) {
                  var $theTemplate = Handlebars.compile($containerHtml);

                  jQuery(self.options.$pricingObj).html(" ");
                  jQuery(self.options.$pricingObj).append($theTemplate(eval(data)));

                  self.updateFinish();
                }
            },

            updateProductSlider: function (data) {
                var self = this,
                    strMainsliderImage = "",
                    strThumbSliderImage = "",
                    isActive = "";

                if (data.SliderImages != null) {
                    jQuery(data.SliderImages).each(function (i) {
                        strMainsliderImage += "<li class='product-gallery__item'><figure class='product-gallery__image'>        <img class='b-lazy' src='" + data.SliderImages[i].ImagePath + "' data-src='" + data.SliderImages[i].ImagePath + "' alt='" + data.SliderImages[i].ImageAlt + "' /></figure></li>";

                        if (i == 0) {
                            strThumbSliderImage += "<a  data-slide-index='" + i + "' class='product-thumbnail-listing__item active'>      <figure class='product-thumbnail-listing__thumbimage'><img class='b-lazy' src='" + data.SliderImages[i].ImagePath + "' data-src='" + data.SliderImages[i].ImagePath + "' alt='" + data.SliderImages[i].ImageAlt + "' /></figure> </a>";
                        } else {
                            strThumbSliderImage += "<a  data-slide-index='" + i + "' class='product-thumbnail-listing__item'>      <figure class='product-thumbnail-listing__thumbimage'><img class='b-lazy' src='" + data.SliderImages[i].ImagePath + "' data-src='" + data.SliderImages[i].ImagePath + "' alt='" + data.SliderImages[i].ImageAlt + "' /></figure> </a>";
                        }
                    });


                    mySlider.destroySlider();

                    jQuery('.product-gallery').html("");
                    jQuery('.product-gallery').html(strMainsliderImage);

                    jQuery('.product-thumbnail-listing').html("");
                    jQuery('<div id="bx-pager" class="product-thumbnail-listing">' + strThumbSliderImage + '</div>').appendTo(jQuery('.gallery-content'));



                    jQuery(".product-gallery").productSlider({
                        isMobile: false,
                        isPagerCustom: true,
                        adaptiveHeight: false,
                        autoRotate: false,
                        duration: "0",
                        pagerCustomObj: "#bx-pager",
                        killSlider: false,
                        showcontrols: true,


                    });

                } else {

                    var emptyContent = "<div class='bx-wrapper'><figure class='product-gallery__image' style='margin-top:38%;'><img src='/_Images/contentManaged/imagenotavailable.gif' alt='Image Not Available'/></figure> </div>";
                    mySlider.destroySlider();
                    jQuery('.product-gallery').html("").append(emptyContent);
                }

                setTimeout(function () {
                    jQuery(".gallery-content").removeClass("slide-effect");
                }, 1500);

                jQuery(".product-loader").removeClass("show").addClass("hide");
            },

            getElementValue: function (objVal) {
                var self = this;
                return jQuery(objVal).val();
            },

            getRadioBtnVal: function (rdlObjName, dataAttrKey, isVal) {
                var self = this,
                    radioVal = "";

                if (!isVal) {
                    radioVal = jQuery("input[name=" + rdlObjName + "]:checked").data(dataAttrKey);
                } else {
                    radioVal = jQuery("input[name=" + rdlObjName + "]:checked").val();
                }

                return radioVal;
            },

            updateTotal: function () {
                var self = this;
                var val1 = parseFloat(self.getRadioBtnVal(self.options.$productFinishObj, "attr-price", false)),
                    val2 = 0,
                    val3 = 0;

                if (jQuery(self.options.$productAvailValesObj).length != 0) {
                    val2 = parseFloat(jQuery(self.options.$productAvailValesObj).val());
                } else {
                    val2 = 0;
                }

                if (jQuery(self.options.$productPlumbingLabel).length != 0) {
                    val3 = parseFloat(jQuery(self.options.$productPlumbingLabel).data("attr-price"));
                } else {
                    val3 = 0;
                }

                var $grandTotal = eval(val1 + val2 + val3).toFixed(2);
                jQuery(self.options.$grandTotal).html(self.options.$currency + $grandTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','));

            },

            updatePlumbing: function (srcFrom) {
                var self = this;
                var plumbFlowRate = 0;
                var plumbOthersRate = 0;
                if (jQuery(".product-plumbing-flowrate .check-box-row").length != 0) {
                    plumbFlowRate = parseFloat(self.getRadioBtnVal(self.options.$productPlumbingFlowRateObj, "price-value", false));
                } else {
                    plumbFlowRate = 0;
                }

                if (jQuery(".product-plumbing-others .check-box-row").length != 0) {
                    plumbOthersRate = parseFloat(self.getRadioBtnVal(self.options.$productPlumbingOthersObj, "price-value", false));
                } else {
                    plumbOthersRate = 0;
                }

                // var  plumbPriceVal = eval(plumbFlowRate != 'NaN' ? plumbFlowRate : 0 + plumbOthersRate != 'NaN' ? plumbOthersRate : 0).toFixed(2);
                var plumbPriceVal = eval(plumbFlowRate + plumbOthersRate).toFixed(2);
                jQuery(self.options.$productPlumbingLabel).html("+ " + self.options.$currency + plumbPriceVal);
                jQuery(self.options.$productPlumbingLabel).data("attr-price", plumbPriceVal);

            },

            updateValves: function () {
                var self = this;
                jQuery(self.options.$productAvailValesLabel).html(self.options.$currency + jQuery(self.options.$productAvailValesObj).val());
                self.updateFinish();
            },


            updateSKU: function () {
                var self = this,
                    modelNumber = $('.product-details__model').data('model'),
                    skuFinish = self.getRadioBtnVal(self.options.$productFinishObj, "sku-value", false) || "",
                    skuValve = jQuery(self.options.$productAvailValesObj).find(':selected').data('attr-code') || "",
                    skuFlowRate = self.getRadioBtnVal(self.options.$productPlumbingFlowRateObj, "sku-value", false) || "",
                    skuOthers = self.getRadioBtnVal(self.options.$productPlumbingOthersObj, "sku-value", false) || "",
                    strSKUVal = skuFinish || "",
                    strSKUValOther = [skuValve, skuFlowRate, skuOthers];

                strSKUValOther = strSKUValOther.filter(function(item) {
                    return item !== "" ? item : null;
                });

                var invalidSku = false;
                $.each(strSKUValOther, function(index, item) {
                    if (item.toString().toLowerCase() === 'any finish') {
                        invalidSku = true;
                        return false;
                    }
                });

                if (invalidSku || !skuFinish.length) {
                    strSKUVal = modelNumber;
                } else {
                    strSKUValOther.unshift(skuFinish);
                    strSKUVal = strSKUValOther.join('-');
                }

                jQuery(this.options.$productSKUObj).html(strSKUVal);

                return strSKUVal;
            },

            updateFinish: function () {
                var self = this;

                self.updatePlumbing();
                self.updateSKU();
                self.updateTotal();



            }
        };
}(jQuery);
