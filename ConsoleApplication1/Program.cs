using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApplication1
{
    class Program
    {
        static int maxlength = 64;
        static string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()_+-={}|\\[]:;\"'<>?/.,";
        static int maxZeros = 0;
        static void Main(string[] args)
        {
            Console.Write("Starting with: ");
            string starting = Console.ReadLine().Trim();
            Dive(starting, 0);
        }

        private static void Dive(string prefix, int level)
        {
            level += 1;
            Parallel.ForEach(validChars, c =>
            {
                byte[] data = Encoding.UTF8.GetBytes(string.Concat(prefix, c));
                string result;
                using (SHA512 shaM = new SHA512Managed())
                {
                    result = (String.Concat((Array.ConvertAll(shaM.ComputeHash(data), x => x.ToString("X2")))));
                }

                int count = 0;
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] != '0')
                        break;

                    count++;
                }

                if (count > maxZeros)
                {
                    Console.WriteLine(string.Concat(prefix, c).Length);
                    Console.WriteLine(string.Concat(prefix, c) + " => " + result);
                    maxZeros = count;
                }

                if (level < maxlength)
                {
                    Dive(prefix + c, level);
                }
            });
        }
    }
}
