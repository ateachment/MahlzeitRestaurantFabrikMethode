﻿/* Implementierung eines Beispiels zur Veranschaulichung des fabric method pattern
 * Quelle: https://de.wikipedia.org/wiki/Fabrikmethode
 * Übersetzung in C# durch W.Eick 3.7.16
 * Erweitert um 3. Hierarchieebene mit neuen parametrisierten Fabrikmethoden 4.7.16
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

    // weitere Klassen
    class Margharita : Pizza {
    }
    class Vegetaria : Pizza {
    }
    //Aufzählungstyp (Integerkonstanten wg. Komfort)
    public enum PizzaTyp { 
        MARGHARITA,
        VEGETARIA
    }


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
            mahlzeit = new Mahlzeit();
        }
        // zusätzliche Methoden mit Parameter anstelle von 
        // jeweils neue konkrete Erzeugerklasse für jeden Pizzatyp
        public void PizzaLiefern(PizzaTyp pizzaTyp)
        {
            BestellungAufnehmen();
            PizzaZubereiten(pizzaTyp); // Aufruf der Factory-Methode
            PizzaServieren();
        }
        protected void PizzaZubereiten(PizzaTyp pizzaTyp)
        {
            switch (pizzaTyp)
            {
                case PizzaTyp.MARGHARITA:
                    mahlzeit = new Margharita();
                    break;
                case PizzaTyp.VEGETARIA:
                    mahlzeit = new Vegetaria();
                    break;
                default:
                    mahlzeit = new Pizza();
                    break;
            }
        }
        protected virtual void PizzaServieren()
        {
            Console.WriteLine("Hier Ihre Pizza. Guten Appetit!\n");
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
            daToni.PizzaLiefern(PizzaTyp.VEGETARIA);

            Rostwurstbude brunosImbiss = new Rostwurstbude();
            brunosImbiss.MahlzeitLiefern();

            Console.ReadKey();
        }
    }
}
