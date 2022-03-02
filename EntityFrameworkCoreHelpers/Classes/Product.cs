namespace EntityFrameworkCoreHelpers.Classes
{
    public class Product : IBase
    {
        public int ProductId { get; set; }
        public int Id => ProductId;
        public string Name { get; set; }
        public string Description { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
    }
}