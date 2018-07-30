using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp3
{
     public class Program
    {

        public static void Main()
        {
            string num1;
            string num2;

            //Строки могут иметь посторонние символы (ошибки набора со стороны пользователя)

            //Ввод чисел 
            Console.WriteLine("Enter num 1:");
            num1 = Console.ReadLine();
            Console.WriteLine("Enter num 2:");
            num2 = Console.ReadLine();
            //Проверка, содержат ли строки цифры
            if ((Regex.Replace(num1, @"[^\d]", string.Empty) == "") || (Regex.Replace(num2, @"[^\d]", string.Empty) == ""))
                Console.Write("Error, string is Empty");
            else
            {
                Console.Write("Сумма чисел равна: ");
                FormattedNums(num1, num2);
            }

            Console.ReadKey();

        }

        /*Редактирование чисел и выбор метода сложения в зависимости от знака чисел */

        public static void FormattedNums(string num1, string num2)
        {

            //Отбрасываем все символы, кроме '-' и цифр
            num1 = Regex.Replace(num1, @"[^\d-]", string.Empty);
            num2 = Regex.Replace(num2, @"[^\d-]", string.Empty);

            if ((num1[0] == '-') && (num2[0] == '-'))
            { // оба значения отрицательные
              // Отбрасываем оставшиеся символы "-" из обоих строк
                num1 = Regex.Replace(num1, @"-", string.Empty);
                num2 = Regex.Replace(num2, @"-", string.Empty);
                Console.Write('-');
                // сложение отрицательных чисел
                SumLongNums(num1, num2);
            }
            else if (num1[0] == '-')
            {
                SubtrLongNums(num2, num1); //вычитание 1 числа из 2-ого

            }
            else if (num2[0] == '-')
            {
                SubtrLongNums(num1, num2); // вычитание 2 числа из 1-ого

            }
            else SumLongNums(num1, num2); // сложение неотрицательных чисел
        }


        /*Сложение двух длинных чисел*/

        public static void SumLongNums(string num1, string num2)
        {

            num1 = Regex.Replace(num1, @"-", string.Empty); //Отбрасываем все символы, кроме цифр
            num2 = Regex.Replace(num2, @"-", string.Empty);
            //Находим число наибольшей длины 
            int len1 = num1.Length;
            int len2 = num2.Length;
            int len = (len1 > len2) ? len1 : len2;
            len++;

            //сложение чисел
            char[] sum = new char[len];

            int e = 0; //запоминание единицы для переноса на старший разряд при сложении
            for (int i = 1; i <= len; i++)
            {
                int a = (i > len1) ? 0 : num1[len1 - i] - '0';
                int b = (i > len2) ? 0 : num2[len2 - i] - '0';
                int c = a + b + e;
                int d = '0' + (c % 10);
                sum[len - i] = (char)d;
                e = c / 10;
            }

            // Вывод чисел без нулей спереди
            bool f = true;
            foreach (char cc in sum)
            {
                if ((cc != '0') && (f == true))
                    f = !f;
                if (f == false)
                    Console.Write(cc);
            }

            if (f == true)
                Console.Write('0');
        }


        /*Вычитание двух длинных чисел */

        public static void SubtrLongNums(string num1, string num2)
        {

            num1 = Regex.Replace(num1, @"-", string.Empty); //Отбрасываем все символы, кроме цифр
            num2 = Regex.Replace(num2, @"-", string.Empty);

            //Находим число наибольшей длины 
            int len1 = num1.Length;
            int len2 = num2.Length;
            int len = 0;

            if (len2 > len1)
            { // отрицательное число длиннее положительного

                string n = num1;
                num1 = num2.Replace(num1, num2);
                num2 = n.Replace(num2, n);
                Console.Write('-');
                len1 = num1.Length;
                len2 = num2.Length;
                len = len1;

            }
            else if (len1 == len2)
            {
                len = len1;
                int i = 0;
                while ((num1[i] == num2[i]) && (i <= len))
                    i++;
                if (num2[i] > num1[i])
                {
                    string n = num1;
                    num1 = num2.Replace(num1, num2);
                    num2 = n.Replace(num2, n);
                    Console.Write('-');
                }
            }
            else len = len1; //положительное число длиннее отрицательного


            //сложение чисел
            char[] sum = new char[len];

            int e = 0; //запоминание единицы для переноса на старший разряд при вычитании
            for (int i = 1; i <= len; i++)
            {
                int a = (i > len1) ? 0 : num1[len1 - i] - '0';
                int b = (i > len2) ? 0 : num2[len2 - i] - '0';
                int c = a - b + e + 10;
                int d = '0' + (c % 10);
                sum[len - i] = (char)d;
                if (c <= 9) e = -1;
                else e = 0;

            }

            // Вывод чисел без лишних нулей
            bool f = true;
            foreach (char cc in sum)
            {
                if ((cc != '0') && (f == true))
                    f = !f;
                if (f == false)
                    Console.Write(cc);
            }

            if (f == true)
                Console.Write('0');

        }
    }

}
