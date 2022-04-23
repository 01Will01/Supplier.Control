
using Supplier.Control.Domain.Extensions;
using System;
using Xunit;


namespace Supplier.Control.Test.Extensions
{
    public class ManagerStringTest
    {

        [Fact]
        public void StandardizeSeparator_Test()
        {
            Assert.Equal("Test Test", ManagerString.StandardizeSeparator("Test_Test", " "));
            Assert.Throws<ArgumentNullException>(()=> ManagerString.StandardizeSeparator(null, " "));
            Assert.Throws<ArgumentNullException>(()=> ManagerString.StandardizeSeparator("Test", null));
            Assert.Equal("Test", ManagerString.StandardizeSeparator("Test", "_"));
            Assert.Equal("Oi tudo bem", ManagerString.StandardizeSeparator("Oi-tudo-bem", " "));
        }
        
        [Fact]
        public void PatterCPF_Test()
        {
            Assert.Equal("000.000.000-00", ManagerString.PatterCPF("00000000000"));
            Assert.Equal("0000000000", ManagerString.PatterCPF("0000000000"));
            Assert.Throws<ArgumentNullException>(() => ManagerString.PatterCPF(null));
        }

        [Fact]
        public void PatterCNPJ_Test()
        {
            Assert.Equal("00.000.000.0000-00", ManagerString.PatterCNPJ("00000000000000"));
            Assert.Equal("0000000000000", ManagerString.PatterCNPJ("0000000000000"));
            Assert.Throws<ArgumentNullException>(() => ManagerString.PatterCNPJ(null));
        }

    }
}
