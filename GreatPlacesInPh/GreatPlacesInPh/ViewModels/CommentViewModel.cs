using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatPlacesInPh.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public UserViewModel User { get; set; }         
        public string Comment { get; set; }
    }
}