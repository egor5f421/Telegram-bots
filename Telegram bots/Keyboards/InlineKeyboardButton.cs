using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Telegram_bots
{
    /// <summary>
    /// The inline keyboard button
    /// </summary>
    [Serializable]
    public class InlineKeyboardButton : IKeyboardButton
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }
        /// <summary>
        /// CallbackData of the inline keyboard buttons
        /// </summary>
        [JsonPropertyName("callback_data")]
        public string CallbackData { get; set; }

        /// <summary>
        /// Create an inline keyboard button
        /// </summary>
        /// <param name="text">Button's text</param>
        /// <param name="callbackData">CallbackData of the inline keyboard buttons</param>
        public InlineKeyboardButton(string text, string callbackData)
        {
            Text = text;
            CallbackData = callbackData;
        }

        /// <summary>
        /// Create an inline keyboard button
        /// </summary>
        /// <param name="text">Button's text</param>
        public InlineKeyboardButton(string text)
        {
            Text = text;
            CallbackData = text;
        }
    }
}
