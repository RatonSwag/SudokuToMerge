using ProjetCAOSudoku.Ressources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlLibrary1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AnswerMenu : UserControl
    {
        private BoardController controller;
        public AnswerMenu(BoardController controller)
        {
            this.controller = controller;
            InitializeComponent();
        }

        private void Set_Answer(object sender, RoutedEventArgs e)
        {
            try
            {
                int selectedRow = int.Parse(RowInput.Text);
                int selectedColumn = int.Parse(ColumnInput.Text);
                int answer = int.Parse(TextBoxAnswerInput.Text);

                controller.SetAnswerAt(selectedRow, selectedColumn, answer);
                controller.view.DrawGrid(controller.board.grid);
            }
            catch (Exception) { }
        }   
    }
}