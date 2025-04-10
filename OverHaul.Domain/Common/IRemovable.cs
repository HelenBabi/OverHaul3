﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Domain.Common
{
    internal interface IRemovable
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeleterUserId { get; set; }
    }
}
