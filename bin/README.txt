Search16S User Manual

****
Before continuing, make sure that the .fasta file you wish to search follows the convention of:

NR_XXXXXX.X Sequence description
GGTCTNATACCGGATATAACAACTCATGGCATGGTTGGTAGTGGAAAGCTCCGGCGGTACGG
NR_YYYYYY.Y Sequence description
TGGGTCTNATACCGGATATGACAACTGATGGCATGGTTGGTTGTGGAAAGCTCCGGCAGCTC

Where NR_XXXXXX.X and NR_YYYYYY.Y are sequence ids, followed by their corresponding gene sequences.
Each sequence id MUST be a total of 11 chracters long, otherwise undesirable functionality may occur.
An example 16S.fasta file has been supplied as an example of what this program expects.
****

7 different levels of search functionality are provided within this program, and they can be used as follows:

SEARCH LEVEL 1
--------------
Command: -level1 filename.fasta lineNumber numberOfSequences
Functionality: Sequentially displays numberOfSequences entries of the filename.fasta file from the starting lineNumber.

SEARCH LEVEL 2
--------------
Command: -level2 filename.fasta NR_XXXXXX.X
Functionality: Sequentially searches filename.fasta for id NR_XXXXXX.X and displays its information & gene sequence.

SEARCH LEVEL 3
--------------
Command: -level3 filename.fasta queryFile.txt resultsFile.txt
Functionality: Sequentially searches filename.fasta for ids listed in queryFile.txt, and stores the output in resultsFile.txt.
               See the supplied query.txt file for an example of how to format the query file. This method is quite slow for many queries.

SEARCH LEVEL 4
--------------
Command: -level4 filename.fasta indexFile.index queryFile.txt resultsFile.txt
Functionality: Directly indexes the filename.fasta file using the supplied byte offsets in the indexFile.index. Like a Level 3 Search,
               query ids are supplied in queryFile.txt, and output is stored in resultsFile.txt. Be sure to index the .fasta file first
               by using the -index command. This method is the fastest for searching by sequence id.

INDEX FASTA FILE (required for Level 4 Search)
----------------
Command: -index filename.fasta indexFile.index
Funcitonality: THIS COMMAND IS REQUIRED BEFORE RUNNING A LEVEL 4 SEARCH.
               Indexes the filename.fasta with byte offsets for each line containing a sequence id. Output is stored in indexFile.index
               with the format: NR_XXXXXX.X BYTEOFFSET

SEARCH LEVEL 5
--------------
Command: -level5 filename.fasta geneSequence
Functionality: Performs a sequential search for the corresponding geneSequence, and outputs each sequence id that matches. Example geneSequence
               may be "GGTCTNATACCGG".

SEARCH LEVEL 6
--------------
Command: -level6 filename.fasta metaDataString
Functionality: Performs a sequential search for the corresponding metaDataString, and outputs each sequence id that matches.
               Example metaDataString may be "Streptomyces".

SEARCH LEVEL 7
--------------
Command: -level7 filename.fasta wildCardSequence
Functionality: Performs a sequential search for the corresponding wildCardSequence containing "wildcards" - '*'. Adding a wild card to the
               string means that any number of characters between either side of the wildcard are allowed in the search. For instance, using
               "ACTG*GTAC*CA" may return a sequence id with gene sequence "ACTGXXXXGTACXXXXXXXXXXXXXXCA". Use this for a more inexact search 
               of gene sequences. Returns each id that matches.

