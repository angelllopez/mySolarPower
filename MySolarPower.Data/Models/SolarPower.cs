using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MySolarPower.Data.Models;

[Table("SolarPower")]
public partial class SolarPower
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Date { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? EnergyProduced { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? EnergyUsed { get; set; }

    [Column("MaxACPowerProduced", TypeName = "decimal(18, 2)")]
    public decimal? MaxAcpowerProduced { get; set; }
}
