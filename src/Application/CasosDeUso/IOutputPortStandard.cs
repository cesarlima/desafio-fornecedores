namespace Application.CasosDeUso
{
    public interface IOutputPortStandard<in TOutput>
    {
         void AddResult(TOutput output);
    }
}