using Demo;
using DemoTest.Data;
using System;
using Xunit;

namespace DemoTest
{
    public class CalculatorTests
    {
        [Fact]
        public void ShouldAdd()
        {
            //Arrange 这里做一些先决的设定。列如创建对象实例、数据等
            var sut = new Calculator(); //sut - System Under

            //Act 这里执行生产代码并返回结果。列如调用方法或者设置属性(Properties)
            var result = sut.Add(1, 2);

            //Assert 这里检查结果。测试通过或者失败。
            Assert.Equal(3, result);

        }

        //这个可以执行多个测试
        [Theory]
        [InlineData(1,2,3)]
        [InlineData(2, 2, 4)]
        [InlineData(5, 5, 10)]
        public void ShouldAddByTheory(int x, int y, int expected)
        {
            var sut = new Calculator();
            var result = sut.Add(x, y);
            Assert.Equal(expected, result);

        }

        /// <summary>
        /// 这个实现方式跟ShouldAddByTheory结果一样
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="expected"></param>
        [Theory]
        [MemberData(nameof(CalculatorTestData.TestData),MemberType =typeof(CalculatorTestData))]
        public void ShouldAddByTheory2(int x, int y, int expected)
        {
            var sut = new Calculator();
            var result = sut.Add(x, y);
            Assert.Equal(expected, result);

        }

        [Theory]
        [MemberData(nameof(CalculatorCvsData.TestData), MemberType = typeof(CalculatorCvsData))]
        public void ShouldAddByTheory3(int x, int y, int expected)
        {
            var sut = new Calculator();
            var result = sut.Add(x, y);
            Assert.Equal(expected, result);

        }

        [Theory]
        [CalculatorDataAttribute]
        public void ShouldAddByTheory4(int x, int y, int expected)
        {
            var sut = new Calculator();
            var result = sut.Add(x, y);
            Assert.Equal(expected, result);

        }
    }
}
