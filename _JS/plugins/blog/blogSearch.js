var Verndale = Verndale || {};

jQuery(function ($) {

    $.fn.blogSearch = function( options ) {
        return this.each(function() {   
            var blogSearch = Object.create(Verndale.blogSearch);  
            blogSearch.init(this, options);
        });
    };

    $.fn.blogSearch.options = {
        
    };

    Verndale.blogSearch = {
        init: function( elem, options ) {
            var self = this;

            self.$container = $( elem );
            self.options = $.extend({}, $.fn.blogSearch.options, options);
            
            self.options.container = self.$container;

            self.bindElements();
            self.bindEvents();
        },
        bindElements: function() {
            var self = this;

            self.$submit = self.$container.find("input[type='submit']");
            self.$searchField = self.$container.find("input[type='search']");
        },
        bindEvents: function() {
            var self = this;

            self.$submit.on("click", function(e) {
                e.preventDefault()
                var curUrl = window.location.href.split( /[\s?]+/ )[0];
                var urlStr = curUrl;
                var textInput = self.$searchField.val();
                
                var newUrl = curUrl + "?search=" + textInput;

                window.location.href = newUrl;
            });

            self.$searchField.keypress( function(e) {
                if(e.which == 13) {
                    var curUrl = window.location.href.split( /[\s?]+/ )[0];
                    var urlStr = curUrl;
                    var textInput = self.$searchField.val();
                    
                    var newUrl = curUrl + "?search=" + textInput;

                    window.location.href = newUrl;
                }
            });
        },
    };
});


