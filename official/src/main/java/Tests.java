import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.PageFactory;

import java.util.List;


public class Tests {

    @Test
    public void enterTextInGoogleSearchInput(){
        WebDriver driver = new ChromeDriver();

        driver.navigate().to("https://google.com");

        GoogleSearchPage googleSearchPage = PageFactory.initElements(driver, GoogleSearchPage.class);

        googleSearchPage.searchFor("Cheese");

        GoogleResultsPage googleResultsPage = PageFactory.initElements(driver, GoogleResultsPage.class);
        List<String> results = googleResultsPage.searchResultsTitles();

        Assertions.assertEquals(9, results.size());

        driver.quit();
    }
}
