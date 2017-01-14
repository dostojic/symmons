var Symmons = Symmons || {};
var $loading = jQuery(".loading-wrapper");

jQuery(function () {

    Symmons.Settings = {

        MediaQueries: {
            Mobile: "only screen",
            BelowTablet: "only screen and (max-width: 767px)",
            Tablet: "only screen and (min-width: 768px)",
            BelowDesktop: "only screen and (max-width: 1024px)",
            Desktop: "only screen and (min-width: 1025px)",
            Any: "only screen"
        }
    };
});