using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetCAOSudoku.Ressources;

namespace TestSudoku
{
    [TestClass]
    public class SudokuTests
    {
        private BoardMock board;
        private GridStateChecker checker;
        private GridSaver saver;

        [TestInitialize]
        public void Setup()
        {
            board = new();
            //Ligne 1: 1, 0, 0, 0, 1, 0, 0, 0, 0 - False
            board.SetAnswerAt(0, 0, 1);
            board.SetAnswerAt(0, 4, 1);

            //Colonne 1: 1, 0, 0, 0, 1, 0, 0, 0, 0 - False
            board.SetAnswerAt(4, 0, 1);

            //Boite 1: 1, 0, 0, 0, 0, 0, 0, 0, 1 - False
            board.SetAnswerAt(2, 2, 1);

            //Ligne 2: 1, 0, 0, 0, 3, 0, 0, 0, 0 - True
            //Colonne 2: 1, 0, 0, 0, 3, 0, 0, 0, 0 - True
            board.SetAnswerAt(4, 4, 3);

            //Boite 2: 4, 0, 0, 0, 3, 0, 0, 0, 0 - True
            board.SetAnswerAt(3, 3, 4);

            checker = new(board.grid, board.GetGridSize(), board.GetBoxSize());

            saver = new();
        }

        [TestMethod]
        public void LineCheckTrue()
        {
            Assert.AreEqual(true, checker.CheckLine(4));
        }

        [TestMethod]
        public void LineCheckFalse()
        {
            Assert.AreEqual(false, checker.CheckLine(0));
        }

        [TestMethod]
        public void ColumnCheckTrue()
        {
            Assert.AreEqual(true, checker.CheckColumn(4));
        }

        [TestMethod]
        public void ColumnCheckFalse()
        {
            Assert.AreEqual(false, checker.CheckColumn(0));
        }

        [TestMethod]
        public void BoxCheckTrue()
        {
            Assert.AreEqual(true, checker.CheckBox(5, 5));
        }

        [TestMethod]
        public void BoxCheckFalse()
        {
            Assert.AreEqual(false, checker.CheckBox(0, 0));
        }

        [TestMethod]
        public void SaveAndLoad()
        {
            saver.SaveGrid(board.grid, board.GetGridSize());
            Assert.AreEqual(board, new BoardMock(saver.LoadGrid(board.GetGridSize())));
        }
    }
}
