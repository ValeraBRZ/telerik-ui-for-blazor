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

namespace TelerikBlazorDemos.Services.FileManager
{
    public class FlatFileService
    {
        #region Properties

        public readonly string PathSeparator = Path.DirectorySeparatorChar.ToString();

        const int ActionDelay = 50;

        public List<FlatFileEntry> Data { get; set; }

        #endregion Properties

        #region Methods

        public async Task<List<FlatFileEntry>> ReadFlatDataAsync()
        {
            await Task.Delay(ActionDelay);

            if (Data == null || Data.Count == 0)
            {
                Data = ReadCustomFlatData();
            }

            return Data;
        }

        public List<FlatFileEntry> ReadCustomFlatData()
        {
            var rootPath = Path.Combine(Directory.GetCurrentDirectory(), FileManagerConstants.ReadPath);
            var rootDirectory = new DirectoryInfo(rootPath);
            var rootDirectoryEntry = ConvertToFlatEntry(rootDirectory);
            var directories = rootDirectory.EnumerateDirectories();
            var files = rootDirectory.EnumerateFiles();

            var data = new List<FlatFileEntry>();

            foreach (var directory in directories)
            {
                var entries = ReadDirectory(directory);
                data.AddRange(entries);
            }

            foreach (var file in files)
            {
                var entry = ConvertToFlatEntry(file, rootDirectoryEntry);
                data.Add(entry);
            }

            return data;
        }

        public List<FlatFileEntry> ReadDirectory(DirectoryInfo directory, FlatFileEntry parentDirectory = null)
        {
            var directoryEntry = ConvertToFlatEntry(directory, parentDirectory);
            var directories = directory.EnumerateDirectories();
            var files = directory.EnumerateFiles();
            var data = new List<FlatFileEntry>();

            data.Add(directoryEntry);

            foreach (var dir in directories)
            {
                var entries = ReadDirectory(dir, directoryEntry);
                data.AddRange(entries);
            }

            foreach (var file in files)
            {
                var entry = ConvertToFlatEntry(file, directoryEntry);
                data.Add(entry);
            }

            return data;
        }

        public FlatFileEntry ConvertToFlatEntry(DirectoryInfo directory, FlatFileEntry parentDirectory = null)
        {
            var id = GetId(directory);

            var entry = new FlatFileEntry()
            {
                Name = directory.Name,
                IsDirectory = true,
                HasDirectories = directory.GetDirectories().Count() > 0,
                Id = id,
                ParentId = parentDirectory?.Id,
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

        public FlatFileEntry ConvertToFlatEntry(FileInfo file, FlatFileEntry parentDirectory)
        {
            var id = GetId(file);

            var entry = new FlatFileEntry()
            {
                Name = Path.GetFileNameWithoutExtension(file.FullName),
                IsDirectory = false,
                HasDirectories = false,
                Id = id,
                ParentId = parentDirectory?.Id,
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

        public string GetId(DirectoryInfo directory)
        {
            var id = directory.FullName.Substring(directory.FullName.IndexOf(FileManagerConstants.FilesPath));
            return id;
        }

        public string GetId(FileInfo file)
        {
            var id = file.FullName.Substring(file.FullName.IndexOf(FileManagerConstants.FilesPath));
            return id;
        }

        #endregion Methods

        #region Methods - CRUD

        public async Task CreateAsync(FlatFileEntry item, string parentPath)
        {
            await Task.Delay(ActionDelay);

            CreateFolderInFileSystem(parentPath, item);

            RefreshData();
        }

        public FlatFileEntry ModelInit(string parentPath)
        {
            var item = new FlatFileEntry();
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

        public async Task UpdateAsync(FlatFileEntry item, string parentPath)
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

            Data.RemoveAll(x => x.Id == item.Id);
            Data.Add(item);

            RefreshData();
        }

        public async Task DeleteAsync(FlatFileEntry item)
        {
            await Task.Delay(ActionDelay);

            var parentDirectory = GetParent(item);

            Data.Remove(item);

            if (parentDirectory != null)
            {
                parentDirectory.HasDirectories = Data.Count(x => x.IsDirectory && x.ParentId == parentDirectory.Id) > 0;
            }

            RefreshData();
        }

        private void RefreshData()
        {
            Data = new List<FlatFileEntry>(Data);
        }

        private FlatFileEntry GetParent(FlatFileEntry item)
        {
            var parentItem = Data
                .FirstOrDefault(x => x.IsDirectory && x.Id == item.ParentId);

            return parentItem;
        }

        private List<FlatFileEntry> GetParentChildren(FlatFileEntry item)
        {
            var items = Data.Where(x => x.Id == item.ParentId).ToList();
            return items;
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
            var directory = GetDirectory(path);

            var files = uploadFiles
                .Select(x => new FlatFileEntry()
                {
                    Id = Guid.NewGuid().ToString(),
                    ParentId = directory?.Id,
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

            var directoryItems = Data;

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                directoryItems.Add(file);
            }

            RefreshData();
        }

        #endregion Upload

        #region Rename

        private void Rename(FlatFileEntry item)
        {
            var parentDirectory = GetParent(item);
            var parentChildren = GetParentChildren(item);

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

        private FlatFileEntry CreateFolderInFileSystem(string path, FlatFileEntry entry)
        {
            var parentId = path;

            if (path.EndsWith(FileManagerConstants.FilesPath))
            {
                parentId = null;
            }

            var newFolderName = GenerateUniqueEntryName(entry);
            entry.Id = Path.Combine(path, newFolderName);
            entry.ParentId = parentId;
            entry.Name = newFolderName;
            entry.IsDirectory = true;
            entry.HasDirectories = false;
            entry.DateCreated = DateTime.Now;
            entry.DateCreatedUtc = DateTime.Now;
            entry.DateModified = DateTime.Now;
            entry.DateModifiedUtc = DateTime.Now;
            entry.Path = Path.Combine(path, newFolderName);
            entry.Extension = null;

            Data.Add(entry);

            var parentDirectory = GetDirectory(path);

            if (parentDirectory != null)
            {
                parentDirectory.HasDirectories = Data.Count(x => x.ParentId == parentDirectory.Id) > 0;
            }

            return entry;
        }

        private string GenerateUniqueEntryName(FlatFileEntry entry)
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
            var entry = GetDirectory(path);

            Data.Remove(entry);
        }

        #endregion Delete

        #region File processing

        private FlatFileEntry GetFile(FlatFileEntry parentDirectory, string fileName)
        {
            var path = parentDirectory?.Path;
            var file = GetFile(path, fileName);

            return file;
        }

        public FlatFileEntry GetFile(string path, string fileName)
        {
            var parentDirectory = GetDirectory(path);
            var fullPath = Path.Combine(path, fileName);
            var file = Data.FirstOrDefault(x => x.Path == fullPath);
            return file;
        }

        private FlatFileEntry GetDirectory(string path)
        {
            var directory = Data.FirstOrDefault(x => x.IsDirectory && x.Path == path);
            return directory;
        }

        public FlatFileEntry GetSubdirectory(FlatFileEntry parentDirectory, string subdirectoryName)
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
                var subdirectory = Data
                    .Where(x => x.ParentId == parentDirectory.Id)
                    .FirstOrDefault(x => x.IsDirectory && x.Name == subdirectoryName);

                return subdirectory;
            }
        }

        #endregion File processing

    }
}
