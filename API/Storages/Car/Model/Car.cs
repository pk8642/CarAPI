using System.ComponentModel.DataAnnotations;

namespace API.Storages.Car.Model
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ModelName { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public int EngineId { get; set; }
    }
}