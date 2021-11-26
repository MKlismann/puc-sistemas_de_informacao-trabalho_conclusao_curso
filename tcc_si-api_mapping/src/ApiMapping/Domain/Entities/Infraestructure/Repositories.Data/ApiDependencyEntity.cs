using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMapping.Domain.Entities.Infraestructure.Repositories.Data
{
    [Table("APIs_Dependencies")]
    public class ApiDependencyEntity
    {
        [Key]      
        public string Consumer { get; set; }
        [Key]
        public string Consumed { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
