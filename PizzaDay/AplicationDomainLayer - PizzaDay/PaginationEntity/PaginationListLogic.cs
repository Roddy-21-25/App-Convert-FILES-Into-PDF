using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationDomainLayer___PizzaDay.PaginationEntity
{
    public class PaginationListLogic<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ElementToShow { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;

        public PaginationListLogic(
            List<T> items, int count, int pageNumber, int elementToShow
            )
        {
            TotalCount = count;
            ElementToShow = elementToShow;
            CurrentPage = pageNumber;

            TotalPages = (int)Math.Ceiling(count / (double)elementToShow);
            AddRange( items );
        }

        public static PaginationListLogic<T> PaginationImplementation(
            IEnumerable<T> elements, int pageNumber, int elementsToShow
            )
        {
            var count = elements.Count();

            var itemsPagination = elements.Skip((pageNumber - 1) * elementsToShow).Take(elementsToShow).ToList();

            return new PaginationListLogic<T>(itemsPagination, count, pageNumber, elementsToShow);
        }
    }
}
