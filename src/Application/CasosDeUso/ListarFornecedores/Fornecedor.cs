using System;
using System.Collections.Generic;

namespace Application.CasosDeUso.ListarFornecedores
{
    public sealed class Fornecedor
    {
        public Guid Id { get; }
        public string Nome { get; }
        public string CpfCnpj { get; }
        public bool PessoaJuridica { get; }
        public IEnumerable<string> Telefones { get; }

        public Fornecedor(Guid id, string nome, string cpfCnpj, bool pessoaJuridica, IEnumerable<string> telefones)
        {
            Id = id;
            Nome = nome;
            CpfCnpj = cpfCnpj;
            PessoaJuridica = pessoaJuridica;
            Telefones = telefones;
        }
    }
}
