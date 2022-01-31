using FinalProjectSharp;
using System;
using System.IO.Enumeration;
using System.Threading.Channels;

namespace FinalProject;


class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, welcome to Translator, a useless app." +
                          "\nPlease enter 2 languages (from and to) to create a new dictionary");
        var from = Console.ReadLine();
        var to = Console.ReadLine();
        var dictionary = new Dictionary(from, to);

        Console.WriteLine("Now, you will add words to your dictionary.");

        // Add words to dictionary
        while (true)
        {
            Console.WriteLine("1 - Continue" +
                              "\n2 - Enough words");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                List<string> translationsForWord = new List<string>();
                Console.Write("Enter the word you want to add:  ");
                var word = Console.ReadLine();

                while (true)
                {
                    Console.WriteLine("1 - Continue" +
                                      "\n2 - Enough Translations");
                    choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        Console.WriteLine("Enter word: ");
                        string? newTranslation = Console.ReadLine();
                        if (newTranslation != null) translationsForWord.Add(newTranslation);
                    }

                    else
                    {
                        break;
                    }
                }

                dictionary.Append(new Translation(word, translationsForWord));
            }

            else
            {
                break;
            }
        }

        var play = true;
        while (play)
        {
            Console.WriteLine("\nChoose one of these options:" +
                              "\n\t1 - Add a new translation to dictionary" +
                              "\n\t2 - Remove a word" +
                              "\n\t3 - Change a word" +
                              "\n\t4 - Print all the words" +
                              "\n\tOther - Exit");

            int choice = Convert.ToInt32(Console.ReadLine());

            int position = 0;
            string newWord;

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Enter new word");
                    newWord = Console.ReadLine();
                    Console.WriteLine("Enter meaning");
                    var newMeaning = Console.ReadLine();
                    var tempList = new List<string>();
                    tempList.Add(newMeaning);

                    var newTranslation = new Translation(newWord, new List<string>(tempList));

                    dictionary.Append(newTranslation);

                    Dictionary.Serialization(dictionary.Words);
                    break;

                case 2:
                    Console.Clear();
                    Console.Write("\nEnter the position of the word you want to delete:  ");
                    position = Convert.ToInt32(Console.ReadLine());
                    dictionary.Remove(position);

                    Dictionary.Serialization(dictionary.Words);
                    break;

                case 3:
                    Console.Clear();
                    Console.Write("\nEnter position of word you want to change:  ");
                    position = Convert.ToInt32(Console.ReadLine());
                    Console.Write("\nEnter the new word you want to use:  ");
                    newWord = Console.ReadLine();
                    dictionary.ChangeWord(position, newWord);

                    Dictionary.Serialization(dictionary.Words);
                    break;


                case 4:
                    Console.WriteLine("List of all words:");
                    dictionary.Print();
                    break;

                default:
                    play = false;
                    break;
            }

            
        }

    }
}
