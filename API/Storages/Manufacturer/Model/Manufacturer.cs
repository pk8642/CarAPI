using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Storages.Manufacturer.Model
{
    /// <summary>
    /// Класс производителя
    /// </summary>
    public class Manufacturer
    {
        /// <summary>
        /// Уникальный номер производителя, первичный ключ
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Наименование производителя
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Список машин, произведенных данным производителем
        /// </summary>
        public List<string> Cars { get; set; }
    }
}