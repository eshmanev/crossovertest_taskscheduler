using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trial.Scheduler.Core.Model
{
    [Table("Client")]
    public class Client
    {
        public Client()
        {
            Command = new HashSet<Command>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        public string Address { get; set; }

        public virtual ICollection<Command> Command { get; set; }
    }
}
