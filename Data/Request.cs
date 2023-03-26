﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Request
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime StartingDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
