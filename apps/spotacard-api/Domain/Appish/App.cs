using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Spotacard.Core.Enums;

namespace Spotacard.Domain
{
    public class App
    {
        public App()
        {
            Card = new Card
            {
                Type = CardTypes.App
            };
        }

        [Key]
        [ForeignKey("Card")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(
            maximumLength: 100,
            MinimumLength = 2,
            ErrorMessage = "App name must be between 2 and 100 characters long"
        )]
        public string Name { get; set; }
        [Required]
        [StringLength(
            maximumLength: 50,
            MinimumLength = 1,
            ErrorMessage = "Organization title must be between 1 and 50 characters long"
        )]
        public string Organization { get; set; }
        [Required]
        [StringLength(
            maximumLength: 10,
            MinimumLength = 1,
            ErrorMessage = "Organization prefix must be between 1 and 10 characters long"
        )]
        public string Prefix { get; set; }
        public string Namingspace { get; set; }
        [JsonIgnore]
        public List<Table> Tables { get; set; }
        [JsonIgnore]
        public List<Page> Pages { get; set; }
        [JsonIgnore]
        public Card Card { get; set; }
    }

    public partial class Card
    {
        [InverseProperty("Card")]
        public App App { get; set; }
    }
}
