using System;
using static System.ConsoleColor;
using AMLR = Lab4.ArrayMethodLessRealization;
using AMFR = Lab4.ArrayMethodFulRealization;
using ALR = Lab4.ArrayLessRealization;

namespace Lab4
{
    static class Custom
    {
        public static Type ReadLine<Type>(Converter<string, Type> convert, bool antiMoron, ConsoleColor color = White, bool check = false) => ReadLine(convert, antiMoron, "", White, color, check);
        public static Type ReadLine<Type>(Converter<string, Type> convert, bool antiMoron, string antiMoronMsg, ConsoleColor antiMoronMsgColor, ConsoleColor color = White, bool check = false)
        {
            string input;
            void Do()
            {
                if (check) Console.Write(">");
                Console.ForegroundColor = color;
                input = Console.ReadLine();
                Console.ResetColor();
            }
            if (antiMoron)
            {
                do
                {
                    Do();
                    try { return convert(input); }
                    catch { Custom.WriteColored(antiMoronMsg + (antiMoronMsg == "" ? ("") : ("\n")), antiMoronMsgColor); }
                } while (true);
            }
            else
            {
                Do();
                try { return convert(input); }
                catch { throw new ArgumentException("wrong input type"); }
            }
        }
        public static string ReadLine(ConsoleColor color, bool check = false)
        {
            if (check) Console.Write(">");
            Console.ForegroundColor = color;
            string output = Console.ReadLine();
            Console.ResetColor();
            return output;
        }
        public static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void WriteColored(params object[] input)
        {
            for (int i = 0; i < input.Length; i += 2)
            {
                Console.ForegroundColor = (ConsoleColor)input[i + 1];
                Console.Write(Convert.ToString(input[i]));
            }
            Console.ResetColor();
        }
    }
    static class ArrayMethodLessRealization 
    {

    }
    static class ArrayMethodFulRealization 
    {

    }
    static class ArrayLessRealization 
    {

        static public byte SelectInputType() 
        {            
            Custom.WriteColored("Виберіть тип введення:\n", White, "0", Yellow, " - заповнити масив випадковими числами.\n", White, "1", Yellow, " - заповнити масив числами в рядок через пробіл.\n", White, "2", Yellow, " - заповнити масив в стовпчик через ", White, "Enter\n", Yellow);
            start:
            var x = Custom.ReadLine(Convert.ToByte, true, "Неправильний тип введення", Red, White, true);
            if (x == 0 || x == 1 || x == 2)
                return x;
            else
            {
                Custom.WriteColored("Неправильний тип введення\n", Red);
                goto start;
            }
        }
        static public byte SelectArrayMethodsUsage() 
        {            
            Custom.WriteColored("Виберіть тип виконання програми:\n", White, "0",Yellow," - Без методів классу Array стандартої бібліотеки.\n",White,"1",Yellow," - З методами классу Array стандартої бібліотеки.\n", White);
            start:
            var x = Custom.ReadLine(Convert.ToByte, true, "Неправильний тип введення",Red , White, true);
            if (x == 0 || x == 1)
                return x;
            else
            {
                Custom.WriteColored("Неправильний тип введення\n", Red);
                goto start;
            }
        }
    }    
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            ALR.SelectArrayMethodsUsage();
            ALR.SelectInputType();
            
            
        }
    }
}
