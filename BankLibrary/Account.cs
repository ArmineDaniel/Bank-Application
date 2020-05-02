using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BankLibrary
{
    public abstract  class Account:IAccount
    {
        //Event that occurs during withdrawal of money
        protected internal event AccountStateHandler Withdrawed;

        //Event that occurs during adding of money
        protected internal event AccountStateHandler Added;

        //Event that occurs during account opening
        protected internal event AccountStateHandler Opened;

        //Event that occurs during account closing
        protected internal event AccountStateHandler Closed;

        //Event that occurs when interest is calculated
        protected internal event AccountStateHandler Calculated;

        static int counter = 0;
        protected int _days = 0; //Time period

        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;
        }

        //Current amount in account
        public decimal Sum { get; private set; }

        //Percent of charges
        public int Percentage { get; private set; }

        //identifier of the account
        public int Id { get; private set; }

        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }
        //Each event has its dedicated virtual method
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }
        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs("added to account " + sum, sum));
        }
        //Withdrawal method, returns how much is withdrawn
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($" {sum} drawn from account {Id}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Not enough money in your account {Id}", 0));
            }
            return result;
        }

        //Account opening 
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Open the new account! Account Id: {Id}", Sum));
        }

        //Account closing
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Account {Id} closed.  total amount: {Sum}", Sum));
        }
        protected internal void IncrementDays()
        {
            _days++;
        }
        //Counting the percent
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Sum + increment;
            OnCalculated(new AccountEventArgs($"Counting the percent: {increment}", increment));
        }
    }
}
