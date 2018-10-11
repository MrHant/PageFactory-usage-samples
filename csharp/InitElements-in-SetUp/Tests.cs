using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using Tiver.Fowl.Drivers.Downloaders;

namespace InitElements_in_SetUp
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
        private IWebDriver Driver;
        private GoogleSearchPage GoogleSearchPage;
        private GoogleResultsPage GoogleResultsPage;
        
        [SetUp]
        public void SetUp()
        {
            // download driver binary
            new ChromeDriverDownloader().DownloadBinary("LATEST_RELEASE");
            
            // launch browser
            Driver = new ChromeDriver();
            
            // init elements
            GoogleSearchPage = PageFactory.InitElements<GoogleSearchPage>(Driver);
            GoogleResultsPage = PageFactory.InitElements<GoogleResultsPage>(Driver);
        }
        
        [Test]
        public void EnterTextInGoogleSearchInput()
        {
            // open Google start page page
            Driver.Navigate().GoToUrl("https://google.com");
            
            // do the search.
            GoogleSearchPage.SearchFor("Cheese");
            
            // list found results
            var results = GoogleResultsPage.SearchResultsTitles();
            
            // assert that there are 10 results
            Assert.IsTrue(results.Count().Equals(10));
            
            
            // close driver
            Driver.Quit();
        }
    }}