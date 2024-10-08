using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionApp.Orders.Models
{
    public class Customer
    {
        public Guid Id { get; set;}
        public string Name { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        public static Customer Create(Guid id, string name, string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            var customer = new Customer
            {
                Id = id,
                Name = name,
                Email = email
            };

            return customer;
        }
    }
}