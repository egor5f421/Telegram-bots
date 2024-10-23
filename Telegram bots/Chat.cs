using System.Text.Json;

namespace Telegram_bots
{
    /// <summary>
    /// The object providing the chat
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// Chat Types
        /// </summary>
        public enum Types
        {
            /// <summary>
            /// Private chat
            /// </summary>
            Private,
            /// <summary>
            /// Group
            /// </summary>
            Group,
            /// <summary>
            /// Supergroup
            /// </summary>
            Supergroup,
            /// <summary>
            /// Channel
            /// </summary>
            Channel,
        }

        /// <summary>
        /// Chat id
        /// </summary>
        public required long Id { get; init; }
        /// <summary>
        /// Chat Type
        /// </summary>
        public required Types Type { get; init; }
        /// <summary>
        /// Chat Title
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Chat`s first name
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Chat's last name
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Chat username
        /// </summary>
        public string? Username { get; set; }

        #region FromJSON
        /// <summary>
        /// Make a chat from JsonDocument
        /// </summary>
        /// <param name="jsonDocument">JsonDocument</param>
        /// <returns>Chat</returns>
        /// <exception cref="ArgumentException">Called if some argument is incorrect</exception>
        public static Chat FromJSON(JsonDocument jsonDocument)
        {
            JsonElement json = jsonDocument.RootElement;

            Chat chat = new()
            {
                Id = json.GetProperty("id").GetInt64(),
                Type = json.GetProperty("type").GetString() switch
                {
                    "private" => Types.Private,
                    "group" => Types.Group,
                    "supergroup" => Types.Supergroup,
                    "channel" => Types.Channel,
                    _ => throw new ArgumentException("Invalid \"type\" argument")
                }
            };

            if (json.TryGetProperty("title", out JsonElement Title))
            {
                chat.Title = Title.GetString();
            }
            if (json.TryGetProperty("first_name", out JsonElement FirstName))
            {
                chat.FirstName = FirstName.GetString();
            }
            if (json.TryGetProperty("last_name", out JsonElement LastName))
            {
                chat.LastName = LastName.GetString();
            }
            if (json.TryGetProperty("username", out JsonElement Username))
            {
                chat.Username = Username.GetString();
            }

            return chat;
        }
        /// <summary>
        /// Make a chat from JsonElement
        /// </summary>
        /// <param name="rootJsonElement">JsonElement</param>
        /// <returns>Chat</returns>
        public static Chat FromJSON(JsonElement rootJsonElement)
        {
            return FromJSON(rootJsonElement.GetRawText());
        }
        /// <summary>
        /// Make a chat from a json string
        /// </summary>
        /// <param name="jsonString">Json string</param>
        /// <returns>Chat</returns>
        public static Chat FromJSON(string jsonString)
        {
            return FromJSON(JsonDocument.Parse(jsonString));
        }
        #endregion

        /// <summary>
        /// Turns it into a string
        /// </summary>
        /// <returns>A string representing the chat</returns>
        public override string ToString()
        {
            string str = string.Empty;
            str += "Id: ";
            str += Id;
            str += ", Type: ";
            str += Type;
            if (Title != null)
            {
                str += ", Title: ";
                str += Title;
            }
            if (FirstName != null)
            {
                str += ", First name: ";
                str += FirstName;
            }
            if (LastName != null)
            {
                str += ", Last name: ";
                str += LastName;
            }
            if (Username != null)
            {
                str += ", Username: ";
                str += Username;
            }
            return str;
        }
    }

}
