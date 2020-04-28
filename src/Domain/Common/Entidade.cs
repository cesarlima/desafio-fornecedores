using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Common
{
    public abstract class Entidade 
    {
        public Guid Id { get; protected set; }
        private List<string> _notificacoes = new List<string>();
        public IReadOnlyCollection<string> Notificacoes => _notificacoes;

        public bool Invalido => _notificacoes.Any();
        public bool Valido => !Invalido;

        protected Entidade()
        {
            Id = Guid.NewGuid();
        }

        protected void AdicionarNotificacao(string mensagem)
        {
            _notificacoes.Add(mensagem);
        }

        protected void AdicionarNotificacoes(List<string> notificacoes)
        {
            _notificacoes.AddRange(notificacoes);
        }

        public bool Equals(Entidade other)
        {
            return other != null &&
                   Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
