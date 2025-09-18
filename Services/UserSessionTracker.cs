using Microsoft.AspNetCore.Components.Server.Circuits;
using System;
using System.Collections.Concurrent;

namespace EventEaseApp.Services
{
    public class UserSessionTracker : CircuitHandler
    {
        private readonly ConcurrentDictionary<string, DateTime> _activeSessions = new();

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _activeSessions[circuit.Id] = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _activeSessions.TryRemove(circuit.Id, out _);
            return Task.CompletedTask;
        }

        public IReadOnlyDictionary<string, DateTime> GetActiveSessions() => _activeSessions;
    }
}
