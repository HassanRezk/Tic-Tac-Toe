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

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char player = 'X';
        public void init()
        {
            Button[][] t = new Button[3][];
            for (int i = 0; i < 3; ++i)
                t[i] = new Button[3];
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                {
                    t[i][j] = new Button();
                    t[i][j].Width = t[i][j].Height = 100;
                    t[i][j].Name = "_" + i + j;
                    wp.Children.Add(t[i][j]);
                    t[i][j].Click += isClicked;
                }
        }

        void isClicked(Object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int i = int.Parse(b.Name[1] + "");
            int j = int.Parse(b.Name[2] + "");
            b.Content = Convert.ToString(player);
            b.FontSize = 80;
        }

        public MainWindow()
        {
            InitializeComponent();
            init();
        }
    }
}
