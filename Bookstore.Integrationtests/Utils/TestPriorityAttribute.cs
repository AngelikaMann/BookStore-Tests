//namespace TestUdemy_Bookstore.Integrationtests.Utils
//{
//    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
//    public class TestPriorityAttribute : Attribute
//    {
//        public int Priority { get; }

//        public TestPriorityAttribute(int priority)
//        {
//            Priority = priority;
//        }
//    }
//}

namespace Bookstore.Integrationtests.Utils;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class TestPriorityAttribute : Attribute
{
    public int Priority { get; }

    public TestPriorityAttribute(int priority)
    {
        Priority = priority;
    }

}