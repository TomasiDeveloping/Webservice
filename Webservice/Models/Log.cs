using System;

namespace Webservice.Models
{
    public class Log
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string Requester { get; set; }
        public string RequestMethod { get; set; }
        public string ErrorMessage { get; set; }
        public string InnerException { get; set; }
        public LogType LogType { get; set; }
        public int LogTypeId { get; set; }
    }
}