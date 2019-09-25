using System;
using System.IO;
using System.Collections.Generic;

namespace FastaSearch
{
    // Class used for indexing a given .fasta file.
    class Indexer
    {
        // Private FileStreams for both the output .index file, and input .fasta file.
        private FileStream outStream;
        private FileStream inStream;
        private string inFile, outFile;

        // Pass desired file names in by constructor.
        public Indexer(string inFile, string outFile)
        {
            this.inFile = inFile;
            this.outFile = outFile;
        }

        // Index()
        // Indexes each sequence id line by finding the byte-offset of each line,
        // and storing them in a .index file.
        public void Index()
        {
            // Open the .fasta input file for reading, create the output file, and open it for writing.
            Console.WriteLine("Indexing \"{0}\"...", inFile);
            inStream = new FileStream(inFile, FileMode.Open);

            File.Create(outFile).Dispose();
            outStream = new FileStream(outFile, FileMode.Open);

            // Seek to the beginning of the stream (start of file, with byte offset 0).
            inStream.Seek(0, SeekOrigin.Begin);

            // Begin reading/writing the files.
            StreamReader reader = new StreamReader(inStream);
            StreamWriter writer = new StreamWriter(outStream);

            string line; // Holds the current line as a string.
            long position = 0; // Holds the current byte offset.

            // While end of file hasn't been reached.
            while ((line = reader.ReadLine()) != null)
            {
                // If the current line contains a sequence-id (all of which start with '>')...
                if (line.Contains(">"))
                {
                    // Get each entry in the line, split by the '>' character.
                    string[] entries = line.Split('>');
                    long temp = 0; // Long used for temporarily holding the current byte offset, it there is more than 1 id in the line.
                    for (int i = 1; i < entries.Length; i++)
                    {
                        // Get each entry separatley. 
                        // The first entry is always empty, so start at i = 1.
                        string str = entries[i];
                        if (i == 1)
                        {
                            // Store current byte position in case there is more than 1 entry.
                            temp = position;

                            // Write the sequence id and corresponding byte position to the output .index file.
                            // First 11 characters of the entry will always contain the sequence-id.
                            writer.WriteLine(str.Substring(0, 11) + " " + position);

                            // Add the length of the line to the byte offset.
                            position += line.Length + 1;
                        }
                        else
                        {
                            // If the entry is the second, third or nth in the line, add the same byte offset (stored in temp)
                            // to each entry. This means that search for any of these ids will result in the same output.
                            writer.WriteLine(str.Substring(0, 11) + " " + temp);
                        }
                    }
                }
                else
                {
                    // If the line is a gene sequence, add the line length and perform no actions on it.
                    position += line.Length + 1;
                }
            }

            // After entire file has been looped, close the FileStreams.
            inStream.Close();
            outStream.Close();

            Console.WriteLine("\n{0} indices saved in {1}.", inFile, outFile);
        }
    }


}
