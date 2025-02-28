namespace Order.Contracts.Events.Logistics;

public class ZakazkaDelivered : OrderMessage
{
    public static string Topic = "events.zakazka-delivered";
}
