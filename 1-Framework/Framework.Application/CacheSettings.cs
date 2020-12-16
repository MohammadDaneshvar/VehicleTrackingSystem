using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Application
{
    public class CacheSettings
    {
        public bool Enabled { get; set; } = true;
        public string ConnectionString { get; set; }
        public int DefaultSecondsToCache { get; set; } = 600;
    }
}
