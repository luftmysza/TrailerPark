using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
﻿using System.ComponentModel.DataAnnotations;

namespace TrailerPark.Core.Models;

public class Rating
{
    [Key]
    public int? RatingID { get;set; } = null!;
    public string? Source { get; set; }
    public string? Value { get; set; }
}
