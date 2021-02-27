using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading.Tasks;

namespace ConsoleSeleniumCovidResultParser
{
    class Program
    {
        static void Main(string[] args)
        {
            using ChromeDriver driver = new ChromeDriver();

            string result;
            try
            {
                driver
                    .Navigate()
                    .GoToUrl("https://securelink.labmed.uw.edu/?code=QRNJZEWDN8TGDTJD");

                var form = driver.FindElement(By.Id("submitform"));
                var barcode = driver.FindElement(By.Id("barcode"));
                var dob = driver.FindElement(By.Id("dob"));
                dob.SendKeys("03/16/1984");

                var button = driver.FindElement(By.TagName("Button"));
                button.Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.ElementExists(By.Id("result-card")));

                var resultCard = driver.FindElement(By.Id("result-card"));
                result = resultCard.Text;

            }
            finally
            {
                driver.Quit();
            }


            Console.WriteLine(result);
        }
    }
}
