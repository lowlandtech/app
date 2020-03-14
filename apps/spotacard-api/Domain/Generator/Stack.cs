using Newtonsoft.Json;
using Spotacard.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spotacard.Domain
{
    public class Stack
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(
            maximumLength: 100,
            MinimumLength = 2,
            ErrorMessage = "Stack name must be between 2 and 100 characters long"
        )]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsRoot { get; set; }
        public StackTypes Type { get; set; }
        public DataTypes Data { get; set; }
        [ForeignKey("Parent")]
        public Guid? ParentId { get; set; }
        [JsonIgnore]
        public Stack Parent { get; set; }
        [ForeignKey("Content")]
        public Guid ContentId { get; set; }
        [JsonIgnore]
        public Content Content { get; set; }
        [JsonIgnore]
        public List<Stack> Children { get; set; }
    }
}
