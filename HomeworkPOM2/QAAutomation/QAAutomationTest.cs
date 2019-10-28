using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;

namespace HomeworkPOM2.QAAutomation
{
    [TestFixture]
    public class QAAutomationTest
    {
        private RemoteWebDriver _driver;
        private SoftUniPage _sUpage;
        private QACoursePage _qACoursePage;

        [SetUp]
        public void ClassInit()
        {
            ChromeOptions options = new ChromeOptions();

            options.PlatformName = "windows";
            options.BrowserVersion = "77.0";

            _driver = new RemoteWebDriver(new Uri("http://192.168.1.103:10608/wd/hub"), options
                .ToCapabilities(), TimeSpan.FromSeconds(30));
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            
            _driver.Manage().Window.Maximize();


            _sUpage = new SoftUniPage(_driver);
            _qACoursePage = new QACoursePage(_driver);
        }

        [Test]
        public void QAAutomationCourse()
        {
            _sUpage.Navigate();

            Assert.AreEqual(_qACoursePage.Result(), "QA Automation - септември 2019");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}