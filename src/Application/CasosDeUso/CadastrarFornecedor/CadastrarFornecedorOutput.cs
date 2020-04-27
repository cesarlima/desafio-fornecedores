using System;
using System.Collections.Generic;

namespace Application.CasosDeUso.CadastrarFornecedor
{
    public sealed class CadastrarFornecedorOutput
    {
        public Guid Id { get; }
        public string Nome { get; }
        public string CpfCnpj { get; }
        public DateTime DataCadastro { get; }

        public CadastrarFornecedorOutput(Guid id, string nome, string cpfCnpj, DateTime dataCadastro)
        {
            Id = id;
            Nome = nome;
            CpfCnpj = cpfCnpj;
            DataCadastro = dataCadastro;
        }

        public override bool Equals(object obj)
        {
            return obj is CadastrarFornecedorOutput output &&
                   Id.Equals(output.Id) &&
                   Nome == output.Nome &&
                   CpfCnpj == output.CpfCnpj &&
                   DataCadastro == output.DataCadastro;
        }

        public override int GetHashCode()
        {
            int hashCode = -778733419;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nome);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CpfCnpj);
            hashCode = hashCode * -1521134295 + DataCadastro.GetHashCode();
            return hashCode;
        }
    }
}
