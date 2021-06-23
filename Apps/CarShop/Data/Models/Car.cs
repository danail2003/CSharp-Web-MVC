namespace CarShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Car
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, MaxLength(20)]
        public string Model { get; set; }

        public int Year { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        [RegularExpression(@"[A-Z]{2}[0-9]{4}[A-Z]{2}")]
        public string PlateNumber { get; set; }

        [ForeignKey(nameof(Owner))]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public ICollection<Issue> Issues { get; set; } = new HashSet<Issue>();
    }
}
