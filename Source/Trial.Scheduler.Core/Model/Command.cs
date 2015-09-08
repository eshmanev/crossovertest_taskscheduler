using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trial.Scheduler.Core.Model
{
    [Table("Command")]
    public class Command
    {
        public Command()
        {
            Log = new HashSet<Log>();
        }

        public int Id { get; set; }

        public int ClientId { get; set; }

        [Required]
        [StringLength(50)]
        public string CommandText { get; set; }

        [StringLength(255)]
        public string Parameters { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<Log> Log { get; set; }
    }
}
