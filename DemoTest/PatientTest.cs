using Demo;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace DemoTest
{
    //���������ĵķ�ʽ������
    [Collection("Long Time Task Collection")]
    public class PatientTest :IClassFixture<LongTimeTaskFixture> , IDisposable
    {
        //�Զ������
        private readonly ITestOutputHelper _output;
        private readonly Patient _patient;
        private readonly LongTimeTask _task;

        public PatientTest(ITestOutputHelper output, LongTimeTaskFixture fixture) {
            _output = output;
            _patient = new Patient();
            //��������ʱ�����������ÿ������ʵ��ʱ������һ�ι��캯��������ÿ�������ӳ���2�룬��ô�����أ�
            //��IClassFixture<>,����ǳ�����,�����ṩ�����ַ�ʽ��ע��ķ�ʽ���ͱ�ǵķ�ʽ
            //_task = new LongTimeTask(); //�����ᵼ���ظ����� 
            /// ������������ظ����أ�����ÿ��������ִ����2�������
            /// IClassFixture������Թ��������ģ�����ÿ��������ִ��2��
            /// ���������ĵķ�ʽ��һ��
            _task = fixture.Task;
        }

        [Fact]
        [Trait("Category","New")] //���Է���
        public void ShouldNewPatient()
        {
            //�Զ������
            _output.WriteLine("��һ�����ԣ�");

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

        [Fact(Skip ="���Բ���")]
        public void ShouldTestIsNullNameWhenCreated()
        {
            var p = new Patient();
            Assert.Null(p.FirstName);
            Assert.NotNull(p);
        }

        //���ϵ��ж�
        [Fact]
        public void shouldGetHistory()
        {

            var diseases = new List<string>{
                "��ð",
                "��к",
                "����"
            };

            var p = new Patient();
            p.History.Add("��ð");
            p.History.Add("��к");
            p.History.Add("����");

            Assert.Contains("��ð", p.History);
            Assert.DoesNotContain("���ಡ", p.History);

            //predicate
            Assert.Contains(p.History, x => x.StartsWith('��'));
            Assert.All(p.History, x => Assert.True(x.Length >= 2));

            //�ж����������Ƿ����
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

        //�쳣�ж�
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

        //����һ����Դ
        public void Dispose()
        {
            
            _output.WriteLine("��������Դ");
        }
    }
}
