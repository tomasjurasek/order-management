using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Contracts.Commands.Logistics
{
    public class ReserveZakazkaCommand : OrderMessage
    {
        public static string Topic = "commands.reserve-zakazka";
    }
}
