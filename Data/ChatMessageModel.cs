using System.ComponentModel.DataAnnotations;

namespace alexandrospetrou.Data {
    public class ChatMessageModel {
        
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Message { get; set; }
        
        [Required]
        public TimeOnly Time { get; set; }

        [Required]
        public string? MessageType { get; set; }
    }
}