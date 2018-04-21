namespace Mediator_GoodLoad.Extensions.MediatorX
{
    public interface IColleague
    {
        Mediator Mediator { get; }

        void MessageNotification(string message, object args);
    }
}
