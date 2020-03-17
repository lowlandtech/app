using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Spotacard.Domain
{
    public class Content
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(
            maximumLength: 100,
            MinimumLength = 2,
            ErrorMessage = "Content name must be between 2 and 100 characters long"
        )]
        public string Name { get; set; }
        public int Index { get; set; }
        public string Text { get; set; }
        public string Data { get; set; }
        public string Example { get; set; }
        public string FileName { get; set; }
        [ForeignKey("Card")]
        public Guid? CardId { get; set; }
        [JsonIgnore]
        public Card Card { get; set; }
    }
}
