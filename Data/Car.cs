using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Car
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int SeatsCount { get; set; }
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public IEnumerable<Request> Requests { get; set; }
    }
}
