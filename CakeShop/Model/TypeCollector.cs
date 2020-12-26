using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeShop.Model
{
    public class TypeCollector
    {
        private int _ID;
        private string _C_NAME;

        public int ID { get => _ID; set => _ID = value; }
        public string C_NAME { get => _C_NAME; set => _C_NAME = value; }
        public TypeCollector(int id, string name)
        {
            this.ID = id;
            this.C_NAME = name;
        }
        public TypeCollector()
        {
        }
    }
}
