using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WordCloudUnameIT;

class Program
{
    static async Task Main()
    {
        // Retrieve the type of input from the user
        Console.WriteLine("Choose your method of input:");
        Console.WriteLine("1: Enter text manually");
        Console.WriteLine("2: Read a textfile (.txt)");
        Console.Write("Your choice: ");
        var choice = Console.ReadLine();

        string inputText = "";

        // 1: Manual text input
        if (choice == "1")
        {
            Console.WriteLine("Enter text (press enter on an empty line to finish the input):");
            var lines = new List<string>();
            string? line;
            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                lines.Add(line);
            }
            inputText = string.Join(" ", lines);
        }

        // 2: Read from a text file
        else if (choice == "2")
        {
            Console.Write("Fill in the entire path to the .txt-file: ");
            var path = Console.ReadLine();

            if (File.Exists(path))
            {
                try
                {
                    inputText = await File.ReadAllTextAsync(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error when reading the file: {ex.Message}");
                    return;
                }
            }
            else
            {
                Console.WriteLine("File not found. Restart the program to try again.");
                return;
            }
        }

        // Invalid input
        else
        {
            Console.WriteLine("Invalid option, choose option 1 or 2. Restart the program to try again.");
            return;
        }

        // Count the words in the input text, write the results to the console
        var counter = new WordCounter();
        var results = counter.CountWords(inputText);

        Console.WriteLine("\nWord frequency:");
        foreach (var item in results)
        {
            Console.WriteLine($"{item.Word}: {item.Count}");
        }
    }
}
