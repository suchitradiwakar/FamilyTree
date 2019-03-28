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
        public List<Relationship> ListOfRelations;
        
        public Person(String name, String gender, List<Relationship> ListOfRel)
        {

            Name = name;
            if (gender == "Male" || gender == "Female")
                Gender = gender;
            else
                throw new System.ArgumentException("Gender does not match");
            ListOfRelations = ListOfRel;
        }


        public Person Mother()
        {
            Relationship momchild = ListOfRelations.Find(r => r.secondperson.Name == Name && r.relationtype == "Child");
            if (momchild == null)
            {
                //Console.WriteLine("NONE");
                return null;
            }
            return momchild.firstperson;
        }

        public Person Father()
        {
            Person mother = Mother();
            Relationship spouse = ListOfRelations.Find(
                    r => (r.secondperson == mother || r.firstperson == mother)
                        && r.relationtype == "Spouse");
            if (spouse == null)
            {
                //Console.WriteLine("NONE");
                return null;
            }
            if (spouse.firstperson == mother)
                return spouse.secondperson;
            else
                return spouse.firstperson;
        }

        public List<String> Sibling(String gender)
        {
            Person mother = Mother();
            List<Relationship> children = ListOfRelations.FindAll(r => r.firstperson == mother && r.relationtype == "Child");
            int index = 0;
            List<String> siblings = new List<String>();
            while (index < children.Count())
            {
                if (children[index].secondperson.Name != Name)
                {
                    if (gender == null)
                        siblings.Add(children[index].secondperson.Name);
                    else
                        if (children[index].secondperson.Gender == gender)
                        siblings.Add(children[index].secondperson.Name);
                }
                index++;
            }
            return siblings;
        }

        public Person Spouse()
        {
            Person spouse;
            Relationship husbwife = ListOfRelations.Find(r => (r.firstperson.Name == Name || r.secondperson.Name == Name) && r.relationtype == "Spouse");
            if (husbwife == null)
            {
                return null;
            }
            if (husbwife.firstperson.Name == Name)
            {
                spouse = husbwife.secondperson;
            }
            else
            {
                spouse = husbwife.firstperson;
            }
            return spouse;
        }

        public List<String> MaternalAunt()
        {
            Person mother = Mother();
            List<String> siblings = mother.Sibling("Female");
            return siblings;
        }

        public List<String> PaternalUncle()
        {
            Person father = Father();
            List<String> uncles = father.Sibling("Male");
            return uncles;
        }

        public List<String> MaternalUncle()
        {
            Person mother = Mother();
            List<String> uncles = mother.Sibling("Male");
            return uncles;
        }

        public List<String> PaternalAunt()
        {
            Person father = Father();
            List<String> aunts = father.Sibling("Female");
            return aunts;
        }

        public List<String> Children(String gender)
        {

            String motherName;
            if (Gender == "Male")
            {
                motherName = Spouse().Name;

            }
            else
            {
                motherName = Name;
            }
            List<String> childrenNames = new List<String>();
            List<Relationship> children = ListOfRelations.FindAll(r => r.firstperson.Name == motherName && r.relationtype == "Child");
            if (gender != null)
            {
                foreach (Relationship child in children)
                {
                    if (child.secondperson.Gender == gender)
                        childrenNames.Add(child.secondperson.Name);
                }
            }
            else
            {
                foreach (Relationship child in children)
                {                  
                    childrenNames.Add(child.secondperson.Name);
                }
            }
            return childrenNames;
        }

    }


}
