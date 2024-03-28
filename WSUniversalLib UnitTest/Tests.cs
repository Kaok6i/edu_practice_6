using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSUniversalLib;

namespace WSUniversalLib_UnitTest
{
    [TestClass]
    public class Tests
    {
        Calculation library = new Calculation();
        
        [TestMethod]
        public void GetQuantityForProduct_NonExistentProductType()
        {
            var result = library.GetQuantityForProduct(4, 1, 1, 1, 1);
            Assert.IsTrue(result == -1);
        }
        [TestMethod]
        public void GetQuantityForProduct_NonExistentMaterialType()
        {
            var result = library.GetQuantityForProduct(3, 3, 15, 20, 45);
            Assert.AreEqual(-1, result);
        }
        [TestMethod]
        public void GetQuantityForProduct_CalculationQuantityRight()
        {
            var result = library.GetQuantityForProduct(3, 1, 15, 20, 45);
            Assert.AreEqual(114148, result);
        }
        [TestMethod]
        public void GetQuantityForProduct_CalculationQuantityWrong()
        {
            var result = library.GetQuantityForProduct(3, 1, 15, 20, 45);
            Assert.AreNotEqual(-1, result);
        }
        [TestMethod]
        public void GetQuantityForProduct_LengthNegative()
        {
            var result = library.GetQuantityForProduct(3, 2, 15, 20, -1);
            Assert.AreEqual(-1, result);
        }
        [TestMethod]
        public void GetQuantityForProduct_WidthNegative()
        {
            var result = library.GetQuantityForProduct(3, 1, 15, -20, 45);
            Assert.AreEqual(-1, result);
        }
        [TestMethod]
        public void GetQuantityForProduct_CountNegative()
        {
            var result = library.GetQuantityForProduct(3, 1, -15, 20, 45);
            Assert.AreEqual(-1, result);
        }
        [TestMethod]
        public void GetQuantityForProduct_ProductTypeNegative()
        {
            var result = library.GetQuantityForProduct(-3, 1, 15, 20, 45);
            Assert.AreEqual(-1, result);
        }
        [TestMethod]
        public void GetQuantityForProduct_MaterialTypeNegative()
        {
            var result = library.GetQuantityForProduct(1, -1, 15, 20, 45);
            Assert.AreEqual(-1, result);
        }
        [TestMethod]
        public void GetQuantityForProduct_ZeroCount()
        {
            var result = library.GetQuantityForProduct(1, 1, 0, 20, 45);
            Assert.AreEqual(-1, result);
        }
    }
}
