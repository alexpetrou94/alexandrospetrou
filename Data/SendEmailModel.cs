using System.ComponentModel.DataAnnotations;

namespace alexandrospetrou.Data {
    public class SendEmailModel {
        
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Subject { get; set; }

        [Required]
        public string? Message { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}