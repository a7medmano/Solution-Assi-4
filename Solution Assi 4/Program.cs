using System;
using System.Collections.Generic;

public class Account
{
    public string Name { get; set; }
    public double Balance { get; set; }

    public Account(string name = "Unnamed Account",
                   double balance = 0.0)
    {
        Name = name;
        Balance = balance;
    }

    public virtual bool Deposit(double amount)
    {
        if (amount < 0)
            return false;

        Balance += amount;
        return true;
    }

    public virtual bool Withdraw(double amount)
    {
        if (amount < 0 || Balance < amount)
            return false;

        Balance -= amount;
        return true;
    }

    public override string ToString()
    {
        return $"{Name}: Balance = {Balance:C}";
    }
}


public class SavingsAccount : Account
{
    public double InterestRate { get; set; }

    public SavingsAccount(
        string name = "Unnamed Savings Account",
        double balance = 0.0,
        double interestRate = 0.0)
        : base(name, balance)
    {
        InterestRate = interestRate;
    }

    public override bool Deposit(double amount)
    {
        if (amount < 0)
            return false;

        double interest = amount * InterestRate / 100;
        Balance += amount + interest;
        return true;
    }
}

public class CheckingAccount : Account
{
    private const double Fee = 1.50;

    public CheckingAccount(
        string name = "Unnamed Checking Account",
        double balance = 0.0)
        : base(name, balance)
    {
    }

    public override bool Withdraw(double amount)
    {
        if (amount <= 0 || Balance < amount + Fee)
            return false;

        Balance -= amount + Fee;
        return true;
    }
}


public class TrustAccount : SavingsAccount
{
    private int withdrawals = 0;
    private const int MaxWithdrawals = 3;
    private const double Bonus = 50.0;

    public TrustAccount(
        string name = "Unnamed Trust Account",
        double balance = 0.0,
        double interestRate = 0.0)
        : base(name, balance, interestRate)
    {
    }

    public override bool Deposit(double amount)
    {
        if (amount < 0)
            return false;

        double interest = amount * InterestRate / 100;
        double bonus = amount >= 5000 ? Bonus : 0;

        Balance += amount + interest + bonus;
        return true;
    }

    public override bool Withdraw(double amount)
    {
        if (amount <= 0)
            return false;

        if (withdrawals >= MaxWithdrawals)
            return false;

        if (amount >= Balance * 0.20)
            return false;

        if (Balance < amount)
            return false;

        Balance -= amount;
        withdrawals++;

        return true;
    }
}


public static class AccountUtil
{
    public static void Deposit(List<Account> accounts,
                               double amount)
    {
        foreach (Account acc in accounts)
            PrintResult(acc, amount, acc.Deposit(amount), true);
    }

    public static void Withdraw(List<Account> accounts,
                                double amount)
    {
        foreach (Account acc in accounts)
            PrintResult(acc, amount, acc.Withdraw(amount), false);
    }

    public static void DepositSavings(
        List<SavingsAccount> accounts, double amount)
    {
        foreach (SavingsAccount acc in accounts)
            PrintResult(acc, amount, acc.Deposit(amount), true);
    }

    public static void WithdrawSavings(
        List<SavingsAccount> accounts, double amount)
    {
        foreach (SavingsAccount acc in accounts)
            PrintResult(acc, amount, acc.Withdraw(amount), false);
    }

    public static void DepositChecking(
        List<CheckingAccount> accounts, double amount)
    {
        foreach (CheckingAccount acc in accounts)
            PrintResult(acc, amount, acc.Deposit(amount), true);
    }

    public static void WithdrawChecking(
        List<CheckingAccount> accounts, double amount)
    {
        foreach (CheckingAccount acc in accounts)
            PrintResult(acc, amount, acc.Withdraw(amount), false);
    }

    public static void DepositTrust(
        List<TrustAccount> accounts, double amount)
    {
        foreach (TrustAccount acc in accounts)
            PrintResult(acc, amount, acc.Deposit(amount), true);
    }

    public static void WithdrawTrust(
        List<TrustAccount> accounts, double amount)
    {
        foreach (TrustAccount acc in accounts)
            PrintResult(acc, amount, acc.Withdraw(amount), false);
    }

    private static void PrintResult(
        Account acc, double amount, bool success, bool deposit)
    {
        string action = deposit ? "Deposited" : "Withdrew";
        string failedAction = deposit ? "Deposit" : "Withdrawal";

        if (success)
            Console.WriteLine(
                $"{action} {amount:C} {(deposit ? "to" : "from")} {acc}");
        else
            Console.WriteLine(
                $"Failed {failedAction} of {amount:C} for {acc}");
    }
}

class Program
{
    static void Main()
    {

        var accounts = new List<Account>
        {
            new Account(),
            new Account("Larry"),
            new Account("Moe", 2000),
            new Account("Curly", 5000)
        };

        AccountUtil.Deposit(accounts, 1000);
        AccountUtil.Withdraw(accounts, 2000);


        var savAccounts = new List<SavingsAccount>
        {
            new SavingsAccount(),
            new SavingsAccount("Superman"),
            new SavingsAccount("Batman", 2000),
            new SavingsAccount("Wonderwoman", 5000, 5.0)
        };

        AccountUtil.DepositSavings(savAccounts, 1000);
        AccountUtil.WithdrawSavings(savAccounts, 2000);

        // Checking Accounts
        var checAccounts = new List<CheckingAccount>
        {
            new CheckingAccount(),
            new CheckingAccount("Larry2"),
            new CheckingAccount("Moe2", 2000),
            new CheckingAccount("Curly2", 5000)
        };

        AccountUtil.DepositChecking(checAccounts, 1000);
        AccountUtil.WithdrawChecking(checAccounts, 2000);
        AccountUtil.WithdrawChecking(checAccounts, 2000);


        var trustAccounts = new List<TrustAccount>
        {
            new TrustAccount(),
            new TrustAccount("Superman2"),
            new TrustAccount("Batman2", 2000),
            new TrustAccount("Wonderwoman2", 5000, 5.0)
        };

        AccountUtil.DepositTrust(trustAccounts, 1000);
        AccountUtil.DepositTrust(trustAccounts, 6000);
        AccountUtil.WithdrawTrust(trustAccounts, 2000);
        AccountUtil.WithdrawTrust(trustAccounts, 3000);
        AccountUtil.WithdrawTrust(trustAccounts, 500);
    }
}
