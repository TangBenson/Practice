using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Form1 : Form
    {
        private Game game = new Game();

        //以下移至Game.cs宣告
        //private Board board = new Board();
        //private PieceType nextPieceType = PieceType.BLACK;

        public Form1()
        {
            InitializeComponent();

            //測試用
            //this.Controls.Add(new BlackPiece(10, 20));
            //this.Controls.Add(new WhitePiece(100, 200));
        }


        //滑鼠指標點擊的事件判斷，第2個參數表示滑鼠按下去的位置
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // [重構] 改善程式碼可讀性，Form1.cs的工作變少(只處理使用者互動)，Game.cs負責遊戲相關，分工更明確
            Piece piece = game.PlaceAPiece(e.X, e.Y);
            if (piece != null)
            {
                this.Controls.Add(piece);

                if (game.Winner==PieceType.BLACK)
                {
                    MessageBox.Show("黑贏");
                }
                else if(game.Winner==PieceType.WHITE)
                {
                    MessageBox.Show("白贏");
                }
            }

            //改寫至Game.cs
            //Piece piece = board.PlaceAPiece(e.X, e.Y, nextPieceType);
            //if (piece != null)
            //{
            //    this.Controls.Add(piece);

            //    if (nextPieceType == PieceType.BLACK)
            //        nextPieceType = PieceType.WHITE;
            //    else if(nextPieceType == PieceType.WHITE)
            //        nextPieceType = PieceType.BLACK;
            //}

            //重複的程式碼太多，改寫成上面的樣子
            //if (isBlack)
            //{
            //    Piece piece = board.PlaceAPiece(e.X, e.Y, PieceType.BLACK);
            //    if(piece != null)
            //    {
            //        this.Controls.Add(piece);
            //        isBlack = false;
            //    }

            //    //this.Controls.Add(new BlackPiece(e.X, e.Y));
            //    //isBlack = false;
            //}
            //else
            //{
            //    Piece piece = board.PlaceAPiece(e.X, e.Y, PieceType.WHITE);
            //    if (piece != null)
            //    {
            //        this.Controls.Add(piece);
            //        isBlack = true;
            //    }
            //    //this.Controls.Add(new WhitePiece(e.X, e.Y));
            //    //isBlack = true;
            //}
        }

        //滑鼠指標移動的事件判斷
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (game.CanBePlaced(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
