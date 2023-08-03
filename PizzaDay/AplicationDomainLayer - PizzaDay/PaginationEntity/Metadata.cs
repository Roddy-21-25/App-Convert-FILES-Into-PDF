namespace AplicationDomainLayer___PizzaDay.PaginationEntity
{
    public class Metadata
    {
        public int TotalCountOfElements { get; set; }
        public int ElementToShow { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set;}
        public string NextPageUrl { get; set; }
        public string PreviousPageUrl { get; set; }
    }
}
