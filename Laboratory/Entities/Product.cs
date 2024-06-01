using Laboratory.Enums;

namespace Laboratory.Entities
{
    public class Product
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public ProductType Type { get; set; }
    }
}
