﻿using System.ComponentModel.DataAnnotations;

namespace FMFT.Web.Client.Models.Forms.Shows
{
    public class UpdateShowFormModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(100, ErrorMessage = "Nazwa nie może mieć więcej niż 100 znaków")]
        public string Name { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(4000, ErrorMessage = "Opis nie może mieć więcej niż 4000 znaków")]
        public string Description { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public TimeOnly StartTime { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        public int DurationMinutes { get; set; }

        [Required(ErrorMessage = "Musisz wybrać audytorium")]
        public int? AudotiriumId { get; set; }
        public Guid? ThumbnailMediaId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        public DateOnly SellStartDateTime { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public TimeOnly SellStartTime { get; set; }

        public bool IsEnabled { get; set; }
    }
}
