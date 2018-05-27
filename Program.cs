using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Diagnostics;

namespace Primfaktorzerlegung
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    
    //#regiong Reine Info:
    //27 Stellen :  100000000000000000000000000
    //25 Stellen :  1000000000000000000000000
    //21 Stellen :  100000000000000000000
    //20 Stellen:   10000000000000000000
    //17 Stellen:   10000000000000000
    //16 Stellen:   1000000000000000

    //Max ulong =  18446744073709551615;
    //Max decimal = 79228162514264337593543950335M;

    //Zwei starke fermatische Pseudoprimzahlen
    //2152302898747
    //3474749660383
    //#endregion

    public partial class Form1 : Form
    {
        //Fields
        ulong zwischenRest;
        int i;

        void Zerlegen()
        {
            decimal wert = 0;
            //Wert Auslesen:
            try
            {
                wert = Convert.ToDecimal(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Bitte nur Zahlen Eingeben! :(\nMax 29 Stellen\n(Datentyp Decimal)");
            }
            //Textbox leeren
            textBox2.Invoke(new Action(() => textBox2.Text = ""));

            //Zeit Messung:
            Stopwatch s = new Stopwatch();


            s.Start();
            //Zerlegungs-Engine! :)
            label1:
            //Kein Flaschenhals!! :O
            UInt64 Wurzel_anfang = (UInt64)Math.Pow(Convert.ToDouble(wert), 0.5);
            // Hier wird bei Bedarf in UInt64 Stücke aufgeteilt
            UInt64[] wertArray = Zerteilen(wert);
            for (ulong teiler = 2; teiler <= Wurzel_anfang; teiler++)
            {
                if (Prüfung(wertArray, teiler))
                {
                    textBox2.Invoke(new Action(() => textBox2.Text += String.Format("{0:#,#}\r\n", teiler)));
                    wert = wert / teiler;                     
                    goto label1;
                }  
            }
            textBox2.Invoke(new Action(() => textBox2.Text += String.Format("{0:#,#}\r\n", wert)));

            //Zeit Messung Ausgeben:
            s.Stop();
            TimeSpan t = s.Elapsed;
            textBox2.Invoke(new Action(() => textBox2.Text +=
            String.Format("Time: {0}d {1}h {2}m {3}s {4}ms\r\n", t.Days, t.Hours, t.Minutes, t.Seconds, t.Milliseconds)));
            textBox2.Invoke(new Action(() => textBox2.Text += "Copyright © Nicolas Sauter"));
        }

        //Kann Modulo auf den zusammengesetzten Wert 
        //der Array Werte berechnen und wenn 0 dann return true
        bool Prüfung(UInt64[] wert, UInt64 teiler)
        {
            zwischenRest = 0;

            for (i = 0; i < wert.Length; i++)
            {
                zwischenRest = (zwischenRest + wert[i]) % teiler;
            }
            return (zwischenRest == 0);
        }

        //Macht aus decimal ein ulong Array um mit ulong zu Rechnen 
        //z.B. im Primzahlenprogramm(weil viel Schneller!!!)
        UInt64[] Zerteilen(decimal wert)
        {
            if (wert <= UInt64.MaxValue)
            {
                //Array mit einem Wert
                return new UInt64[] { (ulong)wert };
            }
            else
            {
                decimal i = 2;
                ulong rest;
                decimal teilmenge = 0;

                //Anzahl des Teiler bestimmen bis im ulong bereich nach Teilung von wert
                for (; i < 1000000000; i++)
                {
                    teilmenge = wert / i;
                    if (teilmenge <= UInt64.MaxValue)
                    {
                        break;
                    }
                }
                //Rest
                rest = (ulong)(wert % i);

                ulong[] arr_erg_sum = new ulong[(int)i];

                //Initialisieren Array
                for (int y = 0; y < i; y++)
                {
                    arr_erg_sum[y] = (ulong)teilmenge;
                }
                //Rest am letzten Eintrag dazuzählen
                arr_erg_sum[(arr_erg_sum.Length - 1)] += rest;

                return arr_erg_sum;
            }
        }
    }
}







