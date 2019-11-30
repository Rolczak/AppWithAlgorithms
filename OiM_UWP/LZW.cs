using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiM_UWP
{
    static class LZW
    {
        static public Dictionary<string, int> InitializeDictionary(char[] text)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            int j = 0;
            for (int i = 0; i < text.GetLength(0); i++)
            {
                if (!dictionary.ContainsKey(text[i].ToString()))
                {
                    dictionary.Add(text[i].ToString(), j);
                    j++;
                }

            }

            return dictionary;
        }

        static public List<int> Encode(Dictionary<string, int> dictionary, char[] text)
        {
            string p_c;
            string previous = "";
            List<int> indexes = new List<int>();

            foreach (char current in text)
            {
                p_c = previous + current;
                if (dictionary.ContainsKey(p_c))
                {
                    previous = p_c;
                }
                else
                {
                    indexes.Add(dictionary[previous]);
                    dictionary.Add(p_c, dictionary.Count);
                    previous = current.ToString();
                }
            }

            if (previous != "")
            {
                indexes.Add(dictionary[previous]);
            }

            return indexes;
        }

        static public string Decode(Dictionary<string, int> dictionary, List<int> code)
        {
            try
            {


                Dictionary<int, string> alphabet = new Dictionary<int, string>();

                foreach (KeyValuePair<string, int> entry in dictionary)
                {
                    alphabet.Add(entry.Value, entry.Key);
                }
                string w = alphabet[code[0]];
                code.RemoveAt(0);
                StringBuilder stringBuilder = new StringBuilder(w);
                foreach (int k in code)
                {
                    string entry = null;
                    if (alphabet.ContainsKey(k))
                        entry = alphabet[k];
                    else if (k == alphabet.Count)
                        entry = w + w[0];

                    stringBuilder.Append(entry);

                    alphabet.Add(alphabet.Count, w + entry[0]);

                    w = entry;
                }

                return stringBuilder.ToString();
            }
            catch(Exception exp)
            {
                Utilities.showErrorMessage(exp.Message);
                return "sd";
            }

            
        }

    }
}
