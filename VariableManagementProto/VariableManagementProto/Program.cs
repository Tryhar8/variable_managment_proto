using Core.Models;
using Core.Repositories;
using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();

string? connString = config.GetConnectionString("SqLite");

ArgumentNullException.ThrowIfNull(connString, $"{nameof(connString)}. Make appsettings.Development.json and there you need to define SqLite property in ConnectionStrings.");

var options = new DbContextOptionsBuilder<VariableDbContext>()
                  .UseSqlite(connString).Options;

using var context = new VariableDbContext(options, connString);
var repository = new VariableRepository(context);
var typeDefsRepository = new TypeDefinitionRepository(context);

await repository.AddAsync(new Variable("TestVar", 2, "10"));
Console.WriteLine("Variable added!");

var getByIdentifier = await repository.GetByIdentifier("TestVar");

Console.WriteLine($"Got by identifier: {getByIdentifier?.CurrentValue}");

VariableChangeHandler handler = (string identifier, string oldValue, string newValue) =>
{
    Console.WriteLine($"Identifier: {identifier} had old value: {oldValue}, which has changed to {newValue}");
};

if (getByIdentifier is not null)
{
    getByIdentifier.VariableChange += handler;

    getByIdentifier.SetValue(20);
}