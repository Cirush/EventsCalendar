using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EventsCalendar.EventScrappers;

public class JuntaDeAndaluciaEventScraper : IEventScraper
{
  private readonly IWebDriver _driver;
  private readonly string _url = SupportedEventPages.JuntaDeAndalucia;

  public JuntaDeAndaluciaEventScraper()
  {
    var options = new ChromeOptions();
    options.AddArgument("--headless");
    options.AddArgument("--no-sandbox");
    options.AddArgument("--disable-gpu");

    _driver = new ChromeDriver(options);
  }
  public List<Event> ScrapeEvents()
  {
    var eventsData = new List<Event>();
    _driver.Navigate().GoToUrl(_url);
    var grid = _driver.FindElements(By.XPath("//*[@id=\"block-wingsuit-content\"]/div/div/div"));
    var elements = _driver.FindElements(By.CssSelector(".flex.flex-col[class='flex flex-col']:not(footer *)"));
    
    foreach (var element in elements)
    {
      var name = element.FindElement(By.CssSelector("article > div > div:nth-child(2) > div:nth-child(1)")).Text;
      var day = element.FindElement(By.CssSelector("div.relative > div > div > div > div.text-4xl.font-light")).Text;
      var month = element.FindElement(
        By.CssSelector("div.relative > div > div > div > div:nth-child(2) > div.text-sm.leading-tight.uppercase")).Text;
      var year = element.FindElement(By.CssSelector("div.relative > div > div > div > div:nth-child(2) > div.text-xs.leading-none")).Text;
      var date = DateTime.Parse($"{day} de {month} de {year}");
      Console.WriteLine($"{name} - {date}");
    }
    _driver.Quit();
    return eventsData;
  }
}