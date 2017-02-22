
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace symmons.com._Classes.Symmons.Helpers
{
    public static class DataHelper
    {
        /// <summary>
        /// Get teh parsed string value based on maximum characters allowed.
        /// </summary>
        /// <param name="fieldValue">string value</param>
        /// <param name="validateCount">maximum characters count</param>
        /// <returns>Parsed string value</returns>
        public static string GetParsedString(string fieldValue, int validateCount)
        {
            return fieldValue.Length <= validateCount ? fieldValue : fieldValue.Substring(0, validateCount);
        }

        public static string GetImageURL(ImageField imageField)
        {
            string imageURL = string.Empty;
            
            if (imageField != null && imageField.MediaItem != null)
            {
                MediaItem image = new MediaItem(imageField.MediaItem);
                imageURL = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));
            }
            
            return imageURL;
        }

        public static string GetItemUrl(Item item)
        {
            return LinkManager.GetItemUrl(item);
        }

        public static string GetTextualFieldValue(TextField textField)
        {
            string value = string.Empty;

            if (textField != null && textField.Value != null)
            {
                value = textField.Value;
            }

            return value;
        }
    }
}