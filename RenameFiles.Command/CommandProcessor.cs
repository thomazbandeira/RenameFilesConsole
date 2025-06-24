using RenameFiles.Adapter.Renamer;
using RenameFiles.Adapter.Sorter;
using RenameFiles.Domain.Interface;

namespace RenameFiles.Command
{
    // Command parser and orchestrator
    public class CommandProcessor
    {
        /// <summary>
        /// If return null, invalid path.
        /// If false, path is valid but directory does not exist.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool? IsValidOrDirectoryExist(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            // Verifica se contém caracteres inválidos
            if (path.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                return null;

            // Verifica se o diretório existe
            return Directory.Exists(path);
        }
        /// <summary>
        /// Provides access to file system resources through the specified file provider.
        /// </summary>
        /// <remarks>This field is used to interact with the underlying file system or virtual file system
        /// via the <see cref="IFileProvider"/> interface. It is intended for internal use only and should not be
        /// accessed directly by external consumers.</remarks>
        private readonly IFileProvider _fileProvider;
        /// <summary>
        /// A collection of file sorters, indexed by their associated file type or key.
        /// </summary>
        /// <remarks>This dictionary maps string keys to implementations of the <see cref="IFileSorter"/>
        /// interface. It is used to retrieve the appropriate file sorter based on the file type or other identifying
        /// key.</remarks>
        private readonly Dictionary<string, IFileSorter> _sorters;
        /// <summary>
        /// A collection of renamers, indexed by their associated string keys.
        /// </summary>
        /// <remarks>This dictionary maps string keys to implementations of the <see cref="IRenamer"/>
        /// interface. It is used to manage and retrieve renamers based on their unique identifiers.</remarks>
        private readonly Dictionary<string, IRenamer> _renamers;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandProcessor"/> class with the specified file provider.
        /// </summary>
        /// <remarks>The <see cref="CommandProcessor"/> class is responsible for processing commands
        /// related to file operations. It initializes a set of predefined file sorters and renamers, which can be used
        /// to perform operations such as sorting or renaming files based on specific criteria (e.g., numeric order,
        /// modification date, or creation date).</remarks>
        /// <param name="fileProvider">An implementation of <see cref="IFileProvider"/> used to access and manage files.</param>
        public CommandProcessor(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
            _sorters = new()
            {
                { "numeric", new NumericFileSorter() },
                { "modificateddate", new ModifiedDateFileSorter() },
                { "createddate", new CreatedDateFileSorter() }
            };
            _renamers = new()
            {
                { "numeric", new NumericRenamer() },
                { "modificateddate", new ModifiedDateRenamer() },
                { "createddate", new CreatedDateRenamer() },
            };
        }
        /// <summary>
        /// Processes a set of files in a specified directory based on sorting and renaming options.
        /// </summary>
        /// <remarks>This method validates the provided directory path and ensures the specified sorting
        /// mode and order are supported.  If the directory is invalid or the arguments are incorrect, an appropriate
        /// message is displayed, and the operation is aborted.  If renaming is enabled, files are renamed based on the
        /// specified mode and starting number. Renamed files are moved to a subdirectory named <c>"renamed"</c> within
        /// the original directory.</remarks>
        /// <param name="args">An array of arguments specifying the operation details: <list type="bullet"> <item>
        /// <description><c>args[0]</c>: The directory path, optionally followed by a starting number for renaming,
        /// separated by a <c>'$'</c> character (e.g., <c>"C:\Files$100"</c>).</description> </item> <item>
        /// <description><c>args[1]</c>: The sorting mode, either <c>"numeric"</c> or
        /// <c>"modificateddate"</c>.</description> </item> <item> <description><c>args[2]</c>: The sorting order,
        /// either <c>"asc"</c> for ascending or <c>"desc"</c> for descending.</description> </item> <item>
        /// <description><c>args[3]</c> (optional): A flag indicating whether to rename the files (<c>"true"</c>,
        /// <c>"yes"</c>, or <c>"1"</c> to enable renaming).</description> </item> </list></param>
        public void Process(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Wrong args... Please re-run this app with another option.");
                Console.WriteLine("Usage: <path> <numeric|modificateddate> <asc|desc>");
                return;
            }
            string[] pathComposition = args[0].Split('$');
            string path = pathComposition[0];
            string mode = (args[1] ?? "").ToLowerInvariant().Trim();
            string order = (args[2] ?? "").ToLowerInvariant().Trim();
            string realRename = "";
            if (args.Length > 3)
                realRename = (args[3] ?? "".ToLowerInvariant()).Trim();

            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(mode) || string.IsNullOrWhiteSpace(order))
            {
                Console.WriteLine("Invalid arguments. Please provide a valid path, mode, and order.");
                return;
            }

            int initializeWith = 0;
            bool renameFile = false;
            if (pathComposition.Length > 1)
            {
                int.TryParse((pathComposition[1] ?? "").Trim(), out initializeWith);
            }
            if (args.Length > 3)
            {
                renameFile = realRename.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                              realRename.Equals("yes", StringComparison.OrdinalIgnoreCase) ||
                              realRename.Equals("1", StringComparison.OrdinalIgnoreCase);
            }
            var validateDirectory = IsValidOrDirectoryExist(path);
            if (!validateDirectory.HasValue)
            {
                Console.WriteLine("Invalid path. Please provide a valid directory path.");
                return;
            }
            else if (!validateDirectory.Value)
            {
                Console.WriteLine("Directory does not exist. Please provide an existing directory path.");
                return;
            }
            if (!_sorters.ContainsKey(mode) || !_renamers.ContainsKey(mode))
            {
                Console.WriteLine("Invalid mode. Use 'numeric' or 'modificateddate' or 'createddate'.");
                return;
            }

            bool ascending = order.Equals("asc", StringComparison.OrdinalIgnoreCase);
            var files = _fileProvider.GetFiles(path).ToList();
            var sorted = _sorters[mode].Sort(files, ascending).ToList();
            _renamers[mode].InitializeNumber = initializeWith;
            _renamers[mode].Rename(sorted);
            if (!renameFile)
            {
                Console.WriteLine("################################## Simulation ##################################");
            }
            foreach (var file in sorted)
            {
                Console.WriteLine($"Original: {file.OriginalName} -> NewName: {file.NewName}");
                if (renameFile)
                {
                    Console.WriteLine("renaming ...");
                    FileInfo fileInfo = new FileInfo(file.FullPath);
                    DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(fileInfo.DirectoryName, "renamed"));
                    if (!directoryInfo.Exists)
                    {
                        Console.WriteLine($"Directory {directoryInfo.FullName} does not exist. Creating it.");
                        directoryInfo.Create();
                    }

                    fileInfo.MoveTo(Path.Combine(fileInfo.DirectoryName, "renamed", file.NewName));
                }
            }
        }
    }
}
