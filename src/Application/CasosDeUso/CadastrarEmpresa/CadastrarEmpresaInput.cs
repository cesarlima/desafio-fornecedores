namespace Application.CasosDeUso.CadastrarEmpresa
{
    public sealed class CadastrarEmpresaInput
    {
        public string UF { get; }
        public string NomeFantasia { get; }
        public string CNPJ { get; }

        public CadastrarEmpresaInput(string uf, string nomeFantasia, string cnpj)
        {
            UF = uf;
            NomeFantasia = nomeFantasia;
            CNPJ = cnpj;
        }
    }
}