using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicle.Domain.Logs
{
    public class ApplicationLog
    {
        public int Id { get; set; }
        public string MachineName { get; set; }
        public string Application { get; set; }
        public string UserName { get; set; }
        public string Ip { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Callsite { get; set; }
        public string Exception { get; set; }
        public int? OrganizationId { get; set; }
        public int? UserId { get; set; }
    }
}
