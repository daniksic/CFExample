using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Claudia.WebApi.Providers;

namespace Claudia.WebApi.DTO
{
    public class ImageRequestDto
    {
        public string ImageCategory { get; set; }
        public Crop Crop { get; set; }
        public PictureSize Size { get; set; }

        public MultipartFileData FileData { get; set; }

        public double[] GetCrop()
        {
            return new double[4]
            {
                Crop.X,
                Crop.Y,
                (Size.W - (Crop.W + Crop.X))*-1,
                (Size.H - (Crop.H + Crop.Y))*-1
            };
        }

        public string GetFullServerPath()
        {
            return FileData.LocalPath + FileData.LocalFileName + FileData.FileExtension;
        }
    }

    public class Crop
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double W { get; set; }
        public double H { get; set; }
    }

    public class PictureSize
    {
        public int W { get; set; }
        public int H { get; set; }
    }


    public class ImageResponseDto
    {
        public string ServerFileName { get; set; }
        public string ClientLocalFileName { get; set; }
    }

}