using System;
namespace Application.CasosDeUso.ListarEmpresas
{
    public sealed class Empresa
    {
        public Guid Id { get; }
        public string NomeFantasia { get; }

        public Empresa(Guid id, string nomeFantasia)
        {
            Id = id;
            NomeFantasia = nomeFantasia;
        }
    }
}
