using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public class UserSettings
    {
        public string UserId { get; set; }

        public SortByProperty SortBy { get; set; }

        public bool OrderByDescending { get; set; }
    }
}
