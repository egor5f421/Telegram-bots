using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Telegram_bots
{
    /// <summary>
    /// Describes reply parameters for the message that is being sent.
    /// </summary>
    [Serializable]
    public class ReplyParameters
    {
        /// <summary>
        /// Id to reply to the message
        /// </summary>
        [JsonPropertyName("message_id")]
        public long MessageId { get; init; }

        /// <summary>
        /// The chat_id where the reply message is located
        /// </summary>
        [JsonPropertyName("chat_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? ChatId { get; set; }

        /// <summary>
        /// Creates empty parameters for responding to a message
        /// </summary>
        public ReplyParameters()
        {
            MessageId = 0;
            ChatId = null;
        }
        /// <summary>
        /// Creates parameters for responding to a message
        /// </summary>
        /// <param name="message">The message that needs to be answered</param>
        public ReplyParameters(Message message)
        {
            MessageId = message.Id;
            ChatId = message.Chat.Id;
        }

        #region FromJSON
        /// <summary>
        /// Create a ReplyParameters from a JsonDocument
        /// </summary>
        /// <param name="jsonDocument">JsonDocument</param>
        /// <returns>ReplyParameters</returns>
        public static ReplyParameters FromJSON(JsonDocument jsonDocument)
        {
            JsonElement json = jsonDocument.RootElement;

            ReplyParameters replyParameters = new()
            {
                MessageId = json.GetProperty("message_id").GetInt64(),
            };

            if (json.TryGetProperty("chat_id", out JsonElement chatId))
            {
                replyParameters.ChatId = chatId.GetInt64();
            }

            return replyParameters;
        }
        /// <summary>
        /// Create a ReplyParameters from a JsonElement
        /// </summary>
        /// <param name="rootJsonElement">Root JsonElement</param>
        /// <returns>ReplyParameters</returns>
        public static ReplyParameters FromJSON(JsonElement rootJsonElement)
        {
            return FromJSON(rootJsonElement.GetRawText());
        }
        /// <summary>
        /// Create a ReplyParameters from a json string
        /// </summary>
        /// <param name="jsonString">Json string</param>
        /// <returns>ReplyParameters</returns>
        public static ReplyParameters FromJSON(string jsonString)
        {
            return FromJSON(JsonDocument.Parse(jsonString));
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append("Message id: ");
            builder.Append(MessageId);
            if (ChatId != null)
            {
                builder.Append(", Chat id: ");
                builder.Append(ChatId);
            }
            return builder.ToString();
        }
    }
}
