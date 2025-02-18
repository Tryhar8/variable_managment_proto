using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Repositories
{
    public interface ITypeDefinitionRepository
    {
        Task<TypeDefinition> GetByIdAsync(int id);
        Task AddAsync(TypeDefinition type);
        Task UpdateAsync(TypeDefinition type);
        Task DeleteAsync(int id);
    }
}
