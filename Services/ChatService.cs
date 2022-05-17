using alexandrospetrou.Data;

namespace alexandrospetrou.Services {
    public class ChatService: IDisposable {

        public List<ChatMessageModel> Messages = new List<ChatMessageModel>();
        public string SessionName { get; private set; } = "";
        public event Action? MessageListChanged;

        private TelegramBotService botService { get; set; }
        
        public ChatService(TelegramBotService bot) {
            botService = bot;
            SessionName = createSessionName();
            botService.MessageReceived += OnMessageReceived;
        }

        public void OnMessageReceived(object? sender, MessageReceivedEventArgs args) {
            (string, string) messageParts = parseMessage(args.Message);
            
            if(isCommand(args.Message) || !isValidMessage(messageParts)) {
                return;
            }
            
            ChatMessageModel messageModel = new ChatMessageModel {
                Message = messageParts.Item2,
                Time = TimeOnly.FromDateTime(DateTime.Now),
                Name = "Alexandros",
                IsSender = false
            };
                    
            Messages.Add(messageModel);
            onMessageListChanged();
        }

        public async void SendMessage(string message) {
            if(message == "") {
                return;
            }

            bool isMessageSent = await botService.SendMessage($"{SessionName}: {message}");

            if(isMessageSent) {
                ChatMessageModel messageModel = new ChatMessageModel {
                    Message = message,
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    Name = SessionName,
                    IsSender = true
                };

                Messages.Add(messageModel);
                onMessageListChanged();
            }
        }

        public void Dispose() {
            botService.MessageReceived -= OnMessageReceived;
        }

        private bool isValidMessage((string, string) messageParts) {
            return messageParts.Item1 == SessionName && messageParts.Item2 != "";
        }

        private bool isCommand(string message) {
            return message.Trim().StartsWith("/");
        }

        private (string, string) parseMessage(string message) {
            string[] messageParts = message.Trim().Split(':');

            if(messageParts.Length == 2) {
                return (messageParts[0].Trim(), messageParts[1].Trim());
            }

            else if(messageParts.Length > 2) {
                string combinedMessage = "";

                for(int i = 1; i < messageParts.Length; i++) {
                    combinedMessage += messageParts[i];
                }

                return (messageParts[0].Trim(), combinedMessage.Trim());
            }

            return ("", "");
        }

        private void onMessageListChanged() {
            MessageListChanged?.Invoke();
        }

        private string createSessionName() {
            Random rng = new Random();
            string[] animals = { "dog", "cat", "bird", "fish", "lizard", "turtle", "doe", "horse", "panda", "zebra" };
            string choosenAnimal = animals[rng.Next(animals.Length)];
            string choosenNumber = rng.Next(1, 100).ToString();
            
            return $"{choosenAnimal}#{choosenNumber}";
        }
    }
}