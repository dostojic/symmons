var Symmons = Symmons || {};

!function ($) {
  "use strict";

  jQuery.fn.toggleContent = function (options) {
    return this.each(function () {
      var toggleContent = Object.create(Symmons.toggleContent);
      toggleContent.init(this, options);
    });
  };

  jQuery.fn.toggleContent.options = {
      showLimit: 3,
      showMoreObject: '',
      toggleMode: 'list',
      showContainerHeight: 190
    },
    Symmons.toggleContent = {
      init: function (elem, options) {
        var self = this;
        self.$container = jQuery(elem);
        self.options = jQuery.extend({}, jQuery.fn.toggleContent.options, options);
        self.$isClicked = false;

        if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {

          if (self.options.toggleMode == "list") {
            self.bindElementsBasedOnList();

          } else if (self.options.toggleMode == "height") {
            self.bindElementsBasedOnHeight();

          }

        } else {
          self.options.showMoreObject.hide();
        }

        self.bindElementsBasedOnTabs();

        self.bindHandlers();
      },

      bindElementsBasedOnHeight: function () {
        var self = this;
        self.$container.addClass("collapsed");
        self.$container.attr("style","height:"+ self.options.showContainerHeight + "px; overflow:hidden");
      },

      bindElementsBasedOnHeightClick: function () {
        var self = this;
        self.$container.removeClass("collapsed");
        self.options.showMoreObject.hide();
         self.$container.removeAttr("style");
      },

      bindElementsBasedOnList: function () {
        var self = this;

        self.$showContent = self.$container.find("li:gt(" + eval(self.options.showLimit - 1) + ")");
        self.$showContent.hide();

        if (self.$container.children().length <= self.options.showLimit) {
          self.options.showMoreObject.hide();
        }
      },

      bindElementsBasedOnListToggle: function () {
        var self = this;

        self.$showContent.slideToggle(300);
        self.options.showMoreObject.hide();
        return false;
      },



      bindElementsBasedOnTabs: function () {
        var self = this;
      },

      bindHandlers: function (event) {
        var self = this;

        self.options.showMoreObject.bind("click", function (event) {
          event.preventDefault();
          if (self.options.toggleMode == "list") {
            self.bindElementsBasedOnListToggle(event);

          } else if (self.options.toggleMode == "height") {
            self.bindElementsBasedOnHeightClick(event);

          }
          self.$isClicked = true;
        });

        jQuery(window).resize(function () {

          if (self.$isClicked == false) {
            if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {
              self.options.showMoreObject.show();
              if (self.options.toggleMode == "list") {
                self.bindElementsBasedOnList();

              } else if (self.options.toggleMode == "height") {
                self.bindElementsBasedOnHeight();

              }

            } else {
              self.options.showMoreObject.hide();
              self.resetAllContent();
            }
          }
        });
      },

      resetAllContent: function () {

        var self = this;
        if (self.options.toggleMode == "list") {
          self.$container.find("li").show(300);
          return false;
        } else if (self.options.toggleMode == "height") {
          self.$container.removeClass("collapsed");
           self.$container.removeAttr("style");
        }
      }

    }
}(jQuery);