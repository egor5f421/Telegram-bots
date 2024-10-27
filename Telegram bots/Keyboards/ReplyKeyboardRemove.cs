using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Telegram_bots.Keyboards
{
    /// <summary>
    /// Remove the keyboard
    /// </summary>
    [Serializable]
    public class ReplyKeyboardRemove : IKeyboard
    {
        /// It's nothing
        [JsonIgnore]
        public IKeyboardButton[][] Keyboard { get; set; } = [[]];
        /// <summary>
        /// Should I remove the keyboard or not?
        /// </summary>
        [JsonPropertyName("remove_keyboard")]
        public bool RemoveKeyboard { get; set; } = true;
    }
}
