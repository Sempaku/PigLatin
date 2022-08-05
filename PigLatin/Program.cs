using System.Collections.Generic;
using System;
namespace PigLatin
{
    public class Program
    {
        public static void Main(string[] args)
        {
           /* PhoneticInstruments phoneticInstruments = new PhoneticInstruments();
            string res = phoneticInstruments.GetSyllable("стекло");
            Console.WriteLine(res);

            res = phoneticInstruments.GetSyllable("чёрная");
            Console.WriteLine(res);


            res = phoneticInstruments.GetSyllable("друзья");
            Console.WriteLine(res);


            res = phoneticInstruments.GetSyllable("вода");
            Console.WriteLine(res);


            res = phoneticInstruments.GetSyllable("масло");
            Console.WriteLine(res);


            res = phoneticInstruments.GetSyllable("лодка");
            Console.WriteLine(res);

            res = phoneticInstruments.GetSyllable("лето");
            Console.WriteLine(res);*/
        }
    }

    public interface IPigLatin
    {        
        string Encrypt(string text);

        string Decrypt(string text);
    }

    public class RuPigLatin : IPigLatin
    {
        
        string syllable;

        PhoneticInstruments phoneticInstruments;
        public bool IsVowel { get; set; }
        private string textMessage;
        private string resultMessage;
        readonly char CONSONANT;

        public RuPigLatin(char DefaultConsonant = 'с')
        {
            CONSONANT = DefaultConsonant;
            phoneticInstruments = new PhoneticInstruments();
        }

        public string Encrypt(string text)
        {
            textMessage = text;
            while(textMessage.Length > 0)
            {
                //syllable = phoneticInstruments.GetSyllable(textMessage, ref textMessage);
                resultMessage += syllable + CONSONANT + syllable[^1];
            }

            return resultMessage;
        }

        public string Decrypt(string text)
        {
            return "";
        }

        
    }

    public class EngPigLatin : IPigLatin
    {
        public string Encrypt(string text)
        {
            return "";
        }

        public string Decrypt(string text)
        {
            return "";
        }
    }
    

    public class PhoneticInstruments
    {
        readonly List<char> vowelsList = new List<char>() 
        {
            'а', 'о', 'э', 'е','и', 'ы', 'у', 'ё', 'ю', 'я' 
        };
        readonly List<char> zvonkSonarSoglList = new List<char> //delete 'й'
        {
            'л','м','н','р'
        };
        readonly List<char> zvonkShumnieSoglList = new List<char>
        {
            'в','з','ж','б','д','г'
        };
        readonly List<char> ShumnieSoglList = new List<char>
        {
            'п','т','к','ф','с','ш','х','щ','ч','ц'
        };
        string flags = "";

        public string[] GetSyllable(string word)
        {
            flags = GetFlags(word);
            string convertedFlags = RunFlags(flags).Trim() ;
            string[] flagArray = convertedFlags.Split(" ");
            string[] result = new string[flagArray.Length];

            for(int i = 0;  i < flagArray.Length; i++)
            {
                string microword = flagArray[i];
                result[i] += word.Substring(0,microword.Length);
                word = word.Remove(0, microword.Length);
            }
            return result;
        }

        public string GetFlags(string word)
        {
            string res = "";
            foreach(char letter in word)
            {
                if (vowelsList.Contains(letter))
                    res += 4;
                else if (zvonkSonarSoglList.Contains(letter))
                    res += 3;
                else if (zvonkShumnieSoglList.Contains(letter))
                    res += 2;
                else if (ShumnieSoglList.Contains(letter))
                    res += 1;
                else
                    res += letter;
            }
            return res;
        }

        public string RunFlags(string flags)
        {
            string result = "";
            int i = 1;
            while(i < flags.Length)
            {
                int first = flags[i-1] - '0';
                int second = flags[i] - '0';
                

                if (i == flags.Length - 1)
                {
                    /*if ((first - second > 1) && (first == 4 || first == 3))
                        result += flags[i-1]+" " + flags[i];
                    else
                        result += flags[i-1] +""+ flags[i];
                    i++;
                    continue;
*/
                    if ((first - second > 1) && (first == 4 || first == 3) ||
                    (first == 4) && (first == second))
                    {
                        result += flags[i - 1] + " " + flags[i];
                    }
                    else if (first == 4 && second == 3)
                    {
                        result += flags[i - 1] + " " + flags[i];
                    }
                    else
                    {
                        if ((flags[i] == 'ъ' || flags[i] == 'ь' || flags[i] == 'й') || (first - second <= 1))
                        {
                            result += flags[i - 1]+""+flags[i];
                        }
                        else
                        {
                            result += flags[i - 1] + " " + flags[i];
                        }
                        
                    }
                    i++;
                    continue;
                }

                if ((first - second > 1) && (first == 4 || first == 3) ||
                    (first==4)&&(first == second))
                {
                    result += flags[i-1] + " ";
                }
                else if(first==4 && (flags[i] == 'й'))
                {
                    result += flags[i - 1] + "" + flags[i] + " ";
                    i +=2;
                    continue;
                }                    
                else if(first == 4 && second == 3)
                {
                    result += flags[i - 1] + " ";
                }
                else
                {
                    if (  (flags[i] == 'ъ' || flags[i] == 'ь' ||flags[i]=='й')  || (first - second <= 1))
                    {
                        result += flags[i-1];
                    }
                    else
                    {
                        result += flags[i -1] + " ";
                    }
                }
                i++;


              
                
            }
            return result;
        }
    }
}