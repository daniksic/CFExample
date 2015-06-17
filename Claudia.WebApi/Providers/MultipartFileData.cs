using System;
using System.Net.Http.Headers;

namespace Claudia.WebApi.Providers
{
    /// <summary>
    /// Represents a multipart file data.
    /// </summary>
    public class MultipartFileData
    {
        /// <summary>
        /// Gets or sets the headers of the multipart file data.
        /// </summary>
        /// 
        /// <returns>
        /// The headers of the multipart file data.
        /// </returns>
        public HttpContentHeaders Headers { get; private set; }

        /// <summary>
        /// Gets or sets the name of the local file for the multipart file data.
        /// </summary>
        /// 
        /// <returns>
        /// The name of the local path and file for the multipart file data.
        /// </returns>
        public string LocalPath { get; private set; }

        /// <summary>
        /// Gets or sets only the file name with extension of the local file.
        /// </summary>
        public string LocalFileName { get; private set; }

        /// <summary>
        /// Gets or sets only file extension
        /// </summary>
        public string FileExtension { get; private set; }

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="headers">The headers of the multipart file data.</param>
        /// <param name="localPath">The path of the local file</param>
        /// <param name="localFileName">The name of the local file for the multipart file data.</param>
        public MultipartFileData(HttpContentHeaders headers, string localPath, string localFileName, string fileExtension)
        {
            if (headers == null)
                throw new ArgumentNullException("headers");
            if (localPath == null)
                throw new ArgumentNullException("localPath");
            if (localFileName == null)
                throw new ArgumentNullException("localFileName");

            this.Headers = headers;
            this.LocalPath = localPath;
            this.LocalFileName = localFileName;
            this.FileExtension = fileExtension;
        }
    }
}