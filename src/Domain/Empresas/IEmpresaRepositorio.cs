using System.Threading.Tasks;

namespace Domain.Empresas
{
    public interface IEmpresaRepositorio
    {
         Task Save(Empresa empresa);
         Task<bool> EmpresaJaCadastrada(string cnpj);
    }
}