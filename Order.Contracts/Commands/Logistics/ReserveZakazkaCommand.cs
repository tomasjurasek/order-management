namespace Order.Contracts.Commands.Logistics;

public class ReserveZakazkaCommand : OrderMessage
{
    public static string Topic = "commands.reserve-zakazka";
}
