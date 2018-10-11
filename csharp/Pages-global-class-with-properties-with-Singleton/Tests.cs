using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using Tiver.Fowl.Drivers.Downloaders;

namespace Pages_global_class_with_properties_with_Singleton
{
    public static class Pages
    {
        public static IWebDriver Driver;

        public static GoogleSearchPage GoogleSearchPage
        {
            get
            {
                _googleSearchPage = _googleSearchPage ?? new GoogleSearchPage(Driver);
                PageFactory.InitElements(Driver, _googleSearchPage);
                return _googleSearchPage;
            }
        }

        public static GoogleResultsPage GoogleResultsPage
        {
            get
            {
                _googleResultsPage = _googleResultsPage ?? new GoogleResultsPage(Driver);
                PageFactory.InitElements(Driver, _googleResultsPage);
                return _googleResultsPage;
            }
        }
        
        private static GoogleSearchPage _googleSearchPage;
        private static GoogleResultsPage _googleResultsPage;        
    }  
    
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
        [SetUp]
        public void Setup()
        {
            // download driver binary
            new ChromeDriverDownloader().DownloadBinary("LATEST_RELEASE");
            
            // launch driver
            Pages.Driver = new ChromeDriver();
        }
        
        [Test]
        public void Test1()
        {
            // open Google start page page
            Pages.Driver.Navigate().GoToUrl("https://google.com");

            
            // do the search.
            Pages.GoogleSearchPage.SearchFor("Cheese");
            
            // list found results
            var results = Pages.GoogleResultsPage.SearchResultsTitles();
            
            
            // assert that there are 10 results
            Assert.IsTrue(results.Count().Equals(10));
        }

        [TearDown]
        public void Teardown()
        {
            // close driver
            Pages.Driver.Quit();
        }
    }}