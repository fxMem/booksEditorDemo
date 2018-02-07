using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public class InMemorySettingsRepository : IUserSettingsRepository
    {
        private readonly string _unknownUserKey = "Unknown";
        private ConcurrentDictionary<string, UserSettings> _settings = new ConcurrentDictionary<string, UserSettings>();

        public InMemorySettingsRepository()
        {
            _settings.TryAdd(_unknownUserKey, new UserSettings
            {
                UserId = _unknownUserKey,
                SortBy = SortByProperty.Title,
                OrderByDescending = false
            });
        }

        public UserSettings GetSettings(string userId)
        {
            _settings.TryGetValue(userId, out UserSettings result);
            return result;
        }

        public void SaveSettings(UserSettings settings)
        {
            _settings.AddOrUpdate(settings.UserId, settings, (key, value) => settings);
        }
    }
}
