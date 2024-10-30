using System.Collections;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Telegram_bots.Keyboards
{
    /// <summary>
    /// Inline keyboard
    /// </summary>
    /// <remarks>
    /// Create an inline keyboard
    /// </remarks>
    /// <param name="buttons">Keyboard's buttons</param>
    [Serializable]
    public class InlineKeyboard(InlineKeyboardButton[][] buttons) : IKeyboard
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [JsonPropertyName("inline_keyboard")]
        public IKeyboardButton[][] Keyboard { get; set; } = buttons;
    }
}
