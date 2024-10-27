using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Telegram_bots.Keyboards
{
    /// <summary>
    /// Keyboard
    /// </summary>
    /// <remarks>
    /// Create a keyboard
    /// </remarks>
    /// <param name="buttons">Keyboard's buttons</param>
    [Serializable]
    public class ReplyKeyboard(KeyboardButton[][] buttons) : IKeyboard
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [JsonPropertyName("keyboard")]
        public IKeyboardButton[][] Keyboard { get; set; } = buttons;
    }
}
