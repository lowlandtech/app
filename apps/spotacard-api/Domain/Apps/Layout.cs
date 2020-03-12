using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spotacard.Domain
{
    public class Layout
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(
            maximumLength: 100,
            MinimumLength = 2,
            ErrorMessage = "Layout name must be between 2 and 100 characters long"
        )]
        public string Name { get; set; }
        public string CodeBehind { get; set; }
        public string Styling { get; set; }
        public string Markup { get; set; }
        public string Items { get; set; }
        [JsonIgnore]
        public List<Page> Pages { get; set; }
    }
}
