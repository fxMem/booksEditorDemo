using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookEditorDemo.Web.Models
{
    public class CreateBookData
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        public string Publisher { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        public int PublishYear { get; set; }

        [Required]
        public List<int> AuthorIds { get; set; }
    }
}