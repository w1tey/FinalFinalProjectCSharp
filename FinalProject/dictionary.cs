using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinalProjectSharp;

public class Dictionary
{
    public List<Translation> Words { get; set; }
    private string? FromLanguage { get; set; }
    private string? ToLanguage { get; set; }

    public Dictionary(string? fromLanguage, string? toLanguage)
    {
        Words = new List<Translation>();
        FromLanguage = fromLanguage;
        ToLanguage = toLanguage;
    }

    public Dictionary(List<Translation> words, string? fromLanguage, string? toLanguage)
    {
        Words = words;
        FromLanguage = fromLanguage;
        ToLanguage = toLanguage;
    }


    public void Print()
    {
        Console.WriteLine($"From: {FromLanguage}" +
                          $"\nTo: {ToLanguage}");

        foreach (var translation in Words)
        {
            translation.Print();
        }
    }

    public void Append(Translation appendedTranslation)
    {
        Words.Add(appendedTranslation);
    }

    public void ChangeWord(int position, string newWord)
    {
        Words[position].ChangeWord(newWord);
    }

    public void ChangeTransaltion(int wordPosition, int translationPosition, string newTranslation)
    {
        Words[wordPosition].ChangeTranslation(translationPosition, newTranslation);
    }

    public void Remove(int position)
    {
        Words.RemoveAt(position);
    }

    public static void Serialization(List<Translation> dictionary)
    {
        using FileStream fs = new("translation.json", FileMode.Create, FileAccess.Write);
        JsonSerializer.Serialize(fs, dictionary);

        fs.Close();
    }

    public static Dictionary Deserialization(Dictionary oldDictionary)
    {
        using FileStream fs = new("translation.json", FileMode.Open, FileAccess.Read);
        List<Translation> newWords = JsonSerializer.Deserialize<List<Translation>>(fs);
        fs.Close();

        Dictionary newDictionary = new (newWords, oldDictionary.FromLanguage, oldDictionary.ToLanguage);

        return newDictionary;
    }
}


public class Translation
{
    public string Word { get; set; }
    public List<string> Translations { get; set; }

    [JsonConstructor] public Translation(string word, List<string> translations)
    {
        Word = word;
        Translations = translations;
    }

    public void Print()
    {
        Console.Write($"\nWord - {Word}" +
                      $"\nTranslations: ");

        foreach (var word in Translations)
        {
            Console.Write($"{word} , ");
        }
    }

    public void AddTranslation(string transl)
    {
        Translations.Add(transl);
    }

    public void DeleteTranslation(int position)
    {
        
        Translations.RemoveAt(position);
    }

    public void ChangeTranslation(int position, string newTranslation)
    {
        Translations[position] = newTranslation;
    }

    public void ChangeWord(string newWord)
    {
        Word = newWord;
    }

}