namespace VKTestTask.Services.Reservation;

public interface ILoginReservationService
{
    public void Reserve(string login);
    public void Cancel(string login);
    public bool IsReserved(string login);
}