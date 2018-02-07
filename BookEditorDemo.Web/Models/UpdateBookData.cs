using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookEditorDemo.Web.Models
{
    // In this demo, update and create DTO are almost same, 
    // but it's not always the case.
    public class UpdateBookData
    {
        public string Title { get; set; }

        public string ISBN { get; set; }

        public string Publisher { get; set; }

        public int? PageNumber { get; set; }

        public int? PublishYear { get; set; }

        public List<int> AuthorIds { get; set; }
    }
}