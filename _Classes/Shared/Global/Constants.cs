using System.Configuration;
 

namespace symmons.com._Classes.Shared.Global
{
    public static class Constants
    {
        public static class ViewPaths
        {
            public static string Header
            {
                get { return ConfigurationManager.AppSettings["ViewPath_Header"]; }
            }
            public static string ContentHeader
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ContentHeader"]; }
            }
            public static string UtilityLinks
            {
                get { return ConfigurationManager.AppSettings["ViewPath_UtilityLinks"]; }
            }
            public static string PrimaryNavigation
            {
                get { return ConfigurationManager.AppSettings["ViewPath_PrimaryNavigation"]; }
            }
            public static string Footer
            {
                get { return ConfigurationManager.AppSettings["ViewPath_Footer"]; }
            }
            public static string FooterNavigation
            {
                get { return ConfigurationManager.AppSettings["ViewPath_FooterNavigation"]; }
            }
            public static string HeaderIncludesPartial
            {
                get { return ConfigurationManager.AppSettings["ViewPath_HeaderIncludesPartial"]; }
            }
            public static string FooterIncludesPartial
            {
                get { return ConfigurationManager.AppSettings["ViewPath_FooterIncludesPartial"]; }
            }
            public static string Breadcrumb
            {
                get { return ConfigurationManager.AppSettings["ViewPath_Breadcrumb"]; }
            }
            public static string SocialMediaConnect
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SocialMediaConnect"]; }
            }
            public static string ContactInfoCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ContactInfoCallout"]; }
            }
            public static string UpcomingEvents
            {
                get { return ConfigurationManager.AppSettings["ViewPath_UpcomingEvents"]; }
            }
            public static string FeaturedNews
            {
                get { return ConfigurationManager.AppSettings["ViewPath_FeaturedNews"]; }
            }
            public static string RecentNews
            {
                get { return ConfigurationManager.AppSettings["ViewPath_RecentNews"]; }
            }
            public static string LatestVideos
            {
                get { return ConfigurationManager.AppSettings["ViewPath_LatestVideos"]; }
            }
            public static string ProductDetails
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductDetails"]; }
            }
            public static string WtbProductRetailExclusive
            {
                get { return ConfigurationManager.AppSettings["ViewPath_WtbProductRetailExclusive"]; }
            }
            public static string ProductSpecialAttribute
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductSpecialAttribute"]; }
            }
            public static string ProductSmartAttribute
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductSmartAttribute"]; }
            }
            public static string DocumentsAndDownloads
            {
                get { return ConfigurationManager.AppSettings["ViewPath_DocumentsAndDownloads"]; }
            }
            public static string ProductFeatures
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductFeatures"]; }
            }
            public static string ProductCollection
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductCollection"]; }
            }
            public static string ProductPrice
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductPrice"]; }
            }
            public static string ProductDimensions
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductDimensions"]; }
            }
            public static string ProductInteractiveTour
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductInteractiveTour"]; }
            }
            public static string Support
            {
                get { return ConfigurationManager.AppSettings["ViewPath_Support"]; }
            }
            public static string SymmonsGeneric
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SymmonsGeneric"]; }
            }
            public static string SymmonsError
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SymmonsError"]; }
            }
            public static string Seo
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SEO"]; }
            }
            public static string CompareProductModals
            {
                get { return ConfigurationManager.AppSettings["ViewPath_CompareProductModals"]; }
            }
            public static string CompareProductFooterModals
            {
                get { return ConfigurationManager.AppSettings["ViewPath_CompareProductFooterModals"]; }
            }
            public static string GoogleAnalytics
            {
                get { return ConfigurationManager.AppSettings["ViewPath_GoogleAnalytics"]; }
            }
            public static string RichTextCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_RichTextCallout"]; }
            }
            public static string ImageCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ImageCallout"]; }
            }
            public static string ShareThisCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ShareThisCallout"]; }
            }
            public static string LatestVideosCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_LatestVideosCallout"]; }
            }
            public static string MapCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_MapCallout"]; }
            }
            public static string ProductLibraryCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductLibraryCallout"]; }
            }
            public static string WheretoBuyCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_WheretoBuyCallout"]; }
            }
            public static string NewsDetails
            {
                get { return ConfigurationManager.AppSettings["ViewPath_NewsDetails"]; }
            }
            public static string EventsDetails
            {
                get { return ConfigurationManager.AppSettings["ViewPath_EventsDetails"]; }
            }
            public static string GenericListing
            {
                get { return ConfigurationManager.AppSettings["ViewPath_GenericListing"]; }
            }
            public static string NewsListing
            {
                get { return ConfigurationManager.AppSettings["ViewPath_NewsListing"]; }
            }
            public static string EventsListing
            {
                get { return ConfigurationManager.AppSettings["ViewPath_EventsListing"]; }
            }
            public static string ApprovedProductsListing
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ApprovedProductsListing"]; }
            }
            public static string ProductList
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductList"]; }
            }
            public static string ProductsBrowse
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductsBrowse"]; }
            }
            public static string ProductCompare
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ProductCompare"]; }
            }
            public static string PhotoGallery
            {
                get { return ConfigurationManager.AppSettings["ViewPath_PhotoGallery"]; }
            }
            public static string Pagination
            {
                get { return ConfigurationManager.AppSettings["ViewPath_Pagination"]; }
            }
            public static string NewsAndEventsListing
            {
                get { return ConfigurationManager.AppSettings["ViewPath_NewsAndEventsListing"]; }
            }
            public static string SuperCategoryDetails
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SuperCategoryDetails"]; }
            }
            public static string CsfCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_CSF"]; }
            }
            public static string CategoryCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_CategoryCallout"]; }
            }
            public static string CollectionCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_CollectionCallout"]; }
            }
            public static string FamilyCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_FamilyCallout"]; }
            }
            public static string ApprovedProductsCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ApprovedProductsCallout"]; }
            }
            public static string CollectionLanding
            {
                get { return ConfigurationManager.AppSettings["ViewPath_CollectionLanding"]; }
            }
            public static string StyleLanding
            {
                get { return ConfigurationManager.AppSettings["ViewPath_StyleLanding"]; }
            }
            public static string FeatureLanding
            {
                get { return ConfigurationManager.AppSettings["ViewPath_FeatureLanding"]; }
            }
            public static string ApprovedProductsLanding
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ApprovedProductsLanding"]; }
            }

            //Super Category Tabs
            public static string SuperCategoryTabCategory
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SuperCategoryTabCategory"]; }
            }
            public static string SuperCategoryTabStyle
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SuperCategoryTabStyle"]; }
            }
            public static string SuperCategoryTabCollection
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SuperCategoryTabCollection"]; }
            }
            public static string SuperCategoryTabSmartFeatures
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SuperCategoryTabSmartFeatures"]; }
            }
            public static string SuperCategoryTabNewProducts
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SuperCategoryTabNewProducts"]; }
            }
            public static string SuperCategoryAdditionalInfoLinkList
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SuperCategoryAdditionalInfoLinkList"]; }
            }

            public static string ContentSearch
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ContentSearch"]; }
            }
            public static string HybridSearch
            {
                get { return ConfigurationManager.AppSettings["ViewPath_HybridSearch"]; }
            }
            public static string HeroSlider
            {
                get { return ConfigurationManager.AppSettings["ViewPath_HeroSlider"]; }
            }
            public static string StyleHeroSlider
            {
                get { return ConfigurationManager.AppSettings["ViewPath_StyleHeroSlider"]; }
            }
            public static string HomePage
            {
                get { return ConfigurationManager.AppSettings["ViewPath_HomePage"]; }
            }
            public static string IconCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_IconCallout"]; }
            }
            public static string SymmonsCommercial
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SymmonsCommercial"]; }
            }
            public static string SymmonsForHome
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SymmonsForHome"]; }
            }
            public static string PrimaryNavCommercial
            {
                get { return ConfigurationManager.AppSettings["ViewPath_PrimaryNavCommercial"]; }
            }
            public static string PrimaryDesignStudio
            {
                get { return ConfigurationManager.AppSettings["ViewPath_PrimaryDesignStudio"]; }
            }
            public static string PrimaryTemptrol
            {
                get { return ConfigurationManager.AppSettings["ViewPath_PrimaryTemptrol"]; }
            }
            public static string PrimaryNavBath
            {
                get { return ConfigurationManager.AppSettings["ViewPath_PrimaryNavBath"]; }
            }
            public static string PrimaryNavKitchen
            {
                get { return ConfigurationManager.AppSettings["ViewPath_PrimaryNavKitchen"]; }
            }
            public static string PrimaryNavMobile
            {
                get { return ConfigurationManager.AppSettings["ViewPath_PrimaryNavMobile"]; }
            }
            public static string Faq
            {
                get { return ConfigurationManager.AppSettings["ViewPath_FAQ"]; }
            }
            public static string Sitemap
            {
                get { return ConfigurationManager.AppSettings["ViewPath_Sitemap"]; }
            }
            public static string WhereToBuy
            {
                get { return ConfigurationManager.AppSettings["ViewPath_WhereToBuy"]; }
            }
            public static string ShopInStore
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ShopInStore"]; }
            }
            public static string WTBSalesRep
            {
                get { return ConfigurationManager.AppSettings["ViewPath_WTBSalesRep"]; }
            }
            public static string ShopOnline
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ShopOnline"]; }
            }
            public static string DesignStudio
            {
                get { return ConfigurationManager.AppSettings["ViewPath_DesignStudio"]; }
            }
            public static string DesignStudioCarousel
            {
                get { return ConfigurationManager.AppSettings["ViewPath_DesignStudioCarousel"]; }
            }
            public static string DesignStudioProcesses
            {
                get { return ConfigurationManager.AppSettings["ViewPath_DesignStudioProcesses"]; }
            }
            public static string SymmonsTimelineVideoCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SymmonsTimelineVideoCallout"]; }
            }
            public static string SymmonsTimelineCallout
            {
                get { return ConfigurationManager.AppSettings["ViewPath_SymmonsTimelineCallout"]; }
            }
            public static string DesignFinishes
            {
                get { return ConfigurationManager.AppSettings["ViewPath_DesignFinishes"]; }
            }
            public static string DesignStudioProduct
            {
                get { return ConfigurationManager.AppSettings["ViewPath_DesignStudioProduct"]; }
            }
            public static string ContactUsPage
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ContactUsPage"]; }
            }
            public static string TemptrolValvePage
            {
                get { return ConfigurationManager.AppSettings["ViewPath_TemptrolValvePage"]; }
            }
            public static string GTMScript
            {
                get { return ConfigurationManager.AppSettings["ViewPath_GTMScript"]; }
            }

            public static string ISSUUScript
            {
                get { return ConfigurationManager.AppSettings["ViewPath_ISSUUScript"]; }
            }
        }

        public static class PageIds
        {
            public static string NewsandEventsLanding
            {
                get { return ConfigurationManager.AppSettings["PageId_NewsandEventsLanding"]; }
            }
            public static string EventsListing
            {
                get { return ConfigurationManager.AppSettings["PageId_EventsListing"]; }
            }
            public static string NewsListing
            {
                get { return ConfigurationManager.AppSettings["PageId_NewsListing"]; }
            }
            public static string SiteSettings
            {
                get { return ConfigurationManager.AppSettings["PageId_SiteSettings"]; }
            }
            public static string GalleryListing
            {
                get { return ConfigurationManager.AppSettings["PageId_GalleryListing"]; }
            }
            public static string Home
            {
                get { return ConfigurationManager.AppSettings["PageId_Home"]; }
            }
            public static string MediaSymmons
            {
                get { return ConfigurationManager.AppSettings["PageId_MediaSymmons"]; }
            }
            public static string ProductBrowse
            {
                get { return ConfigurationManager.AppSettings["PageId_ProductBrowse"]; }
            }
            public static string ProductCompare
            {
                get { return ConfigurationManager.AppSettings["PageId_ProductCompare"]; }
            }
            public static string ProductSectionHome
            {
                get { return ConfigurationManager.AppSettings["PageId_ProductSectionHome"]; }
            }
            public static string ProductSectionCommercial
            {
                get { return ConfigurationManager.AppSettings["PageId_ProductSectionCommercial"]; }
            }
            public static string PrimaryNavCommercial
            {
                get { return ConfigurationManager.AppSettings["PageId_PrimaryNavCommercial"]; }
            }
            public static string PrimaryDesignStudio
            {
                get { return ConfigurationManager.AppSettings["PageId_PrimaryDesignStudio"]; }
            }
            public static string PrimaryTemptrol
            {
                get { return ConfigurationManager.AppSettings["PageId_PrimaryTemptrol"]; }
            }
            public static string PrimaryNavCustomSolutions
            {
                get { return ConfigurationManager.AppSettings["PageId_PrimaryNavCustomSolutions"]; }
            }
            public static string PrimaryNavSymmonsForHome
            {
                get { return ConfigurationManager.AppSettings["PageId_PrimaryNavSymmonsForHome"]; }
            }
            public static string PrimaryNavBath
            {
                get { return ConfigurationManager.AppSettings["PageId_PrimaryNavBath"]; }
            }
            public static string PrimaryNavKitchen
            {
                get { return ConfigurationManager.AppSettings["PageId_PrimaryNavKitchen"]; }
            }
            public static string GlobalFilters
            {
                get { return ConfigurationManager.AppSettings["PageId_GlobalFilters"]; }
            }
            public static string ErrorPage
            {
                get { return ConfigurationManager.AppSettings["PageId_ErrorPage"]; }
            }
            public static string HybridSearchPage
            {
                get { return ConfigurationManager.AppSettings["PageId_HybridSearchPage"]; }
            }
            public static string ContentSearchPage
            {
                get { return ConfigurationManager.AppSettings["PageId_ContentSearchPage"]; }
            }
            public static string Bath
            {
                get { return ConfigurationManager.AppSettings["PageId_Bath"]; }
            }
            public static string Kitchen
            {
                get { return ConfigurationManager.AppSettings["PageId_Kitchen"]; }
            }
            public static string WhereToBuy
            {
                get { return ConfigurationManager.AppSettings["PageId_WhereToBuy"]; }
            }
            public static string RetailExclusivePage
            {
                get { return ConfigurationManager.AppSettings["PageId_RetailExclusivePage"]; }
            }
            public static string CustomerSupport
            {
                get { return ConfigurationManager.AppSettings["PageId_CustomerSupport"]; }
            }
            public static string CustomerService
            {
                get { return ConfigurationManager.AppSettings["PageId_CustomerService"];  }
            }
            public static string ShowroomStore
            {
                get { return ConfigurationManager.AppSettings["PageId_ShowroomStore"]; }
            }
            public static string WholesaleStore
            {
                get { return ConfigurationManager.AppSettings["PageId_WholesaleStore"]; }
            }
            public static string PartsDistributorsStore
            {
                get { return ConfigurationManager.AppSettings["PageId_PartsDistributorsStore"]; }
            }
            public static string RetailStore
            {
                get { return ConfigurationManager.AppSettings["PageId_HomeCenterStore"]; }
            }
            public static string WtbRetailExclusive
            {
                get { return ConfigurationManager.AppSettings["PageId_WtbRetailExclusive"]; }
            }
            public static string DesignStudio
            {
                get { return ConfigurationManager.AppSettings["PageId_DesignStudio"]; }
            }
            public static string AnyFinish
            {
                get { return ConfigurationManager.AppSettings["PageId_AnyFinishData"]; }
            }
            public static string ChromeFinish
            {
                get { return ConfigurationManager.AppSettings["PageId_ChromeFinish"]; }
            }
            public static string DesignStudioProductBrowse
            {
                get { return ConfigurationManager.AppSettings["PageId_DesignStudioProductBrowse"]; }
            }
            public static string ContactUsPage
            {
                get { return ConfigurationManager.AppSettings["PageId_ContactUsPage"]; }
            }
            public static string ContactUsSuccessPage
            {
                get { return ConfigurationManager.AppSettings["PageId_ContactUsSuccessPage"]; }
            }
            public static string DesignStudioContactUsSuccessPage
            {
                get { return ConfigurationManager.AppSettings["PageId_DesignStudioContactUsSuccessPage"]; }
            }
            public static string CanadaStoresRepository
            {
                get { return ConfigurationManager.AppSettings["PageId_CanadaStoresLocation"]; }
            }
            public static string LegacyBathroomProductLocation
            {
                get { return ConfigurationManager.AppSettings["PageId_LegacyBathroomProductLocation"]; }
            }
        }

        public static class CalloutIds
        {
            public static string ProductLibrary
            {
                get { return ConfigurationManager.AppSettings["CalloutId_ProductLibrary"]; }
            }
            public static string WhereToBuy
            {
                get { return ConfigurationManager.AppSettings["CalloutId_WhereToBuy"]; }
            }
        }

        public static class ItemIds
        {
            public static string ProductFamilyBath
            {
                get { return ConfigurationManager.AppSettings["ItemId_ProductFamilyBath"]; }
            }
            public static string ProductFamilyKitchen
            {
                get { return ConfigurationManager.AppSettings["ItemId_ProductFamilyKitchen"]; }
            }
            public static string WhereToBuyStoreType
            {
                get { return ConfigurationManager.AppSettings["ItemId_WhereToBuyStoreType"]; }
            }
            public static string BlankCanvas
            {
                get { return ConfigurationManager.AppSettings["ItemId_BlankCanvas"]; }
            }
            public static string FreeForm
            {
                get { return ConfigurationManager.AppSettings["ItemId_FreeForm"]; }
            }
            public static string ResidentialSegment
            {
                get { return ConfigurationManager.AppSettings["ItemId_ResidentialSegment"]; }
            }
            public static string RedirectType301
            {
                get { return ConfigurationManager.AppSettings["ItemId_RedirectType301"]; }
            }
        }

        // **********************************************************************************************************************

        public static class ConstantValues
        {
            public static int GlobalPageSize
            {
                get { return System.Convert.ToInt32(ConfigurationManager.AppSettings["Constant_GlobalPageSize"]); }
            }
            public static string GalleryHorizontal
            {
                get { return ConfigurationManager.AppSettings["GalleryClass_Horizontal"]; }
            }
            public static string GalleryVertical
            {
                get { return ConfigurationManager.AppSettings["GalleryClass_Vertical"]; }
            }
            public static string SessionGalleryData
            {
                get { return ConfigurationManager.AppSettings["Session_GalleryData"]; }
            }
            public static string SessionGenericListingData
            {
                get { return ConfigurationManager.AppSettings["Session_GenericListingData"]; }
            }
            public static string SessionApprovedProductsListingData
            {
                get { return ConfigurationManager.AppSettings["Session_ApprovedProductsListingData"]; }
            }
            public static string SessionMaxVideos
            {
                get { return ConfigurationManager.AppSettings["Session_MaxVideos"]; }
            }
            public static string SessionProductDetailsModel
            {
                get { return ConfigurationManager.AppSettings["Session_ProductDetailsModel"]; }
            }
            public static string SessionProductModel
            {
                get { return ConfigurationManager.AppSettings["Session_ProductModel"]; }
            }
            public static string ConstantVideo
            {
                get { return ConfigurationManager.AppSettings["Constant_Video"]; }
            }
            public static string ConstantImage
            {
                get { return ConfigurationManager.AppSettings["Constant_Image"]; }
            }
            public static string ConstantVideoCount
            {
                get { return ConfigurationManager.AppSettings["Constant_VideoCount"]; }
            }
            public static string ConstantProductsUrl
            {
                get { return ConfigurationManager.AppSettings["Constant_ProductsUrl"]; }
            }
            public static string ConstantSortByNew
            {
                get { return ConfigurationManager.AppSettings["Constant_SortByNew"]; }
            }
            public static string ConstantSortByHigh
            {
                get { return ConfigurationManager.AppSettings["Constant_SortByHigh"]; }
            }
            public static string ConstantSortByLow
            {
                get { return ConfigurationManager.AppSettings["Constant_SortByLow"]; }
            }
            public static string Abbreviation
            {
                get { return ConfigurationManager.AppSettings["Constant_Abbreviation"]; }
            }
            public static string StateName
            {
                get { return ConfigurationManager.AppSettings["Constant_StateName"]; }
            }
            public static string Commercial
            {
                get { return ConfigurationManager.AppSettings["Constant_Commercial"]; }
            }
            public static string Home
            {
                get { return ConfigurationManager.AppSettings["Constant_Home"]; }
            }
            public static string HttpProtocol
            {
                get { return ConfigurationManager.AppSettings["Constant_HttpProtocol"]; }
            }
            public static string PageTitleLimitHover
            {
                get { return ConfigurationManager.AppSettings["Constant_PageTitleLimitHover"]; }
            }
            public static string PageTeaserLimitHover
            {
                get { return ConfigurationManager.AppSettings["Constant_PageTeaserLimitHover"]; }
            }
            public static string ProductDocumentExtension
            {
                get { return ConfigurationManager.AppSettings["Constant_ProductDocumentExtension"]; }
            }
            public static string StoreRadius
            {
                get { return ConfigurationManager.AppSettings["Constant_StoreRadius"]; }
            }
            public static string MaximumLength20
            {
                get { return ConfigurationManager.AppSettings["Constant_MaximumLength20"]; }
            }
            public static string MaximumLength140
            {
                get { return ConfigurationManager.AppSettings["Constant_MaximumLength140"]; }
            }
            public static string FreeGeoIp
            {
                get { return ConfigurationManager.AppSettings["Constant_FreeGeoIP"]; }
            }
            public static string Latitude
            {
                get { return ConfigurationManager.AppSettings["Constant_Latitude"]; }
            }
            public static string Longitude
            {
                get { return ConfigurationManager.AppSettings["Constant_Longitude"]; }
            }
            public static string CacheStoresTime
            {
                get { return ConfigurationManager.AppSettings["Constant_CacheStoresTime"]; }
            }
            public static string CaSitePath
            {
                get { return ConfigurationManager.AppSettings["Constant_CaSitePath"]; }
            }
            public static string CaSiteKey
            {
                get { return ConfigurationManager.AppSettings["Constant_CaSiteKey"]; }
            }
            public static string DefaultSiteKey
            {
                get { return ConfigurationManager.AppSettings["Constant_DefaultSiteKey"]; }
            }
            public static string TestCanadaIpAddress
            {
                get { return ConfigurationManager.AppSettings["Constant_TestCanadaIpAddress"]; }
            }
            public static string LegacyPremiumProduct
            {
                get { return ConfigurationManager.AppSettings["Constant_PremiumProduct"]; }
            }
            public static string RetailStoreFolderName
            {
                get { return ConfigurationManager.AppSettings["Constant_RetailStoreFolderName"]; }
            }
            public static string WholesaleStoreFolderName
            {
                get { return ConfigurationManager.AppSettings["Constant_WholesaleStoreFolderName"]; }
            }
            public static string ShowroomFolderName
            {
                get { return ConfigurationManager.AppSettings["Constant_ShowroomFolderName"]; }
            }
            public static string PartDistributorsFolderName
            {
                get { return ConfigurationManager.AppSettings["Constant_PartDistributorsFolderName"]; }
            }
            public static string RepresentativesFolderName
            {
                get { return ConfigurationManager.AppSettings["Constant_RepresentativesFolderName"]; }
            }
            public static string ProductOnAppPrice
            {
                get { return ConfigurationManager.AppSettings["Constant_ProductOnAppPrice"]; }
            }
            public static string CountryCodeUSA
            {
                get { return ConfigurationManager.AppSettings["Constant_CountryCodeUSA"]; }
            }
            public static string CountryCodeCanada
            {
                get { return ConfigurationManager.AppSettings["Constant_CountryCodeCanada"]; }
            }
            public static string DesignStudioContactId
            {
                get { return ConfigurationManager.AppSettings["Constant_DesignStudioContactId"]; }
            }
            public static string WTBExcludedStates
            {
                get { return ConfigurationManager.AppSettings["Constant_WTBExcludedStates"]; }
            }
            public static string SuperCategory
            {
                get { return ConfigurationManager.AppSettings["Constant_SuperCategory"]; }
            }
            public static string Kitchen
            {
                get { return ConfigurationManager.AppSettings["Constant_Kitchen"]; }
            }
            public static string Bath
            {
                get { return ConfigurationManager.AppSettings["Constant_Bath"]; }
            }
            public static string DatabaseMaster
            {
                get
                {
                    return ConfigurationManager.AppSettings["DATABASE_Master"];
                }
            }
        }

        public static class TemplateIds
        {
            public static string ProductDetailsTemplateId
            {
                get { return ConfigurationManager.AppSettings["TemplateId_ProductDetails"]; }
            }
            public static string ProductFinishTemplateId
            {
                get { return ConfigurationManager.AppSettings["TemplateId_ProductFinish"]; }
            }
            public static string Events
            {
                get { return ConfigurationManager.AppSettings["TemplateId_Events"]; }
            }
            public static string News
            {
                get { return ConfigurationManager.AppSettings["TemplateId_News"]; }
            }
            public static string GenericPage
            {
                get { return ConfigurationManager.AppSettings["TemplateId_GenericPage"]; }
            }
            public static string SuperCategoryLandingPage
            {
                get { return ConfigurationManager.AppSettings["TemplateId_SuperCategoryLandingPage"]; }
            }
            public static string CollectionLandingPage
            {
                get { return ConfigurationManager.AppSettings["TemplateId_CollectionLandingPage"]; }
            }
            public static string StyleLandingPage
            {
                get { return ConfigurationManager.AppSettings["TemplateId_StyleLandingPage"]; }
            }
            public static string FeatureLandingPage
            {
                get { return ConfigurationManager.AppSettings["TemplateId_FeatureLandingPage"]; }
            }
            public static string TemptrolPage
            {
                get { return ConfigurationManager.AppSettings["TemplateId_TemptrolPage"]; }
            }
            public static string SalesRepresntative
            {
                get { return ConfigurationManager.AppSettings["TemplateId_SalesRepresntative"]; }
            }
            public static string State
            {
                get { return ConfigurationManager.AppSettings["TemplateId_State"]; }
            }
            public static string RetailExclusive
            {
                get { return ConfigurationManager.AppSettings["TemplateId_RetailExclusive"]; }
            }

            public static string Image
            {
                get { return ConfigurationManager.AppSettings["TemplateId_Image"]; }
            }
            public static string Pdf
            {
                get { return ConfigurationManager.AppSettings["TemplateId_Pdf"]; }
            }
            public static string File
            {
                get { return ConfigurationManager.AppSettings["TemplateId_File"]; }
            }
            public static string Doc
            {
                get { return ConfigurationManager.AppSettings["TemplateId_Doc"]; }
            }
            public static string Docx
            {
                get { return ConfigurationManager.AppSettings["TemplateId_Docx"]; }
            }
            public static string MediaItems
            {
                get { return ConfigurationManager.AppSettings["TemplateId_MediaItems"]; }
            }
            public static string ShowroomStores
            {
                get { return ConfigurationManager.AppSettings["TemplateId_ShowroomStores"]; }
            }
            public static string WholesaleStores
            {
                get { return ConfigurationManager.AppSettings["TemplateId_WholesaleStores"]; }
            }
            public static string RetailStores
            {
                get { return ConfigurationManager.AppSettings["TemplateId_RetailStores"]; }
            }
            public static string PartsStores
            {
                get { return ConfigurationManager.AppSettings["TemplateId_PartsStores"]; }
            }
            public static string DesignStudioProductBrowse
            {
                get { return ConfigurationManager.AppSettings["TemplateId_DesignStudioProductBrowse"]; }
            }
            public static string GenericListing
            {
                get { return ConfigurationManager.AppSettings["TemplateId_GenericListing"]; }
            }
            public static string LegacyBathroomProducts
            {
                get { return ConfigurationManager.AppSettings["TemplateId_LegacyBathroomProducts"]; }
            }
            public static string StoreFolder
            {
                get { return ConfigurationManager.AppSettings["TemplateId_StoreFolder"]; }
            }
            public static string CommonFolder
            {
                get { return ConfigurationManager.AppSettings["TemplateId_CommonFolder"]; }
            }
        }

        public static class ViewDatas
        {
            public static string NewsAndEventsLandingModel
            {
                get { return ConfigurationManager.AppSettings["ViewData_NewsAndEventsLandingModel"]; }
            }
            public static string EventsListingModel
            {
                get { return ConfigurationManager.AppSettings["ViewData_EventsListingModel"]; }
            }
            public static string NewsListingModel
            {
                get { return ConfigurationManager.AppSettings["ViewData_EventsListingModel"]; }
            }
        }

        public static class SearchIndex
        {
            public static string Products
            {
                get { return ConfigurationManager.AppSettings["SearchIndex_Products"]; }
            }
            public static string Content
            {
                get { return ConfigurationManager.AppSettings["SearchIndex_Content"]; }
            }
            public static string Media
            {
                get { return ConfigurationManager.AppSettings["SearchIndex_Media"]; }
            }
            public static string Stores
            {
                get { return ConfigurationManager.AppSettings["SearchIndex_Stores"]; }
            }
            public static string LegacyProducts
            {
                get { return ConfigurationManager.AppSettings["SearchIndex_LegacyProducts"]; }
            }
        }

        public static class Dictionary
        {
            public static string ReadMore
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ReadMore"]; }
            }
            public static string Posted
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Posted"]; }
            }
            public static string MoreAboutThisFeature
            {
                get { return ConfigurationManager.AppSettings["Dictionary_MoreAboutThisFeature"]; }
            }
            public static string MoreAboutThisStyle
            {
                get { return ConfigurationManager.AppSettings["Dictionary_MoreAboutThisStyle"]; }
            }
            public static string MoreAboutThisCollection
            {
                get { return ConfigurationManager.AppSettings["Dictionary_MoreAboutThisCollection"]; }
            }
            public static string AllNews
            {
                get { return ConfigurationManager.AppSettings["Dictionary_AllNews"]; }
            }
            public static string AllEvents
            {
                get { return ConfigurationManager.AppSettings["Dictionary_AllEvents"]; }
            }
            public static string UpcomingEventsTitle
            {
                get { return ConfigurationManager.AppSettings["Dictionary_UpcomingEventsTitle"]; }
            }
            public static string RecentNewsTitle
            {
                get { return ConfigurationManager.AppSettings["Dictionary_RecentNewsTitle"]; }
            }
            public static string ProductModel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductModel"]; }
            }
            public static string ProductSelectFinish
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductSelectFinish"]; }
            }
            public static string ProductAvailableValves
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductAvailableValves"]; }
            }
            public static string ProductSelectSKU
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductSelectSKU"]; }
            }
            public static string ProductSKU
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductSKU"]; }
            }
            public static string ProductFlowRate
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductFlowRate"]; }
            }
            public static string ProductOther
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductOther"]; }
            }
            public static string ProductTotalPrice
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductTotalPrice"]; }
            }
            public static string ProductAddToComparison
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductAddToComparison"]; }
            }
            public static string ProductAboutThisCollection
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductAboutThisCollection"]; }
            }
            public static string ProductShowPlumbingModificationOptions
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductShowPlumbingModificationOptions"]; }
            }
            public static string ProductHidePlumbingModificationOptions
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductHidePlumbingModificationOptions"]; }
            }
            public static string ProductShowMoreFeatures
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductShowMoreFeatures"]; }
            }
            public static string ProductSpecialFeaturesNote
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductSpecialFeaturesNote"]; }
            }
            public static string NoRecordsFound
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoRecordsFound"]; }
            }
            public static string NoSalesRepFound
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoSalesRepFound"]; }
            }
            public static string NoStoresResultsFound
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoStoresResultsFound"]; }
            }
            public static string ShowDiagram
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ShowDiagram"]; }
            }
            public static string ProductCurrency
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductCurrency"]; }
            }
            public static string ProductShowMoreDownloads
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductShowMoreDownloads"]; }
            }
            public static string ProductShowMoreLinks
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductShowMoreLinks"]; }
            }
            public static string ShareThis
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ShareThis"]; }
            }
            public static string Home
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Home"]; }
            }
            public static string Commercial
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Commercial"]; }
            }
            public static string SymmonsHome
            {
                get { return ConfigurationManager.AppSettings["Dictionary_SymmonsHome"]; }
            }
            public static string SymmonsCommercial
            {
                get { return ConfigurationManager.AppSettings["Dictionary_SymmonsCommercial"]; }
            }
            public static string Browse
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Browse"]; }
            }
            public static string BrowseProductsBy
            {
                get { return ConfigurationManager.AppSettings["Dictionary_BrowseProductsBy"]; }
            }
            public static string Kitchen
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Kitchen"]; }
            }
            public static string Bath
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Bath"]; }
            }
            public static string KitchenProducts
            {
                get { return ConfigurationManager.AppSettings["Dictionary_KitchenProducts"]; }
            }
            public static string CommercialProducts
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CommercialProducts"]; }
            }
            public static string BathProducts
            {
                get { return ConfigurationManager.AppSettings["Dictionary_BathProducts"]; }
            }
            public static string Products
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Products"]; }
            }
            public static string ProductsRefine
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsRefineMobile"]; }
            }
            public static string ProductsSortBy
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsSortBy"]; }
            }
            public static string ProductsLowToHigh
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsLowToHigh"]; }
            }
            public static string ProductsHighToLow
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsHighToLow"]; }
            }
            public static string ProductsNew
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsNew"]; }
            }
            public static string ProductsFilters
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsFilters"]; }
            }
            public static string ProductsRefineDesktop
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsRefine"]; }
            }
            public static string ProductsSearchFilters
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsSearchFilters"]; }
            }
            public static string ProductsFilterApply
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsFilterApply"]; }
            }
            public static string ProductsFilterCancel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsFilterCancel"]; }
            }
            public static string NoProductsFoundMessage
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoProductsFoundMessage"]; }
            }
            public static string ProductsListPrice
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductsListPrice"]; }
            }
            public static string CompareProductsListPrice
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CompareProductsListPrice"]; }
            }
            public static string SearchProducts
            {
                get { return ConfigurationManager.AppSettings["Dictionary_SearchProducts"]; }
            }
            public static string InThisFeature
            {
                get { return ConfigurationManager.AppSettings["Dictionary_InThisFeature"]; }
            }
            public static string InThisStyle
            {
                get { return ConfigurationManager.AppSettings["Dictionary_InThisStyle"]; }
            }
            public static string InThisCollection
            {
                get { return ConfigurationManager.AppSettings["Dictionary_InThisCollection"]; }
            }
            public static string PriceRangeLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_PriceRangeLabel"]; }
            }
            public static string FinishesLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_FinishesLabel"]; }
            }
            public static string MoreLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_More"]; }
            }
            public static string ListPrice
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ListPrice"]; }
            }
            public static string StarterIdea
            {
                get { return ConfigurationManager.AppSettings["Dictionary_StarterIdea"]; }
            }
            public static string WhereToBuy
            {
                get { return ConfigurationManager.AppSettings["Dictionary_WhereToBuy"]; }
            }

            public static string BrowseProducts
            {
                get { return ConfigurationManager.AppSettings["Dictionary_BrowseProducts"]; }
            }
            public static string Category
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Category"]; }
            }
            public static string Style
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Style"]; }
            }
            public static string Collection
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Collection"]; }
            }
            public static string SmartFeature
            {
                get { return ConfigurationManager.AppSettings["Dictionary_SmartFeature"]; }
            }
            public static string NewProducts
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NewProducts"]; }
            }
            public static string Back
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Back"]; }
            }
            public static string SwipeforPreview
            {
                get { return ConfigurationManager.AppSettings["Dictionary_SwipeforPreview"]; }
            }
            public static string Search
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Search"]; }
            }
            public static string LearnMore
            {
                get { return ConfigurationManager.AppSettings["Dictionary_LearnMore"]; }
            }
            public static string MaxLength50
            {
                get { return ConfigurationManager.AppSettings["Dictionary_MaxLength50"]; }
            }
            public static string MaxLength170
            {
                get { return ConfigurationManager.AppSettings["Dictionary_MaxLength170"]; }
            }
            public static string MaxLength15
            {
                get { return ConfigurationManager.AppSettings["Dictionary_MaxLength15"]; }
            }
            public static string ApprovedProducts
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ApprovedProducts"]; }
            }
            public static string NoProductstoDisplay
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoProductstoDisplay"]; }
            }
            public static string CommercialCustomers
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CommercialCustomers"]; }
            }
            public static string CustomerSupport
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CustomerSupport"]; }
            }
            public static string AvailableAtSelectRetailers
            {
                get { return ConfigurationManager.AppSettings["Dictionary_AvailableAtSelectRetailers"]; }
            }

            public static string NoCategoriesFoundMessage
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoCategoriesFoundMessage"]; }
            }
            public static string NoCollectionsFoundMessage
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoCollectionsFoundMessage"]; }
            }
            public static string NoStylesFoundMessage
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoStylesFoundMessage"]; }
            }
            public static string NoSmartFeaturesFoundMessage
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoSmartFeaturesFoundMessage"]; }
            }
            public static string NoNewProductsFoundMessage
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoNewProductsFoundMessage"]; }
            }
            public static string Go
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Go"]; }
            }

            //Global Search
            public static string ContentResults
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ContentResults"]; }
            }
            public static string ProductResults
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductResults"]; }
            }
            public static string LoadContentResults
            {
                get { return ConfigurationManager.AppSettings["Dictionary_LoadContentResults"]; }
            }
            public static string LoadProductResults
            {
                get { return ConfigurationManager.AppSettings["Dictionary_LoadProductResults"]; }
            }
            public static string Menu
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Menu"]; }
            }
            public static string Support
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Support"]; }
            }
            public static string FaqsGenericMessage
            {
                get { return ConfigurationManager.AppSettings["Dictionary_FAQsGenericMessage"]; }
            }
            public static string ShopInStore
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ShopInStore"]; }
            }
            public static string ShopOnline
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ShopOnline"]; }
            }
            public static string FindASalesRep
            {
                get { return ConfigurationManager.AppSettings["Dictionary_FindASalesRep"]; }
            }
            public static string FindLocations
            {
                get { return ConfigurationManager.AppSettings["Dictionary_FindLocations"]; }
            }
            public static string FindLocationsNear
            {
                get { return ConfigurationManager.AppSettings["Dictionary_FindLocationsNear"]; }
            }
            public static string FindLocationsNearStep2
            {
                get { return ConfigurationManager.AppSettings["Dictionary_FindLocationsNearStep2"]; }
            }
            public static string FindLocationsNearStep3
            {
                get { return ConfigurationManager.AppSettings["Dictionary_FindLocationsNearStep3"]; }
            }
            public static string UsePreciseLocation
            {
                get { return ConfigurationManager.AppSettings["Dictionary_UsePreciseLocation"]; }
            }
            public static string CityTownZipcode
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CityTownZipcode"]; }
            }
            public static string IncludeInYourSearch
            {
                get { return ConfigurationManager.AppSettings["Dictionary_IncludeInYourSearch"]; }
            }
            public static string SalesRepsIn
            {
                get { return ConfigurationManager.AppSettings["Dictionary_SalesRepsIn"]; }
            }
            public static string CompareProducts
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CompareProducts"]; }
            }
            public static string Compare
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Compare"]; }
            }
            public static string ChooseAnotherProduct
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ChooseAnotherProduct"]; }
            }
            public static string ChooseAnotherProductDesc
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ChooseAnotherProductDesc"]; }
            }
            public static string StartComparing
            {
                get { return ConfigurationManager.AppSettings["Dictionary_StartComparing"]; }
            }
            public static string CompareProductsOfSameType
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CompareProductsOfSameType"]; }
            }
            public static string CompareProductsOfSameTypeDesc
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CompareProductsOfSameTypeDesc"]; }
            }
            public static string CompareUpToThreeProducts
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CompareUpToThreeProducts"]; }
            }
            public static string CompareUpToThreeProductsDesc
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CompareUpToThreeProductsDesc"]; }
            }
            public static string CompareProductAlreadyExists
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CompareProductAlreadyExists"]; }
            }
            public static string CompareProductAlreadyExistsDesc
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CompareProductAlreadyExistsDesc"]; }
            }
            public static string CompareProductGotIt
            {
                get { return ConfigurationManager.AppSettings["Dictionary_CompareProductGotIt"]; }
            }
            public static string NewProductCompare
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NewProductCompare"]; }
            }
            public static string DesignStudioCatalogFinishes
            {
                get { return ConfigurationManager.AppSettings["Dictionary_DesignStudioCatalogFinishes"]; }
            }
            public static string DesignStudioCatalogFinish
            {
                get { return ConfigurationManager.AppSettings["Dictionary_DesignStudioCatalogFinish"]; }
            }
            public static string DesignStudioLeadTime
            {
                get { return ConfigurationManager.AppSettings["Dictionary_DesignStudioLeadTime"]; }
            }
            public static string DesignStudioUniquesnessofDesign
            {
                get { return ConfigurationManager.AppSettings["Dictionary_DesignStudioUniquesnessofDesign"]; }
            }
            public static string DesignStudioMinimumOrderQuantity
            {
                get { return ConfigurationManager.AppSettings["Dictionary_DesignStudioMinimumOrderQuantity"]; }
            }
            public static string DesignStudioFinishes
            {
                get { return ConfigurationManager.AppSettings["Dictionary_DesignStudioFinishes"]; }
            }
            public static string DirectionsTitle
            {
                get { return ConfigurationManager.AppSettings["Dictionary_DirectionsTitle"]; }
            }
            public static string MoreLocation
            {
                get { return ConfigurationManager.AppSettings["Dictionary_MoreLocation"]; }
            }
            public static string MoreLike
            {
                get { return ConfigurationManager.AppSettings["Dictionary_MoreLike"]; }
            }
            public static string DirectionsLink
            {
                get { return ConfigurationManager.AppSettings["Dictionary_DirectionsLink"]; }
            }
            public static string LocationsNear
            {
                get { return ConfigurationManager.AppSettings["Dictionary_LocationsNear"]; }
            }
            public static string NoOnlineStoresFound
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoOnlineStoresFound"]; }
            }
            public static string ModifyYourSearch
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ModifyYourSearch"]; }
            }
            public static string NoLocationsEntered
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoLocationEntered"]; }
            }
            public static string NoStoresSelected
            {
                get { return ConfigurationManager.AppSettings["Dictionary_NoStoresSelected"]; }
            }
            public static string ShowMore
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ShowMore"]; }
            }
            public static string ShowLess
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ShowLess"]; }
            }
            public static string AvailableFinishes
            {
                get { return ConfigurationManager.AppSettings["Dictionary_AvailableFinishes"]; }
            }
            public static string AnyFinish
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Anyfinish"]; }
            }
            public static string PolishedChromeFinish
            {
                get { return ConfigurationManager.AppSettings["Dictionary_Polishedchromefinish"]; }
            }
            public static string WhereToBuyNoResults
            {
                get { return ConfigurationManager.AppSettings["Dictionary_WhereToBuyNoResults"]; }
            }
            public static string ContactSymmons
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ContactSymmons"]; }
            }
            public static string ReadyToStart
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ReadyToStart"]; }
            }
            public static string FirstNameLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_FirstNameLabel"]; }
            }
            public static string LastNameLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_LastNameLabel"]; }
            }
            public static string EmailLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_EmailLabel"]; }
            }
            public static string PhoneNoLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_PhoneNoLabel"]; }
            }
            public static string ContactUsMessageLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ContactUsMessageLabel"]; }
            }
            public static string ContactUsHelpTextLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ContactUsHelpTextLabel"]; }
            }
            public static string AttachPhotoLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_AttachPhotoLabel"]; }
            }
            public static string BrowseLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_BrowseLabel"]; }
            }
            public static string AddAnotherFileLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_AddAnotherFileLabel"]; }
            }
            public static string SendMessage
            {
                get { return ConfigurationManager.AppSettings["Dictionary_SendMessage"]; }
            }
            public static string ProductSkuLabel
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ProductSkuLabel"]; }
            }
            public static string SymmonsMailAddress
            {
                get { return ConfigurationManager.AppSettings["Dictionary_SymmonsMailAddress"]; }
            }
            public static string ContactUsError
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ContactUsError"]; }
            }
            public static string SymmonsPreferred
            {
                get { return ConfigurationManager.AppSettings["Dictionary_SymmonsPreferred"]; }
            }
            public static string SymmonsPreferredDescription
            {
                get { return ConfigurationManager.AppSettings["Dictionary_SymmonsPreferredDescription"]; }
            }
            public static string ContactUsDesignStudioText
            {
                get { return ConfigurationManager.AppSettings["Dictionary_ContactUsDesignStudioText"]; }
            }
        }

        public static class FolderIds
        {
            public static string ProductBrowse
            {
                get { return ConfigurationManager.AppSettings["Folder_ProductsBrowse"]; }
            }
            public static string ProductsRepository
            {
                get { return ConfigurationManager.AppSettings["Folder_ProductsRepository"]; }
            }
            public static string ProductAttributesRepository
            {
                get { return ConfigurationManager.AppSettings["Folder_ProductAttributesRepository"]; }
            }
            public static string ProductSmartFeaturesRepository
            {
                get { return ConfigurationManager.AppSettings["Folder_ProductSmartFeaturesRepository"]; }
            }
            public static string StoresRepository
            {
                get { return ConfigurationManager.AppSettings["Folder_StoresRepository"]; }
            }
        }

        public static class FieldNames
        {
            public static string PageTitle
            {
                get { return ConfigurationManager.AppSettings["FieldName_PageTitle"]; }
            }
            public static string Teaser
            {
                get { return ConfigurationManager.AppSettings["FieldName_Teaser"]; }
            }
            public static string MainContent
            {
                get { return ConfigurationManager.AppSettings["FieldName_MainContent"]; }
            }
            public static string ProductName
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductName"]; }
            }
            public static string IsNew
            {
                get { return ConfigurationManager.AppSettings["FieldName_IsNew"]; }
            }
            public static string ListPrice
            {
                get { return ConfigurationManager.AppSettings["FieldName_ListPrice"]; }
            }
            public static string PageImage
            {
                get { return ConfigurationManager.AppSettings["FieldName_PageImage"]; }
            }
            public static string PriceMin
            {
                get { return ConfigurationManager.AppSettings["FieldName_PriceMin"]; }
            }
            public static string PriceMax
            {
                get { return ConfigurationManager.AppSettings["FieldName_PriceMax"]; }
            }
            public static string CanPrice
            {
                get { return ConfigurationManager.AppSettings["FieldName_CanPrice"]; }
            }
            public static string ProductStyles
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductStyles"]; }
            }
            public static string ProductFinishes
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductFinishes"]; }
            }
            public static string ProductSmartFeatures
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductSmartFeatures"]; }
            }
            public static string ProductCollections
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductCollections"]; }
            }
            public static string ProductSection
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductSection"]; }
            }
            public static string ProductSegment
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductSegment"]; }
            }
            public static string ProductAttributes
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductAttributes"]; }
            }
            public static string CompareFilter
            {
                get { return ConfigurationManager.AppSettings["FieldName_CompareFilter"]; }
            }
            public static string ProductModelNumber
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductModelNumber"]; }
            }
            public static string ReferenceFilterItems
            {
                get { return ConfigurationManager.AppSettings["FieldName_ReferenceFilterItems"]; }
            }
            public static string ProductStyle
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductStyle"]; }
            }
            public static string ProductCollection
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductCollection"]; }
            }
            public static string ProductFamily
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductFamily"]; }
            }
            public static string PriceRange
            {
                get { return ConfigurationManager.AppSettings["FieldName_PriceRange"]; }
            }
            public static string ProductCategory
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductCategory"]; }
            }
            public static string ProductFilterFinish
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductFilterFinish"]; }
            }
            public static string EventStartDate
            {
                get { return ConfigurationManager.AppSettings["FieldName_EventStartDate"]; }
            }
            public static string NavigationTitle
            {
                get { return ConfigurationManager.AppSettings["FieldName_NavigationTitle"]; }
            }
            public static string ProductLength
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductLength"]; }
            }
            public static string ProductWidth
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductWidth"]; }
            }
            public static string ProductHeight
            {
                get { return ConfigurationManager.AppSettings["FieldName_ProductHeight"]; }
            }
            public static string Name
            {
                get { return ConfigurationManager.AppSettings["FieldName_Name"]; }
            }
            public static string Icon
            {
                get { return ConfigurationManager.AppSettings["FieldName_Icon"]; }
            }
            public static string SmallIcon
            {
                get { return ConfigurationManager.AppSettings["FieldName_SmallIcon"]; }
            }
            public static string ShowonNavigation
            {
                get { return ConfigurationManager.AppSettings["FieldName_ShowonNavigation"]; }
            }
            public static string StoreLatitude
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreLatitude"]; }
            }
            public static string StoreLongitude
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreLongitude"]; }
            }
            public static string StoreName
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreName"]; }
            }
            public static string StateAbbreviation
            {
                get { return ConfigurationManager.AppSettings["FieldName_StateAbbreviation"]; }
            }
            public static string StateName
            {
                get { return ConfigurationManager.AppSettings["FieldName_StateName"]; }
            }
            public static string IsSymmonsPreferred
            {
                get { return ConfigurationManager.AppSettings["FieldName_IsSymmonsPreferred"]; }
            }
            public static string StorePhone
            {
                get { return ConfigurationManager.AppSettings["FieldName_StorePhone"]; }
            }
            public static string StoreAddressLine1
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreAddressLine1"]; }
            }
            public static string StoreAddressLine2
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreAddressLine2"]; }
            }
            public static string StoreCity
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreCity"]; }
            }
            public static string StoreState
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreState"]; }
            }
            public static string StoreZip
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreZip"]; }
            }
            public static string StoreFax
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreFax"]; }
            }
            public static string StoreEmail
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreEmail"]; }
            }
            public static string StoreManager
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreManager"]; }
            }
            public static string StoreUrl
            {
                get { return ConfigurationManager.AppSettings["FieldName_StoreUrl"]; }
            }
            public static string Title
            {
                get { return ConfigurationManager.AppSettings["FieldName_Title"]; }
            }
            public static string Description
            {
                get { return ConfigurationManager.AppSettings["FieldName_Description"]; }
            }
            public static string LegacyProductStyle
            {
                get { return ConfigurationManager.AppSettings["FieldName_LegacyProductStyle"]; }
            }
            public static string LegacyProductUsPrice
            {
                get { return ConfigurationManager.AppSettings["FieldName_LegacyUSPrice"]; }
            }
            public static string ExcludeFromSearch
            {
                get { return ConfigurationManager.AppSettings["FieldName_ExcludeFromSearch"]; }
            }
        }

        public static class SessionConstants
        {
            public static string SearchFilterId
            {
                get { return ConfigurationManager.AppSettings["Session_SearchFilterId"]; }
            }
            public static string FeatureId
            {
                get { return ConfigurationManager.AppSettings["Session_FeatureId"]; }
            }
            public static string CollectionId
            {
                get { return ConfigurationManager.AppSettings["Session_CollectionId"]; }
            }
            public static string CategoryId
            {
                get { return ConfigurationManager.AppSettings["Session_CategoryId"]; }
            }
            public static string ProductFamily
            {
                get { return ConfigurationManager.AppSettings["Session_ProductFamily"]; }
            }
            public static string ContentSearch
            {
                get { return ConfigurationManager.AppSettings["Session_ContentSearch"]; }
            }
            public static string ProductSegment
            {
                get { return ConfigurationManager.AppSettings["Session_ProductSegment"]; }
            }
            public static string StyleId
            {
                get { return ConfigurationManager.AppSettings["Session_StyleId"]; }
            }
            public static string Keyword
            {
                get { return ConfigurationManager.AppSettings["Session_Keyword"]; }
            }
            public static string SpecialAttributeId
            {
                get { return ConfigurationManager.AppSettings["Session_SpecialAttributeId"]; }
            }
            public static string FromSearchBox
            {
                get { return ConfigurationManager.AppSettings["Session_FromSearchBox"]; }
            }
            public static string ProductResultCount
            {
                get { return ConfigurationManager.AppSettings["Session_ProductResultCount"]; }
            }
            public static string ContentSearchCount
            {
                get { return ConfigurationManager.AppSettings["Session_ContentSearchCount"]; }
            }
            public static string IsFromCallout
            {
                get { return ConfigurationManager.AppSettings["Session_IsFromCallout"]; }
            }
            public static string SalesRepresentatives
            {
                get { return ConfigurationManager.AppSettings["Session_SalesRepresentatives"]; }
            }

        }

        public static class QueryString
        {
            public static string SearchFilterId
            {
                get { return ConfigurationManager.AppSettings["QueryString_SearchFilterId"]; }
            }

            public static string CollectionId
            {
                get { return ConfigurationManager.AppSettings["QueryString_CollectionId"]; }
            }
            public static string CategoryId
            {
                get { return ConfigurationManager.AppSettings["QueryString_CategoryId"]; }
            }
            public static string FamilyId
            {
                get { return ConfigurationManager.AppSettings["QueryString_FamilyId"]; }
            }
            public static string FeatureId
            {
                get { return ConfigurationManager.AppSettings["QueryString_FeatureId"]; }
            }
            public static string StyleId
            {
                get { return ConfigurationManager.AppSettings["QueryString_StyleId"]; }
            }
            public static string SegmentId
            {
                get { return ConfigurationManager.AppSettings["QueryString_SegmentId"]; }
            }
            public static string Keyword
            {
                get { return ConfigurationManager.AppSettings["QueryString_Keyword"]; }
            }
            public static string ProductResultCount
            {
                get { return ConfigurationManager.AppSettings["QueryString_ProductResultCount"]; }
            }
            public static string ContentResultCount
            {
                get { return ConfigurationManager.AppSettings["QueryString_ContentResultCount"]; }
            }
            public static string SpecialAttribute
            {
                get { return ConfigurationManager.AppSettings["QueryString_SpecialAttribute"]; }
            }
            public static string IsHybridSearch
            {
                get { return ConfigurationManager.AppSettings["QueryString_IsHybrid"]; }
            }
            public static string IsFromCallout
            {
                get { return ConfigurationManager.AppSettings["QueryString_IsFromCallout"]; }
            }
        }

    }
}
