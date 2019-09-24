using System;
using System.IO;
using System.Collections.Generic;

namespace Search16s
{
    class Indexer
    {
        private FileStream outStream;
        private FileStream inStream;
        private string inFile, outFile;

        public Indexer(string inFile, string outFile)
        {
            this.inFile = inFile;
            this.outFile = outFile;
        }

        public void Index()
        {
            Console.WriteLine("INDEXING");
            inStream = new FileStream(inFile, FileMode.Open);

            File.Create(outFile).Dispose();
            outStream = new FileStream(outFile, FileMode.Open);

            // Seek to the beginning of the stream (start of file, with byte offset 0).
            inStream.Seek(0, SeekOrigin.Begin);

            // Begin reading the file.
            StreamReader reader = new StreamReader(inStream);
            StreamWriter writer = new StreamWriter(outStream);

            string line; // Holds the current line as a string.
            long position = 0;

            // While end of file hasn't been reached.
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(">"))
                {
                    string[] entries = line.Split('>');
                    long temp = 0;
                    for(int i = 1; i < entries.Length; i++)
                    {
                        string str = entries[i];
                        if (str.Length > 0)
                        {
                            if (i == 1)
                            {
                                temp = position;
                                writer.WriteLine(str.Substring(0, 11) + " " + position);
                                position += line.Length + 1;
                            }
                            else
                            {
                                writer.WriteLine(str.Substring(0, 11) + " " + temp);
                            }
                        }
                    }
                }
                else
                {
                    position += line.Length + 1;
                }
            }

            inStream.Close();
            outStream.Close();

            Console.WriteLine("\n{0} indices saved in {1}.", inFile, outFile);
        }
    }


}
