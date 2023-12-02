using tp.Models;
using tp.Repositories;

namespace tp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public void AddCustomer(Customers customer)
        {
            _customerRepository.AddCustomer(customer);
        }

        public IEnumerable<Customers> GetAllCustomers()
        {
            return(_customerRepository.GetAllCustomers());
        }
    }
}
