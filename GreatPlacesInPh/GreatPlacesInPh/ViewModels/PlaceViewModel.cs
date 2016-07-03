using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatPlacesInPh.ViewModels
{
    public class PlaceViewModel
    {
        public Guid PlaceId { get; set; }
        public string Name { get; set; }
        public UserViewModel User { get; set; }
        public string ImageUrl { get; set; }
        public string Review { get; set; }
        public string Comment { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}