var Symmons = Symmons || {};

if (!Object.create) {
  Object.create = (function () {
    function F() {}

    return function (o) {
      if (arguments.length != 1) {
        throw new Error('Object.create implementation only accepts one parameter.');
      }
      F.prototype = o;
      return new F()
    }
  })();
}

jQuery(function () {

  jQuery.fn.compareProducts = function (options) {
    return this.each(function () {
      var compareProducts = Object.create(Symmons.compareProducts);
      compareProducts.init(this, options);
    });
  };

  jQuery.fn.compareProducts.options = {
    triggerSelector: '.add-comparison',
    contentSelector: '.compareProducts-content',
    productMaxLength: "3",
    listingSelector: "#compare-product-listing",
    urlSelector: jQuery(".product-comparelist__listing").attr('data-ajax-url'),
    compareList: jQuery(".product-comparelist"),
    isCompareTable: false,
    removeSelector: '.product-comparelist__tableClose',
    tableSelector: "#compare-product-tablelisting",
    btnCompare: ".product-comparelist__cta",
    newcompare: ".modal__newcompare"

  };

  Symmons.compareProducts = {

    init: function (elem, options) {
      var self = this;

      self.$container = jQuery(elem);
      self.options = jQuery.extend({}, jQuery.fn.compareProducts.options, options);
      self.$commonCategory = "";
      self.$selectedCategoryArray = [];
      self.$selectedProductArray = [];

      self.catCounter = 0;

      if (localStorage.getItem('selectedCategory1') !== null && localStorage.getItem('selectedCategory1').length) {
        self.catCounter++;
      }

      if (localStorage.getItem('selectedCategory2') !== null && localStorage.getItem('selectedCategory2').length) {
        self.catCounter++;
      }

      if (localStorage.getItem('selectedCategory3') !== null && localStorage.getItem('selectedCategory3').length) {
        self.catCounter++;
      }

      if (localStorage.getItem("selectedProduct") != null && localStorage.getItem("selectedProduct") != "") {
        self.$selectedTableProductArray = localStorage.getItem("selectedProduct").split(",");
        self.$selectedProductArray = localStorage.getItem("selectedProduct").split(",");
        self.$strProductCount = localStorage.getItem('selectedProduct').split(",");
        self.showCompareBtn(self.$strProductCount.length);

      }

      if (localStorage.getItem("selectedCategory") != null && localStorage.getItem("selectedCategory") != "") {
        self.$selectedCategoryArray = localStorage.getItem("selectedCategory").split(",");
      }



      self.bindElements();
      self.bindHandlers();

      if (jQuery("#compare-product-tablelisting").length > 0) {
        self.bindTableElements();
        self.bindTableHandlers();
      }
    },

    bindElements: function () {
      var self = this;

      self.$trigger = self.$container.find(self.options.triggerSelector);
      self.$content = self.$container.find(self.options.contentSelector);

      if (localStorage.getItem('selectedProduct') == null && localStorage.getItem('selectedProduct') == "") {
        self.bindEmptyProductDetails();
        //self.bindEmptyList(self.options.productMaxLength, "");
      }


    },

    bindHandlers: function () {
      var self = this;


      jQuery(self.options.triggerSelector).bind("click", function (event) {
        event.stopPropagation();
        event.preventDefault();
        self.bindAddToCompare(this);

      });

      jQuery(self.options.newcompare).bind("click", function (event) {
        self.bindNewComparision();
      });


      jQuery(".product-comparelist__title").bind("click", function (event) {
        if (!jQuery(this).hasClass("active")) {
          self.bindAjaxCall();
        }

      });
    },

    bindAddToCompare: function (thisItem) {

      var sc1 = localStorage.getItem('selectedCategory1') || '';
      var sc2 = localStorage.getItem('selectedCategory2') || '';
      var sc3 = localStorage.getItem('selectedCategory3') || '';

      if (!sc1.length && !sc2.length && !sc3.length) {
        this.bindNewComparision();
      }

      var self = this,
        currentElement = jQuery(thisItem);
      var $productID = currentElement.data("product-id"),
        $categoryID = currentElement.data("category"),
        $isProductAdded = false,
        $isCategorySame = false,
        $categoryList = $categoryID.split(","),
        $productList = $productID.split(",");

        // check for errors here.

      jQuery($categoryList).each(function (i, v) {
        if (jQuery.inArray(v, self.$selectedCategoryArray) != -1 || self.$selectedCategoryArray.length == 0) {
          $isCategorySame = true;
        }
      });

      if ($isCategorySame) {
        jQuery($categoryList).each(function (i, value) {
          if (jQuery.inArray(value, self.$selectedCategoryArray) == -1) {
            self.$selectedCategoryArray.push(value);
            localStorage.setItem("selectedCategory", self.$selectedCategoryArray);
          }
        });
        if (self.$selectedProductArray.length < self.options.productMaxLength) {
          if (jQuery.inArray($productID, self.$selectedProductArray) >= 0) {
            self.bindAlertPopup("three");
            return false;
          } else {
            self.$selectedProductArray.push($productID);
            localStorage.setItem("selectedProduct", self.$selectedProductArray);

            for (var i = 1; i <= 3; i++) {
              var ls = localStorage.getItem('selectedCategory' + i) || '';

              if (!ls.length) {
                localStorage.setItem('selectedCategory' + i, $categoryID);
                break;
              }
            }

            self.catCounter = self.catCounter + 1;
          }

        } else {
          self.bindAlertPopup("two");
          return false;
        }

      } else {
        self.bindAlertPopup("one");
        return false;
      }

      self.bindAjaxCall();
      jQuery(".product-comparelist").removeClass("closed").addClass("open");
      jQuery(".product-comparelist__title").addClass("active");

    },

    bindNewComparision: function () {
      var self = this;
      //var hdnURL = jQuery(".no-product__url").val();
      self.$selectedProductArray = [];
      self.$selectedCategoryArray = [];

      localStorage.setItem("selectedProduct", "");
      localStorage.setItem("selectedCategory", "");
      localStorage.setItem("selectedCategory1", "");
      localStorage.setItem("selectedCategory2", "");
      localStorage.setItem("selectedCategory3", "");
      localStorage.setItem("myCommonCategory", "");

      // new item
      localStorage.setItem('selectedProduct', $('.add-comparison').data('productId'));
      localStorage.setItem('selectedCategory', $('.add-comparison').data('category'));
      //localStorage.setItem('selectedCategory1', $('.add-comparison').data('category'));

      $('.product-comparelist').addClass('is-loading');

      this.bindAjaxCall();
    },
    bindAlertPopup: function (alertType) {
      var self = this;
      location.hash = "modal-" + alertType;
      return false;

    },

    bindAjaxCall: function (srcFrom) {
      var self = this;
      var strProductId = "";

      //self.bindCommonCategory();
      if (localStorage.getItem("selectedProduct") != null) {
        strProductId = localStorage.getItem("selectedProduct");
      }

      jQuery.ajax({
        url: self.options.urlSelector + "?product=" + strProductId,
        type: "GET",
        cache: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        error: function (msg) {
          console.log("Error" + msg.responseText);
        },
        success: function (data) {
          self.bindTemplateListing(data);
        },
        complete: function() {
          $('.product-comparelist').removeClass('is-loading');
        }
      });
    },

    bindEmptyProductDetails: function () {
      var self = this;
      var strTemplate = "",
        hdnDefaultTitle = jQuery(".hdnDefaultTitle").val(),
        hdnURL = jQuery(".no-product__url").val(),
        hdnBrowseProductsLabel = jQuery(".hdnBrowseProductsLabel").val();

      for (i = 0; i < self.options.productMaxLength; i++) {
        strTemplate += "<li class='product-comparelist__item'>";
        strTemplate += "<p class='product-comparelist__chooseproduct'>" + hdnDefaultTitle + "</p>";
        strTemplate += "<a href='" + hdnURL + "' class='product-comparelist__chooseproduct--cta'>" + hdnBrowseProductsLabel + "</a></li>";
      }
      jQuery(self.options.listingSelector).append(strTemplate);


    },

    bindEmptyList: function (maxlen, srcCategoryID) {
      var self = this;
      var strTemplate = "",
        hdnTeaser = jQuery(".no-product__title").val(),
        hdnCTA = jQuery(".no-product__cta").val(),
        hdnURL = jQuery(".no-product__url").val();

      for (i = 0; i < maxlen; i++) {
        strTemplate += "<li class='product-comparelist__item'>";
        strTemplate += "<p class='product-comparelist__chooseproduct'>" + hdnTeaser + "</p>";
        if (srcCategoryID != undefined && srcCategoryID != "") {
          strTemplate += "<a href='" + hdnURL + "?category=" + srcCategoryID + "' class='product-comparelist__chooseproduct--cta'>" + hdnCTA + "</a>";
        } else {
          strTemplate += "<a href='" + hdnURL + "' class='product-comparelist__chooseproduct--cta'>" + hdnCTA + "</a>";
        }
        strTemplate += "</li>";
      }

      jQuery(self.options.listingSelector).append(strTemplate);


    },

    removeProductItem: function (strCurrent) {
      var self = this;
      var selectedProductItem = jQuery(strCurrent).parent().data("product-id");
      var selectedCategoryItem = jQuery(strCurrent).parent().data("category-id");


      var strArrayProduct = localStorage.getItem("selectedProduct").split(",");
      strArrayProduct.splice(jQuery.inArray(selectedProductItem, strArrayProduct), 1);
      localStorage.setItem("selectedProduct", strArrayProduct);
      self.$selectedProductArray.splice(jQuery.inArray(selectedProductItem, self.$selectedProductArray), 1);

      var localStorageIndex = $(strCurrent).parent().index() + 1;
      var category = localStorage.getItem('selectedCategory' + localStorageIndex) || ''

      if (category.length) {
        localStorage.setItem('selectedCategory' + localStorageIndex, '');
      }

      self.bindAjaxCall();
    },
    bindCommonCategory: function () {
      var self = this;
      var allArrayContainer = [],
        objects = {},
        counter = {},
        intersection = [];

      if (localStorage.getItem("selectedCategory1") != null && localStorage.getItem("selectedCategory1") != "") {

        var Array1 = localStorage.getItem("selectedCategory1").split(",");
        allArrayContainer.push(Array1);

      }
      if (localStorage.getItem("selectedCategory2") != null && localStorage.getItem("selectedCategory2") != "") {
        var Array2 = localStorage.getItem("selectedCategory2").split(",");
        allArrayContainer.push(Array2);
      }
      if (localStorage.getItem("selectedCategory3") != null && localStorage.getItem("selectedCategory3") != "") {
        var Array3 = localStorage.getItem("selectedCategory3").split(",");
        allArrayContainer.push(Array3);
      }

      allArrayContainer.map(function (ary, n) {
        if (ary.length != 0) {
          ary.map(function (obj) {
            var key = JSON.stringify(obj);
            objects[key] = obj;
            counter[key] = (counter[key] || 0) | (1 << n);
          });
        }
      })


      Object.keys(counter).map(function (key) {
        if (counter[key] == (1 << allArrayContainer.length) - 1) {
          intersection.push(objects[key]);
        }
      });
      if (intersection[0] != undefined) {
        self.$commonCategory = intersection[0];
      }

      if (localStorage.getItem("selectedProduct") == "") {
        self.$commonCategory = "";
      }

    },

    bindTemplateListing: function (data) {
      var self = this;
      var strTemplate = "";
      var itemArrayValue = [];
      if (localStorage.getItem("selectedProduct") != null) {
        itemArrayValue = localStorage.getItem("selectedProduct").split(",");
      }


      var $selectedProduct = JSLINQ(data.CompareProductList.CompareProducts)
        .Where(function (item) {
          return JSLINQ(itemArrayValue)
            .Any(function (sitem) {
              return item.ProductId.toString() == sitem.toString()
            })
        });




      jQuery($selectedProduct.items).each(function (i) {
        strTemplate += "<li data-category-id='" + data.CompareProductList.ProductCategoryId + "' data-product-id='" + $selectedProduct.items[i].ProductId + "' class='product-comparelist__item' data-product-id='" + $selectedProduct.items[i].ProductId + "'><i title='Remove Product' class='product-comparelist__close'> </i> ";
        if ($selectedProduct.items[i].CompareProductAttributes.IsNew != "") {

          strTemplate += "<div class='product-comparelist__newproduct'></div>";

        }
        strTemplate += "<figure class='product-comparelist__image'>";
        strTemplate += "<a href='" + $selectedProduct.items[i].ProductURL + "'><img alt='' src='" + $selectedProduct.items[i].CompareProductAttributes.MainProductImage + "'></a>";
        strTemplate += "</figure>";

        strTemplate += " <div class='product-comparelist__productcontent'>";
        strTemplate += " <a class='product-comparelist__name' href='" + $selectedProduct.items[i].ProductURL + "'>" + $selectedProduct.items[i].CompareProductAttributes.Title + " </a> ";

        strTemplate += "<p class='product-comparelist__model'>" + $selectedProduct.items[i].CompareProductAttributes.ModelNumber + "</p>";

        strTemplate += "<p class='product-comparelist__price'>" + $selectedProduct.items[i].CompareProductAttributes.StartingPriceTitle + "<sup>*</sup>: $" + $selectedProduct.items[i].CompareProductAttributes.StartingPrice + "</p>";
        strTemplate += "</div></li>";
      });


      jQuery(self.options.listingSelector).html("");
      jQuery(self.options.listingSelector).append(strTemplate);

      if (localStorage.getItem("selectedProduct") == null || localStorage.getItem("selectedProduct") == "") {
        self.bindEmptyProductDetails();
      } else {
        self.bindEmptyList(eval(self.options.productMaxLength - $selectedProduct.items.length), data.CompareProductList.ProductCategoryId);
        self.showCompareBtn($selectedProduct.items.length);

      }


      jQuery(".product-comparelist__close").bind("click", function (event) {
        self.removeProductItem(this);
      });


    },
    showCompareBtn: function (strCmpLen) {
      var self = this;

      if (strCmpLen >= 2) {
        jQuery(self.options.btnCompare).show();
      } else {
        jQuery(self.options.btnCompare).hide();
      }
    },

    bindEmptyTableList: function (maxlen, srcCategoryID, isDefault) {
      var self = this;
      var strTemplate = "",
        hdnTeaser = jQuery(".no-product__title").val(),
        hdnCTA = jQuery(".no-product__cta").val(),
        hdnURL = jQuery(".no-product__url").val(),
        hdnDefaultTitle = jQuery(".hdnDefaultTitle").val(),
        hdnBrowseProductsLabel = jQuery(".hdnBrowseProductsLabel").val();

      if (!isDefault) {
        for (i = 0; i < maxlen; i++) {
          strTemplate += "<td>";
          strTemplate += "<p class='product-comparelist__chooseproduct'>" + hdnTeaser + "</p>";
          if (srcCategoryID != undefined && srcCategoryID != "") {
            strTemplate += "<a href='" + hdnURL + "?category=" + srcCategoryID + "' class='product-comparelist__chooseproduct--cta'>" + hdnCTA + "</a>";
          } else {
            strTemplate += "<a href='" + hdnURL + "' class='product-comparelist__chooseproduct--cta'>" + hdnCTA + "</a>";
          }
          strTemplate += "</td>";
        }
        jQuery("#tableHeader").append(strTemplate);
      } else {
        strTemplate += "<tr>";
        for (i = 0; i < 3; i++) {
          strTemplate += "<td>";
          strTemplate += "<p class='product-comparelist__chooseproduct'>" + hdnDefaultTitle + "</p>";
          strTemplate += "<a href='" + hdnURL + "' class='product-comparelist__chooseproduct--cta'>" + hdnBrowseProductsLabel + "</a></td>";

        }
        strTemplate += "</tr>";

        jQuery("#compare-product-tablelisting").html("").append(strTemplate);
        jQuery(".product-title").html("");
      }


    },


    bindProductAjaxCall: function () {
      var self = this;
      var strProductId = "";
      //self.bindCommonCategory();

      if (localStorage.getItem("selectedProduct") != null) {
        strProductId = localStorage.getItem("selectedProduct");
      }
      jQuery.ajax({
        url: self.options.urlSelector + "?product=" + strProductId,
        type: "GET",
        cache: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        error: function (msg) {
          console.log("Error" + msg.responseText);
        },
        success: function (data) {
          self.bindTableListing(data);
          if (data.CompareProductList.ProductCategoryName != "") {
            jQuery(".product-title").html('Compare &quot;' + data.CompareProductList.ProductCategoryName + '&quot;');
          } else {
            jQuery(".product-title").html('');
          }
        }
      });
    },

    bindTableElements: function () {
      var self = this;

      self.$trigger = self.$container.find(self.options.triggerSelector);
      self.$content = self.$container.find(self.options.contentSelector);

      if (self.$selectedTableProductArray == "") {

        self.bindEmptyTableList(self.options.productMaxLength, "", false);
      } else {

        self.bindEmptyTableList(self.options.productMaxLength, "", true);

      }
      /* if (self.$selectedTableProductArray == "") {
         self.bindEmptyTableList(self.options.productMaxLength, "");
       }*/
      self.bindProductAjaxCall();

    },

    bindTableHandlers: function () {
      var self = this;
      jQuery(self.options.removeSelector).bind("click", function (event) {
        self.removeProductItemList(this);
      });
    },


    removeProductItemList: function (strCurrent) {
      var self = this;
      var selectedProductItem = jQuery(strCurrent).data("attr-pid");

      self.$selectedTableProductArray.splice(jQuery.inArray(selectedProductItem, self.$selectedTableProductArray), 1);
      localStorage.setItem("selectedProduct", self.$selectedTableProductArray);

      self.bindProductAjaxCall();
      self.bindAjaxCall();
    },

    bindTableListing: function (data) {
      var self = this;
      var strTemplate = "",
        strTemplate1 = "";
      var $selectedProduct = JSLINQ(data.CompareProductList.CompareProducts)
        .Where(function (item) {
          return JSLINQ(self.$selectedTableProductArray)
            .Any(function (sitem) {
              return item.ProductId.toString() == sitem.toString()
            })
        });

      strTemplate += "<tbody id='masterTBody'><tr id='tableHeader'><th></th>";
      jQuery($selectedProduct.items).each(function (i) {
        strTemplate += "<td><div class='product-comparelist__itemwrap'>";
        strTemplate += "<i class='product-comparelist__tableClose' data-attr-pid='" + $selectedProduct.items[i].ProductId + "' title='Remove Product'></i>";
        if ($selectedProduct.items[i].CompareProductAttributes.IsNew != "") {
          strTemplate += "<div class='product-comparelist__newproduct'></div>";
        }

        strTemplate += "<figure class='product-comparelist__image'>";
        strTemplate += "<a href='" + $selectedProduct.items[i].ProductURL + "'><img alt='' src='" + $selectedProduct.items[i].CompareProductAttributes.MainProductImage + "'></a>";
        strTemplate += "</figure>";

        strTemplate += " <div class='product-comparelist__productcontent'>";
        strTemplate += " <a href='" + $selectedProduct.items[i].ProductURL + "' class='product-comparelist__name'>" + $selectedProduct.items[i].CompareProductAttributes.Title + " </a> ";

        strTemplate += "<p class='product-comparelist__model'>" + $selectedProduct.items[i].CompareProductAttributes.ModelNumber + "</p>";

        strTemplate += "<p class='product-comparelist__price'>" + $selectedProduct.items[i].CompareProductAttributes.StartingPriceTitle + "<sup>*</sup>: $" + $selectedProduct.items[i].CompareProductAttributes.StartingPrice + "</p>";
        strTemplate += "</div></td>";


      });

      strTemplate += " </tr> </tbody>";

      //Collection
      if (data.CompareProductList.CollectionTitle != "") {
        strTemplate1 += "<tr>";
        strTemplate1 += "<th scope='row'>" + data.CompareProductList.CollectionTitle + "</th>";
        jQuery($selectedProduct.items).each(function (i) {
          if ($selectedProduct.items[i].CompareProductAttributes.Collection != "") {
            strTemplate1 += "<td>" + $selectedProduct.items[i].CompareProductAttributes.Collection + "</td>";
          } else {
            strTemplate1 += "<td>&nbsp;</td>";
          }

        });

        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
        }
        strTemplate1 += "</tr>";
      }

      var showDimensions = true;

      //Width
      if (data.CompareProductList.WidthTitle != "") {
        strTemplate1 += "<tr class='product-width'>";
        strTemplate1 += "<th scope='row'>" + data.CompareProductList.WidthTitle + "</th>";
        jQuery($selectedProduct.items).each(function (i) {

          if ($selectedProduct.items[i].CompareProductAttributes.Width != "") {
            strTemplate1 += "<td>" + $selectedProduct.items[i].CompareProductAttributes.Width + "</td>";
          } else {
            strTemplate1 += "<td>&nbsp;</td>";
            showDimensions = false;
          }

        });
        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
        }
        strTemplate1 += "</tr>";
      }

      //Length
      if (data.CompareProductList.LengthTitle != "") {
        strTemplate1 += "<tr class='product-length'>";
        strTemplate1 += "<th scope='row'>" + data.CompareProductList.LengthTitle + "</th>";
        jQuery($selectedProduct.items).each(function (i) {
          if ($selectedProduct.items[i].CompareProductAttributes.Length != "") {
            strTemplate1 += "<td>" + $selectedProduct.items[i].CompareProductAttributes.Length + "</td>";
          } else {
            strTemplate1 += "<td>&nbsp;</td>";
            showDimensions = false;
          }

        });
        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
        }
        strTemplate1 += "</tr>";
      }

      //Height
      if (data.CompareProductList.HeightTitle != "") {
        strTemplate1 += "<tr class='product-height'>";
        strTemplate1 += "<th scope='row'>" + data.CompareProductList.HeightTitle + "</th>";
        jQuery($selectedProduct.items).each(function (i) {
          if ($selectedProduct.items[i].CompareProductAttributes.Height != "") {
            strTemplate1 += "<td>" + $selectedProduct.items[i].CompareProductAttributes.Height + "</td>";
          } else {
            strTemplate1 += "<td>&nbsp;</td>";
            showDimensions = false;
          }
        });
        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
        }
        strTemplate1 += "</tr>";
      }


      //Finishes
      if (data.CompareProductList.FinishesTitle != "") {
        strTemplate1 += "<tr>";
        strTemplate1 += "<th scope='row'>" + data.CompareProductList.FinishesTitle + "</th>";
        jQuery($selectedProduct.items).each(function (i) {

          if ($selectedProduct.items[i].CompareProductAttributes.Finishes != "") {
            strTemplate1 += "<td>";
            jQuery($selectedProduct.items[i].CompareProductAttributes.Finishes).each(function (j) {
              strTemplate1 += "<p>";
              if ($selectedProduct.items[i].CompareProductAttributes.Finishes[j].FinishIconUrl != "") {
                strTemplate1 += "<img class='icon-design' src='" + $selectedProduct.items[i].CompareProductAttributes.Finishes[j].FinishIconUrl + "' alt='' />";

              }
              strTemplate1 += $selectedProduct.items[i].CompareProductAttributes.Finishes[j].FinishName + "</p>";
            });
            strTemplate1 += "</td>";
          } else {
            strTemplate1 += "<td>&nbsp;</td>";
          }
        });
        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
        }
        strTemplate1 += "</tr>";
      }


      //SmartFeatures
      if (data.CompareProductList.SmartFeaturesTitle != "") {
        strTemplate1 += "<tr>";
        strTemplate1 += "<th scope='row'>" + data.CompareProductList.SmartFeaturesTitle + "</th>";
        jQuery($selectedProduct.items).each(function (i) {

          if ($selectedProduct.items[i].CompareProductAttributes.SmartFeatures != "") {
            strTemplate1 += "<td>";
            jQuery($selectedProduct.items[i].CompareProductAttributes.SmartFeatures).each(function (j) {
              strTemplate1 += "<p>";
              if ($selectedProduct.items[i].CompareProductAttributes.SmartFeatures[j].FeatureIconUrl != "") {

                strTemplate1 += "<img class='icon-design' src='" + $selectedProduct.items[i].CompareProductAttributes.SmartFeatures[j].FeatureIconUrl + "' alt='' />";
              }
              strTemplate1 += $selectedProduct.items[i].CompareProductAttributes.SmartFeatures[j].FeatureName + "</p>";
            });
            strTemplate1 += "</td>";
          } else {
            strTemplate1 += "<td>&nbsp;</td>";
          }
        });
        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
        }
        strTemplate1 += "</tr>";
      }

      //SpecialFeatures
      if (data.CompareProductList.SpecialFeaturesTitle != "") {
        strTemplate1 += "<tr>";
        strTemplate1 += "<th scope='row'>" + data.CompareProductList.SpecialFeaturesTitle + "</th>";
        jQuery($selectedProduct.items).each(function (i) {
          if ($selectedProduct.items[i].CompareProductAttributes.SpecialFeatures != "") {
            strTemplate1 += "<td>";
            jQuery($selectedProduct.items[i].CompareProductAttributes.SpecialFeatures).each(function (j) {
              strTemplate1 += "<p>";
              if ($selectedProduct.items[i].CompareProductAttributes.SpecialFeatures[j].AttributeIconUrl != "") {
                strTemplate1 += "<img class='icon-design' src='" + $selectedProduct.items[i].CompareProductAttributes.SpecialFeatures[j].AttributeIconUrl + "' alt='' />";
              }
              strTemplate1 += "<span>" + $selectedProduct.items[i].CompareProductAttributes.SpecialFeatures[j].AttributeName + "</span></p>";
            });

            strTemplate1 += "</td>";
          } else {
            strTemplate1 += "<td>&nbsp;</td>";
          }
        });

        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
        }
        strTemplate1 += "</tr>";
      }

      jQuery(self.options.tableSelector).html("");
      jQuery(self.options.tableSelector).append(strTemplate);
      jQuery("#masterTBody").append(strTemplate1);

      if (!showDimensions) {
        jQuery('#masterTBody').find('.product-width, .product-height, .product-length').hide();
      }

      // self.bindCommonCategory();

      if (localStorage.getItem("selectedProduct") == null || localStorage.getItem("selectedProduct") == "") {

        self.bindEmptyTableList(eval(self.options.productMaxLength - $selectedProduct.items.length), "", true);
      } else {
        self.bindEmptyTableList(eval(self.options.productMaxLength - $selectedProduct.items.length), data.CompareProductList.ProductCategoryId, false);
      }



      self.bindTableHandlers();
      $loading.hide();
    }
  }
});
