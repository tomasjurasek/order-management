using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Contracts.Events.Logistics
{
    public class ZakazkaDelivered : OrderMessage
    {
        public static string Topic = "events.zakazka-delivered";
    }
}
