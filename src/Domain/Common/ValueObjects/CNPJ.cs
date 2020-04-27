using System;
using System.Collections.Generic;

namespace Domain.Common.ValueObjects
{
    public struct CNPJ : IEquatable<CNPJ>
    {
        private readonly string _numero;
        public bool Valido { get; }

        public CNPJ(string numero)
        {
            _numero = numero ?? throw new ArgumentNullException(nameof(numero));
            _numero = _numero.Trim();
            _numero = _numero.Replace(".", "").Replace("-", "").Replace("/", "");
            Valido = false;
            Valido = ValidarCnpj(_numero);
        }

        public override bool Equals(object obj)
        {
            return obj is CNPJ cNPJ && Equals(cNPJ);
        }

        public bool Equals(CNPJ other)
        {
            return _numero == other._numero;
        }

        public override int GetHashCode()
        {
            return 1030163562 + EqualityComparer<string>.Default.GetHashCode(_numero);
        }

        public static bool operator ==(CNPJ left, CNPJ right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CNPJ left, CNPJ right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return _numero;
        }

        private bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
           
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }
}
