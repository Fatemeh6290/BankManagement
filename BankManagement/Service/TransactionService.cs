using BankManagement.Enums;
using BankManagement.Model;

namespace BankManagement.Service;

public class TransactionService
{
    private readonly List<Transaction> _transactions = new ();
    private int _transactionId = 1;

    public void AddTransaction(int accountId, TransactionType transactionType, decimal amount)
    {
        _transactions.Add(new Transaction
        {
            TransactionId = _transactionId++,
            AccountId = accountId,
            Amount = amount,
            TransactionType = transactionType,
            Date = DateTime.Now
        });
    }

    public IReadOnlyList<Transaction> GetTransactions()
    {
        return _transactions.ToList();
    }

    public Transaction? GetTransactionById(int transactionId)
    {
        return _transactions.FirstOrDefault(x => x.TransactionId == transactionId);
    }
}