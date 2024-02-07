/******************************************************************************
https://onlinegdb.com/mtK973kJaF
Codestyle: https://learn.microsoft.com/ru-ru/dotnet/csharp/fundamentals/coding-style/coding-conventions
Название: Базовые понятия языка С#
Автор: Тимофеев Гордей Евгеньевич
Версия: 1
*******************************************************************************/

using System;

class Program
{
    static int Multiply(int A, int N)
    {
        int Result = 1;
        for (int Index = 0; Index < N; Index++)
        {
            Result *= A;
        }
        return Result;
    }
    static void Main()
    {
        Console.WriteLine("Введите число а:");
        int A = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Введите число n:");
        int N = Convert.ToInt32(Console.ReadLine());

        int Result = Multiply(A, N);
        Console.WriteLine($"Результат: {Result}");
    }
}
