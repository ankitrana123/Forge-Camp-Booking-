﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CampBooking.Models
{
    public class CampViewModel
    {
        public int Amount { get; set; }

        public int Capacity { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public System.Guid Id { get; set; }

        public bool IsBooked { get; set; }

        public string Title { get; set; }

        public int Rating { get; set; }
    }
}