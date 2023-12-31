﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFDataFirstLIbrary.Models
{
    [Table("CompanyInfo")]
    public partial class CompanyInfo
    {
        public CompanyInfo()
        {
            Teams = new HashSet<Team>();
        }

        [Key]
        public int CompanyId { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string CompanyName { get; set; }

        [InverseProperty("TeamCompany")]
        public virtual ICollection<Team> Teams { get; set; }
    }
}