using System;
using System.IO;
using ikvm.io;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.ContentSearch.Diagnostics;
using Sitecore.ContentSearch.Extracters.IFilterTextExtraction;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace symmons.com._Classes.Symmons.Custom
{
    public class MediaContentExtractor : IComputedIndexField
    {
        public string FieldName { get; set; }
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            Item item = (SitecoreIndexableItem)indexable;
            Assert.ArgumentNotNull(item, "item");

            object result = null;
            if (item != null && item.Paths.IsMediaItem)
            {
                var _media = (MediaItem)item;
                string ext = _media.Extension.ToLower();
                if (ext == "pdf" || _media.MimeType == "application/pdf")
                {
                    result = ParsePDF(_media);
                }
                else if (ext.Contains("doc") || ext.Contains("xls") || ext.Contains("ppt"))
                {
                    result = ParseOfficeDoc(_media);
                }
            }

            return result;
        }

        /// <summary>
        /// Function to parse PDF content
        /// </summary>
        /// <param name="mediaItem"></param>
        /// <returns></returns>
        private string ParsePDF(MediaItem mediaItem)
        {
            PDDocument doc = null;
            string content = string.Empty;
            InputStreamWrapper wrapper = null;

            if (mediaItem != null)
            {
                try
                {
                    wrapper = new InputStreamWrapper(mediaItem.GetMediaStream());
                    doc = PDDocument.load(wrapper);
                    content = new PDFTextStripper().getText(doc);
                }
                catch (Exception ex)
                {
                    CrawlingLog.Log.Error(ex.ToString(), ex);
                    return string.Empty;
                }
                finally
                {
                    if ((doc != null))
                    {
                        doc.close();
                        wrapper.close();
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(content))
            {
                content = content.Replace("\r\n", " ").ToLower();
            }

            return content;
        }

        /// <summary>
        /// Function to parse Office document
        /// </summary>
        /// <param name="mediaItem"></param>
        /// <returns></returns>
        private string ParseOfficeDoc(MediaItem mediaItem)
        {
            string content = string.Empty;
            try
            {
                var streamReader = mediaItem.GetMediaStream();
                TextReader reader = new FilterReader(((FileStream)streamReader).Name);
                using (reader)
                {
                    content = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                CrawlingLog.Log.Error(ex.ToString(), ex);
            }

            if (!string.IsNullOrWhiteSpace(content))
            {
                content = content.Replace("\r\n", " ").ToLower();
            }
            return content;
        }
    }
}
