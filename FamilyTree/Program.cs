using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FamilyTree
{
    class FamilyTree
    {
        public static List<Person> FamilyMembers = new List<Person>();
        public static List<Relationship> ListOfRelation = new List<Relationship>();
        public static void Main(string[] args)
        {
            try
            {
                CreateListOfPeople();
                DisplayMembers();
                AddRelation(FamilyMembers[0], FamilyMembers[1], "Spouse");

                

                StreamReader sw = new StreamReader("C:\\RDMS\\Geo DSS-Demand\\Test Driven Development\\FamilyTree\\FamilyTree\\SampleTest.txt");
                ReadFile(sw);
                sw.Close();

                //DisplayMembers();

                Console.ReadLine();
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Cannot read the file",e);
            }
                                                                     
        }

        public static void ReadFile(StreamReader sr)
        {
            List<String> fileContents = new List<String>();
            //StreamReader sr = new StreamReader(fileName);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                fileContents.Add(line);
                String[] words = line.Split(' ');
                

                if (words[0] == "ADD_CHILD")
                {
                    Console.WriteLine(words[0] + " " + words[1] + " " + words[2] + " " + words[3]);
                    ADD_CHILD(words[1], words[2], words[3]);
                }
                else if (words[0] == "ADD_SPOUSE")
                {
                    Console.WriteLine(words[0] + " " + words[1] + " " + words[2] + " " + words[3]);
                    ADD_SPOUSE(words[1], words[2], words[3]);
                }
                
                else if(words[0] == "GET_RELATIONSHIP")
                {
                    Console.WriteLine(words[0] + " " + words[1] + " " + words[2]);
                    GetRelationship(words[1], words[2]);
                }
                else
                {
                    Console.WriteLine("Wrong command.");
                }
                
            }
            //Console.ReadLine();
            //sr.Close();

        }
        public static void CreateListOfPeople()
        {
            Person king = new Person("King Shan", "Male", ListOfRelation);
            Person queen = new Person("Queen Anga", "Female", ListOfRelation);
            AddMember(king);
            //AddMember(queen);
            ADD_SPOUSE("King Shan", "Queen Anga", "Female");

            ADD_CHILD("Queen Anga", "Chit", "Male");
            ADD_CHILD("Queen Anga", "Ish", "Male");
            ADD_CHILD("Queen Anga", "Vich", "Male");
            ADD_CHILD("Queen Anga", "Aras", "Male");
            ADD_CHILD("Queen Anga", "Satya", "Female");

            StreamReader sr = new StreamReader("C:\\RDMS\\Geo DSS-Demand\\Test Driven Development\\FamilyTree\\FamilyTree\\InputFile.txt");
            ReadFile(sr);
            sr.Close();
        }

        public static void AddMember(Person person)
        {
            FamilyMembers.Add(person);
        }

        public static void DisplayMembers()
        {
            foreach (Person person in FamilyMembers)
                Console.WriteLine(person.Name + " - " + person.Gender);

        }        

        public static void AddRelation(Person personA, Person personB, String type)
        {
            Relationship relation = new Relationship(personA, personB, type);
            ListOfRelation.Add(relation);
            Console.WriteLine(relation.firstperson.Name+" "+ relation.secondperson.Name+ " "+ relation.relationtype);
        }


        public static void ADD_CHILD(String motherName, String childName, String gender)
        {
            Person mother = FamilyMembers.Find(r => r.Name == motherName);
            if (!FamilyMembers.Contains(mother))
            {
                Console.WriteLine("PERSON_NOT_FOUND");
                return;
            }
            if (mother.Gender == "Male")
            {
                Console.WriteLine("CHILD_ADDITION_FAILED");
            }
            else
            {
                Person child = new Person(childName, gender, ListOfRelation);
                if (!FamilyMembers.Contains(child))
                {
                    AddMember(child);
                }

                AddRelation(mother, child, "Child");
                Console.WriteLine("CHILD_ADDITION_SUCCEEDED");
            }
        }

        public static void ADD_SPOUSE(String spouse1, String spouse2, String gender)
        {
            Person spouse = FamilyMembers.Find(r => r.Name == spouse1);
            if(!FamilyMembers.Contains(spouse))
            {
                Console.WriteLine("PERSON_NOT_FOUND");
                return;
            }

            Person newspouse = new Person(spouse2, gender, ListOfRelation);
            if(!FamilyMembers.Contains(newspouse))
            {
                AddMember(newspouse);
                AddRelation(spouse, newspouse, "Spouse");
            }
        }

        //public static Person FindMother(Person child)
        //{
        //    Relationship momchild = ListOfRelation.Find(r => r.secondperson.Name == child.Name && r.relationtype == "Child");
        //    if(momchild == null)
        //    {
        //        Console.WriteLine("NONE");
        //        return null;                
        //    }
        //    return momchild.firstperson;
        //}

        //public static Person FindFather(Person child)
        //{
        //    Person mother = FindMother(child);
        //    Relationship spouse = ListOfRelation.Find(
        //            r => (r.secondperson.Name == mother.Name || r.firstperson.Name == mother.Name)
        //                && r.relationtype == "Spouse");
        //    if(spouse == null)
        //    {
        //        Console.WriteLine("NONE");
        //        return null;
        //    }
        //    if (spouse.firstperson == mother)
        //        return spouse.secondperson;
        //    else
        //        return spouse.firstperson;
        //}

        //public static List<String> FindSibling(Person person, String gender)
        //{
        //    Person mother = FindMother(person);
        //    List<Relationship> children = ListOfRelation.FindAll(r => r.firstperson == mother && r.relationtype == "Child");
        //    int index = 0;
        //    List<String> siblings = new List<String>();
        //    //Console.WriteLine(person.Name + "'s siblings are - ");
        //    while (index < children.Count())
        //    {
        //        if (children[index].secondperson.Name != person.Name)
        //        {
        //            if (gender == null)
        //                siblings.Add(children[index].secondperson.Name);
        //            //Console.WriteLine(children[index].secondperson.Name);
        //            else
        //                if (children[index].secondperson.Gender == gender)
        //                    siblings.Add(children[index].secondperson.Name);
        //                    //Console.WriteLine(children[index].secondperson.Name);
        //        }
        //        index++;
        //    }
        //    return siblings;           
        //} 

        //public static Person FindSpouse(Person person)
        //{
        //    Person spouse;
        //    Relationship husbwife = ListOfRelation.Find(r => (r.firstperson == person || r.secondperson == person) && r.relationtype == "Spouse");
        //    if(husbwife==null)
        //    {               
        //        return null;
        //    }
        //    if (husbwife.firstperson == person)
        //    {
        //        spouse = husbwife.secondperson;
        //    }
        //    else
        //    {
        //        spouse = husbwife.firstperson;
        //    }
        //    return spouse;
        //}

        //public static List<String> FindMaternalAunt(Person person)
        //{           
        //    Person mother = FindMother(person);
        //    List<String> siblings = FindSibling(mother, "Female");
        //    return siblings;
        //}

        //public static List<String> FindPaternalUncle(Person person)
        //{
        //    Person father = FindFather(person);
        //    List<String> uncles = FindSibling(father, "Male");
        //    return uncles;
        //}

        //public static List<String> FindMaternalUncle(Person person)
        //{
        //    Person mother = FindMother(person);
        //    List<String> uncles = FindSibling(mother, "Male");
        //    return uncles;
        //}

        //public static List<String> FindPaternalAunt(Person person)
        //{
        //    Person father = FindFather(person);
        //    List<String> aunts = FindSibling(father, "Female");
        //    return aunts;
        //}

        //public static List<String> FindChildren(Person person, String gender)
        //{
        //    Person mother;
        //    if(person.Gender == "Male")
        //    {
        //        mother = FindSpouse(person);
        //    }
        //    else
        //    {
        //        mother = person;
        //    }
        //    List<String> childrenNames = new List<String>();
        //    List<Relationship> children = ListOfRelation.FindAll(r => r.firstperson == mother && r.relationtype == "Child");
        //    foreach(Relationship child in children)
        //    {
        //        if(child.secondperson.Gender == gender)
        //            childrenNames.Add(child.secondperson.Name);
        //    }
        //    return childrenNames;
        //}
        public static void GetRelationship(String Name, String relationType)
        {
            Person person = FamilyMembers.Find(r => r.Name == Name);
            if (person == null)
            {
                Console.WriteLine("PERSON_NOT_FOUND");
                return;
            }

            switch (relationType)
            {
                case "Siblings":
                case "Sibling":
                    {
                        List<String> siblings = person.Sibling(null);
                        if (siblings == null)
                        {
                            Console.WriteLine("NONE");
                        }
                        else
                        {
                            Console.WriteLine(person.Name + "'s siblings are - ");
                            foreach (String name in siblings)
                            {
                                Console.WriteLine(name);
                            }
                        }
                        break;
                    }
                case "Maternal-Aunt":
                    {
                        List<String> aunts = person.MaternalAunt();
                        if (aunts == null)
                        {
                            Console.WriteLine("NONE");
                        }
                        else
                        {
                            Console.WriteLine(person.Name + "'s Maternal-aunts are - ");
                            foreach (String name in aunts)
                            {
                                Console.WriteLine(name);
                            }
                        }
                        break;

                    }
                case "Paternal-Uncle":
                    {
                        List<String> uncles = person.PaternalUncle();
                        if (uncles == null)
                        {
                            Console.WriteLine("NONE");
                        }
                        else
                        {
                            Console.WriteLine(person.Name + "'s Paternal-Uncle are - ");
                            foreach (String name in uncles)
                            {
                                Console.WriteLine(name);
                            }
                        }
                        break;
                    }
                case "Maternal-Uncle":
                    {
                        List<String> uncles = person.MaternalUncle();
                        if (uncles == null)
                        {
                            Console.WriteLine("NONE");
                        }
                        else
                        {
                            Console.WriteLine(person.Name + "'s Maternal-Uncle are - ");
                            foreach (String name in uncles)
                            {
                                Console.WriteLine(name);
                            }
                        }
                        break;
                    }
                case "Paternal-Aunt":
                    {
                        List<String> aunts = person.PaternalAunt();
                        if (aunts == null)
                        {
                            Console.WriteLine("NONE");
                        }
                        else
                        {
                            Console.WriteLine(person.Name + "'s Paternal-Aunts are - ");
                            foreach (String name in aunts)
                            {
                                Console.WriteLine(name);
                            }
                        }
                        break;
                    }
                case "Sister-In-Law":
                    {
                        Person spouse = person.Spouse();
                        List<String> sil = new List<String>();
                        if (spouse != null)
                        {
                            sil = spouse.Sibling("Female");
                        }            
                         

                        List<String> brothers = person.Sibling("Male");
                        foreach (String brother in brothers)
                        {
                            Person bro = FamilyMembers.Find(r => r.Name == brother);
                            Person wife = bro.Spouse();
                            sil.Add(wife.Name);
                        }


                        Console.WriteLine(person.Name + "'s Sister-in-laws are - ");
                        foreach (String name in sil)
                        {
                            Console.WriteLine(name);
                        }
                        
                        break;
                    }
                case "Brother-In-Law":
                    {
                        Person spouse = person.Spouse();
                        List<String> bil = new List<String>();
                        if (spouse != null)
                        {
                            bil = spouse.Sibling("Male");
                        }

                        List<String> sisters = person.Sibling("Female");
                        foreach (String sister in sisters)
                        {
                            Person sis = FamilyMembers.Find(r => r.Name == sister);
                            Person husband = sis.Spouse();
                            bil.Add(husband.Name);
                        }


                        Console.WriteLine(person.Name + "'s Brother-in-laws are - ");
                        foreach (String name in bil)
                        {
                            Console.WriteLine(name);
                        }
                        
                        break;
                    }
                case "Son":
                    {
                        List<String> sons = person.Children("Male");
                        if (sons == null)
                        {
                            Console.WriteLine("NONE");
                        }
                        else
                        {
                            Console.WriteLine(person.Name + "'s Sons are - ");
                            foreach (String name in sons)
                            {
                                Console.WriteLine(name);
                            }
                        }
                        break;
                    }
                case "Daughter":
                    {
                        List<String> daughters = person.Children("Female");
                        if (daughters == null)
                        {
                            Console.WriteLine("NONE");
                        }
                        else
                        {
                            Console.WriteLine(person.Name + "'s Daughters are - ");
                            foreach (String name in daughters)
                            {
                                Console.WriteLine(name);
                            }
                        }
                        break;
                    }



            }                       

            //Console.ReadLine();

        }
    }
}
