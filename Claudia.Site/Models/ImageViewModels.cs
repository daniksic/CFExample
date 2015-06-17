using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Claudia.Domain.Configuration;

namespace Claudia.Site.Models
{
    public class ImageUploadViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Html)]
        public string Description { get; set; }

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

    public class ImageDisplayViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Html)]
        public string Description { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public string GetThumnailUrl()
        {
            return AppConfig.ImageData["gallery"].GetThumnailUrl(this.ImageUrl);
        }

        public string GetImageUrl()
        {
            return AppConfig.ImageData["gallery"].GetThumnailUrl(this.ImageUrl);
        }
    }

}