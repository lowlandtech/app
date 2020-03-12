using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spotacard.Domain
{
    public class Table
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(
            maximumLength: 100,
            MinimumLength = 2,
            ErrorMessage = "Table name must be between 2 and 100 characters long"
        )]
        public string Name { get; set; }
        public Guid AppId { get; set; }
        [JsonIgnore]
        public App App { get; set; }
        [JsonIgnore]
        public List<Field> Fields { get; set; }
        [JsonIgnore]
        public List<Page> Pages { get; set; }
    }
}
