using System;
using System.Collections.Generic;

namespace images_api.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Img { get; set; }
    }
}
