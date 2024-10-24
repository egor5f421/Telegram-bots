using System.Text.Json;

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
        /// Token for the bot
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
        /// An object for network access
        /// </summary>
        protected HttpClient httpClient;
        #endregion

        #region Constructor
        /// <summary>
        /// A constructor for creating a bot
        /// </summary>
        /// <param name="Token">Token for the bot</param>
        /// <exception cref="Exceptions.IncorrectRequestException">Called if you entered the wrong token</exception>
        /// <example>
        /// <code>
        /// public static async Task Main(string[] args)
        /// {
        ///     using TelegramBot bot = new (args[0]);
        /// }
        /// </code>
        /// </example>
        public TelegramBot(string Token)
        {
            this.Token = Token
            httpClient = new()
            {
                BaseAddress = new Uri("https://api.telegram.org/bot" + Token + "/")
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
        /// <param name="Text">The text of the message</param>
        /// <param name="ChatId">Chat_id of the chat to send the message to</param>
        /// <exception cref="ArgumentNullException">It is thrown if one of the arguments is null</exception>
        /// <exception cref="Exceptions.IncorrectRequestException">It is thrown if an incorrect request was made</exception>
        /// <returns>The message that was sent</returns>
        public async Task<Message> SendMessage(string Text, long ChatId)
        {
            ArgumentNullException.ThrowIfNull(Text);
            ArgumentNullException.ThrowIfNull(ChatId);

            Dictionary<string, string?> contentData = [];

            contentData["text"] = Text.ToString();
            contentData["chat_id"] = ChatId.ToString();

            HttpContent content = new FormUrlEncodedContent(contentData);

            HttpResponseMessage response = await httpClient.PostAsync("sendMessage", content);
            string jsonString = await response.Content.ReadAsStringAsync();

            JsonDocument json = JsonDocument.Parse(jsonString);

            Exceptions.IncorrectRequestException.ThrowIfNotOk(json);

            Message message = Message.FromJSON(json.RootElement.GetProperty("result"));

            return message;
        }
        /// <summary>
        /// Sends a message to the chat from which the latest update was received
        /// </summary>
        /// <param name="Text">The text of the message</param>
        /// <exception cref="ArgumentNullException">It is thrown if one of the arguments is null</exception>
        /// <exception cref="Exceptions.IncorrectRequestException">It is thrown if an incorrect request was made</exception>
        /// <returns>The message that was sent</returns>
        public async Task<Message> SendMessage(string Text)
        {
            return await SendMessage(Text, lastChatId);
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
        public delegate void UpdateHandler(Update update, TelegramBot bot);
        /// <summary>
        /// Called if an update has arrived
        /// </summary>
        public event UpdateHandler? OnUpdate;
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// Release the resources occupied by the bot
        /// </summary>
        /// <param name="disposing">Release managed resources</param>
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

        /// <summary>
        /// Release unmanaged resources occupied by the bot
        /// </summary>
        ~TelegramBot()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// Release the resources occupied by the bot
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
