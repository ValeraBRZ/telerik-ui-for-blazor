using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using IOFile = System.IO.File;

namespace TelerikBlazorDemos.Server.Controllers
{
    public class SourceController : Controller
    {
        // Add a "CopyToOutputDirectory" definition for these directories in TelerikBlazorDemos.csproj
        // so their source code can be read
        readonly List<string> AllowedDemoFilePaths = new List<string>()
        {
            "Constants",
            "Extensions",
            "Models",
            "Services",
            "Controllers",
            "DataAccess",
            "Shared",
            "DocumentProcessing"
        };

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly HtmlEncoder _htmlEncoder;

        private string RootPath => AppContext.BaseDirectory;

        public SourceController(IWebHostEnvironment hostingEnvironment, HtmlEncoder htmlEncoder)
        {
            _hostingEnvironment = hostingEnvironment;
            _htmlEncoder = htmlEncoder;
        }

        public ActionResult Index(string path)
        {
            try
            {
                string mappedPath = CleanUpPath(path);
                return GetFileContent(mappedPath);
            }
            catch (Exception ex)
            {
                // Return diagnostic details in the body so the failure can be inspected
                // in the browser Network tab when reproducing on Azure.
                return NotFound(String.Concat(path, " __ ", RootPath, " __ ", ex.Message));
            }
        }

        public ActionResult Read(string path)
        {
            try
            {
                string filePath = MapDemoFilePath(path);
                return GetFileContent(filePath);
            }
            catch (Exception)
            {
                return NotFound(String.Concat(path, "__", RootPath));
            }
        }

        private ActionResult GetFileContent(string path)
        {
            if (!IOFile.Exists(path))
            {
                throw new FileNotFoundException(path);
            }

            try
            {
                var source = IOFile.ReadAllText(path);
                return Content("<pre class='prettyprint'>" + _htmlEncoder.Encode(source) + "</pre>", "text/plain");
            }
            catch (FileNotFoundException)
            {
                return NotFound(path);
            }
        }

        private string CleanUpPath(string path)
        {
            // take care of GA and other querystrings, and them messing up the extensions
            if (path.IndexOf("?") > -1)
            {
                path = path.Substring(0, path.IndexOf("?"));
            }
            if (!path.EndsWith(".razor"))
            {
                path += ".razor";
            }

            path = path.Replace("/index", "/overview").Replace("/", Path.DirectorySeparatorChar.ToString());
            var mappedPath = RootPath.Replace("Server", "Client") + path;

            // ensure we return only files we want publicly available, such as the components under Pages
            // avoid traversals in the paths with the .\ and .. symbols
            string allowedRoot = Path.Combine(RootPath, "pages").ToLowerInvariant();
            if (mappedPath.ToLowerInvariant().IndexOf(allowedRoot) != 0 ||
                    mappedPath.IndexOf("..") > -1 || mappedPath.IndexOf(".\\") > -1)
            {
                throw new FileNotFoundException();
            }

            // resolve the actual on-disk casing so lookups work on case-sensitive
            // file systems (e.g. Linux App Service), where the requested route is lower-cased
            mappedPath = ResolveCaseInsensitivePath(mappedPath) ?? mappedPath;

            if (!IOFile.Exists(mappedPath))
            {
                throw new FileNotFoundException();
            }

            return mappedPath;
        }

        private string MapDemoFilePath(string path)
        {
            // normalize directory separators
            path = path.Replace("/", Path.DirectorySeparatorChar.ToString());
            path = path.Replace("\\", Path.DirectorySeparatorChar.ToString());

            var fullPath = Path.Combine(RootPath, path);

            List<string> allowedPaths = AllowedDemoFilePaths
                .Select(path => Path.Combine(RootPath, path))
                .ToList();

            // Check if it is deployed on Azure
            if (RootPath.IndexOf("wwwroot") > -1)
            {
                if (path.IndexOf("wwwroot") > -1)
                {
                    fullPath = Path.Combine(RootPath, path);
                    allowedPaths.Add(Path.Combine(RootPath, path));
                }
            }
            else
            {
                if (path.IndexOf("wwwroot") > -1)
                {
                    fullPath = Path.Combine(RootPath.Substring(0, RootPath.IndexOf("bin")), path);
                    allowedPaths.Add(Path.Combine(RootPath.Substring(0, RootPath.IndexOf("bin")), path));
                }
            }

            var pathIsAllowed = allowedPaths.Any(allowedPath => fullPath.StartsWith(allowedPath));

            if (!pathIsAllowed)
            {
                throw new FileNotFoundException();
            }

            // resolve the actual on-disk casing so lookups work on case-sensitive
            // file systems (e.g. Linux App Service)
            fullPath = ResolveCaseInsensitivePath(fullPath) ?? fullPath;

            if (!IOFile.Exists(fullPath))
            {
                throw new FileNotFoundException();
            }

            return fullPath;
        }

        // Walks the requested path segment by segment starting from the known-good
        // application base directory and matches each segment ignoring case.
        // Returns the correctly-cased path or null when no match exists.
        private string ResolveCaseInsensitivePath(string fullPath)
        {
            if (IOFile.Exists(fullPath) || Directory.Exists(fullPath))
            {
                return fullPath;
            }

            string root = RootPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            if (!fullPath.StartsWith(root, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            string relative = fullPath.Substring(root.Length);
            string[] segments = relative.Split(
                new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar },
                StringSplitOptions.RemoveEmptyEntries);

            string currentPath = root;
            foreach (var segment in segments)
            {
                string candidate = Path.Combine(currentPath, segment);
                if (Directory.Exists(candidate) || IOFile.Exists(candidate))
                {
                    currentPath = candidate;
                    continue;
                }

                if (!Directory.Exists(currentPath))
                {
                    return null;
                }

                string match = Directory.EnumerateFileSystemEntries(currentPath)
                    .FirstOrDefault(entry => string.Equals(
                        Path.GetFileName(entry), segment, StringComparison.OrdinalIgnoreCase));

                if (match == null)
                {
                    return null;
                }

                currentPath = match;
            }

            return currentPath;
        }

    }
}
