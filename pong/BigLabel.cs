using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pong
{
    class BigLabel: Panel
    {
        public string text;

        public BigLabel(Panel parent, string player, string movema, string accelerat)
        {
            Font font = new Font("Microsoft Sans Serif", this.Width / 5);




            this.Width = parent.Width;
            this.Height = parent.Height;
            this.Left = 0;
            this.Top = 0;
            this.BringToFront();


            Label pname = new Label();
            pname.Width = this.Width;
            pname.TextAlign = ContentAlignment.TopCenter;
            pname.Height = this.Height / 5;
            pname.ForeColor = Color.White;
            pname.Text = player;
            pname.Font = font;
            this.Controls.Add(pname);

            Label movemant = new Label();
            movemant.Width = this.Width;
            movemant.Height = this.Height / 5;
            movemant.ForeColor = Color.White;
            movemant.TextAlign = ContentAlignment.TopCenter;
            movemant.Left = this.Width / 10;
            movemant.Top = this.Height / 2;
            movemant.Font = font;
            movemant.Text = movema;
            this.Controls.Add(movemant);

            Label acsel = new Label();
            acsel.Width = this.Width;
            acsel.Height = this.Height / 5;
            acsel.ForeColor = Color.White;
            acsel.TextAlign = ContentAlignment.TopCenter;
            acsel.Text = accelerat;
            acsel.Font = font;
            acsel.Left = this.Width / 10;
            acsel.Top = this.Width / 10 * 9;
            this.Controls.Add(acsel);
        }

    }
}
