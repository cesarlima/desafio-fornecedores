using System;
using System.Collections.Generic;
using Domain.Fornecedores.ValueObject;

namespace Domain.Fornecedores
{
    public abstract class Pessoa
    {
        public Guid Id { get; protected set; }
        public string Nome { get; protected set; }
        public DateTime DataCadastro { get; protected set; }
        private readonly List<Telefone> _telefones;
        public IReadOnlyCollection<Telefone> Telefones => _telefones;
        public PessoaTipo PessoaTipo { get; protected set; }

        protected Pessoa() {
            _telefones = new List<Telefone>();
        }

        public Pessoa(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            DataCadastro = DateTime.Now;
            _telefones = new List<Telefone>();
        }
    }
}

//https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implemenation-entity-framework-core

    //sobre contexto async await
//http://www.estacouveflor.com/async-e-await/