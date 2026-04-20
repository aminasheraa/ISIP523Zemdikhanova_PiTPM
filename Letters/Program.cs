using System;

/// <summary>
/// Демонстрирует работу с массивами и постепенное формирование строки,
/// а также отправку сообщений с увеличивающимся счётчиком.
/// </summary>
class ArrayExample
{
    /// <summary>
    /// Точка входа в программу.
    /// Создаёт массив символов, формирует имя и вызывает метод отправки сообщений.
    /// </summary>
    static void Main()
    {
        char[] letters = { 'f', 'r', 'e', 'd', ' ', 's', 'm', 'i', 't', 'h' };
        string name = "";
        int[] a = new int[10];

        for (int i = 0; i < letters.Length; i++)
        {
            name += letters[i];
            a[i] = i + 1;
            SendMessage(name, a[i]);
        }

        Console.ReadKey();
    }

    /// <summary>
    /// Выводит приветственное сообщение с именем и текущим числом.
    /// </summary>
    /// <param name="name">Имя, сформированное из массива символов.</param>
    /// <param name="msg">Число, до которого нужно считать.</param>
    static void SendMessage(string name, int msg)
    {
        Console.WriteLine("Hello, " + name + "! Count to " + msg);
    }
}