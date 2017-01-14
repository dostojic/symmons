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

  jQuery.fn.showHide = function (options) {
    return this.each(function () {
      var showHide = Object.create(Symmons.showHide);
      showHide.init(this, options);
    });
  };

  jQuery.fn.showHide.options = {
    triggerSelector: '.showHide-trigger',
    contentSelector: '.showHide-content',
    startClosed: false,
    openedClass: "is-open",
    closedClass: "is-closed",
    activeClass: "active",
    bodyClicked: true
  };

  Symmons.showHide = {

    init: function (elem, options) {
      var self = this;

      self.$container = jQuery(elem);
      self.options = jQuery.extend({}, jQuery.fn.showHide.options, options);

      self.bindElements();
      self.bindHandlers();
    },

    bindElements: function () {
      var self = this;

      self.$trigger = self.$container.find(self.options.triggerSelector);
      self.$content = self.$container.find(self.options.contentSelector);
    },

    bindHandlers: function () {
      var self = this;

      if (self.options.bodyClicked) {
        jQuery(document).bind("click", function (event) {
          if (!jQuery(event.target).closest(self.$content).length &&
            !jQuery(event.target).closest(self.$trigger).length) {
            self.hideContent();
          }
        });
      }

      self.$trigger.bind("click", function (event) {
        var currentElement = jQuery(this);
        var currentElementTagName = currentElement.prop("tagName");

        if (self.options.activeClass != "") {
          if (currentElement.parent().children().hasClass(self.options.activeClass)) {
            currentElement.parent().children().removeClass(self.options.activeClass);
          }
          currentElement.addClass(self.options.activeClass);
        }
        if (currentElementTagName == "A" || currentElementTagName == "a") {
          var href = jQuery(this).attr('href');
          if (href == "" || href == null) {
            event.preventDefault();
          }
        }
        self.toggleContent();
      });
    },

    toggleContent: function () {
      var self = this;

      if (self.isOpen) {
        self.hideContent();
      } else {
        self.showContent();
      }
    },

    showContent: function () {
      var self = this;

      if (self.isOpen) {
        return;
      }

      self.isOpen = true;

      self.$container.addClass(self.options.openedClass);
      self.$container.removeClass(self.options.closedClass);
    },

    hideContent: function () {
      var self = this;

      if (!self.isOpen) {
        return;
      }

      self.isOpen = false;

      self.$container.addClass(self.options.closedClass);
      self.$container.removeClass(self.options.openedClass);

      if (self.options.activeClass != "") {
        jQuery(self.options.triggerSelector).removeClass(self.options.activeClass);
      }
    },

    reset: function () {
      self.$container.removeClass(self.options.closedClass);
      self.$container.removeClass(self.options.openedClass);

      self.$trigger.unbind();
    }
  }
});