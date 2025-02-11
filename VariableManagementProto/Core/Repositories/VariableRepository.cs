using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class VariableRepository(VariableDbContext context) : IVariableRepository
    {
        private readonly VariableDbContext _context = context ?? throw new ArgumentNullException(nameof (VariableDbContext));

        public async Task AddAsync(Variable variable)
        {
            _context.Variables.Add(variable);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Variable>> GetAllAsync() =>
            await _context.Variables.Include(v => v.Type).ToListAsync();

        public async Task<Variable?> GetByIdAsync(int id) =>
            await _context.Variables.Include(v => v.Type).FirstOrDefaultAsync(v => v.Id == id);

        public async Task<Variable?> GetByIdentifier(string identifier) => await _context.Variables.FirstOrDefaultAsync(v => v.Identifier == identifier);

        public async Task UpdateAsync(Variable variable)
        {
            _context.Variables.Update(variable);
            await _context.SaveChangesAsync();
        }
    }
}
