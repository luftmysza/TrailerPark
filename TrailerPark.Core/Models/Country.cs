using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace TrailerPark.Core.Models;

public class Country
{
    [Key]
    [Index(1)]
    public string Alpha2 { get; set; } = null!;
    [Index(0)]
    public string Name { get; set; } = null!;
    [Index(2)]
    public string Alpha3 { get; set; } = null!;
}
