namespace eFashionShop.ViewModels.Catalog.Products
{
    public class ProductFeatureVm
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Details { set; get; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Trademark { set; get; }
        public string ProductOrigin { set; get; }
        public string Ingredient { set; get; }
        public string Expiry { set; get; }
        public string BlogDescription { set; get; }
        public bool IsFeatured { get; set; }
    }
}
