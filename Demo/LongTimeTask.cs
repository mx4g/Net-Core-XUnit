using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Demo
{
    /// <summary>
    /// 共享上下文测试(加入这个类加载很久)
    /// </summary>
    public class LongTimeTask
    {
        public LongTimeTask() {
            Thread.Sleep(2000);
        }
    }
}
