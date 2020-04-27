namespace Application.CasosDeUso
{
    public interface IOutputPortNotification
    {
         void AddNotification(string message);
         bool Valid { get; }
         bool InValid { get; }
    }
}