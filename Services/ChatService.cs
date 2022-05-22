using alexandrospetrou.Data;

namespace alexandrospetrou.Services {
    public class ChatService: IDisposable {

        public List<ChatMessageModel> Messages = new List<ChatMessageModel>();
        public string SessionName { get; private set; } = "";
        public event Action? MessageListChanged;

        private TelegramBotService botService { get; set; }
        private static HashSet<string> chatSessionNames = new HashSet<string>();
        private const string COULD_NOT_SEND_ERROR = "Could not send message, please try again.";

        public ChatService(TelegramBotService bot) {
            botService = bot;
            SessionName = createSessionName();
            chatSessionNames.Add(SessionName);
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
                MessageType = "right-msg"
            };

            if(args.Edited) {
                messageModel.Message += "*";
            }
                    
            Messages.Add(messageModel);
            onMessageListChanged();
        }

        public async void SendMessage(string message) {
            if(message == "") {
                return;
            }

            if(isCommand(message)) {
                handleCommand(message);
                return;
            }

            bool isMessageSent = await botService.SendMessage($"{SessionName}: {message}");

            ChatMessageModel messageModel = new ChatMessageModel {
                    Time = TimeOnly.FromDateTime(DateTime.Now),
                    Name = SessionName
            };

            messageModel.Message = isMessageSent ? message : COULD_NOT_SEND_ERROR;
            messageModel.MessageType = isMessageSent ? "left-msg" : "error-msg";
            
            Messages.Add(messageModel);
            onMessageListChanged();
        }

        public void Dispose() {
            botService.MessageReceived -= OnMessageReceived;
            chatSessionNames.Remove(SessionName);
        }

        private bool isValidMessage((string, string) messageParts) {
            return messageParts.Item1 == SessionName && messageParts.Item2 != "";
        }

        private bool isCommand(string message) {
            return message.Trim().StartsWith("/");
        }

        private void handleCommand(string command) {
            command = command.Trim().ToLower();
            
            if(command == "/clear") {
                Messages.Clear();
                onMessageListChanged();
            }
        }

        private (string, string) parseMessage(string message) {
            string[] messageParts = message.Trim().Split(':');

            if(messageParts.Length == 2) {
                return (messageParts[0].Trim(), messageParts[1].Trim());
            }

            else if(messageParts.Length > 2) {
                string combinedMessage = "";

                for(int i = 1; i < messageParts.Length; i++) {
                    if(i != 1) {
                        combinedMessage += ":" + messageParts[i];
                        continue;
                    }

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
            string sessionName = "";

            do {
                string choosenAnimal = animals[rng.Next(animals.Length)];
                string choosenNumber = rng.Next(1, 100).ToString();
                sessionName = $"{choosenAnimal}#{choosenNumber}";
            } while (chatSessionNames.Contains(sessionName));
            
            return sessionName;
        }
    }
}