using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMapping.Domain.Entities.Infraestructure.Repositories.Data
{
    [Table("APIs_Resources")]
    public class ApiResourceEntity
    {
        [Key]
        public string Resource { get; set; }
        public string Api_Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
