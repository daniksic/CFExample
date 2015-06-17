using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Claudia.Domain.Configuration;
using ImageResizer;

namespace Claudia.Site.Services
{
    public static class ImageCreationService
    {
        public static async Task<string> ProcessImageAsync(string categoryName, HttpPostedFileBase sourceFile, double[] cropData)
        {
            var configData = AppConfig.ImageData[categoryName];

            //  Saving file to local folder and get name of file
            var savedFileName = await SaveFileAsync(sourceFile, configData)
                .ContinueWith(filename => CreateImageVersions(filename.Result, configData, cropData))
                ;

            // Saving other versions of image like thumbnails
            //var savedImages = await CreateImageVersions(fileName, configData);

            return savedFileName.Result;
        }

        /// <summary>
        /// Creates new versions of image
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="configData"></param>
        /// <param name="cropData"></param>
        /// <returns>Returns file name if everething was ok and saved</returns>
        private static async Task<string> CreateImageVersions(string fileName, AppConfig.ConfigModel configData, double[] cropData)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var savePath = HostingEnvironment.MapPath(configData.FilePathRelative);

                    foreach (var version in configData.ImageVersions)
                    {
                        var srcFileName = string.Concat(savePath, fileName, configData.FileFormatType);
                        var dstFileName = string.Concat(savePath, fileName, version.Key, configData.FileFormatType);

                        var options = version.Value;
                        options += string.Concat("&crop=", string.Join(",", cropData));

                        //var result = 
                        ImageBuilder.Current.Build(srcFileName, dstFileName, new Instructions(options));
                    }

                    return fileName;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Failed to create images!", ex);
                }
            });
        }

        /// <summary>
        /// Saves file to local folder
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="categoryName">Name of category of picture</param>
        /// <returns>Guid string name of file</returns>
        private static async Task<string> SaveFileAsync(HttpPostedFileBase sourceFile, AppConfig.ConfigModel configData)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var fn = Guid.NewGuid().ToString();
                    var fullPath = Path.Combine(HostingEnvironment.MapPath(configData.FilePathRelative),
                        string.Concat(fn, configData.FileFormatType)
                        );

                    sourceFile.SaveAs(fullPath);
                    return fn;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in ImageCreationService", ex);
                }
            });
        }


    }
}