// Sort, filter and get the items by AJAX 
// This plugin is dependant on history.js
!function(a,b,c,d){
// Polyfill for Object.create() in IE8
"function"!=typeof Object.create&&(Object.create=function(a){function b(){}return b.prototype=a,new b});var e={
// Initialize the plugin.
// Setting up Global variables 
init:function(b,c){var d=this;
//The Element (trigger)
d.$elem=a(c),
// Extending the default set of options with the ones set by the user
d.options=a.extend({},a.fn.paginate.options,b),
// Setting variables used across and within the scope of the plugin
d.pageTitle=a("title").text(),d.query=d.options.query,d.pagerData=d.options.pagerData,d.pageData=d.options.pageData,d.prev=d.options.prev,d.next=d.options.next,d.first=d.options.first,d.last=d.options.last,d.pageInput=d.options.pageInput,d.totalPagesElem=d.options.totalPagesElem,d.updatePagerState=d.options.updatePagerState,d.updatePagerURL=d.options.updatePagerURL,d.onAfter=d.options.onAfter,d.ajaxURLData=d.options.ajaxURLData,d.onBefore=d.options.onBefore,d.onSuccess=d.options.onSuccess,d.pushInitialAjaxUrl(d.query,1),d.listen(),d.autoScroll=!0,d.bookmarkName=d.options.bookmarkName},getLastPageNum:function(){var a=this;return parseInt(a.totalPagesElem.eq(0).text())},refresh:function(b,c){var e=this,f=a("[data-"+e.pagerData+"]");typeof c!==d&&(1==c?f.attr("disabled",""):f.removeAttr("disabled"),e.totalPagesElem.text(c)),url=new Url(e.$elem.attr("data-ajax-url")),url.query.pageNum=1,e.$elem.attr("data-ajax-url",url),e.updatePager(b)},getURL:function(){var
// Get the currennt URL
a=b.location.pathname;return a},pushDefaultURL:function(){var c=this,d=c.getURL(),d=d.split("/"),e=a.inArray(c.bookmarkName,d),f=e==-1?"/"+c.bookmarkName+"/1":"";a("body").attr("data-url",b.location+f),c.updatePagerURL&&(History.replaceState(null,"",c.getURL()+f),a("title").text(c.pageTitle))},pushInitialAjaxUrl:function(b,c){var e=this,f=e.getURL(),f=f.split("/"),g=a.inArray(e.bookmarkName,f),c=g+c,h=f[c]||1;"-"!=h&&typeof h!=typeof d&&"undefined"!=h&&h!=d||(h=""),e.buildajaxURL(h)},getNewPageNum:function(a){var b=this,c=new Url(b.$elem.attr("data-"+b.ajaxURLData));return curPage=c.query[b.query]||1,a==b.next?curPage=parseInt(curPage)+1:a==b.prev?curPage=parseInt(curPage)-1:a==b.first?curPage=1:a==b.last?b.getLastPageNum():a},compileURL:function(b){var c=this,d=a("body").attr("data-url"),d=d.split("/"),e=a.inArray(c.bookmarkName,d)+1;d.splice(e,1,b),a("body").attr("data-url",d.join("/")),c.updatePagerURL&&(History.replaceState(null,"",d.join("/")),a("title").text(c.pageTitle))},buildajaxURL:function(a){var b=this,c=new Url(b.$elem.attr("data-"+b.ajaxURLData));c.query[b.query]=a,b.$elem.attr("data-"+b.ajaxURLData,c)},runAJAX:function(){var b=this,c=new Url(b.$elem.attr("data-"+b.ajaxURLData));a.ajax({url:c,type:"GET",cache:!0,success:function(d){b.updatePagerState&&(b.updatePager(c.query[b.query]),b.autoScroll&&a("body, html").animate({scrollTop:b.$elem.offset().top-100},"500")),b.onSuccess.apply(b.$elem,arguments)}})},updatePager:function(b){var c=this,d=a("[data-"+c.pageData+'="'+c.next+'"]'),e=a("[data-"+c.pageData+'="'+c.prev+'"]'),f=a("[data-"+c.pageData+'="'+c.first+'"]'),g=a("[data-"+c.pageData+'="'+c.last+'"]');a("[data-"+c.pagerData+"]").find("[data-"+c.pageData+"]").removeAttr("disabled"),1==b?(f.attr("disabled",""),e.attr("disabled","")):b==c.getLastPageNum()&&(g.attr("disabled",""),d.attr("disabled","")),c.pageInput.html(b)},validate:function(b){var c=this;return!!(a.isNumeric(b)&&b<=c.getLastPageNum()&&b>0)},listen:function(){var b=this,c="";
// On 'change' of a filter selectbox
a("[data-"+b.pageData+"]").on("click",function(c){var d=(a(this),a(this).attr("data-"+b.pageData)),e=b.getNewPageNum(d);b.pushDefaultURL(),b.compileURL(e),b.onBefore.apply(b.$elem),b.buildajaxURL(e),b.runAJAX(),c.preventDefault()}),b.pageInput.on("focusin",function(b){c=a(this).val()}),b.pageInput.on("focusout keyup",function(d){c!==a(this).val()?13!=d.which&&d.which||a(this).val(function(a,d){return b.validate(d)?(b.pushDefaultURL(),b.compileURL(d),b.buildajaxURL(d),b.runAJAX(),d):c}):a(this).val(c)})}};a.fn.paginate=function(b){
// Return this for all the matched objects
// This also allows chaining
return this.each(function(){
// Create a new instance of the object and assign it to 
// jQuery's prototype function
var c=Object.create(e);c.init(b,this),
// Allow access of the plugin data to the user
// fecilitating creation of methods externally
a.data(this,"paginate",c)})},a.fn.paginate.options={query:"pageNum",bookmarkName:"page",pagerData:"pager",pageData:"page",next:"next",prev:"prev",first:"first",last:"last",pageInput:a(".pagination__input"),updatePagerState:!0,updatePagerURL:!1,totalPagesElem:a(".pagination__totalpages"),ajaxURLData:"ajax-url",onBefore:function(){},onSuccess:function(){}}}(jQuery,window,document);
//# sourceMappingURL=symmons_009.js.map