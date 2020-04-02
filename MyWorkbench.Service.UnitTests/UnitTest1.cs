using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWorkBench.Service.Api;

namespace MyWorkbench.Service.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (EmailToTicket emails = new EmailToTicket("Data Source=.\\SQLEXPRESS01;Initial Catalog=BrothersUnitedMyWorkbench;Integrated Security=true;Connect Timeout=30;Application Name=MyWorkbench;", "outlook.office365.com",993,true, "catchall@myworkbench.co.za", "@R3b3cc@1234"))
            {
                emails.Process();
            }
        }
    }
}
