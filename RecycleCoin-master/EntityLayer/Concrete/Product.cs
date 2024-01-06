using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Product : IEntity
    {
        public string TypeName { get; set;}//50
        public int Carbon { get; set; }//150
        public Guid Id { get; set; }
    }
}
