
namespace Telegram_bots.Keyboards
{
    /// <summary>
    /// Keyboard
    /// </summary>
    public interface IKeyboard
    {
        /// <summary>
        /// Keyboard's buttons
        /// </summary>
        public IEnumerable<IEnumerable<IKeyboardButton>> Keyboard { get; set; }
    }
}