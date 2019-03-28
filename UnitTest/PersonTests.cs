using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FamilyTree;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class PersonTests
    {
        public List<Relationship> ListOfRelations = new List<Relationship>();
        
        //AddMember(new Person("King Shan", "Male", ListOfRelation));
        //AddMember(new Person("Queen Anga", "Female", ListOfRelation));

        //ADD_CHILD("Queen Anga", "Chit", "Male");
        //ADD_CHILD("Queen Anga", "Ish", "Male");
        //ADD_CHILD("Queen Anga", "Vich", "Male");
        //ADD_CHILD("Queen Anga", "Aras", "Male");
        //ADD_CHILD("Queen Anga", "Satya", "Female");
        [TestMethod]
        public void AddMalePerson()
        {
            //ListOfRelations.Add(new Person("King Shan"))
            
            Person person = new Person("King Shan", "Male", ListOfRelations);

            Assert.AreEqual("King Shan", person.Name);
            Assert.AreEqual("Male", person.Gender);
        }

        [TestMethod]
        public void AddFemalePerson()
        {
            Person person = new Person("Queen Anga", "Female",ListOfRelations);

            Assert.AreEqual("Queen Anga", person.Name);
            Assert.AreEqual("Female", person.Gender);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddInvalidGender()
        {
            Person person = new Person("Test Person", "Hemale",ListOfRelations);
        }

        [TestMethod]
        public void CheckRelationship()
        {
            
            Person personA = new Person("King Shan", "Male",ListOfRelations);
            Person personB = new Person("Queen Anga", "Female",ListOfRelations);
            Relationship relation = new Relationship(personA, personB, "Spouse");

            String rel = relation.firstperson.Name + " is "+ relation.relationtype+ " of " + relation.secondperson.Name ;
            Assert.AreEqual(rel, "King Shan is Spouse of Queen Anga");
        }

        [TestMethod]
        public void ValidateSpouse()
        {
            Person father = new Person("King Shan", "Male", ListOfRelations);
            Person mother = new Person("Queen Anga", "Female", ListOfRelations);
            Relationship spouse = new Relationship(father, mother, "Spouse");
            ListOfRelations.Add(spouse);

            Person husband = mother.Spouse();
            Assert.AreEqual(father, husband);
        }

        [TestMethod]
        public void ValidateMother()
        {
            Person mother = new Person("Queen Anga", "Female", ListOfRelations);
            Person person = new Person("Chit", "Male", ListOfRelations);
            Relationship relation = new Relationship(mother,person, "Child");
            ListOfRelations.Add(relation);

            Person findmother = person.Mother();

            Assert.AreEqual(mother, findmother);
        }

        [TestMethod]
        public void ValidateFather()
        {
            Person father = new Person("King Shan", "Male", ListOfRelations);
            Person mother = new Person("Queen Anga", "Female", ListOfRelations);
            Relationship spouse = new Relationship(father, mother, "Spouse");
            ListOfRelations.Add(spouse);
            Person person = new Person("Chit", "Male", ListOfRelations);
            Relationship relation = new Relationship(mother, person, "Child");
            ListOfRelations.Add(relation);

            Person findfather = person.Father();

            Assert.AreEqual(father, findfather);
        }

        [TestMethod]
        public void ValideSiblings()
        {
            Person father = new Person("King Shan", "Male", ListOfRelations);
            Person mother = new Person("Queen Anga", "Female", ListOfRelations);
            Relationship spouse = new Relationship(father, mother, "Spouse");
            ListOfRelations.Add(spouse);

            Person child1 = new Person("Chit", "Male", ListOfRelations);
            Relationship relation1 = new Relationship(mother, child1, "Child");
            ListOfRelations.Add(relation1);

            Person child2 = new Person("Ish", "Male", ListOfRelations);
            Relationship relation2 = new Relationship(mother, child2, "Child");
            ListOfRelations.Add(relation2);

            Person child3 = new Person("Vich", "Male", ListOfRelations);
            Relationship relation3 = new Relationship(mother, child3, "Child");
            ListOfRelations.Add(relation3);
            
            List<String> siblings = child1.Sibling(null);

            Assert.AreEqual(siblings[0], "Ish");
            Assert.AreEqual(siblings[1], "Vich");
        }

        [TestMethod]
        public void ValidateMaternalAunt()
        {
            Person father = new Person("King Shan", "Male", ListOfRelations);
            Person mother = new Person("Queen Anga", "Female", ListOfRelations);
            Relationship spouse = new Relationship(father, mother, "Spouse");
            ListOfRelations.Add(spouse);

            Person child1 = new Person("Amba", "Female", ListOfRelations);
            Relationship relation1 = new Relationship(mother, child1, "Child");
            ListOfRelations.Add(relation1);

            Person child2 = new Person("Ish", "Female", ListOfRelations);
            Relationship relation2 = new Relationship(mother, child2, "Child");
            ListOfRelations.Add(relation2);

            Person wife = new Person("Chit", "Male", ListOfRelations);
            Relationship relation = new Relationship(child1, wife, "Spouse");
            ListOfRelations.Add(relation);            

            Person child3 = new Person("Dritha", "Female", ListOfRelations);
            Relationship relation3 = new Relationship(child1, child3, "Child");
            ListOfRelations.Add(relation3);

            List<String> aunts = child3.MaternalAunt();

            Assert.AreEqual(aunts[0], "Ish");
        }

        [TestMethod]
        public void ValidatePaternalAunt()
        {
            Person father = new Person("King Shan", "Male", ListOfRelations);
            Person mother = new Person("Queen Anga", "Female", ListOfRelations);
            Relationship spouse = new Relationship(father, mother, "Spouse");
            ListOfRelations.Add(spouse);

            Person child1 = new Person("Chit", "Male", ListOfRelations);
            Relationship relation1 = new Relationship(mother, child1, "Child");
            ListOfRelations.Add(relation1);

            Person child2 = new Person("Ish", "Female", ListOfRelations);
            Relationship relation2 = new Relationship(mother, child2, "Child");
            ListOfRelations.Add(relation2);

            Person wife = new Person("Amba", "Female", ListOfRelations);
            Relationship relation = new Relationship(child1, wife, "Spouse");
            ListOfRelations.Add(relation);

            Person child3 = new Person("Dritha", "Female", ListOfRelations);
            Relationship relation3 = new Relationship(wife, child3, "Child");
            ListOfRelations.Add(relation3);

            List<String> aunts = child3.PaternalAunt();

            Assert.AreEqual(aunts[0], "Ish");
        }

        [TestMethod]
        public void ValidatePaternalUncle()
        {
            Person father = new Person("King Shan", "Male", ListOfRelations);
            Person mother = new Person("Queen Anga", "Female", ListOfRelations);
            Relationship spouse = new Relationship(father, mother, "Spouse");
            ListOfRelations.Add(spouse);

            Person child1 = new Person("Chit", "Male", ListOfRelations);
            Relationship relation1 = new Relationship(mother, child1, "Child");
            ListOfRelations.Add(relation1);

            Person child2 = new Person("Ish", "Male", ListOfRelations);
            Relationship relation2 = new Relationship(mother, child2, "Child");
            ListOfRelations.Add(relation2);

            Person wife = new Person("Amba", "Female", ListOfRelations);
            Relationship relation = new Relationship(child1, wife, "Spouse");
            ListOfRelations.Add(relation);

            Person child3 = new Person("Dritha", "Female", ListOfRelations);
            Relationship relation3 = new Relationship(wife, child3, "Child");
            ListOfRelations.Add(relation3);

            List<String> uncle = child3.PaternalUncle();

            Assert.AreEqual(uncle[0], "Ish");
        }

        [TestMethod]
        public void ValidateMaternalUncle()
        {
            Person father = new Person("King Shan", "Male", ListOfRelations);
            Person mother = new Person("Queen Anga", "Female", ListOfRelations);
            Relationship spouse = new Relationship(father, mother, "Spouse");
            ListOfRelations.Add(spouse);

            Person child1 = new Person("Amba", "Female", ListOfRelations);
            Relationship relation1 = new Relationship(mother, child1, "Child");
            ListOfRelations.Add(relation1);

            Person child2 = new Person("Ish", "Male", ListOfRelations);
            Relationship relation2 = new Relationship(mother, child2, "Child");
            ListOfRelations.Add(relation2);

            Person wife = new Person("Chit", "Male", ListOfRelations);
            Relationship relation = new Relationship(child1, wife, "Spouse");
            ListOfRelations.Add(relation);

            Person child3 = new Person("Dritha", "Female", ListOfRelations);
            Relationship relation3 = new Relationship(child1, child3, "Child");
            ListOfRelations.Add(relation3);

            List<String> uncle = child3.MaternalUncle();

            Assert.AreEqual(uncle[0], "Ish");
        }

        [TestMethod]
        public void ValidateChildren()
        {
            Person father = new Person("King Shan", "Male", ListOfRelations);
            Person mother = new Person("Queen Anga", "Female", ListOfRelations);
            Relationship spouse = new Relationship(father, mother, "Spouse");
            ListOfRelations.Add(spouse);

            Person child1 = new Person("Chit", "Male", ListOfRelations);
            Relationship relation1 = new Relationship(mother, child1, "Child");
            ListOfRelations.Add(relation1);

            Person child2 = new Person("Ish", "Male", ListOfRelations);
            Relationship relation2 = new Relationship(mother, child2, "Child");
            ListOfRelations.Add(relation2);

            Person wife = new Person("Vich", "Male", ListOfRelations);
            Relationship relation3 = new Relationship(mother, wife, "Child");
            ListOfRelations.Add(relation3);

            List<String> children = mother.Children(null);

            Assert.AreEqual(children[0], "Chit");
            Assert.AreEqual(children[1], "Ish");
            Assert.AreEqual(children[2], "Vich");
        }
    }
}
