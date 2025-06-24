using RenameFiles.Domain.Interface;
using RenameFiles.Domain.Model;

namespace RenameFiles.Adapter.Sorter
{
    /// <summary>
    /// Provides functionality to sort a collection of files by their creation date.
    /// </summary>
    /// <remarks>This class implements the <see cref="IFileSorter"/> interface to sort files based on their 
    /// <see cref="FileEntry.CreatedDate"/> property. The sorting order can be specified as ascending or
    /// descending.</remarks>
    public class CreatedDateFileSorter : IFileSorter
    {
        /// <summary>
        /// Sorts a collection of <see cref="FileEntry"/> objects by their creation date.
        /// </summary>
        /// <param name="files">The collection of <see cref="FileEntry"/> objects to sort. Cannot be null.</param>
        /// <param name="ascending">A boolean value indicating the sort order.  <see langword="true"/> to sort in ascending order; <see
        /// langword="false"/> to sort in descending order.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="FileEntry"/> objects sorted by their creation date.</returns>
        public IEnumerable<FileEntry> Sort(IEnumerable<FileEntry> files, bool ascending)
        {
            return ascending
                ? files.OrderBy(f => f.CreatedDate)
                : files.OrderByDescending(f => f.CreatedDate);
        }
    }
}
