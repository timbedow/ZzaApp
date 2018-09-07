using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using Zza.Entities;

namespace Zza.Services
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class ZzaService : IZzaService, IDisposable
    {
        readonly ZzaDbContext context = new ZzaDbContext();

        public void Dispose()
        {
            context.Dispose();
        }

        public List<Customer> GetCustomers()
        {
            return context.Customers.ToList();
        }

        public List<Product> GetProducts()
        {
            return context.Products.ToList();
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void SubmitOrder(Order order)
        {
            context.Orders.Add(order);
            order.OrderItems.ForEach(oi => context.OrderItems.Add(oi));
            context.SaveChanges();
        }
    }
}
