using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree
{
    public class Person
    {
        public String Name;
        public String Gender;

        public Person(String name, String gender)
        {

            Name = name;
            if (gender == "Male" || gender == "Female")
                Gender = gender;
            else
                throw new System.ArgumentException("Gender does not match");
        }

        
        
    }

    
}
