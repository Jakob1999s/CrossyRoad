using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    public class Bahn
    {
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0

        private int idBahn;

        private Rectangle startzone;
        private Rectangle endzone;
        private Rectangle rand;

        private int minGeschwindigkeit;
        private int maxGeschwindigkeit;

        private int hindernisseMax;
        public int HZaehler=0;
        private bool gesperrt;
        private int geschwindigkeit; 
        private bool vonLinksNachRechts;
        Random r = new Random();
        private List<Hinderniss> hindernisse = new List<Hinderniss>();

        public Bahn(int idBahn,int xLocation, int yLocation, int breite, int hoehe, int minGeschwindigkeit, int maxGeschwindigkeit)
        {
            this.idBahn = idBahn;

            rand = new Rectangle(xLocation, yLocation, breite, hoehe);

            this.minGeschwindigkeit = minGeschwindigkeit;
            this.maxGeschwindigkeit= maxGeschwindigkeit;

            geschwindigkeit = r.Next(minGeschwindigkeit, maxGeschwindigkeit);

            RichtungBestimmenRandom();//funktioniert 

            //Start- und Endzone zuteilen 
            if (vonLinksNachRechts)
            {
                startzone = new Rectangle(xLocation - breite, yLocation, breite, hoehe);
                endzone = new Rectangle(breite, yLocation, breite, hoehe);
            }
            else
            {
                endzone = new Rectangle(xLocation - breite, yLocation, breite, hoehe);
                startzone = new Rectangle(breite, yLocation, breite, hoehe);
            }

            hindernisseMax = HindernisseMaxGenerieren(); ;

            gesperrt = false;

        }

        public int IdBahn
        {
            get
            {
                return idBahn;
            }
        }


        //Gibt an wie viele Hindernisse insgesamt auf der Bahn sein können.
        public int HindernisseMax
        {
            get
            {
                return hindernisseMax;
            }
        }

        public Rectangle Startzone
        {
            get
            {
                return startzone;
            }
            set
            {
                startzone = value;
            }
        }

        public Rectangle Endzone
        {
            get
            {
                return endzone;
            }
            set
            {
                endzone = value;
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
        public bool Gesperrt
        {
            get
            {
                return gesperrt;
            }
            set
            {
                gesperrt = value;
            }
        }

        public int Geschwindigkeit
        {
            get
            {
                return geschwindigkeit;
            }
        }

        public bool VonLinksNachRechts
        {
            get
            {
                return vonLinksNachRechts;
            }
        }

        public List<Hinderniss> Hindernisse
        {
            get
            {
                return hindernisse;
            }
        }

        //Fahrtrichtung wird random bestimmt, beziehungsweise verändert.
        public void RichtungBestimmenRandom()
        {
            //Anzahl der zugelassenen Hindernisse verändern.
            hindernisseMax = HindernisseMaxGenerieren();

            //Richtung wird random zugewiesen.
            int lr = r.Next(0, 2);

            //Eine neue Geschwindigkeit wird zugewiesen
            geschwindigkeit = r.Next(minGeschwindigkeit,maxGeschwindigkeit);

            switch (lr)
            {
                case 0:

                    //End und Startzone wechseln, falls es nötig sein sollte.
                    if(!vonLinksNachRechts)
                    {
                        Rectangle hilfe = startzone;
                        startzone = endzone;
                        endzone = hilfe;
                    }

                    vonLinksNachRechts = true;

                    break;
                case 1:

                    //End und Startzone wechseln falls es nötig sein sollte.
                    if (vonLinksNachRechts)
                    {
                        Rectangle hilfe = startzone;
                        startzone = endzone;
                        endzone = hilfe;
                    }

                    vonLinksNachRechts = false;

                    break;
                default:
                    break;
            }

        }

        //Hindernisse werden nur entfernt sofern sie nicht mehr im Spielfeld zu sehen sind.
        public void HindernissEntfernen()
        {
            for (int i = 0; i < hindernisse.Count; i++)
            {
                if (endzone.Contains(hindernisse[i].Testpunkt))
                {
                    hindernisse.RemoveAt(i);
                }
            }
        }

        //Generiert wie viele Hindernisse auf einer Bahn insgesamt fahren können.
        public int HindernisseMaxGenerieren()
        {

            return r.Next(1, 6);
        }

    }
}
