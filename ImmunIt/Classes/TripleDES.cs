using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ImmunIt.Classes
{
    public class TripleDES
    {
        private static readonly int[] IP = {  58, 50, 42, 34, 26, 18, 10, 2,
                                                60, 52, 44, 36, 28, 20, 12, 4,
                                                62, 54, 46, 38, 30, 22, 14, 6,
                                                64, 56, 48, 40, 32, 24, 16, 8,
                                                57, 49, 41, 33, 25, 17, 9, 1,
                                                59, 51, 43, 35, 27, 19, 11, 3,
                                                61, 53, 45, 37, 29, 21, 13, 5,
                                                63, 55, 47, 39, 31, 23, 15, 7
        };
        private static readonly int[] LP = { 40, 8, 48, 16, 56, 24, 64, 32,
                                    39, 7, 47, 15, 55, 23, 63, 31,
                                    38, 6, 46, 14, 54, 22, 62, 30,
                                    37, 5, 45, 13, 53, 21, 61, 29,
                                    36, 4, 44, 12, 52, 20, 60, 28,
                                    35, 3, 43, 11, 51, 19, 59, 27,
                                    34, 2, 42, 10, 50, 18, 58, 26,
                                    33, 1, 41, 9, 49, 17, 57, 25
        };
        private static readonly int[,,] s = {
            {
                {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7},
                {0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8},
                {4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0},
                {15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13}
            },

            {
                {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10},
                {3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5},
                {0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15},
                {13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9}
            },

            {
                {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8},
                {13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1},
                {13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7},
                {1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12}
            },

            {
                {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15},
                {13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9},
                {10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4},
                {3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14}
            },

            {
                {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9},
                {14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6},
                {4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14},
                {11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3}
            },

            {
                {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11},
                {10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8},
                {9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6},
                {4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13}
            },

            {
                {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1},
                {13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6},
                {1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2},
                {6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12}
            },

            {
                {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7},
                {1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2},
                {7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8},
                {2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}
            }
        };
        private static readonly int[] E = { 32, 1, 2, 3, 4, 5,
                                            4, 5, 6, 7, 8, 9,
                                            8, 9, 10, 11, 12, 13,
                                            12, 13, 14, 15, 16, 17,
                                            16, 17, 18, 19, 20, 21,
                                            20, 21, 22, 23, 24, 25,
                                            24, 25, 26, 27, 28, 29,
                                            28, 29, 30, 31, 32, 1
        };
        private static readonly int[] PC1 = {  57, 49, 41, 33, 25, 17, 9,
                                                1, 58, 50, 42, 34, 26, 18,
                                                10, 2, 59, 51, 43, 35, 27,
                                                19, 11, 3, 60, 52, 44, 36,
                                                63, 55, 47, 39, 31, 23, 15,
                                                7, 62, 54, 46, 38, 30, 22,
                                                14, 6, 61, 53, 45, 37, 29,
                                                21, 13, 5, 28, 20, 12, 4
        };
        private static readonly int[] PC2 = {14, 17, 11, 24, 1, 5, 3, 28,
                                    15, 6, 21, 10, 23, 19, 12, 4,
                                    26, 8, 16, 7, 27, 20, 13, 2,
                                    41, 52, 31, 37, 47, 55, 30, 40,
                                    51, 45, 33, 48, 44, 49, 39, 56,
                                    34, 53, 46, 42, 50, 36, 29, 32
        };
        private static readonly int[] P = {16, 7, 20, 21,
                                           29, 12, 28, 17,
                                            1, 15, 23, 26,
                                            5, 18, 31, 10,
                                            2, 8, 24, 14,
                                           32, 27, 3, 9,
                                            19, 13, 30, 6,
                                            22, 11, 4, 25
        };
        private static readonly int[] Shift = {
            1,1,2,2,2,2,2,2,1,2,2,2,2,2,2,1
        };

        private List<string> Keys { get; set; }

        public TripleDES()
        {
            this.Keys = new List<string>();
        }

        //Help Functions
        private List<string> nsplit(string text, int n)
        {
            return text
                    .Select((c, i) => new { c, i })
                    .GroupBy(x => x.i / n)
                    .Select(g => String.Join("", g.Select(y => y.c)))
                    .ToList();
        }

        private string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        private string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();
            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        private List<string> TextToBinary(string text)
        {
            List<string> textList = nsplit(text, 16);
            for (int i = 0; i < textList.Count; i++)
                textList[i] = HexStringToBinaryString(textList[i]);
            for (int i = 0; i < textList.Count; i++)
            {
                if (textList[i].Length < 64)
                {
                    string str = "";
                    int size = 64 - textList[i].Length;
                    for (int j = 0; j < size; j++)
                        str += "0";
                    textList[i] = String.Concat(textList[i], str);
                }
            }
            return textList;
        }

        private string BinaryToText(List<string> binaryList)
        {
            string text = "";
            foreach (string binary in binaryList)
            {
                text += BinaryToString(binary);
            }
            return text;
        }

        private string StringToHex(string text)
        {
            byte[] arr = Encoding.Default.GetBytes(text);
            string hexString = BitConverter.ToString(arr);
            hexString = hexString.Replace("-", "");
            return hexString;
        }

        private string BinaryStringToHexString(string binary)
        {
            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);
            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }
            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        private string HexStringToBinaryString(string hex)
        {
            return String.Join(String.Empty, hex
                .Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2)
                .PadLeft(4, '0')));
        }

        private string HexStringToString(string HexString)
        {
            string stringValue = "";
            for (int i = 0; i < HexString.Length / 2; i++)
            {
                string hexChar = HexString.Substring(i * 2, 2);
                int hexValue = Convert.ToInt32(hexChar, 16);
                stringValue += Char.ConvertFromUtf32(hexValue);
            }
            return stringValue;
        }

        private string RandomKey(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        //Actions
        private string Permute(string binaryStr, int[] permute)
        {
            char[] binary = binaryStr.ToCharArray();
            string newStr = "";
            for (int i = 0; i < permute.Length; i++)
            {
                newStr += binary[permute[i] - 1];
            }
            return newStr;
        }

        private static string leftRotate(string text, int n)
        {
            char[] arr = text.ToCharArray();
            for (int i = 0; i < n; i++)
                arr = leftRotatebyOne(arr);
            return String.Concat(arr);
        }

        private static char[] leftRotatebyOne(char[] arr)
        {
            int i;
            char temp = arr[0];
            for (i = 0; i < arr.Length - 1; i++)
                arr[i] = arr[i + 1];
            arr[i] = temp;
            return arr;
        }

        private void GenerateKeys(string key)
        {
            this.Keys = new List<string>();
            string PermutedKey = Permute(StringToBinary(key), PC1);
            List<string> list = nsplit(PermutedKey, 32);
            List<Tuple<string, string>> tuples = new List<Tuple<string, string>>();
            tuples.Add(new Tuple<string, string>(list[0], list[1]));
            for (int i = 1; i <= 16; i++)
            {
                tuples.Add(new Tuple<string, string>(
                    leftRotate(tuples[i - 1].Item1, Shift[i - 1]),
                    leftRotate(tuples[i - 1].Item2, Shift[i - 1])));
            }
            for (int i = 1; i <= 16; i++)
            {
                string c = tuples[i].Item1;
                string d = tuples[i].Item2;
                this.Keys.Add(Permute(String.Concat(c, d), PC2));
            }
        }

        private string XOR(string str1, string str2)
        {
            char[] arr1 = str1.ToCharArray(), arr2 = str2.ToCharArray();
            string newStr = "";
            for (int i = 0; i < arr1.Length; i++)
            {
                int a = arr1[i] - '0', b = arr2[i] - '0';
                int aXb = a ^ b;
                newStr += Convert.ToString(aXb);
            }
            return newStr;
        }

        private string sCalc(string data, int k)
        {
            char[] arr = data.ToCharArray();
            string iString = String.Concat(arr[0], arr[5]);
            string jString = String.Concat(arr[1], arr[2], arr[3], arr[4]);
            int i = Convert.ToInt32(iString, 2);
            int j = Convert.ToInt32(jString, 2);
            int sVal = s[k, i, j];
            string newStr = Convert.ToString(sVal, 2);
            if (newStr.Length < 4)
            {
                string str = "";
                for (int l = 0; l < 4 - newStr.Length; l++)
                    str += "0";
                newStr = String.Concat(str, newStr);
            }
            return newStr;
        }

        private string F(string data, string key)
        {
            string expandedR = Permute(data, E);
            string keyXExpandedR = XOR(key, expandedR);
            List<string> list = nsplit(keyXExpandedR, 6);
            string newStr = "";
            for (int i = 0; i < list.Count; i++)
            {
                newStr += sCalc(list[i], i);
            }
            newStr = Permute(newStr, P);
            return newStr;
        }

        private string Encrypt(string text, string key)
        {
            List<string> textList = TextToBinary(text);
            string Cipher = "";
            List<string> CipherList = new List<string>();
            GenerateKeys(key);
            for (int i = 0; i < textList.Count; i++)
            {
                string cipherI = "";
                textList[i] = Permute(textList[i], IP);
                List<LeftRight> LR = new List<LeftRight>();
                List<string> splitText = nsplit(textList[i], 32);
                LeftRight LR0 = new LeftRight(splitText[0], splitText[1]);
                LR.Add(LR0);
                for (int j = 1; j <= 16; j++)
                {
                    string L = LR[j - 1].right;
                    string R = XOR(LR[j - 1].left, F(L, Keys[j - 1]));
                    LR.Add(new LeftRight(L, R));
                }
                cipherI = Permute(String.Concat(LR[16].right, LR[16].left), LP);
                CipherList.Add(cipherI);
            }
            foreach (string str in CipherList)
                Cipher += str;
            return BinaryStringToHexString(Cipher);
        }

        public string TripleEncrypt(string text)
        {
            List<string> hexKeys = new List<string>();
            string Cipher = StringToHex(text);
            for (int i = 0; i < 3; i++)
            {
                hexKeys.Add(RandomKey(8));
                System.Threading.Thread.Sleep(10);
            }

            for (int i = 0; i < 3; i++)
            {
                Cipher = Encrypt(Cipher, hexKeys[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                Cipher = String.Concat(Cipher, ":", StringToHex(hexKeys[i]));
            }
            return Cipher;
        }

        public bool isValid(string cipherText, string password)
        {
            string[] list = cipherText.Split(':');
            string Cipher = StringToHex(password);
            for (int i = 1; i <= 3; i++)
            {
                Cipher = Encrypt(Cipher, HexStringToString(list[i]));
            }
            Console.WriteLine(Cipher);
            Console.WriteLine(list[0]);
            return Cipher == list[0];
        }
    }
    public struct LeftRight
    {
        public string left;
        public string right;
        public LeftRight(string l, string r)
        {
            left = l;
            right = r;
        }
    }
}
}