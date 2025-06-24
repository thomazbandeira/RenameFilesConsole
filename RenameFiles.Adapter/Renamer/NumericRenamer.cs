using RenameFiles.Domain.Interface;
using RenameFiles.Domain.Model;
using RenameFiles.Util.String;

namespace RenameFiles.Adapter.Renamer
{
    /// <summary>
    /// Provides functionality to rename a collection of files by appending sequential numbers to their names.
    /// </summary>
    /// <remarks>The <see cref="NumericRenamer"/> class is designed to rename files by removing any trailing
    /// numbers from their original names,  appending a zero-padded sequential number, and preserving the file
    /// extension. The numbering starts from the value specified  in the <see cref="InitializeNumber"/> property. This
    /// class implements the <see cref="IRenamer"/> interface.</remarks>
    public class NumericRenamer : IRenamer
    {
        /// <summary>
        /// Gets or sets the initial number used to configure the starting value for an operation or process.
        /// </summary>
        public int InitializeNumber { get; set; } = 1;
        /// <summary>
        /// Renames a collection of files by appending a sequential number to their names.
        /// </summary>
        /// <remarks>The method generates new names by removing any trailing numbers from the original
        /// file names,  appending a zero-padded sequential number, and preserving the file extension. The numbering
        /// starts  from the value of the <c>InitializeNumber</c> field.</remarks>
        /// <param name="files">The collection of <see cref="FileEntry"/> objects to rename. Each file's new name will be assigned to its
        /// <c>NomeNovo</c> property.</param>
        public void Rename(IEnumerable<FileEntry> files)
        {
            int i = InitializeNumber;
            foreach (var file in files)
            {
                Tuple<string,string> nameComposition = file.OriginalName.RemoveLastNumberGetNameAndExtension();
                file.NewName = string.Format("{0}{1}{2}", nameComposition.Item1, (i++).ToString().PadLeft(5, '0'), nameComposition.Item2);
            }
        }
    }
}
