using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    // Simple solution for storing sorting settings.
    // May be extened in future to toke into account another settings.
    // For simplicity, in this example I identify user by string userId value (nickname, for example)
    public interface IUserSettingsRepository
    {
        UserSettings GetSettings(string userId);

        void SaveSettings(UserSettings settings);
    }
}
