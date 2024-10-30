using System.Text;
using System.Text.Json;
using Telegram_bots.Keyboards;

namespace Telegram_bots
{
    /// <summary>
    /// The class representing the message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Message ID
        /// </summary>
        public required long Id { get; init; }
        /// <summary>
        /// Date the message was sent
        /// </summary>
        public required int Datetime { get; init; }
        /// <summary>
        /// The chat to which the message was sent
        /// </summary>
        public required Chat Chat { get; init; }

        /// <summary>
        /// The Text of the message
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// The sender of the message
        /// </summary>
        public User? From { get; set; }
        /// <summary>
        /// Parameters for responding to a message
        /// </summary>
        public ReplyParameters? ReplyParameters { get; set; }
        /// <summary>
        /// The message that this message responds to
        /// </summary>
        public Message? ReplyMessage { get; set; }

        #region bool Equals(object? obj)
        /// <summary>
        /// Checks if the objects are the same
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Are the objects the same</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Id.Equals(((Message)obj).Id);
            //return base.Equals(obj);
        }
        #endregion

        #region FromJSON
        /// <summary>
        /// Create a message from a JsonDocument
        /// </summary>
        /// <param name="jsonDocument">JsonDocument</param>
        /// <returns>Message</returns>
        public static Message FromJSON(JsonDocument jsonDocument)
        {
            JsonElement json = jsonDocument.RootElement;

            Message message = new()
            {
                Id = json.GetProperty("message_id")!.GetInt64(),
                Datetime = json.GetProperty("date")!.GetInt32(),
                Chat = Chat.FromJSON(json.GetProperty("chat")),
            };

            if (json.TryGetProperty("text", out JsonElement messageText))
            {
                message.Text = messageText.GetString();
            }
            if (json.TryGetProperty("from", out JsonElement from))
            {
                message.From = User.FromJSON(from);
            }
            if (json.TryGetProperty("reply_to_message", out JsonElement replyMessage))
            {
                message.ReplyMessage = FromJSON(replyMessage);
            }
            if (json.TryGetProperty("reply_parameters", out JsonElement replyParameters))
            {
                message.ReplyParameters = ReplyParameters.FromJSON(replyParameters);
            }

            return message;
        }
        /// <summary>
        /// Create a message from a JsonElement
        /// </summary>
        /// <param name="rootJsonElement">Root JsonElement</param>
        /// <returns>Message</returns>
        public static Message FromJSON(JsonElement rootJsonElement)
        {
            return FromJSON(rootJsonElement.GetRawText());
        }
        /// <summary>
        /// Create a message from a Json string
        /// </summary>
        /// <param name="jsonString">Json string</param>
        /// <returns>Message</returns>
        public static Message FromJSON(string jsonString)
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
            builder.Append(", Date: ");
            builder.Append(Datetime);
            builder.Append(", Chat: (");
            builder.Append(Chat);
            builder.Append(')');
            if (Text != null)
            {
                builder.Append(", Text: ");
                builder.Append(Text);
            }
            if (From != null)
            {
                builder.Append(", From: (");
                builder.Append(From);
                builder.Append(')');
            }
            if (ReplyParameters != null)
            {
                builder.Append(", Reply parameters: (");
                builder.Append(ReplyParameters);
                builder.Append(')');
            }
            if (ReplyMessage != null)
            {
                builder.Append(", Reply to message: (");
                builder.Append(ReplyMessage);
                builder.Append(')');
            }
            return builder.ToString();
        }

        /// <summary>
        /// Returns the hash code of the message
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
