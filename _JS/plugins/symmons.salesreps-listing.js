var Symmons = Symmons || {};

! function ($) {
  "use strict";

  jQuery.fn.salesRepListing = function (options) {
    return this.each(function () {
      var salesRepListing = Object.create(Symmons.salesRepListing);
      salesRepListing.init(this, options);
    });
  };

  //Options
  jQuery.fn.salesRepListing.options = {
    contentSelector: "#salesrep-listing",
    templateSelector: "#template-salesRepListing",
    urlSelector: jQuery("#salesrep-listing").attr('data-ajax-url'),
    stateSelector: ".sales-reps__state",
    totalRecords: "",
    totalPages: "",
    itemCount: "",
    noRecords: ".no-records-found",
    isPaginationEnabled: true,
    paginationObj: jQuery("#pagination-3 .pagination-content"),
    pageLimit: 10,
    storeTab3: "#storeTab3",
    scrollAmount: 10,
    ajaxSpinner: "#loader-3"


  };

  //Product Listing
  Symmons.salesRepListing = {
    init: function (elem, options) {
      var self = this;
      self.$container = jQuery(elem);
      self.options = jQuery.extend({}, jQuery.fn.salesRepListing.options, options);
      self.$selectedState = "";
      self.$latitude = "";
      self.$longitude = "";

      jQuery(self.options.noRecords).hide();
      self.$pageNum = 1;

      self.bindHandlers();
      self.browserSupportFlag = new Boolean();

      var isTab3 = jQuery(self.options.storeTab3).hasClass('active');
      if (isTab3) {
          self.bindTab3Content();
      }
    },

    bindHandlers: function (event) {
      var self = this;

      jQuery(document).on("change", self.options.stateSelector, function (event) {
        self.bindSelectedState(this);
      });

      jQuery(document).on("click", self.options.storeTab3, function (event) {
        event.preventDefault();
        self.bindTab3Content()
      });
    },

    bindTab3Content: function () {
        var self = this;
        self.$selectedState = "";
        self.$selectedStateCode = "";
        jQuery(self.options.contentSelector).hide();
        if (self.options.isPaginationEnabled) {
            self.options.paginationObj.hide();
        }
        self.bindDetectlocation();
    },

    bindSelectedState: function (strParam) {
      var self = this;
      self.$selectedState = jQuery(strParam).val();
      self.bindSalesRepListAjax();
    },

    bindDetectlocation: function (errorFlag) {
      var self = this;
     jQuery(self.options.ajaxSpinner).hide();
      // Try W3C Geolocation (Preferred)
      if (navigator.geolocation) {
        self.browserSupportFlag = true;

        navigator.geolocation.getCurrentPosition(function (position) {
          //initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
          //self.$latitude = position.coords.latitude;
          //self.$longitude = position.coords.longitude;
           //jQuery(self.options.ajaxSpinner).show();
          jQuery(self.options.ajaxSpinner).show();
          self.bindCurrentLocationAdress(position.coords.latitude, position.coords.longitude)
          

        }, function () {
          self.bindHandleNoGeolocation(self.browserSupportFlag);
        });
      } else {
        self.browserSupportFlag = false;
        self.bindHandleNoGeolocation(self.browserSupportFlag);
      }
    },

    bindCurrentLocationAdress: function (latitude, longitude) {
      var self = this;

      var geocoder = new google.maps.Geocoder();
      var yourLocation = new google.maps.LatLng(latitude, longitude);
      
      self.$location = "";
      geocoder.geocode({
        'latLng': yourLocation
      }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
          if (results[0]) {
            self.$locationAddress = self.$currentLocality_long + ", " + self.$currentState_short;

            for (var i = 0; i < results[0].address_components.length; i++) {
              for (var j = 0; j < results[0].address_components[i].types.length; j++) {
                if (results[0].address_components[i].types[j] == 'administrative_area_level_1') {
                  self.$currentState_short = results[0].address_components[i].short_name;

                }
              }
            }
            self.$selectedStateCode = self.$currentState_short;
            self.bindSalesRepListAjax();
          } else {
            console.log('Google did not return any results.');
          }
        } else {
          console.log("Reverse Geocoding failed due to: ", status);
        }


      });
    },


    bindHandleNoGeolocation: function (errorFlag) {
      var self = this;
      if (errorFlag == true) {
        console.log("Geolocation service failed.");
      } else {
         console.log("Your browser doesn't support geolocation.");
      }
      
      self.bindSalesRepListAjax();
    },

    bindSalesRepListAjax: function () {
      var self = this;

      jQuery.ajax({
        url: self.options.urlSelector + "?pageNum=" + self.$pageNum + "&state=" + self.$selectedState + "&statecode=" + self.$selectedStateCode,
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
          if (data.SelectedStateId != null && data.SelectedStateId != "") {

            jQuery(self.options.stateSelector).val(data.SelectedStateId);

          }
          if (self.options.totalRecords > 0) {
            jQuery(self.options.noRecords).hide();
            jQuery(self.options.contentSelector).show();
            if (self.options.isPaginationEnabled) {
              self.options.paginationObj.show();
            }
            self.bindSalesRepListing(data);

            if (self.options.isPaginationEnabled) {

              if (self.options.isPaginationEnabled) {
                if (self.options.totalPages == "1") {
                  self.options.paginationObj.hide();
                } else {
                  self.options.paginationObj.show();
                }
              }

              if (!self.paginationLoaded) {
                self.bindPagination();
              }
              self.paginationLoaded = true;
              jQuery(self.options.contentSelector).data('paginate').refresh(1, self.options.totalPages);
            }
          } else {
            jQuery(self.options.noRecords).show();
            jQuery(self.options.ajaxSpinner).hide();
            jQuery(self.options.contentSelector).hide();
            if (self.options.isPaginationEnabled) {
              self.options.paginationObj.hide();
            }

          }
        }
      });
    },




    bindPagination: function () {
      var self = this;

      jQuery(self.options.contentSelector).paginate({
        query: 'pageNum',
        pagerData: 'pager',
        pageData: 'page',
        next: 'next',
        prev: 'prev',
        first: 'first',
        last: 'last',
        pageInput: $('.pagination__input'),
        updatePagerState: true,
        totalPagesElem: $('.pagination__totalpages'),
        ajaxURLData: 'ajax-url',
        onBefore: function () {
          url = new Url(jQuery(self.options.contentSelector).attr('data-ajax-url'));
          url.query['state'] = self.$selectedState;

          jQuery(self.options.contentSelector).attr('data-ajax-url', url);
        },
        onSuccess: function (data) {
          self.bindSalesRepListing(data);

        }
      });
    },

    bindSalesRepListing: function (data) {
      var self = this;
      var $container = jQuery(self.options.templateSelector),
        $theTemplate = Handlebars.compile($container.html());
      if (self.options.isPaginationEnabled) {
        self.options.paginationObj.show();
      }
      jQuery(self.options.contentSelector).html("").append($theTemplate(eval(data)));
      jQuery(self.options.ajaxSpinner).hide();
    },

  };
}(jQuery);