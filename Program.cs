using EventsCalendar;
using EventsCalendar.EventScrappers;

// var agendaUnicaScraper = new EventScraperFactory().Create(SupportedEventPages.AgendaUnica);
var juntaDeAndaluciaScraper = new EventScraperFactory().Create(SupportedEventPages.JuntaDeAndalucia);
var icsGenerator = new IcsFileGenerator();

// var events = agendaUnicaScraper.ScrapeEvents();
var events = juntaDeAndaluciaScraper.ScrapeEvents();
// icsGenerator.GenerateIcsFile(events, "evento.ics");
// Console.WriteLine("Archivo ICS creado exitosamente.");
