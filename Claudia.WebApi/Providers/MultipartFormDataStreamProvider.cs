using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting.Internal;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Claudia.WebApi.Providers
{
    public class MultipartFormDataStreamProvider : MultipartFileStreamProvider//System.Net.Http.MultipartFormDataStreamProvider
    {
        public MultipartFormDataStreamProvider(string rootPath)
            : base(rootPath)
        {
        }

        public MultipartFormDataStreamProvider(string rootPath, int bufferSize)
            : base(rootPath, bufferSize)
        {
        }

        private NameValueCollection _formData = new NameValueCollection();
        private Collection<bool> _isFormData = new Collection<bool>();
        private CancellationToken _cancellationToken;

        /// <summary>
        /// Gets a <see cref="T:System.Collections.Specialized.NameValueCollection"/> of form data passed as part of the multipart form data.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Collections.Specialized.NameValueCollection"/> of form data.
        /// </returns>
        public NameValueCollection FormData
        {
            get
            {
                return this._formData;
            }
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            if (headers == null) throw new ArgumentNullException("headers");

            ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;
            if (contentDisposition != null)
            {
                if (!string.IsNullOrEmpty(contentDisposition.FileName))
                {
                    this._isFormData.Add(false);
                    return base.GetStream(parent, headers);
                }
                else
                {
                    this._isFormData.Add(true);
                    return (Stream)new MemoryStream();
                }
            }
            else
                throw new ArgumentNullException("Content-Disposition");
        }

        /// <summary>
        /// Reads the non-file contents as form data.
        /// </summary>
        /// 
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        public override async Task ExecutePostProcessingAsync()
        {
            for (int index = 0; index < this.Contents.Count; ++index)
            {
                if (this._isFormData[index])
                {
                    HttpContent formContent = this.Contents[index];
                    ContentDispositionHeaderValue contentDisposition = formContent.Headers.ContentDisposition;

                    string token = contentDisposition.Name;
                    string formFieldName = (string.IsNullOrWhiteSpace(token) ||
                                            !token.StartsWith("\"", StringComparison.Ordinal) ||
                                            (!token.EndsWith("\"", StringComparison.Ordinal) || token.Length <= 1))
                        ? token
                        : token.Substring(1, token.Length - 2); //string.Empty;

                    this._cancellationToken.ThrowIfCancellationRequested();
                    string formFieldValue = await formContent.ReadAsStringAsync();
                    this.FormData.Add(formFieldName, formFieldValue);
                }
            }
        }


        public override Task ExecutePostProcessingAsync(CancellationToken cancellationToken)
        {
            this._cancellationToken = cancellationToken;
            return this.ExecutePostProcessingAsync();
        }
    }
}