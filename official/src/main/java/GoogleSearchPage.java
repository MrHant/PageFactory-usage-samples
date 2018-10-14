import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.How;
import org.openqa.selenium.WebElement;


public class GoogleSearchPage {

    @FindBy(how = How.NAME, using = "q")
    private WebElement queryField;

    public void searchFor(String text) {
        queryField.sendKeys(text);
        queryField.submit();
    }

}
