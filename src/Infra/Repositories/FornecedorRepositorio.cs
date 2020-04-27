using System;
using System.Threading.Tasks;
using Domain.Fornecedores;
using Infra.Contexts;

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
    }
}
