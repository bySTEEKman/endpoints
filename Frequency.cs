using System;
using System.Text.Encodings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Web;
using System.Text.Json.Serialization;
public class Frequency
{
    Dictionary<string, int> frequency = new Dictionary<string, int>();
    public void GetFrequencyWords(string row)
    {
        foreach (string word in row.ToLower().Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries))
        {
            if (frequency.ContainsKey(word))
            {
                frequency[word]++;
            }
            else
            {
                frequency.Add(word, 1);
            }
        }
    }
    public int GetUniqueWords()
    {
        int unique = 0;
        foreach (KeyValuePair<string, int> word in frequency)
        {
            if (word.Value == 1)
            {
                unique++;
            }
        }
        return unique;
    }
    public string GetMostPopularWord()
    {
        string mostPopolar = "";
        int maxValue = 0;
        foreach (KeyValuePair<string, int> word in frequency)
        {
            if (word.Value > maxValue)
            {
                maxValue = word.Value;
            }
        }
        foreach (KeyValuePair<string, int> word in frequency)
        {
            if (word.Value == maxValue)
            {
                mostPopolar += word.Key + "";
            }
        }
        return mostPopolar;
    }
    public Dictionary<string, int> GetDictionary()
    {
        return frequency;
    }
}