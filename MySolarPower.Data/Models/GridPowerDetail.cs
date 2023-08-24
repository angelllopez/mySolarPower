using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MySolarPower.Data.Models;

[Table("GridPowerDetail")]
public partial class GridPowerDetail
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Date { get; set; }

    [Precision(0)]
    public TimeSpan? StartTime { get; set; }

    [Precision(0)]
    public TimeSpan? EndTime { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Usage { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Cost { get; set; }
}
