package com.github.mrhant.springbootdi;

import org.junit.jupiter.api.Test;
import org.openqa.selenium.WebDriver;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;

@SpringBootTest
public class Tests {
    @Autowired
    private WebDriver driver;

    @Autowired
    private GoogleSearchPage googleSearchPage;

    @Autowired
    private GoogleResultsPage googleResultsPage;

    @Test
    void test1() {
        // open Google start page page
        driver.navigate().to("https://google.com");

        // do the search.
        googleSearchPage.searchFor("Cheese");

        // list found results
        List<String> results = googleResultsPage.searchResultsTitles();

        // assert that there are 10 results
        assertEquals(10, results.size());

        // close driver
        driver.quit();
    }
}
