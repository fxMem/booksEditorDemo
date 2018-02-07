using BookEditorDemo.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BookEditorDemo.Web.Controllers
{
    public class FileController: ControllerBase
    {
        private static string[] _acceptedExtensions = { ".jpg", ".jpeg" };
        private BooksService _booksService;

        public FileController(BooksService booksService)
        {
            _booksService = booksService;
        }

        public HttpResponseMessage Get(int id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(_booksService.GetCover(id))
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return result;
        }

        // For purpose of this demo, I'm not concerning myself with buffering mode and filesizes.
        async public Task Post(int id)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
                
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            
            foreach (var file in provider.Contents)
            {
                var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
                var extension = Path.GetExtension(filename);
                if (!_acceptedExtensions.Any(e => string.Equals(e, extension, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new ArgumentException($"Unsupported file format - only {string.Join(",", _acceptedExtensions)} allowed!");
                }

                var buffer = await file.ReadAsByteArrayAsync();
                await _booksService.SaveCover(id, await file.ReadAsStreamAsync());
            }
        }
    }
}