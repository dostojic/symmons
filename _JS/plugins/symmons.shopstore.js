var Symmons = Symmons || {};

! function ($) {
    "use strict";

    jQuery.fn.storeShopping = function (options) {
        return this.each(function () {
            var storeShopping = Object.create(Symmons.storeShopping);
            storeShopping.init(this, options);
        });
    };

    //Options
    jQuery.fn.storeShopping.options = {
        contentSelector2: "#stores-listing2",
        contentSelector3: "#stores-listing3",
        templateSelector2: "#template-storesListing2",
        templateSelector3: "#template-storesListing3",
        urlSelector2: jQuery("#stores-listing2").attr('data-ajax-url2'),
        urlSelector3: jQuery("#stores-listing3").attr('data-ajax-url'),
        findlocationBtnSelector: ".shop-store__findlocation",
        modifysearchBtnSelector: ".shop-store__modifysearch",
        preciselcoationBtnSelector: ".shop-store__preciselcoation",
        moreLocationsBtnSelector: ".stores-listing__cta",
        morelinksBtnSelector: ".stores-listing__morelinks",
        noRecords: ".no-records-found",
        scrollAmount: 10,
        searchLocationBox: "#txtLocationSearch",
        magnifyGlass: "#imgMagnifybtn",
        storeTab1: "#storeTab1",
        storeTab3: "#storeTab3",
        isPaginationEnabled: true,
        paginationObj: jQuery("#pagination-2 .pagination-content"),
        norecord2: "#norecord-2"
    };



    //Product Listing
    Symmons.storeShopping = {
        init: function (elem, options) {
            var self = this;
            var map;
            var geocoder = "";

            self.$container = jQuery(elem);
            self.options = jQuery.extend({}, jQuery.fn.storeShopping.options, options);
            self.$location = "";
            self.$preciseLocation = "";
            self.$searchOptions = "";
            self.$latitude = "";
            self.$longitude = "";
            self.$storeType = "";
            self.$storeLocations = [];
            self.$storeMoreLocations = [];
            self.$boundArrayList = [];
            self.$pageNum = 1;
            self.$locationAddress = "";
            self.bindHandlers();

            if (navigator.geolocation) {
                jQuery(".shop-store__preciselcoation").addClass("active");
            }


        },

        bindHandlers: function (event) {
            var self = this;

            jQuery(document).on("click touchend", self.options.moreLocationsBtnSelector, function (event) {
                event.preventDefault();
                self.$storeType = jQuery(this).data("store-type");
                self.bindMorelocationAjaxCall();
            });
            jQuery(document).on("click touchend", self.options.morelinksBtnSelector, function (event) {
                event.preventDefault();
                self.$storeType = jQuery(this).data("store-type");
                self.bindMorelocationAjaxCall();
            });

            jQuery(document).on("click touchend", self.options.modifysearchBtnSelector, function (event) {
                event.preventDefault();

                self.bindShowTabActive("section-1");
                self.bindHeader("section-1", "");
                self.bindGoogleMapsInit();
            });

            //Search - Text Box Enter
            jQuery(document).on("keypress", self.options.searchLocationBox, function (event) {

                if (event.keyCode == 13) {

                    self.validateSearchBox(this);
                }

            });

            jQuery(document).on("click touchend", self.options.magnifyGlass, function (event) {
                event.preventDefault();
                self.validateSearchBox(this);
            });


            jQuery(document).on("click touchend", self.options.storeTab1, function (event) {
                event.preventDefault();
                self.bindShowTabActive("section-1");
                self.bindHeader("section-1", "");
                self.bindGoogleMapsInit();
            });


            jQuery(document).on("click touchend", self.options.findlocationBtnSelector, function (event) {
                event.preventDefault();
                self.validateSearchBox();
            });

            jQuery(document).on("click touchend", self.options.preciselcoationBtnSelector, function (event) {
                event.preventDefault();
                self.bindPreciseLocation();
            });

            google.maps.event.addDomListener(window, 'load', self.bindGoogleMapsInit);
        },

        validateSearchBox: function () {
            var self = this;
            var isValidate = false;
            var strSearchOptions = jQuery("input:checked").length;
            jQuery(self.options.searchLocationBox).keydown(function () {
                jQuery(".invalid-location").hide();
            });

            if (jQuery(self.options.searchLocationBox).val() != "" && strSearchOptions >= 1) {
                jQuery(".invalid-location").hide();
                jQuery(".invalid-checkbox").hide();
                isValidate = true;
            } else {
                if (jQuery(self.options.searchLocationBox).val() == "") {
                    jQuery(".invalid-location").show();
                }

                if (strSearchOptions == 0) {
                    jQuery(".invalid-checkbox").show();
                }
                isValidate = false;
            }

            if (isValidate) {

                if (strSearchOptions == 1) {
                    self.$storeType = jQuery("input[name='chkSearchOptions']:checked").val();

                    self.bindFindLatLongAddress("step3");
                } else {
                    self.$location = jQuery(self.options.searchLocationBox).val();
                    self.bindFindLatLongAddress("step2");
                }


            }

        },

        bindFindLatLongAddress: function (strStepVal) {
            var self = this;
            var coords = "";
            var strLatitudeVal = "";
            var strLongitudeVal = "";

            var geocoder = new google.maps.Geocoder();
            var addressVal = jQuery(self.options.searchLocationBox).val();
            geocoder.geocode({
                    address: addressVal
                },
                function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        coords = new google.maps.LatLng(
                            results[0]['geometry']['location'].lat(),
                            results[0]['geometry']['location'].lng()
                        );
                        jQuery("#hdnLatitudeValues").val(coords.lat());
                        jQuery("#hdnLongitudeValues").val(coords.lng());
                        if (strStepVal == "step2") {
                            self.bindLocationAjaxCall();
                        } else {
                            self.bindMorelocationAjaxCall();
                        }
                    }

                }
            );


        },
        bindGoogleMapLocations: function (data) {
            var self = this;

            var mapOptions = {
                zoom: 12,
                center: new google.maps.LatLng(-33.9, 151.2),
                zoomControl: false,
                zoomControlOptions: {
                    style: google.maps.ZoomControlStyle.SMALL
                },
                mapTypeControl: false
            }
            var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
            self.$storeLocations = [];

            jQuery(data.StoresListingData).each(function (i, itemVal) {
                self.$storeLocations.push([data.StoresListingData[i].Store.StoreName, parseFloat(data.StoresListingData[i].Store.Latitude), parseFloat(data.StoresListingData[i].Store.Longitude), i + 1, data.StoresListingData[i].Store.StoreMapPin]);
            });

            self.bindGoogleMarkers(map, self.$storeLocations);


        },
        bindGoogleMapMoreLocations: function (data) {
            var self = this;

            var mapOptions = {
                zoom: 12,
                center: new google.maps.LatLng(-33.9, 151.2),
                zoomControl: false,
                zoomControlOptions: {
                    style: google.maps.ZoomControlStyle.SMALL
                },
                mapTypeControl: false
            }
            var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
            self.$storeMoreLocations = [];

            jQuery(data.MoreStores).each(function (i, itemVal) {
                self.$storeMoreLocations.push([data.MoreStores[i].StoreName, parseFloat(data.MoreStores[i].Latitude), parseFloat(data.MoreStores[i].Longitude), i + 1, data.MoreStores[i].StoreMapPin]);

            });
            self.bindGoogleMarkers(map, self.$storeMoreLocations);

        },


        bindsetAllMapMarkers: function (map, markers) {
            var self = this;
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(map);
            }
        },

        bindGoogleMarkers: function (map, tempLocations) {
            var self = this;
            self.$boundArrayList = [];

            for (var i = 0; i < tempLocations.length; i++) {
                var stores = tempLocations[i];
                var myLatLng = new google.maps.LatLng(stores[1], stores[2]);

                self.$boundArrayList.push(new google.maps.LatLng(stores[1], stores[2]));

                var marker = new google.maps.Marker({
                    position: myLatLng,
                    map: map,
                    icon: stores[4],
                    title: stores[0],
                    zIndex: stores[3]
                });
            }

            //console.log(self.$boundArrayList);
            var boundArrayList = self.$boundArrayList;
            var bounds = new google.maps.LatLngBounds();
            for (var i = 0, LtLgLen = boundArrayList.length; i < LtLgLen; i++) {
                bounds.extend(boundArrayList[i]);
            }
            map.fitBounds(bounds);

            jQuery(".store-loading-wrapper").removeClass("active");


        },
        bindPreciseLocation: function () {
            var self = this;

            var strSearchOptions = jQuery("input:checked").length;

            if (strSearchOptions == 0) {
                jQuery(".invalid-checkbox").show();
                return false;
            } else if (strSearchOptions == 1) {
                self.$location = "";
                self.bindFindLatLongAddress();
                self.$storeType = jQuery("input[name='chkSearchOptions']:checked").val();
                self.bindMorelocationAjaxCall();
            } else {
                jQuery(".invalid-checkbox").hide();

                function location(position) {
                  var pos = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                  self.$preciseLocation = pos;
                  jQuery("#hdnLatitudeValues").val(position.coords.latitude);
                  jQuery("#hdnLongitudeValues").val(position.coords.longitude);
                  self.bindCurrentLocationAdress(position.coords.latitude, position.coords.longitude);
                }

                function APIGeolocation() {
                  var key = $('#geolocation-key').val() || '';

                  if (!key.length) {
                    alert('No Geolocation key found!');
                    return;
                  }

                  $.post( "https://www.googleapis.com/geolocation/v1/geolocate?key=" + key, function(success) {
                		location({coords: {latitude: success.location.lat, longitude: success.location.lng}});
                  })
                  .fail(function(err) {
                    alert("API Geolocation error! \n\n");
                    console.log('API Geolocation Error!', err);
                  });
                }

               function locationSuccess(l) {
                 location(l);
               }

               function locationFail(error) {
                 switch (error.code) {
                    case error.TIMEOUT:
                      alert("Browser geolocation error !\n\nTimeout.");
                      break;
                    case error.PERMISSION_DENIED:
                      if(error.message.indexOf("Only secure origins are allowed") == 0) {
                        APIGeolocation();
                      }
                      break;
                    case error.POSITION_UNAVAILABLE:
                      alert("Browser geolocation error !\n\nPosition unavailable.");
                      break;
                  }
               }

                if (navigator.geolocation) {
                    navigator.geolocation.getCurrentPosition(
                      locationSuccess,
                      locationFail,
                      {
                        maximumAge: 50000,
                        timeout: 20000,
                        enableHighAccuracy: true
                      });
                } else {
                    // Browser doesn't support Geolocation
                    self.handleNoGeolocation(false);
                }
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
                        //self.$locationAddress = results[0].formatted_address;
                        //sublocality
                        self.$currentLocality_long = results[0].address_components[3].long_name;
                        self.$currentLocality_short = results[0].address_components[3].short_name;

                        //State
                        self.$currentState_short = results[0].address_components[5].long_name;
                        self.$currentState_short = results[0].address_components[5].short_name;

                        self.$locationAddress = self.$currentLocality_long + ", " + self.$currentState_short;

                        console.log(self.$locationAddress);
                        self.bindLocationAjaxCall();
                    } else {
                        console.log('Google did not return any results.');
                    }
                } else {
                    console.log("Reverse Geocoding failed due to: ", status);
                }


            });
        },

        bindGoogleMapsInit: function (isMarker, data) {
            var self = this;
            //geocoder = new google.maps.Geocoder();
            jQuery(".shop-store__rightcols").addClass("active");
            jQuery(".store-loading-wrapper").removeClass("active");
            /*   self.$latitude = jQuery("#hdnLatitudeValues").val();
               self.$longitude = jQuery("#hdnLongitudeValues").val();*/

             function location(position) {
               self.$latitude = position.coords.latitude;
               self.$longitude = position.coords.longitude;

               var mapOptions = {
                   zoom: 15,
                   center: new google.maps.LatLng(position.coords.latitude, position.coords.longitude),
                   zoomControl: false,
                   zoomControlOptions: {
                       style: google.maps.ZoomControlStyle.SMALL
                   },
                   mapTypeControl: false
               }

               var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
               var myLatLng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);


               var marker = new google.maps.Marker({
                   position: myLatLng,
                   map: map
               });

               map.setCenter(myLatLng);
             }

             function APIGeolocation() {
               var key = $('#geolocation-key').val() || '';

               if (!key.length) {
                 alert('No Geolocation key found!');
                 return;
               }

               $.post( "https://www.googleapis.com/geolocation/v1/geolocate?key=" + key, function(success) {
             		location({coords: {latitude: success.location.lat, longitude: success.location.lng}});
               })
               .fail(function(err) {
                 alert("API Geolocation error! \n\n");
                 console.log('API Geolocation Error!', err);
               });
             }

            function locationSuccess(l) {
              location(l);
            }

            function locationFail(error) {
              switch (error.code) {
                 case error.TIMEOUT:
                   alert("Browser geolocation error !\n\nTimeout.");
                   break;
                 case error.PERMISSION_DENIED:
                   if(error.message.indexOf("Only secure origins are allowed") == 0) {
                     APIGeolocation();
                   }
                   break;
                 case error.POSITION_UNAVAILABLE:
                   alert("Browser geolocation error !\n\nPosition unavailable.");
                   break;
               }
            }

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(
                  locationSuccess,
                  locationFail,
                  {
                    maximumAge: 50000,
                    timeout: 20000,
                    enableHighAccuracy: true
                  })
            } else {
                // Browser doesn't support Geolocation
                self.handleNoGeolocation(false);
            }
        },

        handleNoGeolocation: function (errorFlag) {
            var self = this;
            if (errorFlag) {
                console.log('Error: The Geolocation service failed.');
            } else {
                console.log('Error: Your browser doesn\'t support geolocation.');
            }
        },

        bindCheckBoxVal: function () {
            var self = this;

            var checkedValues = [];
            jQuery.each(jQuery("input[name='chkSearchOptions']:checked"), function () {
                checkedValues.push(jQuery(this).val());
                self.$searchOptions = checkedValues;
            });
        },



        bindMorelocationAjaxCall: function () {
            var self = this;
            self.$latitude = jQuery("#hdnLatitudeValues").val();
            self.$longitude = jQuery("#hdnLongitudeValues").val();
            // Make the AJAX call
            jQuery.ajax({
                url: self.options.urlSelector3 + "?pageNum=" + self.$pageNum + "&store=" + self.$storeType + "&latitude=" + self.$latitude + "&longitude=" + self.$longitude,
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
                        jQuery(self.options.noRecords).hide();
                        jQuery(".no-records-found").hide();
                        jQuery(".shop-store__rightcols").addClass("active");
                        jQuery(self.options.contentSelector3).show();
                        self.bindMoreStoresListing(data);

                        if (self.options.isPaginationEnabled) {
                            self.bindPagination();
                            jQuery(self.options.contentSelector3).data('paginate').refresh(1, self.options.totalPages);
                        }
                    } else {
                        jQuery(self.options.noRecords).show();
                        jQuery(".no-records-found").show();
                        jQuery(".shop-store__rightcols").removeClass("active");
                        jQuery(self.options.contentSelector3).hide();
                        if (self.options.isPaginationEnabled) {
                            self.options.paginationObj.hide();
                        }
                        $loading.hide();
                    }

                }
            });
        },

        bindLocationAjaxCall: function () {
            var self = this;
            jQuery(self.options.findlocationBtnSelector).addClass("disabled");
            jQuery(".store-loading-wrapper").addClass("active");
            self.$latitude = jQuery("#hdnLatitudeValues").val();
            self.$longitude = jQuery("#hdnLongitudeValues").val();
            if (self.$location == "") {
                self.$location = self.$locationAddress;
            }
            self.bindCheckBoxVal();
            // Make the AJAX call
            jQuery.ajax({

                url: self.options.urlSelector2 + "?location=" + self.$location + "&latitude=" + self.$latitude + "&longitude=" + self.$longitude + "&searchOptions=" + self.$searchOptions,
                type: "GET",
                cache: true,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                error: function (msg) {
                    console.log("Error" + msg.responseText);
                },
                success: function (data) {
                    self.bindStoreTypeListing(data);
                    jQuery(self.options.findlocationBtnSelector).removeClass("disabled");
                }
            });
        },

        bindShowTabActive: function (selectedTab) {
            var self = this;
            jQuery(".shop-store__leftcols").removeClass("active");
            jQuery(".shop-store").attr("data-section", selectedTab);
            jQuery(".shop-store__leftcols[data-content='" + selectedTab + "']").addClass("active");

        },

        bindHeader: function (selectedTab, data) {
            var self = this;
            if (selectedTab == "section-2") {
                jQuery(".shop-store__header").attr("style", "display:table");
                if (data.LocationNearTitle != "") {
                    jQuery(".shop-store__locationnear").html(data.LocationNearTitle);
                }
                if (data.LocationNear != "") {
                    jQuery(".shop-store__location").html(data.LocationNear);
                }

            } else if (selectedTab == "section-3") {
                jQuery(".shop-store__header").attr("style", "display:table");
                if (data.LocationNearTitle != "") {
                    jQuery(".shop-store__locationnear").html(data.StoreTypeName + " " + data.LocationNearTitle);
                }
                if (data.LocationNear != "") {
                    jQuery(".shop-store__location").html(data.LocationNear);
                }

            } else {
                jQuery(".shop-store__header").attr("style", "display:none");


            }

        },
        bindStoreTypeListing: function (data) {
            var self = this;

            //Math Helper
            Handlebars.registerHelper("math", function (lvalue, operator, rvalue, options) {
                lvalue = parseFloat(lvalue);
                rvalue = parseFloat(rvalue);
                return {
                    "+": lvalue + rvalue,
                    "-": lvalue - rvalue,
                    "*": lvalue * rvalue,
                    "/": lvalue / rvalue,
                    "%": lvalue % rvalue
                }[operator];
            });

            self.bindShowTabActive("section-2");
            self.bindHeader("section-2", data);

            self.options.totalRecords = jQuery(data)[0].TotalRecordCount;

            if (self.options.totalRecords > 0) {
                jQuery("#norecord-2").hide();
                jQuery(".shop-store__rightcols").addClass("active");
                jQuery(self.options.contentSelector2).show();
                //Compiler
                var $container = jQuery(self.options.templateSelector2),
                    $theTemplate = Handlebars.compile($container.html());
                jQuery(self.options.contentSelector2).html("").append($theTemplate(eval(data)));
                self.bindGoogleMapLocations(data);

            } else {
                // jQuery(self.options.noRecords).show();
                jQuery("#norecord-2").show();
                jQuery(".shop-store__rightcols").removeClass("active");
                jQuery(self.options.contentSelector2).hide();
            }


        },

        bindMoreStoresListing: function (data) {
            var self = this;

            self.bindShowTabActive("section-3");
            self.bindHeader("section-3", data);

            //Math Helper
            Handlebars.registerHelper("math", function (lvalue, operator, rvalue, options) {
                lvalue = parseFloat(lvalue);
                rvalue = parseFloat(rvalue);
                return {
                    "+": lvalue + rvalue,
                    "-": lvalue - rvalue,
                    "*": lvalue * rvalue,
                    "/": lvalue / rvalue,
                    "%": lvalue % rvalue
                }[operator];
            });

            self.bindGoogleMapMoreLocations(data);
            var $container = jQuery(self.options.templateSelector3),
                $theTemplate = Handlebars.compile($container.html());
            jQuery(self.options.contentSelector3).html("").append($theTemplate(eval(data)));
            $loading.hide();

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
            self.$latitude = jQuery("#hdnLatitudeValues").val();
            self.$longitude = jQuery("#hdnLongitudeValues").val();


            jQuery(self.options.contentSelector3).paginate({
                query: 'pageNum',
                pagerData: 'pager',
                pageData: 'page-1',
                next: 'next',
                prev: 'prev',
                first: 'first',
                last: 'last',
                pageInput: $('.pagination__input_1'),
                updatePagerState: true,
                totalPagesElem: $('.pagination__totalpages_1'),
                ajaxURLData: 'ajax-url',
                onBefore: function () {
                    url = new Url(jQuery(self.options.contentSelector3).attr('data-ajax-url'));
                    url.query['store'] = self.$storeType;
                    url.query['latitude'] = self.$latitude;
                    url.query['longitude'] = self.$longitude;

                    jQuery(self.options.contentSelector3).attr('data-ajax-url', url);
                },
                onSuccess: function (data) {
                    jQuery(self.options.contentSelector3).html("");
                    self.bindMoreStoresListing(data);

                }


            });
        }

    };
}(jQuery);
