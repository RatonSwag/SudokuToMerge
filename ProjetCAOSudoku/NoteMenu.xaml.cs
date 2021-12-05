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
    public partial class NoteMenu : UserControl
    {
        private BoardController controller;

        public NoteMenu(BoardController controller)
        {
            this.controller = controller;

            InitializeComponent();

            DropDownPosition.SelectedValuePath = "Key";
            DropDownPosition.DisplayMemberPath = "Value";
            this.DropDownPosition.Items.Add(new KeyValuePair<int, string>(1, "Haut Gauche"));
            this.DropDownPosition.Items.Add(new KeyValuePair<int, string>(2, "Haut Droit"));
            this.DropDownPosition.Items.Add(new KeyValuePair<int, string>(3, "Bas Gauche"));
            this.DropDownPosition.Items.Add(new KeyValuePair<int, string>(4, "Bas Droit"));
            this.DropDownPosition.Items.Add(new KeyValuePair<int, string>(5, "Centre"));
        }

        private void Set_Note(object sender, RoutedEventArgs e)
        {
            try
            {
                List<char> note = TextBoxNoteInput.Text.ToList();
                bool valid = true;
                foreach(char c in note)
                {
                    if (note.Count(x => x == c) > 1 )
                    {   
                        valid = false;
                        break;
                    }
                }

                if (valid && note.Count() <= controller.board.GetGridSize())
                    controller.AddNoteAt(int.Parse(RowInput.Text), int.Parse(ColumnInput.Text), TextBoxNoteInput.Text, DropDownPosition.SelectedIndex + 1);
            }
            catch (Exception) { }

        }
    }
}