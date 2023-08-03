using AplicationDomainLayer___PizzaDay.Interfaces;

namespace AplicationDomainLayer___PizzaDay.Services
{
    public class UrlServices : IUrlServices
    {
        private readonly string _baseUri;

        public UrlServices(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri UrlPagination(string ControllerUrl)
        {
            string baseUrl = $"{_baseUri}{ControllerUrl}";
            return new Uri(baseUrl);
        }
    }
}
