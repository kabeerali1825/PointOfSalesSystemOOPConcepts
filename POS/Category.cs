using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    internal class Category
    {
        private readonly int id;
        private readonly string name;

        public Category(string name)
        {
            this.name = name;
        }

        public string Name { get { return name; } }

        public override string ToString()
        {
            return Name;
        }
    }
}
