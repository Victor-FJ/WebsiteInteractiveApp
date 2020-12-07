using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


namespace WebsiteInteractiveApp
{
    public class Selenium 
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
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

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

                _driver.Navigate().GoToUrl(url + "3/ri1Q7.html");



                DateTime now = DateTime.Now;
                int day = ((int) DateTime.Now.DayOfWeek == 0) ? 7 : (int) DateTime.Now.DayOfWeek;
                Console.WriteLine(day);

                bool x = true;
                int index = 0;
                bool beyondInfo = false;
                string extralist = "nextPage_o";
                int listextendednumber = 15;
                int classesmeeting = 0;
                int failcheck = 0;
                int allread = 0;

                while (allread < 3)
                {
                    js.ExecuteScript("window.scrollTo(0, 0)");
                    IWebElement searchName = _driver.FindElement(By.ClassName("ffinput"));
                    Actions actionfindsearchname = new Actions(_driver);
                    actionfindsearchname.MoveToElement(searchName);
                    actionfindsearchname.Perform();
                    searchName.SendKeys(Keys.Backspace);
                    searchName.SendKeys(Keys.Backspace);
                    searchName.SendKeys(Keys.Backspace);
                    searchName.SendKeys(Keys.Backspace);
                    if (allread == 0)
                    {
                        searchName.SendKeys("rf20");
                    }

                    if (allread == 1)
                    {
                        searchName.SendKeys("re");
                    }

                    if (allread == 2)
                    {
                        Console.WriteLine("IM DONE IM DONE IM DONE");
                        Console.WriteLine("Så mange klasser skal møde i skole:"+ classesmeeting);
                        break;
                    }

                    IWebElement searchButton20 = _driver.FindElement(By.XPath("//*[@id=\"searchDivContent2\"]/div[2]/div[1]/h2[2]/div/input[2]"));
                    Actions actionfindsearch30 = new Actions(_driver);
                    actionfindsearch30.MoveToElement(searchButton20);
                    actionfindsearch30.Perform();
                    js.ExecuteScript("window.scrollTo(0, 0)");
                    searchButton20.Click();

                    for (int i = 0; i < 25; i++)
                    {
                        try
                        {
                            IWebElement viewButton = _driver.FindElement(By.ClassName("loadNextPage"));
                            viewButton.Click();
                        }
                        catch (Exception e)
                        {

                        }


                    }



                    if (beyondInfo == false)
                    {

                        IWebElement classname = _driver.FindElement(By.XPath($"//*[@id=\"info{index}\"]/div[1]"));
                        Actions actions = new Actions(_driver);
                        actions.MoveToElement(classname);
                        actions.Perform();

                        index++;
                        if (index > 14)
                        {
                            beyondInfo = true;
                            index = 1;
                            
                        }

                        else
                        {
                            classname.Click();

                            IWebElement show = _driver.FindElement(By.Id("objectbasketgo"));
                            Actions actions1 = new Actions(_driver);
                            actions1.MoveToElement(show);
                            actions1.Perform();
                            show.Click();

                            try
                            {
                                IWebElement meetinghour = _driver.FindElement(By.XPath($"//*[@id=\"contents\"]/div[1]/div[{day}]/div[3]/div[2]/div[1]"));
                                if (meetinghour.Text != null)
                                {
                                    classesmeeting++;
                                    Console.WriteLine("så mange klasser skal møde "+ classesmeeting);
                                }
                                else Console.WriteLine("nothing here");
                            }
                            catch (Exception e)
                            {
                                
                            }

                            
                        }
                        _driver.Navigate().GoToUrl(url + "3/ri1Q7.html");

                    }
                    else
                    {
                        try
                        {
                            IWebElement classname2 = _driver.FindElement(By.XPath(
                                $"//*[@id=\"{extralist}{listextendednumber}\"]/div[{index}]/table/tbody/tr/td/div/div[1]"));
                            Actions actions4 = new Actions(_driver);
                            actions4.MoveToElement(classname2);
                            actions4.Perform();
                            classname2.Click();
                            index++;
                            Console.WriteLine($"listextendednumber " + listextendednumber);
                            Console.WriteLine($"Index " + (index-1));
                            Console.WriteLine("så mange klasser skal møde " + classesmeeting);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("failed to find class element");
                        }
                        
                        if (index > 15)
                        {
                            index = 1;
                            listextendednumber = listextendednumber + 15;
                            Console.WriteLine(listextendednumber);
                        }
                        else
                        {
                            try
                            {
                                IWebElement classname2 = _driver.FindElement(By.XPath(
                                    $"//*[@id=\"{extralist}{listextendednumber}\"]/div[{index}]/table/tbody/tr/td/div/div[1]"));
                               

                                IWebElement show = _driver.FindElement(By.Id("objectbasketgo"));
                                Actions actions1 = new Actions(_driver);
                                actions1.MoveToElement(show);
                                actions1.Perform();
                                js.ExecuteScript("window.scrollTo(0, 0)");
                                show.Click();
                                
                               

                                try
                                {
                                    IWebElement meetinghour = _driver.FindElement(By.XPath($"//*[@id=\"contents\"]/div[1]/div[{day}]/div[3]/div[2]/div[1]"));
                                    if (meetinghour.Text != null)
                                    {
                                        classesmeeting++;
                                        Console.WriteLine(classesmeeting);
                                    }
                                    else Console.WriteLine("nothing here");
                                }
                                catch (Exception e)
                                {

                                }
                                _driver.Navigate().GoToUrl(url + "3/ri1Q7.html");
                            }
                       
                         
                            catch (Exception e)
                            {
                                failcheck++;
                                if (failcheck > 100)
                                {
                                    allread++;
                                    Console.WriteLine("ALL DONE WITH RF");
                                    beyondInfo = false;
                                    index = 0;
                                    failcheck = 0;
                                }

                                
                            }
                        }

                    }

                }



            }

            return true;
        }

    }
}
