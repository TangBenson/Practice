using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms; //為了繼承PictureBox
using System.Drawing; //為了設定BackColor、Location、Size

namespace Gomoku
{
    //piece不是要建立的旗子，所以設成abstract
    abstract class Piece : PictureBox
    {
        private static readonly int IMAGE_WIDTH = 50;
        public Piece(int x,int y)
        {
            this.BackColor = Color.Transparent;
            this.Location = new Point(x-IMAGE_WIDTH/2, y-IMAGE_WIDTH/2);
            this.Size = new Size(IMAGE_WIDTH, IMAGE_WIDTH);
        }

        public abstract PieceType GetPieceType();
    }
}
