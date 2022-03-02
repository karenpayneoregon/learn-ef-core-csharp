namespace EntityFrameworkCoreHelpers.Classes
{
    public class Category : IBase
    {
        public int CategoryId { get; set; }
        public int Id => CategoryId;
        public string Name { get; set; }
        public string Description { get; set; }

    }

}
