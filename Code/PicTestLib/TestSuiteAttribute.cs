using System;

namespace PicTestLib
{
    public class TestSuiteAttribute:System.Attribute
    {
        string name;

        public string Name { 
            get { return this.name; }
            set { this.name = value; }
        }
        public TestSuiteAttribute()
        {

        }
        public TestSuiteAttribute(string name)
        {
            this.name = name;
        }

    }
}
