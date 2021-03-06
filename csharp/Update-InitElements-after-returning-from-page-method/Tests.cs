﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using Tiver.Fowl.Drivers.Downloaders;

namespace Update_InitElements_after_returning_from_page_method
{
    public class GoogleSearchPage {
        private IWebDriver _driver;

        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement _queryField;

        public GoogleSearchPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public GoogleResultsPage SearchFor(string text) {
            _queryField.SendKeys(text);
            _queryField.Submit();
            return new GoogleResultsPage(_driver);
        }
    }

    public class GoogleResultsPage {
        private IWebDriver _driver;

        [FindsBy(How = How.TagName, Using = "h3")]
        private IList<IWebElement> _searchResults;

        public GoogleResultsPage(IWebDriver driver)
        {
            _driver = driver;
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
            var page = new GoogleSearchPage(driver);
            PageFactory.InitElements(driver, page);

            // do the search.
            var resultsPage = page.SearchFor("Cheese");
            PageFactory.InitElements(driver, resultsPage);
            
            // list found results
            var results = resultsPage.SearchResultsTitles();
            
            // assert that there are 10 results
            Assert.IsTrue(results.Count().Equals(10));
            
            
            // close driver
            driver.Quit();
        }
    }
}