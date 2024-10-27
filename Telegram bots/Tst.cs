using Telegram_bots;
using Telegram_bots.Keyboards;

namespace MyTGBot
{
    class Tst
    {
        public static async Task Main(string[] args)
        {
            TelegramBot bot = new(args[0]);

            bot += Update;
            await bot.StartPolling();

            Console.ReadLine();
        }

        private static async Task Update(Update update, TelegramBot bot)
        {
            if (update.Message != null)
            {
                if (update.Message.Text != null)
                {
                    if (update.Message.ReplyMessage != null)
                    {
                        if (update.Message.ReplyMessage.Text != null)
                        {
                            
                        }
                    }
                }
            }
        }
    }
}
