using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicTestLib
{
    public class TestAttribute:System.Attribute
    {
        string name;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public TestAttribute()
        {

        }
        public TestAttribute(string name)
        {
            this.name = name;
        }
    }
}
