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
using WpfControlLibrary1;

namespace ProjetCAOSudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int GridTileSize = 90;
        private BoardController controller;

        public MainWindow()
        {
            InitializeComponent();
            controller = new BoardController(this);
            DataContext = controller;
        }

        public void DrawGrid(Tile[][] grid)
        {
            //Dessine la grille de sudoku
            for(int ctrRow = 0; ctrRow < grid.Length; ctrRow++)
            {
                for(int ctrColumn = 0; ctrColumn < grid[ctrRow].Length; ctrColumn++)
                {
                    Grid tile = CreateTile(grid[ctrRow][ctrColumn]);
                    SudokuGrid.Children.Add(tile);
                    Canvas.SetTop(tile, ctrColumn * GridTileSize);
                    Canvas.SetLeft(tile, ctrRow * GridTileSize);
                }
            }

            //Dessine les boîte (les lignes plus épaisses)
            for(int ctrRow = 0; ctrRow < grid.Length / 3; ctrRow++)
            {
                for(int ctrColumn = 0; ctrColumn < grid[ctrRow].Length / 3; ctrColumn++)
                {
                    Rectangle box = CreateBox();
                    SudokuGrid.Children.Add(box);
                    Canvas.SetTop(box, ctrColumn * (GridTileSize * 3));
                    Canvas.SetLeft(box, ctrRow* (GridTileSize * 3));
                }
            }
        }

        private Grid CreateTile(Tile tile)
        {
            var grid = new Grid();
            Width = GridTileSize;
            Height = GridTileSize;
            grid.Children.Add(CreateRectangle(tile));
            grid.Children.Add(new TextBlock() { HorizontalAlignment = HorizontalAlignment.Center,VerticalAlignment = VerticalAlignment.Center, Text = tile.answer.ToString(), FontSize = 40});

            foreach(Note n in tile.notes)
            {
                grid.Children.Add(CreateNote(n));
            }
            return grid;
        }

        private Rectangle CreateRectangle(Tile tile)
            => new Rectangle
            {
                Width = GridTileSize,
                Height = GridTileSize,
                Stroke = Brushes.Black,
                StrokeThickness = 0.5,
                Fill = tile.isValid switch
                {
                    true => Brushes.White,
                    false => Brushes.Red
                }
            };

        private Rectangle CreateBox()
            => new Rectangle
            {
                Width = GridTileSize * 3,
                Height = GridTileSize * 3,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Fill = Brushes.Transparent
            };
        private TextBlock CreateNote(Note note)
        {
            TextBlock txtNote = new TextBlock();
            txtNote.FontSize = 20;

            switch (note.position)
            {
                //Top Left
                case 1:
                    {
                        txtNote.HorizontalAlignment = HorizontalAlignment.Left;
                        txtNote.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    }
                //Top Right
                case 2:
                    {
                        txtNote.HorizontalAlignment = HorizontalAlignment.Right;
                        txtNote.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    }
                //Bottom Left
                case 3:
                    {
                        txtNote.HorizontalAlignment = HorizontalAlignment.Left;
                        txtNote.VerticalAlignment = VerticalAlignment.Bottom;
                        break;
                    }
                //Bottom Right
                case 4:
                    {
                        txtNote.HorizontalAlignment = HorizontalAlignment.Right;
                        txtNote.VerticalAlignment = VerticalAlignment.Bottom;
                        break;
                    }
                //Center
                case 5:
                    {
                        txtNote.HorizontalAlignment = HorizontalAlignment.Center;
                        txtNote.VerticalAlignment = VerticalAlignment.Center;
                        txtNote.FontSize = 15;
                        break;
                    }
            }

            txtNote.Text = note.ToDisplay();
            txtNote.Foreground = Brushes.Blue;

            return txtNote;
        }
        public void ShowAnswerMenu()
        {
            Menu.Child = new AnswerMenu(controller);
        }

        public void ShowNoteMenu()
        {
            Menu.Child = new NoteMenu(controller);
        }

        private void RadioButton_Checked_Answer(object sender, RoutedEventArgs e)
        {
            Menu.Child = new AnswerMenu(controller);
        }

        private void RadioButton_Checked_Note(object sender, RoutedEventArgs e)
        {
            Menu.Child = new NoteMenu(controller);
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            controller.SaveBoard();
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e) 
        {
            controller.LoadBoard();
            DrawGrid(controller.board.grid);
            DataContext = controller;
        }
    }
}
