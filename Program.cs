using System;

class SquareMatrix
{
    int[,] Matrix;
    public int[,] Value { get { return Matrix; } }

    public int GetLength { get; set; }
    public double Determinant { get; set; }
    static readonly Random random = new Random();


    public SquareMatrix(int size)
    {
        Matrix = new int[size, size];
        for (var rowOfMatrix = 0; rowOfMatrix < Matrix.GetLength(0); ++rowOfMatrix)
        {
            for (var columnOfMatrix = 0; columnOfMatrix < Matrix.GetLength(1); ++columnOfMatrix)
            {
                int randomNumber = random.Next(100);
                Matrix[rowOfMatrix, columnOfMatrix] = randomNumber;
            }
        }
    }

    //Далее перегрузки:
    public static SquareMatrix operator +(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        int Rows = matrix1.Value.GetLength(0);
        int Columns = matrix1.Value.GetLength(1);

        if (Rows != matrix2.Value.GetLength(0) || Columns != matrix2.Value.GetLength(1))
        {
            throw new SquareMatrixDimensionsException("Матрицы разного размера.", Rows, matrix2.Value.GetLength(0));
        }

        int[,] result = new int[Rows, Columns];
        for (int IndexOfRow = 0; IndexOfRow < Rows; ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Columns; ++IndexOfColumn)
            {
                result[IndexOfRow, IndexOfColumn] = matrix1.Value[IndexOfRow, IndexOfColumn] + matrix2.Value[IndexOfRow, IndexOfColumn];
            }
        }
        SquareMatrix NewMatrix = new SquareMatrix(Rows);
        NewMatrix.Matrix = result;
        return NewMatrix;
    }

    public static SquareMatrix operator *(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        int Rows1 = matrix1.Value.GetLength(0);
        int Columns1 = matrix1.Value.GetLength(1);
        int Rows2 = matrix2.Value.GetLength(0);
        int Columns2 = matrix2.Value.GetLength(1);
        if (Columns1 != Rows2)
        {
            throw new SquareMatrixDimensionsException("Нельзя перемножить матрицы. Количество столбцов матрицы 1 не равно количеству строк матрицы 2.", matrix1.Value.GetLength(0), matrix2.Value.GetLength(0));
        }
        int[,] result = new int[Rows1, Columns2];

        for (int IndexOfRow1 = 0; IndexOfRow1 < Rows1; IndexOfRow1++)
        {
            for (int IndexOfColumn2 = 0; IndexOfColumn2 < Columns2; IndexOfColumn2++)
            {
                for (int IndexOfColumn1 = 0; IndexOfColumn1 < Columns1; IndexOfColumn1++)
                {
                    result[IndexOfRow1, IndexOfColumn2] += matrix1.Value[IndexOfRow1, IndexOfColumn1] * matrix2.Value[IndexOfColumn1, IndexOfColumn2];
                }
            }
        }

        SquareMatrix NewMatrix = new SquareMatrix(Rows1);
        NewMatrix.Matrix = result;
        return NewMatrix;

    }
    public static bool operator >(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        for (int IndexOfRow = 0; IndexOfRow < matrix1.Value.GetLength(0); ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < matrix1.Value.GetLength(1); ++IndexOfColumn)
            {
                if (matrix1.Value[IndexOfRow, IndexOfColumn] <= matrix2.Value[IndexOfRow, IndexOfColumn])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static bool operator <(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        for (int IndexOfRow = 0; IndexOfRow < matrix1.Value.GetLength(0); ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < matrix1.Value.GetLength(1); ++IndexOfColumn)
            {
                if (matrix1.Value[IndexOfRow, IndexOfColumn] >= matrix2.Value[IndexOfRow, IndexOfColumn])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static bool operator >=(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        for (int IndexOfRow = 0; IndexOfRow < matrix1.Value.GetLength(0); ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < matrix1.Value.GetLength(1); ++IndexOfColumn)
            {
                if (matrix1.Value[IndexOfRow, IndexOfColumn] < matrix2.Value[IndexOfRow, IndexOfColumn])
                {
                    return false;  // в случае, если хоть один элемент меньше, вернем false
                }
            }
        }
        return true;  // если все элементы больше или равны, вернем true
    }

    public static bool operator <=(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        for (int IndexOfRow = 0; IndexOfRow < matrix1.Value.GetLength(0); ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < matrix1.Value.GetLength(1); ++IndexOfColumn)
            {
                if (matrix1.Value[IndexOfRow, IndexOfColumn] > matrix2.Value[IndexOfRow, IndexOfColumn])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static bool operator ==(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        if (ReferenceEquals(matrix1, matrix2))
        {
            return true;
        }
        if (ReferenceEquals(matrix1, null) || ReferenceEquals(matrix2, null))
        {
            return false;
        }

        if (matrix1.Value.GetLength(0) != matrix2.Value.GetLength(0) || matrix1.Value.GetLength(1) != matrix2.Value.GetLength(1))
        {
            return false;
        }

        for (int IndexOfRow = 0; IndexOfRow < matrix1.Value.GetLength(0); IndexOfRow++)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < matrix1.Value.GetLength(1); IndexOfColumn++)
            {
                if (matrix1.Value[IndexOfRow, IndexOfColumn] != matrix2.Value[IndexOfRow, IndexOfColumn])
                {
                    return false;
                }
            }
        }
        return true;
    }
    public static bool operator !=(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        return !(matrix1 == matrix2);
    }

    //ПРОЧИЕ МЕТОДЫ

    public static int[,] ConvertMatrixToDouble(double[,] inputMatrix) // Приведение типов
    {
        int Rows = inputMatrix.GetLength(0);
        int Columns = inputMatrix.GetLength(1);
        int[,] outputMatrix = new int[Rows, Columns];

        for (int IndexOfRow = 0; IndexOfRow < Rows; ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Columns; ++IndexOfColumn)
            {
                outputMatrix[IndexOfRow, IndexOfColumn] = (int)inputMatrix[IndexOfRow, IndexOfColumn]; // просто отбрасываем дробную часть
            }
        }

        return outputMatrix;
    }

    public double CalculateDeterminant(int[,] matrix)
    {
        int LenghtOfMatrix = matrix.GetLength(0);
        if (LenghtOfMatrix == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        double Determinant = 0;
        for (int IndexOfRow = 0; IndexOfRow < LenghtOfMatrix; ++IndexOfRow)
        {
            Determinant += Math.Pow(-1, IndexOfRow) * matrix[0, IndexOfRow] * CalculateDeterminant(GetSubMatrix(matrix, 0, IndexOfRow));
        }
        return Determinant;
    }

    private int[,] GetSubMatrix(int[,] matrix, int excludeRow, int excludeCol)
    {
        int LenghtOfMatrix = matrix.GetLength(0);
        int[,] subMatrix = new int[LenghtOfMatrix - 1, LenghtOfMatrix - 1];
        int Row = -1;
        for (int IndexOfRow = 0; IndexOfRow < LenghtOfMatrix; ++IndexOfRow)
        {
            if (IndexOfRow == excludeRow)
            {
                continue;
            }
            ++Row;
            int Column = -1;
            for (int IndexOfColumn = 0; IndexOfColumn < LenghtOfMatrix; ++IndexOfColumn)
            {
                if (IndexOfColumn == excludeCol) continue;
                subMatrix[Row, ++Column] = matrix[IndexOfRow, IndexOfColumn];
            }
        }
        return subMatrix;
    }



    public double[,] InvertMatrix(int[,] matrix)
    {
        int LenthgtOfMatrix = matrix.GetLength(0);

        // Проверяем, существует ли обратная матрица (детерминант не равен 0)
        if (CalculateDeterminant(matrix) == 0)
        {
            throw new SquareMatrixDimensionsException("Матрица вырожденная, обратной матрицы не существует.");
        }

        double Determinant = CalculateDeterminant(matrix);

        // Находим присоединенную матрицу
        int[,] AdjointMatrix = GetAdjointMatrix(matrix);

        // Создаем обратную матрицу путем деления каждого элемента присоединенной матрицы на детерминант
        double[,] InverseMatrix = new double[LenthgtOfMatrix, LenthgtOfMatrix];
        for (int IndexOfRow = 0; IndexOfRow < LenthgtOfMatrix; ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < LenthgtOfMatrix; ++IndexOfColumn)
            {
                InverseMatrix[IndexOfRow, IndexOfColumn] = AdjointMatrix[IndexOfRow, IndexOfColumn] / Determinant;
            }
        }

        return InverseMatrix;
    }

    private int[,] GetAdjointMatrix(int[,] matrix)
    {
        int LenghtOfMatrix = matrix.GetLength(0);
        int[,] Adjoint = new int[LenghtOfMatrix, LenghtOfMatrix];

        for (int IndexOfRow = 0; IndexOfRow < LenghtOfMatrix; ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < LenghtOfMatrix; ++IndexOfColumn)
            {
                Adjoint[IndexOfRow, IndexOfColumn] = (int)Math.Pow(-1, IndexOfRow + IndexOfColumn) * (int)CalculateDeterminant(GetSubMatrix(matrix, IndexOfRow, IndexOfColumn));
            }
        }
        // Транспонируем присоединенную матрицу для получения окончательной присоединенной матрицы
        return TransposeMatrix(Adjoint);
    }
    private int[,] TransposeMatrix(int[,] matrix)
    {
        int Rows = matrix.GetLength(0);
        int Columns = matrix.GetLength(1);

        int[,] TransposedMatrix = new int[Columns, Rows];

        for (int IndexOfRow = 0; IndexOfRow < Columns; IndexOfRow++)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Rows; IndexOfColumn++)
            {
                TransposedMatrix[IndexOfRow, IndexOfColumn] = matrix[IndexOfColumn, IndexOfRow];
            }
        }

        return TransposedMatrix;
    }

    public void PrintMatrix()
    {
        for (int IndexOfRow = 0; IndexOfRow < Matrix.GetLength(0); ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Matrix.GetLength(1); ++IndexOfColumn)
            {
                Console.Write(Matrix[IndexOfRow, IndexOfColumn] + " ");
            }
            Console.WriteLine();
        }
    }

    public void PrintDeterminant()
    {
        Determinant = CalculateDeterminant(Matrix);
        Console.WriteLine(Determinant);
    }

    public void PrintInvertMatrix()
    {
        double[,] InvertedMatrix = InvertMatrix(Matrix);
        int Rows = InvertedMatrix.GetLength(0);
        int Columns = InvertedMatrix.GetLength(1);

        for (int IndexOfRow = 0; IndexOfRow < Rows; IndexOfRow++)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Columns; IndexOfColumn++)
            {
                Console.Write(InvertedMatrix[IndexOfRow, IndexOfColumn].ToString("0.000") + "\t");
            }
            Console.WriteLine();
        }
    }

    public void PrintClone()
    {
        SquareMatrix СlonedMatrix = (SquareMatrix)Clone();
        СlonedMatrix.PrintMatrix();
    }

    public void PrintHashCode()
    {
        var HashCode = Matrix.GetHashCode();
        Console.WriteLine(HashCode);
    }


    //Методы ToString(), CompareTo(), Equals(), GetHashCode():

    public override string ToString()
    {
        string MatrixString = "";
        for (int IndexOfRow = 0; IndexOfRow < Value.GetLength(0); IndexOfRow++)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Value.GetLength(1); IndexOfColumn++)
            {
                MatrixString += Value[IndexOfRow, IndexOfColumn] + "\t";
            }
            MatrixString += "\n";
        }
        return MatrixString;
    }

    public int CompareTo(SquareMatrix other)
    {
        if (other == null)
        {
            return 1; // Матрица не существует, поэтому текущая матрица больше
        }
        if (this.Value.GetLength(0) != other.Value.GetLength(0) || this.Value.GetLength(1) != other.Value.GetLength(1))
        {
            return -1;
        }
        return 0;
    }
    public override bool Equals(object obj)
    {
        return Equals(obj as SquareMatrix);
    }

    public bool Equals(SquareMatrix other)
    {
        if (other == null)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        if (this.Value.GetLength(0) != other.Value.GetLength(0) || this.Value.GetLength(1) != other.Value.GetLength(1))
        {
            return false;
        }
        for (int IndexOfRow = 0; IndexOfRow < this.Value.GetLength(0); IndexOfRow++)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < this.Value.GetLength(1); IndexOfColumn++)
            {
                if (this.Value[IndexOfRow, IndexOfColumn] != other.Value[IndexOfRow, IndexOfColumn])
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    public override int GetHashCode()
    {
        unchecked
        {
            int Hash = 17;

            for (int IndexOfRow = 0; IndexOfRow < Value.GetLength(0); ++IndexOfRow)
            {
                for (int IndexOfColumn = 0; IndexOfColumn < Value.GetLength(1); ++IndexOfColumn)
                {
                    Hash = Hash * 23 + Value[IndexOfRow, IndexOfColumn].GetHashCode();
                }
            }

            return Hash;
        }
    }

    //Паттерн "прототип" 

    public object Clone()
    {
        SquareMatrix NewMatrix = new SquareMatrix(Matrix.GetLength(0));
        for (int IndexOfRow = 0; IndexOfRow < Matrix.GetLength(0); ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Matrix.GetLength(1); ++IndexOfColumn)
            {
                NewMatrix.Matrix[IndexOfRow, IndexOfColumn] = Matrix[IndexOfRow, IndexOfColumn];
            }
        }
        return NewMatrix;
    }

}
class SquareMatrixDimensionsException : Exception
{
    public int SizeOfMatrix1 { get; }
    public int SizeOfMatrix2 { get; }

    public SquareMatrixDimensionsException()
    {
    }

    public SquareMatrixDimensionsException(string message)
      : base(message)
    {
    }

    public SquareMatrixDimensionsException(string message, int sizeOfMatrix1, int sizeOfMatrix2)
      : base(message)
    {
        SizeOfMatrix1 = sizeOfMatrix1;
        SizeOfMatrix2 = sizeOfMatrix2;
    }
}



class Program
{
    static void Main(string[] args)
    {
        int SizeOfMatrix1 = 0;
        int SizeOfMatrix2 = 0;

        try
        {
            Console.WriteLine("Введите размер первой квадратной матрицы (одно число):");
            SizeOfMatrix1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите размер второй квадратной матрицы (одно число):");
            SizeOfMatrix2 = int.Parse(Console.ReadLine());
        }
        catch (Exception)
        {
            Console.WriteLine($"Ошибка: Вы ничего не ввели!");
        }

        SquareMatrix ExampleMatrix1 = new SquareMatrix(SizeOfMatrix1);
        SquareMatrix ExampleMatrix2 = new SquareMatrix(SizeOfMatrix2);

        Console.WriteLine();
        ExampleMatrix1.PrintMatrix();
        Console.WriteLine();
        ExampleMatrix2.PrintMatrix();
        Console.WriteLine();

        try
        {
            Console.WriteLine("Обратная первая матрица:");
            ExampleMatrix1.PrintInvertMatrix();
            Console.WriteLine();
        }
        catch (SquareMatrixDimensionsException exception)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
        }
        try
        {
            Console.WriteLine("Обратная вторая матрица:");
            ExampleMatrix2.PrintInvertMatrix();
            Console.WriteLine();
        }
        catch (SquareMatrixDimensionsException exception)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
        }

        Console.WriteLine("Прототип первой матрицы:");
        ExampleMatrix1.Clone();
        ExampleMatrix1.PrintClone();
        Console.WriteLine();
        try
        {
            Console.WriteLine("Сложение матриц:");
            Console.WriteLine(ExampleMatrix1 + ExampleMatrix2);
            Console.WriteLine();
            Console.WriteLine("Произведение матриц:");
            Console.WriteLine(ExampleMatrix1 * ExampleMatrix2);
            Console.WriteLine();
            Console.WriteLine("Сравнение матриц (==):");
            Console.WriteLine(ExampleMatrix1 == ExampleMatrix2);
            Console.WriteLine();
            Console.WriteLine("Сравнение матриц (>):");
            Console.WriteLine(ExampleMatrix1 == ExampleMatrix2);
            Console.WriteLine();
        }
        catch (SquareMatrixDimensionsException exception)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
            Console.WriteLine($"Определись с размером! Либо: {exception.SizeOfMatrix1} Либо: {exception.SizeOfMatrix2}");
        }

        Console.WriteLine("Определитель первой матрицы:");
        ExampleMatrix1.PrintDeterminant();
        Console.WriteLine();
        Console.WriteLine("Хеш код первой матрицы:");
        ExampleMatrix1.PrintHashCode();
    }
}
