using System.Text;
using System.Text.Json;

namespace Telegram_bots
{
    /// <summary>
    /// A class representing Update
    /// </summary>
    public class Update
    {
        /// <summary>
        /// Update ID
        /// </summary>
        public required long UpdateId { get; init; }

        /// <summary>
        /// Message
        /// </summary>
        public Message? Message { get; set; }

        #region bool Equals(object? obj)
        /// <summary>
        /// Checks the similarity of objects
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>The similarity of objects</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return UpdateId.Equals(((Update)obj).UpdateId);
            //return base.Equals(obj);
        }
        #endregion

        #region FromJSON
        /// <summary>
        /// Get an update from a JsonDocument
        /// </summary>
        /// <param name="jsonDocument">JsonDocument</param>
        /// <returns>Update</returns>
        public static Update FromJSON(JsonDocument jsonDocument)
        {
            JsonElement json = jsonDocument.RootElement;

            Update update = new()
            {
                UpdateId = json.GetProperty("update_id")!.GetInt64(),
            };

            if (json.TryGetProperty("message", out JsonElement message))
            {
                update.Message = Message.FromJSON(message);
            }

            return update;
        }
        /// <summary>
        /// Get an update from a JsonElement
        /// </summary>
        /// <param name="rootJsonElement">Root JsonElement</param>
        /// <returns>Update</returns>
        public static Update FromJSON(JsonElement rootJsonElement)
        {
            return FromJSON(rootJsonElement.GetRawText());
        }
        /// <summary>
        /// Get an update from a json string
        /// </summary>
        /// <param name="jsonString">Json string</param>
        /// <returns>Update</returns>
        public static Update FromJSON(string jsonString)
        {
            return FromJSON(JsonDocument.Parse(jsonString));
        }
        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append("Update id: ");
            builder.Append(UpdateId);
            if (Message != null)
            {
                builder.Append(", Message: (");
                builder.Append(Message);
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
