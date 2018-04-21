using System.Collections.Generic;

namespace Mediator_BugOnLoad.Extensions.MediatorX
{
    public class Mediator
    {
        MultiDictionary<string, IColleague> internalList = new MultiDictionary<string, IColleague>();

        public void Register(IColleague colleague, IEnumerable<string> messages)
        {
            foreach (string message in messages)
            {
                internalList.AddValue(message, colleague);
            }
        }

        public void NotifyColleagues(string message, object args)
        {
            if (internalList.ContainsKey(message))
            {
                //forward the message to all listeners
                foreach (IColleague colleague in internalList[message])
                {
                    colleague.MessageNotification(message, args);
                }
            }
        }
    }
}
