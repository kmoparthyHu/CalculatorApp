using Microsoft.VisualStudio.TestTools.UnitTesting;
using calculatorapp.services;
namespace CalculatorApp.Services.Tests
{
    [TestClass]
    public class CalculatorTest
    {

        Calculator _calc;

        public CalculatorTest()
        {
            _calc = new Calculator();
        }

	[TestMethod]
       public void shouldaddtwonumbers()
       {
           int res = _calc.Add(5, 3);
           Assert.AreEqual(res, 8);
       }
	[TestMethod]
    	public void shouldsubstracttwonumbers()
    	{
        	int res = _calc.Sub(5, 3);
        	Assert.AreEqual(res, 2);
    	}
    }
}