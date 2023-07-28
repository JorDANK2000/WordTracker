using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WordTracker
{
    public class FileIO
    {
        //The file path of the repository.ser file
        private static String FILE_PATH = "tmp/repository.xml";

        //The error message for deserialization errors
        private static String ERROR_MESSAGE = "Error deserializaing the BSTree from the repository.xml file";

        //The error message for serialization errors
        private static String ERROR_MESSAGE_2 = "Error serializing the BSTree to the repository.xml file";

        //Method the loads the BSTree object from the repository.xml file
        // -- Calls the private loadTreeFromFile method
        // @Return the BSTree
        public static BSTree<Word> loadTree(){
            return  loadTreeFromFile();
        }
        
        //Method that loads the BSTree object from the repository.xml file
        //@Return the BSTree
        // -- Returns a new BSTree object if the repository.xml file does not exisit
        private static BSTree<Word> loadTreeFromFile(){
            BSTree<Word> tree = new BSTree<Word>();
            //Deserialize the BSTree object if the repository.xml file exisits
            if(File.Exists(FILE_PATH)){
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(BSTree<Word>));
                    using (Stream str = File.Open(FILE_PATH, FileMode.Open))
                    {
                        tree = (BSTree<Word>) ser.Deserialize(str);
                        Console.WriteLine("Tree deserialzied from file");
                    }
                }
                catch (System.Exception)
                {
                    Console.WriteLine(ERROR_MESSAGE);
                    throw new Exception();
                }
                
            } else {
                //Create a new WordTracker object if the repository.xml file does not exist
                tree = new BSTree<Word>();
            }
            return tree;
        }

        //Method that saves the BSTree object to the repository.xml file
        // -- Calls the private saveTreeToFile method
        //@Param tree - The BSTree object to be saved
        public static void saveTree(BSTree<Word> tree){
            saveTreeToFile(tree);
        }

        //Method that save the BSTree object to the repository.xml file
        //@Param tree - The BSTree object to be saved
        private static void saveTreeToFile(BSTree<Word> tree){
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(BSTree<Word>));
                using (Stream str = File.Create(FILE_PATH))
                {
                    ser.Serialize(str, tree);
                    Console.WriteLine("Tree serialized to file");
                }
            }
            catch (System.Exception e)
            {
                //Handle serialization error
                Console.WriteLine(ERROR_MESSAGE_2);
                Console.WriteLine(e.StackTrace);
                throw new Exception();
            }
        }
    }
}