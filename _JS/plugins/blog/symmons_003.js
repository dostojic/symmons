var Symmons=Symmons||{};Object.create||(Object.create=function(){function a(){}return function(b){if(1!=arguments.length)throw new Error("Object.create implementation only accepts one parameter.");return a.prototype=b,new a}}()),jQuery(function(){jQuery.fn.compareProducts=function(a){return this.each(function(){var b=Object.create(Symmons.compareProducts);b.init(this,a)})},jQuery.fn.compareProducts.options={triggerSelector:".add-comparison",contentSelector:".compareProducts-content",productMaxLength:"3",listingSelector:"#compare-product-listing",urlSelector:jQuery(".product-comparelist__listing").attr("data-ajax-url"),compareList:jQuery(".product-comparelist"),isCompareTable:!1,removeSelector:".product-comparelist__tableClose",tableSelector:"#compare-product-tablelisting",btnCompare:".product-comparelist__cta",newcompare:".modal__newcompare"},Symmons.compareProducts={init:function(a,b){var c=this;c.$container=jQuery(a),c.options=jQuery.extend({},jQuery.fn.compareProducts.options,b),c.$commonCategory="",c.$selectedCategoryArray=[],c.$selectedProductArray=[],c.catCounter=0,null!==localStorage.getItem("selectedCategory1")&&localStorage.getItem("selectedCategory1").length&&c.catCounter++,null!==localStorage.getItem("selectedCategory2")&&localStorage.getItem("selectedCategory2").length&&c.catCounter++,null!==localStorage.getItem("selectedCategory3")&&localStorage.getItem("selectedCategory3").length&&c.catCounter++,null!=localStorage.getItem("selectedProduct")&&""!=localStorage.getItem("selectedProduct")&&(c.$selectedTableProductArray=localStorage.getItem("selectedProduct").split(","),c.$selectedProductArray=localStorage.getItem("selectedProduct").split(","),c.$strProductCount=localStorage.getItem("selectedProduct").split(","),c.showCompareBtn(c.$strProductCount.length)),null!=localStorage.getItem("selectedCategory")&&""!=localStorage.getItem("selectedCategory")&&(c.$selectedCategoryArray=localStorage.getItem("selectedCategory").split(",")),c.bindElements(),c.bindHandlers(),jQuery("#compare-product-tablelisting").length>0&&(c.bindTableElements(),c.bindTableHandlers())},bindElements:function(){var a=this;a.$trigger=a.$container.find(a.options.triggerSelector),a.$content=a.$container.find(a.options.contentSelector),null==localStorage.getItem("selectedProduct")&&""==localStorage.getItem("selectedProduct")&&a.bindEmptyProductDetails()},bindHandlers:function(){var a=this;jQuery(a.options.triggerSelector).bind("click",function(b){b.stopPropagation(),b.preventDefault(),a.bindAddToCompare(this)}),jQuery(a.options.newcompare).bind("click",function(b){a.bindNewComparision()}),jQuery(".product-comparelist__title").bind("click",function(b){jQuery(this).hasClass("active")||a.bindAjaxCall()})},bindAddToCompare:function(a){var b=localStorage.getItem("selectedCategory1")||"",c=localStorage.getItem("selectedCategory2")||"",d=localStorage.getItem("selectedCategory3")||"";b.length||c.length||d.length||this.bindNewComparision();var e=this,f=jQuery(a),g=f.data("product-id"),h=f.data("category"),i=!1,j=h.split(",");g.split(",");if(
// check for errors here.
jQuery(j).each(function(a,b){jQuery.inArray(b,e.$selectedCategoryArray)==-1&&0!=e.$selectedCategoryArray.length||(i=!0)}),!i)return e.bindAlertPopup("one"),!1;if(jQuery(j).each(function(a,b){jQuery.inArray(b,e.$selectedCategoryArray)==-1&&(e.$selectedCategoryArray.push(b),localStorage.setItem("selectedCategory",e.$selectedCategoryArray))}),!(e.$selectedProductArray.length<e.options.productMaxLength))return e.bindAlertPopup("two"),!1;if(jQuery.inArray(g,e.$selectedProductArray)>=0)return e.bindAlertPopup("three"),!1;e.$selectedProductArray.push(g),localStorage.setItem("selectedProduct",e.$selectedProductArray);for(var k=1;k<=3;k++){var l=localStorage.getItem("selectedCategory"+k)||"";if(!l.length){localStorage.setItem("selectedCategory"+k,h);break}}e.catCounter=e.catCounter+1,e.bindAjaxCall(),jQuery(".product-comparelist").removeClass("closed").addClass("open"),jQuery(".product-comparelist__title").addClass("active")},bindNewComparision:function(){var a=this;
//var hdnURL = jQuery(".no-product__url").val();
a.$selectedProductArray=[],a.$selectedCategoryArray=[],localStorage.setItem("selectedProduct",""),localStorage.setItem("selectedCategory",""),localStorage.setItem("selectedCategory1",""),localStorage.setItem("selectedCategory2",""),localStorage.setItem("selectedCategory3",""),localStorage.setItem("myCommonCategory",""),
// new item
localStorage.setItem("selectedProduct",$(".add-comparison").data("productId")),localStorage.setItem("selectedCategory",$(".add-comparison").data("category")),
//localStorage.setItem('selectedCategory1', $('.add-comparison').data('category'));
$(".product-comparelist").addClass("is-loading"),this.bindAjaxCall()},bindAlertPopup:function(a){return location.hash="modal-"+a,!1},bindAjaxCall:function(a){var b=this,c="";
//self.bindCommonCategory();
null!=localStorage.getItem("selectedProduct")&&(c=localStorage.getItem("selectedProduct")),jQuery.ajax({url:b.options.urlSelector+"?product="+c,type:"GET",cache:!0,contentType:"application/json; charset=utf-8",dataType:"json",async:!0,error:function(a){console.log("Error"+a.responseText)},success:function(a){b.bindTemplateListing(a)},complete:function(){$(".product-comparelist").removeClass("is-loading")}})},bindEmptyProductDetails:function(){var a=this,b="",c=jQuery(".hdnDefaultTitle").val(),d=jQuery(".no-product__url").val(),e=jQuery(".hdnBrowseProductsLabel").val();for(i=0;i<a.options.productMaxLength;i++)b+="<li class='product-comparelist__item'>",b+="<p class='product-comparelist__chooseproduct'>"+c+"</p>",b+="<a href='"+d+"' class='product-comparelist__chooseproduct--cta'>"+e+"</a></li>";jQuery(a.options.listingSelector).append(b)},bindEmptyList:function(a,b){var c=this,d="",e=jQuery(".no-product__title").val(),f=jQuery(".no-product__cta").val(),g=jQuery(".no-product__url").val();for(i=0;i<a;i++)d+="<li class='product-comparelist__item'>",d+="<p class='product-comparelist__chooseproduct'>"+e+"</p>",d+=void 0!=b&&""!=b?"<a href='"+g+"?category="+b+"' class='product-comparelist__chooseproduct--cta'>"+f+"</a>":"<a href='"+g+"' class='product-comparelist__chooseproduct--cta'>"+f+"</a>",d+="</li>";jQuery(c.options.listingSelector).append(d)},removeProductItem:function(a){var b=this,c=jQuery(a).parent().data("product-id"),d=(jQuery(a).parent().data("category-id"),localStorage.getItem("selectedProduct").split(","));d.splice(jQuery.inArray(c,d),1),localStorage.setItem("selectedProduct",d),b.$selectedProductArray.splice(jQuery.inArray(c,b.$selectedProductArray),1);var e=$(a).parent().index()+1,f=localStorage.getItem("selectedCategory"+e)||"";f.length&&localStorage.setItem("selectedCategory"+e,""),b.bindAjaxCall()},bindCommonCategory:function(){var a=this,b=[],c={},d={},e=[];if(null!=localStorage.getItem("selectedCategory1")&&""!=localStorage.getItem("selectedCategory1")){var f=localStorage.getItem("selectedCategory1").split(",");b.push(f)}if(null!=localStorage.getItem("selectedCategory2")&&""!=localStorage.getItem("selectedCategory2")){var g=localStorage.getItem("selectedCategory2").split(",");b.push(g)}if(null!=localStorage.getItem("selectedCategory3")&&""!=localStorage.getItem("selectedCategory3")){var h=localStorage.getItem("selectedCategory3").split(",");b.push(h)}b.map(function(a,b){0!=a.length&&a.map(function(a){var e=JSON.stringify(a);c[e]=a,d[e]=(d[e]||0)|1<<b})}),Object.keys(d).map(function(a){d[a]==(1<<b.length)-1&&e.push(c[a])}),void 0!=e[0]&&(a.$commonCategory=e[0]),""==localStorage.getItem("selectedProduct")&&(a.$commonCategory="")},bindTemplateListing:function(data){var self=this,strTemplate="",itemArrayValue=[];null!=localStorage.getItem("selectedProduct")&&(itemArrayValue=localStorage.getItem("selectedProduct").split(","));var $selectedProduct=JSLINQ(data.CompareProductList.CompareProducts).Where(function(a){return JSLINQ(itemArrayValue).Any(function(b){return a.ProductId.toString()==b.toString()})});jQuery($selectedProduct.items).each(function(a){strTemplate+="<li data-category-id='"+data.CompareProductList.ProductCategoryId+"' data-product-id='"+$selectedProduct.items[a].ProductId+"' class='product-comparelist__item' data-product-id='"+$selectedProduct.items[a].ProductId+"'><i title='Remove Product' class='product-comparelist__close'> </i> ",""!=$selectedProduct.items[a].CompareProductAttributes.IsNew&&(strTemplate+="<div class='product-comparelist__newproduct'></div>"),strTemplate+="<figure class='product-comparelist__image'>",strTemplate+="<a href='"+$selectedProduct.items[a].ProductURL+"'><img alt='' src='"+$selectedProduct.items[a].CompareProductAttributes.MainProductImage+"'></a>",strTemplate+="</figure>",strTemplate+=" <div class='product-comparelist__productcontent'>",strTemplate+=" <a class='product-comparelist__name' href='"+$selectedProduct.items[a].ProductURL+"'>"+$selectedProduct.items[a].CompareProductAttributes.Title+" </a> ",strTemplate+="<p class='product-comparelist__model'>"+$selectedProduct.items[a].CompareProductAttributes.ModelNumber+"</p>",strTemplate+="<p class='product-comparelist__price'>"+$selectedProduct.items[a].CompareProductAttributes.StartingPriceTitle+"<sup>*</sup>: $"+$selectedProduct.items[a].CompareProductAttributes.StartingPrice+"</p>",strTemplate+="</div></li>"}),jQuery(self.options.listingSelector).html(""),jQuery(self.options.listingSelector).append(strTemplate),null==localStorage.getItem("selectedProduct")||""==localStorage.getItem("selectedProduct")?self.bindEmptyProductDetails():(self.bindEmptyList(eval(self.options.productMaxLength-$selectedProduct.items.length),data.CompareProductList.ProductCategoryId),self.showCompareBtn($selectedProduct.items.length)),jQuery(".product-comparelist__close").bind("click",function(a){self.removeProductItem(this)})},showCompareBtn:function(a){var b=this;a>=2?jQuery(b.options.btnCompare).show():jQuery(b.options.btnCompare).hide()},bindEmptyTableList:function(a,b,c){var d="",e=jQuery(".no-product__title").val(),f=jQuery(".no-product__cta").val(),g=jQuery(".no-product__url").val(),h=jQuery(".hdnDefaultTitle").val(),j=jQuery(".hdnBrowseProductsLabel").val();if(c){for(d+="<tr>",i=0;i<3;i++)d+="<td>",d+="<p class='product-comparelist__chooseproduct'>"+h+"</p>",d+="<a href='"+g+"' class='product-comparelist__chooseproduct--cta'>"+j+"</a></td>";d+="</tr>",jQuery("#compare-product-tablelisting").html("").append(d),jQuery(".product-title").html("")}else{for(i=0;i<a;i++)d+="<td>",d+="<p class='product-comparelist__chooseproduct'>"+e+"</p>",d+=void 0!=b&&""!=b?"<a href='"+g+"?category="+b+"' class='product-comparelist__chooseproduct--cta'>"+f+"</a>":"<a href='"+g+"' class='product-comparelist__chooseproduct--cta'>"+f+"</a>",d+="</td>";jQuery("#tableHeader").append(d)}},bindProductAjaxCall:function(){var a=this,b="";
//self.bindCommonCategory();
null!=localStorage.getItem("selectedProduct")&&(b=localStorage.getItem("selectedProduct")),jQuery.ajax({url:a.options.urlSelector+"?product="+b,type:"GET",cache:!0,contentType:"application/json; charset=utf-8",dataType:"json",async:!0,error:function(a){console.log("Error"+a.responseText)},success:function(b){a.bindTableListing(b),""!=b.CompareProductList.ProductCategoryName?jQuery(".product-title").html("Compare &quot;"+b.CompareProductList.ProductCategoryName+"&quot;"):jQuery(".product-title").html("")}})},bindTableElements:function(){var a=this;a.$trigger=a.$container.find(a.options.triggerSelector),a.$content=a.$container.find(a.options.contentSelector),""==a.$selectedTableProductArray?a.bindEmptyTableList(a.options.productMaxLength,"",!1):a.bindEmptyTableList(a.options.productMaxLength,"",!0),/* if (self.$selectedTableProductArray == "") {
         self.bindEmptyTableList(self.options.productMaxLength, "");
       }*/
a.bindProductAjaxCall()},bindTableHandlers:function(){var a=this;jQuery(a.options.removeSelector).bind("click",function(b){a.removeProductItemList(this)})},removeProductItemList:function(a){var b=this,c=jQuery(a).data("attr-pid");b.$selectedTableProductArray.splice(jQuery.inArray(c,b.$selectedTableProductArray),1),localStorage.setItem("selectedProduct",b.$selectedTableProductArray),b.bindProductAjaxCall(),b.bindAjaxCall()},bindTableListing:function(data){var self=this,strTemplate="",strTemplate1="",$selectedProduct=JSLINQ(data.CompareProductList.CompareProducts).Where(function(a){return JSLINQ(self.$selectedTableProductArray).Any(function(b){return a.ProductId.toString()==b.toString()})});
//Collection
if(strTemplate+="<tbody id='masterTBody'><tr id='tableHeader'><th></th>",jQuery($selectedProduct.items).each(function(a){strTemplate+="<td><div class='product-comparelist__itemwrap'>",strTemplate+="<i class='product-comparelist__tableClose' data-attr-pid='"+$selectedProduct.items[a].ProductId+"' title='Remove Product'></i>",""!=$selectedProduct.items[a].CompareProductAttributes.IsNew&&(strTemplate+="<div class='product-comparelist__newproduct'></div>"),strTemplate+="<figure class='product-comparelist__image'>",strTemplate+="<a href='"+$selectedProduct.items[a].ProductURL+"'><img alt='' src='"+$selectedProduct.items[a].CompareProductAttributes.MainProductImage+"'></a>",strTemplate+="</figure>",strTemplate+=" <div class='product-comparelist__productcontent'>",strTemplate+=" <a href='"+$selectedProduct.items[a].ProductURL+"' class='product-comparelist__name'>"+$selectedProduct.items[a].CompareProductAttributes.Title+" </a> ",strTemplate+="<p class='product-comparelist__model'>"+$selectedProduct.items[a].CompareProductAttributes.ModelNumber+"</p>",strTemplate+="<p class='product-comparelist__price'>"+$selectedProduct.items[a].CompareProductAttributes.StartingPriceTitle+"<sup>*</sup>: $"+$selectedProduct.items[a].CompareProductAttributes.StartingPrice+"</p>",strTemplate+="</div></td>"}),strTemplate+=" </tr> </tbody>",""!=data.CompareProductList.CollectionTitle){for(strTemplate1+="<tr>",strTemplate1+="<th scope='row'>"+data.CompareProductList.CollectionTitle+"</th>",jQuery($selectedProduct.items).each(function(a){strTemplate1+=""!=$selectedProduct.items[a].CompareProductAttributes.Collection?"<td>"+$selectedProduct.items[a].CompareProductAttributes.Collection+"</td>":"<td>&nbsp;</td>"}),i=$selectedProduct.items.length;i<3;i++)strTemplate1+="<td class='empty'></td>";strTemplate1+="</tr>"}var showDimensions=!0;
//Width
if(""!=data.CompareProductList.WidthTitle){for(strTemplate1+="<tr class='product-width'>",strTemplate1+="<th scope='row'>"+data.CompareProductList.WidthTitle+"</th>",jQuery($selectedProduct.items).each(function(a){""!=$selectedProduct.items[a].CompareProductAttributes.Width?strTemplate1+="<td>"+$selectedProduct.items[a].CompareProductAttributes.Width+"</td>":(strTemplate1+="<td>&nbsp;</td>",showDimensions=!1)}),i=$selectedProduct.items.length;i<3;i++)strTemplate1+="<td class='empty'></td>";strTemplate1+="</tr>"}
//Length
if(""!=data.CompareProductList.LengthTitle){for(strTemplate1+="<tr class='product-length'>",strTemplate1+="<th scope='row'>"+data.CompareProductList.LengthTitle+"</th>",jQuery($selectedProduct.items).each(function(a){""!=$selectedProduct.items[a].CompareProductAttributes.Length?strTemplate1+="<td>"+$selectedProduct.items[a].CompareProductAttributes.Length+"</td>":(strTemplate1+="<td>&nbsp;</td>",showDimensions=!1)}),i=$selectedProduct.items.length;i<3;i++)strTemplate1+="<td class='empty'></td>";strTemplate1+="</tr>"}
//Height
if(""!=data.CompareProductList.HeightTitle){for(strTemplate1+="<tr class='product-height'>",strTemplate1+="<th scope='row'>"+data.CompareProductList.HeightTitle+"</th>",jQuery($selectedProduct.items).each(function(a){""!=$selectedProduct.items[a].CompareProductAttributes.Height?strTemplate1+="<td>"+$selectedProduct.items[a].CompareProductAttributes.Height+"</td>":(strTemplate1+="<td>&nbsp;</td>",showDimensions=!1)}),i=$selectedProduct.items.length;i<3;i++)strTemplate1+="<td class='empty'></td>";strTemplate1+="</tr>"}
//Finishes
if(""!=data.CompareProductList.FinishesTitle){for(strTemplate1+="<tr>",strTemplate1+="<th scope='row'>"+data.CompareProductList.FinishesTitle+"</th>",jQuery($selectedProduct.items).each(function(a){""!=$selectedProduct.items[a].CompareProductAttributes.Finishes?(strTemplate1+="<td>",jQuery($selectedProduct.items[a].CompareProductAttributes.Finishes).each(function(b){strTemplate1+="<p>",""!=$selectedProduct.items[a].CompareProductAttributes.Finishes[b].FinishIconUrl&&(strTemplate1+="<img class='icon-design' src='"+$selectedProduct.items[a].CompareProductAttributes.Finishes[b].FinishIconUrl+"' alt='' />"),strTemplate1+=$selectedProduct.items[a].CompareProductAttributes.Finishes[b].FinishName+"</p>"}),strTemplate1+="</td>"):strTemplate1+="<td>&nbsp;</td>"}),i=$selectedProduct.items.length;i<3;i++)strTemplate1+="<td class='empty'></td>";strTemplate1+="</tr>"}
//SmartFeatures
if(""!=data.CompareProductList.SmartFeaturesTitle){for(strTemplate1+="<tr>",strTemplate1+="<th scope='row'>"+data.CompareProductList.SmartFeaturesTitle+"</th>",jQuery($selectedProduct.items).each(function(a){""!=$selectedProduct.items[a].CompareProductAttributes.SmartFeatures?(strTemplate1+="<td>",jQuery($selectedProduct.items[a].CompareProductAttributes.SmartFeatures).each(function(b){strTemplate1+="<p>",""!=$selectedProduct.items[a].CompareProductAttributes.SmartFeatures[b].FeatureIconUrl&&(strTemplate1+="<img class='icon-design' src='"+$selectedProduct.items[a].CompareProductAttributes.SmartFeatures[b].FeatureIconUrl+"' alt='' />"),strTemplate1+=$selectedProduct.items[a].CompareProductAttributes.SmartFeatures[b].FeatureName+"</p>"}),strTemplate1+="</td>"):strTemplate1+="<td>&nbsp;</td>"}),i=$selectedProduct.items.length;i<3;i++)strTemplate1+="<td class='empty'></td>";strTemplate1+="</tr>"}
//SpecialFeatures
if(""!=data.CompareProductList.SpecialFeaturesTitle){for(strTemplate1+="<tr>",strTemplate1+="<th scope='row'>"+data.CompareProductList.SpecialFeaturesTitle+"</th>",jQuery($selectedProduct.items).each(function(a){""!=$selectedProduct.items[a].CompareProductAttributes.SpecialFeatures?(strTemplate1+="<td>",jQuery($selectedProduct.items[a].CompareProductAttributes.SpecialFeatures).each(function(b){strTemplate1+="<p>",""!=$selectedProduct.items[a].CompareProductAttributes.SpecialFeatures[b].AttributeIconUrl&&(strTemplate1+="<img class='icon-design' src='"+$selectedProduct.items[a].CompareProductAttributes.SpecialFeatures[b].AttributeIconUrl+"' alt='' />"),strTemplate1+="<span>"+$selectedProduct.items[a].CompareProductAttributes.SpecialFeatures[b].AttributeName+"</span></p>"}),strTemplate1+="</td>"):strTemplate1+="<td>&nbsp;</td>"}),i=$selectedProduct.items.length;i<3;i++)strTemplate1+="<td class='empty'></td>";strTemplate1+="</tr>"}jQuery(self.options.tableSelector).html(""),jQuery(self.options.tableSelector).append(strTemplate),jQuery("#masterTBody").append(strTemplate1),showDimensions||jQuery("#masterTBody").find(".product-width, .product-height, .product-length").hide(),
// self.bindCommonCategory();
null==localStorage.getItem("selectedProduct")||""==localStorage.getItem("selectedProduct")?self.bindEmptyTableList(eval(self.options.productMaxLength-$selectedProduct.items.length),"",!0):self.bindEmptyTableList(eval(self.options.productMaxLength-$selectedProduct.items.length),data.CompareProductList.ProductCategoryId,!1),self.bindTableHandlers(),$loading.hide()}}});
//# sourceMappingURL=symmons_003.js.map