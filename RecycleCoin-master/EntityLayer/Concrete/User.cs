
using EntityLayer.Abstract;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Tc { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public AuthEnum Auth { get; set; }
        public string PublicKey { get; set; }
    }
}
