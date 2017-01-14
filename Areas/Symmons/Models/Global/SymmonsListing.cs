using System.Collections.Generic;

namespace symmons.com.Areas.Symmons.Models.Global
{
    public class SymmonsListing
    {
        public class GenericListData
        {
            public string ListingId { get; set; }
            public string ListingType { get; set; }
            public string ListingTitle { get; set; }
            public string ListingSubTitle { get; set; }
            public string ListingUrl { get; set; }
            public string ListingThumbImageUrl { get; set; }
            public string ListingImageAlt { get; set; }
            public string ListingVideoUrl { get; set; }
            public string ListingImageUrl { get; set; }
            public string ListingTeaserDesc { get; set; }
            public string ListingDate { get; set; }
            public string ListingCTA { get; set; }
            public string ListingCtaUrl { get; set; }
        }

        public class GenericListing
        {
            public int TotalRecordCount { get; set; }
            public int TotalPagesCount { get; set; }
            public List<GenericListData> GenericListingData { get; set; }
        }

        public class SearchListingData
        {
            public string ListingId { get; set; }
            public string ListingTitle { get; set; }
            public string ListingTeaserDesc { get; set; }
            public string ListingCTA { get; set; }
            public string ListingShowUrl { get; set; }
        }

        public class SearchListing
        {
            public string TotalRecordCount { get; set; }
            public string TotalPagesCount { get; set; }
            public string TotalProductCount { get; set; }
            public List<SearchListingData> SearchListingData { get; set; }
        }

        public class SalesRepresentativeListingData
        {
            public string ListingId { get; set; }
            public string ListingFirstName { get; set; }
            public string ListingLastName { get; set; }
            public string ListingAddress { get; set; }
            public string ListingPhone { get; set; }
            public string ListingEmail { get; set; }
            public string ListingCity { get; set; }
            public string ListingZip { get; set; }
        }

        public class SalesRepListing
        {
            public int TotalRecordCount { get; set; }
            public int TotalPagesCount { get; set; }
            public string SelectedStateId { get; set; }
            public List<SalesRepresentativeListingData> SalesRepListingData { get; set; }
        }
    }
}