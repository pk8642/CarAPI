using System.ComponentModel.DataAnnotations;

namespace API.Storages.Car.Model
{
    /// <summary>
    /// Класс машины
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Уникальный номер машины, первичный ключ
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя модели
        /// </summary>
        [Required]
        public string ModelName { get; set; }
        
        /// <summary>
        /// Уникальный номер производителя
        /// </summary>
        [Required]
        public int ManufacturerId { get; set; }

        /// <summary>
        /// Уникальный номер типа двигателя
        /// </summary>
        [Required]
        public int EngineId { get; set; }
    }
}