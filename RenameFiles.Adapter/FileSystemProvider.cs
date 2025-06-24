using RenameFiles.Domain.Interface;
using RenameFiles.Domain.Model;

namespace RenameFiles.Adapter
{
    /// <summary>
    /// Provides functionality to retrieve file information from a specified directory path.
    /// </summary>
    /// <remarks>This class implements the <see cref="IFileProvider"/> interface to enumerate files in a
    /// directory and return their metadata, such as name, full path, creation date, and last modified date.</remarks>
    public class FileSystemProvider : IFileProvider
    {
        /// <summary>
        /// Retrieves a collection of file entries from the specified directory path.
        /// </summary>
        /// <remarks>Each <see cref="FileEntry"/> in the returned collection contains metadata about a
        /// file, including its original name, full path, last modified date, and creation date. Subdirectories are not
        /// included in the results.</remarks>
        /// <param name="path">The path of the directory to search for files. Must be a valid, accessible directory path.</param>
        /// <returns>An enumerable collection of <see cref="FileEntry"/> objects, each representing a file in the specified
        /// directory.</returns>
        public IEnumerable<FileEntry> GetFiles(string path)
        {
            return Directory.GetFiles(path)
                .Select(f => new FileEntry
                {
                    OriginalName = Path.GetFileName(f),
                    FullPath = f,
                    LastModified = File.GetLastWriteTime(f),
                    CreatedDate = File.GetCreationTime(f)
                });
        }
    }
}
