using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Telegram_bots
{
    /// <summary>
    /// This class is a telegram bot
    /// </summary>
    public class TelegramBot : IDisposable
    {
        #region Fields
        private bool disposedValue;

        /// <summary>
        /// token for the bot
        /// </summary>
        protected readonly string Token = string.Empty;

        /// <summary>
        /// Chat_id from the last message
        /// </summary>
        protected long lastChatId;

        /// <summary>
        /// Update_id from the last update
        /// </summary>
        protected long lastUpdateId;

        /// <summary>
        /// Callback_query_id from the last update.callback_query
        /// </summary>
        protected string lastCallbackQueryId = string.Empty;

        /// <summary>
        /// An object for network access
        /// </summary>
        protected HttpClient httpClient;
        #endregion

        #region Constructor
        /// <summary>
        /// A constructor for creating a bot
        /// </summary>
        /// <param name="token">token for the bot</param>
        /// <exception cref="Exceptions.IncorrectRequestException">Called if you entered the wrong token</exception>
        /// <example>
        /// <code>
        /// public static async Task Main(string[] args)
        /// {
        ///     using TelegramBot bot = new (args[0]);
        /// }
        /// </code>
        /// </example>
        public TelegramBot(string token)
        {
            Token = token;
            httpClient = new()
            {
                BaseAddress = new Uri("https://api.telegram.org/bot" + token + "/")
            };

            JsonDocument me = GetMe().Result;
            Exceptions.IncorrectRequestException.ThrowIfNotOk(me);
        }
        #endregion

        #region GetMe
        /// <summary>
        /// Receives information about the bot
        /// </summary>
        /// <exception cref="Exceptions.IncorrectRequestException">It is thrown if an incorrect request was made</exception>
        /// <returns>Information about the bot</returns>
        public async Task<JsonDocument> GetMe()
        {
            HttpContent response = (await httpClient.GetAsync("getMe")).Content;
            string stream = await response.ReadAsStringAsync();

            JsonDocument json = JsonDocument.Parse(stream);

            Exceptions.IncorrectRequestException.ThrowIfNotOk(json);

            return json;
        }
        #endregion

        #region SendMessage
        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="messageText">The Text of the message</param>
        /// <param name="chatId">The chat_id to which the message will be sent. Do not use if you want to use the chat_id from the latest update</param>
        /// <param name="replyParameters">Parameters for responding to a message</param>
        /// <param name="keyboard">Keyboard</param>
        /// <exception cref="ArgumentNullException">It is thrown if one of the arguments is null</exception>
        /// <exception cref="Exceptions.IncorrectRequestException">It is thrown if an incorrect request was made</exception>
        /// <returns>The message that was sent</returns>
        public async Task<Message> SendMessage(object messageText,
            long? chatId = null,
            ReplyParameters? replyParameters = null,
            Keyboards.IKeyboard? keyboard = null)
        {
            string? text = messageText.ToString();
            ArgumentNullException.ThrowIfNull(text);
            chatId ??= lastChatId;

            Dictionary<string, string?> contentData = [];

            contentData["text"] = text.ToString();
            contentData["chat_id"] = chatId.ToString();
            if (replyParameters != null)
            {
                contentData["reply_parameters"] = JsonSerializer.Serialize(replyParameters);
            }
            if (keyboard != null)
            {
                contentData["reply_markup"] = JsonSerializer.Serialize(keyboard, keyboard.GetType());
            }

            HttpContent content = new FormUrlEncodedContent(contentData);

            HttpResponseMessage response = await httpClient.PostAsync("sendMessage", content);
            string jsonString = await response.Content.ReadAsStringAsync();

            JsonDocument json = JsonDocument.Parse(jsonString);

            Exceptions.IncorrectRequestException.ThrowIfNotOk(json);

            Message message = Message.FromJSON(json.RootElement.GetProperty("result"));

            return message;
        }
        #endregion

        #region DeleteMessage
        /// <summary>
        /// Use this method to delete a message
        /// </summary>
        /// <param name="messageId">Identifier of the message to delete</param>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <exception cref="ArgumentNullException">It is thrown if one of the arguments is null</exception>
        /// <exception cref="Exceptions.IncorrectRequestException">It is thrown if an incorrect request was made</exception>
        /// <returns>True on success</returns>
        public async Task<bool> DeleteMessage(long messageId, long? chatId = null)
        {
            ArgumentNullException.ThrowIfNull(messageId);

            chatId ??= lastChatId;

            Dictionary<string, string?> contentData = [];

            contentData["chat_id"] = chatId.ToString();
            contentData["message_id"] = messageId.ToString();

            HttpContent content = new FormUrlEncodedContent(contentData);

            HttpResponseMessage response = await httpClient.PostAsync("deleteMessage", content);
            string jsonString = await response.Content.ReadAsStringAsync();

            JsonDocument json = JsonDocument.Parse(jsonString);

            Exceptions.IncorrectRequestException.ThrowIfNotOk(json);

            bool success = json.RootElement.GetProperty("result").GetBoolean();

            return success;
        }
        #endregion

        #region EditMessageText
        /// <summary>
        /// Use this method to edit text messages
        /// </summary>
        /// <param name="messageText">New text of the message</param>
        /// <param name="messageId">Identifier of the message to edit</param>
        /// <param name="chatId">Unique identifier for the target chat</param>
        /// <param name="keyboard">Inline keyboard</param>
        /// <exception cref="ArgumentNullException">It is thrown if one of the arguments is null</exception>
        /// <exception cref="Exceptions.IncorrectRequestException">It is thrown if an incorrect request was made</exception>
        /// <returns></returns>
        public async Task<Message> EditMessageText(object messageText,
            long messageId,
            long? chatId = null,
            Keyboards.InlineKeyboard? keyboard = null)
        {
            ArgumentNullException.ThrowIfNull(messageText);
            ArgumentNullException.ThrowIfNull(messageId);

            chatId ??= lastChatId;

            Dictionary<string, string?> contentData = [];

            contentData["text"] = messageText.ToString();
            contentData["message_id"] = messageId.ToString();
            contentData["chat_id"] = chatId.ToString();
            if (keyboard != null)
            {
                contentData["reply_markup"] = JsonSerializer.Serialize(keyboard, typeof(Keyboards.InlineKeyboard));
            }

            HttpContent content = new FormUrlEncodedContent(contentData);

            HttpResponseMessage response = await httpClient.PostAsync("editMessageText", content);
            string jsonString = await response.Content.ReadAsStringAsync();

            JsonDocument json = JsonDocument.Parse(jsonString);

            Exceptions.IncorrectRequestException.ThrowIfNotOk(json);

            Message message = Message.FromJSON(json.RootElement.GetProperty("result"));

            return message;
        }
        #endregion

        #region AnswerCallbackQuery
        /// <summary>
        /// This method must be called after receiving the callback query request.
        /// </summary>
        /// <param name="text">The text in the window. Which will appear to the user.</param>
        /// <param name="callbackQueryId">Callback query id</param>
        /// <param name="showAlert">Should I display the text in a regular window or in a modal window?</param>
        /// <returns>Has the function been completed successfully or not</returns>
        public async Task<bool> AnswerCallbackQuery(string? text = null,
            string? callbackQueryId = null,
            bool showAlert = false)
        {
            callbackQueryId ??= lastCallbackQueryId;

            Dictionary<string, string> keyValuePairs = new()
            {
                ["callback_query_id"] = callbackQueryId,
                ["show_alert"] = showAlert.ToString().ToLower(),
            };
            if (text != null)
            {
                keyValuePairs["text"] = text;
            }

            HttpContent content = new FormUrlEncodedContent(keyValuePairs);
            HttpResponseMessage response = await httpClient.PostAsync("answerCallbackQuery", content);
            string jsonString = await response.Content.ReadAsStringAsync();

            JsonDocument json = JsonDocument.Parse(jsonString);

            Exceptions.IncorrectRequestException.ThrowIfNotOk(json);

            JsonElement root = json.RootElement;
            JsonElement result = root.GetProperty("result");

            return result.GetBoolean();
        }
        #endregion

        #region StartPolling
        /// <summary>
        /// Start polling
        /// </summary>
        /// <exception cref="Exceptions.IncorrectRequestException">It is thrown if an incorrect request was made</exception>
        /// <returns></returns>
        public async Task StartPolling()
        {
            while (true)
            {
                HttpContent content = new FormUrlEncodedContent([new("offset", (lastUpdateId + 1).ToString())]);
                HttpResponseMessage response = await httpClient.PostAsync("getUpdates", content);
                string jsonString = await response.Content.ReadAsStringAsync();

                JsonDocument json = JsonDocument.Parse(jsonString);

                Exceptions.IncorrectRequestException.ThrowIfNotOk(json);

                JsonElement root = json.RootElement;
                JsonElement result = root.GetProperty("result");

                if (result.GetRawText() != "[]")
                {
                    foreach (var item in result.EnumerateArray())
                    {
                        Update update = Update.FromJSON(item);
                        if (update.Message != null)
                        {
                            lastChatId = update.Message.Chat.Id;
                        } else if (update.CallbackQuery != null)
                        {
                            lastChatId = update.CallbackQuery.From.Id;
                            lastCallbackQueryId = update.CallbackQuery.Id;
                        }
                        if (update.UpdateId > lastUpdateId) lastUpdateId = update.UpdateId;
                        OnUpdate?.Invoke(update, this);
                    }
                }
                await Task.Delay(1000);
            }
        }
        #endregion

        #region OnUpdate event
        /// <summary>
        /// Signature of the "OnUpdate" event
        /// </summary>
        /// <param name="update">The latest update</param>
        /// <param name="bot">Telegram bot</param>
        public delegate Task UpdateHandler(Update update, TelegramBot bot);
        /// <summary>
        /// Called if an update has arrived
        /// </summary>
        public event UpdateHandler? OnUpdate;
        #endregion

        #region +, - operators
        /// <summary>
        /// Add the <seealso cref="OnUpdate"/> event function
        /// </summary>
        /// <param name="bot">Telegram bot</param>
        /// <param name="function">Function</param>
        /// <returns>Telegram bot</returns>
        public static TelegramBot operator +(TelegramBot bot, UpdateHandler function)
        {
            bot.OnUpdate += function;
            return bot;
        }

        /// <summary>
        /// Remove the <seealso cref="OnUpdate"/> event function
        /// </summary>
        /// <param name="bot">Telegram bot</param>
        /// <param name="function">Function</param>
        /// <returns>Telegram bot</returns>
        public static TelegramBot operator -(TelegramBot bot, UpdateHandler function)
        {
            bot.OnUpdate -= function;
            return bot;
        }
        #endregion

        #region IDisposable implementation
        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: освободить управляемое состояние (управляемые объекты)
                }

                httpClient.Dispose();
                disposedValue = true;
            }
        }

        /// <inheritdoc/>
        ~TelegramBot()
        {
            Dispose(disposing: false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
