using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using homicide_detective;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

namespace unit_tests
{
    [TestClass]
    public class MenuTests
    {
        //system variables
        static string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";
        static string personFolder = Directory.GetCurrentDirectory() + @"\objects\person";
        static string itemFolder = Directory.GetCurrentDirectory() + @"\objects\item";
        static string sceneFolder = Directory.GetCurrentDirectory() + @"\objects\scene";

        string[] personPaths = Directory.GetFiles(personFolder);
        string[] itemPaths = Directory.GetFiles(itemFolder);
        string[] scenePaths = Directory.GetFiles(sceneFolder);
        TestIO io = new TestIO();

        //these is to get input from a file instead of the console
        public class TestIO : Menu.IO
        {
            public override string Get()
            {
                TextReader textReaderOriginal = Console.In;
                
                //set console.in to read from the file
                FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Open);
                StreamReader streamReader = new StreamReader(fileStream);
                Console.SetIn(streamReader);

                //get test string from the file
                string outcome = Console.ReadLine();

                //return console.in to the console and close the file
                Console.SetIn(textReaderOriginal);
                streamReader.Close();

                return outcome;
            }

            public override void Send(string output)
            {
                TextWriter textWriterOriginal = Console.Out;

                //Set console.out to write to the file instead of the console
                FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                Console.SetOut(streamWriter);

                //Write test string to the file
                Console.Write(output);

                //Return the console output to the console and close the file
                Console.SetOut(textWriterOriginal);
                streamWriter.Close();
            }

            public override void SendLine(string output)
            {
                TextWriter textWriterOriginal = Console.Out;

                //Set console.out to write to the file instead of the console
                FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                Console.SetOut(streamWriter);

                //Write test string to the file
                Console.WriteLine(output);

                //Return the console output to the console and close the file
                Console.SetOut(textWriterOriginal);
                streamWriter.Close();
            }

            public override void SendLine(string output, string detective)
            {
                TextWriter textWriterOriginal = Console.Out;

                //Set console.out to write to the file instead of the console
                FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                Console.SetOut(streamWriter);

                //Write test string to the file
                Console.WriteLine(output, detective);

                //Return the console output to the console and close the file
                Console.SetOut(textWriterOriginal);
                streamWriter.Close();
            }
        }

        #region file stream example
        /*
        [TestMethod]
        public void SetOutTest()
        {
            //The new textwriter and reader
            TextWriter textWriter;// = new StreamWriter(saveFolder + "test.txt");
            TextReader textReader;// = new StreamReader(saveFolder + "test.txt");

            //save the original text writer and reader
            TextWriter textWriterOriginal = Console.Out;// = new StreamWriter(saveFolder + "test.txt");
            TextReader textReaderOriginal = Console.In;// = new StreamReader(saveFolder + "test.txt");

            //Set console.out to write to the file instead of the console
            FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            Console.SetOut(streamWriter);

            //Write test string to the file
            Console.WriteLine("this is a test");

            //Return the console output to the console and close the file
            Console.SetOut(textWriterOriginal);
            streamWriter.Close();

            //set console.in to read from the file
            fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Open);
            StreamReader streamReader = new StreamReader(fileStream);
            Console.SetIn(streamReader);

            //get test string from the file
            string outcome = Console.ReadLine();

            //return console.in to the console and close the file
            Console.SetIn(textReaderOriginal);
            streamReader.Close();

            //and test
            Assert.AreEqual("this is a test",outcome);
        }
        */
        #endregion

        [TestMethod]
        public void IOTest()
        {
            io.Send("abcdefg");
            Assert.AreEqual("abcdefg", io.Get());
        }

        [TestMethod]
        public void EvaluateMainMenuCommandTest()
        {
            throw new NotImplementedException();

            Game game = new Game();
            game = Menu.EvaluateMainMenuCommand("new", game);
            Assert.AreEqual("What is your name, Detective?", game.detective);
        }

        [TestMethod]
        public void PrintMainMenuCommandsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PrintTitleTest()
        {
            throw new NotImplementedException();

        }

        [TestMethod]
        public void CaseMenuTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void EvaluateCaseCommandTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CreateCaseIfNullTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PrintCaseMenuTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void PrintCaseSynopsisTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CheatTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CrimeScenemenuTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void WitnessDialogueTest()
        {
            throw new NotImplementedException();
        }
    }
}
