using System.Text.Json.Serialization;
using Telegram_bots.Keyboards;

namespace Telegram_bots
{
    /// <summary>
    /// Keyboard's button
    /// </summary>
    [JsonDerivedType(typeof(InlineKeyboardButton))]
    [JsonDerivedType(typeof(KeyboardButton))]
    public interface IKeyboardButton
    {
        /// <summary>
        /// Button's text
        /// </summary>
        [JsonPropertyName("text")]
        string Text { get; set; }
    }
}