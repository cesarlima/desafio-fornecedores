using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.CasosDeUso.CadastrarEmpresa
{
    public class CadastrarEmpresaRequest
    {
        [Required]
        public string UF { get; set; }
        [Required]
        public string NomeFantasia { get; set; }
        [Required]
        public string CNPJ { get; set; }
    }
}
