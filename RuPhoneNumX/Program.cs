/****************************************************************/
/*   Вычисление числа номера телефона (реализовано в консоли)   */
/*                 by PapaProger, 26.02.2022 г.                 */
/****************************************************************/

namespace RuPhoneNumX
{
    static class Counter
    {
        // n = Int32.Parse(str);

        // Возвращает инт из чара ¯\_(ツ)_/¯
        public static int GetIntFromChar(this char ch)
        {
            return (int)(ch - '0');
        }

        // Возвращает преведенную сумму по заданной
        public static int CountSummTo1(this int s)
        {
            while (s >= 10) s = s / 10 + (s % 10);
            return s;
        }
    }

    class RuPhoneNumber
    {
        public static int PrefixLength { get { return 3; } }
        public static int NumberLength { get { return 7; } }
        public static int FullNumberLength { get { return 10; } }

        // Сумма 7 цифр номера
        public int SumNumber
        {
            get
            {
                sumNumber = 0;
                foreach (var x in fullNumberInt[1]) sumNumber += x;
                return sumNumber;
            }
        }

        // Приведенная сумма 7 цифр номера
        public int SumNumberTo1
        {
            get
            {
                return sumNumberTo1 = sumNumber.CountSummTo1();
            }
        }

        // Сумма 10 цифр номера
        public int SumFullNumber
        {
            get
            {
                sumFullNumber = 0;
                foreach (var x in fullNumberInt[0]) sumFullNumber += x;
                return sumFullNumber += sumNumber;
            }
        }

        // Приведенная сумма 10 цифр номера
        public int SumFullNumberTo1
        {
            get
            {
                return sumFullNumberTo1 = sumFullNumber.CountSummTo1();
            }
        }

        private int sumNumber;
        private int sumNumberTo1;
        private int sumFullNumber;
        private int sumFullNumberTo1;

        // Захотелось сделать рваный массив
        private int[][] fullNumberInt = new int[2][];

        // Строка, содержащая введенный номер телефона
        private string fullNumberString;

        public RuPhoneNumber()
        {
            fullNumberInt[0] = new int[PrefixLength];
            fullNumberInt[1] = new int[NumberLength];
            fullNumberInt[0][0] = 9;
        }

        // Тут задаем номер телефона
        public virtual void Set()
        {
            // Ввод номера
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Введите федеральный номер телефона:\t");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("+7\t9");
                fullNumberString = "9" + Console.ReadLine();
                Console.WriteLine();
            }
            while (fullNumberString.Length != FullNumberLength);

            // Перекладка номера из строки в рваный массив инт
            for (int i = PrefixLength; i < FullNumberLength; i++)
                fullNumberInt[1][i - PrefixLength] = fullNumberString[i].GetIntFromChar();
            for (int i = 0; i < PrefixLength; i++)
                fullNumberInt[0][i] = fullNumberString[i].GetIntFromChar();
        }

        // Считает и выводит значения всех сумм (лень перечислять)
        public virtual void CountSums()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Сумма  7 цифр номера\t\t-\t");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(SumNumber + "\t->\t" + SumNumberTo1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Сумма 10 цифр номера\t\t-\t");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(SumFullNumber + "\t->\t" + SumFullNumberTo1);
            Console.WriteLine();
        }

        /**********************************************************************************************************/
        /* Возможные префиксы, при которых приведенная сумма 7 цифр номера равна приведенной сумме 10 цифр номера */
        /*             P.S. Они всегда будут одни и те же 12 штук для 9XY, но пусть машина считает...             */
        /**********************************************************************************************************/
        public virtual void CountPrefs()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Возможные префиксы:");
            Console.WriteLine();

            int k = 0;

            int papaproger = fullNumberInt[0][0] + sumNumber;

            for (int i = 0; i <= 9; i++)
            {
                for (int j = 0; j <= 9; j++)
                {
                    if ((papaproger + i + j).CountSummTo1() == sumNumberTo1)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\t\t\t\t" + ++k + "\t");

                        Console.ForegroundColor = fullNumberInt[0][1] == i && fullNumberInt[0][2] == j ? ConsoleColor.Red : ConsoleColor.White;
                        Console.WriteLine(fullNumberInt[0][0].ToString() + i + j);
                    }
                }
            }

            // Только если номер вида +79XY0000000
            if (k == 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Таковых нет");
            }

        }

    }

    class Program
    {
        // Мэин (оставим аргументы как есть)
        static void Main(string[] args)
        {
            RuPhoneNumber pn = new RuPhoneNumber();

            pn.Set();
            pn.CountSums();
            pn.CountPrefs();

            Console.ResetColor();
            Console.Write("\nДля выхода нажмите <Enter>...");
            Console.ReadLine();
        }
    }
}