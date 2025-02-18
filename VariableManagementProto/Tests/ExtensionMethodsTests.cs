using Core.ExtensionMethods;
using Core.Models;

namespace Tests
{
    public class Tests
    {
        Variable intVar;
        Variable floatVar;
        Variable stringVar;
        Variable boolVar;
        TypeDefinition intType = new TypeDefinition(1, "int");
        TypeDefinition floatType = new TypeDefinition(2, "float");
        TypeDefinition stringType = new TypeDefinition(3, "string");
        TypeDefinition boolType = new TypeDefinition(4, "bool");
        [SetUp]
        public void Setup()
        {
            intVar = new Variable(1, "intVar", 1, intType, "0", "0");
            floatVar = new Variable(2, "floatVar", 1, floatType, "0.0", "0.0");
            stringVar = new Variable(3, "stringVar", 1, stringType, "", "");
            boolVar = new Variable(4, "boolVar", 1, boolType, "false", "false");
        }

        [Test]
        public void ConvertInt()
        {
            Assert.Equals((int)intVar.Convert(), 10);
        }
    }
}
