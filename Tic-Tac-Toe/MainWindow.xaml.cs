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
        public const int N = 3;

        char[][] grid;
        List<string> src;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        Button[][] t;

        public void Init()
        {
            t = new Button[N][];
            grid = new char[N][];
            for (int i = 0; i < N; ++i)
            {
                t[i] = new Button[N];
                grid[i] = new char[N];
            }
            for (int i = 0; i < N; ++i)
                for (int j = 0; j < N; ++j)
                {
                    t[i][j] = new Button();
                    t[i][j].Width = t[i][j].Height = 100;
                    t[i][j].Name = "_" + i + j;
                    t[i][j].FontSize = 80;
                    grid[i][j] = '-';
                    wp.Children.Add(t[i][j]);
                    t[i][j].Click += IsClicked;
                }
            src = new List<string>();
            src.Add("Easy");
            src.Add("Medium");
            src.Add("Hard");
            cmbx.ItemsSource = src;
        }

        bool play = true;

        int counter;

        public void IsClicked(Object sender, EventArgs e)
        {
            counter = 0;
            if (!play) return;
            Button b = (Button)sender;
            int i = int.Parse(b.Name[1] + "");
            int j = int.Parse(b.Name[2] + "");
            if (grid[i][j] == '-')
            {
                grid[i][j] = player;
                b.Content = Convert.ToString(player);
                char[][] grid2 = new char[N][];
                for (int k = 0; k < N; ++k)
                {
                    grid2[k] = new char[N];
                    for (int l = 0; l < N; ++l)
                        grid2[k][l] = grid[k][l];
                }
                //MessageBox.Show("After click\n" + PrintGrid());
                int w = Check();
                if (w == -2)
                {
                    MyPair v;
                    if (cmbx.SelectedIndex == 0)
                    {
                        Random rand = new Random();
                        int p = rand.Next(1, 10);
                        if (p < 7)
                            v = GetNextMove(false);
                        else
                            v = GetNextMove(true);
                    }
                    else if (cmbx.SelectedIndex == 2)
                        v = GetNextMove(true);
                    else
                    {
                        Random rnd = new Random();
                        int p = rnd.Next(0, 10);
                        if (p >= 6) v = GetNextMove(true);
                        else v = GetNextMove(false);
                    }
                    for (int k = 0; k < N; ++k)
                    {
                        grid[k] = new char[N];
                        for (int l = 0; l < N; ++l)
                            grid[k][l] = grid2[k][l];
                    }
                    grid[v.i][v.j] = GetC(true);
                    //MessageBox.Show(v + "\n" + PrintGrid());
                    t[v.i][v.j].Content = Convert.ToString(GetC(true));
                    w = Check();
                    if (w == 1)
                    {
                        MessageBox.Show("You Lose");
                        play = false;
                    }
                }
                else if (Check() == 0)
                {
                    MessageBox.Show("Draw");
                    play = false;
                }
                else if (w == -1)
                {
                    MessageBox.Show("You Win");
                    play = false;
                }
            }
           // MessageBox.Show(""+counter);
        }

        private struct MyPair
        {
            public int val;
            public int i;
            public int j;

            public MyPair(int val, int i, int j) 
            {
                this.val = val;
                this.i = i;
                this.j = j;
            }

            public MyPair(int val)
            {
                this.val = val;
                i = j = -1;
            }

            public override string ToString() 
            {
                return val + " " + i + " " + j;
            }
         }

        private string PrintGrid()
        {
            string ret = "";
            for (int i = 0; i < N; ++i)
            {
                for (int j = 0; j < N; ++j)
                    ret += grid[i][j] + " ";
                ret += "\n";
            }
            return ret;
        }

        private int GetReturnValue(char c)
        {
            return c == player ? -1 : 1;
        }

        private int Check()
         /*
          *  player win return -1;
          *  draw return 0;
          *  ai win return 1;
          *  neither all return -2;
          */
        {
            bool row, col, diag1 = true, diag2 = true;
            bool hasDash = false;
            for (int i = 0; i < N; ++i)
            {
                row = true;
                col = true;
                diag1 &= (grid[0][0] == grid[i][i]);
                diag2 &= (grid[0][N - 1] == grid[i][N - 1 - i]);
                for (int j = 0; j < N; ++j)
                {
                    if (grid[i][j] == '-') hasDash = true;
                    row &= (grid[i][0] == grid[i][j]);
                    col &= (grid[0][i] == grid[j][i]);
                }
                if (row && grid[i][0] != '-') return GetReturnValue(grid[i][0]);
                if (col && grid[0][i] != '-') return GetReturnValue(grid[0][i]);
            }
            if (diag1 && grid[0][0] != '-') return GetReturnValue(grid[0][0]);
            if (diag2 && grid[0][N - 1] != '-') return GetReturnValue(grid[0][N - 1]);
            return hasDash ? -2 : 0;
        }

        //val, i, j
        private MyPair GetNextMove(bool maximizer) 
        {
            ++counter;
            int v = Check();
            //MessageBox.Show("v = " + v + "\n\n" + PrintGrid());
            if (v != -2)
            {
                //MessageBox.Show("\n\n" + (maximizer ? "maximizer" : "minimizer") + "\n\n"+ PrintGrid());
                return new MyPair(v);
            }
            MyPair ret = new MyPair(maximizer ? -50 : 50);
            for (int i = 0; i < N; ++i) 
                for (int j = 0; j < N; ++j)
                    if (grid[i][j] == '-')
                    {
                        grid[i][j] = GetC(maximizer);
                        MyPair r = GetNextMove(!maximizer);
                        grid[i][j] = '-';
                        if ((maximizer && r.val > ret.val) || (!maximizer && r.val < ret.val))
                        {
                            ret.val = r.val;
                            ret.i = i;
                            ret.j = j;
                        }
                        if ((maximizer && ret.val == 1) || (!maximizer && ret.val == -1)) 
                            // alpha beta pruning
                            return ret;
                    }
            return ret;
        }

        private char GetC(bool maximizer)
        {
            if(maximizer) return player == 'X' ? '0' : 'X';
            return player;
        } 

        private string GridToString()
        {
            string ret = "";
            for (int i = 0; i < N; ++i)
                for (int j = 0; j < N; ++j)
                    ret += grid[i][j];
            return ret;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0 ; i < N ; ++i)
                for (int j = 0; j < N; ++j)
                {
                    grid[i][j] = '-';
                    t[i][j].Content = "";
                }
            play = true;
        }

    }
}
