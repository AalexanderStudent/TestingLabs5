using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using System.Linq;

namespace SeleniumTestChrome
{
    public class Tests
    {
        public static string chromeDriverPath = Environment.CurrentDirectory;
        public static int sleepTime = 5000;
        private IWebDriver _driver;

        [OneTimeSetUp]
        public void Setup() => _driver = new ChromeDriver(chromeDriverPath);
        
        [Test]
        public void MietSearchBarCheck()
        {
            WebDriver driver = new ChromeDriver(chromeDriverPath);
            driver.Url = "https://miet.ru/search/";
            WebElement searchBar = null;
            try
            {
                searchBar = (WebElement)driver.FindElement(By.ClassName("search-bar"));
            }
            catch (NoSuchElementException)
            {
                driver.Quit();
                Assert.Fail("No Search bar on page");
            }
            driver.Quit();
            Assert.IsTrue(true);
        }

        [Test]
        public void MietSearchResultsCheck()
        {
            WebDriver driver = new ChromeDriver(chromeDriverPath);
            driver.Url = "https://miet.ru/search/";
            WebElement searchBar = (WebElement)driver.FindElement(By.ClassName("search-bar__input"));
            searchBar.SendKeys("Отсутствующий человек\n");
            Thread.Sleep(sleepTime);
            WebElement PeopleCount = (WebElement)driver.FindElement(By.LinkText("Люди"));
            Assert.AreEqual(PeopleCount.GetAttribute("data-count"), 0.ToString());
        }

        [Test]
        public void SteamSearchCheck()
        {
            _driver.Navigate().GoToUrl("https://www.google.com/");
            var searchBar = _driver.FindElement(By.Name("q"));
            searchBar.SendKeys("deep rock");
            searchBar.SendKeys(Keys.Return);
            Thread.Sleep(sleepTime);
            var steamLink = _driver.FindElement(By.CssSelector(@"a[href='https://store.steampowered.com/app/548430/Deep_Rock_Galactic/']"));
            steamLink.Click();
            Thread.Sleep(sleepTime);
            var tags = _driver.FindElements(By.CssSelector(@"a[class=app_tag]")).FirstOrDefault(x => x.Text == "Кооператив");
            Assert.NotNull(tags);
            _driver.Close();
        }

        [Test]
        public void OrioksRegistrationCheck()
        {
            WebDriver driver = new ChromeDriver(chromeDriverPath);
            driver.Url = "https://orioks.miet.ru/user/login";
            WebElement username = (WebElement)driver.FindElement(By.Id("loginform-login"));
            WebElement password = (WebElement)driver.FindElement(By.Id("loginform-password"));
            WebElement authorization = (WebElement)driver.FindElement(By.Id("loginbut"));
            username.SendKeys(""); // data removed 
            password.SendKeys(""); // data removed 
            authorization.Click();
            Thread.Sleep(1000);
            string expectedUrl = driver.Url;
            string actualUrl = "https://orioks.miet.ru/";
            driver.Quit();
            Assert.AreEqual(expectedUrl, actualUrl);
        }
    }
}