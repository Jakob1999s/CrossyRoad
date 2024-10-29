using CrossyRoad.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    internal class Frosch
    {
        //Auf welcher Bahn befindet sich der Frosch?
        private int idAktuelleBahn;
        
        //Auf welcher ist der Frosch gestartet?
        private int idStartBahn;

        public  int BahnBreite;

        //Wo befindet sich der Frosch im Vergleich zur Spielbreite: 0.00 heißt ganz links, 1.00 heißt ganz rechts.
        //Man kann es auch wie eine Prozentrechnung betrachten.
        public double Skallierer;

        private Rectangle rand;
        private Image bild;
        private int anzahlBahnen;

        public bool BewegungSperren = false;

        public Frosch(Bahn b)
        {
            idAktuelleBahn = b.IdBahn;

            idStartBahn = b.IdBahn;

            BahnBreite = b.Rand.Width;

            //Frosch ist gleich und hoch.
            int hoehe = b.Rand.Height;
            int breite = b.Rand.Height;

            int xLocation = (b.Rand.Width/2)-(breite/2);
            int yLocation = b.Rand.Y;

            rand = new Rectangle(xLocation,yLocation,breite,hoehe);

            //Nicht elegant
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            projectPath = projectPath.Remove(projectPath.Length-4,4);
            string pfad = Path.Combine(projectPath, "Bilder\\frosch.png");

            bild = Image.FromFile(pfad);
            //Nicht elegant
        }

        public int IdAktuelleBahn
        {
            get
            {
                return idAktuelleBahn;
            }
            set
            {
                idAktuelleBahn = value;
            }
        }

        public Image Bild
        {
            get
            {
                return bild;
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

        public void NachOben()
        {
            //Der Frosch kann nicht höher als bahn[0] laufen.
            if(idAktuelleBahn !=0)
            {
                rand.Location = new Point(rand.X, rand.Y - rand.Height);
                idAktuelleBahn--;
            }
        }
        public void NachUnten()
        {
            //Der Frosch kann nicht niederiger als die letzte Bahn laufen.
            if(idAktuelleBahn != idStartBahn)
            {
                rand.Location = new Point(rand.X, rand.Y + rand.Height);
                idAktuelleBahn++;
            }
        }
        public void NachLinks()
        {
            //Der Frosch kann nicht links aus dem Blickfeld laufen.
            if(rand.X >  0 + rand.Width / 2)
            {
                rand.Location = new Point(rand.X - rand.Width, rand.Y);
            }
        }
        public void NachRechts()
        {
            //Der Frosch kann nicht rechgts aus dem Blickfeld laufen.
            if (rand.X < BahnBreite-rand.Width)
            {
                rand.Location = new Point(rand.X + rand.Width, rand.Y);
            }
        }

    }
}
