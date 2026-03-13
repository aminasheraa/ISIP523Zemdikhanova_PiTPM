using System;

namespace BankAccountNS
{
    /// <summary>
    /// Класс, представляющий банковский счет клиента
    /// Позволяет выполнять операции пополнения и списания средств
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Имя владельца счета
        /// </summary>
        private readonly string m_customerName;

        /// <summary>
        /// Текущий баланс счета
        /// </summary>
        private double m_balance;

        /// <summary>
        /// Приватный конструктор по умолчанию
        /// Используется только внутри класса
        /// </summary>
        private BankAccount() { }

        /// <summary>
        /// Создает новый банковский счет
        /// </summary>
        /// <param name="customerName">Имя владельца счета.</param>
        /// <param name="balance">Начальный баланс счета.</param>
        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
        }

        /// <summary>
        /// Получает имя владельца счета
        /// </summary>
        public string CustomerName
        {
            get { return m_customerName; }
        }

        /// <summary>
        /// Получает текущий баланс счета
        /// </summary>
        public double Balance
        {
            get { return m_balance; }
        }

        /// <summary>
        /// Выполняет списание средств со счета
        /// </summary>
        /// <param name="amount">Сумма для списания</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Программа вызывает ошибку и прекращает выполнение операции, если сумма отрицательная или превышает баланс счета
        /// </exception>
        public void Debit(double amount)
        {
            if (amount > m_balance)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            m_balance -= amount;
        }

        /// <summary>
        /// Выполняет пополнение счета
        /// </summary>
        /// <param name="amount">Сумма пополнения</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Вызывает ошибку и прекращает выполнение операции, если сумма отрицательная
        /// </exception>
        public void Credit(double amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount");
            }

            m_balance += amount;
        }

        /// <summary>
        /// Главная точка входа консольного приложения
        /// Создает банковский счет и выполняет операции пополнения и списания
        /// </summary>

        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr. Roman Abramovich", 11.99);

            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
            Console.ReadLine();
        }
    }
}

