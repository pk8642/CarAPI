using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Storages.Engine.Model
{
    public class Engine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }

        public List<string> Cars { get; set; }
    }
}