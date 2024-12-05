public static class ArrayExtensions
{
    public static T[,] To2D<T>(this T[][] source)
    {
        int rows = source.Length;
        int cols = source[0].Length;
        T[,] result = new T[rows, cols];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[i, j] = source[i][j];

        return result;
    }
}