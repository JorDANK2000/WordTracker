using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordTracker
{
    [Serializable]
    public class Word
    {
        /**
        * The word itself
        * {get;} The getter for the word
        */
        private String word{get;}

        /**
        * The locations of the word's appearances
        * (list of FileInfo objects that hold the file name and line number)
        * {get;} The getter for the locations list
        */
        private List<FileInfo> locations{get;}

        /**
        * Constructor that takes in the word and the file name and line number
        * @param word - the word
        * @param fileName - the file name
        * @param line - the line number
        */
        public Word(String word, String fileName, int line){
            //Set the word
            this.word = word;
            //Creates a new list of FileInfo objects
            locations = new List<FileInfo>();
            //Add the file name and line number to the locations list
            addLocation(fileName, line);
        }
        /**
        * Method that adds the file name and line number to the locations list
        * @param fileName - the file name
        * @param line - the line number
        */
        public void addLocation(String fileName, int line){
            //Create a new FileInfo object and add it to the locations lsit
            locations.Add(new FileInfo(fileName, line));
        }

        /**
        * Method that checks if the word is equal to another word
        *           (used for the Compare method)
        * @param obj - the other word
        *           (the word that is being compared to)
        * @return - true if the words are equal
        *        - false if the words are not equal
        *        - false if the object is null or not a Word object
        */
        public Boolean equals(Object obj){
            //Check if the object is null of if the object is not a Word object
            if(obj == null){
                return false;
            }
            //Cast the object to a Word object
            Word other = (Word) obj;
            //Check if the words are equal
            return word.Equals(other.word);
        }

        /**
        * Method that returns the word as a string
        * @return - the word
        *       (used for printing the word)
        */
        public override string ToString()
        {
            return word;
        }

        /**
        * Method that prints the word and the locations of the word if the -pf flag is used
        * @return - the word and the file its in
        */
        public String printBasicWordInfo(){
            StringBuilder sb = new StringBuilder();
            sb.Append(word).Append(":\n");
            foreach (FileInfo location in locations)
            {
                sb.Append("\t" + location.fileName + "\n");
            }
            sb.Append("------------------");
            return sb.ToString();
        }

        /**
        * Method that prints the word and the locations of the word if the -pl flag is used
        * @return - the word, line and file its in
        */
        public String printWordInfo(){
            StringBuilder sb = new StringBuilder();
            sb.Append(word).Append(":\n");
            foreach (FileInfo location in locations)
            {
                sb.Append("\t" + "Line " + location.lineNumber + " : " + location.fileName + "\n");
            }
            sb.Append("------------------");
            return sb.ToString();
        }

        /**
        * Method that prints the word and the locations of the word if the -po flag is used
        * @return - the word, line, frequency and the file its in
        */
        public String printExtraWordInfo(){
            StringBuilder sb = new StringBuilder();
            sb.Append(word).Append(":\n");
            int count = 0;
            foreach (FileInfo location in locations)
            {
                sb.Append("\t" + "Line " + location.lineNumber + " : " + location.fileName + "\n");
                count++;
            }
            sb.Append("Word Frequency: " + count + "\n");
            sb.Append("------------------");
            return sb.ToString();
        }
    }


    /**
    * Class that holds the file name and line number
    *      (used for the locations list)
    */
    public class FileInfo{
        /**
        * The file name
        * {get;} The getter for the fileName
        */
        public String fileName{get;}

        /**
        * The line number
        * {get;} The getter for the lineNumber
        */
        public int lineNumber{get;}

        /**
        * Constructor that takes in the file name and line number
        * @param fileName - the file name
        * @param lineNumber - the line number
        */
        public FileInfo(String fileName, int lineNumber){
            this.fileName = fileName;
            this.lineNumber = lineNumber;
        }

        /**
        * Method that returns the file name and line number as a string
        * @return - the file name and line number
        */
        public override String ToString(){
            return lineNumber + " : " + fileName;
        }
    }
}