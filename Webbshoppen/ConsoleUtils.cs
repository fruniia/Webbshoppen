﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webbshoppen
{
    static class ConsoleUtils
    {
        public static string GetStringFromUser(string prompt)
        {
            Console.Write(prompt);
            string result = Console.ReadLine();
            return result;
        }
        public static int GetIntFromUser(string prompt)
        {
            bool isValid = true;
            int result;
            do
            {
                if (!isValid)
                {
                    Console.WriteLine("Felaktigt värde, testa igen.");
                }
                else isValid = false;

                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out result));

            return result;
        }
        public static float GetFloatFromUser(string prompt)
        {
            bool isValid = true;
            float result;
            do
            {
                if (!isValid)
                {
                    Console.WriteLine("Felaktigt värde, testa igen.");
                }
                else isValid = false;

                Console.Write(prompt);
            } while (!float.TryParse(Console.ReadLine(), out result));

            return result;
        }
     
    }
}