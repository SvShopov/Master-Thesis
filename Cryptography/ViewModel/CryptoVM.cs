using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cryptography.ViewModel
{
    public static class CryptoVM
    {
        //-----------------------------------------------------------------------------
        public static string SimpleEncode(string plain, string key, string alpha)
        {
            plain = plain.ToUpper();
            key = key.ToUpper();
            int i;
            string result = "";
            int posM;
            int posK;
            for (i = 0; i < plain.Length; i++)
            {
                posM = ReturnPosition(plain[i], alpha);
                if (posM == -1)
                {
                    return string.Empty;
                }
                if (i < key.Length)
                {
                    posK = ReturnPosition(key[i], alpha);
                }
                else
                {
                    posK = ReturnPosition(key[i % key.Length], alpha);
                }
                result += alpha[(posM + posK + 1) % alpha.Length];
            }
            return result;
        }

        public static string SimpleDecode(string plain, string key, string alpha)
        {
            plain = plain.ToUpper();
            key = key.ToUpper();
            int i;
            string result = "";
            int posM;
            int posK;
            for (i = 0; i < plain.Length; i++)
            {
                posM = ReturnPosition(plain[i], alpha);
                if (i < key.Length)
                {
                    posK = ReturnPosition(key[i], alpha);
                }
                else
                {
                    posK = ReturnPosition(key[i % key.Length], alpha);
                }
                int d = (posM - posK - 1);
                if (d >= 0)
                    result += alpha[d];
                else
                    result += alpha[d + alpha.Length];
            }
            return result;
        }

        static int ReturnPosition(char c, string alpha)
        {
            for (int i = 0; i < alpha.Length; i++)
            {
                if (c == alpha[i])
                {
                    return i;
                }
            }
            return -1;
        }

        //-----------------------------------------------------------------------------

        public static string VigenereEncode(string input, string alphaa, string key)
        {
            input = input.ToUpper();
            alphaa = alphaa.ToUpper();
            key = key.ToUpper();
            char[] alpha = alphaa.ToCharArray();
            char[] inputt = input.ToCharArray();
            int j = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (alpha.Contains(input[i]))
                    inputt[i] = alpha[(Array.IndexOf(alpha, input[i]) + Array.IndexOf(alpha, key[j])) % alpha.Length];
                j = (j + 1) % key.Length;
            }
            string result = new string(inputt);
            return result;
        }

        public static string VigenereDecode(string input, string alphaa, string key)
        {
            input = input.ToUpper();
            alphaa = alphaa.ToUpper();
            key = key.ToUpper();
            char[] alpha = alphaa.ToCharArray();
            char[] inputt = input.ToCharArray();
            int j = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (alpha.Contains(input[i]))
                    inputt[i] = input[i] >= key[j] ?
                    (char)(input[i] - key[j] + alphaa[0]) : (char)(alphaa[0] + (alphaa[alphaa.Length - 1] - key[j] + input[i] - alphaa[0]) + 1);
                //j = j + 1 == key.Length ? 0 : j + 1; // if j+1 == keylength  j = 0  else j = j+1
                if (j + 1 == key.Length)
                {
                    j = 0;
                }
                else
                {
                    j++;
                }
            }
            string result = new string(inputt);
            return result;
        }

        //-----------------------------------------------------------------------------

        public static string NihilistEncode(string plain, string keyword, string key)
        {
            plain = plain.ToUpper();
            keyword = keyword.ToUpper();
            key = key.ToUpper();
            string code = "";

            keyword = FixKeywordNihilist(keyword);
            key = FixKeyNihilist(plain, key);
            string alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            for (int j = 0; j < keyword.Length; j++)
            {
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (alpha[i] == keyword[j])
                    {
                        alpha = alpha.Remove(i, 1);
                    }
                }
            }
            string realAlpha = keyword + alpha;

            char[,] square = new char[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    square[j, i] = realAlpha[i + j * 5];
                }
            }

            List<int> plainNums = new List<int>();
            List<int> keyNums = new List<int>();

            for (int k = 0; k < plain.Length; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (square[i, j] == plain[k])
                        {
                            int plainNum = int.Parse((i + 1).ToString() + (j + 1).ToString());
                            plainNums.Add(plainNum);
                        }
                        if (square[i, j] == key[k])
                        {
                            int keyNum = int.Parse((i + 1).ToString() + (j + 1).ToString());
                            keyNums.Add(keyNum);
                        }
                    }
                }
            }

            for (int i = 0; i < plain.Length; i++)
            {
                int codeNum = plainNums[i] + keyNums[i]; //goes BOOM look for Exceptions!!!
                code += codeNum + " ";
            }

            return code;
        }

        public static string NihilistDecode(string code, string keyword, string key)
        {
            code = code.ToUpper();
            keyword = keyword.ToUpper();
            key = key.ToUpper();
            string plain = "";

            List<int> codeNums = code.Trim(' ').Split(' ').Select(int.Parse).ToList();
            int codeNumsLength = codeNums.Count();
            string lengthKey = "";
            for (int i = 0; i < codeNumsLength; i++)
            {
                lengthKey += "x";
            }

            key = FixKeyNihilist(lengthKey, key);
            keyword = FixKeywordNihilist(keyword);
            string alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            for (int j = 0; j < keyword.Length; j++)
            {
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (alpha[i] == keyword[j])
                    {
                        alpha = alpha.Remove(i, 1);
                    }
                }
            }
            string realAlpha = keyword + alpha;

            char[,] square = new char[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    square[j, i] = realAlpha[i + j * 5];
                }
            }

            List<int> plainNums = new List<int>();
            List<int> keyNums = new List<int>();

            for (int k = 0; k < key.Length; k++)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (square[i, j] == key[k])
                        {
                            int keyNum = int.Parse((i + 1).ToString() + (j + 1).ToString());
                            keyNums.Add(keyNum);
                        }
                    }
                }
            }

            for (int i = 0; i < key.Length; i++)
            {
                int plainNum = codeNums[i] - keyNums[i];
                plainNums.Add(plainNum);
            }

            foreach (var item in plainNums)
            {
                string numToString = item.ToString();
                char row = numToString[0];
                char col = numToString[1];
                int rowNum = row - '0';
                int colNum = col - '0';

                plain += (square[rowNum - 1, colNum - 1]);
            }

            return plain;
        }

        static string FixKeyNihilist(string plain, string key)
        {
            int repeat = plain.Length / key.Length;
            int add = plain.Length % key.Length;
            string realKey = "";
            for (int i = 0; i < repeat; i++)
            {
                realKey += key;
            }
            for (int i = 0; i < add; i++)
            {
                realKey += key[i];
            }
            //Console.WriteLine(realKey);

            return realKey;
        }

        static string FixKeywordNihilist(string keyword)
        {
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            List<char> letters = new List<char>();
            string newKey = "";

            for (int i = 0; i < keyword.Length; i++)
            {
                for (int j = 0; j < alpha.Length; j++)
                {
                    if (keyword[i] == alpha[j])
                    {
                        letters.Add(keyword[i]);
                    }
                }
            }

            List<char> noDuplicates = letters.Distinct().ToList();
            foreach (var item in noDuplicates)
            {
                newKey += item;
            }

            return newKey;
        }

        //-----------------------------------------------------------------------------

        public static string PlayfairEncode(string plain, string key)
        {
            plain = plain.ToUpper();
            key = key.ToUpper();
            string code = "";
            string fixedKey = FixKeyPlayfair(key);
            List<string> fixedPlain = FixPlainPlayfair(plain);

            string alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            for (int j = 0; j < fixedKey.Length; j++)
            {
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (alpha[i] == fixedKey[j])
                    {
                        alpha = alpha.Remove(i, 1);
                    }
                }
            }
            string realAlpha = fixedKey + alpha;


            char[,] square = new char[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    square[j, i] = realAlpha[i + j * 5];
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    //Console.Write(square[i, j] + " ");
                }
                //Console.WriteLine();
            }

            code = Encode(square, fixedPlain);

            return code;
        }

        public static string PlayfairDecode(string code, string key)
        {
            code = code.ToUpper();
            key = key.ToUpper();
            string plain = "";
            List<string> stringParts = new List<string>();
            for (int i = 0; i < code.Length - 1; i += 2)
            {
                string twoLetters = code.Substring(i, 2);
                stringParts.Add(twoLetters);
            }

            string fixedKey = FixKeyPlayfair(key);
            string alpha = "ABCDEFGHIKLMNOPQRSTUVWXYZ";
            for (int j = 0; j < fixedKey.Length; j++)
            {
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (alpha[i] == fixedKey[j])
                    {
                        alpha = alpha.Remove(i, 1);
                    }
                }
            }
            string realAlpha = fixedKey + alpha;


            char[,] square = new char[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    square[j, i] = realAlpha[i + j * 5];
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    //Console.Write(square[i, j] + " ");
                }
                //Console.WriteLine();
            }

            plain = Decode(square, stringParts);
            plain = RemoveX(plain);
            return plain;
        }

        static String FixKeyPlayfair(string key)
        {
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            List<char> letters = new List<char>();
            string newKey = "";

            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < alpha.Length; j++)
                {
                    if (key[i] == alpha[j])
                    {
                        letters.Add(key[i]);
                    }
                }
            }

            List<char> noDuplicates = letters.Distinct().ToList();
            foreach (var item in noDuplicates)
            {
                newKey += item;
            }

            return newKey;
        }

        static List<string> FixPlainPlayfair(string plain)
        {
            string plainWithX = " ";
            List<string> stringParts = new List<string>();

            List<int> indexes = new List<int>();
            for (int i = 0; i < plain.Length - 1; i++)
            {
                for (int j = i; j < plain.Length; j++)
                {
                    if (plain[i] == plain[j] && j - i == 1)
                    {
                        indexes.Add(j - 1);
                    }
                }
            }
            int[] indexArray = new int[indexes.Count];
            for (int i = 0; i < indexes.Count; i++)
            {
                indexArray[i] = indexes[i];
            }
            plainWithX = InsertX(plain, indexArray);

            if (plainWithX.Length % 2 == 1)
            {
                if (plainWithX[plainWithX.Length - 1] == 'X')
                {
                    plainWithX += 'Z';
                }
                else
                {
                    plainWithX += 'X';
                }
            }

            for (int i = 0; i < plainWithX.Length - 1; i += 2)
            {
                string twoLetters = plainWithX.Substring(i, 2);
                stringParts.Add(twoLetters);
            }

            //foreach (var item in stringParts)
            //{
            //    //Console.WriteLine(item);
            //}

            return stringParts;
        }

        static String InsertX(this string str, params int[] positions)
        {
            StringBuilder sb = new StringBuilder(str.Length + positions.Length);
            var posLookup = new HashSet<int>(positions);
            for (int i = 0; i < str.Length; i++)
            {
                sb.Append(str[i]);
                if (posLookup.Contains(i))
                    sb.Append('X');
            }
            return sb.ToString();
        }

        static String RemoveX(string input)
        {
            string newString = "";
            List<int> toRemove = new List<int>();

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == 'X' && input[i - 1] == input[i + 1])
                {
                    toRemove.Add(i);
                }
            }

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < toRemove.Count; j++)
                {
                    if (i == toRemove[j])
                    {
                        input = input.Replace(input[i], '.');
                        //Console.WriteLine(input);
                    }
                }
            }

            newString = input.Replace(".", string.Empty);
            return newString;
        }


        static string Encode(char[,] grid, List<string> duets)
        {
            string result = "";

            List<int> firstcoordinates = new List<int>();
            List<int> secondcoordinates = new List<int>();
            List<int> resultcoordinates = new List<int>();

            int firstRow = 0;
            int firstCol = 0;
            int secondRow = 0;
            int secondCol = 0;

            foreach (var item in duets)
            {
                char firstLetter = item[0];
                char secondLetter = item[1];

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (grid[i, j] == firstLetter)
                        {
                            firstRow = i;
                            firstCol = j;
                            firstcoordinates.Add(firstRow);
                            firstcoordinates.Add(firstCol);
                        }
                        if (grid[i, j] == secondLetter)
                        {
                            secondRow = i;
                            secondCol = j;
                            secondcoordinates.Add(secondRow);
                            secondcoordinates.Add(secondCol);
                        }
                    }
                }
            }

            for (int i = 0; i < firstcoordinates.Count; i += 2)
            {
                firstRow = firstcoordinates[i];
                firstCol = firstcoordinates[i + 1];
                secondRow = secondcoordinates[i];
                secondCol = secondcoordinates[i + 1];

                if (firstRow == secondRow)
                {
                    result += grid[firstRow, firstCol + 1 > 4 ? 0 : firstCol + 1];
                    result += grid[firstRow, secondCol + 1 > 4 ? 0 : secondCol + 1];
                }
                else if (firstCol == secondCol)
                {
                    result += grid[firstRow + 1 > 4 ? 0 : firstRow + 1, firstCol];
                    result += grid[secondRow + 1 > 4 ? 0 : secondRow + 1, firstCol];
                }
                else if (firstCol != secondCol && firstRow != secondRow)
                {
                    result += grid[firstRow, secondCol];
                    //Console.WriteLine(grid[firstRow, secondCol]);
                    result += grid[secondRow, firstCol];
                    //Console.WriteLine(grid[secondRow, firstCol]);
                }
            }

            return result;
        }


        static string Decode(char[,] grid, List<string> duets)
        {
            string result = "";

            List<int> firstcoordinates = new List<int>();
            List<int> secondcoordinates = new List<int>();
            List<int> resultcoordinates = new List<int>();

            int firstRow = 0;
            int firstCol = 0;
            int secondRow = 0;
            int secondCol = 0;

            foreach (var item in duets)
            {
                char firstLetter = item[0];
                char secondLetter = item[1];

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (grid[i, j] == firstLetter)
                        {
                            firstRow = i;
                            firstCol = j;
                            firstcoordinates.Add(firstRow);
                            firstcoordinates.Add(firstCol);
                        }
                        if (grid[i, j] == secondLetter)
                        {
                            secondRow = i;
                            secondCol = j;
                            secondcoordinates.Add(secondRow);
                            secondcoordinates.Add(secondCol);
                        }
                    }
                }
            }

            for (int i = 0; i < firstcoordinates.Count; i += 2)
            {
                firstRow = firstcoordinates[i];
                firstCol = firstcoordinates[i + 1];
                secondRow = secondcoordinates[i];
                secondCol = secondcoordinates[i + 1];

                if (firstRow == secondRow)
                {
                    result += grid[firstRow, firstCol - 1 < 0 ? 4 : firstCol - 1];
                    result += grid[firstRow, secondCol - 1 < 0 ? 4 : secondCol - 1];
                }
                else if (firstCol == secondCol)
                {
                    result += grid[firstRow - 1 < 0 ? 4 : firstRow - 1, firstCol];
                    result += grid[secondRow - 1 < 0 ? 4 : secondRow - 1, firstCol];
                }
                else if (firstCol != secondCol && firstRow != secondRow)
                {
                    result += grid[firstRow, secondCol];
                    //Console.WriteLine(grid[firstRow, secondCol]);
                    result += grid[secondRow, firstCol];
                    //Console.WriteLine(grid[secondRow, firstCol]);
                }
            }
            return result;
        }

        //-----------------------------------------------------------------------------

        public static string DirectEncode(string inputt, string alpha1, string alpha2)
        //(char[] inputt, char[] alpha1, char[] alpha2)
        {
            inputt = inputt.ToUpper();
            alpha1 = alpha1.ToUpper();
            alpha2 = alpha2.ToUpper();
            if (alpha1.Length!=alpha2.Length)
            {
                MessageBox.Show("Абуките трябва да са с еднаква дължина!");
            }
            int position = 0;
            string result = "";
            for (int j = 0; j < inputt.Length; j++)
            {
                for (int i = 0; i < alpha1.Length; i++)
                {
                    if (inputt[j] == alpha1[i])
                    {
                        position = i;

                        result += alpha2[position];
                    }
                }
            }
            return result;
        }

        //public static string DirectDecode()
        //{
        //    return "";
        //}

        //-----------------------------------------------------------------------------

        public static string CaesarEncode(string input, string alpha, int num)
        {
            input = input.ToUpper();
            alpha = alpha.ToUpper();
            List<int> nums = new List<int>();
            for (int j = 0; j < input.Length; j++)
            {
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (input[j] == alpha[i])
                    {
                        int alphanum = i + 1;
                        int newnum = (alphanum + num) % alpha.Length;
                        if (newnum == 0)
                        {
                            nums.Add(alpha.Length);
                        }
                        else
                        {
                            nums.Add(newnum);
                        }
                    }
                }
            }

            string result = "";

            for (int i = 0; i < nums.Count; i++)
            {
                for (int j = 0; j < alpha.Length; j++)
                {
                    if (j == nums[i] - 1)
                    {
                        result += alpha[j];
                    }
                }
            }

            return result;
        }

        public static string CaesarDecode(string input, string alpha, int num)
        {
            input = input.ToUpper();
            alpha = alpha.ToUpper();
            List<int> nums = new List<int>();
            for (int j = 0; j < input.Length; j++)
            {
                for (int i = 0; i < alpha.Length; i++)
                {
                    if (input[j] == alpha[i])
                    {
                        int alphanum = i + 1;
                        int newnum = (alphanum - num) % alpha.Length;

                        if (newnum <= 0)
                        {
                            nums.Add(newnum + alpha.Length);
                        }
                        else
                        {
                            nums.Add(newnum);
                        }
                    }
                }
            }

            string result = "";

            for (int i = 0; i < nums.Count; i++)
            {
                for (int j = 0; j < alpha.Length; j++)
                {
                    if (j == nums[i] - 1)
                    {
                        result += alpha[j];
                    }
                }
            }

            return result;
        }

        //-----------------------------------------------------------------------------

        public static string BeaufortEncode(string plain, string key, string alphabet)
        {
            plain = plain.ToUpper();
            key = key.ToUpper();
            alphabet = alphabet.ToUpper();
            string code = "";
            key = FixKeyBeaufort(plain, key);

            //texttocipher
            //alltech
            //hhoalofswepn

            char[,] tabulaRecta = new char[alphabet.Length, alphabet.Length];

            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (i == 0)
                    {
                        tabulaRecta[i, j] = alphabet[j];
                    }
                    else if (j == 0)
                    {
                        tabulaRecta[i, j] = alphabet[i];
                    }
                    else if (i + j <= alphabet.Length - 1)
                    {
                        tabulaRecta[i, j] = alphabet[i + j];
                    }
                    else if (i + j > alphabet.Length - 1)
                    {
                        tabulaRecta[i, j] = alphabet[Math.Abs(alphabet.Length - (i + j))];
                    }

                    //Console.Write(tabulaRecta[i, j] + " ");
                }
                //Console.WriteLine();
            }


            for (int i = 0; i < plain.Length; i++)
            {
                int col = 0;
                if (plain[i] == 'Ь')
                {
                    col = 27;
                }
                else if (plain[i] == 'Ю')
                {
                    col = 28;
                }
                else if (plain[i] == 'Я')
                {
                    col = 29;
                }
                else
                {
                    col = plain[i] - alphabet[0];
                }

                //Console.WriteLine(col);
                char find = key[i];

                int index = 0;
                for (int j = 0; j < alphabet.Length; j++)
                {
                    if (tabulaRecta[j, col] == find)
                    {
                        //Console.WriteLine(find);
                        index = j;
                        //Console.WriteLine(index);
                        code += tabulaRecta[index, 0];
                        break;
                    }
                }
            }

            return code;
        }

        public static string BeaufortDecode(string code, string key, string alphabet)
        {
            string plain = BeaufortEncode(code, key, alphabet);
            return plain;
        }

        static string FixKeyBeaufort(string plain, string key)
        {
            if (key.Length > plain.Length)
            {
                Console.WriteLine("Дължината на ключа трябва да е по-малка или равна на тази на съобщението!");
                throw new Exception();
            }

            int repeat = plain.Length / key.Length;
            int add = plain.Length % key.Length;
            string realKey = "";
            for (int i = 0; i < repeat; i++)
            {
                realKey += key;
            }
            for (int i = 0; i < add; i++)
            {
                realKey += key[i];
            }

            Console.WriteLine(realKey);
            return realKey;
        }

        //-----------------------------------------------------------------------------

        public static string AutokeyEncode(string plain, string key, string alphabet)
        {
            plain = plain.ToUpper();
            key = key.ToUpper();
            alphabet = alphabet.ToUpper();
            string code = "";

            key += plain;
            key = key.Substring(0, plain.Length);

            code = VigenereEncode(plain, alphabet, key);

            return code;
        }

        public static string AutokeyDecode(string code, string key, string alphabet)
        {
            code = code.ToUpper();
            key = key.ToUpper();
            alphabet = alphabet.ToUpper();
            //WHOLE KEY IS NEEDED!
            string plain = "";

            plain = VigenereDecode(code,key,alphabet);
            

            return plain;
        }

        //static char[,] CreateTabulaAutokey(string alphabet)
        //{
        //    char[,] tabulaRecta = new char[alphabet.Length, alphabet.Length];

        //    for (int i = 0; i < alphabet.Length; i++)
        //    {
        //        for (int j = 0; j < alphabet.Length; j++)
        //        {
        //            if (i == 0)
        //            {
        //                tabulaRecta[i, j] = alphabet[j];
        //            }
        //            else if (j == 0)
        //            {
        //                tabulaRecta[i, j] = alphabet[i];
        //            }
        //            else if (i + j < alphabet.Length)
        //            {
        //                tabulaRecta[i, j] = alphabet[i + j];
        //            }
        //            else if (i + j >= alphabet.Length)
        //            {
        //                tabulaRecta[i, j] = alphabet[Math.Abs(alphabet.Length - (i + j))];
        //            }

        //            Console.Write(tabulaRecta[i, j] + " ");
        //        }
        //        Console.WriteLine();
        //    }
        //    return tabulaRecta;
        //}

        //-----------------------------------------------------------------------------

        public static string TranspBlockEncode(string plainn, string keyy)
        {
            plainn = plainn.ToUpper();
            keyy = keyy.ToUpper();
            string result = "";

            int blocks = plainn.Length / keyy.Length;
            if (plainn.Length % keyy.Length != 0)
            {
                blocks++;
            }

            char[] plain = FixPlainTransp(plainn, keyy, blocks);


            int[] key = KeyLetToNum(keyy);
            int[] temp = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                temp[i] = key[i];
            }

            int blocksize = key.Length;

            List<char[]> list = new List<char[]>();

            for (int i = 0; i < plain.Length; i += blocksize)
            {
                char[] part = new char[blocksize];

                Array.Copy(plain, i, part, 0, blocksize);
                list.Add(part);
            }

            for (int i = 0; i < list.Count; i++)
            {
                Array.Sort(key, list[i]);

                for (int j = 0; j < key.Length; j++)
                {
                    key[j] = temp[j];
                }
                string fpart = new string(list[i]);
                result += fpart;
            }

            return result;
        }

        public static string TranspBlockDecode(string ress, string keyy)
        {
            ress = ress.ToUpper();
            keyy = keyy.ToUpper();
            string result = "";

            int blocks = ress.Length / keyy.Length;
            if (ress.Length % keyy.Length != 0)
            {
                blocks++;
            }

            char[] res = FixPlainTransp(ress, keyy, blocks);

            int[] key = KeyLetToNum(keyy);
            int blocksize = key.Length;

            List<char[]> list = new List<char[]>();

            for (int i = 0; i < res.Length; i += blocksize)
            {
                char[] part = new char[blocksize];

                Array.Copy(res, i, part, 0, blocksize);
                list.Add(part);
            }

            int[] newkey = new int[key.Length];
            for (int j = 0; j < key.Length; j++)
            {
                for (int k = 0; k < key.Length; k++)
                {
                    if (k + 1 == key[j])
                    {
                        newkey[k] = j + 1;
                    }
                }
            }
            int[] temp = new int[newkey.Length];
            for (int i = 0; i < newkey.Length; i++)
            {
                temp[i] = newkey[i];
            }

            for (int i = 0; i < list.Count; i++)
            {
                Array.Sort(newkey, list[i]);

                for (int j = 0; j < key.Length; j++)
                {
                    newkey[j] = temp[j];
                }
                string fpart = new string(list[i]);
                result += fpart;
            }

            return result;
        }

        public static string TranspFormatEncode(string plainn, string keyy)
        {
            plainn = plainn.ToUpper();
            keyy = keyy.ToUpper();
            string result1 = "";

            int blocks = plainn.Length / keyy.Length;
            if (plainn.Length % keyy.Length != 0)
            {
                blocks++;
            }

            char[] plain = FixPlainTransp(plainn, keyy, blocks);


            int[] key = KeyLetToNum(keyy);

            int[] temp = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                temp[i] = key[i];
            }

            int blocksize = key.Length;

            List<char[]> list = new List<char[]>();

            for (int i = 0; i < plain.Length; i += blocksize)
            {
                char[] part = new char[blocksize];

                Array.Copy(plain, i, part, 0, blocksize);
                list.Add(part);
            }

            for (int i = 0; i < list.Count; i++)
            {
                Array.Sort(key, list[i]);

                for (int j = 0; j < key.Length; j++)
                {
                    key[j] = temp[j];
                }
                string fpart = new string(list[i]);
                result1 += fpart;
            }
            //Console.WriteLine(result1);

            char[] fin = result1.ToCharArray();
            string result2 = "";

            for (int j = 0; j < fin.Length / list.Count; j++)
            {
                for (int i = j; i < fin.Length; i += fin.Length / list.Count)
                {
                    result2 += fin[i];
                }
            }
            return result2;
        }

        public static string TranspFormatDecode(string ress, string keyy)
        {
            ress = ress.ToUpper();
            keyy = keyy.ToUpper();
            List<char[]> list = new List<char[]>();
            int[] key = KeyLetToNum(keyy);

            int blocks = ress.Length / keyy.Length;
            if (ress.Length % keyy.Length != 0)
            {
                blocks++;
            }

            char[] inputt = FixPlainTransp(ress, keyy, blocks);

            int blocksize = key.Length;
            int num = inputt.Length / blocksize;

            int[] newkey = new int[key.Length];
            for (int j = 0; j < key.Length; j++)
            {
                for (int k = 0; k < key.Length; k++)
                {
                    if (k + 1 == key[j])
                    {
                        newkey[k] = j + 1;
                    }
                }
            }
            
            int count = 0;
            char[,] field = new char[blocksize, num];
            for (int i = 0; i < blocksize; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    field[i, j] = inputt[count++];
                }
            }

            string work = "";
            for (int j = 0; j < num; j++)
            {
                for (int i = 0; i < blocksize; i++)
                {
                    char[] part = new char[blocksize];
                    part[i] = field[i, j];
                    work += part[i];
                }
            }
            char[] workchar = work.ToCharArray();

            int[] temp = new int[newkey.Length];
            for (int i = 0; i < newkey.Length; i++)
            {
                temp[i] = newkey[i];
            }

            for (int i = 0; i < work.Length; i += blocksize)
            {
                char[] part = new char[blocksize];

                Array.Copy(workchar, i, part, 0, blocksize);
                list.Add(part);
            }

            string result = "";
            for (int i = 0; i < list.Count; i++)
            {
                Array.Sort(newkey, list[i]);

                for (int j = 0; j < newkey.Length; j++)
                {
                    newkey[j] = temp[j];
                }
                string fpart = new string(list[i]);
                result += fpart;
            }
            return result;
        }

        static char[] FixPlainTransp(string inputt, string keyy, int blocks)
        {
            int additionalblocks = keyy.Length * blocks - inputt.Length;
            char[] plain = new char[inputt.Length + additionalblocks];

            for (int i = 0; i < inputt.Length; i++)
            {
                plain[i] = inputt[i];
            }
            for (int i = inputt.Length; i < plain.Length; i++)
            {
                plain[i] = '.';
            }

            return plain;
        }

        static int[] KeyLetToNum(string key)
        {
            string keyorder = String.Concat(key.OrderBy(c => c));
            char[] order = keyorder.ToCharArray();
            int[] result = new int[order.Length];

            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < order.Length; j++)
                {
                    if (key[i] == order[j])
                    {
                        result[i] = j + 1;
                    }
                }
            }
            return result;
        }

        //-----------------------------------------------------------------------------

        public static string RailFenceEncode(string plain, int rails)
        {
            plain = plain.ToUpper();
            string code = "";
            int plainLength = plain.Length;
            char[,] fence = new char[rails, plainLength];
            bool directionFlag = false;
            int counter = 0;

            for (int i = 0; i < plainLength; i++)
            {
                if (counter == 0 || counter == rails - 1)
                {
                    directionFlag = !directionFlag;
                }

                fence[counter, i] = plain[i];

                if (directionFlag)
                {
                    counter++;
                }
                else
                {
                    counter--;
                }

            }

            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < plainLength; j++)
                {
                    if (fence[i, j] != 0)
                    {
                        code += fence[i, j];
                    }
                    //Console.Write(fence[i, j] + " ");
                }
                //Console.WriteLine();
            }

            return code;
        }

        public static string RailFenceDecode(string code, int rails)
        {
            code = code.ToUpper();
            string plain = "";
            int codeLength = code.Length;
            char[,] fence = new char[rails, codeLength];
            bool directionFlag = false;
            int counter = 0;

            for (int i = 0; i < codeLength; i++)
            {
                if (counter == 0 || counter == rails - 1)
                {
                    directionFlag = !directionFlag;
                }

                fence[counter, i] = '*';

                if (directionFlag)
                {
                    counter++;
                }
                else
                {
                    counter--;                 //fix it goes to -1 - done I guess?   
                }

            }

            int index = 0;
            for (int i = 0; i < rails; i++)
            {
                for (int j = 0; j < codeLength; j++)
                {
                    if (fence[i, j] == '*' && index < codeLength)
                    {
                        fence[i, j] = code[index];
                        index++;
                    }
                    //Console.Write(fence[i, j] + " ");
                }
                //Console.WriteLine();
            }

            counter = 0;
            directionFlag = false;
            for (int i = 0; i < codeLength; i++)
            {
                if (counter == 0 || counter == rails - 1)
                {
                    directionFlag = !directionFlag;
                }

                //if (counter < 0)
                //{
                //    counter = 0;
                //}
                plain += fence[counter, i];

                if (directionFlag)
                {
                    counter++;
                }
                else
                {
                    counter--;
                }
            }

            return plain;
        }

        //-----------------------------------------------------------------------------

        public static string SpiralEncode(string plain, int gridH, int gridW, int start, int direction)
        {
            plain = plain.ToUpper();
            string code = "";
            char[,] grid = new char[gridH, gridW];
            int gridSpaces = gridW * gridH;
            if (gridSpaces < plain.Length)
            {
                MessageBox.Show("Нужни са по-големи височина/ширина или по-малка дължина на входното съобщение!"); //fix
            }
            else if (gridSpaces > plain.Length)
            {
                int add = gridSpaces - plain.Length;
                if (add == 1)
                {
                    plain += 'J';
                }
                if (add > 1)
                {
                    plain += 'J';
                    for (int i = 0; i < add - 1; i++)
                    {
                        plain += 'X';
                    }
                }
            }

            //Console.WriteLine(plain);

            int counter = 0;
            for (int i = 0; i < gridW; i++)
            {
                for (int j = 0; j < gridH; j++)
                {
                    grid[j, i] = plain[counter];
                    counter++;
                }
            }

            //for (int i = 0; i < gridW; i++)
            //{
            //    for (int j = 0; j < gridH; j++)
            //    {
            //        Console.Write(grid[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            if (direction == 1)
            {
                code = PrintSpiralCounterClockwise(grid, gridW, gridH, start);
            }
            else if (direction == 2)
            {
                code = PrintSpiralClockwise(grid, gridW, gridH, start);
            }

            //wearediscoveredfleeatonce
            //X = 9  Y = 3
            //start = 2   dir = 2
            //EJXCTEDECDAEWRIORFEONALEVSE

            //I V O e  4 3  4 1
            //l e u s
            //o y d i

            return code;
        }

        public static string SpiralDecode(string code, int gridH, int gridW, int start, int direction)
        {
            code = code.ToUpper();
            string plain = "";

            if (direction == 1)
            {
                plain = FromSpiralToStringCounterClock(code, gridW, gridH, start);
            }
            else if (direction == 2)
            {
                plain = FromSpiralToStringClock(code, gridW, gridH, start);
            }
            
            return plain;
        }

        static String PrintSpiralClockwise(char[,] grid, int gridW, int gridH, int start)
        {
            string spiral = "";

            int T = 0;
            int B = gridH - 1;
            int L = 0;
            int R = gridW - 1;

            int dir = 0;

            while (T <= B && L <= R)
            {
                if (start == 1 && dir == 0 ||
                    start == 2 && dir == 3 ||
                    start == 3 && dir == 1 ||
                    start == 4 && dir == 2)
                {
                    for (int i = L; i <= R; i++)
                    {
                        spiral += grid[T, i];
                    }
                    T++;
                }
                else if (start == 1 && dir == 1 ||
                    start == 2 && dir == 0 ||
                    start == 3 && dir == 2 ||
                    start == 4 && dir == 3)
                {
                    for (int i = T; i <= B; i++)
                    {
                        spiral += grid[i, R];
                    }
                    R--;
                }
                else if (start == 1 && dir == 2 ||
                    start == 2 && dir == 1 ||
                    start == 3 && dir == 3 ||
                    start == 4 && dir == 0)
                {
                    for (int i = R; i >= L; i--)
                    {
                        spiral += grid[B, i];
                    }
                    B--;
                }
                else if (start == 1 && dir == 3 ||
                    start == 2 && dir == 2 ||
                    start == 3 && dir == 0 ||
                    start == 4 && dir == 1)
                {
                    for (int i = B; i >= T; i--)
                    {
                        spiral += grid[i, L];
                    }
                    L++;
                }
                dir = (dir + 1) % 4;
            }

            return spiral;
        }


        static String PrintSpiralCounterClockwise(char[,] grid, int gridW, int gridH, int start)
        {
            string spiral = "";

            int T = 0;
            int B = gridH - 1;
            int L = 0;
            int R = gridW - 1;

            int dir = 0;

            while (T <= B && L <= R)
            {
                if (start == 1 && dir == 0 ||
                    start == 2 && dir == 1 ||
                    start == 3 && dir == 3 ||
                    start == 4 && dir == 2)
                {
                    for (int i = T; i <= B; i++)
                    {
                        spiral += grid[i, L];
                    }
                    L++;
                }
                else if (start == 1 && dir == 1 ||
                    start == 2 && dir == 2 ||
                    start == 3 && dir == 0 ||
                    start == 4 && dir == 3)
                {
                    for (int i = L; i <= R; i++)
                    {
                        spiral += grid[B, i];
                    }
                    B--;
                }
                else if (start == 1 && dir == 2 ||
                    start == 2 && dir == 3 ||
                    start == 3 && dir == 1 ||
                    start == 4 && dir == 0)
                {
                    for (int i = B; i >= T; i--)
                    {
                        spiral += grid[i, R];
                    }
                    R--;
                }
                else if (start == 1 && dir == 3 ||
                    start == 2 && dir == 0 ||
                    start == 3 && dir == 2 ||
                    start == 4 && dir == 1)
                {
                    for (int i = R; i >= L; i--)
                    {
                        spiral += grid[T, i];
                    }
                    T++;
                }
                dir = (dir + 1) % 4;
            }

            return spiral;
        }

        static String FromSpiralToStringClock(string code, int gridW, int gridH, int start)
        {
            string plain = "";

            char[,] grid = new char[gridH, gridW];

            int codeIndex = 0;

            int T = 0;
            int B = gridH - 1;
            int L = 0;
            int R = gridW - 1;

            int dir = 0;

            while (T <= B && L <= R)
            {
                if (start == 1 && dir == 0 ||
                    start == 2 && dir == 3 ||
                    start == 3 && dir == 1 ||
                    start == 4 && dir == 2)
                {
                    for (int i = L; i <= R; i++)
                    {
                        grid[T, i] = code[codeIndex];
                        codeIndex++;
                    }
                    T++;
                }
                else if (start == 1 && dir == 1 ||
                    start == 2 && dir == 0 ||
                    start == 3 && dir == 2 ||
                    start == 4 && dir == 3)
                {
                    for (int i = T; i <= B; i++)
                    {
                        grid[i, R] = code[codeIndex];
                        codeIndex++;
                    }
                    R--;
                }
                else if (start == 1 && dir == 2 ||
                    start == 2 && dir == 1 ||
                    start == 3 && dir == 3 ||
                    start == 4 && dir == 0)
                {
                    for (int i = R; i >= L; i--)
                    {
                        grid[B, i] = code[codeIndex];
                        codeIndex++;
                    }
                    B--;
                }
                else if (start == 1 && dir == 3 ||
                    start == 2 && dir == 2 ||
                    start == 3 && dir == 0 ||
                    start == 4 && dir == 1)
                {
                    for (int i = B; i >= T; i--)
                    {
                        grid[i, L] = code[codeIndex];
                        codeIndex++;
                    }
                    L++;
                }
                dir = (dir + 1) % 4;
            }

            for (int i = 0; i < gridH; i++)
            {
                for (int j = 0; j < gridW; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < gridW; i++)
            {
                for (int j = 0; j < gridH; j++)
                {
                    plain += grid[j, i];
                }
            }

            return plain;
        }

        static String FromSpiralToStringCounterClock(string code, int gridW, int gridH, int start)
        {
            string plain = "";

            char[,] grid = new char[gridH, gridW];

            int codeIndex = 0;

            int T = 0;
            int B = gridH - 1;
            int L = 0;
            int R = gridW - 1;

            int dir = 0;

            while (T <= B && L <= R)
            {
                if (start == 1 && dir == 0 ||
                    start == 2 && dir == 1 ||
                    start == 3 && dir == 3 ||
                    start == 4 && dir == 2)
                {
                    for (int i = T; i <= B; i++)
                    {
                        grid[i, L] = code[codeIndex];
                        codeIndex++;
                    }
                    L++;
                }
                else if (start == 1 && dir == 1 ||
                    start == 2 && dir == 2 ||
                    start == 3 && dir == 0 ||
                    start == 4 && dir == 3)
                {
                    for (int i = L; i <= R; i++)
                    {
                        grid[B, i] = code[codeIndex];
                        codeIndex++;
                    }
                    B--;
                }
                else if (start == 1 && dir == 2 ||
                    start == 2 && dir == 3 ||
                    start == 3 && dir == 1 ||
                    start == 4 && dir == 0)
                {
                    for (int i = B; i >= T; i--)
                    {
                        grid[i, R] = code[codeIndex];
                        codeIndex++;
                    }
                    R--;
                }
                else if (start == 1 && dir == 3 ||
                    start == 2 && dir == 0 ||
                    start == 3 && dir == 2 ||
                    start == 4 && dir == 1)
                {
                    for (int i = R; i >= L; i--)
                    {
                        grid[T, i] = code[codeIndex];
                        codeIndex++;
                    }
                    T++;
                }
                dir = (dir + 1) % 4;
            }

            for (int i = 0; i < gridH; i++)
            {
                for (int j = 0; j < gridW; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < gridW; i++)
            {
                for (int j = 0; j < gridH; j++)
                {
                    plain += grid[j, i];
                }
            }

            return plain;
        }

        //-----------------------------------------------------------------------------

    }
}
