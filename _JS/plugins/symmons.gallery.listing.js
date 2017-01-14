var Symmons = Symmons || {};

var myDatas = {};
! function ($) {
  "use strict";

  jQuery.fn.galleryListing = function (options) {
    return this.each(function () {
      var galleryListing = Object.create(Symmons.galleryListing);
      galleryListing.init(this, options);
    });
  };

  //Options
  jQuery.fn.galleryListing.options = {
    contentSelector: ".gallery-thumbnail__listing",
    templateSelector: "#template-Thumbnaillisting",
    urlSelector: jQuery(".gallery-thumbnail__listing").attr('data-ajax-url'),

    featuredContentSelector: '.featured-video__content',
    featuredFrameSelector: 'featured-video__frame',
    featuredTitle: '.featured-video__title',
    featuredTeaser: '.featured-video__teaser',
    videoListSelector: '.gallery-thumbnail__teaser',

    youtubeURL: ' http://www.youtube.com/embed/',
    youtubeAutohide: 1,
    youtubeShowinfo: 0,
    youtubeAllowfullscreen: true,
    featuredImageSelector: "featuredImagePlaceholder"
  };

  //Gallery Listing
  Symmons.galleryListing = {

    init: function (elem, options) {
      var self = this;
      jQuery(".no-records-found").hide();
      self.$container = jQuery(elem);
      self.options = jQuery.extend({}, jQuery.fn.galleryListing.options, options);

      $loading.show();
      self.makeAjaxCall();

    },

    bindElements: function () {
      var self = this;
      self.$videoTrigger = self.$container.find(self.options.videoListSelector);

    },

    bindHandlers: function () {
      var self = this;

      self.$videoTrigger.bind("click", function (event) {
        event.preventDefault();
        self.loadCurrentVideoOnClick(this);
      });

      jQuery(window).resize(function () {
        self.bindPinVideo();
      });
    },

    loadCurrentVideoOnClick: function (objVal) {
      var self = this;
      var __this = jQuery(objVal);

      var strTitle = __this.next().next().find(".gallery-thumbnail__content-title").html(),
        strVideoID = __this.find("img").data("video-url"),
        strTeaserDesc = __this.next().next().find(".gallery-thumbnail__content-teaser").html(),
        strImageURL = __this.find("img").data("image-url");


      if (strTitle != undefined && strTitle != "") {
        jQuery(self.options.featuredTitle).show();
        jQuery(self.options.featuredTitle).html(strTitle);
      } else {
        jQuery(self.options.featuredTitle).hide();
      }


      if (__this.parent().data("content-type") == "video") {
        jQuery("#" + self.options.featuredImageSelector).hide();
        jQuery("#" + self.options.featuredFrameSelector).show();
        jQuery("#" + self.options.featuredFrameSelector).attr('src', self.options.youtubeURL + strVideoID + '?rel=0&autohide=' + self.options.youtubeAutohide + '&showinfo=' + self.options.youtubeShowinfo);
      } else {

        if (Modernizr.mq(Symmons.Settings.MediaQueries.BelowTablet)) {
          
          window.open(strImageURL,'_blank');
          //window.location.href = strImageURL;
          jQuery(self.options.featuredTitle).hide();
        } else {
          jQuery("#" + self.options.featuredFrameSelector).hide();
          jQuery("#" + self.options.featuredImageSelector).show();
          jQuery("#" + self.options.featuredFrameSelector).attr('src', '');
          jQuery("#" + self.options.featuredImageSelector).find("img").attr('src', strImageURL);
        }

      }

      if (strTeaserDesc != undefined && strTeaserDesc != "") {
        jQuery(self.options.featuredTeaser).html(strTeaserDesc);
      } else {
        jQuery(self.options.featuredTeaser).hide();
      }



    },

    makeAjaxCall: function () {
      var self = this;

      // Make the AJAX call
      jQuery.ajax({
        url: self.options.urlSelector,
        type: "GET",
        cache: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        error: function (msg) {
          console.log("Error" + msg.responseText);
        },
        success: function (data) {
          self.bindGalleryListing(data);
          self.bindElements();
          self.bindHandlers();
          self.bindPinVideo();


        }
      });

    },

    bindPinVideo: function () {
      var self = this;
      jQuery(".featured-video__content").removeAttr("style", "");
      jQuery(".pinned").pin({
        containerSelector: ".featured-video__two-cols",
        minWidth: 767
      });
    },

    bindGalleryListing: function (data) {
      var self = this;
      var $container = jQuery(self.options.templateSelector),
        $theTemplate = Handlebars.compile($container.html());
      jQuery(self.options.contentSelector).append($theTemplate(eval(data)));
      self.loadFeaturedVideo(data);

    },

    loadFeaturedVideo: function (data) {
      var self = this;
      var featuredContent = "",
        isVideoExists = 0;

      jQuery.each(jQuery(data)[0].MasterListingData, function (key, innerjson) {

        if (innerjson.ListingType === "video") {
          isVideoExists = 1;

          if (innerjson.ListingTitle != "") {
            jQuery(self.options.featuredTitle).html(innerjson.ListingTitle);
          } else {
            jQuery(self.options.featuredTitle).hide();
          }

          if (innerjson.ListingVideoUrl != "") {

            if (self.options.youtubeAllowfullscreen) {
              featuredContent += '<iframe src="' + self.options.youtubeURL + innerjson.ListingVideoUrl + '?rel=0&autohide=' + self.options.youtubeAutohide + '&showinfo=' + self.options.youtubeShowinfo + '" class="' + self.options.featuredFrameSelector + '" id="' + self.options.featuredFrameSelector + '" frameborder="0" allowfullscreen></iframe><figure class="featured-video__image" id="featuredImagePlaceholder"><img src="" alt="" /></figure>';
            } else {
              featuredContent += '<iframe src="' + self.options.youtubeURL + innerjson.ListingVideoUrl + '?rel=0&autohide=' + self.options.youtubeAutohide + '&showinfo=' + self.options.youtubeShowinfo + '" id="' + self.options.featuredFrameSelector + '" frameborder="0"></iframe>';
            }

          }

          if (innerjson.ListingTeaserDesc != "") {
            featuredContent += '<p class="featured-video__teaser">' + innerjson.ListingTeaserDesc + '</p>';
          }

          return false;

        }

      });

      //No Featured Video Exists
      if (isVideoExists == 0) {
        jQuery(".no-records-found").show();
      }

      //Finally appending the template
      jQuery(self.options.featuredContentSelector).append(featuredContent);
      $loading.hide();
    }
  }

}(jQuery);