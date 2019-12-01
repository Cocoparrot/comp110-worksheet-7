using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7
{
	public static class DirectoryUtils
	{
		// Return the size, in bytes, of the given file
		public static long GetFileSize(string filePath)
		{
			return new FileInfo(filePath).Length;
		}

		// Return true if the given path points to a directory, false if it points to a file
		public static bool IsDirectory(string path)
		{
			return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
		}

		// Return the total size, in bytes, of all the files below the given directory
		public static long GetTotalSize(string directory)
		{
            //declare a list for the files and a variable to store the size of the files
			String[] files;
            long size = 0;

            //populate the list with the files and do a foreach loop to store the size of every file in the list.
            files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            foreach(string file in files)
                {
                    size += GetFileSize(file);
                }
            return size;
		}

		// Return the number of files (not counting directories) below the given directory
		public static int CountFiles(string directory)
		{
            //list to store the files we find in the directory
			string[] files;

            //Populate the list with the files we find in the given directory
            files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            //This will return the number of files stored in the list
            return files.Length;
		}

		// Return the nesting depth of the given directory. A directory containing only files (no subdirectories) has a depth of 0.
		public static int GetDepth(string directory)
		{
			string[] depth;
            int depthValue = 0;

            depth = Directory.GetDirectories(directory);
            foreach (string dir in depth)
                {
                    depthValue++;
                }

            return depthValue;
		}

		// Get the path and size (in bytes) of the smallest file below the given directory
		public static Tuple<string, long> GetSmallestFile(string directory)
		{
            //decalre a list variable for all the files and a tuple where we will store the smallest filename and the size
			string[] files;
            Tuple<string, long> size;

            //populate the variables
            //the tuple with dummy information we will overwrite and the list with all the files we find in the given directory
            size = new Tuple<string, long>("file", long.MaxValue);
            files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            //select the smallest files from the list using Linq and store in in a variable smallessize
            //From the filename we got in the smallestFile string we can get the size using the GetFileSize function and store this information into a variable minimumSize
            string smallestFile = (from file in files let length = GetFileSize(file) where length > 0 orderby length ascending select file).First();
            long minimumSize = GetFileSize(smallestFile);
            
            //store the path of the file into a variable so we can use this to populate the tuple
            string minimumSizePath = (smallestFile);

            //populate the tuple with the path and size of the smallest file we have found.
            size = new Tuple<string, long>(minimumSizePath, minimumSize);
            return size;
		}

		// Get the path and size (in bytes) of the largest file below the given directory
		public static Tuple<string, long> GetLargestFile(string directory)
		{
			string[] files;
            Tuple<string, long> size;

            size = new Tuple<string, long>("file", long.MaxValue);
            files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);

            string biggestFile = (from file in files let length = GetFileSize(file) where length > 0 orderby length descending select file).First();
            long maximumSize = GetFileSize(biggestFile);

            string maximuSizePath = (biggestFile);

            size = new Tuple<string, long>(maximuSizePath,maximumSize);
            return size;

		}

		// Get all files whose size is equal to the given value (in bytes) below the given directory
		public static IEnumerable<string> GetFilesOfSize(string directory, long size)
		{
			string[] files;
            List<string>filesOfSize = new List<string>();
            files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            long fileSize;

            foreach(string file in files)
                {
                    fileSize = GetFileSize(file);
                    if (fileSize == size)
                    {
                        filesOfSize.Add(file);
                    }
                    else
                    {
                        
                    }
                }
            return filesOfSize;

		}
	}
}
