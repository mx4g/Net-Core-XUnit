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
            //Arrange ������һЩ�Ⱦ����趨�����紴������ʵ�������ݵ�
            var sut = new Calculator(); //sut - System Under

            //Act ����ִ���������벢���ؽ����������÷���������������(Properties)
            var result = sut.Add(1, 2);

            //Assert ��������������ͨ������ʧ�ܡ�
            Assert.Equal(3, result);

        }

        //�������ִ�ж������
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
        /// ���ʵ�ַ�ʽ��ShouldAddByTheory���һ��
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
