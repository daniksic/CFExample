using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Claudia.Site.Areas.CMS.Models
{
    public class ImageUploadViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required, HiddenInput()]
        public int LinkId { get; set; }

        [Required]
        [Display(Name = "Image upload")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }

        [Required]
        public CropCords PictureCropCords { get; set; }

        [Required]
        public PictureDimensions PictureSize { get; set; }

        public double[] GetPictureCropCords()
        {
            return new double[4]
            {
                PictureCropCords.X,
                PictureCropCords.Y,
                (PictureSize.W - (PictureCropCords.W + PictureCropCords.X))*-1,
                (PictureSize.H - (PictureCropCords.H + PictureCropCords.Y))*-1
            };
        }

        public class CropCords
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double W { get; set; }
            public double H { get; set; }
        }

        public class PictureDimensions
        {
            public int W { get; set; }
            public int H { get; set; }
        }
    }

}