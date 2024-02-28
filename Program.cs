using System;
using System.Linq.Expressions;

class SquareMatrix
{
    int[,] _matrix;
    public int[,] Value { get { return _matrix; } }
    static readonly Random s_random = new Random();

    public SquareMatrix(int size)
    {

        try
        {
            _matrix = new int[size, size];
        }
        catch (Exception e) 
        {
            Console.WriteLine(e.Message);   
        }
        
        for (var RowOfMatrix = 0; RowOfMatrix < _matrix.GetLength(0); ++RowOfMatrix)
        {
            for (var ColumnOfMatrix = 0; ColumnOfMatrix < _matrix.GetLength(1); ++ColumnOfMatrix)
            {
                int RandomNumber = s_random.Next(100);
                _matrix[RowOfMatrix, ColumnOfMatrix] = RandomNumber;
            }
        }
    }

    // Далее перегрузки:
    public static SquareMatrix operator +(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        int Rows = matrix1.Value.GetLength(0);
        int Columns = matrix1.Value.GetLength(1);

        if (Rows != matrix2.Value.GetLength(0) || Columns != matrix2.Value.GetLength(1))
        {
            throw new SquareMatrixDimensionsException("Матрицы разного размера.", Rows, matrix2.Value.GetLength(0));
        }

        int[,] MatrixResultOfOperation = new int[Rows, Columns];
        for (int IndexOfRow = 0; IndexOfRow < Rows; ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Columns; ++IndexOfColumn)
            {
                MatrixResultOfOperation[IndexOfRow, IndexOfColumn] = matrix1.Value[IndexOfRow, IndexOfColumn] + matrix2.Value[IndexOfRow, IndexOfColumn];
            }
        }
        SquareMatrix NewMatrix = new SquareMatrix(Rows);
        NewMatrix._matrix = MatrixResultOfOperation;
        return NewMatrix;
    }

    public static SquareMatrix operator *(SquareMatrix matrix1, SquareMatrix matrix2)
    {
        int RowCount1 = matrix1.Value.GetLength(0);
        int ColumnCount1 = matrix1.Value.GetLength(1);
        int RowCount2 = matrix2.Value.GetLength(0);
        int ColumnCount2 = matrix2.Value.GetLength(1);
        if (ColumnCount1 != RowCount2) 
        {
            throw new SquareMatrixDimensionsException("Нельзя перемножить матрицы. Количество столбцов матрицы 1 не равно количеству строк матрицы 2.", matrix1.Value.GetLength(0), matrix2.Value.GetLength(0));
        }
        int[,] MatrixResultOfOperation = new int[RowCount1, ColumnCount2];

        for (int IndexOfRow1 = 0; IndexOfRow1 < RowCount1; ++IndexOfRow1)
        {
            for (int IndexOfColumn2 = 0; IndexOfColumn2 < ColumnCount2; ++IndexOfColumn2)
            {
                for (int IndexOfColumn1 = 0; IndexOfColumn1 < ColumnCount1; ++IndexOfColumn1)
                {
                    MatrixResultOfOperation[IndexOfRow1, IndexOfColumn2] += matrix1.Value[IndexOfRow1, IndexOfColumn1] * matrix2.Value[IndexOfColumn1, IndexOfColumn2];
                }
            }
        }

        SquareMatrix NewMatrix = new SquareMatrix(RowCount1);
        NewMatrix._matrix = MatrixResultOfOperation;
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

    // ПРОЧИЕ МЕТОДЫ

    public static explicit operator int[,](SquareMatrix inputMatrix) // Приведение типов
    {
        return inputMatrix._matrix;
    }

    public double CalculateDeterminant(int[,] matrix)
    {
        int LengthOfMatrix = matrix.GetLength(0);
        if (LengthOfMatrix == 2)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        double Determinant = 0;
        for (int IndexOfRow = 0; IndexOfRow < LengthOfMatrix; ++IndexOfRow)
        {
            Determinant += Math.Pow(-1, IndexOfRow) * matrix[0, IndexOfRow] * CalculateDeterminant(GetSubMatrix(matrix, 0, IndexOfRow));
        }
        return Determinant;
    }

    private int[,] GetSubMatrix(int[,] matrix, int excludeRow, int excludeCol)
    {
        int LengthOfMatrix = matrix.GetLength(0);
        int[,] SubMatrix = new int[LengthOfMatrix - 1, LengthOfMatrix - 1];
        int Row = -1;
        for (int IndexOfRow = 0; IndexOfRow < LengthOfMatrix; ++IndexOfRow)
        {
            if (IndexOfRow == excludeRow)
            {
                continue;
            }
            ++Row;
            int Column = -1;
            for (int IndexOfColumn = 0; IndexOfColumn < LengthOfMatrix; ++IndexOfColumn)
            {
                if (IndexOfColumn == excludeCol) continue;
                SubMatrix[Row, ++Column] = matrix[IndexOfRow, IndexOfColumn];
            }
        }
        return SubMatrix;
    }

    public double[,] InvertMatrix(int[,] matrix)
    {
        int LengthOfMatrix = matrix.GetLength(0);

        // Проверяем, существует ли обратная матрица (детерминант не равен 0)
        double Determinant = CalculateDeterminant(matrix);
        if (Determinant == 0)
        {
            throw new SquareMatrixDimensionsException("Матрица вырожденная, обратной матрицы не существует.");
        }
        // Создаем обратную матрицу путем деления каждого элемента присоединенной матрицы на детерминант
        double[,] InverseMatrix = new double[LengthOfMatrix, LengthOfMatrix];
        if (LengthOfMatrix == 2)
        {
            double multiplier = 1.0 / Determinant;

            InverseMatrix[0, 0] = matrix[1, 1] * multiplier;
            InverseMatrix[0, 1] = -matrix[0, 1] * multiplier;
            InverseMatrix[1, 0] = -matrix[1, 0] * multiplier;
            InverseMatrix[1, 1] = matrix[0, 0] * multiplier;
            return InverseMatrix;
        }

        // Находим присоединенную матрицу
        int[,] AdjointMatrix = GetAdjointMatrix(matrix);

        for (int IndexOfRow = 0; IndexOfRow < LengthOfMatrix; ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < LengthOfMatrix; ++IndexOfColumn)
            {
                InverseMatrix[IndexOfRow, IndexOfColumn] = AdjointMatrix[IndexOfRow, IndexOfColumn] / Determinant;
            }
        }

        return InverseMatrix;
    }

    private int[,] GetAdjointMatrix(int[,] matrix)
    {
        int LengthOfMatrix = matrix.GetLength(0);
        int[,] Adjoint = new int[LengthOfMatrix, LengthOfMatrix];

        for (int IndexOfRow = 0; IndexOfRow < LengthOfMatrix; ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < LengthOfMatrix; ++IndexOfColumn)
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

        for (int IndexOfRow = 0; IndexOfRow < Columns; ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Rows; ++IndexOfColumn)
            {
                TransposedMatrix[IndexOfRow, IndexOfColumn] = matrix[IndexOfColumn, IndexOfRow];
            }
        }

        return TransposedMatrix;
    }

    public void PrintMatrix()
    {
        for (int IndexOfRow = 0; IndexOfRow < _matrix.GetLength(0); ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < _matrix.GetLength(1); ++IndexOfColumn)
            {
                Console.Write(_matrix[IndexOfRow, IndexOfColumn] + " ");
            }
            Console.WriteLine();
        }
    }

    public void PrintDeterminant()
    {
        Double Determinant = CalculateDeterminant(_matrix);
        Console.WriteLine(Determinant);
    }

    public void PrintInvertMatrix()
    {
        double[,] InvertedMatrix = InvertMatrix(_matrix);
        int Rows = InvertedMatrix.GetLength(0);
        int Columns = InvertedMatrix.GetLength(1);

        for (int IndexOfRow = 0; IndexOfRow < Rows; ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Columns; ++IndexOfColumn)
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
        var HashCode = _matrix.GetHashCode();
        Console.WriteLine(HashCode);
    }


    // Методы ToString(), CompareTo(), Equals(), GetHashCode():

    public override string ToString()
    {
        string MatrixString = "";
        for (int IndexOfRow = 0; IndexOfRow < Value.GetLength(0); ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < Value.GetLength(1); ++IndexOfColumn)
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
        for (int IndexOfRow = 0; IndexOfRow < this.Value.GetLength(0); ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < this.Value.GetLength(1); ++IndexOfColumn)
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

    // Паттерн "прототип" 
    public object Clone()
    {
        SquareMatrix NewMatrix = new SquareMatrix(_matrix.GetLength(0));
        for (int IndexOfRow = 0; IndexOfRow < _matrix.GetLength(0); ++IndexOfRow)
        {
            for (int IndexOfColumn = 0; IndexOfColumn < _matrix.GetLength(1); ++IndexOfColumn)
            {
                NewMatrix._matrix[IndexOfRow, IndexOfColumn] = _matrix[IndexOfRow, IndexOfColumn];
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
        string InputSizeOfMatrix1 = "";
        string InputSizeOfMatrix2 = "";
        int SizeOfMatrix1 = 0;
        int SizeOfMatrix2 = 0;
        bool InputSuccess = false;

        while (!InputSuccess)
        {
            try
            {
                InputSizeOfMatrix1 = ""; // обнуление для корректной работы обработчика исключений
                InputSizeOfMatrix1 = "";
                SizeOfMatrix1 = 0;
                SizeOfMatrix2 = 0;

                Console.WriteLine("Введите размер первой квадратной матрицы (одно число):");
                InputSizeOfMatrix1 = (Console.ReadLine());
                SizeOfMatrix1 = int.Parse(InputSizeOfMatrix1);

                Console.WriteLine("Введите размер второй квадратной матрицы (одно число):");
                InputSizeOfMatrix2 = (Console.ReadLine());
                SizeOfMatrix2 = int.Parse(InputSizeOfMatrix2);
                if (SizeOfMatrix1 != 0 && SizeOfMatrix2 != 0)
                {
                    InputSuccess = true;
                }
                if (SizeOfMatrix1 <= 0 || SizeOfMatrix2 <= 0)
                {
                    try
                    {
                        throw new Exception("Размер должен быть больше 0");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\nОшибка: " + ex.Message + "\n");
                        InputSuccess = false;
                    }
                    
                }
            }
            catch (FormatException)
            {
                if (InputSizeOfMatrix1.Length == 0) 
                {
                    Console.WriteLine($"Ошибка: Вы ничего не ввели для 1 матрицы!");
                }
                else if (InputSizeOfMatrix1.Length != 0 && SizeOfMatrix1 == 0) 
                {
                    Console.WriteLine($"Ошибка: Вы ввели буквы для 1 матрицы!");
                }
                else if (InputSizeOfMatrix2.Length == 0) 
                {
                    Console.WriteLine($"Ошибка: Вы ничего не ввели для 2 матрицы!");
                }
                else if (InputSizeOfMatrix2.Length != 0 && SizeOfMatrix2 == 0) 
                {
                    Console.WriteLine($"Ошибка: Вы ввели буквы для 2 матрицы!");
                }
                else if (SizeOfMatrix1 == 0 || SizeOfMatrix2 == 0)
                {
                    Console.WriteLine($"Ошибка: Размер должен быть больше нуля!");
                }
                Console.WriteLine();
            }
        }

        SquareMatrix ExampleMatrix1 = new SquareMatrix(SizeOfMatrix1);
        SquareMatrix ExampleMatrix2 = new SquareMatrix(SizeOfMatrix2);

        Console.WriteLine();
        ExampleMatrix1.PrintMatrix();
        Console.WriteLine();
        ExampleMatrix2.PrintMatrix();
        Console.WriteLine();

        Console.WriteLine("Приведение типов второй матрицы (из SquareMatrix в int[,])");
        int[,] ExampleMatrixToInt = (int[,])ExampleMatrix2;
        for (int RowOfMatrix = 0; RowOfMatrix < ExampleMatrixToInt.GetLength(0); RowOfMatrix++) // Вывод матрицы int[,]
        {
            for (int ColumnOfMatrix = 0; ColumnOfMatrix < ExampleMatrixToInt.GetLength(0); ColumnOfMatrix++)
            {
                Console.Write(ExampleMatrixToInt[RowOfMatrix, ColumnOfMatrix] + "\t");
            }
            Console.WriteLine();
        }
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
            Console.WriteLine(ExampleMatrix1 > ExampleMatrix2);
            Console.WriteLine();
        }
        catch (SquareMatrixDimensionsException exception)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
            Console.WriteLine($"Определись с размером! Либо: {exception.SizeOfMatrix1} Либо: {exception.SizeOfMatrix2}");
            Console.WriteLine();
        }

        Console.WriteLine("Определитель первой матрицы:");
        ExampleMatrix1.PrintDeterminant();
        Console.WriteLine();
        Console.WriteLine("Хеш код первой матрицы:");
        ExampleMatrix1.PrintHashCode();
    }
}
