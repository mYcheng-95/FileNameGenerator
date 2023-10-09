using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GeneratorTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           Console.WriteLine( FileNameGenerator.Generate());
           Console.WriteLine( FileNameGenerator.Generate(template: $"{{dateTime:yyyyMMddHHmmss}}.pb"));
        }
    }
}
