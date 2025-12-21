using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Generic;
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrailerPark.Core.Models;

public class Person
{
    [Key]
    public int PersonID { get; set; }
    public string Name { get; set; } = "";
}
