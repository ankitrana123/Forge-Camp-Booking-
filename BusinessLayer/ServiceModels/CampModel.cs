using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ServiceModels
{
    public  class CampModel
    {
        public int Amount { get; set; }

        public int Capacity { get; set; }

        public string Description { get; set; }

        
        public byte[] Image { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public System.Guid Id { get; set; }

        public bool IsBooked { get; set; }

        public string Title { get; set; }
    }
}
