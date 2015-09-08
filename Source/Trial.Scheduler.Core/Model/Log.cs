using System.ComponentModel.DataAnnotations.Schema;

namespace Trial.Scheduler.Core.Model
{

    [Table("Log")]
    public class Log
    {
        public int Id { get; set; }

        public string Output { get; set; }

        public int CommandId { get; set; }

        public virtual Command Command { get; set; }
    }
}
