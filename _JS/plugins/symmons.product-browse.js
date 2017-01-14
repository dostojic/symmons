var Symmons = Symmons || {};

! function ($) {
  "use strict";

  jQuery.fn.productBrowse = function (options) {
    return this.each(function () {
      var productBrowse = Object.create(Symmons.productBrowse);
      productBrowse.init(this, options);
    });
  };

  //Options
  jQuery.fn.productBrowse.options = {
    contentSelector: "#productBrowseListing",
    contentFilterSelector: "#productFilterListing",
    contentAppliedFilterSelector: "#productAppliedFilters",
    templateSelector: "#template-productBrowseListing",
    templateFilterSelector: "#template-productFiltersListing",
    contentFilterCloseBy: ".product-browse__filtersclose",
    contentResetAllSelector: ".product-browse__filtersreset",
    contentRefinebuttonSelector: ".product-browse__refinebutton",
    contentCloseFilter: ".img-close-green",
    contentClearSearchVal: ".img-close-grey",
    contentSearchFilter: ".img-search-filters",
    contentCancelFilter: ".product-browse__cancelbutton",
    contentBrowseFilter: ".product-browse__filters",
    contentSearchField: ".product-browse__search",
    contentApplyButton: ".product-browse__applybutton",
    contentFiltersSection: ".product-browse__filters-applied",
    contentSortBySelector: ".product-browse__sortby--dropdown",
    urlSelector: jQuery("#productBrowseListing").attr('data-ajax-url'),
    urlFilterSelector: jQuery("#productFilterListing").attr('data-ajax-url'),
    totalRecords: "",
    totalPages: "",
    itemCount: "",
    noRecords: ".no-records-found",
    isPaginationEnabled: false,
    paginationObj: jQuery(".pagination-content"),
    pageLimit: 10,
    scrollAmount: 10


  };

  //Product Listing
  Symmons.productBrowse = {
    init: function (elem, options) {
      var self = this;
      self.$container = jQuery(elem);
      self.options = jQuery.extend({}, jQuery.fn.productBrowse.options, options);

      //As of now 5 filters - Fixed
      self.$selectedStylesArray = [];
      self.$selectedFinishArray = [];
      self.$selectedPriceArray = [];
      self.$selectedFeaturesArray = [];
      self.$selectedCollectionsArray = [];
      self.$selectedSegmentArray = [];
      self.$selectedFamilyArray = [];
      self.$selectedCategoryArray = [];
      self.$selectedAttributesArray = [];

      self.$appliedFiltersArray = [];
      self.$searchKeyword = "";
      self.$sortByVal = "";
      self.$strTemplate = "";
      $loading.show();
      jQuery(self.options.noRecords).hide();
      self.paginationLoaded = false;
      self.isResetAdded = false;
      self.isURLBinded = false;
      self.$pageNum = 1;

      self.$isHybrid = false;

      /* if (self.options.isPaginationEnabled) {
         self.options.urlSelector = self.options.urlSelector + "?pageNum="+self.$pageNum;
       }*/

      self.bindProductFilters();
      // self.bindProductListAjax();
    },

    bindHandlers: function (event) {
      var self = this;
      if (self.getUrlParameter("contentresultcount") != "")
        self.$isHybrid = true;

      jQuery(document).on("click", "input[type=checkbox]", function (event) {
        if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {

        } else {
          self.bindSelectedFilters();
          self.isURLBinded = false;
        }
      });

      jQuery(document).on("click", self.options.contentFilterCloseBy, function (event) {
        self.bindRemoveSelectedFilter(event, this);
      });

      jQuery(document).on("click", self.options.contentResetAllSelector, function (event) {
        self.bindResetAllFilter(this);
      });

      jQuery(document).on("click", self.options.contentRefinebuttonSelector, function (event) {
        jQuery(self.options.contentBrowseFilter).show();
      });

      jQuery(document).on("click", self.options.contentCloseFilter, function (event) {
        jQuery(self.options.contentBrowseFilter).hide();
      });

      jQuery(document).on("click", self.options.contentCancelFilter, function (event) {

        jQuery(self.options.contentBrowseFilter).hide();
        jQuery(".product-browse__filtershead").removeClass("expanded");
        //self.bindResetAllFilter();
      });


      jQuery(document).on("change", self.options.contentSortBySelector, function (event) {
        self.bindProductSorting(this);
        self.isURLBinded = false;
      });

      jQuery(document).on("click", self.options.contentSearchFilter, function (event) {
        if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {

        } else {
          self.bindProductSearch(this);
        }

      });

      jQuery(document).on("click", self.options.contentApplyButton, function (event) {
        self.bindSelectedFiltersMobile();
      });


      /* if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {
         jQuery(window).scroll(function () {
           if (jQuery(this).scrollTop() > self.options.scrollAmount) {
             jQuery(self.options.contentBrowseFilter).addClass('fixed');
           }
         });
       }*/

      //Search - Text Box Enter
      jQuery(document).on("keypress", self.options.contentSearchField, function (event) {
        if (jQuery(".self.options.contentSearchField").val() != "") {
          jQuery(".img-close-grey").addClass("show");
        } else {
          jQuery(".img-close-grey").removeClass("show");
        }
        if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {

        } else {
          //Search - Text Box Enter
          if (event.keyCode == 13) {
            self.bindProductSearch(this);
          }
        }
      });

      jQuery(document).on("click", self.options.contentClearSearchVal, function (event) {
        event.preventDefault();
        jQuery(self.options.contentSearchField).val("");
        jQuery(self.options.contentClearSearchVal).removeClass("show");

      });


      // jQuery(window).resize(function () {
      //   if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {
      //     //jQuery(self.options.contentBrowseFilter).hide();
      //     jQuery(self.options.contentFiltersSection).hide();
      //   } else {
      //     // jQuery(self.options.contentBrowseFilter).removeClass('fixed');
      //     jQuery(self.options.contentBrowseFilter).show();
      //     self.bindShowHideFiltersSection();
      //
      //   }
      //
      // });

      self.bindSelectedItem();
    },

    getUrlParameter: function (name) {
      var self = this;
      name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
      var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
      return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    },

    bindSelectedItem: function () {
      var self = this;
      var $collectionItemID = self.getUrlParameter("collection");
      var $featureItemID = self.getUrlParameter("feature");
      var $styleItemID = self.getUrlParameter("style");
      var $categoryItemID = self.getUrlParameter("category");
      var $segmentItemID = self.getUrlParameter("segment");
      var $attributeItemID = self.getUrlParameter("attribute");
      var $familyItemID = self.getUrlParameter("family");
      var $strKeyword = jQuery.trim(self.getUrlParameter("keyword"));


      if ($categoryItemID !== "") {
        self.$selectedCategoryArray.push($categoryItemID);
        var itemVal = jQuery('input:checkbox[name="chk_' + $categoryItemID + '"]');
        itemVal.prop('checked', true);
        self.$appliedFiltersArray.push(itemVal.data("attr-group") + "|" + itemVal.data("attr-filtername") + "|" + itemVal.val());
      }


      if ($segmentItemID !== "") {
        self.$selectedSegmentArray.push($segmentItemID);
        var itemVal = jQuery('input:checkbox[name="chk_' + $segmentItemID + '"]');
        itemVal.prop('checked', true);
        self.$appliedFiltersArray.push(itemVal.data("attr-group") + "|" + itemVal.data("attr-filtername") + "|" + itemVal.val());
      }

      if ($attributeItemID !== "") {
        self.$selectedAttributesArray.push($attributeItemID);
        var itemVal = jQuery('input:checkbox[name="chk_' + $attributeItemID + '"]');
        itemVal.prop('checked', true);
        self.$appliedFiltersArray.push(itemVal.data("attr-group") + "|" + itemVal.data("attr-filtername") + "|" + itemVal.val());
      }

      if ($familyItemID !== "") {
        self.$selectedFamilyArray.push($familyItemID);
        var itemVal = jQuery('input:checkbox[name="chk_' + $familyItemID + '"]');
        itemVal.prop('checked', true);
        self.$appliedFiltersArray.push(itemVal.data("attr-group") + "|" + itemVal.data("attr-filtername") + "|" + itemVal.val());
      }

      if ($collectionItemID !== "") {
        self.$selectedCollectionsArray.push($collectionItemID);
        var itemVal = jQuery('input:checkbox[name="chk_' + $collectionItemID + '"]');
        itemVal.prop('checked', true);
        self.$appliedFiltersArray.push(itemVal.data("attr-group") + "|" + itemVal.data("attr-filtername") + "|" + itemVal.val());
      }

      if ($featureItemID !== "") {
        self.$selectedFeaturesArray.push($featureItemID);
        var itemVal = jQuery('input:checkbox[name="chk_' + $featureItemID + '"]');
        itemVal.prop('checked', true);
        self.$appliedFiltersArray.push(itemVal.data("attr-group") + "|" + itemVal.data("attr-filtername") + "|" + itemVal.val());
      }

      if ($styleItemID !== "") {
        self.$selectedStylesArray.push($styleItemID);
        var itemVal = jQuery('input:checkbox[name="chk_' + $styleItemID + '"]');
        itemVal.prop('checked', true);
        self.$appliedFiltersArray.push(itemVal.data("attr-group") + "|" + itemVal.data("attr-filtername") + "|" + itemVal.val());
      }

      if ($strKeyword !== "") {
        jQuery(self.options.contentSearchField).val($strKeyword);
        self.$searchKeyword = $strKeyword;

      }
      self.bindProductSearch();

      //self.bindAppliedFilters();
    },

    bindRemoveSelectedFilter: function (event, strThis) {
      var self = this;

      if (jQuery(strThis).data("attr-type") != "search") {
        var selectedFilter = jQuery(strThis).parent().parent().data("attr-filterobj"),
          checkBoxID = selectedFilter.split("|")[2],
          groupType = selectedFilter.split("|")[0].split("__")[0];

        jQuery('input:checkbox[name="chk_' + checkBoxID + '"]').attr('checked', false);

        self.$appliedFiltersArray.splice(jQuery.inArray(selectedFilter, self.$appliedFiltersArray), 1);


        switch (groupType) {
        case 'Styles':
          self.$selectedStylesArray.splice(jQuery.inArray(checkBoxID, self.$selectedStylesArray), 1);
          break;
        case 'Finish':
          self.$selectedFinishArray.splice(jQuery.inArray(checkBoxID, self.$selectedFinishArray), 1);
          break;
        case 'Price Range':
          self.$selectedPriceArray.splice(jQuery.inArray(checkBoxID, self.$selectedPriceArray), 1);
          break;
        case 'Smart Features':
          self.$selectedFeaturesArray.splice(jQuery.inArray(checkBoxID, self.$selectedFeaturesArray), 1);
          break;
        case 'Collections':
          self.$selectedCollectionsArray.splice(jQuery.inArray(checkBoxID, self.$selectedCollectionsArray), 1);
          break;
        case 'Product Segment':
          self.$selectedSegmentArray.splice(jQuery.inArray(checkBoxID, self.$selectedSegmentArray), 1);
          break;
        case 'Product Family':
          self.$selectedFamilyArray.splice(jQuery.inArray(checkBoxID, self.$selectedFamilyArray), 1);
          break;
        case 'Product Category':
          self.$selectedCategoryArray.splice(jQuery.inArray(checkBoxID, self.$selectedCategoryArray), 1);
          break;
        case 'Special Attributes':
          self.$selectedAttributesArray.splice(jQuery.inArray(checkBoxID, self.$selectedAttributesArray), 1);
          break;
        }

        self.bindAppliedFilters();
      } else {
        self.$searchKeyword = "";
        jQuery(self.options.contentSearchField).val("");
        jQuery(strThis).parent().parent().remove();
        jQuery(self.options.contentClearSearchVal).removeClass("show");
        self.bindShowHideFiltersSection();
        self.bindProductListAjax();
      }


    },

    bindResetAllFilter: function () {
      var self = this;

      self.$selectedStylesArray = [];
      self.$selectedFinishArray = [];
      self.$selectedPriceArray = [];
      self.$selectedFeaturesArray = [];
      self.$selectedCollectionsArray = [];
      self.$selectedSegmentArray = [];
      self.$selectedFamilyArray = [];
      self.$selectedCategoryArray = [];
      self.$selectedAttributesArray = [];

      self.$appliedFiltersArray = [];

      jQuery(".product-browse__filterlist .custom-checkbox").attr("checked", false);

      self.$searchKeyword = "";
      jQuery(self.options.contentSearchField).val("");

      self.$sortByVal = "";
      jQuery(self.options.contentSortBySelector).val("High");

      self.bindAppliedFilters();

    },

    bindProductFilters: function () {
      var self = this;

      // Make the AJAX call
      jQuery.ajax({
        url: self.options.urlFilterSelector,
        type: "GET",
        cache: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        error: function (msg) {
          console.log("Error" + msg.responseText);
        },
        success: function (data) {
          // console.log(data);
          jQuery(self.options.contentFilterSelector).html("");
          self.bindFilterListing(data);

          if (jQuery.exists(".product-browse__filtershead")) {
            addExpanded(".product-browse__filtershead", "expanded", false, false);
          }
        }
      });
    },

    bindProductListAjax: function () {
      var self = this;

      var strStyles = self.$selectedStylesArray,
        strFinish = self.$selectedFinishArray,
        strPrice = self.$selectedPriceArray,
        strSmartFeatures = self.$selectedFeaturesArray,
        strCollections = self.$selectedCollectionsArray,
        strSegment = self.$selectedSegmentArray,
        strFamily = self.$selectedFamilyArray,
        strCategory = self.$selectedCategoryArray,
        strAttributes = self.$selectedAttributesArray;


      // Make the AJAX call
      jQuery.ajax({
        url: self.options.urlSelector + "?pageNum=" + self.$pageNum + "&sortby=" + self.$sortByVal + "&searchkey=" + self.$searchKeyword + "&styles=" + strStyles + "&finish=" + strFinish + "&price=" + strPrice + "&smartfeatures=" + strSmartFeatures + "&collections=" + strCollections + "&segment=" + strSegment + "&family=" + strFamily + "&category=" + strCategory + "&attributes=" + strAttributes + "&isHybrid=" + self.$isHybrid,
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
          jQuery(self.options.itemCount).html(self.options.totalRecords);
          self.$isHybrid = false;
          if (jQuery(data)[0].ContentListingCount != 0) {
            jQuery(".product-browse__content-count").show();
            jQuery(".product-browse__backcta").addClass("show");
            jQuery(".product-browse__backcta-desktop").removeClass("hide").addClass("show");
            jQuery(".product-browse__contentcount").html(", " + jQuery(data)[0].ContentListingCount);
          } else {
            jQuery(".product-browse__content-count").hide();
            jQuery(".product-browse__backcta").removeClass("show");
            jQuery(".product-browse__backcta-desktop").removeClass("show").addClass("hide");

          }

          if (self.options.totalRecords > 0) {
            jQuery(self.options.noRecords).hide();
            jQuery(self.options.contentSelector).show();
            self.bindProductListing(data);

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
            jQuery(self.options.contentSelector).hide();
            if (self.options.isPaginationEnabled) {
              self.options.paginationObj.hide();
            }
            $loading.hide();
          }
        }
      });
    },


    bindSelectedFiltersMobile: function (event) {
      var self = this;

      self.$searchKeyword = jQuery(self.options.contentSearchField).val();
      self.$searchKeyword = jQuery.trim(self.$searchKeyword);
      jQuery(self.options.contentBrowseFilter).hide();
      self.bindSelectedFilters();

      //reset values
      jQuery(".product-browse__filterlist .custom-checkbox").attr("checked", false);
      jQuery(self.options.contentSearchField).val("");
      jQuery(".product-browse__filtershead").removeClass("expanded");

    },
    bindSelectedFilters: function (event) {
      var self = this;
      self.$selectedStylesArray = [];
      self.$selectedFinishArray = [];
      self.$selectedPriceArray = [];
      self.$selectedFeaturesArray = [];
      self.$selectedCollectionsArray = [];
      self.$selectedSegmentArray = [];
      self.$selectedFamilyArray = [];
      self.$selectedCategoryArray = [];
      self.$selectedAttributesArray = [];

      self.$appliedFiltersArray = [];

      jQuery('input[type="checkbox"]:checked').each(function (i, itemVal) {



        var strGroupType = jQuery(itemVal).data("attr-group").split("__")[0];
        switch (strGroupType) {
        case 'Styles':
          self.$selectedStylesArray.push(jQuery(itemVal).val());
          break;
        case 'Finish':
          self.$selectedFinishArray.push(jQuery(itemVal).val());
          break;
        case 'Price Range':
          self.$selectedPriceArray.push(jQuery(itemVal).val());
          break;
        case 'Smart Features':
          self.$selectedFeaturesArray.push(jQuery(itemVal).val());
          break;
        case 'Collections':
          self.$selectedCollectionsArray.push(jQuery(itemVal).val());
          break;
        case 'Product Segment':
          self.$selectedSegmentArray.push(jQuery(itemVal).val());
          break;
        case 'Product Family':
          self.$selectedFamilyArray.push(jQuery(itemVal).val());
          break;
        case 'Product Category':
          self.$selectedCategoryArray.push(jQuery(itemVal).val());
          break;
        case 'Special Attributes':
          self.$selectedAttributesArray.push(jQuery(itemVal).val());
          break;
        }

        self.$appliedFiltersArray.push(jQuery(itemVal).data("attr-group") + "|" + jQuery(itemVal).data("attr-filtername") + "|" + jQuery(itemVal).val());

      });

      self.bindAppliedFilters();
    },

    bindShowHideFiltersSection: function () {
      var self = this;
      if (jQuery(self.$appliedFiltersArray).length > 0 || self.$searchKeyword != "") {
        jQuery(self.options.contentFiltersSection).show();
      } else {
        jQuery(self.options.contentFiltersSection).hide();
      }
    },

    bindAppliedFilters: function () {
      var self = this;
      self.$strTemplate = "";

      if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {

      } else {
        self.bindShowHideFiltersSection();
      }



      jQuery(self.$appliedFiltersArray).each(function (i, itemVal) {

        var data = itemVal.split("|");

        if (data[0] === 'undefined' ||
            data[1] === 'undefined' ||
            data[2] === 'undefined') {
              return false;
            }

        self.$strTemplate += "<li class='product-browse__filterslist--item' data-attr-filterobj='" + data[0] + "|" + data[1] + "|" + data[2] + "'><label class='product-browse__filterslabel'>";
        self.$strTemplate += data[1];
        self.$strTemplate += "<a class='product-browse__filtersclose' data-attr-type='options'></a></label></li>";
      });

      if (self.$searchKeyword != "") {
        self.$strTemplate += "<li class='product-browse__filterslist--item'><label class='product-browse__filterslabel'>"
        self.$strTemplate += "<span>" + self.$searchKeyword + "</span>";
        self.$strTemplate += "<a class='product-browse__filtersclose by-search' data-attr-type='search'></a></label></li>";
      }

      // if (!self.isResetAdded) {
      self.$strTemplate += "<li><a class='product-browse__filtersreset'>Reset All</a></li>";
      self.isResetAdded = true;
      // }

      jQuery(self.options.contentAppliedFilterSelector).html("").append(self.$strTemplate);

      self.bindProductListAjax();


    },

    bindProductSorting: function (strThis) {
      var self = this;
      self.$sortByVal = "";
      self.$sortByVal = jQuery(strThis).val();
      self.bindProductListAjax();

    },

    bindProductSearch: function (strThis) {
      var self = this;

      self.$strTemplate = "";
      self.$searchKeyword = jQuery(self.options.contentSearchField).val();
      self.$searchKeyword = jQuery.trim(self.$searchKeyword);

      self.bindShowHideFiltersSection();

      if (jQuery(".product-browse__filterslist").children().hasClass("search-key")) {
        jQuery(".search-key .product-browse__filterslabel span").html(self.$searchKeyword);
      } else {
        self.$strTemplate += "<li class='product-browse__filterslist--item search-key'><label class='product-browse__filterslabel'>"
        self.$strTemplate += "<span>" + self.$searchKeyword + "</span>";
        self.$strTemplate += "<a class='product-browse__filtersclose' data-attr-type='search'></a></label></li>";

        if (!self.isResetAdded) {
          self.$strTemplate += "<li><a class='product-browse__filtersreset'>Reset All</a></li>";
          self.isResetAdded = true;
        }



        if (jQuery(self.options.contentAppliedFilterSelector).children().length == 0) {
          jQuery(self.options.contentAppliedFilterSelector).append(self.$strTemplate);
        } else {

          jQuery(self.$strTemplate).insertBefore(jQuery(self.options.contentAppliedFilterSelector).children().last());
        }

      }
      self.bindAppliedFilters();

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
          url.query['sortby'] = self.$sortByVal;
          url.query['searchkey'] = self.$searchKeyword;
          url.query['styles'] = self.$selectedStylesArray.toString();;
          url.query['finish'] = self.$selectedFinishArray.toString();;
          url.query['price'] = self.$selectedPriceArray.toString();;
          url.query['smartfeatures'] = self.$selectedFeaturesArray.toString();;
          url.query['collections'] = self.$selectedCollectionsArray.toString();;
          url.query['segment'] = self.$selectedSegmentArray.toString();
          url.query['family'] = self.$selectedFamilyArray.toString();
          url.query['category'] = self.$selectedCategoryArray.toString();;
          url.query['attributes'] = self.$selectedAttributesArray.toString();;
          url.query['isHybrid'] = self.$isHybrid;
          jQuery(self.options.contentSelector).attr('data-ajax-url', url.toString());
        },
        onSuccess: function (data) {
          self.bindProductListing(data);

        }
      });
    },

    bindProductListing: function (data) {
      var self = this;
      var $container = jQuery(self.options.templateSelector),
        $theTemplate = Handlebars.compile($container.html());
      jQuery(self.options.contentSelector).html("").append($theTemplate(eval(data)));
      $loading.hide();
      jQuery('.product-browse__results').addClass('product-browse__results--loaded');
    },

    bindFilterListing: function (data) {
      var self = this;
      var $container = jQuery(self.options.templateFilterSelector),
        $theTemplate = Handlebars.compile($container.html());
      jQuery(self.options.contentFilterSelector).append($theTemplate(eval(data)));
      self.bindHandlers();

    }
  };
}(jQuery);
