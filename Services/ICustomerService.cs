using tp.Models;

namespace tp.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customers> GetAllCustomers();
        void AddCustomer(Customers customer);
    }
}
