using tp.Models;

namespace tp.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customers> GetAllCustomers();
        void AddCustomer(Customers customer);
    }
}
