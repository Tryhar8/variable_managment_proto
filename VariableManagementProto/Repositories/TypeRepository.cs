using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class TypeDefinitionRepository(VariableDbContext context) : ITypeDefinitionRepository
    {
        private readonly VariableDbContext _context = context ?? throw new ArgumentNullException(nameof (VariableDbContext));

        public async Task AddAsync(TypeDefinition type)
        {
            _context.Types.Add(type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TypeDefinition?> GetByIdAsync(int id)
        {
            return await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(TypeDefinition type)
        {
            if (await GetByIdAsync(type.Id) is null) return; // @todo rethink return value

            _context.Types.Update(type);
            await _context.SaveChangesAsync();
        }
    }
}
