using System.Text.Json.Serialization;

namespace Telegram_bots.Reactions
{
    /// <summary>
    /// The reaction is based on an emoji.
    /// </summary>
    [Serializable]
    public class EmojiReaction : IReaction
    {
        /// <inheritdoc/>
        /// <remarks>
        /// Always “emoji”
        /// </remarks>
        [JsonPropertyName("type")]
        public string Type { get; } = "emoji";

        /// <summary>
        /// Reaction emoji.
        /// </summary>
        [JsonPropertyName("emoji")]
        public string Emoji { get; init; }

        /// <summary>
        /// Creates a reaction
        /// </summary>
        /// <param name="emoji">Reaction emoji. Currently, it can be one of "👍", "👎", "❤", "🔥", "🥰", "👏", "😁", "🤔", "🤯", "😱", "🤬", "😢", "🎉", "🤩", "🤮", "💩", "🙏", "👌", "🕊", "🤡", "🥱", "🥴", "😍", "🐳", "🌚", "🌭", "💯", "🤣", "⚡", "🍌", "🏆", "💔", "🤨", "😐", "🍓", "🍾", "💋", "🖕", "😈", "😴", "😭", "🤓", "👻", "👨‍💻", "👀", "🎃", "🙈", "😇", "😨", "🤝", "✍", "🤗", "🫡", "🎅", "🎄", "☃", "💅", "🤪", "🗿", "🆒", "💘", "🙉", "🦄", "😘", "💊", "🙊", "😎", "👾", "🤷", "😡"</param>
        public EmojiReaction(string emoji)
        {
            Emoji = emoji;
        }
    }
}
