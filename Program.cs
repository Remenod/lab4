using System;
using System.Numerics;
using static System.ConsoleColor;
using static Lab4.Other;
using AMFR = Lab4.ArrayMethodFulRealization;
using AMLR = Lab4.ArrayMethodLessRealization;

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
        private static double[] DefaultArrayInit(byte inputType)
        {
            double[] output = [];
            switch (inputType)
            {
                case 1:
                    output = AMFR.DefaultArrayInit(inputType);
                    break;
                case 2:
                    var input = Custom.ReadLine(White, true).Split(' ', '\t');
                    output = new double[input.Length];
                    for (int i = 0; i < input.Length; i++)
                        output[i] = double.Parse(input[i]);
                    break;
                case 3:
                    output = AMFR.DefaultArrayInit(inputType);
                    break;
            }
            return output;
        }
        public void Task1(byte inputType)
        {
            var input = DefaultArrayInit(inputType);
            BigDouble result = new(1, 0);
            BigDouble tempResult = new(1, 0);
            var lastMax = (value: double.MinValue, index: 0);
            for (int i = 0; i < input.Length; i++)
            {
                if (lastMax.value <= input[i])
                {
                    lastMax = (input[i], i);
                    result = tempResult;
                }
                tempResult *= input[i];
            }
            Custom.WriteColored(((lastMax.index == 0)
                                ? "Перший елемент максимальний, добуток чисел перед останім входженням максимального елементу не існує"
                                : $"Добуток чисел перед останнім входженням максимального числа:\n{result}")
                                + "\n", White);
        }
        public void Task2(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task3(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task4(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task5(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task6(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task7(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task8(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
    }
    class ArrayMethodFulRealization : ITaskContaiter
    {
        public static double[] DefaultArrayInit(byte inputType)
        {
            double[] output = [];
            switch (inputType)
            {
                case 1:
                    uint num;
                    do
                    {
                        Custom.WriteColored("Введіть кількість елементів масиву для генерації масиву з елементами від -1000 до 1000:\n", White);
                        num = Custom.ReadLine(uint.Parse, true, "Неправильний тип введення", Red, Yellow, true);
                        Custom.WriteColored("Використовувати лише цілі числа?:\n", White,
                                            "0", Yellow, " - ні (кількість знаків після коми між 0 та 7).\n", White,
                                            "1", Yellow, " - так.\n", White);
                        var rnNumType = Custom.ReadLine(uint.Parse, true, "Неправильний тип введення", Red, Yellow, true);
                        var rn = new Random();
                        Func<double> random = (rnNumType == 0) ? () => Math.Round((rn.NextDouble() * 2000) - 1000, rn.Next(7)) : () => rn.Next(-1000, 1000);
                        output = new double[num];
                        for (int i = 0; i < num; i++)
                            output[i] = random();
                        Custom.WriteColored($"Результат:\n{String.Join(" ", output)}\n", White);
                    }
                    while (num == 0);
                    break;
                case 2:
                    Custom.WriteColored("Введіть послідовно елементи масиву через пробіл або/та табуляцію:\n", White);
                    output = Array.ConvertAll(Custom.ReadLine(White, true).Split(' ', '\t'), double.Parse);
                    break;
                case 3:
                    do
                    {
                        Custom.WriteColored("Введіть кількість елементів масиву:\n", White);
                        num = Custom.ReadLine(uint.Parse, true, "Неправильний тип введення", Red, Yellow, true);
                    }
                    while (num == 0);
                    Custom.WriteColored("Введіть послідовно елементи масиву через ", White, "Enter", Yellow, ":\n", White);
                    output = new double[num];
                    for (int i = 0; i < num; i++)
                    {
                    start:
                        try
                        {
                            Custom.WriteColored($"{i + 1}: ", White);
                            output[i] = Custom.ReadLine(double.Parse, false, Yellow);
                        }
                        catch (Exception e) when (e is ArgumentException)
                        {
                            Custom.WriteColored("Неправильний тип введення\n", Red);
                            goto start;
                        }
                    }
                    break;
            }
            return output;
        }
        public void Task1(byte inputType)
        {
            var input = DefaultArrayInit(inputType);
            BigDouble result = new(1, 0);
            BigDouble tempResult = new(1, 0);
            var lastMax = (value: double.MinValue, index: 0);
            for (int i = 0; i < input.Length; i++)
            {
                if (lastMax.value <= input[i])
                {
                    lastMax = (input[i], i);
                    result = tempResult;
                }
                tempResult *= input[i];
            }
            Custom.WriteColored(((lastMax.index == 0)
                                ? "Перший елемент максимальний, добуток чисел перед останім входженням максимального елементу не існує"
                                : $"Добуток чисел перед останнім входженням максимального числа:\n{result}")
                                + "\n", White);
        }
        public void Task2(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task3(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task4(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task5(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task6(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task7(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
        public void Task8(byte inputType)
        {
            var input = DefaultArrayInit(inputType);

        }
    }
    static class Other
    {
        public class BigDouble
        {
            public BigInteger Mantissa { get; private set; }
            public BigInteger Exponent { get; private set; }
            public BigDouble(BigInteger mantissa, BigInteger exponent) => (Mantissa, Exponent) = (mantissa, exponent);
            public static BigDouble FromDouble(double value)
            {
                if (value == 0) return new(0, 0);
                bool isNegative = value < 0;
                value = Math.Abs(value);
                string valueStr = value.ToString("0.#######################################################E+00");
                var valueStrSplit = valueStr.Split('e', 'E');
                var result = (mantissa: double.Parse(valueStrSplit[0]), exponent: int.Parse(valueStrSplit[1]));
                string[] parts = valueStrSplit[0].Split('.', ',');
                if (parts.Length == 1) return new((BigInteger)result.mantissa * BigInteger.Pow(10, result.exponent), 0);
                result.mantissa = double.Parse(parts[0] + parts[1]);
                int decimalPlaces = parts[1].Length;
                result.exponent += -decimalPlaces;
                if (isNegative) result.mantissa = -result.mantissa;
                return new BigDouble((BigInteger)result.mantissa, result.exponent);
            }
            public override string ToString()
            {
                if (Exponent == 0)
                    return $"{Mantissa}";
                else if (Exponent > 0)
                    return $"{Mantissa * BigInteger.Pow(10, (int)Exponent)}";
                else if (Exponent < 0)
                {
                    string output = "";
                    var mantissaChars = Mantissa.ToString();
                    for (int i = 0; i < mantissaChars.Length + Exponent; i++)
                        output += $"{mantissaChars[i]}";
                    output += ".";
                    for (int i = (int)(mantissaChars.Length + Exponent); i < mantissaChars.Length; i++)
                        output += $"{mantissaChars[i]}";
                    return output;
                }
                else
                    return $"{Mantissa}e{Exponent}";
            }

            public static BigDouble operator +(BigDouble a, BigDouble b)
            {
                if (a.Exponent == b.Exponent)
                    return new BigDouble(a.Mantissa + b.Mantissa, a.Exponent);
                else if (a.Exponent > b.Exponent)
                {
                    BigInteger shift = BigInteger.Pow(10, (int)(a.Exponent - b.Exponent));
                    return new BigDouble(a.Mantissa + b.Mantissa * shift, a.Exponent);
                }
                else
                {
                    BigInteger shift = BigInteger.Pow(10, (int)(b.Exponent - a.Exponent));
                    return new BigDouble(a.Mantissa * shift + b.Mantissa, b.Exponent);
                }
            }
            public static BigDouble operator +(BigDouble a, double b) => a + FromDouble(b);
            public static BigDouble operator +(double a, BigDouble b) => FromDouble(a) + b;

            public static BigDouble operator -(BigDouble a, BigDouble b)
            {
                if (a.Exponent == b.Exponent)
                    return new BigDouble(a.Mantissa - b.Mantissa, a.Exponent);
                else if (a.Exponent > b.Exponent)
                {
                    BigInteger shift = BigInteger.Pow(10, (int)(a.Exponent - b.Exponent));
                    return new BigDouble(a.Mantissa - b.Mantissa * shift, a.Exponent);
                }
                else
                {
                    BigInteger shift = BigInteger.Pow(10, (int)(b.Exponent - a.Exponent));
                    return new BigDouble(a.Mantissa * shift - b.Mantissa, b.Exponent);
                }
            }
            public static BigDouble operator -(BigDouble a, double b) => a - FromDouble(b);
            public static BigDouble operator -(double a, BigDouble b) => FromDouble(a) - b;

            public static BigDouble operator /(BigDouble a, BigDouble b) => new BigDouble(a.Mantissa / b.Mantissa, a.Exponent - b.Exponent);
            public static BigDouble operator /(BigDouble a, double b) => a / FromDouble(b);
            public static BigDouble operator /(double a, BigDouble b) => FromDouble(a) / b;

            public static BigDouble operator *(BigDouble a, BigDouble b) => new BigDouble(a.Mantissa * b.Mantissa, a.Exponent + b.Exponent);
            public static BigDouble operator *(BigDouble a, double b) => a * FromDouble(b);
            public static BigDouble operator *(double a, BigDouble b) => FromDouble(a) * b;
        }
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
        static public byte SelectArrayMethodsUsage()
        {
            Custom.WriteColored("Виберіть тип виконання програми:\n", White,
                                "0", Yellow, " - Закрити програму.\n", White,
                                "1", Yellow, " - Без методів классу Array стандартої бібліотеки.\n", White,
                                "2", Yellow, " - З методами классу Array стандартої бібліотеки.\n", White);
        start:
            var x = Custom.ReadLine(Convert.ToByte, true, "Неправильний тип введення", Red, Yellow, true);
            if (x == 0) Environment.Exit(0);
            if (x == 1 || x == 2)
                return x;
            else
            {
                Custom.WriteColored("Неправильний тип введення\n", Red);
                goto start;
            }
        }
        static public byte SelectTask()
        {
            Custom.WriteColored("Виберіть задачу:\n", White,
                                "1", Yellow, " - Знайти добуток елементів масиву, які розміщені перед останнім входженням максимального числа.\n", White,
                                "2", Yellow, " - Задача Table з netoi.org.ua.\n", White,
                                "3", Yellow, " - Задача A «Гра “Вгадай число”» зі змагання «53 Дорішування теми \"Бінарний та тернарний пошуки\".\n", White,
                                "4", Yellow, " - Задача B «Пошук елементів у масиві–1» зі змагання «53 Дорішування теми \"Бінарний та тернарний пошуки\".\n", White,
                                "6", Yellow, " - Задача C «Пошук елементів у масиві–2» зі змагання «53 Дорішування теми \"Бінарний та тернарний пошуки\".\n", White,
                                "6", Yellow, " - Задача A «Операції над множинами» зі змагання «61 День Іллі Порубльова \"Школи Бобра\".\n", White,
                                "7", Yellow, " - Задача B «Всюдисущi числа» зі змагання «61 День Іллі Порубльова \"Школи Бобра\".\n", White,
                                "8", Yellow, " - Задача C «Школярi з хмарочосiв» зі змагання «61 День Іллі Порубльова \"Школи Бобра\".\n", White);
        start:
            var x = Custom.ReadLine(Convert.ToByte, true, "Неправильний тип введення", Red, Yellow, true);
            if (x >= 1 && x <= 8)
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
                                "1", Yellow, " - Заповнити масив випадковими числами.\n", White,
                                "2", Yellow, " - Заповнити масив числами в рядок через пробіл.\n", White,
                                "3", Yellow, " - Заповнити масив в стовпчик через ", White, "Enter\n", Yellow);
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
        static public void StartTask(byte arrayMethodsUsage, byte taskNum, byte inputType)
        {
            ITaskContaiter TaskContainer = (arrayMethodsUsage == 1) ? new AMLR() : new AMFR();
            Custom.WriteColored(
                "\nЗапускаю задачу ", White, $"{taskNum} ", Yellow,
                ((arrayMethodsUsage == 0) ? "без використання " : "з використанням ") + "методів класу System.", White, "Array ", DarkGreen,
                "та використанням " + inputType switch
                {
                    1 => "випадкового заповнення.\n",
                    2 => "заповнення в рядок.\n",
                    3 => "заповнення в стовпчик.\n",
                    _ => throw new NotImplementedException("Something went wrong")
                }, White);
            Task task = taskNum switch
            {
                1 => TaskContainer.Task1,
                2 => TaskContainer.Task2,
                3 => TaskContainer.Task3,
                4 => TaskContainer.Task4,
                5 => TaskContainer.Task5,
                6 => TaskContainer.Task6,
                7 => TaskContainer.Task7,
                8 => TaskContainer.Task8,
                _ => throw new NotImplementedException("Something went wrong")
            };
            task(inputType);
            Console.WriteLine();
        }
    }
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            while (true) StartTask(SelectArrayMethodsUsage(), SelectTask(), SelectInputType());
        }
    }
}
