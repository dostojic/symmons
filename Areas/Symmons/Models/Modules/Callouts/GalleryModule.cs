using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Fields;

namespace symmons.com.Areas.Symmons.Models.Modules.Callouts
{
    [SitecoreType(TemplateId = "{711E13D6-2257-4945-8147-0034BD6AE707}", AutoMap = true)]
    public class GalleryModule
    {
        [SitecoreId]
        public virtual Guid Id { get; set; }

        [SitecoreField(FieldName = "Gallery List")]
        public virtual IEnumerable<Gallery> GalleryList { get; set; }

        [SitecoreField(FieldName = "Latest Videos Headline")]
        public virtual string LatestVideoHeadline { get; set; }

        [SitecoreField(FieldName = "Show More Link")]
        public virtual Link ShowMoreLink { get; set; }

        [SitecoreField(FieldName = "No Videos Text")]
        public virtual string NoVideos { get; set; }

        [SitecoreField(FieldName = "Max Videos")]
        public virtual bool MaxVideos { get; set; }

        public virtual string GalleryClass{ get; set; }
    }

    public class GalleryListingData
    {
        public string ListingId { get; set; }
        public string ListingType { get; set; }
        public string ListingTitle { get; set; }
        public string ListingUrl { get; set; }
        public string ListingThumbImageUrl { get; set; }
        public string ListingImageAlt { get; set; }
        public string ListingVideoUrl { get; set; }
        public string ListingImageUrl { get; set; }
        public string ListingTeaserDesc { get; set; }
    }

    public class MasterListing
    {
        public List<GalleryListingData> MasterListingData { get; set; }
    }
}
