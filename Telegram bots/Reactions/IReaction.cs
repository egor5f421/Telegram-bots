using System.Text.Json.Serialization;

namespace Telegram_bots.Reactions
{
    /// <summary>
    /// This object describes the type of a reaction
    /// </summary>
    public interface IReaction
    {
        /// <summary>
        /// Type of the reaction.
        /// </summary>
        [JsonPropertyName("type")]
        string Type { get; }
    }
}