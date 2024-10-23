using System.Text.Json;

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
        public required Chat Chat { get; set; }

        /// <summary>
        /// The text of the message
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// The sender of the message
        /// </summary>
        public User? From { get; set; }

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
        /// Create a message from a json string
        /// </summary>
        /// <param name="jsonString">Json string</param>
        /// <returns>Message</returns>
        public static Message FromJSON(string jsonString)
        {
            return FromJSON(JsonDocument.Parse(jsonString));
        }
        #endregion

        /// <summary>
        /// Turns it into a string
        /// </summary>
        /// <returns>The string representing the message</returns>
        public override string ToString()
        {
            string str = "Id: ";
            str += Id;
            str += ", Date: ";
            str += Datetime;
            str += ", Chat: (";
            str += Chat;
            str += ")";
            if (Text != null)
            {
                str += ", Text: ";
                str += Text;
            }
            if (From != null)
            {
                str += ", From: (";
                str += From;
                str += ")";
            }
            return str;
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
