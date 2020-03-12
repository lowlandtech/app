using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spotacard.Domain
{
    public class Widget
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(
            maximumLength: 100,
            MinimumLength = 2,
            ErrorMessage = "Widget name must be between 2 and 100 characters long"
        )]
        public string Name { get; set; }
        public string Packages { get; set; }
        public string Wiring { get; set; }
        public string CodeBehind { get; set; }
        public string Styling { get; set; }
        public string Markup { get; set; }
        [JsonIgnore]
        public List<Field> Fields { get; set; }
        [JsonIgnore]
        public List<Cell> Cells { get; set; }
    }
}
