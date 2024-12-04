using System;
using System.Collections.Generic;
using System.Linq;
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
    abstract class ArrayInit
    {
        protected T[] Init<T>(byte inputType, byte forTask = 0) where T : IConvertible, IComparable =>
            inputType switch
            {
                1 => InitRand<T>(forTask),
                2 => InitInLine<T>(forTask),
                3 => InitInColon<T>(forTask),
                _ => throw new Exception("Somwting went wrong")
            };
        protected virtual T[] InitRand<T>(byte forTask) where T : IConvertible, IComparable
        {
            T[] output = [];
            uint num;
            do
            {
                Custom.WriteColored("Введіть кількість елементів масиву для генерації масиву з елементами від -1000 до 1000:\n", White);
                num = Custom.ReadLine(uint.Parse, true, "Неправильний тип введення", Red, Yellow, true);
                Custom.WriteColored("Використовувати лише цілі числа?:\n", White,
                                    "1", Yellow, " - так.\n", White,
                                    "2", Yellow, " - ні (кількість знаків після коми між 0 та 7).\n", White);
                var rnNumType = Custom.ReadLine(uint.Parse, true, "Неправильний тип введення", Red, Yellow, true);
                var rn = new Random();
                Func<T> random = (rnNumType == 2) ? () => (T)Convert.ChangeType(Math.Round((rn.NextDouble() * 2000) - 1000, rn.Next(7)), typeof(T)) : () => (T)Convert.ChangeType(rn.Next(-1000, 1000), typeof(T));
                output = new T[num];
                for (int i = 0; i < num; i++)
                    output[i] = random();
                Custom.WriteColored($"Результат:\n{String.Join(" ", output)}\n", White);
            }
            while (num == 0);
            return output;
        }
        protected virtual T[] InitInLine<T>(byte forTask) where T : IConvertible, IComparable
        {
            T[] output;
            Custom.WriteColored("Введіть послідовно елементи масиву через пробіл або/та табуляцію:\n", White);
            while (true)
                try
                {
                    var input = Custom.ReadLine(Yellow, true).Split(' ', '\t');
                    output = new T[input.Length];
                    for (int i = 0; i < input.Length; i++)
                        output[i] = (T)Convert.ChangeType(input[i], typeof(T));
                    break;
                }
                catch { Custom.WriteColored("Неправильний тип введення\n", Red); }
            return output;
        }
        protected virtual T[] InitInColon<T>(byte forTask) where T : IConvertible, IComparable
        {
            T[] output = [];
            uint num;
            while (true)
            {
                Custom.WriteColored("Введіть кількість елементів масиву:\n", White);
                num = Custom.ReadLine(uint.Parse, true, "Неправильний тип введення", Red, Yellow, true);
                if (num == 0)
                {
                    Custom.WriteColored("Неправильний тип введення\n", Red);
                    continue;
                }
                break;
            }
            Custom.WriteColored("Введіть послідовно елементи масиву через ", White, "Enter", Yellow, ":\n", White);
            output = new T[num];
            for (int i = 0; i < num; i++)
                try
                {
                    Custom.WriteColored($"{i + 1}: ", White);
                    output[i] = (T)Convert.ChangeType(Custom.ReadLine(double.Parse, false, Yellow), typeof(T));
                }
                catch (Exception e) when (e is ArgumentException)
                {
                    Custom.WriteColored("Неправильний тип введення\n", Red);
                    i--;
                }
            return output;
        }
    }
    class ArrayMethodLessRealization : ArrayInit, ITaskContaiter
    {
        protected override T[] InitRand<T>(byte forTask)
        {
            T[] output;
            var rn = new Random();
            uint num;
            switch (forTask)
            {
                case 3:
                    output = new T[2];                    
                    output[0] = (T)Convert.ChangeType(rn.Next(-2000000000, 2000000000), typeof(T));
                    output[1] = (T)Convert.ChangeType(rn.Next((int)Convert.ChangeType(output[0], typeof(T)), 2000000000), typeof(T));
                    Custom.WriteColored($"{String.Join(" ", output)}\n", White);
                    return output;
                case 4:
                    do
                    {
                        Custom.WriteColored("Введіть кількість елементів масиву для генерації масиву з елементами від -1000000000 до 1000000000:\n", White);
                        num = Custom.ReadLine(uint.Parse, true, "Неправильний тип введення", Red, Yellow, true);
                        Func<T> random = () => (T)Convert.ChangeType(rn.Next(-1000000000, 1000000000), typeof(T));
                        output = new T[num];
                        for (int i = 0; i < num; i++)
                            output[i] = random();
                        Custom.WriteColored($"Результат:\n{String.Join(" ", output)}\n", White);
                    }
                    while (num == 0);
                    return output;
                default:
                    return base.InitRand<T>(forTask);
            }
        }
        protected override T[] InitInLine<T>(byte forTask)
        {
            T[] output;
            switch (forTask)
            {
                case 3:
                    output = new T[2];
                    while (true)
                        try
                        {
                            while (true)
                            {
                                Custom.WriteColored("Введіть послідовно елементи масиву через пробіл або/та табуляцію:\n", White);
                                var temp = Custom.ReadLine(Yellow, true).Split(' ', '\t');
                                if (temp.Length != 2)
                                {
                                    Custom.WriteColored($"Неправильний тип введення\n", Red);
                                    continue;
                                }
                                output[0] = (T)Convert.ChangeType(long.Parse(temp[0]), typeof(T));
                                output[1] = (T)Convert.ChangeType(long.Parse(temp[1]), typeof(T));
                                if (output[1].CompareTo(output[0]) >= 0) break;
                                Custom.WriteColored("Друга межа має бути більшою\n", Red);
                            }
                            break;
                        }
                        catch { Custom.WriteColored("Неправильний тип введення\n", Red); }
                    return output;
                default:
                    return base.InitInLine<T>(forTask);
            }
        }
        protected override T[] InitInColon<T>(byte forTask)
        {
            T[] output;
            switch (forTask)
            {
                case 3:
                    output = new T[2];
                    Custom.WriteColored("Введіть першу межу:\n", White);
                    output[0] = (T)Convert.ChangeType(Custom.ReadLine(long.Parse, true, "Неправильний тип введення", Red, Yellow, true), typeof(T));
                    Custom.WriteColored("Введіть другу межу:\n", White);
                    while (true)
                    {
                        output[1] = (T)Convert.ChangeType(Custom.ReadLine(long.Parse, true, "Неправильний тип введення", Red, Yellow, true), typeof(T));
                        if (output[1].CompareTo(output[0]) >= 0) break;
                        Custom.WriteColored("Друга межа має бути більшою\n", Red);
                    }
                    return output;
                default:
                    return base.InitInColon<T>(forTask);
            }
        }

        public void Task1(byte inputType)
        {
            var input = Init<double>(inputType);
            BigDouble result = 1;
            BigDouble tempResult = 1;
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
            var input = Init<double>(inputType);
            double targetValue = double.MaxValue;
            Custom.WriteColored("Введіть число K\n", White);
            int k = Custom.ReadLine(int.Parse, true, "Неправильний тип введення", Red, Yellow, true);
            for (int i = 0; i < k; i++)
            {
                double currentMax = double.MinValue;
                for (int j = 0; j < input.Length; j++)
                    if (input[j] > currentMax && input[j] < targetValue)
                        currentMax = input[j];
                targetValue = currentMax;
            }
            int position = -1;
            for (int i = 0; i < input.Length; i++)
                if (input[i] == targetValue)
                {
                    position = i + 1;
                    break;
                }
            Custom.WriteColored("Номер K-го елемента по спаданню: ", White, $"{position}\n", Yellow);
        }
        public void Task3(byte inputType)
        {
            var input = Init<long>(inputType, 3);
            var (answer, num) = ("", 0L);
            input[0]--; input[1]++;
            Custom.WriteColored("Типи відовідей:\n", White,
                                "+", Yellow, " - загадане число більше.\n", White,
                                "-", Yellow, " - загадане число менше.\n", White,
                                "+", Yellow, " - дорівнює загаданому числу.\n", White);
            for (int i = 1; answer != "=" && i <= 50; i++)
            {
                num = (input[1] - input[0]) / 2 + input[0];
                Custom.WriteColored($"Ваше число {num}?\n", White);
                answer = Custom.ReadLine(White);
                switch (answer)
                {
                    case "-":
                        input[1] = num;
                        break;
                    case "+":
                        input[0] = num;
                        break;
                }
            }
            Console.WriteLine($"Відповідь {num}");
        }
        public void Task4(byte inputType)
        {
            int[] input0;
            int[] input1;

            input0 = Init<int>(inputType, 4);
            if (inputType == 1)
            {
                int[] GetRandomElements(int[] source)
                {
                    Random random = new Random();
                    int count = random.Next(1, source.Length + 1);
                    return source.OrderBy(x => random.Next()).Take(count).ToArray();
                }
                input1 = GetRandomElements(input0);
                Custom.WriteColored($"Автоматично згенерований другий масив:\n{String.Join(" ", input1)}\n", White);
            }
            else
            {
                while (true)
                {
                    input1 = Init<int>(inputType, 4);
                    if (input0.Length < input1.Length)
                    {
                        Custom.WriteColored("\nКількість елементів другого масиву не може перевищувати кількість елементів першого\n\n", Red, "Введіть повторно другий масив:\n", White);
                        continue;
                    }
                    break;
                }
            }

            void Sort(int[] array)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    int minIndex = i;
                    for (int j = i + 1; j < array.Length; j++)                    
                        if (array[j] < array[minIndex])
                            minIndex = j;                                   
                    int temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                }
            }
            Sort(input0);           
            var indexMap = new Dictionary<int, int>();
            for (int i = 0; i < input0.Length; i++)            
                if (!indexMap.ContainsKey(input0[i]))
                    indexMap[input0[i]] = i + 1;
            string result = "";
            foreach (var x in input1)            
                result += $"{indexMap[x]} ";            
            Custom.WriteColored($"Номери елементів з другого масиву в відсортованому першому:\n{result}\n", White);
        }
        public void Task5(byte inputType)
        {
            var input = Init<double>(inputType);

        }
        public void Task6(byte inputType)
        {
            var input = Init<double>(inputType);

        }
        public void Task7(byte inputType)
        {
            var input = Init<double>(inputType);

        }
        public void Task8(byte inputType)
        {
            var input = Init<double>(inputType);

        }
    }
    class ArrayMethodFulRealization : ArrayInit, ITaskContaiter
    {
        protected override T[] InitRand<T>(byte forTask)
        {
            T[] output;
            var rn = new Random();
            uint num;
            switch (forTask)
            {
                case 3:
                    output = new T[2];
                    output[0] = (T)Convert.ChangeType(rn.Next(-2000000000, 2000000000), typeof(T));
                    output[1] = (T)Convert.ChangeType(rn.Next((int)Convert.ChangeType(output[0], typeof(T)), 2000000000), typeof(T));
                    Custom.WriteColored($"{String.Join(" ", output)}\n", White);
                    return output;
                case 4:
                    do
                    {
                        Custom.WriteColored("Введіть кількість елементів масиву для генерації масиву з елементами від -1000000000 до 1000000000:\n", White);
                        num = Custom.ReadLine(uint.Parse, true, "Неправильний тип введення", Red, Yellow, true);
                        Func<T> random = () => (T)Convert.ChangeType(rn.Next(-1000000000, 1000000000), typeof(T));
                        output = new T[num];
                        for (int i = 0; i < num; i++)
                            output[i] = random();
                        Custom.WriteColored($"Результат:\n{String.Join(" ", output)}\n", White);
                    }
                    while (num == 0);
                    return output;
                default:
                    return base.InitRand<T>(forTask);
            }
        }
        protected override T[] InitInLine<T>(byte forTask)
        {
            T[] output;
            switch (forTask)
            {
                case 3:
                    output = new T[2];
                    while (true)
                        try
                        {
                            while (true)
                            {
                                var temp = Custom.ReadLine(Yellow, true).Split(' ','\t');
                                Array.ForEach(temp, x => output[Array.IndexOf(temp, x)] = (T)Convert.ChangeType(x, typeof(T)));
                                if (output[1].CompareTo(output[0]) >= 0) break;
                                Custom.WriteColored("Друга межа має бути більшою\n", Red);
                            }
                            break;
                        }
                        catch { Custom.WriteColored($"Неправильний тип введення\n", Red); }
                    return output;
                default:
                    Custom.WriteColored("Введіть послідовно елементи масиву через пробіл або/та табуляцію:\n", White);
                    while (true)
                        try
                        {
                            var temp = Custom.ReadLine(Yellow, true).Split(' ', '\t');
                            output = new T[temp.Length];
                            Array.ForEach(temp, x => output[Array.IndexOf(temp, x)] = (T)Convert.ChangeType(x, typeof(T)));
                            break;
                        }
                        catch { Custom.WriteColored("Неправильний тип введення\n", Red); }
                    return output;
            }
        }
        protected override T[] InitInColon<T>(byte forTask)
        {
            T[] output;
            switch (forTask)
            {
                case 3:
                    output = new T[2];
                    Custom.WriteColored("Введіть першу межу:\n", White);
                    output[0] = (T)Convert.ChangeType(Custom.ReadLine(long.Parse, true, "Неправильний тип введення", Red, Yellow, true), typeof(T));
                    Custom.WriteColored("Введіть другу межу:\n", White);
                    while (true)
                    {
                        output[1] = (T)Convert.ChangeType(Custom.ReadLine(long.Parse, true, "Неправильний тип введення", Red, Yellow, true), typeof(T));
                        if (output[1].CompareTo(output[0]) >= 0) break;
                        Custom.WriteColored("Друга межа має бути більшою\n", Red);
                    }
                    return output;
                default:
                    return base.InitInColon<T>(forTask);
            }
        }

        public void Task1(byte inputType)
        {
            var input = Init<double>(inputType);
            double maxValue = input.Max();
            int maxIndex = Array.FindLastIndex(input, input.Length - 1, input.Length, x => x == maxValue);
            if (maxIndex == 0)
            {
                Custom.WriteColored("Перший елемент максимальний, добуток чисел перед останнім входженням максимального елементу не існує\n", White);
                return;
            }
            double[] trimmedArray = new double[maxIndex];
            Array.Copy(input, trimmedArray, maxIndex);
            BigDouble result = 1;
            Array.ForEach(trimmedArray, x => result *= x);
            Custom.WriteColored($"Добуток чисел перед останнім входженням максимального числа:\n{result}\n", White);
        }
        public void Task2(byte inputType)
        {
            double[] input = Init<double>(inputType);
            Custom.WriteColored("Введіть число K\n", White);
            int k = Custom.ReadLine(int.Parse, true, "Неправильний тип введення", Red, Yellow, true);
            double[] sortedArray = (double[])input.Clone();
            Array.Sort(sortedArray);
            Array.Reverse(sortedArray);
            int position = Array.IndexOf(input, sortedArray[k - 1]) + 1;
            Custom.WriteColored("Номер K-го елемента по спаданню: ", White, $"{position}\n", Yellow);
        }
        public void Task3(byte inputType)
        {
            long[] input = Init<long>(inputType, 3);
            var (answer, num) = ("", 0L);
            input[0]--; input[1]++;
            Custom.WriteColored("Типи відовідей:\n", White,
                                "+", Yellow, " - загадане число більше.\n", White,
                                "-", Yellow, " - загадане число менше.\n", White,
                                "+", Yellow, " - дорівнює загаданому числу.\n", White);
            for (int i = 1; answer != "=" && i <= 50; i++)
            {
                num = (input[1] - input[0]) / 2 + input[0];
                Custom.WriteColored($"Ваше число {num}?\n", White);
                answer = Custom.ReadLine(White);
                switch (answer)
                {
                    case "-":
                        input[1] = num;
                        break;
                    case "+":
                        input[0] = num;
                        break;
                }
            }
            Console.WriteLine($"Відповідь {num}");
        }
        public void Task4(byte inputType)
        {
            int[] input0;
            int[] input1;

            input0 = Init<int>(inputType, 4);
            if (inputType == 1)
            {
                int[] GetRandomElements(int[] source)
                {
                    Random random = new Random();
                    int count = random.Next(1, source.Length + 1);
                    return source.OrderBy(x => random.Next()).Take(count).ToArray();
                }
                input1 = GetRandomElements(input0);
                Custom.WriteColored($"Автоматично згенерований другий масив:\n{String.Join(" ", input1)}\n", White);
            }
            else
            {
                while (true)
                {
                    input1 = Init<int>(inputType, 4);
                    if (input0.Length < input1.Length)
                    {
                        Custom.WriteColored("\nКількість елементів другого масиву не може перевищувати кількість елементів першого\n\n", Red, "Введіть повторно другий масив:\n", White);
                        continue;
                    }
                    break;
                }
            }

            Array.Sort(input0);
            Custom.WriteColored("Номери елементів з другого масиву в відсортованому першому:\n", White);
            Array.ForEach(input1, x => Custom.WriteColored($"{Array.IndexOf(input0, x) + 1} ", White));
        }
        public void Task5(byte inputType)
        {
            var input = Init<double>(inputType);

        }
        public void Task6(byte inputType)
        {
            var input = Init<double>(inputType);

        }
        public void Task7(byte inputType)
        {
            var input = Init<double>(inputType);

        }
        public void Task8(byte inputType)
        {
            var input = Init<double>(inputType);

        }
    }
    public static class Other
    {
        public struct BigDouble
        {
            private BigInteger Mantissa { get; set; }
            private BigInteger Exponent { get; set; }
            public BigDouble(BigInteger mantissa, BigInteger exponent) => (Mantissa, Exponent) = (mantissa, exponent);
            private static BigDouble FromDouble(double value)
            {
                if (value == 0) return 0;
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
            public override bool Equals(object obj)
            {
                if (obj is BigDouble other)
                    return this == other;
                return false;
            }
            public override int GetHashCode() => HashCode.Combine(Mantissa, Exponent);

            public static implicit operator BigDouble(double value) => FromDouble(value);

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

            public static bool operator ==(BigDouble a, BigDouble b)
            {
                if (a.Exponent == b.Exponent)
                    return a.Mantissa == b.Mantissa;

                BigInteger shift = BigInteger.Pow(10, (int)Math.Abs((double)(a.Exponent - b.Exponent)));

                if (a.Exponent > b.Exponent)
                    return a.Mantissa == b.Mantissa * shift;
                else
                    return a.Mantissa * shift == b.Mantissa;
            }
            public static bool operator ==(BigDouble a, double b) => a == FromDouble(b);
            public static bool operator ==(double a, BigDouble b) => FromDouble(a) == b;

            public static bool operator !=(BigDouble a, BigDouble b) => !(a == b);
            public static bool operator !=(BigDouble a, double b) => !(a == b);
            public static bool operator !=(double a, BigDouble b) => !(a == b);

            public static bool operator >(BigDouble a, BigDouble b)
            {
                if (a.Exponent == b.Exponent)
                    return a.Mantissa > b.Mantissa;

                if (a.Exponent > b.Exponent)
                {
                    BigInteger shift = BigInteger.Pow(10, (int)(a.Exponent - b.Exponent));
                    return a.Mantissa > b.Mantissa * shift;
                }
                else
                {
                    BigInteger shift = BigInteger.Pow(10, (int)(b.Exponent - a.Exponent));
                    return a.Mantissa * shift > b.Mantissa;
                }
            }
            public static bool operator >(BigDouble a, double b) => a > FromDouble(b);
            public static bool operator >(double a, BigDouble b) => FromDouble(a) > b;

            public static bool operator <(BigDouble a, BigDouble b) => !(a >= b);
            public static bool operator <(BigDouble a, double b) => !(a >= b);
            public static bool operator <(double a, BigDouble b) => !(a >= b);

            public static bool operator >=(BigDouble a, BigDouble b) => a == b || a > b;
            public static bool operator >=(BigDouble a, double b) => a == b || a > b;
            public static bool operator >=(double a, BigDouble b) => a == b || a > b;

            public static bool operator <=(BigDouble a, BigDouble b) => a == b || a < b;
            public static bool operator <=(BigDouble a, double b) => a == b || a < b;
            public static bool operator <=(double a, BigDouble b) => a == b || a < b;
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
                                "2", Yellow, " - Знайти номер K-го елемента по спаданню.\n", White,
                                "3", Yellow, " - Задача A «Гра “Вгадай число”» зі змагання «53 Дорішування теми \"Бінарний та тернарний пошуки\".\n", White,
                                "4", Yellow, " - Задача B «Пошук елементів у масиві–1» зі змагання «53 Дорішування теми \"Бінарний та тернарний пошуки\".\n", White,
                                "5", Yellow, " - Задача C «Пошук елементів у масиві–2» зі змагання «53 Дорішування теми \"Бінарний та тернарний пошуки\".\n", White,
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
