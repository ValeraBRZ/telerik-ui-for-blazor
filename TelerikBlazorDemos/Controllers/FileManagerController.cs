using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Telerik.Blazor.Components.FileManager;
using System.Text.Json;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using System.Net.Mime;
using Microsoft.AspNetCore.StaticFiles;
using TelerikBlazorDemos.Models.FileManager;
using TelerikBlazorDemos.Services.FileManager;
using TelerikBlazorDemos.Constants;
using System.Net.Http;

namespace TelerikBlazorDemos.Controllers
{
    [Route("api/[controller]/[action]")]
    public class FileManagerController : Controller
    {
        public IWebHostEnvironment HostingEnvironment { get; set; }

        public HierarchicalFileService HierarchicalFileService { get; set; }

        public FlatFileService FlatFileService { get; set; }

        public FileManagerController(
            IWebHostEnvironment hostingEnvironment,
            HierarchicalFileService hierarchicalFileService,
            FlatFileService flatFileService)
        {
            HostingEnvironment = hostingEnvironment;
            HierarchicalFileService = hierarchicalFileService;
            FlatFileService = flatFileService;
        }

        #region Hierarchical data

        [HttpPost]
        public async Task<IActionResult> UploadHierarchicalFiles(IEnumerable<IFormFile> files)
        {
            var result = await UploadFiles(files);
            return result;
        }

        #endregion Hierarchical data

        #region Flat data

        [HttpPost]
        public async Task<IActionResult> UploadFlatFiles(IEnumerable<IFormFile> files)
        {
            var result = await UploadFiles(files);
            return result;
        }

        #endregion Flat data

        #region Methods

        private async Task<IActionResult> UploadFiles(IEnumerable<IFormFile> files)
        {
            // save the files somewhere
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
                    var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, fileName);

                    // implement validation and authentication here

                    // The files are not actually saved in this demo
                    // using (var fileStream = new FileStream(physicalPath, FileMode.Create))
                    // {
                    //    await file.CopyToAsync(fileStream);
                    // }

                    // instead mock async operation
                    await Task.Yield();
                }
            }

            // this controller always returns a success, unless an exception is thrown
            return new OkResult();
        }

        #endregion Methods
    }
}
