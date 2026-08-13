using BankManagement.Enums;

namespace BankManagement.Model;

public class Transaction
{
    public int TransactionId { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType TransactionType { get; set; }
}