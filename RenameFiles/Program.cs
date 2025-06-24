using RenameFiles.Adapter;
using RenameFiles.Command;

namespace RenameFiles
{
    internal class Program
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">An array of command-line arguments passed to the application.</param>
        static void Main(string[] args)
        {
            PrintUsage();
            var processor = new CommandProcessor(new FileSystemProvider());
            processor.Process(args);
        }
        private static void PrintUsage()
        {
            string prefixInitializationText =
@"###############################################################################################################################################################
#                                                                                                                                                             #
#                                          ====================>  Application name :: RenameFile  <====================                                       #
#                                                                                                                                                             #
#                                                                                                                                                             #
#                                =>  Objective: organize files in a directory in a way that helps versioning, avoiding names that don't make any sense,       #
#                                    using createddate or lastmodifieddate                                                                                    #
#                                                                                                                                                             #
#                                                                                                                                                             #
#                                    Example: RenameFilesConsole.exe ""c:\_git"" ""modificateddate"" ""asc""                                                        #
#                                                                                                                                                             #
#                                                                                                                                                             #
                                    Options => modificateddate, createddate, numeric. Order by ""asc"" or ""desc""                                                #
#                                                                                                                                                             #
                                    ******** If you need to start with a specific number, after the path, add $number. Example: ""c:\_git$8""                   #
#                                                                                                                                                             #
#                                                                                                                                                             #
#                                                                                                                                                             #
#                                                           ==========> Author: Thomaz de Torres Bandeira <==========                                         #
#                                                            =>  Linkedin: https://www.linkedin.com/in/thomazbandeira/                                        #
#                                                                                                                                                             #
#                                                                                                                                                             #
#                                                                                                                                                             #
#                                                                                                                                                             #
###############################################################################################################################################################";
            Console.WriteLine(prefixInitializationText);
            Console.WriteLine("");
        }
    }
}
