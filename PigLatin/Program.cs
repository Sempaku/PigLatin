using System;
using System.Collections.Generic;

namespace PigLatin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IPigLatin pig = new RuPigLatin();
            string result = pig.Encrypt("У, вас!");
            Console.WriteLine(result);
        }
    }

    public interface IPigLatin
    {
        string EncryptWord(string word);
        string Encrypt(string text);
        string DecryptWord(string word);
        string Decrypt(string text);
    }

    public class RuPigLatin : IPigLatin
    {
        private string[] syllables;
        private List<char> unalphList = new List<char> 
        {
            ' ','!','@','#',',','.','%','\\','?','(',')','{','}',
            ':',';','<','>','/','*','-','+','=','$','^','&','"','№'
        };

        private PhoneticInstruments phoneticInstruments;
        // public bool IsVowel { get; set; }
        private readonly char CONSONANT;
        private string[] textMsg;
        public RuPigLatin(char DefaultConsonant = 'с')
        {
            CONSONANT = DefaultConsonant;
            phoneticInstruments = new PhoneticInstruments();
        }

        public string EncryptWord(string text)
        {
            text = text.ToLower();
            string result = "";
            syllables = phoneticInstruments.GetSyllable(text);
            for (int i = 0; i < syllables.Length; i++)
            {
                char vowel = phoneticInstruments.GetVowel(syllables[i]);
                result = result + syllables[i] + CONSONANT + vowel;
            }
            return result;
        }

        public string Encrypt(string text)
        {
            textMsg = GetWords(text);
            textMsg = phoneticInstruments.DeleteEmptyStrings(textMsg);

           
            string result = "";
            for (int i = 0; i < textMsg.Length; i++)
            {
                if ((textMsg[i].Length == 1 ) &&
                    unalphList.Contains(Convert.ToChar(textMsg[i])))
                {
                    
                    result += textMsg[i];

                }
                else
                {
                    result += EncryptWord(textMsg[i]);
                }

            }
            return result;
        }
        public string DecryptWord(string text)
        {
            return "";
        }
        public string Decrypt(string text)
        {
            return "";
        }

        public string[] GetWords(string text)
        {
            string[] result;
            text = text.ToLower();
            string alh = "abcdefghijklmnopqrstuvwxyzабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            List<string> arr = new List<string>() { "" } ;
            int indexWord = 0;
            int indexMassiv = 0;
            while (indexWord < text.Length)
            {
                if (alh.Contains(text[indexWord]))
                {
                    arr[indexMassiv] += text[indexWord];
                    indexWord++;
                }
                else
                {
                    arr.Add(Convert.ToString(text[indexWord]));
                    arr.Add("");
                    indexWord++;
                    indexMassiv += 2;
                    
                }
            }
            result = new string[arr.Count];
            for (int j = 0; j < arr.Count; j++)
                result[j] += arr[j];
            return result;
        }
    }

/*    public class EngPigLatin : IPigLatin
    {
        public string EncryptWord(string text)
        {
            return "";
        }

        public string DecryptWord(string text)
        {
            return "";
        }
    }
*/
    public class PhoneticInstruments
    {
        private readonly List<char> vowelsList = new List<char>()
        {
            'а', 'о', 'э', 'е','и', 'ы', 'у', 'ё', 'ю', 'я'
        };

        private readonly List<char> zvonkSonarSoglList = new List<char> //delete 'й'
        {
            'л','м','н','р'
        };

        private readonly List<char> zvonkShumnieSoglList = new List<char>
        {
            'в','з','ж','б','д','г'
        };

        private readonly List<char> ShumnieSoglList = new List<char>
        {
            'п','т','к','ф','с','ш','х','щ','ч','ц'
        };

        private string flags = "";

        // Получаем массив слогов
        public string[] GetSyllable(string word)
        {
            flags = GetFlags(word); // получаем флаги слов "145432"
            string convertedFlags = RunFlags(flags).Trim(); // ставим пробелы между флагами
            string[] flagArray = convertedFlags.Split(" "); // делаем массив флагов
            string[] result = new string[flagArray.Length];

            // в массив results[] конвертируем флаги в буквы
            for (int i = 0; i < flagArray.Length; i++)
            {
                string microword = flagArray[i];
                result[i] += word.Substring(0, microword.Length);
                word = word.Remove(0, microword.Length);
            }

            result = ResearchResults(result); // проверяем по остаточным правилам
            result = DeleteEmptyStrings(result); // удаляем пустые строки в массиве
            result = CheckResults(result); // проверка на остаточные правила 2
            return result;
        }

        public string[] CheckResults(string[] arr)
        {
            string[] result = arr;
            for (int i = 1; i < result.Length; i++)
            {
                string firstWord = result[i - 1];
                string secondWord = result[i];

                // проверка на "ять"
                if (firstWord.Contains("ять") && (!(i == result.Length)))
                {
                    result[i - 1] = result[i - 1].Remove(firstWord.Length - 2, 2);
                    result[i] = "ть" + result[i];
                    continue;
                }

                // проверка : если первый слог кончается на гласную, а след. слог начинается
                // на 2 согласные, то к первому слогу добавляем первую букву второго слога
                if ((secondWord.Length > 1) && (result.Length >= 3) && (i != 1) &&
                    (secondWord.Length > firstWord.Length) &&
                    (secondWord.Length > 2) &&
                    ((HaveVowel(Convert.ToString(firstWord[^1]))) &&
                    (!HaveVowel(Convert.ToString(secondWord[0]))) &&
                    (!HaveVowel(Convert.ToString(secondWord[1])))))
                {
                    result[i - 1] += secondWord[0];
                    result[i] = result[i].Remove(0, 1);
                }
            }

            // проверка на удвоенные согласные
            for (int i = 1; i < result.Length; i++)
            {
                string firstWord = result[i - 1];
                string secondWord = result[i];
                if (firstWord[^1] == secondWord[0])
                {
                    result[i - 1] = result[i - 1].Remove(firstWord.Length - 1, 1);
                    result[i] = firstWord[^1] + result[i];
                    continue;
                }
            }
            return result;
        }

        public string[] DeleteEmptyStrings(string[] arr)
        {
            List<string> newArr = new List<string>();
            foreach (string x in arr)
            {
                if (!(x == ""))
                    newArr.Add(x);
            }
            string[] result = new string[newArr.Count];

            for (int i = 0; i < newArr.Count; i++)
            {
                result[i] = newArr[i];
            }

            return result;
        }

        public string[] ResearchResults(string[] arr)
        {
            string[] result = arr;
            for (int i = 1; i < arr.Length; i++)
            {
                string firstWord = result[i - 1];
                string secondWord = result[i];

                if (HaveVowel(firstWord) && (!HaveVowel(secondWord)))
                {
                    result[i - 1] += result[i];
                    result[i] = "";
                }
            }

            return result;
        }

        public bool HaveVowel(string word)
        {
            foreach (var c in word)
            {
                if (vowelsList.Contains(c))
                    return true;
            }
            return false;
        }

        public string GetFlags(string word)
        {
            string res = "";
            foreach (char letter in word)
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
            if (flags.Length == 1)
                return flags;
            string result = "";
            int i = 1;
            while (i < flags.Length)
            {
                int first = flags[i - 1] - '0';
                int second = flags[i] - '0';

                if (i == flags.Length - 1)
                {
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
                            result += flags[i - 1] + "" + flags[i];
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
                    (first == 4) && (first == second))
                {
                    result += flags[i - 1] + " ";
                }
                else if (first == 4 && (flags[i] == 'й'))
                {
                    result += flags[i - 1] + "" + flags[i] + " ";
                    i += 2;
                    continue;
                }
                else if (first == 4 && second == 3)
                {
                    result += flags[i - 1] + " ";
                }
                else
                {
                    if ((flags[i] == 'ъ' || flags[i] == 'ь' || flags[i] == 'й') || (first - second <= 1))
                    {
                        result += flags[i - 1];
                    }
                    else
                    {
                        result += flags[i - 1] + " ";
                    }
                }
                i++;
            }
            return result;
        }

        public char GetVowel(string syllable)
        {
            foreach (char vowel in syllable)
            {
                if (vowelsList.Contains(vowel))
                    return vowel;
            }
            return 'q';
        }
    }
}