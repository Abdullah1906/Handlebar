using HandlebarPractice.Models;

namespace HandlebarPractice.Interfaces
{
    public interface ICustomer
    {
        List<Customer> GetAllCustomers();
        Customer? GetCustomerById(long id);
        void SaveCustomer(Customer customer);
        void DeleteCustomer(long id);
    }
    
}
