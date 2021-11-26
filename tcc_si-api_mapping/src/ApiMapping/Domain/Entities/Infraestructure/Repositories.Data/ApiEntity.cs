using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMapping.Domain.Entities.Infraestructure.Repositories.Data
{
    [Table("APIs")]
    public class ApiEntity
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
