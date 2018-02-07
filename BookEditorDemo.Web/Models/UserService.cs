using BookEditorDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookEditorDemo.Web.Models
{
    public class UserService
    {
        private IUserSettingsRepository _settingsRepository;
        private string _currentUserId;
        private UserSettings _settings;

        public UserService(IUserSettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        // Authentificate user somehow...
        public void SetCurrentUser(string userId)
        {
            _currentUserId = userId;
        }

        public UserSettings Settings
        {
            get
            {
                // This service has request lifetime, so addiitonal locking is not required
                if (_settings == null)
                {
                    _settings = _settingsRepository.GetSettings(_currentUserId ?? "Unknown");
                }

                return _settings;
            }
        }

        public void SetSortingProperty(SortByProperty sortByProperty)
        {
            Settings.SortBy = sortByProperty;
            _settingsRepository.SaveSettings(Settings);
        }

        public void SetSortingOrder(bool orderByDescending)
        {
            Settings.OrderByDescending = orderByDescending;
            _settingsRepository.SaveSettings(Settings);
        }
    }
}