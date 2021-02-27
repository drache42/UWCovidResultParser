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
            var driver = new WebDriverResultGrabber();
            Console.WriteLine(driver.getResult());
            Console.ReadKey();
        }
    }
}
