﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class DataTableDto<T>
    {
        public int? draw { get; set; }
        public int? recordsFiltered { get; set; }
        public int? recordsTotal { get; set; }
        public List<T> data { get; set; } = new List<T>();
    }
}
