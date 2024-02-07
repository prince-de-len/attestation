/******************************************************************************
https://onlinegdb.com/XcMwzStvL
Codestyle: https://learn.microsoft.com/ru-ru/dotnet/csharp/fundamentals/coding-style/coding-conventions
Название: Базовые понятия языка С#
Автор: Тимофеев Гордей Евгеньевич
Версия: 1
*******************************************************************************/

using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите число, содержащее более двух цифр");
        int x = Convert.ToInt32(Console.ReadLine());

        if (x < 100)
        {
            Console.WriteLine("Число должно быть больше или равно 100");
            return;
        }

        string stringX = x.ToString();
        char removedDigit = stringX[1];
        string stringXWithoutRemovedDigit = stringX.Substring(0, 1) + stringX.Substring(2);
        string n = stringXWithoutRemovedDigit + removedDigit;

        Console.WriteLine($"Для введенного x = {x}, n = {n}");
    }
}
