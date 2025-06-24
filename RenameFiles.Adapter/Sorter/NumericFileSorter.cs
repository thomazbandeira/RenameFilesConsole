using RenameFiles.Domain.Interface;
using RenameFiles.Domain.Model;

namespace RenameFiles.Adapter.Sorter
{
    /// <summary>
    /// Provides functionality to sort a collection of files based on numeric values extracted from their names.
    /// </summary>
    /// <remarks>This class implements the <see cref="IFileSorter"/> interface and sorts files by extracting
    /// the last numeric sequence from their names. The sorting can be performed in either ascending or descending
    /// order.</remarks>
    public class NumericFileSorter : IFileSorter
    {
        /// <summary>
        /// Sorts a collection of <see cref="FileEntry"/> objects based on the numeric value extracted  from the file
        /// name, in either ascending or descending order.
        /// </summary>
        /// <remarks>The numeric value is extracted from the file name by identifying the last sequence of
        /// digits  before the file extension. If no numeric value is found, a default value of 0 is used for
        /// sorting.</remarks>
        /// <param name="files">The collection of <see cref="FileEntry"/> objects to sort.</param>
        /// <param name="ascending">A boolean value indicating the sort order.  <see langword="true"/> to sort in ascending order; otherwise,
        /// <see langword="false"/> to sort in descending order.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="FileEntry"/> objects sorted by the extracted numeric value.</returns>
        public IEnumerable<FileEntry> Sort(IEnumerable<FileEntry> files, bool ascending)
        {
            int ExtractLastNumber(string name)
            {
                var parts = name.Split('.');
                var numberPart = parts.Length > 1 ? parts[^2] : parts[0];
                var digits = new string(numberPart.Reverse().TakeWhile(char.IsDigit).Reverse().ToArray());
                return int.TryParse(digits, out var n) ? n : 0;
            }

            return ascending
                ? files.OrderBy(f => ExtractLastNumber(f.OriginalName))
                : files.OrderByDescending(f => ExtractLastNumber(f.OriginalName));
        }
    }
}
