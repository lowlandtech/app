using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Spotacard.Domain
{
    public class Page
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(
            maximumLength: 100,
            MinimumLength = 2,
            ErrorMessage = "Page name must be between 2 and 100 characters long"
        )]
        public string Name { get; set; }
        public string Styling { get; set; }
        public string CodeBehind { get; set; }
        public string Markup { get; set; }
        public Guid AppId { get; set; }
        [JsonIgnore]
        public App App { get; set; }
        public Guid? TableId { get; set; }
        [JsonIgnore]
        public Table Table { get; set; }
        public Guid? LayoutId { get; set; }
        [JsonIgnore]
        public Layout Layout { get; set; }
        [JsonIgnore]
        public List<Cell> Cells { get; set; }
    }
}
