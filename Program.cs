using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ReversePhrase
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //variable to control the cycle
                ConsoleKeyInfo cki;

                do{
                    //read the user input
                    Console.Write("Insert a phrase: ");
                    string phrase = Console.ReadLine();

                    //call a method to reverse the phrase
                    Console.WriteLine("Reverse Phrase: " + ReturnReversePhrase(phrase.Trim()));
                    
                    //ask user if wants to keep trying
                    Console.Write("Try again -- [ [y] to continue / any key to exit ] ---> ");
                    cki = Console.ReadKey();// reads one char
                    Console.Write("\n \n");
                }while(cki.Key == ConsoleKey.Y); //keep working until user input is  not Y

            }
            catch (Exception ex)
            {
                //controll the exception sending a message
                Console.WriteLine(ex.Message, ex.GetType());
            }

            Console.Write("Thank you. Press any key to exit.");
            //wait until user presses any key
            Console.ReadKey(true);
        }

        /// <summary>
        /// Reverse a portion of the phrase
        /// </summary>
        /// <param name="phrase">A word</param>
        /// <returns>Word in reverse mode</returns>
        private static string ReversePhraseMethod(string phrase)
        {
            //obtain phrase length
            int len = phrase.Length;

            //obtain from the middle to the end
            string n = phrase.Substring(len/2);
            //obtain from beginning to the middle
            string n2 = phrase.Substring(0, len / 2);

            // apply recursion until length is equal 1
            //chars are getting it corresponding place in memory
            return len <= 1 ? phrase : (
                ReversePhraseMethod(n) +
                ReversePhraseMethod(n2)
            );
        }

        /// <summary>
        /// Substring the phrase to reverse it
        /// </summary>
        /// <param name="phrase">The Whole Phrase</param>
        /// <returns>The whole phrase in reverse mode</returns>
        private static string ReturnReversePhrase(string phrase)
        {
            //variable to save the input during changes
            string phraseReturn = string.Empty;
            //temp variable to cut phrase
            string temp = string.Empty;

            //cycle: it works as a split method.
            for (int i = phrase.Length-1; i >= 0; i--)
            {
                //keep running until find a WhiteSpace
                if (!char.IsWhiteSpace(phrase[i]))
                    temp += phrase[i].ToString(); //putting together chars until white space
                else
                {
                    phraseReturn += " " + ReversePhraseMethod(temp); //call this method to put in order chars
                    temp = string.Empty;//clear temp variable, so can work with next values
                }
            }
            // return the whole phrase, inverse the last word here.
            return phraseReturn + " " + ReversePhraseMethod(temp);
        }
    }
}
