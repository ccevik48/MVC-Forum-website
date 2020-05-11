using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnTwoWheels.Models
{
    public class UserPostViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public byte[] Thumbnail { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> UserId { get; set; }

        public virtual IndexViewModel IndexViewModel { get; set; }
    }
}