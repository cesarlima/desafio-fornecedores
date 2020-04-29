using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Fornecedores;
using Infra.Contexts;
using Microsoft.Data.SqlClient;
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
            await _context.Fornecedores.AddAsync(fornecedor);
        }

        public async Task<IEnumerable<Fornecedor>> ObterFornecedores(string nome, string cpfCnpj, DateTime? dataCadastro)
        {
            var parametros = new Dictionary<string, object>();

            if (string.IsNullOrEmpty(nome) == false)
                parametros.Add("nome = @nome", new SqlParameter("@nome", nome));

            if (string.IsNullOrEmpty(cpfCnpj) == false)
                parametros.Add("(p.cpf = @cpfCnpj OR p.cnpj = @cpfCnpj)", new SqlParameter("@cpfCnpj", cpfCnpj));

            if (dataCadastro != null)
                parametros.Add("CONVERT(date, dataCadastro) = @dataCadastro", new SqlParameter("@dataCadastro", dataCadastro.Value.Date));

            var clausulaWhere = parametros.Any() ? $" WHERE {string.Join(" AND ", parametros.Keys.ToList())}"  : "";

            var slquery = @$"SELECT DISTINCT forn.id,
                                    p.id AS pessoaId,
                                    e.id AS empresaId
                            FROM Fornecedor forn
                            INNER JOIN Pessoa p ON p.id = forn.pessoaId
                            INNER JOIN Empresa e ON e.id = forn.empresaId
                            {clausulaWhere} ";

            var result = _context.Fornecedores
                .FromSqlRaw(slquery, parametros.Values.ToArray())
                .Include(f => f.Pessoa);

            //var sql = result.ToSql();
            var fornecedores = await result.ToListAsync();
            return fornecedores;
        }
    }
}


