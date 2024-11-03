using EventsCalendar;
using EventsCalendar.EventScrappers;

var agendaUnicaScraper = new EventScraperFactory().Create(SupportedEventPages.AgendaUnica);
var juntaDeAndaluciaScraper = new EventScraperFactory().Create(SupportedEventPages.JuntaDeAndalucia);
var icsGenerator = new IcsFileGenerator();
var events = new List<Event>();

var agendaUnicaTask = Task.Run(() =>
{
    var agendaUnicaEvents = agendaUnicaScraper.ScrapeEvents();
    lock (events)
    {
        events.AddRange(agendaUnicaEvents);
    }
});

var juntaTask = Task.Run(() =>
{
    var juntaDeAndaluciaEvents = juntaDeAndaluciaScraper.ScrapeEvents();
    lock (events)
    {
        events.AddRange(juntaDeAndaluciaEvents);
    }
});

await Task.WhenAll(agendaUnicaTask, juntaTask);

icsGenerator.GenerateIcsFile(events, "evento.ics");
Console.WriteLine("Archivo ICS creado exitosamente.");
