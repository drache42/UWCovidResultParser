using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSeleniumCovidResultParser
{
    class WebDriverResultGrabber
    {
        public async Task<string> getResultAsync(string code = "QRNJZEWDN8TGDTJD", string dob = "03/16/1984")
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            using ChromeDriver driver = new ChromeDriver(chromeOptions);

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

                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                //wait.Until(ExpectedConditions.ElementExists(By.Id("result-card")));
                await Task.Delay(TimeSpan.FromSeconds(3));

                string resultCard = "";
                try
                {
                    resultCard = driver.FindElement(By.Id("result-card")).ToString();
                    Console.WriteLine(DateTime.Now + " Found a result card!\n" + resultCard.ToString());
                }
                catch(Exception e)
                {
                    Console.WriteLine(DateTime.Now + " Error: " + e.Message);
                }
                
                if(resultCard != "")
                {
                    result = driver.PageSource;
                }
                else
                {
                    result = resultCard.ToString();
                }

            }
            finally
            {
                driver.Quit();
            }

            return result;
        }
    }
}
