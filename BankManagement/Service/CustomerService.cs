using BankManagement.Model;

namespace BankManagement.Service;

public class CustomerService
{
    private readonly List<Customer> _customers = new();
    private int _customerId = 1;

    public void AddCustomer(string name, string email)
    {
        _customers.Add(new Customer
        {
            CustomerId = _customerId++,
            Name = name, 
            Email = email
        });
    }

    public List<Customer> GetCustomers()
    {
        return _customers.ToList();
    }

    public Customer? GetCustomerById(int customerId)
    {
        return _customers.FirstOrDefault(x => x.CustomerId == customerId);
    }

    public bool DeleteCustomer(int id)
    {
        var customer = GetCustomerById(id);
        if (customer is null)
            return false;

        _customers.Remove(customer);
        return true;
    }

    public List<Customer> SearchCustomerByName(string name)
    {
        return _customers.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}