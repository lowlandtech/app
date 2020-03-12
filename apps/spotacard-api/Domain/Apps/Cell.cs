using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Spotacard.Domain
{
    public class Cell
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(
            maximumLength: 100,
            MinimumLength = 2,
            ErrorMessage = "Cell name must be between 2 and 100 characters long"
        )]
        public string Name { get; set; }
        public string Area { get; set; }
        public Guid? FieldId { get; set; }
        [JsonIgnore]
        public Field Field { get; set; }
        public Guid? WidgetId { get; set; }
        [JsonIgnore]
        public Widget Widget { get; set; }
        public Guid PageId { get; set; }
        [JsonIgnore]
        public Page Page { get; set; }
    }
}
