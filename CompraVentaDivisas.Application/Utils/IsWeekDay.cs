namespace CompraVentaDivisas.Application.Utils;

public class IsWeekday
{
    public static bool Get(DateTime date)
    {
        return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
    }
}
