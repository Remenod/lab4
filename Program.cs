using System;
using static System.ConsoleColor;
using static Lab4.Other;
using AMLR = Lab4.ArrayMethodLessRealization;
using AMFR = Lab4.ArrayMethodFulRealization;

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
    class ArrayMethodLessRealization : ITaskContaiter
    {
        public void Task1(byte inputType)
        {
            
        }
        public void Task2(byte inputType)
        {

        }
        public void Task3(byte inputType)
        {

        }
        public void Task4(byte inputType)
        {

        }
        public void Task5(byte inputType)
        {

        }
        public void Task6(byte inputType)
        {

        }
        public void Task7(byte inputType)
        {

        }
        public void Task8(byte inputType)
        {

        }
    }
    class ArrayMethodFulRealization : ITaskContaiter
    {
        public void Task1(byte inputType) 
        {

        }
        public void Task2(byte inputType) 
        {

        }
        public void Task3(byte inputType) 
        {

        }
        public void Task4(byte inputType) 
        {

        }
        public void Task5(byte inputType) 
        {

        }
        public void Task6(byte inputType) 
        {

        }
        public void Task7(byte inputType) 
        {

        }
        public void Task8(byte inputType) 
        {

        }
    }
    static class Other
    {
        public delegate void Task(byte inputType);
        public interface ITaskContaiter 
        {
            public void Task1(byte inputType);
            public void Task2(byte inputType);
            public void Task3(byte inputType);
            public void Task4(byte inputType);
            public void Task5(byte inputType);
            public void Task6(byte inputType);
            public void Task7(byte inputType);
            public void Task8(byte inputType);
        }
        static public void StartTasks()
        {            
            ITaskContaiter TaskContainer = (SelectArrayMethodsUsage() == 0) ? new AMLR() : new AMFR();                     
            Task task = SelectTask() switch {
                1 => TaskContainer.Task1,
                2 => TaskContainer.Task2,
                3 => TaskContainer.Task3,
                4 => TaskContainer.Task4,
                5 => TaskContainer.Task5,
                6 => TaskContainer.Task6,
                7 => TaskContainer.Task7,
                8 => TaskContainer.Task8,
                _ => throw new Exception("Щось пішло не так.")};
            task(SelectInputType());
        }        
        static public byte SelectTask() 
        {
            Custom.WriteColored("Виберіть задачу:\n", White,
                                "1", Yellow, " - знайти добуток елементів масиву, які розміщені перед останнім входженням максимального числа.\n", White,
                                "2", Yellow, " - задача Table з netoi.org.ua.\n", White,
                                "3", Yellow, " - Задача A «Гра “Вгадай число”» зі змагання «53 Дорішування теми \"Бінарний та тернарний пошуки\".\n", White,
                                "4", Yellow, " - Задача B «Пошук елементів у масиві–1» зі змагання «53 Дорішування теми \"Бінарний та тернарний пошуки\".\n", White,
                                "6", Yellow, " - Задача C «Пошук елементів у масиві–2» зі змагання «53 Дорішування теми \"Бінарний та тернарний пошуки\".\n", White,
                                "6", Yellow, " - Задача A «Операції над множинами» зі змагання «61 День Іллі Порубльова \"Школи Бобра\".\n", White,
                                "7", Yellow, " - Задача B «Всюдисущi числа» зі змагання «61 День Іллі Порубльова \"Школи Бобра\".\n", White,
                                "8", Yellow, " - Задача C «Школярi з хмарочосiв» зі змагання «61 День Іллі Порубльова \"Школи Бобра\".\n", White);
            start:
            var x = Custom.ReadLine(Convert.ToByte, true, "Неправильний тип введення", Red, Yellow, true);
            if (x >=1 && x<=8)
                return x;
            else
            {
                Custom.WriteColored("Неправильний тип введення\n", Red);
                goto start;
            }
        } 
        static public byte SelectInputType() 
        {            
            Custom.WriteColored("Виберіть тип введення:\n", White, 
                                "1", Yellow, " - заповнити масив випадковими числами.\n", White, 
                                "2", Yellow, " - заповнити масив числами в рядок через пробіл.\n", White, 
                                "3", Yellow, " - заповнити масив в стовпчик через ", White, "Enter\n", Yellow);
            start:
            var x = Custom.ReadLine(Convert.ToByte, true, "Неправильний тип введення", Red, Yellow, true);
            if (x >= 1 && x <= 3)
                return x;
            else
            {
                Custom.WriteColored("Неправильний тип введення\n", Red);
                goto start;
            }
        }
        static public byte SelectArrayMethodsUsage() 
        {            
            Custom.WriteColored("Виберіть тип виконання програми:\n", White, 
                                "0",Yellow," - Без методів классу Array стандартої бібліотеки.\n",White,
                                "1",Yellow," - З методами классу Array стандартої бібліотеки.\n", White);
            start:
            var x = Custom.ReadLine(Convert.ToByte, true, "Неправильний тип введення",Red , Yellow, true);
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
            while(true) StartTasks();            
        }
    }
}
