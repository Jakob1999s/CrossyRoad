using System.Diagnostics;
using System.Net.Http.Headers;

namespace CrossyRoad
{
    public partial class FrmCrossyRoad : Form
    {
        private bool handleEvents = false;

        public FrmCrossyRoad()//
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0

        Benutzeroberflaeche b;


        private void FrmCrossyRoad_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;

            //Benutezroberfläche erstellen 
            b = new Benutzeroberflaeche(schwierigkeitsgrad: 3, this.ClientSize.Width, this.ClientSize.Height);



            tmrCrossyRoad.Start();

            //btnPause das Pausebild zuweisen.
            //Image iPause = Image.FromFile("Pause.JPEG");
            //Bitmap bitmap = new Bitmap(iPause, pnlPause.Size);
            //iPause = (Image)bitmap;
            //pnlPause.BackgroundImage = iPause;

            handleEvents = true;
        }

        private void FrmCrossyRoad_Paint(object sender, PaintEventArgs e)
        {
            if (!handleEvents)
            {
                return;
            }
            Graphics g = e.Graphics;

            #region Bahnen zeichnen
            //Startbahn
            g.FillRectangle(b.BrStartBahn, b.Bahnen[b.Bahnen.Length - 1].Rand);

            //Blinken:
            //Wenn es gün blinkt, soll die Bahn zu dem Zeitpunkt weiß sein.
            if (!b.BoolBlinken)
            {
                //Zielbahn
                g.FillRectangle(b.BrZielBahn, b.Bahnen[0].Rand);
            }


            //mittlere Bahnen
            for (int i = 1; i < b.Bahnen.Length - 1; i += 2)
            {
                g.FillRectangle(b.BrBahnHell, b.Bahnen[i].Rand);

            }

            for (int i = 2; i < b.Bahnen.Length - 1; i += 2)
            {
                g.FillRectangle(b.BrBahnDunkel, b.Bahnen[i].Rand);
            }

            //Blinken:
            //Falls nötig grün blinken
            if (b.BoolBlinken /*&& b.GruenBlinken */)
            {
                b.Blinken();

                if (b.GruenBlinken)
                {
                    g.FillRectangle(new SolidBrush(Color.Green), b.Bahnen[0].Rand);
                }
            }
            //

            //Bahnränder zeichnen
            foreach (Bahn b in b.Bahnen)
            {
                g.DrawRectangle(new Pen(Color.Black), b.Rand);
            }
            #endregion

            #region Hindernisse zeichnen
            foreach (Bahn b in b.Bahnen)
            {
                foreach (Hinderniss h in b.Hindernisse)
                {
                    g.DrawImage(h.Bild, h.Rand);
                }
            }
            #endregion

            //Frosch zeichnen
            g.DrawImage(b.frosch.Bild, b.frosch.Rand);


        }


        private void FrmCrossyRoad_SizeChanged(object sender, EventArgs e)
        {
            if (!handleEvents)
            {
                return;
            }

            tmrCrossyRoad.Stop();

            this.ResizeRedraw = true;

            b.AlleGroessenAnpassen(this.ClientSize.Width, this.ClientSize.Height);

            this.Refresh();

            tmrCrossyRoad.Start();
        }



        private void tmrCrossyRoad_Tick(object sender, EventArgs e)
        {
            if (!handleEvents)
            {
                return;
            }
            #region Hinderniss erstellen

            //Wahrscheinlichkeit dass ein Hinderniss (Auto oder Zug) erstellt wird.
            int hindernissErstellen = b.R.Next(0, 15);

            //Auto erstellen
            if (hindernissErstellen == 0)
            {
                int id;

                do
                {
                    //Random Bahn auswählen
                    id = b.R.Next(1, b.Bahnen.Length - 1);

                } while (!b.BahnBesetzbar(id));//Prüfen die ausgewählte Bahn b esetzbar ist.

                if (b.BahnBesetzbar(id))
                {
                    b.BahnBesetzen(id);
                }
            }
            #endregion

            #region Ready
            //Hinderniss entfernen, sofern es nicht mehr sichtbar ist 
            foreach (Bahn b in b.Bahnen)
            {
                b.HindernissEntfernen();
            }

            //Hindernisse werden bewegt
            foreach (Bahn b in b.Bahnen)
            {
                foreach (Hinderniss h in b.Hindernisse)
                {
                    h.Bewegen();
                }
            }

            //Bahn wechselt automatisch die Richtung. Sobald sich keine Hindernisse mehr auf ihr befinden.
            b.BahnLeer();
            #endregion

            //Wo befinden sich Objekte im Vergleich zu der Breite des Spielfelds.
            b.SkalliererBerechnen(this.ClientSize.Width);

            this.Refresh();
        }

        //Tastenbenutzung

        //private void btnPause_Click(object sender, EventArgs e)
        //{
        //    if (!b.Pause)
        //    {
        //        tmrCrossyRoad.Stop();
        //        b.Pause = true;
        //    }
        //    else
        //    {

        //        tmrCrossyRoad.Start();
        //        b.Pause = false;
        //    }

        //}



        private void FrmCrossyRoad_KeyDown(object sender, KeyEventArgs e)
        {
            if (!handleEvents)
            {
                return;
            }
            //Wenn die Bewegung des Frosches gesperrt ist, dann die Ereignisbehnadlungsmethode direkt verlsassen.
            //Das habe ich in jede "key." Bedingung mit eingefügt
            if (e.KeyCode == Keys.Up && !b.frosch.BewegungSperren)
            {
                b.frosch.NachOben();

                //Wenn der Frosch auf der Bahn "0" angekommen ist, blinkt die Bahn grün.
                if (b.frosch.IdAktuelleBahn == 0)
                {
                    b.frosch.BewegungSperren = true;
                    b.Blinken();
                }
            }

            if (e.KeyCode == Keys.Down && !b.frosch.BewegungSperren)
            {
                b.frosch.NachUnten();
            }

            if (e.KeyCode == Keys.Left && !b.frosch.BewegungSperren)
            {
                b.frosch.NachLinks();
            }

            if (e.KeyCode == Keys.Right && !b.frosch.BewegungSperren)
            {
                b.frosch.NachRechts();
            }


            this.Refresh();
        }

    }



    //Aufgaben:

    //(Funktioniert)-----------------(Funktioniert)-----------------(Funktioniert)

    //Ich bin grade dabei die Startzonen zutesten. Ich erschaffe ein
    //Hinderniss auf der gleichen Bahn.Jeden einzelnen Tick. Es soll
    //aber nur erfolgreich sein sofern sich kein altes Hinderniss in der 
    //Startzone befindet.

    //Ich beschränke die Hindernissanzahl auf einer Bahn undwerde sie dann sperren.

    //Autopilot: wenn die Bahn erneut frei ist, soll das Programm automatisch eine neue 
    //Richtung erstellen und mit dem draufsetzen der Hindernisse fortfahren.


    //Anzahl der zugelassenen Hindernisse random bestimmen (mit Legitimierungen)

    //Geschwindigkeit wird ebenfalls geändert

    //Jetzt werde ich die Bahnen random auswählen.

    //(Funktioniert)-----------------(Funktioniert)-----------------(Funktioniert)


    //Image a = new Image();
    //Size

}