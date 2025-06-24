using RenameFiles.Domain.Model;

namespace RenameFiles.Domain.Interface
{
    /// <summary>
    /// Defines a method for sorting a collection of file entries.
    /// </summary>
    /// <remarks>Implementations of this interface provide functionality to sort a collection of <see
    /// cref="FileEntry"> objects based on specified criteria, such as ascending or descending order.</remarks>
    public interface IFileSorter
    {
        /// <summary>
        /// Sorts a collection of <see cref="FileEntry"/> objects by their file names.
        /// </summary>
        /// <param name="files">The collection of <see cref="FileEntry"/> objects to sort. Cannot be null.</param>
        /// <param name="ascending">A value indicating whether the sorting should be in ascending order.  <see langword="true"/> for ascending
        /// order; otherwise, <see langword="false"/> for descending order.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the sorted <see cref="FileEntry"/> objects. The order of the
        /// collection is determined by the <paramref name="ascending"/> parameter.</returns>
        IEnumerable<FileEntry> Sort(IEnumerable<FileEntry> files, bool ascending);
    }
}
