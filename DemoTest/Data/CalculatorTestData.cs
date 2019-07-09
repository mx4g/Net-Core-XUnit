using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTest.Data
{
    public class CalculatorTestData
    {
        private static readonly List<object[]> Data = new List<object[]>()
        {
            new object[]{ 1,2,3},
            new object[]{ 2,2,4},
            new object[]{ 5,5,10}
        };

        public static IEnumerable<object[]> TestData => Data;
    }
}
