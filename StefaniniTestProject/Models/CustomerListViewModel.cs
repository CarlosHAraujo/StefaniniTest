using System;
using System.Linq;
using System.Collections.Generic;

namespace StefaniniTestProject.Models
{
    public class CustomerListViewModel
    {
        public CustomerListViewModel()
        {
            this.Customers = new List<CustomerViewModel>();
            this.Search = new SearchCustomerViewModel();
        }
        public CustomerListViewModel(IEnumerable<CustomerViewModel> customers)
            : this()
        {
            if(customers != null)
            {
                this.Customers.AddRange(customers.Select(c => c));
            }
        }

        public SearchCustomerViewModel Search { get; set; }
        public List<CustomerViewModel> Customers { get; set; }
    }
}