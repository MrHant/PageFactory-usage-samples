package com.github.mrhant.springbootdi;

import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.How;

import java.util.List;
import java.util.stream.Collectors;

public class GoogleResultsPage {
    @FindBy(how = How.TAG_NAME, using = "h3")
    private List<WebElement> searchResults;

    public List<String> searchResultsTitles(){
        return searchResults.stream()
                .map(r -> r.getText())
                .collect(Collectors.toList());
    }
}
