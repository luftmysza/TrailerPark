using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TrailerPark.Core.Models;

public class Rating
{
    public int RatingID { get;set; }
    public string? Source { get; set; }
    public float? Value { get; set; }
}
