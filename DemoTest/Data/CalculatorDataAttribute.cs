using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace DemoTest.Data
{
    /// <summary>
    /// 自定义DataAttribute
    /// </summary>
    public class CalculatorDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { 1,2,3};
            yield return new object[] { 2, 2, 4 };
            yield return new object[] { 5, 5, 10 };
        }
    }
}
