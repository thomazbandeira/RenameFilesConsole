using System.Text.RegularExpressions;

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
            FileInfo fileInfo = new FileInfo(name);

            int dotIdx = name.LastIndexOf('.');
            var baseName = dotIdx > 0 ? name.Substring(0, dotIdx) : name;
            var ext = fileInfo.Extension;//dotIdx > 0 ? name.Substring(dotIdx) : "";
            if (!Regex.IsMatch(baseName, @"(\d+\.\d+)$"))
                baseName = Regex.Replace(baseName, @"\s?\d+$", "");
            return new Tuple<string, string>(baseName.RemoveTrailingNumberInParentheses(), ext);

        }

        /// <summary>
        /// Removes a trailing numeric value enclosed in parentheses from the end of the specified string.
        /// </summary>
        /// <remarks>This method removes a numeric value enclosed in parentheses only if it appears at the
        /// end of the string. For example, "Example (123)" becomes "Example", while "Example (abc)" remains
        /// unchanged.</remarks>
        /// <param name="input">The input string to process. Can be null or empty.</param>
        /// <returns>A string with the trailing numeric value in parentheses removed, if present;  otherwise, the original string.</returns>
        public static string RemoveTrailingNumberInParentheses(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remove "(número)" do final da string, apenas se for só número
            return Regex.Replace(input, @"\s*\(\d+\)$", "");
        }
    }
}
