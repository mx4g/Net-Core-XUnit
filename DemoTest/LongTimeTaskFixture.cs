using Demo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTest
{
    /// <summary>
    /// 解决上下文类重复加载，导致每个方法都执行了2秒的问题
    /// IClassFixture这个可以共享上下文，决绝每个方法都执行2秒
    /// </summary>
    public class LongTimeTaskFixture : IDisposable
    {
        public LongTimeTask Task { get; }
        public LongTimeTaskFixture() {
            Task = new LongTimeTask();
        }

        public void Dispose()
        {
             
        }
    }
}
