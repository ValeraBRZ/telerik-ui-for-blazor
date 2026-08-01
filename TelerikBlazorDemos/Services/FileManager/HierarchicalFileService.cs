using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.DataSource.Extensions;
using Telerik.Blazor.Components;
using TelerikBlazorDemos.Models.FileManager;
using TelerikBlazorDemos.Constants;
using TelerikBlazorDemos.Extensions;

namespace TelerikBlazorDemos.Services.FileManager
{
    public class HierarchicalFileService
    {
        #region Properties

        public readonly string PathSeparator = Path.DirectorySeparatorChar.ToString();

        const int ActionDelay = 50;

        public List<HierarchicalFileEntry> Data { get; set; }

        #endregion Properties

        #region Methods

        public async Task<List<HierarchicalFileEntry>> ReadHierarchicalDataAsync()
        {
            await Task.Delay(ActionDelay);

            if (Data == null || Data.Count == 0)
            {
                Data = ReadCustomHierarchicalData();
            }

            return Data;
        }

        public List<HierarchicalFileEntry> ReadCustomHierarchicalData()
        {
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), FileManagerConstants.ReadPath);
            var rootDirectory = new DirectoryInfo(rootPath);
            var directories = rootDirectory.EnumerateDirectories();
            var files = rootDirectory.EnumerateFiles();

            var data = new List<HierarchicalFileEntry>();

            foreach (var directory in directories)
            {
                var entry = ConvertToHierarchicalEntry(directory);
                data.Add(entry);
            }

            foreach (var file in files)
            {
                var entry = ConvertToHierarchicalEntry(file);
                data.Add(entry);
            }

            return data;
        }

        public static HierarchicalFileEntry ConvertToHierarchicalEntry(DirectoryInfo directory)
        {
            var directories = directory.GetDirectories()
                .Select(x => ConvertToHierarchicalEntry(x))
                .ToList();
            var files = directory.GetFiles()
                .Select(x => ConvertToHierarchicalEntry(x))
                .ToList();
            var items = directories.Union(files).ToList();

            var entry = new HierarchicalFileEntry()
            {
                Name = directory.Name,
                IsDirectory = true,
                HasDirectories = directory.GetDirectories().Count() > 0,
                Items = items,
                Directories = directories,
                DateCreated = directory.CreationTime,
                DateCreatedUtc = directory.CreationTimeUtc,
                DateModified = directory.LastWriteTime,
                DateModifiedUtc = directory.LastWriteTimeUtc,
                // trim the real path to avoid exposing it
                // for security purposes
                Path = directory.FullName.Substring(directory.FullName.IndexOf(FileManagerConstants.FilesPath)),
                Extension = directory.Extension,
                Size = 2 * 1024 * directory.GetFiles().LongCount()
            };

            return entry;
        }

        public static HierarchicalFileEntry ConvertToHierarchicalEntry(FileInfo file)
        {
            var entry = new HierarchicalFileEntry()
            {
                Name = Path.GetFileNameWithoutExtension(file.FullName),
                IsDirectory = false,
                HasDirectories = false,
                Items = new List<HierarchicalFileEntry>(),
                Directories = new List<HierarchicalFileEntry>(),
                DateCreated = file.CreationTime,
                DateCreatedUtc = file.CreationTimeUtc,
                DateModified = file.LastWriteTime,
                DateModifiedUtc = file.LastWriteTimeUtc,
                // trim the real path to avoid exposing it
                // for security purposes
                Path = file.FullName.Substring(file.FullName.IndexOf(FileManagerConstants.FilesPath)),
                Extension = file.Extension,
                Size = file.Length
            };

            return entry;
        }

        #endregion Methods

        #region Methods - CRUD

        public async Task CreateAsync(HierarchicalFileEntry item, string parentPath)
        {
            await Task.Delay(ActionDelay);

            CreateFolderInFileSystem(parentPath, item);

            RefreshData();
        }

        public HierarchicalFileEntry ModelInit(string parentPath)
        {
            var item = new HierarchicalFileEntry();
            item.Name = $"some name";
            item.Size = 0;
            item.Path = Path.Combine(parentPath, item.Name);
            item.IsDirectory = true;
            item.HasDirectories = false;
            item.DateCreated = DateTime.Now;
            item.DateCreatedUtc = DateTime.Now;
            item.DateModified = DateTime.Now;
            item.DateModifiedUtc = DateTime.Now;
            return item;
        }

        public async Task UpdateAsync(HierarchicalFileEntry item, string parentPath)
        {
            await Task.Delay(ActionDelay);

            // the name prop is updated, but update the path to the file as well
            var name = item.Name ?? string.Empty;
            var extension = item.Extension ?? string.Empty;
            var fullName = extension.Length > 0 && name.EndsWith(extension) ?
                name : $"{name}{extension}";

            if (item.IsDirectory)
            {
                // intentionally skip update
                // as this will require updating the Path of children recursively
            }
            else
            {
                Rename(item);
            }

            var parentChildren = GetParentItems(item);

            parentChildren.RemoveAll(x => x.Id == item.Id);
            parentChildren.Add(item);

            RefreshData();
        }

        public async Task DeleteAsync(HierarchicalFileEntry item)
        {
            await Task.Delay(ActionDelay);

            var parentDirectory = GetParent(item);
            var parentItems = GetParentItems(item);
            var parentDirectories = GetParentDirectories(item);

            parentItems.Remove(item);
            parentItems.RemoveAll(x => x.Id == item.Id);

            parentDirectories.Remove(item);
            parentDirectories.RemoveAll(x => x.Id == item.Id);

            if (parentDirectory != null)
            {
                parentDirectory.HasDirectories = parentDirectory.Directories.Count > 0;
            }

            RefreshData();
        }

        private void RefreshData()
        {
            Data = new List<HierarchicalFileEntry>(Data);
        }

        private HierarchicalFileEntry GetParent(HierarchicalFileEntry item)
        {
            var parentItem = Data
                .FindRecursive(x => x.Items.Any(y => y.Id == item.Id));

            return parentItem;
        }

        private List<HierarchicalFileEntry> GetParentItems(HierarchicalFileEntry item)
        {
            var parentItem = GetParent(item);
            var parentItemChildren = parentItem?.Items ?? Data;
            return parentItemChildren;
        }

        private List<HierarchicalFileEntry> GetParentDirectories(HierarchicalFileEntry item)
        {
            var parentItem = GetParent(item);
            var parentItemDirectories = parentItem?.Directories ?? Data;
            return parentItemDirectories;
        }

        #endregion Methods - CRUD

        #region Download

        public async Task DownloadAsync(FileManagerDownloadEventArgs args)
        {
            await Task.Delay(ActionDelay);

            // use inline dataUrl for a sample file
            var dataUrl = FileManagerConstants.SampleFileBase64String;
            var bytes = Convert.FromBase64String(dataUrl);
            var stream = new MemoryStream(bytes);

            args.FileName = FileManagerConstants.SampleFileName;
            args.Stream = stream;
            args.MimeType = FileManagerConstants.SampleFileMimeType;
        }

        #endregion Download

        #region Upload

        public async Task UploadAsync(UploadSuccessEventArgs args, string path)
        {
            await Task.Delay(ActionDelay);

            var uploadFiles = args.Files;

            var files = uploadFiles
                .Select(x => new HierarchicalFileEntry()
                {
                    Name = Path.GetFileNameWithoutExtension(x.Name),
                    IsDirectory = false,
                    HasDirectories = false,
                    DateCreated = DateTime.Now,
                    DateCreatedUtc = DateTime.Now,
                    DateModified = DateTime.Now,
                    DateModifiedUtc = DateTime.Now,
                    Path = Path.Combine(path, x.Name),
                    Extension = x.Extension,
                    Size = x.Size
                })
                .ToList();

            var directory = GetDirectory(path);
            var directoryItems = directory != null ? directory.Items : Data;

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                directoryItems.Add(file);
            }

            RefreshData();
        }

        #endregion Upload

        #region Rename

        private void Rename(HierarchicalFileEntry item)
        {
            var parentDirectory = GetParent(item);
            var parentChildren = GetParentItems(item);

            var existingFiles = parentChildren
                // depending on whether the file system supports
                // files with the same name, but different extensions or not, etc.
                .Where(x =>
                    x.Name == item.Name ||
                    x.Name == $"{item.Name}{item.Extension}" ||
                    $"{x.Name}{x.Extension}" == item.Name ||
                    $"{x.Name}{x.Extension}" == $"{item.Name}{item.Extension}"
                )
                .ToList();

            var newName = existingFiles.Count == 0 ?
                item.Name :
                GenerateUniqueEntryName(item);

            item.Name = newName;

            var fullName = $"{item.Name}{item.Extension}";
            var pathSegments = item.Path.Split(PathSeparator).ToList();
            pathSegments.Remove(pathSegments.LastOrDefault());
            var parentPath = string.Join(PathSeparator, pathSegments);

            item.Path = Path.Combine(parentPath, fullName);
        }

        #endregion Rename

        #region Create

        private HierarchicalFileEntry CreateFolderInFileSystem(string path, HierarchicalFileEntry entry)
        {
            var newFolderName = GenerateUniqueEntryName(entry);
            entry.Name = newFolderName;
            entry.IsDirectory = true;
            entry.HasDirectories = false;
            entry.DateCreated = DateTime.Now;
            entry.DateCreatedUtc = DateTime.Now;
            entry.DateModified = DateTime.Now;
            entry.DateModifiedUtc = DateTime.Now;
            entry.Path = Path.Combine(path, newFolderName);
            entry.Extension = null;

            var parentDirectory = GetDirectory(path);

            if (parentDirectory != null)
            {
                // simulate add in file system
                parentDirectory.Items.Add(entry);
                parentDirectory.Directories.Add(entry);
                parentDirectory.HasDirectories = parentDirectory.Directories.Count > 0;
            }
            else
            {
                // create a folder in the root dir
                Data.Add(entry);
            }

            return entry;
        }

        private string GenerateUniqueEntryName(HierarchicalFileEntry entry)
        {
            var parentDirectory = GetParent(entry);
            var uniqueEntryName = entry.Name;
            var entryNameCounter = 0;

            if (entry.IsDirectory)
            {
                var existingSubdirectory = GetSubdirectory(parentDirectory, uniqueEntryName);

                while (existingSubdirectory != null)
                {
                    entryNameCounter++;
                    uniqueEntryName = $"{entry.Name} ({entryNameCounter})";
                    existingSubdirectory = GetSubdirectory(parentDirectory, uniqueEntryName);
                }

                return uniqueEntryName;
            }
            else
            {
                var existingFile = GetFile(parentDirectory, uniqueEntryName);

                while (existingFile != null)
                {
                    entryNameCounter++;
                    uniqueEntryName = $"{entry.Name} ({entryNameCounter})";
                    existingFile = GetFile(parentDirectory, uniqueEntryName);
                }

                return uniqueEntryName;
            }
        }

        #endregion Create

        #region Delete

        private void DeleteFromFileSystem(string path, List<string> fileNames)
        {
            for (int i = 0; i < fileNames.Count; i++)
            {
                var fileName = fileNames[i];
                DeleteFromFileSystem(path, fileName);
            }
        }

        private void DeleteFromFileSystem(string path, string fileName)
        {
            var directory = GetDirectory(path);

            if (directory == null)
            {
                // root dir
                var entry = Data
                    .FirstOrDefault(x => x.Name == fileName || $"{x.Name}{x.Extension}" == fileName);

                Data.Remove(entry);
            }
            else
            {
                var entry = GetFile(directory, fileName);
                directory.Items.Remove(entry);
            }
        }

        #endregion Delete

        #region File processing

        private HierarchicalFileEntry GetFile(HierarchicalFileEntry parentDirectory, string fileName)
        {
            var path = parentDirectory?.Path;
            var file = GetFile(path, fileName);

            return file;
        }

        public HierarchicalFileEntry GetFile(string path, string fileName)
        {
            var parentDirectory = GetDirectory(path);

            if (parentDirectory == null)
            {
                // root dir
                var file = Data
                    .FirstOrDefault(x =>
                        x.IsDirectory == false &&
                        (x.Name == fileName || $"{x.Name}{x.Extension}" == fileName));

                return file;
            }
            else
            {
                var file = parentDirectory?.Items?
                    .FirstOrDefault(x =>
                        x.IsDirectory == false &&
                        (x.Name == fileName || $"{x.Name}{x.Extension}" == fileName));

                return file;
            }
        }

        private IEnumerable<HierarchicalFileEntry> SubdirectoriesSelector(HierarchicalFileEntry entry)
        {
            if (entry != null && entry.IsDirectory)
            {
                var subdiretories = entry.Items?.Where(x => x.IsDirectory);
                return subdiretories;
            }

            return null;
        }

        private HierarchicalFileEntry GetDirectory(string path)
        {
            var directory = Data.FirstOrDefault(x => x.IsDirectory && x.Path == path);

            if (directory == null)
            {
                directory = GetSubdirectory(path);
            }

            return directory;
        }

        private HierarchicalFileEntry GetSubdirectory(string path)
        {
            var subdirectory = Data
                .SelectRecursive(x => SubdirectoriesSelector(x))
                .FirstOrDefault(x => x.IsDirectory && x.Path == path);

            return subdirectory;
        }

        public HierarchicalFileEntry GetSubdirectory(HierarchicalFileEntry parentDirectory, string subdirectoryName)
        {
            if (parentDirectory == null)
            {
                // root dir
                var subdirectory = Data
                    .FirstOrDefault(x => x.IsDirectory && x.Name == subdirectoryName);

                return subdirectory;
            }
            else
            {
                var subdirectory = parentDirectory?.Items?
                    .FirstOrDefault(x => x.IsDirectory && x.Name == subdirectoryName);

                return subdirectory;
            }
        }

        #endregion File processing
    }
}
