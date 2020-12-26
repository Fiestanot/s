using Entity;
using Repostiroy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(string connectionString, string database, string collection) : base(connectionString, database, collection)
        {
        }
    }
}
