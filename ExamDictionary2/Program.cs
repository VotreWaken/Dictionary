using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExamDictionary2
{

    internal class Program
    {
        // Vocabulary With Dictionary Of Words and their Translates
        [Serializable]
        [DataContract]
        public class Vocabulary
        {
            [DataMember]
            public string Name { set; get; }
            [DataMember]
            protected Dictionary<string, List<string>> vocabulary = new Dictionary<string, List<string>>();

            public Vocabulary()
            {
                SetVocabularyName();
            }

            public void SetVocabularyName()
            {
                Console.Write("Enter the Original Vocabulary Language:");
                string word = Console.ReadLine();
                Console.Write("Enter the Vocabulary Translate Language:");
                string word1 = Console.ReadLine();
                Name = word + '/' + word1;
                Console.Clear();
            }
            public string GetVocabularyName()
            {
                return Name;
            }
            public void AddWord()
            {
                Console.Write("Enter the word in English:");
                string word = Console.ReadLine();
                Console.Write("Enter the translation:");
                string translate = Console.ReadLine();
                List<string> translationWords = new List<string> { translate };
                vocabulary.Add(word, translationWords);
                Console.Clear();
            }
            public void Show()
            {
                foreach (var el in vocabulary)
                {
                    Console.WriteLine($"\nKey: {el.Key}");
                    foreach (var item1 in el.Value)
                        Console.WriteLine(item1 + ", ");
                }
                vocabulary.OrderBy(x => x);
            }
            public void SearchTranslate()
            {
                Console.WriteLine("Enter word to search");
                string input = Console.ReadLine();
                foreach (var item in vocabulary)
                    if (item.Key.ToLower() == input.ToLower() || item.Value.Contains(input))
                    {
                        Console.WriteLine("Word:");

                        Console.WriteLine($"Original:{item.Key}");

                        Console.Write("Translate: ");

                        foreach (var item1 in item.Value)
                            Console.Write(item1 + " ");
                    }
            }
            public void AddTranslate()
            {

                Console.WriteLine("Enter what translate change:");

                string input = Console.ReadLine();
                if (input != null)
                {
                    Console.WriteLine("Enter what Original word must be Added:");

                    string wordWhatChange = Console.ReadLine();

                    Console.WriteLine("Enter word to Add:");

                    string wordToChange = Console.ReadLine();

                    if (wordWhatChange != null && vocabulary.ContainsKey(input))
                    {
                        vocabulary[input].Add(wordToChange);
                    }
                }
                Console.Clear();
            }
            public void ChangeTranslateWord()
            {

                Console.WriteLine("Enter what translate word need change translation:");

                string input = Console.ReadLine();
                if (input != null)
                {

                    Console.WriteLine("Enter what translate change:");

                    string wordWhatChange = Console.ReadLine();


                    Console.WriteLine("Enter word to change:");

                    string wordToChange = Console.ReadLine();

                    if (wordWhatChange != null && wordToChange != null && vocabulary.ContainsKey(input))
                    {
                        vocabulary[input].Remove(wordWhatChange);
                        vocabulary[input].Add(wordToChange);
                    }
                }
                Console.Clear();
            }
            public void ChangeWord()
            {
                Console.WriteLine("Enter Original word which you want to change translation:");
                string input = Console.ReadLine();

                if (input != null)
                {

                    Console.WriteLine("Enter word to change:");

                    string wordToChange = Console.ReadLine();
                    if (wordToChange != null && vocabulary.ContainsKey(wordToChange))
                    {
                        KeyValuePair<string, List<string>> pair = new KeyValuePair<string, List<string>>(wordToChange, vocabulary[input]);
                        vocabulary[input.Replace(input, wordToChange)] = pair.Value;
                        vocabulary.Remove(input);
                    }
                }
                Console.Clear();
            }
            public void RemoveWord()
            {
                Console.WriteLine("Enter the word what you want remove:");
                string input = Console.ReadLine();
                if (input != null)
                    foreach (var item in vocabulary)
                        if (item.Key.ToLower() == input.ToLower())
                        {
                            vocabulary.Remove(item.Key);
                            break;
                        }
                Console.Clear();
            }
            public void RemoveWordTranslation()
            {
                Console.WriteLine("Enter Original word which you want to remove translation:");
                string input = Console.ReadLine();

                if (input != null)
                {
                    Console.WriteLine("Enter the translation word to remove:");

                    string wordToRemove = Console.ReadLine();

                    if (wordToRemove != null)
                        foreach (var item in vocabulary)
                            if (item.Value.Contains(wordToRemove) && item.Key.ToLower() == input.ToLower())
                                item.Value.Remove(wordToRemove);
                            else
                    Console.WriteLine("Can`t Be Found");

                    Console.Clear();
                }
            }
        }


        // List Of Vocabularyes 
        public class VocabularyCollection
        {
            public List<Vocabulary> vocabulary = new List<Vocabulary>();
            public VocabularyCollection()
            {

            }
        }
        [Serializable]
        [DataContract]
        public class UserInterface
        {
            [DataMember]
            List<Vocabulary> vocabularycollection = new List<Vocabulary>();

            public void ShowVocabulary()
            {
                int i = 0;
                foreach (var item in vocabularycollection)
                {
                    i++;
                    Console.WriteLine($"{i}. {item.Name}");
                }
            }
            public void Start()
            {
                vocabularycollection.Add(new Vocabulary());
                while (true)
                {
                    Console.WriteLine("1. Add New Word");
                    Console.WriteLine("2. Add New Translate To The Word");
                    Console.WriteLine("3. Change Word");
                    Console.WriteLine("4. Change Word Translation");
                    Console.WriteLine("5. Delete Word");
                    Console.WriteLine("6. Delete Word Translation");
                    Console.WriteLine("7. Search Word Translation");
                    Console.WriteLine("8. Save Vocabulary To File");
                    Console.WriteLine("9. Load Vocabulary From File");
                    Console.WriteLine("10. Show Vocabulary");
                    Console.WriteLine("11. Add New Vocabulary");
                    Console.WriteLine("12. Exit");
                    int choiceVoc;
                    int choice;
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                    }
                    catch (Exception a)
                    {
                        Console.WriteLine(a.Message);
                        continue;
                    }
                    if (choice > 12)
                    {
                        Console.WriteLine("Error Input Choice");
                        continue;
                    }
                    if (vocabularycollection.Count < 1 || choice == 11 || choice == 9 || choice == 8)
                    {
                        choiceVoc = 0;
                    }
                    else
                    {
                        Console.WriteLine("Indicate which dictionary to perform the operation on");
                        ShowVocabulary();
                        choiceVoc = int.Parse(Console.ReadLine());
                        if (choiceVoc > vocabularycollection.Count)
                        {
                            Console.WriteLine("Error Input Index");
                            continue;
                        }
                    }
                    switch (choice)
                    {
                        case 1:
                            vocabularycollection[choiceVoc - 1].AddWord();
                            break;
                        case 2:
                            vocabularycollection[choiceVoc - 1].AddTranslate();
                            break;
                        case 3:
                            vocabularycollection[choiceVoc - 1].ChangeWord();
                            break;
                        case 4:
                            vocabularycollection[choiceVoc - 1].ChangeTranslateWord();
                            break;
                        case 5:
                            vocabularycollection[choiceVoc - 1].RemoveWord();
                            break;
                        case 6:
                            vocabularycollection[choiceVoc - 1].RemoveWordTranslation();
                            break;
                        case 7:
                            vocabularycollection[choiceVoc - 1].SearchTranslate();
                            break;
                        case 8:
                            SavingToFile save = new SavingToFile();
                            save.Save(vocabularycollection[choiceVoc - 1]);
                            break;
                        case 9:
                            LoadFromFile dow = new LoadFromFile();
                            vocabularycollection.Add(dow.Load());
                            break;
                        case 10:
                            vocabularycollection[choiceVoc - 1].Show();
                            break;
                        case 11:
                            vocabularycollection.Add(new Vocabulary());
                            break;
                        case 12:
                            return;
                        default:
                            break;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            UserInterface voc = new UserInterface();
            voc.Start();
        }

        class LoadFromFile
        {
            
            public Vocabulary Load()
            {
                FileStream stream = new FileStream("Vocabulary.xml", FileMode.Open);
                DataContractJsonSerializer downloader = new DataContractJsonSerializer(typeof(Vocabulary));
                Vocabulary collectionVocabulary = (Vocabulary)downloader.ReadObject(stream);
                stream.Close();
                return collectionVocabulary;
            }
            
        }


        public class SavingToFile
        {
            public void Save(Vocabulary collection)
            {
                FileStream stream = new FileStream("Vocabulary.xml", FileMode.Create);
                DataContractJsonSerializer saver = new DataContractJsonSerializer(typeof(Vocabulary));
                saver.WriteObject(stream, collection);
                stream.Close();
                Console.WriteLine("Json serializer OK");
            }
        }

    }
}
