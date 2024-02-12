/******************************************************************************
https://onlinegdb.com/9souugzeu
Codestyle: https://learn.microsoft.com/ru-ru/dotnet/csharp/fundamentals/coding-style/coding-conventions
Название: Базовые понятия языка С#
Автор: Тимофеев Гордей Евгеньевич
Версия: 1
*******************************************************************************/

using System;

class Program
{
    static int Multiply(int a, int n)
    {
        int result = 1;
        for (int index = 0; index < n; ++index)
        {
            result *= a;
        }
        return result;
    }
    static void Main()
    {
        Console.WriteLine("Введите число а:");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Введите число n:");
        int n = Convert.ToInt32(Console.ReadLine());

        int Result = Multiply(a, n);
        Console.WriteLine($"Результат: {Result}");
    }
}
