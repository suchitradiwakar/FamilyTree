using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FamilyTree;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class RelationshipTest
    {
        public List<Relationship> ListOfRelations = new List<Relationship>();

        [TestMethod]
        public void AddRelationTest()
        {
            Person second = new Person("Queen Anga", "Female",ListOfRelations);
            Person first = new Person("Chitra", "Female",ListOfRelations);
            

            
        }
    }
}
