using System.Threading.Tasks;

namespace Application.CasosDeUso
{
    public interface IUseCase<in T>
    {
         Task Execute(T input);
    }
}