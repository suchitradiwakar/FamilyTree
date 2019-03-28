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
                //DisplayMembers();
                AddRelation(FamilyMembers[0], FamilyMembers[1], "Spouse");

                

                StreamReader sw = new StreamReader("c:\\rdms\\geo dss-demand\\test driven development\\familytree\\familytree\\sampletest.txt");
                ReadFile(sw,"test");
                sw.Close();

                //DisplayMembers();

                Console.ReadLine();
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Cannot read the file",e);
            }
                                                                     
        }

        public static void ReadFile(StreamReader sr, String Filetype)
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
                    if (Filetype == "Input")
                    {
                        ADD_CHILD(words[1], words[2], words[3], "Input");
                    }
                    else
                    {
                        ADD_CHILD(words[1], words[2], words[3]);
                    }
                }
                else if (words[0] == "ADD_SPOUSE")
                {
                    ADD_SPOUSE(words[1], words[2], words[3]);
                }
                
                else if(words[0] == "GET_RELATIONSHIP")
                {
                    GetRelationship(words[1], words[2]);
                }

                
            }


        }
        public static void CreateListOfPeople()
        {
            Person king = new Person("King Shan", "Male", ListOfRelation);
            Person queen = new Person("Queen Anga", "Female", ListOfRelation);
            AddMember(king);

            ADD_SPOUSE("King Shan", "Queen Anga", "Female");

            ADD_CHILD("Queen Anga", "Chit", "Male","Input");
            ADD_CHILD("Queen Anga", "Ish", "Male", "Input");
            ADD_CHILD("Queen Anga", "Vich", "Male", "Input");
            ADD_CHILD("Queen Anga", "Aras", "Male", "Input");
            ADD_CHILD("Queen Anga", "Satya", "Female", "Input");

            StreamReader sr = new StreamReader("C:\\RDMS\\Geo DSS-Demand\\Test Driven Development\\FamilyTree\\FamilyTree\\InputFile.txt");
            ReadFile(sr,"Input");
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
            //Console.WriteLine(relation.firstperson.Name+" "+ relation.secondperson.Name+ " "+ relation.relationtype);
        }

        public static void ADD_CHILD(String motherName, String childName, String gender, String fileType)
        {
            if(fileType == "Input")
            {
                Person mother = FamilyMembers.Find(r => r.Name == motherName);
                Person child = new Person(childName, gender, ListOfRelation);
                AddMember(child);
                AddRelation(mother, child, "Child");
            }
            else if(fileType == "Test")
            {
                ADD_CHILD(motherName, childName, gender);
            }
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
