using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Common.ValueObjects;
using Domain.Empresas;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class EmpresaRepositorio : IEmpresaRepositorio
    {
        private readonly FornecedoresContext _context;

        public EmpresaRepositorio(FornecedoresContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> EmpresaJaCadastrada(CNPJ cnpj)
        {
            return _context.Empresas.AnyAsync(x => x.CNPJ.Equals(cnpj));
        }

        public async Task<Empresa> ObterEmpresa(Guid id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            return empresa;
        }

        public async Task<IEnumerable<Empresa>> ObterEmpresas()
        {
            var empresas = await _context.Empresas.AsNoTracking().ToListAsync();
            return empresas;
        }

        public async Task Save(Empresa empresa)
        {
            await _context.AddAsync(empresa);
        }
    }
}
