using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public delegate void AccountStateHandler(object sender, AccountEventArgs e);
    public class AccountEventArgs
    {
        //message
        public string Message { get; private set; }

        //Amount by which the account has changed
        public decimal Sum { get; private set; }
        public AccountEventArgs(string _mes, decimal _sum)
        {
            Message = _mes;
            Sum = _sum;
        }
    }
}
