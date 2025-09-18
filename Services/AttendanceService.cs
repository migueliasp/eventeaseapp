using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EventEaseApp.Models;

namespace EventEaseApp.Services
{
    public class AttendanceService
    {
        private readonly string _filePath = "attendance.json";
        public List<Registration> Registrations { get; private set; } = new();

        public AttendanceService()
        {
            LoadRegistrations();
        }

        public void LoadRegistrations()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var regs = JsonSerializer.Deserialize<List<Registration>>(json);
                Registrations = regs ?? new List<Registration>();
            }
        }

        public void SaveRegistrations()
        {
            var json = JsonSerializer.Serialize(Registrations, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public void RegisterAttendance(Registration reg)
        {
            Registrations.Add(reg);
            SaveRegistrations();
        }

        public List<Registration> GetRegistrationsForEvent(Guid eventId)
        {
            return Registrations.FindAll(r => r.EventId == eventId);
        }
    }
}
