﻿using P01_StudentSystem.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public Course()
        {
            this.StudentsCourses = new HashSet<StudentCourse>();
            this.Homeworks = new HashSet<Homework>();
            this.Resources = new HashSet<Resource>();
        }

        [Key]
        public int CourseId { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CourseNameMaxLength)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<StudentCourse> StudentsCourses { get; set; } = null!;
        public virtual ICollection<Resource> Resources { get; set; } = null!;
        public virtual ICollection<Homework> Homeworks { get; set; } = null!;
    }
}
