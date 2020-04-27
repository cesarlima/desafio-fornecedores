using System;
using System.Collections.Generic;

namespace Application.CasosDeUso.CadastrarFornecedor
{
    public sealed class CadastrarFornecedorInput
    {
        public Guid EmpresaId { get; }
        public string Nome { get; }
        public string CpfCnpj { get; }
        public string RG { get; }
        public string UF { get; }
        public List<string> Telefones { get; }
        public bool PessoaJuridica { get; set; }
        public DateTime? DataNascimento { get; set; }

        public CadastrarFornecedorInput(Guid empresaId, string nome, string cpfCnpj, string uf, string rg, List<string> telefones, bool pessoaJuridica, DateTime? dataNascimento)
        {
            EmpresaId = empresaId;
            Nome = nome;
            CpfCnpj = cpfCnpj;
            UF = uf;
            RG = rg;
            Telefones = telefones ?? new List<string>();
            PessoaJuridica = pessoaJuridica;
            DataNascimento = dataNascimento;
        }
    }
}
