using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleSeleniumCovidResultParser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<string> emailToNotify = new List<string>()
            {
                "drache42@gmail.com",
                "audreydb13@gmail.com"
            };

            string code = "G39AQYXA7P2PG32a";
            string dob = "08/23/2017";

            string result = "";
            while(true)
            {
                var driver = new WebDriverResultGrabber();
                var newresult = await driver.GetResultAsync(code, dob);

                if(result != newresult.Result)
                {
                    Console.WriteLine(DateTime.Now + " Results are different!");
                    string subject = "UW Covid Result UPDATE " + DateTime.Now.ToString();
                    result = newresult.Result;

                    Console.WriteLine(DateTime.Now + " " + result);

                    var emailer = new GmailSender();
                    foreach(string email in emailToNotify)
                    {
                        emailer.sendEmail(email, "drache42@gmail.com", subject, result, newresult.isHtml);
                    }
                    
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
