var Symmons = Symmons || {};

! function ($) {
    "use strict";

    jQuery.fn.genericListing = function (options) {
        return this.each(function () {
            var genericListing = Object.create(Symmons.genericListing);
            genericListing.init(this, options);
        });
    };

    //Options
    jQuery.fn.genericListing.options = {
        contentSelector: ".generic-listing",
        templateSelector: "#template-genericListing",
        urlSelector: jQuery(".generic-listing").attr('data-ajax-url'),
        totalRecords: "",
        totalPages: "",
        isPaginationEnabled: false,
        paginationObj: jQuery(".pagination-content")
    };

    //Gallery Listing
    Symmons.genericListing = {

        init: function (elem, options) {
            var self = this;
            jQuery(".no-records-found").hide();
            self.$container = jQuery(elem);
            self.options = jQuery.extend({}, jQuery.fn.genericListing.options, options);

            $loading.show();


            if (self.options.isPaginationEnabled) {
                self.options.urlSelector = self.options.urlSelector + "?pageNum=1";
            }

            self.makeAjaxCall();
        },

        bindElements: function () {
            var self = this;

        },

        bindHandlers: function () {
            var self = this;


        },

        makeAjaxCall: function () {
            var self = this;

            // Make the AJAX call
            jQuery.ajax({
                url: self.options.urlSelector,
                type: "GET",
                cache: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                error: function (msg) {
                    console.log("Error" + msg.responseText);
                },
                success: function (data) {
                    self.options.totalRecords = jQuery(data)[0].TotalRecordCount;
                    self.options.totalPages = jQuery(data)[0].TotalPagesCount;
                    

                    if (self.options.totalRecords > 0) {
                        self.bindgenericListing(data);
                        if (self.options.isPaginationEnabled) {
                            self.bindPagination();
                            jQuery(self.options.contentSelector).data('paginate').refresh(1, self.options.totalPages);
                        }
                        self.bindElements();
                        self.bindHandlers();
                    } else {
                        jQuery(self.options.noRecords).show();
                        jQuery(".no-records-found").show();
                        jQuery(self.options.contentSelector).hide();
                        if (self.options.isPaginationEnabled) {
                            self.options.paginationObj.hide();
                        }
                        $loading.hide();
                    }

                }
            });

        },

        bindPagination: function () {
            var self = this;
           
            if (self.options.isPaginationEnabled) {
                if (self.options.totalPages == "1") {
                    self.options.paginationObj.hide();
                } else {
                    self.options.paginationObj.show();
                }
            }

            jQuery(self.options.contentSelector).paginate({
              
                onSuccess: function (data) {
                    jQuery(self.options.contentSelector).html("");
                    self.bindgenericListing(data);

                }
            });
        },

        bindgenericListing: function (data) {
            var self = this;
            var $container = jQuery(self.options.templateSelector),
              $theTemplate = Handlebars.compile($container.html());
            jQuery(self.options.contentSelector).append($theTemplate(eval(data)));

            //self.loadListing(data);
            $loading.hide();
        }


    }

}(jQuery);