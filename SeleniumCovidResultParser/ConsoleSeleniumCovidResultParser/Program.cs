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
        static async Task Main(string[] args)
        {
            string result = "";
            while(true)
            {
                var driver = new WebDriverResultGrabber();
                var newresult = await driver.getResultAsync("L8WQAQGLEKYH8ZT8", "02/07/2020");

                if(result != newresult)
                {
                    Console.WriteLine(DateTime.Now + " Results are different!");
                    string subject = "UW Covid Result UPDATE " + DateTime.Now.ToString();
                    result = newresult;

                    Console.WriteLine(DateTime.Now + " " + result);

                    var email = new GmailSender();
                    email.sendEmail("drache42@gmail.com", "drache42@gmail.com", subject, result);
                }
                else
                {
                    Console.WriteLine(DateTime.Now + " Results are the same!");
                }

                Console.WriteLine(DateTime.Now + " Waiting 5 minutes");
                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }
    }
}
