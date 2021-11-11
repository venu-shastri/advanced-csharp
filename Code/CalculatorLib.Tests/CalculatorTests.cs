using System;

namespace CalculatorLib.Tests
{
    //extending metadata 
    [PicTestLib.TestSuite("Calculator Tests")]
    public class CalculatorTests
    {
        [PicTestLib.Test(Name ="Add Test")]
        public void AssertAddTrue()
        {
            int a = 10;
            int b = 20;
            int c = a + b;
            //Assert.Equals(c,30);
        }
        [PicTestLib.TestCleanup]
        public void Dispose()
        {

        }
        public void Util() { }
    }
}
