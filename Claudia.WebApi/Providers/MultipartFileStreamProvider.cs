using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Claudia.WebApi.Providers
{
    /// <summary>
    /// A suited for writing each MIME body parts of the MIME multipart
    /// message to a file using a <see cref="FileStream"/>.
    /// </summary>
    public class MultipartFileStreamProvider : MultipartStreamProvider
    {
        private const int MinBufferSize = 1;
        private const int DefaultBufferSize = 0x1000;

        private string _rootPath;
        private int _bufferSize = DefaultBufferSize;

        private Collection<MultipartFileData> _fileData = new Collection<MultipartFileData>();

        /// <summary>
        /// Initializes a new instance class.
        /// </summary>
        /// <param name="rootPath">The root path where the content of MIME multipart body parts are written to.</param>
        public MultipartFileStreamProvider(string rootPath)
            : this(rootPath, DefaultBufferSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="rootPath">The root path where the content of MIME multipart body parts are written to.</param>
        /// <param name="bufferSize">The number of bytes buffered for writes to a file.</param>
        public MultipartFileStreamProvider(string rootPath, int bufferSize)
        {
            if (rootPath == null)
            {
                throw new ArgumentNullException("rootPath");
            }

            if (bufferSize < MinBufferSize)
            {
                throw new ArgumentOutOfRangeException("bufferSize", bufferSize, MinBufferSize.ToString(CultureInfo.InvariantCulture));
            }

            _rootPath = Path.GetFullPath(rootPath);
            _bufferSize = bufferSize;
        }

        /// <summary>
        /// Gets a collection containing the local files names and associated HTTP content headers of MIME 
        /// body parts written to file.
        /// </summary>
        public Collection<MultipartFileData> FileData
        {
            get { return _fileData; }
        }

        /// <summary>
        /// Gets the root path where the content of MIME multipart body parts are written to.
        /// </summary>
        protected string RootPath
        {
            get { return _rootPath; }
        }

        /// <summary>
        /// Gets the number of bytes buffered for writes to a file.
        /// </summary>
        protected int BufferSize
        {
            get { return _bufferSize; }
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Stream is closed by caller (MultipartWriteDelegatingStream is just a wrapper that calls into the inner stream.)")]
        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (parent == null) { throw new ArgumentNullException("parent"); }

            if (headers == null) { throw new ArgumentNullException("headers"); }

            string localFilePath, localFileName, localFileExtension;
            try
            {
                localFileName= GetLocalFileName(headers);
                localFilePath = GetLocalPath(); //Path.Combine(_rootPath, Path.GetFileName(filename));
                localFileExtension = GetFileExtension(headers);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("MultipartStreamProviderInvalidLocalFileName", e);
            }

            // Add local file name 
            MultipartFileData fileData = new MultipartFileData(headers, localFilePath, localFileName, localFileExtension);
            _fileData.Add(fileData);

            try
            {
                return File.Create(localFilePath + localFileName + localFileExtension, _bufferSize, FileOptions.Asynchronous);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Faild to create file", e);
            }
            
        }

        /// <summary>
        /// Gets the name of the local file. Combine with the localpath to
        /// create an absolute file name where the contents of the current MIME body part
        /// will be stored.
        /// </summary>
        /// <param name="headers">The headers for the current MIME body part.</param>
        /// <returns>A relative filename with no path component.</returns>
        public virtual string GetLocalFileName(HttpContentHeaders headers)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }

            return Guid.NewGuid().ToString();
        }

        public virtual string GetLocalPath()
        {
            return _rootPath;
        }

        public virtual string GetFileExtension(HttpContentHeaders headers)
        {
            if (headers == null)
            {
                throw new ArgumentNullException("headers");
            }
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName) ? headers.ContentDisposition.FileName : "NoName.jpg";
            name = name.Replace("\"", string.Empty); //this is here because Chrome submits files in quotation marks which get treated as part of the filename and get escaped
            return name.Substring(name.LastIndexOf(".", System.StringComparison.Ordinal));
        }

        public virtual bool CheckIfImg(HttpContentHeaders headers)
        {
            if (headers == null) { throw new ArgumentNullException("headers");}
            return string.Equals("img", headers.ContentDisposition.Name);
        }
    }

}