/*      Developed by Chris Pratt   
 *      February 17th 2023
 *      
 *      Developed for Quadax
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    internal class MainFrame
    {
        int wins, losses, tries, numCount;
        bool success;        

        // The starting point of the system
        // handles the start of the game and the logic for repeated attempts
        public void run()
        {
            wins = 0;
            losses = 0;
            tries = 10;
            numCount = 4;            

            while (true)
            {
                Console.WriteLine("Welcome to Mastermind!");
                Console.WriteLine("\nPlease enter 4 numbers between 1 and 6.");
                Console.WriteLine("If a number is the correct value and in the correct place there will be a plus sign (+).");
                Console.WriteLine("If you guess the correct number but it is in the wrong place there will be a minus sign (-).");
                Console.WriteLine("If there are no signs then the number is wrong.");
                Console.WriteLine("Are you READY!? You only get 10 tries - Good luck!");

                List<int> secretCode = setup();
                bool final = play(secretCode);
                if (final)
                {
                    Console.WriteLine("\n\nCongratulations, You have Guessed Correct!");
                    wins++;
                }
                else
                {
                    Console.WriteLine("\n\nAwww, sorry to say it - but you are out of tries. You Lost!");
                    losses++;
                }
                Console.Write("The Secret Code was ");
                printFinal(secretCode);
                Console.WriteLine("\n\nThankyou for playing! Would you like to play again? (y/n)");
                Console.WriteLine("Wins: " + wins + " Losses: " + losses);
                if (Console.ReadKey().KeyChar == 'n') break;
                else Console.WriteLine("\nAlrighty! Here we go again!\n\n");
            }            
        }   


        // generate secret code
        private List<int> setup()
        {
            success = false;
            List<int> secretCode = new List<int>();
            Random rnd = new Random(Environment.TickCount);
            for (int i = 0;i < numCount; i++) 
            {
                secretCode.Add(rnd.Next(1, 7));
            }  
            return secretCode;
        }

        // controls number of tries the user gets
        // launches guess functions
        private bool play(List<int> secretCode)
        {
            
            for (int i = 0; i < tries; i++)
            {
                if (success) return true;
                List<int> guessCode = guess(i);
                List<char> results = checkGuess(guessCode, secretCode);
                printResults(results); 
            }
            return false;
        }

        // collects the guess information
        // verifies that the characters are numbers between 1 and 6
        private List<int> guess(int tryNum)
        {
            tryNum++;
            bool isNumber = false;
            int value = 0;
            List<int> guessCode = new List<int>();

            Console.Write("Guess " + tryNum.ToString() + ": | ");

            for (int i = 0; i < numCount; i++)
            {
                string entry = Console.ReadKey().KeyChar.ToString();
                (isNumber, value) = Helper.convertInt(entry);

                if (isNumber && value >= 1 && value <= 6)
                {
                    Console.Write(" | ");
                    guessCode.Add(value);
                }
                else
                {
                    Console.WriteLine("\nIncorrect value. Please enter a number between 1 and 6.");
                    i--;
                    printGuess(guessCode, tryNum);
                    continue;
                }
            }  
            return guessCode;
        }

        // handles re-printing out a guess
        private void printGuess(List<int> myGuess, int tryNum)
        {            
            Console.Write("Guess " + tryNum.ToString() + ":");
            for (int i = 0; i < myGuess.Count; i++)
            {
                Console.Write(" | " + myGuess[i]);
            }
            Console.Write(" | ");
        }

        private void printFinal(List<int> myGuess)
        {
            for (int i = 0; i < myGuess.Count; i++)
            {
                Console.Write(" | " + myGuess[i]);
            }
            Console.Write(" | ");
        }

        // compares the guess code to the secret code
        // returns a list of + and - characters
        private List<char> checkGuess (List<int> guessCode, List<int> secretCode)
        {   
            List<int> tempGuess = new List<int>();
            List<int> tempSecret = new List<int>();
            tempGuess = guessCode.ToList();
            tempSecret = secretCode.ToList();
            List<char> result = new List<char>();
            for (int a = 0; a < tempGuess.Count; a++)
            {
                if (tempGuess[a] == tempSecret[a])
                {
                    result.Add('+');
                    tempGuess.RemoveAt(a);
                    tempSecret.RemoveAt(a);
                    a--;
                    continue;
                }
            }
            for (int a = 0; a < tempSecret.Count; a++)
            {            
                for (int b = 0; b < tempGuess.Count; b++)
                {
                    if (tempSecret[a] == tempGuess[b])
                    {
                        result.Add('-');
                        tempGuess.RemoveAt(b);                        
                        break;
                    }                    
                }
            }
            return result;
        }

        // determines if the sequence is correct to win the game.
        // prints out the + and - characters
        private void printResults(List<char> results)
        {
            
            if (results.All(a => a == '+') && results.Count == numCount)
            {               
                success = true;
                return;
            }
            string resultsMsg = "";
            results.Sort();
            Console.Write("\nResults:");
            foreach (char val in results)
            {                
                resultsMsg += " " + val;
            }
            Console.WriteLine(resultsMsg);
            
        }
    }
}
