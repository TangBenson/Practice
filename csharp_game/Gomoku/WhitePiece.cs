using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gomoku
{
    class WhitePiece : Piece
    {
        public WhitePiece(int x, int y) : base(x, y)
        {
            this.Image = Properties.Resources.white; //呼叫我上傳的圖片
        }
        public override PieceType GetPieceType()
        {
            return PieceType.WHITE;
        }
    }
}
