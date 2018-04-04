using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AKI = AkiNet;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AKI.Client client = AKI.Client.StartGame(AKI.Client.Language.Hebrew);
            bool GameFinished = false;
            List<ulong> DeclinedCharacters = new List<ulong>();
            while (!GameFinished)
            {
                Console.WriteLine(client.Question.ToString());
                Console.ForegroundColor = ConsoleColor.Yellow;
                string Answer = Console.ReadLine();
                Console.ResetColor();
                AKI.Client.AnswerOptions AnswerOption;
                switch (Answer.ToLower())
                {
                    case "y":
                    case "yes":
                        AnswerOption = AKI.Client.AnswerOptions.Yes;
                        break;
                    case "n":
                    case "no":
                        AnswerOption = AKI.Client.AnswerOptions.No;
                        break;
                    case "idk":
                    case "i":
                    case "i dont know":
                        AnswerOption = AKI.Client.AnswerOptions.DontKnow;
                        break;
                    case "p":
                    case "probably":
                        AnswerOption = AKI.Client.AnswerOptions.Probably;
                        break;
                    case "pn":
                    case "probably not":
                        AnswerOption = AKI.Client.AnswerOptions.ProbablyNot;
                        break;
                    default:
                        Console.WriteLine("This is invalid answer.\nPlease type Yes/No/I Don't Know/Probably/Probably Not");
                        continue;
                }
                var nextQuestion = client.Answer(AnswerOption);
                var guesses = client.GetGuess();
                if (guesses.Characters == null) continue;
                var pGuesses = guesses.Characters.FirstOrDefault(g => g.Probabilty > 0.85 && !DeclinedCharacters.Contains(g.Id));
                if (pGuesses != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Is your character is:\n\t{pGuesses.Name}\n\t\t{pGuesses.Description}");
                    Console.ResetColor();
                    Answer = Console.ReadLine();
                    switch (Answer.ToLower())
                    {
                        case "y":
                        case "yes":
                            Console.WriteLine("YAY, I gussed that correctly!");
                            GameFinished = true;
                            break;
                        case "n":
                        case "no":
                            DeclinedCharacters.Contains(pGuesses.Id);
                            continue;
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
