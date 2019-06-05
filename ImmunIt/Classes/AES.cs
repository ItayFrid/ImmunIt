﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ImmunIt.Classes
{
    public class AES
    {
        //Fields
        private byte[,] sBox = {
            { 0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76 },
            { 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0},
            { 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15},
            { 0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75},
            { 0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84},
            { 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf},
            { 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8},
            { 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2},
            { 0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73},
            { 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb},
            { 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79},
            { 0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08},
            { 0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a},
            { 0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e},
            { 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf},
            { 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16},
        };
        private byte[,] invSBox = {
            { 0x52, 0x09, 0x6A, 0xD5, 0x30, 0x36, 0xA5, 0x38, 0xBF, 0x40, 0xA3, 0x9E, 0x81, 0xF3, 0xD7, 0xFB },
            { 0x7C, 0xE3, 0x39, 0x82, 0x9B, 0x2F, 0xFF, 0x87, 0x34, 0x8E, 0x43, 0x44, 0xC4, 0xDE, 0xE9, 0xCB },
            { 0x54, 0x7B, 0x94, 0x32, 0xA6, 0xC2, 0x23, 0x3D, 0xEE, 0x4C, 0x95, 0x0B, 0x42, 0xFA, 0xC3, 0x4E },
            { 0x08, 0x2E, 0xA1, 0x66, 0x28, 0xD9, 0x24, 0xB2, 0x76, 0x5B, 0xA2, 0x49, 0x6D, 0x8B, 0xD1, 0x25 },
            { 0x72, 0xF8, 0xF6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xD4, 0xA4, 0x5C, 0xCC, 0x5D, 0x65, 0xB6, 0x92 },
            { 0x6C, 0x70, 0x48, 0x50, 0xFD, 0xED, 0xB9, 0xDA, 0x5E, 0x15, 0x46, 0x57, 0xA7, 0x8D, 0x9D, 0x84 },
            { 0x90, 0xD8, 0xAB, 0x00, 0x8C, 0xBC, 0xD3, 0x0A, 0xF7, 0xE4, 0x58, 0x05, 0xB8, 0xB3, 0x45, 0x06 },
            { 0xD0, 0x2C, 0x1E, 0x8F, 0xCA, 0x3F, 0x0F, 0x02, 0xC1, 0xAF, 0xBD, 0x03, 0x01, 0x13, 0x8A, 0x6B },
            { 0x3A, 0x91, 0x11, 0x41, 0x4F, 0x67, 0xDC, 0xEA, 0x97, 0xF2, 0xCF, 0xCE, 0xF0, 0xB4, 0xE6, 0x73 },
            { 0x96, 0xAC, 0x74, 0x22, 0xE7, 0xAD, 0x35, 0x85, 0xE2, 0xF9, 0x37, 0xE8, 0x1C, 0x75, 0xDF, 0x6E },
            { 0x47, 0xF1, 0x1A, 0x71, 0x1D, 0x29, 0xC5, 0x89, 0x6F, 0xB7, 0x62, 0x0E, 0xAA, 0x18, 0xBE, 0x1B },
            { 0xFC, 0x56, 0x3E, 0x4B, 0xC6, 0xD2, 0x79, 0x20, 0x9A, 0xDB, 0xC0, 0xFE, 0x78, 0xCD, 0x5A, 0xF4 },
            { 0x1F, 0xDD, 0xA8, 0x33, 0x88, 0x07, 0xC7, 0x31, 0xB1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xEC, 0x5F },
            { 0x60, 0x51, 0x7F, 0xA9, 0x19, 0xB5, 0x4A, 0x0D, 0x2D, 0xE5, 0x7A, 0x9F, 0x93, 0xC9, 0x9C, 0xEF },
            { 0xA0, 0xE0, 0x3B, 0x4D, 0xAE, 0x2A, 0xF5, 0xB0, 0xC8, 0xEB, 0xBB, 0x3C, 0x83, 0x53, 0x99, 0x61 },
            { 0x17, 0x2B, 0x04, 0x7E, 0xBA, 0x77, 0xD6, 0x26, 0xE1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0C, 0x7D }
        };
        private byte[] RS = {
            0x8d, 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1b, 0x36
        };
        //Casting Methods
        private int HexCharToInt(char c)
        {
            switch (c)
            {
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;
                case 'A':
                case 'a':
                    return 10;
                case 'B':
                case 'b':
                    return 11;
                case 'C':
                case 'c':
                    return 12;
                case 'D':
                case 'd':
                    return 13;
                case 'E':
                case 'e':
                    return 14;
                case 'F':
                case 'f':
                    return 15;
                case '0':
                default:
                    return 0;
            }
        }
        private byte[,] StringToByte(string text)
        {
            int i, j;
            //Add Padding to the end of the block
            int textSize = text.Length;
            if (textSize < 16)
                for (i = 0; i < 16 - textSize; i++)
                    text += "0";

            i = 0;
            byte[,] byteArray = new byte[4, 4];
            byte[] textByte = Encoding.Default.GetBytes(text);
            for (j = 0; j < 16; j++)
            {
                if (j % 4 == 0 && j != 0)
                    i++;
                byteArray[j % 4, i] = textByte[j];
            }
            return byteArray;
        }
        private string ByteArrayToString(byte[,] byteText)
        {
            string s = "";
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (byteText[j, i].ToString("X").Length < 2)
                        s += ",";
                    s += byteText[j, i].ToString("X");
                }
            return s;
        }
        private byte[,] StringToByteArray(string text)
        {
            List<string> arr = text.Select((x, i) => i)
                                    .Where(i => i % 2 == 0)
                                    .Select(i => String.Concat(text.Skip(i).Take(2)))
                                    .ToList();
            byte[,] byteText = new byte[4, 4];
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (arr[index].Contains(","))
                        arr[index] = arr[index].Trim(new char[] { ',' });
                    int val = Convert.ToInt32(arr[index], 16);
                    byteText[j, i] = (byte)val;
                    index++;
                }
            }

            return byteText;
        }
        private string HextoString(string InputText)
        {
            InputText = InputText.Replace(',', '0');
            byte[] bb = Enumerable.Range(0, InputText.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(InputText.Substring(x, 2), 16))
                             .ToArray();
            return System.Text.Encoding.ASCII.GetString(bb);
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

        private byte[,] XOR(byte[,] b1, byte[,] b2)
        {
            byte[,] newB = new byte[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    int x = b1[i, j] ^ b2[i, j];
                    newB[i, j] = (byte)x;
                }
            return newB;
        }

        private byte[,] ColumnXOR(byte[,] byteKey, int i1, int i2)
        {
            for (int j = 0; j < 4; j++)
                byteKey[j, i1] = (byte)(byteKey[j, i1] ^ byteKey[j, i2]);
            return byteKey;
        }

        private byte[,] S(byte[,] byteText)
        {
            byte[,] b = new byte[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    b[j, i] = sPhase(byteText[j, i]);
                }
            return b;
        }

        private byte[,] invS(byte[,] byteText)
        {
            byte[,] b = new byte[4, 4];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    b[j, i] = invSPhase(byteText[j, i]);
                }
            return b;
        }

        private byte sPhase(byte b)
        {
            int iS, jS;
            //Getting HexValue
            char[] hex = (b.ToString("X") + "").ToCharArray();
            if (hex.Length == 2)
            {
                iS = HexCharToInt(hex[0]);
                jS = HexCharToInt(hex[1]);
            }
            else
            {
                iS = 0;
                jS = HexCharToInt(hex[0]);
            }
            return sBox[iS, jS];
        }

        private byte invSPhase(byte b)
        {
            int iS, jS;
            //Getting HexValue
            char[] hex = (b.ToString("X") + "").ToCharArray();
            if (hex.Length == 2)
            {
                iS = HexCharToInt(hex[0]);
                jS = HexCharToInt(hex[1]);
            }
            else
            {
                iS = 0;
                jS = HexCharToInt(hex[0]);
            }
            return invSBox[iS, jS];
        }

        private byte[,] LeftShift(byte[,] byteText, int i)
        {
            byte temp = byteText[i, 0];
            for (int j = 0; j < 3; j++)
                byteText[i, j] = byteText[i, j + 1];
            byteText[i, 3] = temp;
            return byteText;
        }

        private byte[,] RightShift(byte[,] byteText, int i)
        {
            byte temp = byteText[i, 3];
            for (int j = 3; j > 0; j--)
                byteText[i, j] = byteText[i, j - 1];
            byteText[i, 0] = temp;
            return byteText;
        }

        private byte[,] RightShiftRow(byte[,] byteText)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < i; j++)
                    byteText = RightShift(byteText, i);
            return byteText;
        }

        private byte[,] LeftShiftRow(byte[,] byteText)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < i; j++)
                    byteText = LeftShift(byteText, i);
            return byteText;
        }

        private byte GMul(byte a, byte b)
        { // Galois Field (256) Multiplication of two Bytes
            byte p = 0;
            for (int counter = 0; counter < 8; counter++)
            {
                if ((b & 1) != 0)
                    p ^= a;
                bool highBit = (a & 0x80) != 0;
                a <<= 1;
                if (highBit)
                    a ^= 0x1B;
                b >>= 1;
            }
            return p;
        }

        private byte[,] MixColumns(byte[,] s)
        {
            byte[,] ss = new byte[4, 4];
            for (int c = 0; c < 4; c++)
            {
                ss[0, c] = (byte)(GMul(0x02, s[0, c]) ^ GMul(0x03, s[1, c]) ^ s[2, c] ^ s[3, c]);
                ss[1, c] = (byte)(s[0, c] ^ GMul(0x02, s[1, c]) ^ GMul(0x03, s[2, c]) ^ s[3, c]);
                ss[2, c] = (byte)(s[0, c] ^ s[1, c] ^ GMul(0x02, s[2, c]) ^ GMul(0x03, s[3, c]));
                ss[3, c] = (byte)(GMul(0x03, s[0, c]) ^ s[1, c] ^ s[2, c] ^ GMul(0x02, s[3, c]));
            }
            return ss;
        }

        private byte[,] invMixColumns(byte[,] s)
        {
            byte[,] ss = new byte[4, 4];
            for (int c = 0; c < 4; c++)
            {
                ss[0, c] = (byte)(GMul(0x0E, s[0, c]) ^
                                  GMul(0x0B, s[1, c]) ^
                                  GMul(0x0D, s[2, c]) ^
                                  GMul(0x09, s[3, c]));

                ss[1, c] = (byte)(GMul(0x09, s[0, c]) ^
                                  GMul(0x0E, s[1, c]) ^
                                  GMul(0x0B, s[2, c]) ^
                                  GMul(0x0D, s[3, c]));

                ss[2, c] = (byte)(GMul(0x0D, s[0, c]) ^
                                  GMul(0x09, s[1, c]) ^
                                  GMul(0x0E, s[2, c]) ^
                                  GMul(0x0B, s[3, c]));

                ss[3, c] = (byte)(GMul(0x0B, s[0, c]) ^
                                  GMul(0x0D, s[1, c]) ^
                                  GMul(0x09, s[2, c]) ^
                                  GMul(0x0E, s[3, c]));
            }
            return ss;
        }

        private byte[,] G(byte[,] byteKey, int i, int iRS)
        {
            byte[,] newG = new byte[4, 4];
            //switch Locations
            for (int j = 0; j < 3; j++)
                newG[j, i] = byteKey[j + 1, i];
            newG[3, i] = byteKey[0, i];
            //Applying sBox
            for (int j = 0; j < 4; j++)
                newG[j, i] = sPhase(newG[j, i]);
            //XOR with RS table to the first Byte
            newG[0, i] = (byte)(newG[0, i] ^ RS[iRS]);
            return newG;
        }

        private byte[,] KeyScedule(byte[,] byteKey, int i)
        {
            byteKey = G(byteKey, 3, i);
            byteKey = ColumnXOR(byteKey, 0, 3);
            byteKey = ColumnXOR(byteKey, 1, 0);
            byteKey = ColumnXOR(byteKey, 2, 1);
            byteKey = ColumnXOR(byteKey, 3, 2);
            return byteKey;
        }

        private byte[,,] ComputeCipherKey(string key)
        {
            //TODO: check Error
            byte[,,] keys = new byte[11, 4, 4];
            byte[,] iterationKey = StringToByte(key);
            for (int i = 0; i < 11; i++)
            {
                iterationKey = KeyScedule(iterationKey, i);
                for (int j = 0; j < 4; j++)
                    for (int k = 0; k < 4; k++)
                        keys[i, j, k] = iterationKey[j, k];
            }

            return keys;
        }

        private byte[,] GetSingleKey(byte[,,] keys, int i)
        {
            byte[,] key = new byte[4, 4];
            for (int j = 0; j < 4; j++)
                for (int k = 0; k < 4; k++)
                    key[j, k] = keys[i, j, k];
            return key;
        }

        private string EncryptSingleBlock(string text, string key)
        {
            //Casting Strings To 4x4 Bytes Array
            byte[,] byteText = StringToByte(text);
            byte[,] byteKey = StringToByte(key);

            byteKey = KeyScedule(byteKey, 0);   // Key Schedule 0
            byteText = XOR(byteText, byteKey);  // Key Addition 0
            //Iterations
            for (int i = 1; i <= 9; i++)
            {
                byteText = S(byteText);                 // Byte Substitution
                //Diffusion
                byteText = LeftShiftRow(byteText);      // Left Shift Row
                byteText = MixColumns(byteText);         // Mix Column

                byteKey = KeyScedule(byteKey, i);       // Key Schedule i
                byteText = XOR(byteText, byteKey);      // Key Addition i
            }
            //Final Round
            byteText = S(byteText);                 // Byte Substitution
            byteText = LeftShiftRow(byteText);      // Left Shift Row
            byteKey = KeyScedule(byteKey, 10);      // Key Schedule 10
            byteText = XOR(byteText, byteKey);      // Key Addition 10

            //Castring To String
            string cipherText = ByteArrayToString(byteText);

            return cipherText;
        }

        private string DecryptSingleBlock(string cipherText, string Key)
        {
            //PreComputing The Keys
            byte[,,] keys = ComputeCipherKey(Key);
            //Casting Strings To 4x4 Bytes Array
            byte[,] byteText = StringToByteArray(cipherText);
            byte[,] iterationKey = GetSingleKey(keys, 10);
            //Final Round
            byteText = XOR(byteText, iterationKey); // Key Addition 10
            byteText = RightShiftRow(byteText);     // ShiftRow
            byteText = invS(byteText);              // Byte Substitution
            //Iterations
            for (int i = 9; i > 1; i--)
            {
                iterationKey = GetSingleKey(keys, i);
                byteText = XOR(byteText, iterationKey); // Key Addition i

                byteText = invMixColumns(byteText);     // Mix Columns
                byteText = RightShiftRow(byteText);     // ShiftRow

                byteText = invS(byteText);              // Byte Substitution
            }
            //First Round
            iterationKey = GetSingleKey(keys, 1);
            byteText = XOR(byteText, iterationKey); // Key Addition 1
            byteText = invMixColumns(byteText);     // Mix Columns
            byteText = RightShiftRow(byteText);     // ShiftRow
            byteText = invS(byteText);              // Byte Substitution
            iterationKey = GetSingleKey(keys, 0);
            byteText = XOR(byteText, iterationKey); // Key Addition 0
            return HextoString(ByteArrayToString(byteText));
        }

        public string Encrypt(string text)
        {
            string cipherText = "";
            List<string> arr;
            //Generate Random 16 Byte Password
            string key = RandomKey(16);
            //Split The Text To 16 Byte Blocks
            arr = text.Select((x, i) => i)
                      .Where(i => i % 16 == 0)
                      .Select(i => String.Concat(text.Skip(i).Take(16)))
                      .ToList();
            //Encrypt Phase
            foreach (string s in arr)
                cipherText += EncryptSingleBlock(s, key);
            cipherText += ":" + ByteArrayToString(StringToByte(key));
            return cipherText;
        }

        public string Decrypt(string cipherText)
        {
            string text, key, plainText = "";
            List<string> arr;
            text = cipherText.Split(':')[0];
            key = HextoString(cipherText.Split(':')[1]);

            arr = text.Select((x, i) => i)
                      .Where(i => i % 32 == 0)
                      .Select(i => String.Concat(text.Skip(i).Take(32)))
                      .ToList();
            foreach (string s in arr)
                plainText += DecryptSingleBlock(s, key);
            return TrimEnd(plainText);
        }

        private string TrimEnd(string plainText)
        {
            char[] arr = plainText.ToCharArray();
            int i = arr.Length - 1, j = 0;
            while (arr[i] == '0') { j++; i--; }
            return plainText.Remove(plainText.Length - j);
        }

    }
}