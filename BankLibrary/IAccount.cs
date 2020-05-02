using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public interface IAccount
    {
        //Put money in the account
        void Put(decimal sum);

        //Take money from the account
        decimal Withdraw(decimal sum);
    }
}
