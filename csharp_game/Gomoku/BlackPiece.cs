using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Gomoku
{
    class BlackPiece : Piece
    {
        //因為基底類別有需要參數的建構子，故衍伸類別也要設定
        public BlackPiece(int x, int y) : base(x, y)
        {
            this.Image = Properties.Resources.black; //呼叫我上傳的圖片
        }
        public override PieceType GetPieceType()
        {
            return PieceType.BLACK;
        }
    }
}
