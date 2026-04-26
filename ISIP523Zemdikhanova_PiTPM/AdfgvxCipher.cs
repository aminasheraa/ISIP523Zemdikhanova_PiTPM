using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Статический класс, реализующий криптографический алгоритм ADFGVX.
/// </summary>
/// <remarks>
/// Шифр ADFGVX сочетает в себе два метода защиты: 
/// 1. Замена через квадрат Полибия 6x6.
/// 2. Транспозиция на основе ключевого слова.
/// </remarks>
public static class AdfgvxCipher
{
    private const string Adfgvx = "ADFGVX";
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    /// <summary>
    /// Возвращает базовый набор символов (A-Z, 0-9) для инициализации матрицы.
    /// </summary>
    /// <returns>Строка, содержащая стандартный алфавит шифра.</returns>
    public static string GetDefaultMatrix() => Alphabet;

    /// <summary>
    /// Выполняет полное шифрование входного сообщения.
    /// </summary>
    /// <param name="text">Открытый текст для шифрования.</param>
    /// <param name="key">Ключевое слово, определяющее порядок перестановки столбцов.</param>
    /// <param name="matrixStr">Текущая конфигурация матрицы 6x6 (алфавит замены).</param>
    /// <returns>Зашифрованная строка, состоящая только из символов A, D, F, G, V, X.</returns>
    /// <remarks>
    /// На первом этапе каждая буква текста заменяется на два символа (координаты строки и столбца матрицы).
    /// На втором этапе полученная последовательность проходит процедуру транспозиции.
    /// </remarks>
    public static string Encrypt(string text, string key, string matrixStr = Alphabet)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(key)) return string.Empty;

        text = text.ToUpper().Replace(" ", "");
        key = key.ToUpper();

        string substitution = "";
        foreach (char c in text)
        {
            int index = matrixStr.IndexOf(c);
            if (index == -1) continue;

            substitution += Adfgvx[index / 6]; 
            substitution += Adfgvx[index % 6]; 
        }

        return Transpose(substitution, key);
    }

    /// <summary>
    /// Выполняет расшифрование сообщения.
    /// </summary>
    /// <param name="cipher">Зашифрованный текст (состоящий из букв ADFGVX).</param>
    /// <param name="key">Ключевое слово, использованное при шифровании.</param>
    /// <param name="matrixStr">Конфигурация матрицы 6x6, использованная при шифровании.</param>
    /// <returns>Исходное расшифрованное сообщение.</returns>
    /// <remarks>
    /// Процесс обратный шифрованию. Сначала восстанавливается порядок символов,
    /// затем пары символов ADFGVX преобразуются обратно в буквы алфавита по матрице.
    /// </remarks>
    public static string Decrypt(string cipher, string key, string matrixStr = Alphabet)
    {
        if (string.IsNullOrEmpty(cipher) || string.IsNullOrEmpty(key)) return string.Empty;

        key = key.ToUpper();
        string detransposed = DeTranspose(cipher, key);

        string result = "";
        for (int i = 0; i < detransposed.Length; i += 2)
        {
            int row = Adfgvx.IndexOf(detransposed[i]);
            int col = Adfgvx.IndexOf(detransposed[i + 1]);

            if (row == -1 || col == -1) return string.Empty;

            result += matrixStr[row * 6 + col];
        }
        return result;
    }

    /// <summary>
    /// Транспозиция.
    /// </summary>
    /// <param name="input">Строка после этапа замены.</param>
    /// <param name="key">Ключевое слово для сортировки столбцов.</param>
    /// <returns>Строка с переставленными символами.</returns>
    /// <remarks>
    /// Текст записывается в таблицу по строкам, а затем считывается по столбцам 
    /// в порядке, определяемом алфавитным порядком символов ключа.
    /// </remarks>
    private static string Transpose(string input, string key)
    {
        int columns = key.Length;
        int rows = (int)Math.Ceiling((double)input.Length / columns);
        char[,] grid = new char[rows, columns];

        for (int i = 0; i < input.Length; i++)
        {
            grid[i / columns, i % columns] = input[i];
        }

        int[] keyOrder = GetKeyOrder(key);
        string output = "";

        foreach (int index in keyOrder)
        {
            for (int r = 0; r < rows; r++)
            {
                if (grid[r, index] != '\0')
                {
                    output += grid[r, index];
                }
            }
        }

        return output;
    }

    /// <summary>
    /// Внутренний метод для восстановления исходного порядка символов перед дешифровкой.
    /// </summary>
    /// <param name="input">Зашифрованный текст.</param>
    /// <param name="key">Ключевое слово.</param>
    /// <returns>Строка с восстановленным порядком символов для этапа дешифровки по матрице.</returns>
    /// <remarks>
    /// Метод учитывает возможные "пустые" ячейки в последней строке таблицы, 
    /// если длина сообщения не кратна длине ключа.
    /// </remarks>
    private static string DeTranspose(string input, string key)
    {
        int columns = key.Length;
        int rows = input.Length / columns;
        int extraChars = input.Length % columns;

        int[] keyOrder = GetKeyOrder(key);
        char[,] grid = new char[rows + 1, columns];
        int currentPos = 0;

        foreach (int colIdx in keyOrder)
        {
            int charsInCol = rows + (colIdx < extraChars ? 1 : 0);
            for (int r = 0; r < charsInCol; r++)
            {
                if (currentPos < input.Length)
                {
                    grid[r, colIdx] = input[currentPos++];
                }
            }
        }

        string output = "";
        for (int r = 0; r <= rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                if (grid[r, c] != '\0')
                {
                    output += grid[r, c];
                }
            }
        }

        return output;
    }

    /// <summary>
    /// Вычисляет алфавитный порядок индексов на основе символов ключевого слова.
    /// </summary>
    /// <param name="key">Ключевое слово.</param>
    /// <returns>Массив целых чисел, представляющий последовательность обработки столбцов.</returns>
    private static int[] GetKeyOrder(string key)
    {
        return key.Select((c, i) => new { Char = c, Index = i }).OrderBy(x => x.Char).Select(x => x.Index).ToArray();
    }
}