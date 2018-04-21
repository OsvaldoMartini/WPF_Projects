namespace Mediator_BugOnLoad.Extensions.MediatorX
{
    public interface IColleague
    {
        Mediator Mediator { get; }

        void MessageNotification(string message, object args);
    }
}
