using CellularAutomatonLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CellularAutomatonTests
{
    [TestClass]
    public class UniverseTests
    {
        [TestMethod]
        public void GetCellTest()
        {
            Universe testUniverse = new Universe(10, 10);

            testUniverse.SetCell(2, 2, true);
            Cell testCell = testUniverse.GetCell(2, 2);

            testUniverse.Print();

            Assert.IsTrue(testCell.Alive);
        }

        [TestMethod]
        public void NeighborsTest()
        {
            Universe testUniverse = new Universe(10, 10);

            List<Cell> topLeftNeighbors = testUniverse.GetNeighbors(0, 0);
            Assert.IsTrue(topLeftNeighbors.Count == 3);

            List<Cell> topRightNeighbors = testUniverse.GetNeighbors(9, 0);
            Assert.IsTrue(topRightNeighbors.Count == 3);

            List<Cell> bottomLeftNeighbors = testUniverse.GetNeighbors(0, 9);
            Assert.IsTrue(bottomLeftNeighbors.Count == 3);

            List<Cell> bottomRightNeighbors = testUniverse.GetNeighbors(9, 9);
            Assert.IsTrue(bottomRightNeighbors.Count == 3);

            List<Cell> nonEdgeCellNeighbors = testUniverse.GetNeighbors(4, 4);
            Assert.IsTrue(nonEdgeCellNeighbors.Count == 8);
        }

        [TestMethod]
        public void BlockTest()
        {
            Universe testUniverse = new Universe(4, 4);

            testUniverse.SetCell(1, 1, true);
            testUniverse.SetCell(1, 2, true);
            testUniverse.SetCell(2, 1, true);
            testUniverse.SetCell(2, 2, true);

            Assert.IsFalse(testUniverse.GetCell(0, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(0, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(0, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(0, 3).Alive);

            Assert.IsFalse(testUniverse.GetCell(1, 0).Alive);
            Assert.IsTrue(testUniverse.GetCell(1, 1).Alive);
            Assert.IsTrue(testUniverse.GetCell(1, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 3).Alive);

            Assert.IsFalse(testUniverse.GetCell(2, 0).Alive);
            Assert.IsTrue(testUniverse.GetCell(2, 1).Alive);
            Assert.IsTrue(testUniverse.GetCell(2, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 3).Alive);

            Assert.IsFalse(testUniverse.GetCell(3, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 3).Alive);

            testUniverse.Print();

            testUniverse.Evolve();

            testUniverse.Print();

            Assert.IsFalse(testUniverse.GetCell(0, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(0, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(0, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(0, 3).Alive);

            Assert.IsFalse(testUniverse.GetCell(1, 0).Alive);
            Assert.IsTrue(testUniverse.GetCell(1, 1).Alive);
            Assert.IsTrue(testUniverse.GetCell(1, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 3).Alive);

            Assert.IsFalse(testUniverse.GetCell(2, 0).Alive);
            Assert.IsTrue(testUniverse.GetCell(2, 1).Alive);
            Assert.IsTrue(testUniverse.GetCell(2, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 3).Alive);

            Assert.IsFalse(testUniverse.GetCell(3, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 3).Alive);
        }

        [TestMethod]
        public void BlinkerTest()
        {
            Universe testUniverse = new Universe(5, 5);

            testUniverse.SetCell(1, 2, true);
            testUniverse.SetCell(2, 2, true);
            testUniverse.SetCell(3, 2, true);

            Assert.IsFalse(testUniverse.GetCell(0, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 0).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 1).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 2).Alive);
            Assert.IsTrue(testUniverse.GetCell(1, 2).Alive);
            Assert.IsTrue(testUniverse.GetCell(2, 2).Alive);
            Assert.IsTrue(testUniverse.GetCell(3, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 2).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 3).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 3).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 3).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 3).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 3).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 4).Alive);

            testUniverse.Print();

            testUniverse.Evolve();

            testUniverse.Print();

            Assert.IsFalse(testUniverse.GetCell(0, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 0).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 1).Alive);
            Assert.IsTrue(testUniverse.GetCell(2, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 1).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 2).Alive);
            Assert.IsTrue(testUniverse.GetCell(2, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 2).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 3).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 3).Alive);
            Assert.IsTrue(testUniverse.GetCell(2, 3).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 3).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 3).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 4).Alive);
        }

        [TestMethod]
        public void ToadTest()
        {
            Universe testUniverse = new Universe(6, 6);

            testUniverse.SetCell(2, 2, true);
            testUniverse.SetCell(3, 2, true);
            testUniverse.SetCell(4, 2, true);
            testUniverse.SetCell(1, 3, true);
            testUniverse.SetCell(2, 3, true);
            testUniverse.SetCell(3, 3, true);

            testUniverse.Print();

            testUniverse.Evolve();

            testUniverse.Print();

            Assert.IsFalse(testUniverse.GetCell(0, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 0).Alive);
            Assert.IsFalse(testUniverse.GetCell(5, 0).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 1).Alive);
            Assert.IsTrue(testUniverse.GetCell(3, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 1).Alive);
            Assert.IsFalse(testUniverse.GetCell(5, 1).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 2).Alive);
            Assert.IsTrue(testUniverse.GetCell(1, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 2).Alive);
            Assert.IsTrue(testUniverse.GetCell(4, 2).Alive);
            Assert.IsFalse(testUniverse.GetCell(5, 2).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 3).Alive);
            Assert.IsTrue(testUniverse.GetCell(1, 3).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 3).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 3).Alive);
            Assert.IsTrue(testUniverse.GetCell(4, 3).Alive);
            Assert.IsFalse(testUniverse.GetCell(5, 3).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 4).Alive);
            Assert.IsTrue(testUniverse.GetCell(2, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 4).Alive);
            Assert.IsFalse(testUniverse.GetCell(5, 4).Alive);

            Assert.IsFalse(testUniverse.GetCell(0, 5).Alive);
            Assert.IsFalse(testUniverse.GetCell(1, 5).Alive);
            Assert.IsFalse(testUniverse.GetCell(2, 5).Alive);
            Assert.IsFalse(testUniverse.GetCell(3, 5).Alive);
            Assert.IsFalse(testUniverse.GetCell(4, 5).Alive);
            Assert.IsFalse(testUniverse.GetCell(5, 5).Alive);
        }
    }
}
