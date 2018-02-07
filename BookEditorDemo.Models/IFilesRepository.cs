using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public interface IFilesRepository
    {
        Task AddFile(File file, Stream data);

        Stream GetFileContents(int fileId);
    }
}
