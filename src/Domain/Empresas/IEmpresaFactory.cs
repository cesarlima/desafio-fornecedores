namespace Domain.Empresas
{
    public interface IEmpresaFactory
    {
         Empresa NovaEmpresa(string uf, string nomeFantasia, string cnpj);
    }
}