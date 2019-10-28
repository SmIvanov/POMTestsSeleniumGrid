using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;

namespace HomeworkPOM1.GoogleSearch
{
    [TestFixture]
    public partial class GoogleSearchTest
    {
        private RemoteWebDriver _driver;
        private GoogleSearchPage _googleSearchPage;
        private GoogleResultsPage _googleResultsPage;

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


            _googleSearchPage = new GoogleSearchPage(_driver);
            _googleResultsPage = new GoogleResultsPage(_driver);

        }

        [Test]
        public void SeleniumSearch()
        {
            _googleResultsPage.Navigate(_googleSearchPage);
            var foundResult = _driver.Title;

        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}