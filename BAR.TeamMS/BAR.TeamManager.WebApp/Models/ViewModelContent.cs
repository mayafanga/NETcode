using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BAR.TeamManager.Model.EnumType;

namespace BAR.TeamManager.WebApp.Models
{
    public class ViewModelContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public LuceneTypeEnum LuceneEnum { get; set; }
    }
}