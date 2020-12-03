using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace WebsiteInteractiveApp
{
    public class Selenium : IDisposable
    {
        private static readonly string DriverDirectory = @"C:\Users\trist\Documents\skole\selenium";
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
            const string url = "https://cloud.timeedit.net/zealand/web/";

            if (_driver.Title != "Skema Web")
            {
                _driver.Navigate().GoToUrl(url);

                var items = _driver.FindElements(By.ClassName("items"));

                IWebElement enterButton = items[2];
                enterButton.Click();

                IWebElement userNameElement = _driver.FindElement(By.Id("userNameInput"));
                userNameElement.Clear();
                userNameElement.SendKeys(manager.UserName);

                IWebElement nextButton = _driver.FindElement(By.Id("nextButton"));
                nextButton.Click();


                IWebElement passwordElement = _driver.FindElement(By.Id("passwordInput"));
                passwordElement.Clear();
                passwordElement.SendKeys(manager.Password);

                IWebElement submitButton = _driver.FindElement(By.Id("submitButton"));
                submitButton.Click();

                _driver.Navigate().GoToUrl(url+"3/ri1Q7.html");

                IWebElement searchName = _driver.FindElement(By.Id("ffsearchname"));
                searchName.SendKeys("re");

                Thread.Sleep(500);

                bool x = true;
                int index = 0;
                while (x = true)
                {

                    for (int i = 0; i < 20; i++)
                    {
                        try
                        {
                            IWebElement viewButton = _driver.FindElement(By.ClassName("loadNextPage"));
                            viewButton.Click();
                        }
                        catch (Exception e)
                        {
                            break;
                        }

                    }



                }




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
