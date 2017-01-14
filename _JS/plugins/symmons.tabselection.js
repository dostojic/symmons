var Symmons = Symmons || {};

! function ($) {
  "use strict";

  jQuery.fn.tabSelection = function (options) {
    return this.each(function () {
      var tabSelection = Object.create(Symmons.tabSelection);
      tabSelection.init(this, options);
    });
  };

  jQuery.fn.tabSelection.options = {
      triggerSelector: '',
      contentSelector: '',
      contentTabSelector: '',
      backButton: '',
      activeClass: 'active',
      triggeredClass: 'triggered',
      urlParamName: 'tab',
      isMobileEnabled: false
    },
    Symmons.tabSelection = {
      init: function (elem, options) {
        var self = this;
        self.$container = jQuery(elem);
        self.options = jQuery.extend({}, jQuery.fn.tabSelection.options, options);
        self.bindHandlers();
        self.bindTabsContent();

      },

      bindHandlers: function () {
        var self = this;

        jQuery(document).on("click", self.options.backButton, function (event) {

          jQuery(self.options.contentSelector).removeClass(self.options.activeClass);
          jQuery(self.options.contentTabSelector).removeClass(self.options.triggeredClass);
        });

        jQuery(document).on("click", self.options.triggerSelector, function (event) {
          self.bindTabClick(event, this, "");
        });

        var supportsOrientationChange = "onorientationchange" in window,
          orientationEvent = supportsOrientationChange ? "orientationchange" : "resize";

        window.addEventListener(orientationEvent, function () {
          // alert('HOLY ROTATING SCREENS BATMAN:' + window.orientation + " " + screen.width);
          if (window.orientation == undefined) {
            if (self.options.isMobileEnabled) {
              if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {
                jQuery(self.options.contentSelector).first().removeClass(self.options.activeClass);
                jQuery(self.options.triggerSelector).first().removeClass(self.options.activeClass);

              } else {
                jQuery(self.options.triggerSelector).removeClass(self.options.activeClass);
                jQuery(self.options.contentSelector).removeClass(self.options.activeClass);
                jQuery(self.options.contentSelector).first().addClass(self.options.activeClass);
                jQuery(self.options.triggerSelector).first().addClass(self.options.activeClass);

                jQuery(self.options.triggerSelector).parent().removeClass(self.options.triggeredClass);
              }
            }
          }
        }, false);

},

bindTabsContent: function (event) {
    var self = this;
    var $selectedTab = self.getUrlParameter(self.options.urlParamName);

    if ($selectedTab != "") {
      self.bindTabClick(event, this, $selectedTab);
    } else {

      if (self.options.isMobileEnabled) {
        if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {
          // jQuery(self.options.contentSelector).first().addClass(self.options.activeClass);
          //jQuery(self.options.triggerSelector).first().addClass(self.options.activeClass);
        } else {

          jQuery(self.options.contentSelector).first().addClass(self.options.activeClass);
          jQuery(self.options.triggerSelector).first().addClass(self.options.activeClass);
        }

      } else {
        jQuery(self.options.contentSelector).first().addClass(self.options.activeClass);
        jQuery(self.options.triggerSelector).first().addClass(self.options.activeClass);
      }
    }
  },



  bindTabClick: function (event, $this, $datatab) {
    var self = this;


    if ($datatab === "") {
      jQuery($this).addClass(self.options.activeClass).siblings('[data-tab]').removeClass(self.options.activeClass);
      jQuery($this).parent().parent().find('[data-content]').removeClass(self.options.activeClass);
      jQuery($this).parent().parent().find('[data-content=' + jQuery($this).data('tab') + ']').addClass(self.options.activeClass);

    } else {
      jQuery(self.options.contentSelector).parent().find('[data-content]').removeClass(self.options.activeClass);
      jQuery(self.options.contentSelector).parent().find('[data-content=' + $datatab + ']').addClass(self.options.activeClass);
      jQuery(self.$container).find('[data-tab]').removeClass(self.options.activeClass);
      jQuery(self.$container).find('[data-tab=' + $datatab + ']').addClass(self.options.activeClass);
    }

    jQuery($this).parent().parent().addClass(self.options.triggeredClass);



  },

  getUrlParameter: function (name) {
    var self = this;
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
      results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
  }


}
}(jQuery);