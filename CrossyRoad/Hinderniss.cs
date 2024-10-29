using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    public class Hinderniss
    {
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0

        #region Eigenschaften
        public Rectangle rand;
        private Point testpunkt; //Ist das Hinderniss immer noch in der Startzone?

        //Wo befindet sich der Frosch im Vergleich zur Spielbreite: 0.00 heißt ganz links, 1.00 heißt ganz rechts.
        //Man kann es auch wie eine Prozentrechnung betrachten.
        public double Skallierer;

        private int geschwindigkeit;
        private bool vonLinksNachRechts;

        //private int idBahn;
        private bool zug;

        private Image bild;
        #endregion

        //Auto erstellen
        public Hinderniss(Bahn bahn, bool zug)
        {
            //Wenn bool zug true ist, wird es ein Zug. Wenn der boolean false ist ein Auto.

            this.zug = zug;

            int breite;

            if (zug)
            {
                //Hinderniss (Breite) anpassen ---> Zug.
                breite = HindernissBreiteAufBahn(bahn.Rand.Size.Width, true);
            }
            else
            {
                //Hinderniss (Breite) anpassen ---> Auto.
                breite = HindernissBreiteAufBahn(bahn.Rand.Size.Width, false);
            }

            //Zug und Auto haben die leiche Höhe.
            int hoehe = bahn.Rand.Size.Height;

            geschwindigkeit = bahn.Geschwindigkeit;

            vonLinksNachRechts = bahn.VonLinksNachRechts;

            rand.Size = new Size(breite, hoehe);

            if (bahn.VonLinksNachRechts)
            {
                rand.Location = new Point(bahn.Rand.Location.X - breite, bahn.Rand.Location.Y);
                testpunkt = new Point(rand.Location.X, rand.Location.Y);

                if(zug)
                {
                    //Wenn es ein Zug ist, bekommmt es das  Abbild eines Zuges.

                    //Nicht elegant
                    string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    projectPath = projectPath.Remove(projectPath.Length - 4, 4);
                    string pfad = Path.Combine(projectPath, "Bilder\\zug.png");

                    bild = Image.FromFile(pfad);
                    //Nicht elegant

                }
                else
                {
                    //Wenn es ein Auto ist, bekommt es das Abbild eines Autos, dass nach rechts ausgerichtet ist.

                    //Nicht elegant
                    string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    projectPath = projectPath.Remove(projectPath.Length - 4, 4);
                    string pfad = Path.Combine(projectPath, "Bilder\\autoNachRechts.png");

                    bild = Image.FromFile(pfad);
                    //Nicht elegant

                }
            }
            else
            {
                rand.Location = new Point(bahn.Rand.Width, bahn.Rand.Location.Y);
                testpunkt = new Point(rand.Location.X + breite, rand.Location.Y);

                if (zug)
                {
                    //Wenn es ein Zug ist, bekommmt es das  Abbild eines Zuges.

                    //Nicht elegant
                    string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    projectPath = projectPath.Remove(projectPath.Length - 4, 4);
                    string pfad = Path.Combine(projectPath, "Bilder\\zug.png");

                    bild = Image.FromFile(pfad);
                    //Nicht elegant

                }
                else
                {
                    //Wenn es ein Auto ist, bekommt es das Abbild eines Autos, dass nach links ausgerichtet ist.

                    //Nicht elegant
                    string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    projectPath = projectPath.Remove(projectPath.Length - 4, 4);
                    string pfad = Path.Combine(projectPath, "Bilder\\autoNachLinks.png");

                    bild = Image.FromFile(pfad);
                    //Nicht elegant

                }
            }

        }

        public Image Bild
        {
            get
            {
                return bild;
            }
        }

        public bool Zug
        {
            get
            {
                return zug;
            }
        }

        public Rectangle Rand
        {
            get
            {
                return rand;
            }
            set
            {
                rand = value;
            }
        }

        public int Geschwindigkeit
        {
            get
            {
                return geschwindigkeit;
            }
        }

        public Point Testpunkt
        {
            get
            {
                if (vonLinksNachRechts)
                {
                    testpunkt = new Point(rand.Location.X, rand.Location.Y);
                }
                else
                {
                    testpunkt = new Point(rand.Location.X + rand.Width, rand.Location.Y);
                }

                return testpunkt;
            }
        }

        public bool VonLinksNachRechts
        {
            get
            {
                return vonLinksNachRechts;
            }
        }

        //Hinderniss bewegt sich je nachdem nach rechts oder links. Und so schnell wie es durch
        //die Variable "Geschwindigkeit" angegeben ist.
        public void Bewegen()
        {
            int xLocation = rand.Location.X;
            int yLocation = rand.Location.Y;

            if (vonLinksNachRechts)
            {
                rand.Location = new Point(xLocation + geschwindigkeit, yLocation);
            }
            else
            {
                rand.Location = new Point(xLocation - geschwindigkeit, yLocation);
            }
        }

        //Wenn bool zug false ist, ist es auf ein Auto bezogen. Ansonsten auf einen Zug. Je nachdem wird die Breite des
        //Hindernisses korrekt angepasst.
        public int HindernissBreiteAufBahn(int bahnBreite, bool zug)
        {
            int hBreite;

            if (zug)
            {
                hBreite = bahnBreite / 4;

            }
            else
            {
                hBreite = bahnBreite / 14;
            }

            return hBreite;
        }

    }
}
