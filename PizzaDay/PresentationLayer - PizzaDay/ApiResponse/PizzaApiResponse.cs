using AplicationDomainLayer___PizzaDay.PaginationEntity;

namespace PresentationLayer___PizzaDay.ApiResponse
{
    public class PizzaApiResponse<T>
    {
        public T _Data { get; set; }
        public Metadata Meta { get; set; }
        public PizzaApiResponse(T Data)
        {
            _Data = Data;
        }
    }
}
