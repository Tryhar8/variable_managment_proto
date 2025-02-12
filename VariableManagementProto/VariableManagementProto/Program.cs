using Core.Models;
using Core.Repositories;
using Core;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<VariableDbContext>()
                  .UseSqlite("Data Source=../../db/test.db").Options;

using var context = new VariableDbContext(options);
var repository = new VariableRepository(context);

var variable = new Variable
{
    Identifier = "TestVar",
    Type = new TypeDefinition() { Id = 1, Name = "int"},
    DefaultValue = "10",
    CurrentValue = "10"
};

await repository.AddAsync(variable);
Console.WriteLine("Variable added!");

var retrievedVar = await repository.GetByIdAsync(variable.Id);
Console.WriteLine($"Retrieved: {retrievedVar?.Identifier} - {retrievedVar?.CurrentValue}");
