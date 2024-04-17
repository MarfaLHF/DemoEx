using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DemoEx.Models
{
    public class Application
    {
        [Key]
        public int IdApplication { get; set; }

        [Required(ErrorMessage = "Дата добавления заявки обязательна.")]
        public DateTime DateAddition { get; set; }

        [Required(ErrorMessage = "Наименование оборудования обязательно.")]
        public string NameEquipment { get; set; }

        [Required(ErrorMessage = "Тип проблемы обязателен.")]
        [ForeignKey("TypeProblem")]
        public int IdTypeProblem { get; set; }
        public virtual TypeProblem TypeProblem { get; set; }

        public string? Comment { get; set; }

        [Required(ErrorMessage = "Статус обязателен.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Имя клиента обязательно.")]
        public string NameClient { get; set; }


        public decimal? Cost { get; set; }

   
        public DateTime? DateEnd { get; set; }

        public TimeSpan? TimeWork { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required(ErrorMessage = "Статус работы обязателен.")]
        public string? WorkStatus { get; set; }

        public DateTime? PeriodExecution { get; set; }

        [Required(ErrorMessage = "Тип оборудования обязателен.")]
        [ForeignKey("TypeEquipment")]
        public int IdTypeEquipment { get; set; }
        public virtual TypeEquipment TypeEquipment { get; set; }

        [Required(ErrorMessage = "Номер обязателен.")]
        public int Number { get; set; }

        public string? Description { get; set; }
    }
}
