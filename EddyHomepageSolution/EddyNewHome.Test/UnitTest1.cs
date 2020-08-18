using EddyNewHome.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EddyNewHome.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CommonGetIP()
        {
            // 210.119.12.57
            string expectedIP = "210.119.12.57";    // 중단점 걸어두고 테스트디버그로 값이 몇 번째 배열에 들어있는지 타고 들어가봐야 함!

            string actualIP = Commons.GetIpAddress();
            Assert.AreEqual(expectedIP, actualIP);
        }
    }
}
