using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public class SimpleFileRepository : IFilesRepository
    {
        private ConcurrentDictionary<int, File> _files = new ConcurrentDictionary<int, File>();
        private int _idSequence = 0;
        private string _fileDirectoryName = "booksDemo_files";
        private object _lock = new object();

        public Task AddFile(File file, Stream data)
        {
            var newId = Interlocked.Increment(ref _idSequence);

            file.Id = newId;
            _files.TryAdd(newId, file);

            return SaveContents(file, data);
        }

        public void SetFileDirectory(string dir)
        {
            _fileDirectoryName = dir;
        }

        public Stream GetFileContents(int fileId)
        {
            _files.TryGetValue(fileId, out File file);
            if (file == null)
            {
                throw new ArgumentException($"File with id {fileId} not found!");
            }

            return ReadFile(file);
        }

        private Task SaveContents(File file, Stream data)
        {
            using (var stream = CreateFile(file))
            {
                return data.CopyToAsync(stream);
            }
        }

        private Stream ReadFile(File file)
        {
            var filePath = GetFilePath(file);
            return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, useAsync: true);
        }

        private Stream CreateFile(File file)
        {
            var filePath = GetFilePath(file);
            return new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
        }

        private string GetFilePath(File file)
        {
            CheckFileDirectory();
            var filename = GetFileName(file);
            return Path.Combine(_fileDirectoryName, filename);
        }

        private string GetFileName(File file)
        {
            return $"_{file.Id}";
        }

        private void CheckFileDirectory()
        {
            if (!Directory.Exists(_fileDirectoryName))
            {
                lock(_lock)
                {
                    if (!Directory.Exists(_fileDirectoryName))
                    {
                        Directory.CreateDirectory(_fileDirectoryName);
                    }
                }
            }
        }
    }
}
