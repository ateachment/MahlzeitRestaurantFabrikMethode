/* Implementierung eines Beispiels zur Veranschaulichung des fabric method pattern
 * Quelle: https://de.wikipedia.org/wiki/Fabrikmethode
 * Übersetzung in C# durch W.Eick 3.7.16
 */

using System;

namespace MahlzeitRestaurantFabrikMethode
{
    // Produkt
    class Mahlzeit {
    };

    // konkretes Produkt
    class Pizza : Mahlzeit {
        public Pizza() {
            Console.WriteLine("Pizza gebacken.");
        }
    };

    // noch ein konkretes Produkt
    class Rostwurst : Mahlzeit {
        public Rostwurst(string beilage) {
            Console.WriteLine("Rostwurst gebraten.");
            if (beilage != "") {
                Console.WriteLine("Serviert mit " + beilage);
            }
        }
    };

    // Erzeuger
    abstract class Restaurant {
        protected Mahlzeit mahlzeit;

        protected virtual void BestellungAufnehmen()
        {
            Console.WriteLine("Ihre Bestellung bitte!");
        }

        // Die abstrakte Factory-Methode, die von Erzeugern implementiert werden muss.
        protected abstract void MahlzeitZubereiten();

        protected virtual void MahlzeitServieren() {
            Console.WriteLine("Hier Ihre Mahlzeit. Guten Appetit!\n");
        }

        // Diese Methode benutzt die Factory-Methode.
        public void MahlzeitLiefern() {
            BestellungAufnehmen();
            MahlzeitZubereiten(); // Aufruf der Factory-Methode
            MahlzeitServieren();
        }
    };

    // konkreter Erzeuger für konkretes Produkt "Pizza"
    class Pizzeria : Restaurant {
        // Implementierung der abstrakten Methode der Basisklasse
        protected override void MahlzeitZubereiten() {
            mahlzeit = new Pizza();
        }
    };

    // konkreter Erzeuger für konkretes Produkt "Rostwurst"
    class Rostwurstbude : Restaurant {
        // Implementierung der abstrakten Methode der Basisklasse
        protected override void MahlzeitZubereiten() {
            mahlzeit = new Rostwurst("Pommes und Ketchup");
        }
    };


    class Client
    {
        static void Main(string[] args)
        {
            Pizzeria daToni = new Pizzeria();
            daToni.MahlzeitLiefern();

            Rostwurstbude brunosImbiss = new Rostwurstbude();
            brunosImbiss.MahlzeitLiefern();

            Console.ReadKey();
        }
    }
}
