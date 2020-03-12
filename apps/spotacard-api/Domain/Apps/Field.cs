using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Spotacard.Core.Enums;

namespace Spotacard.Domain
{
    public class Field
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(
            maximumLength: 100,
            MinimumLength = 2,
            ErrorMessage = "Field name must be between 2 and 100 characters long"
        )]
        public string Name { get; set; }
        public FieldTypes Type { get; set; }
        public int Index { get; set; }
        public Guid? WidgetId { get; set; }
        [JsonIgnore]
        public Widget Widget { get; set; }
        public Guid TableId { get; set; }
        [JsonIgnore]
        public Table Table { get; set; }
        [JsonIgnore]
        public List<Cell> Cells { get; set; }

        
    }
}
