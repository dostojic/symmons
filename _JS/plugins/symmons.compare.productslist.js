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
  })()
}

jQuery(function () {

  jQuery.fn.compareProductsList = function (options) {
    return this.each(function () {
      var compareProductsList = Object.create(Symmons.compareProductsList);
      compareProductsList.init(this, options);
    });
  };

  jQuery.fn.compareProductsList.options = {
    triggerSelector: '.add-comparison',
    contentSelector: '.compareProducts-content',
    removeSelector: '.product-comparelist__tableClose',
    productMaxLength: "3",
    listingSelector: "#compare-product-tablelisting",
    urlSelector: jQuery("#compare-product-tablelisting").attr('data-ajax-url')
  };

  Symmons.compareProductsList = {

    init: function (elem, options) {
      var self = this;

      self.$container = jQuery(elem);
      self.options = jQuery.extend({}, jQuery.fn.compareProductsList.options, options);
      self.$commonCategory = "";
      self.$selectedProductArray = [];
      self.$selectedCategoryArray = [];
      self.catCounter = 0;

      if (localStorage.getItem("selectedProduct") != null && localStorage.getItem("selectedProduct") != "") {
        self.$selectedProductArray = localStorage.getItem("selectedProduct").split(",");
      }

      self.bindTableElements();
      self.bindTableHandlers();
    },

    bindTableElements: function () {
      var self = this;

      self.$trigger = self.$container.find(self.options.triggerSelector);
      self.$content = self.$container.find(self.options.contentSelector);

      if (self.$selectedProductArray == "") {
        self.bindEmptyTableList(self.options.productMaxLength, "");
      }
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

      self.$selectedProductArray.splice(jQuery.inArray(selectedProductItem, self.$selectedProductArray), 1);
      localStorage.setItem("selectedProduct", self.$selectedProductArray);

      //console.log(localStorage.getItem("selectedProduct"));
      self.bindProductAjaxCall();
    },

    bindCommonCategoryList: function () {
      var self = this;
      var self = this;
      var allArrayContainer = [],
        objects = {},
        counter = {},
        intersection = [];


      if (localStorage.getItem("selectedCategory1") != "") {
        var Array1 = localStorage.getItem("selectedCategory1").split(",");
        allArrayContainer.push(Array1);

      }
      if (localStorage.getItem("selectedCategory2") != "") {
        var Array2 = localStorage.getItem("selectedCategory2").split(",");
        allArrayContainer.push(Array2);
      }
      if (localStorage.getItem("selectedCategory3") != "") {
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

      self.$commonCategory = intersection[0];
      if (localStorage.getItem("selectedProduct") == "") {
        self.$commonCategory = "";
      }

    },
    bindProductAjaxCall: function () {
      var self = this;
      self.bindCommonCategoryList();
      jQuery.ajax({
        url: self.options.urlSelector + "?product=" + localStorage.getItem("selectedProduct") + "&category=" + self.$commonCategory,
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

    bindEmptyTableList: function (maxlen, srcCategoryID) {
      var self = this;
      var strTemplate = "",
        hdnTeaser = jQuery(".no-product__title").val(),
        hdnCTA = jQuery(".no-product__cta").val(),
        hdnURL = jQuery(".no-product__url").val();


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

    },




    bindTableListing: function (data) {
      var self = this;
      var strTemplate = "",
        strTemplate1 = "";
      var $selectedProduct = JSLINQ(data.CompareProductList.CompareProducts)
        .Where(function (item) {
          return JSLINQ(self.$selectedProductArray)
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
        strTemplate += "<img alt='' src='" + $selectedProduct.items[i].CompareProductAttributes.MainProductImage + "'>";
        strTemplate += "</figure>";

        strTemplate += " <div class='product-comparelist__productcontent'>";
        strTemplate += " <a class='product-comparelist__name'>" + $selectedProduct.items[i].CompareProductAttributes.Title + " </a> ";

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
          strTemplate1 += "<td>" + $selectedProduct.items[i].CompareProductAttributes.Collection + "</td>";
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
          strTemplate1 += "<td>" + $selectedProduct.items[i].CompareProductAttributes.Width + "</td>";
        });
        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
          //showDimensions = false;
        }
        strTemplate1 += "</tr>";
      }

      //Length
      if (data.CompareProductList.LengthTitle != "") {
        strTemplate1 += "<tr class='product-length'>";
        strTemplate1 += "<th scope='row'>" + data.CompareProductList.LengthTitle + "</th>";
        jQuery($selectedProduct.items).each(function (i) {
          strTemplate1 += "<td>" + $selectedProduct.items[i].CompareProductAttributes.Length + "</td>";
        });
        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
          //showDimensions = false;
        }
        strTemplate1 += "</tr>";
      }

      //Height
      if (data.CompareProductList.HeightTitle != "") {
        strTemplate1 += "<tr class='product-height'>";
        strTemplate1 += "<th scope='row'>" + data.CompareProductList.HeightTitle + "</th>";
        jQuery($selectedProduct.items).each(function (i) {
          strTemplate1 += "<td>" + $selectedProduct.items[i].CompareProductAttributes.Height + "</td>";
        });
        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
          //showDimensions = false;
        }
        strTemplate1 += "</tr>";
      }


      //Finishes
      if (data.CompareProductList.FinishesTitle != "") {
        strTemplate1 += "<tr>";
        strTemplate1 += "<th scope='row'>" + data.CompareProductList.FinishesTitle + "</th>";
        jQuery($selectedProduct.items).each(function (i) {
          strTemplate1 += "<td>";
          jQuery($selectedProduct.items[i].CompareProductAttributes.Finishes).each(function (j) {
            strTemplate1 += "<p>";
            if ($selectedProduct.items[i].CompareProductAttributes.Finishes[j].FinishIconUrl != "") {
              strTemplate1 += "<p><img class='icon-design' src='" + $selectedProduct.items[i].CompareProductAttributes.Finishes[j].FinishIconUrl + "' alt='' />";

            }
            strTemplate1 += "<span>" + $selectedProduct.items[i].CompareProductAttributes.Finishes[j].FinishName + "</span></p>"
          });
          strTemplate1 += "</td>";
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
          strTemplate1 += "<td>";
          jQuery($selectedProduct.items[i].CompareProductAttributes.SmartFeatures).each(function (j) {
            if ($selectedProduct.items[i].CompareProductAttributes.SmartFeatures[j].FeatureIconUrl != "") {
              strTemplate1 += "<p>";
              strTemplate1 += "<img class='icon-design' src='" + $selectedProduct.items[i].CompareProductAttributes.SmartFeatures[j].FeatureIconUrl + "' alt='' />";
            }
            strTemplate1 += "<span>" + $selectedProduct.items[i].CompareProductAttributes.SmartFeatures[j].FeatureName + "</span></p>";
          });
          strTemplate1 += "</td>";
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
          strTemplate1 += "<td>";
          jQuery($selectedProduct.items[i].CompareProductAttributes.SpecialFeatures).each(function (j) {
            strTemplate1 += "<p>";
            if ($selectedProduct.items[i].CompareProductAttributes.SpecialFeatures[j].AttributeIconUrl != "") {
              strTemplate1 += "<img class='icon-design' src='" + $selectedProduct.items[i].CompareProductAttributes.SpecialFeatures[j].AttributeIconUrl + "' alt='' />";
            }
            strTemplate1 += "<span>" + $selectedProduct.items[i].CompareProductAttributes.SpecialFeatures[j].AttributeName + "</span></p>";
          });

          strTemplate1 += "</td>";
        });

        for (i = $selectedProduct.items.length; i < 3; i++) {
          strTemplate1 += "<td class='empty'></td>";
        }
        strTemplate1 += "</tr>";
      }



      jQuery(self.options.listingSelector).html("");
      jQuery(self.options.listingSelector).append(strTemplate);
      jQuery("#masterTBody").append(strTemplate1);
      if (!showDimensions) {
        jQuery('#masterTBody').find('.product-width, .product-height, .product-length').hide();
      }

      self.bindCommonCategoryList();
      self.bindEmptyTableList(eval(self.options.productMaxLength - $selectedProduct.items.length), self.$commonCategory);
      self.bindTableHandlers();
      $loading.hide();
    }
  }
});
