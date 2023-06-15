﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBFirstLayer.Models
{
    [Table("Subject")]
    public partial class Subject
    {
        public Subject()
        {
            Exams = new HashSet<Exam>();
            Expenses = new HashSet<Expense>();
            StudentAttendances = new HashSet<StudentAttendance>();
            TeacherSubjects = new HashSet<TeacherSubject>();
        }

        [Key]
        public int SubjectId { get; set; }
        public int? ClassId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SubjectName { get; set; }

        [ForeignKey("ClassId")]
        [InverseProperty("Subjects")]
        public virtual Class Class { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Expense> Expenses { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}