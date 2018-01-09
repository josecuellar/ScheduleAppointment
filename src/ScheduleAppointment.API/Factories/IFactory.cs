namespace ScheduleAppointment.API.Factories
{
    public interface IFactory<TIn, TOut>
    {
        TOut From(TIn inputObject);
    }
}
