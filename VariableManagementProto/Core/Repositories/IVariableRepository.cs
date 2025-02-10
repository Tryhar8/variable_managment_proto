using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Repositories
{
    public interface IVariableRepository
    {
        Task<Variable?> GetByIdAsync(int id);
        Task<IEnumerable<Variable>> GetAllAsync();
        Task AddAsync(Variable variable);
        Task UpdateAsync(Variable variable);
        Task DeleteAsync(int id);
    }
}
