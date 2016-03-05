using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Collections;
using System.Numerics;

namespace Extended_Euclid
{
    public class ExtendedEuclidianAlgo
    {

        static void Main(string[] args)
        {

        }

        public BigInteger ExtendedEuclid(BigInteger m, BigInteger n)
        {
            BigInteger temp_n = n, temp_m = m;                   // temporäre Variablen um n und m für die Ausgabe zu speichern
            BigInteger temp_x = 0, temp_y = 0;                   // temporäre Variablen um x und y für die Tabellenrechnung zu speichern
            BigInteger q = 0, temp = 0, temp_1_m = 0;            // q stellt den multiplikativen Faktor dar, temp ist ein Zwischenspeicher		
            BigInteger a = 1, b = 0;                             // a und b stellen Zeile 1 (1 0) der Tabellenform dar
            BigInteger x = 0, y = 1;                             // x und y stellen Zeile 2 (0 1) der Tabellenform dar

            if (m > n)
            {
                temp = n;                         // Vor der Berechnung tauschen die Werte von n und m untereinander die Plätze
                n = m;
                m = temp;
            }
            while (m != 0)
            {
                //for(q=0; n>=m; q++)				// Subtraktion m von n und Inkrementierung von q pro Schleifendurchlauf 
                //{
                //	n=n-m;						// um den negativen Multiplikator für die Matrizenumformung zu finden 
                //}	
                temp_1_m = m;

                q = n / m;
                m = n % m;

                temp_x = x;                       // x wird gesichert um nachher a mit dem korrekten Wert ersetzen zu können
                x = x * (q * (-1));               // Matrizenumformung - Multiplikation mit negativem q
                x = x + a;                        // Matrizenumformung - Addition von x und a für neues x

                temp_y = y;                       // y wird gesichert um nachher b mit dem korrekten Wert ersetzen zu können
                y = y * (q * (-1));               // Matrizenumformung - Multiplikation mit negativem q
                y = y + b;                        // Matrizenumformung - Addition von y und b für neues y

                a = temp_x;                       // Matrizenumformung - a bekommt den Wert von x vor den Matrizenumformungen
                b = temp_y;                       // Matrizenumformung - b bekommt den Wert von y vor den Matrizenumformungen

                n = temp_1_m;

                //temp=n;							// da n nun kleiner als m ist und somit die Schleife frühzeitig verlassen würde,
                //n=m;							// tauschen die Werte von n und m die Plätze untereinander
                //m=temp;
            }
            Console.WriteLine ("Der GCD lautet: %llu\n", n);                                                                    
            Console.WriteLine ("Der LCM lautet: %llu\n", (x * y * (-1)) / n);
            /* Ausgabe Koordinaten und Formel */                                                 
            Console.WriteLine ("Die ganzzahligen Koordinaten sind %lld * %llu + %lld * %llu = %llu\n", a, temp_n, b, temp_m, n);        
        
            m = m + n;
            return m;
        }
    }
}
