using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FamilyTree;

namespace UnitTest
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public void AddMalePerson()
        {
            Person person = new Person("King Shan", "Male");

            Assert.AreEqual("King Shan", person.Name);
            Assert.AreEqual("Male", person.Gender);
        }
        [TestMethod]
        public void AddFemalePerson()
        {
            Person person = new Person("Queen Anga", "Female");

            Assert.AreEqual("Queen Anga", person.Name);
            Assert.AreEqual("Female", person.Gender);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddInvalidGender()
        {
            Person person = new Person("Test Person", "Hemale");
        }


        [TestMethod]
        public void CheckRelationship()
        {
            Person personA = new Person("King Shan", "Male");
            Person personB = new Person("Queen Anga", "Female");
            Relationship relation = new Relationship(personA, personB, "Spouse");

            String rel = relation.firstperson.Name + " is "+ relation.relationtype+ " of " + relation.secondperson.Name ;
            Assert.AreEqual(rel, "King Shan is Spouse of Queen Anga");
        }
        
    }
}
