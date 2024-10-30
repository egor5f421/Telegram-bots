using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telegram_bots
{
    /// <summary>
    /// A class representing a callback query
    /// </summary>
    public class CallbackQuery
    {
        /// <summary>
        /// Id
        /// </summary>
        public required string Id { get; init; }
        /// <summary>
        /// Who sent the callback query
        /// </summary>
        public required User From {  get; init; }

        /// <summary>
        /// Data
        /// </summary>
        public string? Data { get; set; }
        /// <summary>
        /// The message from which the callback appeared
        /// </summary>
        public Message? Message { get; set; }

        #region FromJSON
        /// <summary>
        /// Create a <seealso cref="CallbackQuery"/> from a JsonDocument
        /// </summary>
        /// <param name="jsonDocument">JsonDocument</param>
        /// <returns><seealso cref="CallbackQuery"/></returns>
        public static CallbackQuery FromJSON(JsonDocument jsonDocument)
        {
            JsonElement json = jsonDocument.RootElement;

            CallbackQuery callbackQuery = new()
            {
                Id = json.GetProperty("id").GetString()!,
                From = User.FromJSON(json.GetProperty("from"))
            };

            if (json.TryGetProperty("data", out JsonElement data))
            {
                callbackQuery.Data = data.GetString();
            }
            if (json.TryGetProperty("message", out JsonElement message))
            {
                callbackQuery.Message = Message.FromJSON(message);
            }

            return callbackQuery;
        }
        /// <summary>
        /// Create a <seealso cref="CallbackQuery"/> from a JsonElement
        /// </summary>
        /// <param name="rootJsonElement">Root JsonElement</param>
        /// <returns><seealso cref="CallbackQuery"/></returns>
        public static CallbackQuery FromJSON(JsonElement rootJsonElement)
        {
            return FromJSON(rootJsonElement.GetRawText());
        }
        /// <summary>
        /// Create a <seealso cref="CallbackQuery"/> from a Json string
        /// </summary>
        /// <param name="jsonString">Json string</param>
        /// <returns><seealso cref="CallbackQuery"/></returns>
        public static CallbackQuery FromJSON(string jsonString)
        {
            return FromJSON(JsonDocument.Parse(jsonString));
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append("Id: ");
            builder.Append(Id);
            builder.Append(", From: (");
            builder.Append(From);
            builder.Append(')');
            if (Data != null)
            {
                builder.Append(", Data: ");
                builder.Append(Data);
            }
            if (Message != null)
            {
                builder.Append(", Message: (");
                builder.Append(Message);
                builder.Append(')');
            }
            return builder.ToString();
        }
    }
}
