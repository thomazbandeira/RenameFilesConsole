using RenameFiles.Domain.Model;

namespace RenameFiles.Domain.Interface
{
    /// <summary>
    /// Defines a contract for renaming a collection of files with customizable behavior.
    /// </summary>
    /// <remarks>Implementations of this interface should provide specific logic for renaming files based on
    /// the provided collection of <see cref="FileEntry"/> objects. The behavior of the renaming process may depend on
    /// the value of the <see cref="InitializeNumber"/> property.</remarks>
    public interface IRenamer
    {
        /// <summary>
        /// Gets or sets the initial number used to configure the starting value for a process or calculation.
        /// </summary>
       int InitializeNumber { get; set; }
        /// <summary>
        /// Renames the specified collection of files.
        /// </summary>
        /// <remarks>This method processes each file in the provided collection and applies the renaming
        /// logic.  Ensure that the <see cref="FileEntry"/> objects contain the necessary metadata for
        /// renaming.</remarks>
        /// <param name="files">A collection of <see cref="FileEntry"/> objects representing the files to be renamed.  Each file must have a
        /// valid name and path. The collection cannot be null or empty.</param>
        void Rename(IEnumerable<FileEntry> files);
    }
}
