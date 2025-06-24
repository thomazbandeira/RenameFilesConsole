using RenameFiles.Domain.Model;

namespace RenameFiles.Domain.Interface
{
    /// <summary>
    /// Defines a mechanism for retrieving files from a specified path.
    /// </summary>
    /// <remarks>This interface provides a way to access file entries within a given path.  Implementations
    /// may vary in how files are retrieved, such as from a local file system,  a remote server, or an in-memory
    /// store.</remarks>
    public interface IFileProvider
    {
        /// <summary>
        /// Retrieves a collection of files from the specified directory path.
        /// </summary>
        /// <remarks>This method does not include subdirectories in the search. Only files directly within
        /// the specified directory are returned. Ensure the caller has the necessary permissions to access the
        /// directory.</remarks>
        /// <param name="path">The path to the directory from which to retrieve files. This must be a valid, non-null, and non-empty
        /// string.</param>
        /// <returns>An enumerable collection of <see cref="FileEntry"/> objects representing the files in the specified
        /// directory. If the directory is empty or does not contain any files, the returned collection will be empty.</returns>
        IEnumerable<FileEntry> GetFiles(string path);
    }
}
