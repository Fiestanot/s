using Entity;
using Repostiroy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(string connectionString, string database, string collection) : base(connectionString, database, collection)
        {
        }
    }
}
