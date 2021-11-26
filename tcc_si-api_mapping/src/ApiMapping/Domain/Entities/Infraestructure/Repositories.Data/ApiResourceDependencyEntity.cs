using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMapping.Domain.Entities.Infraestructure.Repositories.Data
{
    [Table("APIs_Resources_Dependencies")]
    public class ApiResourceDependencyEntity
    {
        [Key]
        public string Consumer_Api { get; set; }
        [Key]
        public string Consumed_Api { get; set; }
        [Key]
        public string Consumer_Resource { get; set; }
        [Key]
        public string Consumed_Resource { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
