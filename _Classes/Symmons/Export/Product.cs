using System.Collections;

namespace symmons.com._Classes.Symmons.Export
{
    public class Product
    {
        private string _sku;

        public string Model
        {
            get { return _sku; }
            set { _sku = value; }
        }

        public string SKU { get; set; }

        public string ProductFamily { get; set; }

        public string ProductCategory { get; set; }

        public string ProductType { get; set; }

        public string ProductTitle { get; set; }

        public string Description { get; set; }

        public string SubmittalPDF { get; set; }

        public string InstallationPDF { get; set; }

        public string CatalogPDF { get; set; }

        public string PriceUS { get; set; }

        public string PriceCAN { get; set; }
    }

    public class ProductCollection : ArrayList
    {
        public ProductCollection() : base() { }
        public ProductCollection(ICollection c) : base(c) { }
    }
}