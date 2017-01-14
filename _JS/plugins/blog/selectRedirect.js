var Verndale = Verndale || {};

jQuery(function ($) {

    $.fn.selectRedirect = function( options ) {
        return this.each(function() {   
            var selectRedirect = Object.create(Verndale.selectRedirect);  
            selectRedirect.init(this, options);
        });
    };

    $.fn.selectRedirect.options = {
        select: '.wb-entries',
        href: 'data-href'
    };

    Verndale.selectRedirect = {
        init: function( elem, options ) {
            var self = this;

            self.$container = $( elem );
            self.options = $.extend({}, $.fn.selectRedirect.options, options);
            
            self.options.container = self.$container;

            self.bindElements();
            self.bindEvents();
        },
        bindElements: function() {
            var self = this;

            self.$select = self.$container.find(self.options.select);
        },
        bindEvents: function() {
            var self = this;

            self.$select.on("change", function(e) {
                e.preventDefault()
                var selected = $(this).find('option:selected');
                var url = selected.attr(self.options.href);

                window.location.href = url;
            });
        },
    };
});


