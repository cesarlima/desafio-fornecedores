
using System;
using System.Threading.Tasks;
using Application.Services;
using Infra.Contexts;

namespace Infra.Transaction
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FornecedoresContext _context;

        public UnitOfWork(FornecedoresContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Commit()
        {
            await this._context.SaveChangesAsync();
        }
    }
}
