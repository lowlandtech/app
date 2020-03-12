using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spotacard.Domain
{
    public class App
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
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
    }
}
