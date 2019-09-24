using System;
using System.IO;

namespace Search16s
{
    class Search16s
    {
        static void Main(string[] args)
        {
            // Check if there are at least 3 arguments supplied.
            if (args.Length < 3)
            {
                // Display error message if not.
                Console.WriteLine("\n\t<ERROR>\n\n\tPlease enter at least 3 arguments (inclduing the search level and file name) in the following format:");
                Console.WriteLine("\n\n\tSearch16s -searchLevel filename.fasta arg1 arg2 ...\n\n");
            }
            else
            {
                // Instantiate vairables to hold the search level and file name from the command line.
                string searchLevel, fileName;
                searchLevel = args[0];
                fileName = args[1];

                // Check if the provided file name exists in the binary's directory.
                if (File.Exists(fileName))
                {
                    // Check if there are exactly 2 arguments supplied.
                    if (searchLevel == "-index" && args.Length == 3)
                    {
                        string outFile = args[2];
                        Indexer indexer = new Indexer(fileName, outFile);
                        indexer.Index();
                    }
                    else {

                        // Create object of class Searchable, so search functions may be performed.
                        Searchable searchObj = new Searchable(fileName);

                        // Search level 1 requires 4 arguments in total.
                        if (searchLevel == "-level1" && args.Length == 4)
                        {
                            // Convert the the args for line number and number of output sequences into ints.
                            int lineNumber = Convert.ToInt32(args[2]);
                            int nSequences = Convert.ToInt32(args[3]);

                            // Perform a Level 1 search on the .fasta file with the desired starting line number and number of output sequences,
                            // offset array, and file stream.
                            searchObj.Level1Search(lineNumber, nSequences);
                        }
                        // Search level 2 requires 3 arguments in total.
                        else if (searchLevel == "-level2" && args.Length == 3)
                        {
                            // Hold the search ID from user input in a string vairable, and pass it into the Level2 search function.
                            string sequenceId = args[2];
                            searchObj.Level2Search(sequenceId);
                        }
                        // Search level 3 requires 4 arguments in total.
                        else if (searchLevel == "-level3" && args.Length == 4)
                        {
                            // Hold the file names from user input in string variables.
                            string inFile = args[2];
                            string outFile = args[3];

                            // Parse them into the Level3 search function.
                            searchObj.Level3Search(inFile, outFile);
                        }
                        else if (searchLevel == "-level4" && args.Length == 5)
                        {
                            string indexFile = args[2];
                            string inFile = args[3];
                            string outFile = args[4];

                            searchObj.Level4Search(indexFile, inFile, outFile);
                        }
                        else if (searchLevel == "-level5" && args.Length == 3)
                        {
                            string queryString = args[2];
                            searchObj.Level5Search(queryString);
                        }
                        else if(searchLevel == "-level6" && args.Length == 3)
                        {
                            string metaDataString = args[2];
                            searchObj.Level6Search(metaDataString);
                        }
                        else
                        {
                            // If a sufficient number of arguments is not found, display and error and exit the program.
                            Console.WriteLine("\n\t<ERROR>\n\n\tPlease enter the correct number of arguments for the desired search level.");
                            Console.WriteLine("\tRefer to the user manual for appropriate usage.\n\n");
                        }
                    }
                }
                else
                {
                    // If the supplied .fasta file does not exist, display and error and exit the program.
                    Console.WriteLine("\n\t<ERROR>\n\n\tFILE '{0}' DOES NOT EXIST.", fileName);
                    Console.ReadLine();
                }
            }
        }
    }
}