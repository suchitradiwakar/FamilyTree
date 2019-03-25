using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyTree
{
    public class Relationship
    {
        
        public  Person firstperson;
        public  Person secondperson;
        public  String relationtype;

        public Relationship(Person personA, Person personB, String type)
        {
            firstperson = personA;
            secondperson = personB;
            relationtype = type; 
        }

        

    }
}
