using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Claudia.Domain.Configuration;
using Claudia.WebApi.DTO;
using Claudia.WebApi.Providers;
using ImageResizer;

namespace Claudia.WebApi.ImageEngine
{
    public static class ImageEngine
    {
        static void ProcessImage(MultipartFileData source, string path)
        {
            foreach (var version in AppConfig.GalleryImages.Versions)
            {
                var srcFileName = source.LocalPath + source.LocalFileName + source.FileExtension;
                var dstFileName = path + source.LocalFileName + version.Key + AppConfig.GalleryImages.FileFormatType;

                try
                {
                    ImageBuilder.Current.Build(srcFileName, dstFileName, new Instructions(version.Value));
                }
                catch (Exception msg)
                {
                    throw new ArgumentException("Failed to create file!", msg);
                }
            }
        }
        [Obsolete("use more generic with image category and new appconfig", true)]
        private static void ProcessImage(MultipartFileData source, string path, double[] crop)
        {
            foreach (var version in AppConfig.GalleryImages.Versions)
            {
                var srcFileName = source.LocalPath + source.LocalFileName + source.FileExtension;
                var dstFileName = path + source.LocalFileName + version.Key + AppConfig.GalleryImages.FileFormatType;

                var options = version.Value;
                options += "&crop=" + string.Join(",", crop);

                try
                {
                    //var result = 
                    ImageBuilder.Current.Build(srcFileName, dstFileName, new Instructions(options));

                }
                catch (Exception msg)
                {
                    throw new ArgumentException("Failed to create file!", msg);
                }
            }
        }

        private static void ProcessImage(string imgCategory, MultipartFileData source, string path, double[] crop)
        {
            var config = AppConfig.ImageData.FirstOrDefault(i => i.Key == imgCategory).Value;
            foreach (var version in config.ImageVersions)
            {
                var srcFileName = source.LocalPath + source.LocalFileName + source.FileExtension;
                var dstFileName = path + source.LocalFileName + version.Key + config.FileFormatType;

                var options = version.Value;
                options += "&crop=" + string.Join(",", crop);

                try
                {
                    //var result = 
                    ImageBuilder.Current.Build(srcFileName, dstFileName, new Instructions(options));

                }
                catch (Exception msg)
                {
                    throw new ArgumentException("Failed to create file!", msg);
                }
            }
        }


        [Obsolete("use ImageRequestDto", true)]
        public static bool ProcessImages(MultipartFormDataStreamProvider source)
        {
            var fileData = source.FileData;
            var formData = source.FormData;

            foreach (var file in fileData)
            {
                ProcessImage(file, source.GetLocalPath());
            }
            return true;
        }

        public static void ProcessImage(ImageRequestDto data)
        {
            ProcessImage(data.ImageCategory, data.FileData, data.FileData.LocalPath, data.GetCrop());
        }

    }
}