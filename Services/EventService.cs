using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EventEaseApp.Models;

namespace EventEaseApp.Services
{
    public class EventService
    {
        private readonly string _filePath = "events.json";
        public List<Event> Events { get; private set; } = new();

        public EventService()
        {
            LoadEvents();
        }

        public void LoadEvents()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var events = JsonSerializer.Deserialize<List<Event>>(json);
                Events = events ?? new List<Event>();
            }
        }

        public void SaveEvents()
        {
            var json = JsonSerializer.Serialize(Events, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public void AddEvent(Event ev)
        {
            Events.Add(ev);
            SaveEvents();
        }

        public void DeleteEvent(Guid id)
        {
            Events.RemoveAll(e => e.Id == id);
            SaveEvents();
        }

        public Event? GetEventById(Guid id)
        {
            return Events.Find(e => e.Id == id);
        }
    }
}
