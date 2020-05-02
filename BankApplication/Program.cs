using System;
using System.Runtime.InteropServices;
using BankLibrary;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("ADAbank");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. To open the account \t 2. To withdraw amount \t 3. To put on account");
                Console.WriteLine("4. To cloe the account \t 5. Miss the day \t 6. To leave the program");
                Console.WriteLine("Enter number");
                Console.ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());
                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    //Display an error message in red
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }
        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Specify the amount to create the account");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Choose account type: 1.Call account 2. Depoiste");
            AccountType accountType;
            int type = Convert.ToInt32(Console.ReadLine());
            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open(accountType,
                sum,
                 AddSumHandler,
                 WithdrawSumHandler,
                 (o, e) => Console.WriteLine(e.Message),//Interest calculation handler as lambda expression
                 CloseAccountHandler,
                  OpenAccountHandler);
        }
        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Specify the amount to withdraw from your account");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter account id:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Enter amount to put on account:");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter account id:");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Put(sum, id);
        }
        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter the id of the account you want to close:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }

        //Account class event handlers
        //handler of opening account
        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        //handler of puting amount on account
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        //handler of withdraw amount 
        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if (e.Sum > 0)
                Console.WriteLine("Go to spend money");
        }

        //handler of closing account
        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
