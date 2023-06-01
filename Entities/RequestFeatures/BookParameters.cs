namespace Entities.RequestFeatures
{
    public class BookParameters : RequestParameters
    {
        public uint minPrice { get; set; }
        public uint maxPrice { get; set; }
        public bool ValidPriceRange => maxPrice > minPrice;

        public String? SearchTerm { get; set; }

        public BookParameters()
        {
            OrderBy = "id";
            minPrice = 0;
            maxPrice = 1000;
        }

    }
}
