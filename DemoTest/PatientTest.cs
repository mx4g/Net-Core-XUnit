using Demo;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace DemoTest
{
    //共享上下文的方式（二）
    [Collection("Long Time Task Collection")]
    public class PatientTest :IClassFixture<LongTimeTaskFixture> , IDisposable
    {
        //自定义输出
        private readonly ITestOutputHelper _output;
        private readonly Patient _patient;
        private readonly LongTimeTask _task;

        public PatientTest(ITestOutputHelper output, LongTimeTaskFixture fixture) {
            _output = output;
            _patient = new Patient();
            //这个类加载时间过长，下面每个方法实现时都会走一次构造函数，导致每个方法延迟了2秒，怎么决绝呢？
            //用IClassFixture<>,这个非常有用,这里提供了两种方式：注入的方式，和标记的方式
            //_task = new LongTimeTask(); //这样会导致重复加载 
            /// 解决上下文类重复加载，导致每个方法都执行了2秒的问题
            /// IClassFixture这个可以共享上下文，决绝每个方法都执行2秒
            /// 共享上下文的方式（一）
            _task = fixture.Task;
        }

        [Fact]
        [Trait("Category","New")] //可以分组
        public void ShouldNewPatient()
        {
            //自定义输出
            _output.WriteLine("第一个测试！");

            //Arrange 
            var sut = _patient;

            //Act 
            var result = sut.isNew;

            //Assert  
            Assert.True(result);

        }

        [Fact]
        [Trait("Category", "New")]
        public void ShouldGetFullName()
        {
            //Arrange 
            var sut = new Patient()
            {
                FirstName = "Nick",
                LastName = "Carter"
            };

            //Act 
            var result = sut.FullName;

            //Assert  
            Assert.Equal("Nick Carter", result);
            Assert.StartsWith("Nick", result);
            Assert.EndsWith("Carter", result);
            Assert.Contains("Carter", result);
            Assert.NotEqual("NICK CARTER", result);
            Assert.Matches(@"^[A-Z][a-z]*\s[A-Z][a-z]*", result);

        }

        [Fact]
        [Trait("Category", "New")]
        public void ShouldGetBloodSugar()
        {
            var p = new Patient();
            var result = p.BloodSugar;

            Assert.Equal(4.9f, result);
            Assert.InRange(result, 3.9f, 6.1f);
        }

        [Fact(Skip ="忽略测试")]
        public void ShouldTestIsNullNameWhenCreated()
        {
            var p = new Patient();
            Assert.Null(p.FirstName);
            Assert.NotNull(p);
        }

        //集合的判断
        [Fact]
        public void shouldGetHistory()
        {

            var diseases = new List<string>{
                "感冒",
                "腹泻",
                "发烧"
            };

            var p = new Patient();
            p.History.Add("感冒");
            p.History.Add("腹泻");
            p.History.Add("发烧");

            Assert.Contains("感冒", p.History);
            Assert.DoesNotContain("心脏病", p.History);

            //predicate
            Assert.Contains(p.History, x => x.StartsWith('发'));
            Assert.All(p.History, x => Assert.True(x.Length >= 2));

            //判断两个集合是否相等
            Assert.Equal(diseases, p.History);

        }

        [Fact]
        public void BeAPerson()
        {
            var p = new Patient();

            var p2 = new Patient();

            Assert.IsType<Patient>(p);
            Assert.IsNotType<Person>(p);

            Assert.IsAssignableFrom<Person>(p);


            Assert.NotSame(p, p2);

        }

        //异常判断
        [Fact]
        public void ThrowExceptionsWhenErrorOccurred()
        {
            var p = new Patient();
            var ex = Assert.Throws<InvalidOperationException>(() => p.NotAllowed());

            Assert.Equal("Not able to create", ex.Message);
        }

        [Fact]
        public void RaiseSleepEvent()
        {
            var p = new Patient();

            Assert.Raises<EventArgs>(
                handler => p.PatientSlept += handler,
                handler => p.PatientSlept -= handler,
                () => p.Sleep()

             );
        }

        [Fact]
        public void RaisePropertyChangedEvent()
        {
            var p = new Patient();

            Assert.PropertyChanged(p, 
                nameof(p.HeartBeatRate),
                () => p.IncreaseHeartBeatRate()
             );
        }

        //清理一下资源
        public void Dispose()
        {
            
            _output.WriteLine("清理了资源");
        }
    }
}
