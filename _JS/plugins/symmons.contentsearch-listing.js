var Symmons = Symmons || {};

! function ($) {
  "use strict";

  jQuery.fn.contentSearchListing = function (options) {
    return this.each(function () {
      var contentSearchListing = Object.create(Symmons.contentSearchListing);
      contentSearchListing.init(this, options);
    });
  };

  //Options
  jQuery.fn.contentSearchListing.options = {
    contentSelector: ".search-results__contentlisting",
    templateSelector: "#template-contentsearchListing",
    urlSelector: jQuery(".search-results__contentlisting").attr('data-ajax-url'),
    totalRecords: "",
    totalPages: "",
    isPaginationEnabled: false,
    paginationObj: jQuery(".pagination-content"),
    contentSearchField: jQuery(".search-results__searchfield")
  };

  //Gallery Listing
  Symmons.contentSearchListing = {

    init: function (elem, options) {
      var self = this;
      jQuery(".search-results__noresults").hide();
      self.$container = jQuery(elem);
      self.options = jQuery.extend({}, jQuery.fn.contentSearchListing.options, options);
      self.paginationLoaded = false;
   self.strSearchSource = false;
      $loading.show();


      if (self.options.isPaginationEnabled) {
        self.options.urlSelector = self.options.urlSelector + "?pageNum=1";
      }
     
      self.bindElements();
      self.bindHandlers();
      self.makeAjaxCall();
    },

    bindElements: function () {
      var self = this;

    },

    bindHandlers: function () {
      var self = this;

      //Search - Text Box Enter
      jQuery(document).on("keypress", self.options.contentSearchField, function (event) {
        self.options.contentSearchField.parent().removeClass("error");
        if (event.keyCode == 13) {
          if (self.options.contentSearchField.val() != "") {
 self.strSearchSource = true;
            self.makeAjaxCall();
            event.preventDefault();
          } else {
            self.options.contentSearchField.parent().addClass("error");
            return false;
          }
        }
      });

      jQuery(".search-results__searchbox .img-global-search").click(function () {
        self.options.contentSearchField.parent().removeClass("error");
        if (self.options.contentSearchField.val() != "") {
 self.strSearchSource = true;
          self.makeAjaxCall();
        } else {
          self.options.contentSearchField.parent().addClass("error");
          return false;
        }

      });


    },


    makeAjaxCall: function () {
      var self = this;
      var strKeyword = jQuery(self.options.contentSearchField).val();
      // Make the AJAX call
      jQuery.ajax({
          url: self.options.urlSelector + "&keyword=" + strKeyword + "&fromSearch="+self.strSearchSource,
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


          jQuery(".spn-contentresults").html(self.options.totalRecords + " Content results");

           if (jQuery(data)[0].TotalProductCount != 0) {
              jQuery(".spn-productresults").html(", " + jQuery(data)[0].TotalProductCount + " Product results");
              jQuery(".search-results__backcta").show();

          } else {
              jQuery(".spn-productresults").html("");
              jQuery(".search-results__backcta").hide();
          }

          if (self.options.totalRecords > 0) {
              jQuery(self.options.noRecords).hide();
              jQuery(".search-results__noresults").hide();
            jQuery(self.options.contentSelector).show();
            self.bindcontentSearchListing(data);

            if (self.options.isPaginationEnabled) {
              jQuery(self.options.itemCount).html(self.options.totalRecords);
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
              jQuery(".search-results__noresults").show();
              var strHtml = jQuery(".txtKeyword").html();
              var strResult = "";
              
              if (strHtml.indexOf("@keyword") != -1) {
                  strResult = strHtml.replace(/@keyword/gi, "<span class='searchKey'>" + jQuery(self.options.contentSearchField).val() + "</span>");
                  jQuery(".txtKeyword").html(strResult);
              } else {
                  jQuery(".searchKey").text(jQuery(self.options.contentSearchField).val());
              }
             

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
        self.bindcontentSearchListing(data);

      }
    });
  },

  bindcontentSearchListing: function (data) {
    var self = this;
    jQuery(self.options.contentSelector).html("");
    var $container = jQuery(self.options.templateSelector),
      $theTemplate = Handlebars.compile($container.html());
    jQuery(self.options.contentSelector).append($theTemplate(eval(data)));

    //self.loadListing(data);
    $loading.hide();
  }


}

}(jQuery);