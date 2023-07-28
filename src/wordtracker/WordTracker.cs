using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordTracker
{
    public class WordTracker
    {
        //The binary search tree that holds the words
        private BSTree<Word> tree;

        //The Constructor that creates a new binary search tree
        public WordTracker(){
            tree = new BSTree<Word>();
        }

        //The constructor that takes in a binary search tree
        public WordTracker(BSTree<Word> tree){
            this.tree = tree;
        }

        //Method that processes the file and adds the words to the binary search tree
        //@Param file -The file to be processed
        //@Param sortType - The type of sorting to be used for the output
        //@Param optionalOutput - Optional output flag
        //@Param outputFile = The Optional output file (Only used if the optional output flag is used)
        public void processFile(String filePath, int sortType, String? optionalOutput, String? outputFilePath){
            String fileName = Path.GetFileName(filePath);
            try
            {
                var lines = File.ReadLines(filePath);
                int lineNumber = 1;
                //Process each line in the file
                foreach (var line in lines)
                {
                    processLine(line, fileName, lineNumber);
                    lineNumber++;  
                }
                //Chooses the output method
                if (optionalOutput != null)
                {
                    //Prints the words in the binary tree to the output file
                    printToFile(sortType, outputFilePath);
                } else {
                    //Prints the words in the binary tree to the command line
                    printTree(sortType);
                }
            }
            catch (FileNotFoundException)
            {
                //Error message for file not found
                Console.WriteLine("File not found: " + fileName);
                throw new FileNotFoundException();
            }
            catch(IOException){
                //Erro message for error reading the file
                Console.WriteLine("Error reading file: " + filePath);
                throw new IOException();
            }
        }

        //Method the processes each line in the file
        //@Param line - The line to be processed
        //@Param fileName - The name of the file
        //@Param lineNumber - The line number of the line
        public void processLine(String line, String fileName, int lineNumber){
            //Split the line into a list of words
            String[] words = Regex.Split(line,"\\s");
            //Process each word in the line
            foreach (String word in words){
                //Conver the word to lower case
                String lowerWord = word.ToLower();
                //Create a new word object
                Word currentWord = new Word(lowerWord, fileName, lineNumber);

                //Add the word to the tree if the tree is empty
                if(tree.isEmpty()){
                    tree.add(currentWord);
                } else {
                    try
                    {
                        //Check if the word is already in the tree
                        if (tree.contains(currentWord))
                        {
                            try
                            {
                                //Get the node containing the word
                                BSTreeNode<Word> node = tree.search(currentWord);
                                //Add the location to the existing word
                                node.data.addLocation(fileName, lineNumber);
                            }
                            catch (TreeException)
                            {
                                //Error message for error adding locaiton to word
                                Console.WriteLine("Error adding location to word: "+  lowerWord);
                                throw;
                            }
                        } else {
                            //Add the word to the tree if it is not already in the tree
                            tree.add(currentWord);
                        }
                    }
                    catch (TreeException)
                    {
                        //Error message if the tree is empty
                        Console.WriteLine("Error, tree is empty");
                        throw;
                    }
                }
            }
        }

        //Method the prints the words in the binary tree to the command line
        //@Param sortType - The type of sorting to be used for the output
        //                  1 - Prints just the word and file its in based on -pf arg
        //                  2 - Prints word, line, and file its in based on -pl arg
        //                  3 - Prints word, line, frequency, and file its in based on -po arg
        public void printTree(int sortType){
            //Get the inorder traversal iterator for the tree
            Iterator<Word> iter = tree.inorderIterator();
            switch (sortType)
            {
                //Prints just word and file its in based on -pf arg //Needs loops
                case 1: {
                    while (iter.hasNext())
                    {
                        Console.WriteLine(iter.next().printBasicWordInfo());
                    }
                    break;
                }
                case 2: {
                    while (iter.hasNext())
                    {
                        Console.WriteLine(iter.next().printWordInfo());
                    }
                    break;
                }
                case 3: {
                    while (iter.hasNext())
                    {
                        Console.WriteLine(iter.next().printExtraWordInfo());
                    }
                    break;
                }
                default:{
                    Console.WriteLine("This should never occur");
                    throw new Exception();
                }
            }
        }

        //Method the prints the words in the binary tree to the command line
        //@Param sortType - The type of sorting to be used for the output
        //                  1 - Prints just the word and file its in based on -pf arg
        //                  2 - Prints word, line, and file its in based on -pl arg
        //                  3 - Prints word, line, frequency, and file its in based on -po arg
        //@Param outputFile - The output file to print the words to
        public void printToFile(int sortType, String outputFilePath){
            try
            {
                //Get the inorder traversal iterator for the tree
                Iterator<Word> iter = tree.inorderIterator();
                //StreamWriter to write to a file
                StreamWriter writer = new StreamWriter(outputFilePath);
                switch (sortType)
                {
                    //Prints just word and file its in based on -pf arg //Needs loops and proper access
                    case 1: {
                        while (iter.hasNext())
                        {
                            writer.WriteLine(iter.next().printBasicWordInfo());
                            writer.WriteLine("\n");
                        }
                        break;
                    }
                    case 2: {
                        while (iter.hasNext())
                        {
                            writer.WriteLine(iter.next().printWordInfo());
                            writer.WriteLine("\n");
                        }
                        break;
                    }
                    case 3: {
                        while (iter.hasNext())
                        {
                            writer.WriteLine(iter.next().printExtraWordInfo());
                            writer.WriteLine("\n");
                        }
                        break;
                    }
                    default:{
                        Console.WriteLine("This should never occur");
                        throw new Exception();
                    }
                }
            }
            catch (IOException)
            {
                throw new IOException("Error writing to file: " + outputFilePath);
            }
            
        }
    }
}