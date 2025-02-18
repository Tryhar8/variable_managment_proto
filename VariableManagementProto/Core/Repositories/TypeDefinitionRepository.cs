using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories
{
    public class TypeDefinitionRepository : ITypeDefinitionRepository
    {
        private readonly VariableDbContext _context;

        public TypeDefinitionRepository(VariableDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(VariableDbContext));
            InitializeAsync().Wait(); // for sake of simplicity, this will be run synchronously. It is not really best idea to call such DB operations in constructor but ...
        }

        private async Task InitializeAsync()
        {
            // Define the required TypeDefinitions
            var requiredTypeDefinitions = new List<TypeDefinition>
        {
            new TypeDefinition(1, "bool"),
            new TypeDefinition(2, "int"),
            new TypeDefinition(3, "double"),
            new TypeDefinition(4, "string")
        };

            // Check if each required TypeDefinition exists in the database
            foreach (var typeDef in requiredTypeDefinitions)
            {
                var exists = await _context.Set<TypeDefinition>()
                                           .AnyAsync(t => t.Id == typeDef.Id || t.Name == typeDef.Name);

                if (!exists)
                {
                    _context.Set<TypeDefinition>().Add(typeDef);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<TypeDefinition?> GetByIdAsync(int id)
        {
            return await _context.Types.FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<TypeDefinition> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
