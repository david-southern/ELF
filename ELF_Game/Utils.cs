using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using ELF_Resources.Properties;

namespace ELF
{
    class Utils
    {
        public static void Shuffle<T>(List<T> list)
        {
            Random my_rand = new Random();

            for (int shuffle_index = 0; shuffle_index < list.Count - 1; shuffle_index++ )
            {
                int swap_index = my_rand.Next(shuffle_index, list.Count);
                T tmp = list[shuffle_index];
                list[shuffle_index] = list[swap_index];
                list[swap_index] = tmp;
            }
        }

        private static byte[] ELF_Key = { 
            161, 60, 176, 208, 53, 92, 45, 65,
            167, 187, 144, 171, 6, 111, 171, 225,
            181, 54, 37, 129, 207, 64, 40, 117,
            35, 63, 180, 199, 158, 141, 138, 16
        };

        private static byte[] ELF_IV = { 
            208, 150, 144, 60, 214, 4, 23, 180,
            239, 180, 219, 1, 52, 169, 76, 85
        };

        private static Rijndael rijn = null;
        private static ICryptoTransform encryptor = null;
        private static ICryptoTransform decryptor = null;

        private static void SetupEncryption()
        {
            rijn = Rijndael.Create();
            rijn.KeySize = ELF_Key.Length * 8;
            rijn.Key = ELF_Key;
            rijn.IV = ELF_IV;
            rijn.Padding = PaddingMode.ISO10126;
            rijn.Mode = CipherMode.CBC;

            encryptor = rijn.CreateEncryptor();
            decryptor = rijn.CreateDecryptor();
        }

        public static string SimpleEncrypt(string clear_text)
        {
            if (rijn == null)
            {
                SetupEncryption();
            }

            MemoryStream ms = new MemoryStream();

            CryptoStream crypt_stream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

            byte[] clear_buffer = UTF8Encoding.UTF8.GetBytes(clear_text);

            crypt_stream.Write(clear_buffer, 0, clear_buffer.Length);
            crypt_stream.FlushFinalBlock();
            crypt_stream.Clear();
            crypt_stream.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string SimpleDecrpyt(string crypt_text)
        {
            if (rijn == null)
            {
                SetupEncryption();
            }

            byte[] crypt_buffer = Convert.FromBase64String(crypt_text);
            MemoryStream ms = new MemoryStream(crypt_buffer);

            CryptoStream crypt_stream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

            byte[] clear_buffer = new byte[crypt_buffer.Length];

            crypt_stream.Read(clear_buffer, 0, clear_buffer.Length);

            crypt_stream.Clear();
            crypt_stream.Close();

            int end_index;
            for (end_index = clear_buffer.Length - 1; end_index > 0 && clear_buffer[end_index] == 0; end_index--)
                ;

            byte[] trunc_buffer = new byte[end_index + 1];

            Array.Copy(clear_buffer, trunc_buffer, end_index + 1);

            return UTF8Encoding.UTF8.GetString(trunc_buffer);
        }

        private static List<string> CensoredWords = null;
        private static Dictionary<string, bool> CensoredDict = null;

        private static void LoadCensoredWords()
        {
            CensoredWords = new List<string>();
            string[] bad_words = Resources.REALLY_BAD_WORDS_DO_NOT_READ.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            CensoredDict = new Dictionary<string, bool>();

            foreach (string bad_word in bad_words)
            {
                CensoredDict[bad_word] = true;
            }

            CensoredWords.AddRange(CensoredDict.Keys);
        }

        public static bool IsCensoredWord(string check_word)
        {
            if (CensoredDict == null)
            {
                LoadCensoredWords();
            }

            return CensoredDict.ContainsKey(check_word);
        }

        public static string CensorText(string text)
        {
            if (CensoredWords == null)
            {
                LoadCensoredWords();
            }

            if (String.IsNullOrEmpty(text))
            {
                return text;
            }

            string censoredText = text;
            foreach (string censoredPattern in CensoredWords)
            {
                string regexPattern = ToRegexPattern(censoredPattern);
                censoredText = Regex.Replace(censoredText, regexPattern, "", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }

            return censoredText;
        }

        private static string ToRegexPattern(string wildcardSearch)
        {
            string regexPattern = Regex.Escape(wildcardSearch);
            regexPattern = regexPattern.Replace(@"\*", ".*?");
            regexPattern = regexPattern.Replace(@"\?", ".");
            if (regexPattern.StartsWith(".*?"))
            {
                regexPattern = regexPattern.Substring(3);
                regexPattern = @"(^\b)*?" + regexPattern;
            }
            regexPattern = @"\b" + regexPattern + @"\b";
            return regexPattern;
        }

        private static string StarCensoredMatch(Match m)
        {
            string word = m.Captures[0].Value;
            return new string('*', word.Length);
        }

        private static Dictionary<string, bool> valid_words = null;

        public static bool CheckDictionaryWord(string check_word)
        {
            if (valid_words == null)
            {
                InitializeDictionary();
            }

            return valid_words.ContainsKey(check_word.ToLower());
        }

        private static void InitializeDictionary()
        {
            valid_words = new Dictionary<string, bool>();
            string[] word_list = Resources.US_DICTIONARY.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in word_list)
            {
                valid_words.Add(word.ToLower(), true);
            }
        }
    }
}
