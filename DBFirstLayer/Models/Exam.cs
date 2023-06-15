﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstLayer.Models
{
    [Table("Exam")]
    public partial class Exam
    {
        [Key]
        public int ExamId { get; set; }
        public int? ClassId { get; set; }
        public int? SubjectId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string RollNo { get; set; }
        public int? TotalMarks { get; set; }
        public int? OutOfMarks { get; set; }

        [ForeignKey("ClassId")]
        [InverseProperty("Exams")]
        public virtual Class Class { get; set; }
        [ForeignKey("SubjectId")]
        [InverseProperty("Exams")]
        public virtual Subject Subject { get; set; }
    }
}