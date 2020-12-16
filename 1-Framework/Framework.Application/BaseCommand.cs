using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Application
{
 public   class BaseCommand
    {
        bool CalculateRequestCost { get; }
        decimal CommandPrice { get; set; }
    }
}
