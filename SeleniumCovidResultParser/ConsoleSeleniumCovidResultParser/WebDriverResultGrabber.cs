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
        public class WebDriverResultGrabberResult
        {
            public string Result { get; set; }
            public bool isHtml { get; set; }
        }

        public async Task<WebDriverResultGrabberResult> GetResultAsync(string code, string dob)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            using ChromeDriver driver = new ChromeDriver(chromeOptions);

            string textResult;
            WebDriverResultGrabberResult result = new WebDriverResultGrabberResult();

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
                    resultCard = driver.FindElement(By.Id("result-card")).Text;
                    Console.WriteLine(DateTime.Now + " Found a result card!\n" + resultCard.ToString());
                }
                catch(Exception e)
                {
                    Console.WriteLine(DateTime.Now + " Error: " + e.Message);
                }
                
                if(resultCard == "")
                {
                    result.Result = driver.PageSource;
                    result.isHtml = true;
                }
                else
                {
                    result.Result = resultCard;
                    result.isHtml = false;
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
