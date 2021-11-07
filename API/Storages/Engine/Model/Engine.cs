using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Storages.Engine.Model
{
    /// <summary>
    /// Класс двигателя
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Уникальный номер двигателя, первичный ключ
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Тип двигателя
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// Список машин, в которых используется данный двигатель
        /// </summary>
        public List<string> Cars { get; set; }
    }
}