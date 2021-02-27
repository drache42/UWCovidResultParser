using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSeleniumCovidResultParser
{
    class WebDriverResultGrabber
    {
        public string getResult(string code = "QRNJZEWDN8TGDTJD", string dob = "03/16/1984")
        {
            using ChromeDriver driver = new ChromeDriver();

            string result;
            try
            {
                string url = $"https://securelink.labmed.uw.edu/?code={code}";
                driver
                    .Navigate()
                    .GoToUrl(url);

                var form = driver.FindElement(By.Id("submitform"));
                var barcode = driver.FindElement(By.Id("barcode"));
                var dobelement = driver.FindElement(By.Id("dob"));
                dobelement.SendKeys(dob);

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

            return result;
        }
    }
}
