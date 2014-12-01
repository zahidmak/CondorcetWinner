using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Speech;

namespace CondorcetWinner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Initialize a new instance of the SpeechSynthesizer.
                SpeechSynthesizer synth = new SpeechSynthesizer();
                synth.SelectVoiceByHints(VoiceGender.Female);
                // Configure the audio output. 
                synth.SetOutputToDefaultAudioDevice();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-------------------------------------------------------------------------------\n");
                Console.WriteLine("********************  WELCOME TO THE CONDORCET ELECTION      *****************\n");
                Console.WriteLine("-------------------------------------------------------------------------------\n");
                synth.Speak("Welcome to the condorcet election.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                string file = "test.txt";
                StreamReader reader = new StreamReader(file);
                Console.WriteLine("Calculating election results.................\n");
                synth.Speak("Now calculating election results.");
                bool go = true;

                int ballot = 0;
                int candidate = 0;

                do
                {

                    //Reading first line for number of ballot and number of candidate
                    string[] temp = reader.ReadLine().ToString().Split(' ');
                    ballot = int.Parse(temp[0]);
                    candidate = int.Parse(temp[1]);

                    if (ballot == 0 && candidate == 0)
                    {
                        go = false;

                    }
                    else
                    {

                        //Creating jagged arrary for storing candidate position in each ballot
                        int[][] election = new int[ballot][];
                        for (int i = 0; i < ballot; i++)
                            election[i] = new int[candidate];
                        for (int i = 0; i < ballot; i++)
                        {
                            string[] temp2 = new string[candidate];
                            int[] temp1 = new int[candidate];
                            Console.ForegroundColor = ConsoleColor.DarkGreen;

                            string s = reader.ReadLine().ToString();
                            temp2 = s.Split(' ');
                            for (int k = 0; k < temp2.Length; k++)
                            {
                                election[i][k] = int.Parse(temp2[k]);
                            }

                        }

                        int[] sumOfPosition = new int[candidate];
                        for (int z = 0; z < sumOfPosition.Length; z++)
                        {
                            for (int x = 0; x < ballot; x++)
                            {
                                sumOfPosition[z] += Array.IndexOf(election[x], z);
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        var dict = new Dictionary<int, int>();

                        foreach (var value in sumOfPosition)
                        {
                            if (dict.ContainsKey(value))
                                dict[value]++;
                            else
                                dict[value] = 1;
                        }
                        bool IsWinnerDeclared = false;

                        foreach (var pair in dict)
                            if (pair.Value > 1)
                            {
                                Console.WriteLine("No winner");
                                IsWinnerDeclared = true;
                            }

                        if (IsWinnerDeclared == false)
                        {
                            Console.WriteLine("{0} is the winner", Array.IndexOf(sumOfPosition, sumOfPosition.Min()));

                        }
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Red;

                    }
                } while (go == true);



                Console.WriteLine("\n\n\t\t\tPRESS ENTER to exit....");
                synth.Speak("This is the result.");
                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message + " Please check if the file was in correct format.");
                Console.ReadLine();

            }

        }
    

    }
}
