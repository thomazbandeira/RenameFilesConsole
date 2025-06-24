using RenameFiles.Domain.Interface;
using RenameFiles.Domain.Model;

namespace RenameFiles.Adapter.Sorter
{
    /// <summary>
    /// Provides functionality to sort a collection of files by their last modified date.
    /// </summary>
    /// <remarks>This class implements the <see cref="IFileSorter"/> interface to sort files based on their 
    /// <see cref="FileEntry.LastModified"/> property. The sorting order can be specified as ascending or
    /// descending.</remarks>
    public class ModifiedDateFileSorter : IFileSorter
    {
        /// <summary>
        /// Sorts a collection of <see cref="FileEntry"/> objects by their last modified date.
        /// </summary>
        /// <param name="files">The collection of <see cref="FileEntry"/> objects to sort.</param>
        /// <param name="ascending">A value indicating whether to sort the collection in ascending order.  <see langword="true"/> to sort in
        /// ascending order; otherwise, <see langword="false"/> to sort in descending order.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the sorted <see cref="FileEntry"/> objects.</returns>
        public IEnumerable<FileEntry> Sort(IEnumerable<FileEntry> files, bool ascending)
        {
            return ascending
                ? files.OrderBy(f => f.LastModified)
                : files.OrderByDescending(f => f.LastModified);
        }
    }
}
