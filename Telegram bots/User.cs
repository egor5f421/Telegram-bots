using System.Text.Json;

namespace Telegram_bots
{
    /// <summary>
    /// A class representing the user
    /// </summary>
    public class User
    {
        /// <summary>
        /// User ID
        /// </summary>
        public required long Id { get; init; }
        /// <summary>
        /// Is the user a bot?
        /// </summary>
        public required bool IsBot { get; init; }
        /// <summary>
        /// First name`s user
        /// </summary>
        public required string FirstName { get; init; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// A premium user?
        /// </summary>
        public bool? IsPremium { get; set; }

        #region FromJSON
        /// <summary>
        /// Get an update from a JsonDocument
        /// </summary>
        /// <param name="jsonDocument">JsonDocument</param>
        /// <returns>User</returns>
        public static User FromJSON(JsonDocument jsonDocument)
        {
            JsonElement json = jsonDocument.RootElement;

            User user = new()
            {
                Id = json.GetProperty("id").GetInt64(),
                FirstName = json.GetProperty("first_name").GetString()!,
                IsBot = json.GetProperty("is_bot").GetBoolean()
            };

            if (json.TryGetProperty("is_premium", out JsonElement IsPremium))
            {
                user.IsPremium = IsPremium.GetBoolean();
            }
            if (json.TryGetProperty("last_name", out JsonElement LastName))
            {
                user.LastName = LastName.GetString();
            }
            if (json.TryGetProperty("username", out JsonElement Username))
            {
                user.Username = Username.GetString();
            }

            return user;
        }
        /// <summary>
        /// Get an update from a JsonElement
        /// </summary>
        /// <param name="rootJsonElement">Root JsonElement</param>
        /// <returns>User</returns>
        public static User FromJSON(JsonElement rootJsonElement)
        {
            return FromJSON(rootJsonElement.GetRawText());
        }
        /// <summary>
        /// Get an update from a json string
        /// </summary>
        /// <param name="jsonString">Json string</param>
        /// <returns>User</returns>
        public static User FromJSON(string jsonString)
        {
            return FromJSON(JsonDocument.Parse(jsonString));
        }
        #endregion

        /// <summary>
        /// Turns it into a string
        /// </summary>
        /// <returns>The line representing the user</returns>
        public override string ToString()
        {
            string str = "Id: ";
            str += Id;
            str += ", Is bot: ";
            str += IsBot;
            str += ", First name: ";
            str += FirstName;
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
            if (IsPremium != null)
            {
                str += ", Is premium: ";
                str += IsPremium;
            }
            return str;
        }
    }

}
