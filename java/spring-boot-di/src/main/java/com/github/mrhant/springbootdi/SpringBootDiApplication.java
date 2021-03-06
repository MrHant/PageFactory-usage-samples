package com.github.mrhant.springbootdi;

import io.github.bonigarcia.wdm.WebDriverManager;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.PageFactory;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;

@SpringBootApplication
public class SpringBootDiApplication {
    @Bean(destroyMethod = "quit")
    public WebDriver webDriver() {
        WebDriverManager.chromedriver().setup();
        return new ChromeDriver();
    }

    @Bean
    public GoogleSearchPage googleSearchPage(WebDriver driver) {
        return PageFactory.initElements(driver, GoogleSearchPage.class);
    }

    @Bean
    public GoogleResultsPage googleResultsPage(WebDriver driver) {
        return PageFactory.initElements(driver, GoogleResultsPage.class);
    }
}
