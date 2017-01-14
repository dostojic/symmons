using System.Collections.Generic;

namespace symmons.com._Classes.Symmons.Global
{
    public class StoresContainer
    {
        public string StoreTypeName { get; set; }
        public string StoreTypeId { get; set; }
        public Store Store { get; set; }
    }

    public class StoresContainerMaster
    {
        public int TotalRecordCount { get; set; }
        public string LocationNearTitle { get; set; }
        public string LocationNear { get; set; }
        public string LatitudeValues { get; set; }
        public string LongitudeValues { get; set; }
        public List<StoresContainer> StoresListingData { get; set; }
    }

    public class Store
    {
        public string StoreMapPin { get; set; }
        public string StoreName { get; set; }
        public string IsSymmonsPreferred { get; set; }
        public string IsSymmonsPreferredDescription { get; set; }
        public string Address { get; set; }
        public string Distance { get; set; }
        public double DistanceInMiles { get; set; }
        public string DirectionsTitle { get; set; }
        public string Directions { get; set; }
        public string PhoneNo { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MoreLikeThis { get; set; }
        public string MoreLocationsLikeThis { get; set; }
    }

    public class StoreTypeStores
    {
        public int TotalRecordCount { get; set; }
        public int TotalPagesCount { get; set; }
        public string LocationNearTitle { get; set; }
        public string LocationNear { get; set; }
        public string LatitudeValues { get; set; }
        public string LongitudeValues { get; set; }
        public string StoreTypeName { get; set; }
        public string StoreTypeId { get; set; }
        public List<Store> MoreStores { get; set; }
    }
}