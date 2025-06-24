namespace RenameFiles.Domain.Model
{
    // Model for file info and new name
    public class FileEntry
    {
        /// <summary>
        /// Gets or sets the original name associated with the entity.
        /// </summary>
        public string OriginalName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the full path to a file or directory.
        /// </summary>
        public string FullPath { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the date and time when the object was last modified.
        /// </summary>
        public DateTime LastModified { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Gets or sets the new name associated with the entity.
        /// </summary>
        public string NewName { get; set; } = string.Empty;
    }
}
