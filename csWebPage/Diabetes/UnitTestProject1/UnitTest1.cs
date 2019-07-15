using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelperNamespace;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAlgorithm()
        {
            int kcal = Helper.Get_kcal(50,150,5,1.2,0.99,20);

            Assert.AreEqual(kcal, 1594);
        }
    }
}
