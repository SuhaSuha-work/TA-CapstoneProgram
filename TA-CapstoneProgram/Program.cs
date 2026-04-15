using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;
using System;
using System.Security.Cryptography.X509Certificates;

namespace SeleniumTests
{
    [TestFixture]
    public class Program
    {
        //public void Main(string[] args)
        //{
        /*
         * open chrome browser
         * to avoid flakiness setup webdriver wait
         * launch turnup portal
         * wait for the page to load
         * identify username field and enter valid username
         * identify password field and enter valid password
         * identify login button and click on it
         * check if user logged in sucessfully
        */

        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
        }

        [Test]
        public void Valid_Login()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("username")));

            IWebElement usernameField = driver.FindElement(By.Name("username"));
            usernameField.SendKeys("Admin");
            IWebElement passwordField = driver.FindElement(By.Name("password"));
            passwordField.SendKeys("admin123");
            IWebElement button = driver.FindElement(By.XPath("//button[@type='submit']"));
            button.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h6[text()='Dashboard']")));

            //Assert that the Dashboard header is present to confirm successful login
            string headerText = driver.FindElement(By.XPath("//h6[text()='Dashboard']")).Text;
            Assert.That(headerText, Is.EqualTo("Dashboard"), "Login failed - Dashboard header not found");

            //Assert URL verification
            string currentUrl = driver.Url;
            Assert.That(currentUrl, Does.Contain("dashboard"), "Login failed - URL mismatch");

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        //}
    }
}