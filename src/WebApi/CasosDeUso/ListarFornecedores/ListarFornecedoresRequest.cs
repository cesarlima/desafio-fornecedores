using System;
namespace WebApi.CasosDeUso.ListarFornecedores
{
    public sealed class ListarFornecedoresRequest
    {
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
