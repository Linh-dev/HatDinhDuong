﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eFashionShop.Data.Enums
{
    public enum Status
    {
        [Display(Name = "Không hoạt động")]
        InActive,
        [Display(Name = "Hoạt động")]
        Active
    }
}
