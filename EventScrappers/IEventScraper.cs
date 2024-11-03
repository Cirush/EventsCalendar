namespace EventsCalendar.EventScrappers;

public interface IEventScraper
{
  List<Event> ScrapeEvents();
}