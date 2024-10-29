using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    internal class Benutzeroberflaeche
    {
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0
        //------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0------------2.0

        #region Eigenschaften
        public Frosch frosch;

        public bool Pause = false;

        //Blinken (grün)
        private int blinkZaehler = 0;
        private bool boolBlinken = false;
        private bool gruenBlinken = false;

        private int schwierigkeitsgrad;
        private Bahn[] bahnen;        
        private int minGeschwindigkeit;
        private int maxgeschwindigkeit;

        private SolidBrush brBahnHell = new SolidBrush(Color.LightGray);
        private SolidBrush brBahnDunkel = new SolidBrush(Color.Gray);
        private SolidBrush brStartBahn = new SolidBrush(Color.PaleTurquoise);
        private SolidBrush brZielBahn = new SolidBrush(Color.PaleTurquoise);

        public Random R = new Random();
        #endregion


        public Benutzeroberflaeche(int schwierigkeitsgrad, int formBreite, int formHoehe)
        {

            Schwierigkeitsgrad = schwierigkeitsgrad;

            switch (schwierigkeitsgrad)
            {
                case 1:
                    bahnen = new Bahn[8];
                    minGeschwindigkeit = 10;
                    maxgeschwindigkeit = 11;
                    break;
                case 2:
                    bahnen = new Bahn[12];
                    minGeschwindigkeit = 10;
                    maxgeschwindigkeit = 15;
                    break;
                case 3:
                    bahnen = new Bahn[16];
                    minGeschwindigkeit = 5;//temporäre Änderung
                    maxgeschwindigkeit = 20;//temporäre Änderung
                    break;
                case 4:
                    bahnen = new Bahn[20];
                    minGeschwindigkeit = 15;
                    maxgeschwindigkeit = 30;
                    break;
                case 5:
                    bahnen = new Bahn[24];
                    minGeschwindigkeit = 1;
                    maxgeschwindigkeit = 30;
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
                case 18:
                    break;
                case 19:
                    break;
                case 20:
                    break;
                case 21:
                    break;
                case 22:
                    break;

                default:
                    break;
            }

            //Den unteren Bereich für die Scoreanzeige etc. schaffen.
            formHoehe = Kuerzen(formHoehe);

            //Bahnen erstellen
            for (int i = 0; i < bahnen.Length; i++)
            {
                bahnen[i] = new Bahn(i,0, (formHoehe / bahnen.Length) * i, formBreite, (formHoehe / bahnen.Length), minGeschwindigkeit, maxgeschwindigkeit);
            }

            //Frosch auf die erste Bahn (im Array die letzte Bahn) platzieren.
            frosch = new Frosch(bahnen[bahnen.Length-1]);
        }


        //1. 
        public int Schwierigkeitsgrad
        {
            get
            {
                return schwierigkeitsgrad;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Der Wert darf nicht unter 1 sein!");
                }

                this.schwierigkeitsgrad = value;
            }
        }

        //2.

        public Bahn[] Bahnen
        {
            get
            {
                return bahnen;
            }
        }

        public SolidBrush BrStartBahn
        {
            get
            {
                return brStartBahn;
            }
        }

        public SolidBrush BrZielBahn
        {
            get
            {
                return brZielBahn;
            }
        }

        public SolidBrush BrBahnHell
        {
            get
            {
                return brBahnHell;
            }
        }

        public SolidBrush BrBahnDunkel
        {
            get
            {
                return brBahnDunkel;
            }
        }

        public bool BoolBlinken
        {
            get
            {
                return boolBlinken;
            }
        }

        public bool GruenBlinken
        {
            get
            {
                return gruenBlinken;
            }
        }

        public void AlleGroessenAnpassen(int formBreite, int formHoehe)
        {

            //Kürzen für den unteren Bereich.
            formHoehe = Kuerzen(formHoehe);

            //Bahngröße anpassen
            int bBreite = formBreite;
            int bHoehe = formHoehe / bahnen.Length;

            for (int i = 0; i < bahnen.Length; i++)
            {
                bahnen[i].Rand = new Rectangle(0, bHoehe * i, bBreite, bHoehe);
            }


            //End- und Startzonen müssen auch verschoben werden!!!!
            for (int i = 0; i < bahnen.Length; i++)
            {
                if (bahnen[i].VonLinksNachRechts)
                {
                    bahnen[i].Startzone = new Rectangle(0 - bBreite, bHoehe * i, bBreite, bHoehe);
                    bahnen[i].Endzone = new Rectangle(bBreite , bHoehe * i, bBreite, bHoehe);
                }
                else
                {
                    bahnen[i].Endzone = new Rectangle(0 - bBreite, bHoehe * i, bBreite, bHoehe);
                    bahnen[i].Startzone = new Rectangle(bBreite , bHoehe * i, bBreite, bHoehe);
                }
            }

            for (int i = 0; i < bahnen.Length; i++)
            {
                for (int a = 0; a < bahnen[i].Hindernisse.Count; a++)
                {
                    int hBreite;
                    //Hindernissgröße anpassen (Auto)
                    if (bahnen[i].Hindernisse[a].Zug)
                    {
                        hBreite = bahnen[i].Hindernisse[a].HindernissBreiteAufBahn(bahnen[a].Rand.Width, true);
                    }
                    else
                    {
                        hBreite = bahnen[i].Hindernisse[a].HindernissBreiteAufBahn(bahnen[a].Rand.Width, false);
                    }

                    int hHoehe = bahnen[i].Rand.Height;

                    //Die xLocation der Hindernisse muss angepasst werden.
                    int xLocation = Skallieren(formBreite, bahnen[i].Hindernisse[a].Skallierer);

                    bahnen[i].Hindernisse[a].Rand = new Rectangle(xLocation, hHoehe * i, hBreite, hHoehe);
                }
            }

            //Die xLocation des Frosches muss angepasst werden.
            int xFLocation = Skallieren(formBreite,frosch.Skallierer);

            int yFLoxation = bahnen[frosch.IdAktuelleBahn].Rand.Y;

            //Froschgröße anpassen --> Frosch ist so hoch und breit wie die Höhe einer Bahn.
            int fBreite = bahnen[0].Rand.Height;
            int fHoehe = bahnen[0].Rand.Height;

            frosch.BahnBreite = formBreite;

            frosch.Rand = new Rectangle(xFLocation,yFLoxation,fBreite,fHoehe);
        }

        public void SkalliererBerechnen(int formBreite)
        {
            //Froschskallierer:
            double skallierer = Convert.ToDouble(frosch.Rand.X) / Convert.ToDouble(formBreite);
            frosch.Skallierer = skallierer;

            //Hindernissskallierer:
            foreach(Bahn b in bahnen)
            {
                foreach(Hinderniss h in b.Hindernisse)
                {
                    skallierer = Convert.ToDouble(h.Rand.X) / Convert.ToDouble(formBreite);
                    h.Skallierer = skallierer;
                }
            }
        }

        private int Skallieren(int formBreite,double skallierer)
        {
            int xFLocation = Convert.ToInt32(formBreite * skallierer);
            return xFLocation;
        }

        //Den unteren Bereich für die Scoreanzeige etc. schaffen.
        public int Kuerzen(int formHoehe)
        {
            return formHoehe -= (formHoehe / bahnen.Length) * 2;
        }

        public bool BahnBesetzbar(int idBahn)
        {
            //Kann ein Hinderniss auf die Bahn gesetzt werden ohne dass sich Hindernisse sichtbar überschneiden?
            bool ready = true;

            //Ist die Bahn gesperrt? Wenn ja, dann die Methode direkt verlassen.
            if (bahnen[idBahn].Gesperrt)
            {
                ready = false; 
                return ready;
            }

            //Befindet sich ein Hinderniss in der Startzone?
            foreach(Hinderniss h in bahnen[idBahn].Hindernisse)
            {
                if (bahnen[idBahn].Startzone.Contains(h.Testpunkt))
                {
                    ready = false;  
                }
            }

            return ready;
        }

        public void BahnBesetzen(int idBahn)
        {
            //Soll es ein Auto oder ein Zug werden?
            //Wahrscheinlichkeit:
            int autoZug = R.Next(0,8);

            Hinderniss h;

            //Hinderniss erstellen
            if (autoZug == 0 )
            {
                h = new Hinderniss(bahnen[idBahn], true);
            }
            else
            {
                h = new Hinderniss(bahnen[idBahn], false);
            }


            //Hinderniss wird der Liste hinzufügen 
            bahnen[idBahn].Hindernisse.Add(h);

            //Hindernissanzahl um 1 erhöhen.
            bahnen[idBahn].HZaehler++;


            //Ist die maximale Anzahl von Hindernissen auf der Bahn erfolgt?
            if (bahnen[idBahn].HindernisseMax == bahnen[idBahn].HZaehler)
            {
                bahnen[idBahn].Gesperrt = true;
            }
        }

        public void BahnLeer()
        {
            foreach(Bahn b in bahnen)
            {
                //Wenn die Bahn gesperrt sein sollte und dazu sich keine Hindernisse mehr
                //auf ihr befinden sollten.
                if(b.Gesperrt && b.Hindernisse.Count ==0)
                {
                    //Bahn wird eine random Richtung zugewiesen. Dazu verändern sich die 
                    //Start und Endzonen. Für mehr Infos geh auf die Methode.
                    b.RichtungBestimmenRandom();

                    //Zähler wird zurückgesetzt.
                    b.HZaehler = 0;

                    //Bahn wird entsperrt.
                    b.Gesperrt = false;
                }
            }
        }

        public void Blinken()
        {
            boolBlinken = true;

            if( blinkZaehler == 2 || blinkZaehler == 3 || blinkZaehler == 4 
                || blinkZaehler == 8 || blinkZaehler == 9 || blinkZaehler == 10
                || blinkZaehler == 14 || blinkZaehler == 15 || blinkZaehler == 16)
            {
                gruenBlinken = true;
            }
            else
            {
                gruenBlinken= false;
            }

            blinkZaehler++;

            if (blinkZaehler==16)
            {
                //Blinken hört auf 
                boolBlinken = false;
                blinkZaehler = 0;

                //Frosch an den Start setzen
                SpielerAnStart();

                //Frosch kann sich wieder bewegen
                frosch.BewegungSperren = false;

                //Frosch befindet sich wieder auf der Startbahn.
                frosch.IdAktuelleBahn = bahnen.Length - 1;
            }

        }

        public void SpielerAnStart(/*int formBreite*/)
        {
            int breite = bahnen[0].Rand.Height;
            int hoehe = bahnen[0].Rand.Height;

            int xLocation = bahnen[0].Rand.Width / 2 - frosch.Rand.Width / 2;
            int yLocation = bahnen[bahnen.Length-1].Rand.Y;

            frosch.Rand = new Rectangle(xLocation,yLocation,breite,hoehe);
        }
    }
}
