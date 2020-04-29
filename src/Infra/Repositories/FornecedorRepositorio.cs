using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Fornecedores;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class FornecedorRepositorio : IFornecedorRepositorio
    {
        private readonly FornecedoresContext _context;

        public FornecedorRepositorio(FornecedoresContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Salvar(Fornecedor fornecedor)
        {
            await _context.Fornecedors.AddAsync(fornecedor);
        }

        public async Task<IEnumerable<Fornecedor>> ObterFornecedores(string nome, string cpfCnpj, DateTime? dataCadastro)
        {
            var fornecedores = await (from forn in _context.Set<Fornecedor>()
                                from pf in _context.Set<PessoaFisica>().Where(p => p.Id == forn.Pessoa.Id).DefaultIfEmpty()
                                from pj in _context.Set<PessoaJuridica>().Where(p => p.Id == forn.Pessoa.Id).DefaultIfEmpty()
                                where
                                     (nome == null || (pf.Nome == nome || pj.Nome == nome)) &&
                                     (cpfCnpj == null || (pf.CPF.ToString() == cpfCnpj || pj.CNPJ.ToString() == cpfCnpj)) &&
                                     (dataCadastro == null || (pf.DataCadastro.Date == dataCadastro.Value.Date
                                                           || pj.DataCadastro.Date == dataCadastro.Value.Date))
                                select forn)
                                .Include(x => x.Pessoa)
                                .ToListAsync();

            return fornecedores;
        }
    }
}
