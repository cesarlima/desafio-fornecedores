using System.Collections.Generic;

namespace Application.CasosDeUso
{
    public interface IOutputPortNotification
    {
         void AddNotification(string message);
         void AddNotifications(IEnumerable<string> messages);
         bool Valid { get; }
         bool InValid { get; }
    }
}