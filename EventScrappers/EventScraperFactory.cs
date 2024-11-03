namespace EventsCalendar.EventScrappers;

public class EventScraperFactory
{
  public IEventScraper Create(string eventPage)
  {
    return eventPage switch
    {
      SupportedEventPages.AgendaUnica => new AgendaUnicaEventScraper(),
      SupportedEventPages.JuntaDeAndalucia => new JuntaDeAndaluciaEventScraper(),
      _ => throw new ArgumentOutOfRangeException(nameof(eventPage), eventPage, "Scrapper not found.")
    };
  }
}