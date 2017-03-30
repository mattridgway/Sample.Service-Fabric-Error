using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary1;
using Microsoft.ServiceFabric.Services.Remoting.Client;

namespace Web1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
			var counter = ServiceProxy.Create<ICounter>(new Uri("fabric:/Application1/Stateful1"), new Microsoft.ServiceFabric.Services.Client.ServicePartitionKey(0));
			long count = await counter.GetCount();
			return count.ToString();
        }
    }
}
