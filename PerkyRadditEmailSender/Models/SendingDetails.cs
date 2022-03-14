using System.ComponentModel.DataAnnotations;

namespace PerkyRadditEmailSender.Models
{
    public class SendingDetails
    {
        [MaxLength(100)]
        public string Subject { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string To { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Bcc { get; set; }
        [DataType(DataType.EmailAddress)]
        public string CC { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Message { get; set; }

    }
}
