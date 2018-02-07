using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public enum SortByProperty
    {
        Title = 0,
        PublishYear
    }

    public class PaginationData
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public SortByProperty SortBy { get; set; }

        public bool OrderByDescending { get; set; }
    }
}
