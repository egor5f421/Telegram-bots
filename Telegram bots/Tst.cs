using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram_bots;

namespace MyTGBot
{
    class Tst
    {
        public static async Task Main(string[] args)
        {
            using TelegramBot bot = new(args[0]);

            bot.OnUpdate += Update;
            await bot.StartPolling();
        }

        public static async Task Update(Update update, TelegramBot bot)
        {
            if (update.Message != null)
            {
                if (update.Message.Text != null)
                {
                    await bot.SendMessage(update.Message.Text);
                }
            }
        }
    }
}
