using HomeworkPOM.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using HomeworkPOM.Extensions;
using OpenQA.Selenium.Remote;

namespace HomeworkPOM
{
    [TestFixture]
    public class POMTests
    {

        private RemoteWebDriver _driver;
        private LoginPage _loginPage;
        private RegistrationPage _regPage;
        private RegistrationUser _user;

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


            _loginPage = new LoginPage(_driver);
            _regPage = new RegistrationPage(_driver);

            _user = UserFactoy.CreateValidUser();
        }

        [Test]

        public void FillRegistrationFormWithoutFirstName()
        {

            _user.FirstName = "";

            _regPage.Navigate(_loginPage);
            _regPage.FillForm(_user);


            _regPage.AssertErrorMessage("firstname is required.");

        }

        [Test]

        public void FillRegistrationFormWithoutLastName()
        {
            _user.LastName = "";

            _regPage.Navigate(_loginPage);
            _regPage.FillForm(_user);

            _regPage.AssertErrorMessage("lastname is required.");

        }

        [Test]

        public void FillRegistrationFormWithoutPostCode()
        {
            _user.PostCode = "127638";

            _regPage.Navigate(_loginPage);
            _regPage.FillForm(_user);

            _regPage.AssertErrorMessage("The Zip/Postal code you've entered is invalid. It must follow this format: 00000");

        }

        [Test]

        public void FillRegistrationFormWithoutCityAndPassword()
        {
            _user.Password = "";
            _user.City = "";

            _regPage.Navigate(_loginPage);
            _regPage.FillForm(_user);

            _regPage.AssertErrorMessages("passwd is required.\r\ncity is required.");

        }

        [Test]

        public void FillRegistrationFormWithoutPhone()
        {
            _user.Phone = "";

            _regPage.Navigate(_loginPage);
            _regPage.FillForm(_user);

            _regPage.AssertErrorMessage("You must register at least one phone number.");

        }



        [TearDown]
        public void ClosePage()
        {
            _driver.Quit();
        }
    }
}
