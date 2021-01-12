using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.ComponentModel.Design;

namespace AutoOOP
{
    class Auto
    {
        protected double prezzo;
        protected string marca;
        protected string colore;
        protected int cilindrata; //La cilindrata esprime la capacità in centimetri cubici del motore o propulsore. 
        protected string modello;
        protected string tipo_carburante;
        protected int potenza;
        protected string CodiceID;

        public double MPrezzo { get { return prezzo; } }
        public string MMarca { get { return marca; } }
        public string MColore { get { return colore; } }
        public int MCilindrata { get { return cilindrata; } }

        public string MModello { get { return modello; } }
        public string MTipo_Carburante { get { return tipo_carburante; } }
        public int MPotenza { get { return potenza; } }
        public string MCodiceID { 
        get { return CodiceID; } 
        set {MCodiceID= GenCodice(); } 
        }

        public string GenCodice()
        {
            //codice iden= 3 caratt marca, 2 caratt colore, 2 caratt cilindr, 2 caratt modello, 2 caratt carb, 2 caratt potenza +2 random == 15 caratt tot

            string CarMarca = MMarca.Substring(0, 3);
            string CarColore = MColore.Substring(0, 2);
            string CilindrataString = MCilindrata.ToString();   //portando int in string per prenderne i caratteri
            string CarCilindrata = CilindrataString.Substring(0, 2);
            string CarModello = MModello.Substring(0, 2);
            string CarCarburante = MTipo_Carburante.Substring(0, 2);
            string PotenzaString = MPotenza.ToString();         //portando int in string per prenderne i caratteri
            string CarPotenza = PotenzaString.Substring(0, 2);

            Random RandomID = new Random();
            int RandomIDInt = RandomID.Next(10, 99);
            string RandomIDString = RandomIDInt.ToString();

            string CodiceFin = CarMarca + CarColore + CarCilindrata + CarModello + CarCarburante + CarPotenza+ RandomIDString;

            return CodiceFin;

        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            //Interfaccia
            Auto Vettura = new Auto();      //gli attributi del veicolo fanno parte dell'oggetto Vettura e sono inseriti dall'utente

            string VetturaSerial = JsonSerializer.Serialize(Vettura); //inizia la serializzazione degli attributi della vettura inseriti per ultimi
            System.IO.File.WriteAllText(@"C:\Users\Utente\Desktop\Francesco\Triennio\4E\C#\ProgettoAutoOOP\Oggetto.txt", VetturaSerial);    //dopo la @ va il percorso del file da creare
            Console.WriteLine(VetturaSerial);   //visualizzazione serializzazione

           


            Console.ReadKey();

        }
    }
}
