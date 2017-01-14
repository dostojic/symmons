var Symmons = Symmons || {};

! function ($) {
  "use strict";

  jQuery.fn.contactUs = function (options) {
    return this.each(function () {
      var contactUs = Object.create(Symmons.contactUs);
      contactUs.init(this, options);
    });
  };

  //Options
  jQuery.fn.contactUs.options = {
    formSelector: ".frm__contactus",
    uploadSelector: ".file-upload",
    btnSubmitHandler: ".btn-send",
    loadingSelector: ".contact-loading-wrapper",
    rowSelector: ".drop-area-row",
    resetValueSelector: ".drop-area-row__close",
    addAnotherSelector: ".field-addanother",
    deleteSelector: ".img-delete",
    fieldBrowseSelector: ".field-browse",
    maxImageFileSize: 2097152,
    maxFileSize: 5242880,
    templateSelector: "#template-genericListing"
  };

  //Gallery Listing
  Symmons.contactUs = {

    init: function (elem, options) {
      var self = this;
      self.$strCounter = "";
      self.$container = jQuery(elem);
      self.options = jQuery.extend({}, jQuery.fn.contactUs.options, options);
      jQuery(self.options.loadingSelector).removeClass("active");
      jQuery(self.options.btnSubmitHandler).removeClass("disabled");
      self.bindHandlers();

      var strSKUval = self.getParameterName("sku");
      if (strSKUval != "") {
        jQuery(".sku-panel").addClass("active");
        jQuery('.product-skuval').html("#" + strSKUval);
        jQuery('#txtProductSKU').val("#" + strSKUval);
      } else {
        jQuery(".sku-panel").removeClass("active");
      }
      var strDSval = self.getParameterName("dsType");
      if (strDSval != "") {
        jQuery(".design-studio-panel").addClass("active");
        jQuery('.design-studio-val').html(strDSval);
        jQuery('#txtDesignStudioVal').val(strDSval);
      } else {
        jQuery(".design-studio-panel").removeClass("active");
      }

    },

    bindElements: function () {
      var self = this;

    },

    bindHandlers: function () {
      var self = this;
      //
      self.bindDisableState();

      jQuery(document).on("click", self.options.fieldBrowseSelector, function (event) {
        jQuery(this).parent().find(self.options.uploadSelector).click();
      });

      jQuery(document).on("click", self.options.btnSubmitHandler, function (event) {
        self.validateForm();

      });

      jQuery(document).on("change", self.options.uploadSelector, function (event) {
        self.bindFileUpload(this, event);
      });

      jQuery(document).on("click", self.options.addAnotherSelector, function (event) {
        self.bindAnotherFileRow(event);
      });

      jQuery(document).on("click", self.options.deleteSelector, function (event) {
        self.bindDeleteFileRow(event, this);
      });

      jQuery(document).on("click", self.options.resetValueSelector, function (event) {
        self.bindResetValue(event, this);
      });

      jQuery("input.numeric").numeric()

    },

    getParameterName: function (name) {
      var self = this;
      name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
      var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
      results = regex.exec(location.search);
      return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    },

    validateForm: function () {
      var self = this;
      jQuery(self.options.formSelector).validate({
        errorElement: 'em',
        errorPlacement: function (error, element) {
          jQuery(element).closest('.field-wrapper').append(error);
        }
      });

      if (jQuery(self.options.formSelector).valid()) {
        jQuery(self.options.loadingSelector).addClass("active");
        jQuery(self.options.btnSubmitHandler).addClass("disabled");

      }

    },
    bindAnotherFileRow: function (event) {
      var self = this;
      self.$strCounter = jQuery(self.options.rowSelector).length;
      event.preventDefault();
      var strFileUploadTemp = "";
      if (self.$strCounter <= 4) {
        strFileUploadTemp += "<div class='drop-area-row'>  <div class='drop-area-row__fieldwrap'>";
        strFileUploadTemp += "<input type='text' class='field' name='txtPhotoFile_" + eval(self.$strCounter + 1) + "' readonly /> <i class='drop-area-row__close' title='Reset'></i></div>";
        strFileUploadTemp += "<a class='field-browse'>Browse</a>";
        strFileUploadTemp += "<input type='file' class='file-upload' name='files' id='txtPhotoFileUpload_" + eval(self.$strCounter + 1) + "' />";
        strFileUploadTemp += "<i class='img-delete' title='Delete'></i></div>";

      }



      jQuery(strFileUploadTemp).insertBefore(jQuery(self.options.addAnotherSelector));
      self.bindDisableState();

    },

    bindDisableState: function () {
      var self = this;
      if (jQuery(self.options.rowSelector).length == 5) {
        jQuery(self.options.addAnotherSelector).addClass("disabled");
      } else {
        jQuery(self.options.addAnotherSelector).removeClass("disabled");
      }
    },

    bindDeleteFileRow: function (event, thisVal) {
      var self = this;
      var __this = thisVal;
      event.preventDefault();
      jQuery(__this).parent().remove();
      self.bindDisableState();
    },

    bindResetValue: function (event, thisVal) {

      var self = this;
      var __this = thisVal;

      jQuery(__this).parent().find(".field").val("");
      jQuery(__this).parent().parent().find(self.options.uploadSelector).val("");
      event.preventDefault();
    },


    bindFileUpload: function (thisVal, event) {
      var self = this;
      var __this = thisVal;
      event.preventDefault();

      if (jQuery(__this).val() != "") {
        var ext = jQuery(__this).val().split('.').pop().toLowerCase();

        if (jQuery.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp', 'doc', 'docx', 'pdf']) == -1) {
          alert('Please upload only image or documents.');
        }

        if (jQuery.inArray(ext, ['gif', 'png', 'jpg', 'jpeg', 'bmp']) != -1) {
          if (jQuery(__this)[0].files[0].size >= self.options.maxImageFileSize) {
            alert("Please upload a image which is less than 2 MB in file size.");
          } else {
            jQuery(__this).parent().find('.field').val(jQuery(__this).val().split('\\').pop().split('/').pop());

          }
        } else if (jQuery.inArray(ext, ['doc', 'docx', 'pdf']) != -1) {
          if (jQuery(__this)[0].files[0].size >= self.options.maxFileSize) {
            alert("Please upload a file which is less than 5 MB in file size.");
          } else {
            jQuery(__this).parent().find('.field').val(jQuery(__this).val().split('\\').pop().split('/').pop());

          }

        }
      }

    },



  }
}(jQuery);

//Numberic Validation

jQuery.fn.numeric = function (decimal, callback) {
  decimal = decimal || ".";
  callback = typeof callback == "function" ? callback : function () {};
  this.keypress(
    function (e) {
      var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
      // allow enter/return key (only when in an input box)
      if (key == 13 && this.nodeName.toLowerCase() == "input") {
        return true;
      } else if (key == 13) {
        return false;
      }
      var allow = false;
      // allow Ctrl+A
      if ((e.ctrlKey && key == 97 /* firefox */ ) || (e.ctrlKey && key == 65) /* opera */ ) return true;
      // allow Ctrl+X (cut)
      if ((e.ctrlKey && key == 120 /* firefox */ ) || (e.ctrlKey && key == 88) /* opera */ ) return true;
      // allow Ctrl+C (copy)
      if ((e.ctrlKey && key == 99 /* firefox */ ) || (e.ctrlKey && key == 67) /* opera */ ) return true;
      // allow Ctrl+Z (undo)
      if ((e.ctrlKey && key == 122 /* firefox */ ) || (e.ctrlKey && key == 90) /* opera */ ) return true;

      // allow or deny Ctrl+V (paste), Shift+Ins
      if ((e.ctrlKey && key == 118 /* firefox */ ) || (e.ctrlKey && key == 86) /* opera */ || (e.shiftKey && key == 45)) return false;


      // if a number was not pressed
      if (key < 48 || key > 57) {
        /* '-' only allowed at start */
        //if(key == 45 && this.value.length == 0) return true;

        if (key == 45) return true;
        if (key == 40) return true;
        if (key == 41) return true;

        if (key == 35) return false;
        if (key == 36) return false;
        if (key == 37) return false;

        /* only one decimal separator allowed */
        if (key == decimal.charCodeAt(0) && this.value.indexOf(decimal) != -1) {
          allow = false;
        }
        // check for other keys that have special purposes
        if (
          key != 8 /* backspace */ &&
          key != 9 /* tab */ &&
          key != 13 /* enter */ &&
          key != 35 /* end */ &&
          key != 36 /* home */ &&
          key != 37 /* left */ &&
          key != 39 /* right */ &&
          key != 46 /* del */ &&
          key != 221 &&
          key != 219 &&
          key != 189
        ) {
          allow = false;
        } else {
          // for detecting special keys (listed above)
          // IE does not support 'charCode' and ignores them in keypress anyway
          if (typeof e.charCode != "undefined") {
            // special keys have 'keyCode' and 'which' the same (e.g. backspace)
            if (e.keyCode == e.which && e.which != 0) {
              allow = true;
            }
            // or keyCode != 0 and 'charCode'/'which' = 0
            else if (e.keyCode != 0 && e.charCode == 0 && e.which == 0) {
              allow = true;
            }
          }
        }
        // if key pressed is the decimal and it is not already in the field
        if (key == decimal.charCodeAt(0) && this.value.indexOf(decimal) == -1) {
          allow = true;
        }
      } else {
        allow = true;
      }
      return allow;
    }
  )
  .blur(
    function () {
      var val = jQuery(this).val();
      if (val != "") {
        var re = new RegExp("^\\d+$|\\d*" + decimal + "\\d+");
        if (!re.exec(val)) {
          callback.apply(this);
        }
      }
    }
  )
  return this;
}
