using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ShortTermRental.UiTests;

public class UiTests : IDisposable
{
    private readonly IWebDriver _driver;
    private const string BaseUrl = "http://localhost:5017/"; // change if needed

    public UiTests()
    {
        var options = new ChromeOptions();
        //options.AddArgument("--headless=new");   // comment this out if you want to SEE the browser
        options.AddArgument("--window-size=1920,1080");

        _driver = new ChromeDriver(options);
    }

    public void Dispose()
    {
        _driver.Quit();
    }

    [Fact]
    public void DeerfieldRetreatProperty_Loads_AndShowsPropertyTitle()
    {
        _driver.Navigate().GoToUrl(BaseUrl);

        Assert.Contains("Deerfield Retreat", _driver.Title + _driver.PageSource);
    }

    [Fact]
    public void DeerfieldRetreatProperty_HasAirbnbAndVrboButtons()
    {
        _driver.Navigate().GoToUrl(BaseUrl);

        // These are <a> tags styled as buttons, so LinkText should work
        var airbnbButton = _driver.FindElement(By.LinkText("View on Airbnb"));
        var vrboButton = _driver.FindElement(By.LinkText("View on Vrbo"));

        Assert.NotNull(airbnbButton);
        Assert.NotNull(vrboButton);

        // Basic sanity check that they actually go somewhere
        Assert.False(string.IsNullOrWhiteSpace(airbnbButton.GetAttribute("href")));
        Assert.False(string.IsNullOrWhiteSpace(vrboButton.GetAttribute("href")));
    }

    [Fact]
    public void NavBar_HasHomeContactLoginRegister()
    {
        _driver.Navigate().GoToUrl(BaseUrl);

        // top-right nav
        Assert.Contains("Home", _driver.PageSource);
        Assert.Contains("Contact", _driver.PageSource);
        Assert.Contains("Login/Register", _driver.PageSource);
    }
    [Fact]
    public void Smoke_ContactPageLoads()
    {
        _driver.Navigate().GoToUrl($@"{BaseUrl}contact");
        var h1 = WaitForElement(By.TagName("h1"));
        Assert.Equal("Contact Us", h1.Text);
    }

    [Fact]
    public void Smoke_LoginPageLoads()
    {
        _driver.Navigate().GoToUrl($@"{BaseUrl}login");
        var loginButton = WaitForElement(By.CssSelector("button.btn-login"));
        Assert.Equal("Login", loginButton.Text);
    }

    [Fact]
    public void Smoke_RegisterPageLoads()
    {
        _driver.Navigate().GoToUrl($@"{BaseUrl}Register");
        var createAccountBtn = WaitForElement(By.CssSelector("button.btn-login"));
        Assert.Equal("Create Account", createAccountBtn.Text.Trim());
    }

    [Fact]
    public void Smoke_DeerfieldRetreatPropertyPageLoads()
    {
        _driver.Navigate().GoToUrl($@"{BaseUrl}");
        var title = WaitForElement(By.CssSelector("h1.hero-title"));
        Assert.Equal("Deerfield Retreat", title.Text.Trim());
    }
    private IWebElement WaitForElement(By by, int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(new SystemClock(), _driver, TimeSpan.FromSeconds(timeoutSeconds), TimeSpan.FromMilliseconds(500));
        return wait.Until(drv => drv.FindElement(by));
    }


}
