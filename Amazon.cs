using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;

namespace AnilKumarOtte_Assignment
{
    public class Tests
    {
        public IWebDriver driver;
        [SetUp]

        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Url = "https://www.amazon.in";

            string url = driver.Url;
            driver.Manage().Window.Maximize();
            Assert.AreEqual(url, driver.Url);

        }

        [Test]
        public void Test1()
        {
            driver.FindElement(By.XPath("//input[@placeholder=\"Search Amazon.in\"]")).SendKeys("Titanwatch");
           
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Wait for suggestions dropdown to be visible
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".autocomplete-results-container")));

            // Capture all suggestions
            IList<IWebElement> suggestions = driver.FindElements(By.CssSelector(".s-suggestion"));
            suggestions[1].Click();
            

           

            // Capture details of the first product
            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait1.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.s-main-slot div.s-result-item")));
           
            IList<IWebElement> firstProduct = driver.FindElements(By.CssSelector("div.s-main-slot div.s-result-item"));
           

            var productLinkElement = firstProduct[3].FindElement(By.CssSelector("h2 a, span.a-text-normal"));
            productLinkElement.Click();
            var producttext = firstProduct[3].Text;
            Console.WriteLine(producttext);
            string originalWindow = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(driver.WindowHandles[1]);


            // Add the product to the cart
            driver.FindElement(By.Id("add-to-cart-button")).Click();
            Thread.Sleep(3000);
            
            driver.FindElement(By.XPath("/html/body/div[8]/div[3]/div[1]/div/div/div[2]/div[2]/div/div/div[3]/div/span[2]/span/input")).Click();
            Console.WriteLine("Product added to cart.");
            WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            IWebElement checkoutButton = wait3.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#sc-buy-box-ptc-button > span > input")));
            checkoutButton.Click();

            
            Console.WriteLine("Clicked on buy now button.");


                                

        }
        [TearDown]
        public void teardown()
        {
            driver.Quit();
        }
    }
}