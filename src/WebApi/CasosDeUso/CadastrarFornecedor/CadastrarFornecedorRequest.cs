using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.CasosDeUso.CadastrarFornecedor
{
    public sealed class CadastrarFornecedorRequest
    {
        [Required]
        public Guid EmpresaId { get; set; }
        [Required]
        [MaxLength(60)]
        public string Nome { get; set; }
        [Required]
        public string CpfCnpj { get; set; }
        public string RG { get; set; }
        [Required]
        [MaxLength(2)]
        public string UF { get; set; }
        public List<string> Telefones { get; set; }
        public bool PessoaJuridica { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
