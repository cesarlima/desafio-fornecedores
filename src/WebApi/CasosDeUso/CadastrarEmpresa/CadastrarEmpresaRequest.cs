using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.CasosDeUso.CadastrarEmpresa
{
    public class CadastrarEmpresaRequest
    {
        [Required]
        [MaxLength(2)]
        public string UF { get; set; }
        [Required]
        [MaxLength(60)]
        public string NomeFantasia { get; set; }
        [Required]
        public string CNPJ { get; set; }
    }
}
