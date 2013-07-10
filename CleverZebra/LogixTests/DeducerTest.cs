#define DEBUG 
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logix;
using System.Collections.Generic;

namespace LogixTests
{
    [TestClass]
    public class DeducerTest
    {
        private RelationFactory relationBuilder;

        [TestInitialize]
        public void Initialise() {
            relationBuilder = RelationFactory.getInstance();
        }

        [TestMethod]
        public void Create_Deducer() {
            Deducer brains = new Deducer(4,5);
            List<Category> cats = brains.getCategoryCollection();
            Assert.AreEqual(4, cats.Count);
        }

        [TestMethod]
        public void Retrieve_Line_From_Identifier() {
            Deducer brains = new Deducer(4, 5);
            Category l = brains.getCategoryFromIdentifier('C');
            Assert.IsNotNull(l);
            l.considerRelationToCategory(relationBuilder.createRelation("C1=B3"), false);
            string consideredItem = l.checkForMatch("B3");
            Assert.AreEqual("C1", consideredItem);
        }

        [TestMethod]
        public void Set_Clues() {
            List<string> clues = new List<string>{ "A3=D1", "A2(C)-A3(C)=2", "A2=B3", "B2=D3", "D2!=C2" };
            Deducer brains = new Deducer(4, 3);
            brains.setClues(clues);
            Assert.AreNotEqual(0, brains.getRemainingClueCount());
        }

        [TestMethod]
        public void First_Problem_Test() {
            List<string> clues = new List<string> { "A3=D1", "A2(C)-A3(C)=1", "A2=B3", "B2=D3", "D2!=C2" };
            Deducer brains = new Deducer(4, 3, new string[] {"", "", "Numeric", ""});
            brains.setClues(clues);
            brains.enterCategoryValues('C', new object[] {1, 2, 3});
            int[,] matrix = brains.go();
            Assert.IsNotNull(matrix);
            int[,] providedMatrix = new int[3,4]{{1,2,1,3},{2,3,3,2},{3,1,2,1}};
            Assert.IsTrue(solutionsMatch(providedMatrix, matrix));
        }

        [TestMethod]
        public void Second_Problem_Test() {
            List<string> clues = new List<string> { "C2=D1", "A1=B1", "A3=C3", "B3!=D3", "B2=D2"};
            Deducer brains = new Deducer(4, 3, new string[] { "", "", "", "" });
            brains.setClues(clues);
            int[,] matrix = brains.go();
            Assert.IsNotNull(matrix);
            int[,] providedMatrix = new int[3, 4] { { 1, 1, 1, 3 }, { 2, 3, 2, 1 }, { 3, 2, 3, 2 } };
            Assert.IsTrue(solutionsMatch(providedMatrix, matrix));
        }

        [TestMethod]
        public void Third_Problem_Test() {
            List<string> clues = new List<string> { "C1=D3", "C1!=B2", "B2!=C2", "C2!=A4", "B1=D1", "B4=A3", "B4!=D4", "C4=A2", "C4!=D2", "B3(A)-C3(A)=1" };
            Deducer brains = new Deducer(4, 4, new string[] { "Numeric", "", "", "" });
            brains.setClues(clues);
            brains.enterCategoryValues('A', new object[] { 1, 2, 3, 4 });
            int[,] matrix = brains.go();
            Assert.IsNotNull(matrix);
            int[,] providedMatrix = new int[4, 4] { { 1, 1, 2, 1 }, { 2, 2, 4, 4 }, { 3, 4, 3, 2 }, { 4, 3, 1, 3 } };
            Assert.IsTrue(solutionsMatch(providedMatrix, matrix));
        }

        private bool solutionsMatch(int[,] providedMatrix, int[,] matrix) {
            List<string> strung1 = new List<string>();
            List<string> strung2 = new List<string>();
            for (int x = 0; x < matrix.GetLength(0); x++) {
                string item1 = "";
                string item2 = "";
                for (int y = 0; y < matrix.GetLength(1); y++) {
                    item1 += providedMatrix[x, y];
                    item2 += matrix[x, y];
                }
                strung1.Add(item1);
                strung2.Add(item2);
            }
            foreach (string s in strung1) {
                if (!strung2.Contains(s)) {
                    return false;
                }
            }
            return true;
        }

    }
}
