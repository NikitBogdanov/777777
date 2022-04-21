using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack
{
    public partial class Form1 : Form
    {
        public int PlayerScore = 0;
        public int OldPlaerScore = 0;
        public int BotScore = 0;
        public bool Pass = false;
        Bitmap Panel = new Bitmap(@"C:\Users\intre\source\repos\Blackjack\Cards\Panel.png");
        Bitmap Back = new Bitmap(@"C:\Users\intre\source\repos\Blackjack\Cards\Back.png");
        List<Card> Cards = new List<Card>();
        List<Card> ReserveCards = new List<Card>();
        List<Card> Reshuffle = new List<Card>();
        public bool EndOfGame = false;
        public int Rnd { get; set; }
        public Form1()
        {
            InitializeComponent();
        }
        public void DrawElement()
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            DirectoryInfo directory = new DirectoryInfo(@"C:\Users\intre\source\repos\Blackjack\Cards\All");
            foreach(FileInfo file in directory.EnumerateFiles("*.png"))
            {                
                switch (file.Name[0])
                {
                    case 'A' :
                        Cards.Add(new Card(1, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case 'J' :
                        Cards.Add(new Card(10, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case 'K':
                        Cards.Add(new Card(10, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case 'Q':
                        Cards.Add(new Card(10, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case 'T':
                        Cards.Add(new Card(10, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case '2':
                        Cards.Add(new Card(2, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case '3':
                        Cards.Add(new Card(3, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case '4':
                        Cards.Add(new Card(4, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case '5':
                        Cards.Add(new Card(5, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case '6':
                        Cards.Add(new Card(6, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case '7':
                        Cards.Add(new Card(7, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case '8':
                        Cards.Add(new Card(8, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                    case '9':
                        Cards.Add(new Card(9, new Bitmap(Image.FromFile(file.FullName)), 140, 140));
                        break;
                }
            }
            ReserveCards = Cards;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Random random = new Random();
            int index = 52;
            while(Reshuffle.Count != 52)
            {
                int temp = random.Next(0, Cards.Count);
                Reshuffle.Add(Cards[temp]);
                Cards.RemoveAt(temp);
            }
            Cards = ReserveCards;
            foreach(Card card in Reshuffle)
            {
                g.DrawImage(card.IMG, new Rectangle(card.X - 60, card.Y - 60, 100, 140));
            }
            g.DrawImage(Back, new Rectangle(80, 80, 100, 140));
            g.DrawImage(Back, new Rectangle(81, 81, 100, 140));
            g.DrawImage(Back, new Rectangle(82, 82, 100, 140));
            g.DrawImage(Back, new Rectangle(83, 83, 100, 140));
            g.DrawImage(Back, new Rectangle(84, 84, 100, 140));
            g.DrawImage(Back, new Rectangle(85, 85, 100, 140));
            g.DrawImage(Back, new Rectangle(86, 86, 100, 140));
            g.DrawImage(Back, new Rectangle(87, 87, 100, 140));                    
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (Card card in Reshuffle)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if ((card.X + Back.Width - 100> Cursor.Position.X) && (card.X + 50 < Cursor.Position.X))
                    {
                        if ((card.Y + Back.Height - 200 > Cursor.Position.Y) && (card.Y + 50 < Cursor.Position.Y))
                        {
                            if(Cursor.Position.Y > ClientRectangle.Height / 2 + 210)
                            {
                                if(card.Number == 1)
                                {
                                    PlayerScore += PlayerScore + 11 > 21 ? 1 : 11;
                                }
                                else
                                {
                                    PlayerScore += card.Number;
                                }
                                button1.Text = $"{PlayerScore}";
                                card.Number = 0;                                
                            }                           
                            card.X = e.X;
                            card.Y = e.Y;
                            Refresh();
                            break;
                        }
                    }
                }
            }            
        }
        public int PositionButton = 700;
        public void BotPlay()
        {
            Random random = new Random();
            Rnd = random.Next(0, Reshuffle.Count);
            if(BotScore == 20)
            {
                if (Pass == true)
                {
                    EndOfGame = true;
                    End();
                }
            }
            else if(PlayerScore > BotScore)
            {
                Reshuffle[Rnd].X = PositionButton;
                Reshuffle[Rnd].Y = 70;
                if (Reshuffle[Rnd].Number == 1)
                {
                    BotScore += BotScore + 11 > 21 ? 1 : 11;
                }
                else
                {
                    BotScore += Reshuffle[Rnd].Number;
                }
                button4.Text = Convert.ToString(BotScore);
                Refresh();
            }
            else if (BotScore > 18)
            {
                switch (random.Next(0, 2))
                {
                    case 0:
                        if (Pass == true)
                        {
                            EndOfGame = true;
                            End();
                        }
                        break;
                    case 1:
                        if (Reshuffle[Rnd].Number != 0)
                        {
                            Reshuffle[Rnd].X = PositionButton;
                            Reshuffle[Rnd].Y = 70;
                            if (Reshuffle[Rnd].Number == 1)
                            {
                                BotScore += BotScore + 11 > 21 ? 1 : 11;
                            }
                            else
                            {
                                BotScore += Reshuffle[Rnd].Number;
                            }
                            button4.Text = Convert.ToString(BotScore);
                            Refresh();
                        }
                        else
                        {
                            BotPlay();
                        }
                        break;
                }
            }
            else if(BotScore == 18 || BotScore == 17 || BotScore == 16 || BotScore == 15)
            {
                switch (random.Next(0, 3))
                {
                    case 0:
                        if(Pass == true)
                        {
                            EndOfGame = true;
                            End();
                        }                        
                        break;
                    default:
                        if (Reshuffle[Rnd].Number != 0)
                        {
                            Reshuffle[Rnd].X = PositionButton;
                            Reshuffle[Rnd].Y = 70;
                            if (Reshuffle[Rnd].Number == 1)
                            {
                                BotScore += BotScore + 11 > 21 ? 1 : 11;
                            }
                            else
                            {
                                BotScore += Reshuffle[Rnd].Number;
                            }
                            button4.Text = Convert.ToString(BotScore);
                            Refresh();
                        }
                        else
                        {
                            BotPlay();
                        }
                        break;
                }
            }
            else if (Reshuffle[Rnd].Number != 0)
            {
                Reshuffle[Rnd].X = PositionButton;
                Reshuffle[Rnd].Y = 70;
                if(Reshuffle[Rnd].Number == 1)
                {
                    BotScore += BotScore + 11 > 21 ? 1 : 11;
                }
                else
                {
                    BotScore += Reshuffle[Rnd].Number;
                }
                button4.Text = Convert.ToString(BotScore);
                Refresh();
            }
            else if(Reshuffle[Rnd].Number == 0)
            {
                BotPlay();
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
                
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(PlayerScore > OldPlaerScore)
            {
                BotPlay();
                PositionButton += 10;
                OldPlaerScore = PlayerScore;
            }
        }
        public void End()
        {
            if (BotScore > PlayerScore)
            {
                PlayerScore = 0;
                BotScore = 0;
                MessageBox.Show("Вы проиграли!");
                Application.Restart();
            }
            else if (BotScore == PlayerScore)
            {
                PlayerScore = 0;
                BotScore = 0;
                MessageBox.Show("Ничья!");
                Application.Restart();
            }
            else
            {
                PlayerScore = 0;
                BotScore = 0;
                MessageBox.Show("Вы выиграли!");
                Application.Restart();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Pass = true;
            while(EndOfGame == false)
            {
                BotPlay();
            }         
        }
        private void NewTimer_Tick(object sender, EventArgs e)
        {
            if(BotScore > 21)
            {
                EndOfGame = true;
                PlayerScore = 0;
                BotScore = 0;
                MessageBox.Show("Вы выиграли!");
                Application.Restart();                
            }
            else if(PlayerScore > 21)
            {
                EndOfGame = true;
                PlayerScore = 0;
                BotScore = 0;
                MessageBox.Show("Вы проиграли!");
                Application.Restart();
            }
            else if(PlayerScore == 21)
            {
                EndOfGame = true;
                PlayerScore = 0;
                BotScore = 0;
                MessageBox.Show("Вы выиграли!");
                Application.Restart();
            }
            else if (BotScore == 21)
            {
                EndOfGame = true;
                PlayerScore = 0;
                BotScore = 0;
                MessageBox.Show("Вы проиграли!");
                Application.Restart();
            }
        }
    }
}
