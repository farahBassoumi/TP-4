using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Web.Mvc;
using tp.Data;
using tp.Models;

namespace tp.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {

        private readonly ApplicationDBContext _db;
        private readonly Logger<Movies> _logger;


        public CustomerRepository(ApplicationDBContext db, Logger<Movies> logger)
        {
            _db = db;
            _logger = logger;

        }
       

        IEnumerable<Customers> ICustomerRepository.GetAllCustomers()
        {
            return _db.Customers.ToList();
        }

        void ICustomerRepository.AddCustomer(Customers c)
        {
            _db.Customers.Add(c);
            _db.SaveChanges();
        }
    }
}
