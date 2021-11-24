using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreExperiment.JsonSearching
{
    public class MyEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        [Column(TypeName = "json")]
        public Translations Translations { get; set; }
    }
}
