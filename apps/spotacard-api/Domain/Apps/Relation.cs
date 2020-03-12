using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotacard.Domain
{
    public class Relation
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(
            maximumLength: 250,
            MinimumLength = 2,
            ErrorMessage = "Relation name must be between 2 and 250 characters long"
        )]
        public string Name { get; set; }

        public Guid PkFieldId { get; set; }
        public Field PkField { get; set; }
        public string PkName { get; set; }
        public Guid FkFieldId { get; set; }
        public Field FkField { get; set; }
        public string FkName { get; set; }
    }
}
