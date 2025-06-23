using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordCloudUnameIT
{
    public class WordCounter
    {
        public List<WordFrequency> CountWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new List<WordFrequency>();

            // 1. Normalise the input
            string cleaned = Regex.Replace(input.ToLower(), @"[^\w]+", " ");

            // 2. Split the sentences into words
            var words = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // 3. Count the frequency of each word
            var frequencyDict = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (frequencyDict.ContainsKey(word))
                    frequencyDict[word]++;
                else
                    frequencyDict[word] = 1;
            }

            // 4. Sort the words by frequency and then alphabetically
            return frequencyDict
                .Select(kvp => new WordFrequency { Word = kvp.Key, Count = kvp.Value })
                .OrderByDescending(wf => wf.Count)
                .ThenBy(wf => wf.Word)
                .ToList();
        }
    }

}
