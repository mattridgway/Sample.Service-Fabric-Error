using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace ClassLibrary1
{
    public interface ICounter : IService
    {
		Task<long> GetCount();
    }
}
