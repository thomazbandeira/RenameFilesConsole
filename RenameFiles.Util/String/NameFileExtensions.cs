namespace RenameFiles.Util.String
{
    public static class NameFileExtensions
    {
        /// <summary>
        /// Removes the trailing numeric sequence from the base name of a file and returns the modified name along with
        /// its extension.
        /// </summary>
        /// <remarks>This method assumes that the file name may optionally end with a numeric sequence,
        /// which will be removed if present. The extension is preserved unless the base name ends with a numeric
        /// sequence and no extension exists.</remarks>
        /// <param name="name">The full name of the file, including its extension.</param>
        /// <returns>A tuple containing two elements: <list type="bullet"> <item> <description>The base name of the file with the
        /// trailing numeric sequence removed.</description> </item> <item> <description>The file extension, or an empty
        /// string if no extension is present or if the base name does not end with a numeric sequence.</description>
        /// </item> </list></returns>
        public static Tuple<string, string> RemoveLastNumberGetNameAndExtension(this string name)
        {
            int dotIdx = name.LastIndexOf('.');
            var baseName = dotIdx > 0 ? name.Substring(0, dotIdx) : name;
            var ext = dotIdx > 0 ? name.Substring(dotIdx) : "";
            var digits = baseName.Reverse().TakeWhile(char.IsDigit).Count();
            return new Tuple<string, string>(baseName.Substring(0, baseName.Length - digits), digits > 0 ? ext : "");

        }
    }
}
