using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Storages.Manufacturer.Model
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<string> Cars { get; set; }
    }
}