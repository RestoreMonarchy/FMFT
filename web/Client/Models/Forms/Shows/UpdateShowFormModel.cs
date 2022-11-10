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
        public DateOnly EndDate { get; set; }
        [Required(ErrorMessage = "To pole jest wymagane")]
        public TimeOnly EndTime { get; set; }

        public int AudotiriumId { get; set; }
        [Required(ErrorMessage = "Zdjęcie miniaturki jest wymagane")]
        public Guid? MediaId { get; set; }
    }
}
