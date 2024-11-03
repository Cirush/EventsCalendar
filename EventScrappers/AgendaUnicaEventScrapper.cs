using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace EventsCalendar.EventScrappers;

public class AgendaUnicaEventScraper : IEventScraper
{
  private readonly IWebDriver _driver;
  private readonly string _url = SupportedEventPages.AgendaUnica;
  public AgendaUnicaEventScraper()
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
    var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

    bool hasNextPage = true;
    while (hasNextPage)
    {
      try
      {
        wait.Until(driver => driver.FindElements(By.ClassName("cards-wrapper")).Count > 0);
      }
      catch (WebDriverTimeoutException ex)
      {
        Console.WriteLine("Cards wrapper not found " + ex);
      }
      
      var cardElements = _driver.FindElements(By.ClassName("card-body"));
      foreach (var card in cardElements)
      {
        var e = new Event();
        try
        {
          e.Title = card.FindElements(By.CssSelector("header > h3 > a")).FirstOrDefault()?.Text;
          e.Description = card.FindElements(By.CssSelector(".card-description")).FirstOrDefault()?.Text;
          e.Location = card.FindElements(By.CssSelector(".card-event-location > a")).FirstOrDefault()?.Text;
          e.Organizer = card.FindElements(By.CssSelector("#event-organizer > a")).FirstOrDefault()?.Text;
          var dateInitText = card.FindElements(By.CssSelector(".card-event-init")).FirstOrDefault()?.Text;
          if (DateTime.TryParse(dateInitText, out DateTime eventInitDateTime))
          {
            e.Date = eventInitDateTime;
          }
        }
        catch (Exception exception)
        {
          Console.WriteLine(exception);
        }
        eventsData.Add(e);
        Console.WriteLine("Evento scrapeado: {0}", e.Title);
      }
      hasNextPage = GoToNextPage();
    }
    
    _driver.Quit();
    return eventsData;
  }

  private bool GoToNextPage()
  {
    var paginatorItems = _driver.FindElements(By.ClassName("page-item"));
    var nextButton = paginatorItems.LastOrDefault();

    if (nextButton != null && nextButton.Text == "»" && !nextButton.GetAttribute("class").Contains("disabled"))
    {
      Console.WriteLine("Voy a la siguiente pagina");
      nextButton.Click();
      return true;
    }
    
    Console.WriteLine("Llegado a la ultima pagina");
    return false;
  }
}