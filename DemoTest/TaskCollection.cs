using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DemoTest
{
    /// <summary>
    /// 这个可以实现用标记来共享上下文
    /// </summary>
    [CollectionDefinition("Long Time Task Collection")]
    public class TaskCollection:ICollectionFixture<LongTimeTaskFixture>
    {
    }
}
