using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TypeDefinition(int id, string name)
    {
        public int Id { get; init; } = id;
        public string Name { get; init; } = name;
    }
}
