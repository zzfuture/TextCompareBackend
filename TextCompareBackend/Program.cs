using System;
using System.Linq;

namespace TextCompareBackend
{
    class Program
    {
        static void Main(string[] args)
        {
            // var compare = CompareService.GetInstance();

            string[] texts = { "Hello", "Hello" };
            var text1 = texts[0];
            var text2 = texts[1];
            var difference = CompareService.GetTextDifference(text1, text2, out var differenceTexts);
            
            if (!difference) Console.WriteLine("Iguales :D");
            else Console.WriteLine("Diferentes");
            
            // La impresion en consola puede que salga al reves, ya lo arreglo después :D
            for(var i = 0; i < texts.Length; i++)
            {
                Console.WriteLine(string.Join(" ", texts[i].ToCharArray()));
                Console.WriteLine(string.Join(" ", differenceTexts[i]));
            }
            
        }
    }
}