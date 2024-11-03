using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;

namespace EventsCalendar;

public class IcsFileGenerator
{
  public void GenerateIcsFile(List<Event> events, string fileName)
  {
    var calendar = new Calendar();

    foreach (var e in events)
    {
      if (e.Date.HasValue)
      {
        var evento = new CalendarEvent
        {
          Summary = e.Title,
          Description = e.Description,
          Location = e.Location,
          DtStart = new CalDateTime(e.Date.Value),
          DtEnd = new CalDateTime(e.Date.Value),
          Uid = GenerateUid(e)
        };
        calendar.Events.Add(evento);
      }
    }

    var serializer = new CalendarSerializer();
    var icsContent = serializer.SerializeToString(calendar);
    File.WriteAllText(fileName, icsContent);
  }

  private static string GenerateUid(Event e)
  {
    return $"{e.Title}-{e.Date}-{e.Location}-{e.Organizer}".GetHashCode().ToString();
  }
}
