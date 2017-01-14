// Sort, filter and get the items by AJAX 
// This plugin is dependant on history.js

;
(function ($, window, document, undefined) {

  // Polyfill for Object.create() in IE8
  if (typeof Object.create !== 'function') {
    Object.create = function (obj) {
      function F() {};
      F.prototype = obj;
      return new F();
    };
  }

  var Paginate = {

    // Initialize the plugin.
    // Setting up Global variables 
    init: function (options, elem) {
      var self = this;

      //The Element (trigger)
      self.$elem = $(elem);

      // Extending the default set of options with the ones set by the user
      self.options = $.extend({}, $.fn.paginate.options, options);

      // Setting variables used across and within the scope of the plugin
      self.pageTitle = $('title').text();
      self.query = self.options.query;
      self.pagerData = self.options.pagerData;
      self.pageData = self.options.pageData;
      self.prev = self.options.prev;
      self.next = self.options.next;
      self.first = self.options.first;
      self.last = self.options.last;
      self.pageInput = self.options.pageInput;
      self.totalPagesElem = self.options.totalPagesElem;
      self.updatePagerState = self.options.updatePagerState;
      self.updatePagerURL = self.options.updatePagerURL;
      self.onAfter = self.options.onAfter;
      self.ajaxURLData = self.options.ajaxURLData;
      self.onBefore = self.options.onBefore;
      self.onSuccess = self.options.onSuccess;
      self.pushInitialAjaxUrl(self.query, 1);
      self.listen();
      self.autoScroll = true;
      self.bookmarkName = self.options.bookmarkName;
   
      
      
    },

    getLastPageNum: function () {
      var self = this;
      return parseInt(self.totalPagesElem.eq(0).text());
    },

    refresh: function (current, total) {
      var self = this,
        $pager = $('[data-' + self.pagerData + ']');

      if (typeof total !== undefined) {
        (total == 1) ? $pager.attr('disabled', ''): $pager.removeAttr('disabled');

        self.totalPagesElem.text(total)
      }

      
      url = new Url(self.$elem.attr('data-ajax-url'));
      url.query['pageNum'] = 1;      
      self.$elem.attr('data-ajax-url', url);
      
      self.updatePager(current);
    },

    getURL: function () {
      var self = this,
        // Get the currennt URL
        url = window.location.pathname;

      return (url);
    },

    pushDefaultURL: function () {
      var self = this,
        url = self.getURL(),
        url = url.split('/'),
        pos = $.inArray(self.bookmarkName, url);

      var urlToPush = (pos == -1) ? '/' + self.bookmarkName + '/1' : '';

      $('body').attr('data-url', window.location + urlToPush);

      if (self.updatePagerURL) {

        History.replaceState(null, '', self.getURL() + urlToPush);
        $('title').text(self.pageTitle);
      }

    },

    pushInitialAjaxUrl: function (query, pos) {
      var self = this,
        url = self.getURL(),
        url = url.split('/'),
        basePos = $.inArray(self.bookmarkName, url),
        pos = basePos + pos,
        value = url[pos] || 1;

      if (value == '-' || typeof value == typeof undefined || value == 'undefined' || value == undefined) value = '';

      self.buildajaxURL(value);
    },

    getNewPageNum: function (val) {
      var self = this,
        url = new Url(self.$elem.attr('data-' + self.ajaxURLData));

      curPage = url.query[self.query] || 1;

      if (val == self.next) {
        return curPage = parseInt(curPage) + 1;
      } else if (val == self.prev) {
        return curPage = parseInt(curPage) - 1;
      } else if (val == self.first) {
        return curPage = 1;
      } else if (val == self.last) {
        return self.getLastPageNum();
      } else {
        return val;
      }
    },

    compileURL: function (val) {
      var self = this,
        url = $('body').attr('data-url'),
        url = url.split('/'),
        pos = $.inArray(self.bookmarkName, url) + 1;

      url.splice(pos, 1, val);

      $('body').attr('data-url', url.join('/'));

      if (self.updatePagerURL) {
        History.replaceState(null, '', url.join('/'));
        $('title').text(self.pageTitle);
      }
    },

    buildajaxURL: function (val) {
      var self = this,
        url = new Url(self.$elem.attr('data-' + self.ajaxURLData));

      url.query[self.query] = val;
        
    self.$elem.attr('data-' + self.ajaxURLData, url);
    },

    runAJAX: function () {
      var self = this,
        url = new Url(self.$elem.attr('data-' + self.ajaxURLData));

      

      $.ajax({
        url: url,
        type: "GET",
        cache: true,
        success: function (result) {
          if (self.updatePagerState) {
            self.updatePager(url.query[self.query]);
            if (self.autoScroll) {
              $('body, html').animate({
                scrollTop: self.$elem.offset().top - 100
              }, '500');
            }

          }
          self.onSuccess.apply(self.$elem, arguments);
        }
      });
    },

    updatePager: function (page) {
      var self = this,
        $next = $('[data-' + self.pageData + '="' + self.next + '"]'),
        $prev = $('[data-' + self.pageData + '="' + self.prev + '"]'),
        $first = $('[data-' + self.pageData + '="' + self.first + '"]'),
        $last = $('[data-' + self.pageData + '="' + self.last + '"]');

      $('[data-' + self.pagerData + ']')
        .find('[data-' + self.pageData + ']')
        .removeAttr('disabled');

      if (page == 1) {
        $first.attr('disabled', '');
        $prev.attr('disabled', '');
      } else if (page == self.getLastPageNum()) {
        $last.attr('disabled', '');
        $next.attr('disabled', '');
      };

      self.pageInput.html(page);
    },

    validate: function (val) {
      var self = this;

      if ($.isNumeric(val) && val <= self.getLastPageNum() && val > 0) {
        return true;
      } else {
        return false;
      };
    },

    listen: function () {
      var self = this,
        oldVal = "";

      // On 'change' of a filter selectbox
      $('[data-' + self.pageData + ']').on('click', function (e) {
        var $elem = $(this),
          val = $(this).attr('data-' + self.pageData),
          newVal = self.getNewPageNum(val);

        self.pushDefaultURL();
        self.compileURL(newVal);
        self.onBefore.apply(self.$elem);
        self.buildajaxURL(newVal);
        self.runAJAX();

        e.preventDefault();
      });

      self.pageInput.on('focusin', function (e) {
        oldVal = $(this).val();
      });

      self.pageInput.on('focusout keyup', function (e) {
        if (oldVal !== $(this).val()) {
          if (e.which == 13 || !e.which) {
            $(this).val(function (i, val) {
              if (self.validate(val)) {

                self.pushDefaultURL();
                self.compileURL(val);
                self.buildajaxURL(val);
                self.runAJAX();

                return val;
              } else {
                return oldVal;
              }
            });
          }
        } else {
          $(this).val(oldVal);
        }
      });
    }
  };

  $.fn.paginate = function (options) {
    // Return this for all the matched objects
    // This also allows chaining
    return this.each(function () {

      // Create a new instance of the object and assign it to 
      // jQuery's prototype function
      var paginate = Object.create(Paginate);
      paginate.init(options, this);

      // Allow access of the plugin data to the user
      // fecilitating creation of methods externally
      $.data(this, 'paginate', paginate);
    });
  };

  $.fn.paginate.options = {
    query: 'pageNum',
    bookmarkName: 'page',
    pagerData: 'pager',
    pageData: 'page',
    next: 'next',
    prev: 'prev',
    first: 'first',
    last: 'last',
    pageInput: $('.pagination__input'),
    updatePagerState: true,
    updatePagerURL: false,
    totalPagesElem: $('.pagination__totalpages'),
    ajaxURLData: 'ajax-url',
    onBefore: function () {},
    onSuccess: function () {} // On Ajax success
  };

})(jQuery, window, document);