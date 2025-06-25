using RenameFiles.Domain.Interface;
using RenameFiles.Domain.Model;
using RenameFiles.Util.String;

namespace RenameFiles.Adapter.Renamer
{
    /// <summary>
    /// Provides functionality to rename files by appending their last modified timestamp to their names.
    /// </summary>
    /// <remarks>This class implements the <see cref="IRenamer"/> interface and modifies file names by
    /// removing the last number from the original name, appending the file's last modified timestamp in the format
    /// "yyyyMMdd_HHmmss.fff", and preserving the original file extension.</remarks>
    public class ModifiedDateRenamer : IRenamer
    {
        /// <summary>
        /// Gets or sets the initial number used to configure the starting value for an operation or process.
        /// </summary>
        public int InitializeNumber { get; set; }
        /// <summary>
        /// Renames a collection of files by appending a timestamp to their names.
        /// </summary>
        /// <remarks>Each file's name is modified by removing the last number from its original name,
        /// appending the file's last modified timestamp  in the format "yyyyMMdd_HHmmss.fff", and preserving the
        /// original file extension.</remarks>
        /// <param name="files">A collection of <see cref="FileEntry"/> objects representing the files to be renamed. Cannot be null.</param>
        public void Rename(IEnumerable<FileEntry> files)
        {
            foreach (var file in files)
            {
                Tuple<string,string> nameComposition = file.OriginalName.RemoveLastNumberGetNameAndExtension();
                file.NewName =
                    string.Format("{0}_{1}{2}",
                    nameComposition.Item1,
                    file.LastModified.ToString("yyyyMMdd_HHmmss.fff"), nameComposition.Item2);
            }
        }
    }
}
