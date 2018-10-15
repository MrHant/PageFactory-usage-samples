import io.github.bonigarcia.wdm.WebDriverManager;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.PageFactory;

import java.util.List;


public class Tests {

    @Test
    public void enterTextInGoogleSearchInput(){
        // download driver binary
        WebDriverManager.chromedriver().setup();

        // launch driver
        WebDriver driver = new ChromeDriver();


        // open Google start page page
        driver.navigate().to("https://google.com");

        // init elements
        GoogleSearchPage googleSearchPage = PageFactory.initElements(driver, GoogleSearchPage.class);

        // do the search.
        googleSearchPage.searchFor("Cheese");

        // list found results
        GoogleResultsPage googleResultsPage = PageFactory.initElements(driver, GoogleResultsPage.class);
        List<String> results = googleResultsPage.searchResultsTitles();

        // assert that there are 9 results
        Assertions.assertEquals(9, results.size());


        // close driver
        driver.quit();
    }
}
