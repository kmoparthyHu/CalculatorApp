using System;

namespace calculatorapp.services
{
    public class Calculator
    {

        public int Add(int num1, int num2)
       {

           int result = num1 + num2;
           return result;
       }

        public int Sub(int num1, int num2)
	{
    		int result = num1 - num2;
    		return result;

	}
    }
}