using Telegram_bots;
using Telegram_bots.Keyboards;

namespace MyTGBot
{
    class Test
    {
        public static async Task Main()
        {
            TelegramBot bot = new("7509560384:AAFuV9kHTTL2Y1eTA3OlkvcmCmNgwMOxfQ0");

            bot += OnUpdate;
            await bot.StartPolling();

            Console.ReadLine();
        }

        private static async Task OnUpdate(Update update, TelegramBot bot)
        {
            switch (update.Type)
            {
                case Update.Types.Message:
                    {
                        if (update.Message!.Text == null) return;
                        if (update.Message!.Text.Equals("g"))
                        {
                            await bot.SendMessage("Hello",
                                update.Message!.Chat.Id,
                                keyboard: new ReplyKeyboardRemove());
                            return;
                        }
                        ReplyKeyboard keyboard = new([
                            [new("a"), new("b")],
                            [new("c"), new("d")],
                            [new("e"), new("f"), new("g")],
                            [new("h"), new("i"), new("j")],
                        ]);
                        await bot.SendMessage("Hello",
                            update.Message!.Chat.Id,
                            keyboard: keyboard);
                        break;
                    }
            }
        }
    }
}
