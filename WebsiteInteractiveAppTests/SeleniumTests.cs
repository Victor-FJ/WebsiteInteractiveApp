using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebsiteInteractiveApp;
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebsiteInteractiveApp.Tests
{
    [TestClass()]
    public class SeleniumTests
    {

        private static readonly string DriverDirectory = @"C:\Users\trist\Documents\skole\selenium";
        private static IWebDriver _driver;
        FileManager manager = new FileManager("LogInFile.txt");
        const string url = "https://cloud.timeedit.net/zealand/web/";
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

        [TestMethod()]


        public void TestURL()
        {
            //Arrange

            Selenium driver = new Selenium();

            //Act

            driver._driver.Navigate().GoToUrl(url);

            //Assert

            Assert.AreEqual("https://cloud.timeedit.net/zealand/web/", driver._driver.Url);



        }

        [TestMethod()]
        public void AmILoggedIn()
        {
            //Arrange

            Selenium driver = new Selenium();

            //Act

            driver.login();

            //Assert

            Assert.AreEqual("https://cloud.timeedit.net/zealand/web/3/ri1Q7.html", driver._driver.Url);

        }
    }
}