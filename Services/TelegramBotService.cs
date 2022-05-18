using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace alexandrospetrou.Services {
    public class TelegramBotService: IDisposable {

        public event EventHandler<MessageReceivedEventArgs>? MessageReceived ;
        
        private TelegramBotClient bot;
        private CancellationTokenSource cts;
        private IConfiguration configuration;

        public TelegramBotService(IConfiguration config) {
            configuration = config;
            bot = new TelegramBotClient(configuration["telegram-apikey"]);
            cts = new CancellationTokenSource();

            ReceiverOptions receiverOptions = new ReceiverOptions {
                AllowedUpdates = new UpdateType[] {
                    UpdateType.Message,
                    UpdateType.EditedMessage
                }
            };

            bot.StartReceiving(updateHandler, errorHandler, receiverOptions, cts.Token);
        }

        public async Task<bool> SendMessage(string message) {
            
            Message msgResult = await bot.SendTextMessageAsync(configuration["telegram-chatid"], message);
            return msgResult.Text == message;
        }

        public void Dispose() {
            cts.Cancel();
            cts.Dispose();
        }

        private Task errorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }

        private Task updateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) {
            if(update.Type == UpdateType.Message) {
                string msg = update.Message?.Text == null ? "" : update.Message.Text;
                MessageReceivedEventArgs eventArgs = new MessageReceivedEventArgs();
                eventArgs.Message = msg;
                OnMessageReceived(this, eventArgs);

                return Task.CompletedTask;
            }

            else if(update.Type == UpdateType.EditedMessage) {
                string msg = update.EditedMessage?.Text == null ? "" : update.EditedMessage.Text;
                MessageReceivedEventArgs eventArgs = new MessageReceivedEventArgs();
                eventArgs.Message = msg;
                eventArgs.Edited = true;
                OnMessageReceived(this, eventArgs);

                return Task.CompletedTask;
            }

            Console.WriteLine("Unknown message type recieved.");
            
            return Task.CompletedTask;
        }

        protected virtual void OnMessageReceived(object? sender, MessageReceivedEventArgs args) {
            MessageReceived?.Invoke(sender, args);
        }
    }

    public class MessageReceivedEventArgs: EventArgs {
        public string Message { get; set; } = "";
        public bool Edited { get; set; }
    }
}