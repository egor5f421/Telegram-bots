using System.Text.Json.Serialization;

namespace Telegram_bots.Keyboards
{
    /// <summary>
    /// Keyboard button
    /// </summary>
    /// <remarks>
    /// Create a keyboard button
    /// </remarks>
    /// <param name="text">Button's text</param>
    [Serializable]
    public class KeyboardButton(string text) : IKeyboardButton
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; } = text;
    }
}