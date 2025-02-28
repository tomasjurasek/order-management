using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Contracts
{
    public abstract class OrderMessage
    {
        public Guid OrderId { get; set; }
    }
}
