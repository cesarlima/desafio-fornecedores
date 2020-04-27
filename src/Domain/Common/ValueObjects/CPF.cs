using System;
using System.Collections.Generic;

namespace Domain.Common.ValueObjects
{
    public struct CPF : IEquatable<CPF>
    {
        private readonly string _numero;
        public bool Valido { get; }

        public CPF(string numero) : this()
        {
            _numero = numero ?? throw new ArgumentNullException(nameof(numero));
            _numero = _numero.Trim();
            _numero = _numero.Replace(".", "").Replace("-", "");

            Valido = false;
            Valido = ValidarCpf(_numero);
        }

        public override bool Equals(object obj)
        {
            return obj is CPF cPF && Equals(cPF);
        }

        public bool Equals(CPF other)
        {
            return _numero == other._numero;
        }

        public override int GetHashCode()
        {
            return 1030163562 + EqualityComparer<string>.Default.GetHashCode(_numero);
        }

        public static bool operator ==(CPF left, CPF right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CPF left, CPF right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return _numero;
        }

        private bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
           
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
