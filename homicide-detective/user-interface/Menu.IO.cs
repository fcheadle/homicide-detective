using System;
using System.IO;

namespace homicide_detective
{
    public class IO
    {
        string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";

        //these get input from a file instead of the console
        public class Input
        {
            public virtual string Get()
            {
                string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";

                TextReader textReaderOriginal = Console.In;

                //set console.in to read from the file
                FileStream fileStream = new FileStream(saveFolder + "test.txt", FileMode.Open);
                StreamReader streamReader = new StreamReader(fileStream);
                Console.SetIn(streamReader);

                //get test string from the file
                string outcome = Console.ReadLine();

                //return console.in to the console and close the file
                Console.SetIn(textReaderOriginal);
                streamReader.Close();

                return outcome;
            }
        }

        public class Output
        {
            public virtual void Send(string output)
            {
                string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";

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

            public virtual void SendLine(string output)
            {
                string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";

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

            public virtual void SendLine(string output, string first)
            {
                string saveFolder = Directory.GetCurrentDirectory() + @"\saves\";

                TextWriter textWriterOriginal = Console.Out;

                //Set console.out to write to the file instead of the console
                FileStream fileStream = new FileStream(saveFolder + "Test.txt", FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fileStream);
                Console.SetOut(streamWriter);

                //Write test string to the file
                Console.WriteLine(output, first);

                //Return the console output to the console and close the file
                Console.SetOut(textWriterOriginal);
                streamWriter.Close();
            }
        }
    }
}
