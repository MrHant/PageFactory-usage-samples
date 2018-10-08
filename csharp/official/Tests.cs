using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using Tiver.Fowl.Drivers.Downloaders;

namespace official
{
    public class GoogleSearchPage {
        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement _queryField;

        public GoogleSearchPage(IWebDriver driver)
        {
        }

        public void SearchFor(string text) {
            _queryField.SendKeys(text);
            _queryField.Submit();
        }
    }

    public class GoogleResultsPage {
        [FindsBy(How = How.TagName, Using = "h3")]
        private IList<IWebElement> _searchResults;

        public GoogleResultsPage(IWebDriver driver)
        {
        }

        public IEnumerable<string> SearchResultsTitles()
        {
            return _searchResults.Select(r => r.Text);
        }
    }
    
    [TestFixture]
    public class Tests
    {
        [Test]
        public void EnterTextInGoogleSearchInput()
        {
            // download driver binary
            new ChromeDriverDownloader().DownloadBinary("LATEST_RELEASE");
            
            // launch driver
            var driver = new ChromeDriver();
              
            
            // open Google start page page
            driver.Navigate().GoToUrl("https://google.com");
            
            // init elements
            var page = PageFactory.InitElements<GoogleSearchPage>(driver);

            // do the search.
            page.SearchFor("Cheese");
            
            // list found results
            var resultsPage = PageFactory.InitElements<GoogleResultsPage>(driver);
            var results = resultsPage.SearchResultsTitles();
            
            // assert that there are 10 results
            Assert.IsTrue(results.Count().Equals(10));
            
            
            // close driver
            driver.Quit();
        }
    }
}