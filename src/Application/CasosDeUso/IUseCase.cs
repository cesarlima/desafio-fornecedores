using System.Threading.Tasks;

namespace Application.CasosDeUso
{
    public interface IUseCase<T>
    {
         Task Execute(T input);
    }
}