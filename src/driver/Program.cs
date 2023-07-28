using System;
namespace WordTracker;

class Program
{
    static void Main(string[] args)
    {
        //Create a WordTracker object
        WordTracker tracker;

        //Load the binary search tree from the repoistory.ser file
        BSTree<Word> tree = FileIO.loadTree();

        //If the binary search tree is null, create a new WordTracker object
        //Otherwise, create a new WordTracker object with the binary search tree
        if(tree == null){
            tracker = new WordTracker();
        } else {
            tracker = new WordTracker(tree);
        }

        //Check for valid arguments
        try
        {
            String filePath;
            String infoLvl;
            int sortType;
            String? optionalOutputFlag = null;
            String? optionalFilePathOutput = null;
            filePath = args[0];
            infoLvl = args[1];

            //Check if there are optional arguments
            if(args.Length > 2){
                try
                {
                    optionalOutputFlag = args[2];
                    optionalFilePathOutput =  args[3];
                }
                catch (System.Exception)
                {
                    //Error message for invalid arguments
                    Console.WriteLine("Invalid argument");
                    throw new ArgumentException("File output arguments should be in the style of: -f <output.txt>");
                }
            }

            //Check for valid information level
            switch (infoLvl)
            {
                case "-pf" : {
                    sortType = 1;
                    break;
                }
                case "-pl" : {
                    sortType = 2;
                    break;
                }
                case "-po" : {
                    sortType = 3;
                    break;
                }
                default: {
                    //Error for invalid arguments
                    Console.WriteLine("Invalid argument");
                    throw new ArgumentException("Invalid argument: -pf, -pl, or -po is expected.");
                }
            }
            //Proces the file
            tracker.processFile(filePath, sortType, optionalOutputFlag, optionalFilePathOutput);
            //Save the binary search tree to the repository.dat file
            FileIO.saveTree(tree);
        }
        catch (System.Exception)
        {
            //Error message for invalid arguments
            throw new ArgumentException("Invalid arguments, please input in the style of '<RUN EXE TEXT HERE> <input.txt> -pf/-pl/-po -f <output.txt>'");
        }
    }
}
