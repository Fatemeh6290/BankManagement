using BankManagement.Enums;

namespace BankManagement.Model;

public class Account
{
    public int AccountId { get; set; }
    public int CustomerId { get; set; }
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
}