using BankManagement.Model;

namespace BankManagement.Service;

public class CustomerService
{
    private List<Customer> _customers = new();
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

    public Customer? GetCustomerById(int CustomerId)
    {
        return _customers.FirstOrDefault(x => x.CustomerId == CustomerId);
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
        return _customers.Where(x => x.Name.Contains(name)).ToList();
    }
}