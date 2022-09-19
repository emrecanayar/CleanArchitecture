﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.ComplexTypes
{
    public enum RecordStatu
    {
        None = 0,
        Active = 1,
        Passive = 2,
    }

    public enum FileType
    {
        None,
        Xls,
        Xlsx,
        Doc,
        Pps,
        Pdf,
        Img,
        Mp4
    }
}
