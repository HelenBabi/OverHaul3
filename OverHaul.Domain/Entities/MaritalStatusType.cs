﻿using Overhaul.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Domain.Entities;

public class MaritalStatusType:Entity
{
    [Key]
    public new int Id { get; set; }
    public string Title { get; set; }
}
