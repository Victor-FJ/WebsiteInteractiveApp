using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebsiteInteractiveApp
{
    public class Selenium : IDisposable
    {
        private static readonly string DriverDirectory = @"C:\Users\victo\Documents\Zeeland\Div\Selenium";
        private static IWebDriver _driver;

        public Selenium()
        {
            //Service
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(DriverDirectory);
            service.SuppressInitialDiagnosticInformation = true;
            service.HideCommandPromptWindow = true;
            //Options
            ChromeOptions options = new ChromeOptions();
            //options.AddArguments("--disable-extensions");
            //options.AddArgument("test-type");
            //options.AddArgument("--ignore-certificate-errors");
            //options.AddArgument("no-sandbox");
            
            //options.AddArgument("--headless");//hide browser
            
            //Driver
            _driver = new ChromeDriver(service, options); // fast
            // if your Chrome browser was updated, you must update the driver as well ...
            //    https://chromedriver.chromium.org/downloads
            //_driver = new FirefoxDriver(DriverDirectory);  // slow
        }

        public bool IsClassInSchool()
        {
            FileManager manager = new FileManager("LogInFile.txt");
            const string url = "https://ums.easj.dk/WebTimeTable/default.aspx";

            if (_driver.Title != "Skema Web")
            {
                _driver.Navigate().GoToUrl(url);

                if (_driver.Title != "Log på")
                    throw new NotSupportedException("Unknown Website");
                IWebElement userNameElement = _driver.FindElement(By.Id("userNameInput"));
                userNameElement.Clear();
                userNameElement.SendKeys(manager.UserName);
                IWebElement passwordElement = _driver.FindElement(By.Id("passwordInput"));
                passwordElement.Clear();
                passwordElement.SendKeys(manager.Password);

                IWebElement submitButton = _driver.FindElement(By.Id("submitButton"));
                submitButton.Click();
            }

            IWebElement listElement = _driver.FindElement(By.Id("day0Col"));

            var elements = listElement.FindElements(By.TagName("div"));

            if (elements.Count < 48)
                throw new NotSupportedException("Unknown number of elements");

            return elements.Count != 48;
        }

        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}
