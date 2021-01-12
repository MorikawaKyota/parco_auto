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

namespace Gestione_Parco_Auto
{
    class Program
    {
        static void Main(string[] args)
        {
            Auto Vettura = new Auto();      //gli attributi del veicolo fanno parte dell'oggetto Vettura e sono inseriti dall'utente

            string VetturaSerial = JsonSerializer.Serialize(Vettura); //inizia la serializzazione degli attributi della vettura inseriti per ultimi
            System.IO.File.WriteAllText(@"C:\Users\Utente\Desktop\Francesco\Triennio\4E\C#\ProgettoAutoOOP\Oggetto.txt", VetturaSerial);    //dopo la @ va il percorso del file da creare
            Console.WriteLine(VetturaSerial);   //visualizzazione serializzazione

        }
        public class ParcoAuto
        {
            protected int autoVendute = 0, autoPresenti = 0;
            public int AutoVendute
            {
                get { return autoVendute; }
            }
            public int AutoPresenti
            {
                get { return autoPresenti; }
            }
            //list delle auto presenti nel parco auto
            protected List<Auto> leAuto = new List<Auto>();

            public void InserireAuto()
            {
                Auto template = new Auto();//si genera il template che viene inserito nel list

                Console.WriteLine("Inserimento dati del veicolo.\n");
                GenerazioneTemplate(ref template);

                autoPresenti++;
                leAuto.Add(template);
                foreach (Auto item in leAuto)
                {
                    Console.WriteLine)
                }
            }
            public void RicercareAuto()
            {
                Console.WriteLine("Indicare come cercare l'auto che desideri:\n" +
                        "1-Cercare col codice ID\n" +
                        "2-Inserire i dati dell'auto\n");
                string scelta = Console.ReadLine();
                switch (scelta)
                {
                    case "1":
                        {
                            Console.WriteLine("Indicare il codice ID dell`auto.");
                            string codiceIDConfronto = Console.ReadLine();

                            if (leAuto.Exists(auto => auto.CodiceID == codiceIDConfronto) == true)//nel caso in cui esista un tale oggetto nel list "leAuto"
                            {
                                Auto risultato = leAuto.Find(auto => auto.CodiceID == codiceIDConfronto);//si copiano i dati dell'oggetto in uno nuovo e li si visualizzano;
                                Console.WriteLine("Codice ID dell'auto: {0}\n" +
                                    "Nuova: {1}\n" +
                                    "Marca: {2}\n" +
                                    "Modello: {3}\n" +
                                    "Colore: {4}\n" +
                                    "Motore: {5}\n" +
                                    "Tipo di carburante utilizzato: {6}\n" +
                                    "Potenza: {7} cavalli\n" +
                                    "Cilindrata: {6} cc\n" +
                                    "Km percorsi: {9}\n" +
                                    "Prezzo: {10}\n",
                                    risultato.CodiceID, risultato.Nuova, risultato.Marca, risultato.Modello, risultato.Colore, risultato.Motore, risultato.TipoCarburante, risultato.Potenza, risultato.Cilindrata, risultato.KmPercorsi, risultato.Prezzo);
                            }
                            else//altrimenti si visualizza un  messaggio
                            {
                                Console.WriteLine("Nessun auto travata.");
                            }
                            break;
                        }
                    case "2":
                        {
                            Auto template = new Auto();//si genera il template come oggetto di confronto durante la ricerca
                            GenerazioneTemplate(ref template);
                            int risultati = 0;//numero degli oggetti che soddisfani i requisiti dell'utente

                            foreach (Auto auto in leAuto)
                            {
                                bool idoneo = true;
                                //confronto dei dati dei campi obbligatori
                                if (auto.Prezzo != template.Prezzo || auto.Marca != template.Marca || auto.Modello != template.Modello || auto.Motore != template.Motore)
                                {
                                    idoneo = false;
                                    break;
                                }
                                //i successivi 5 if hanno la funzione di evitare inutili controlli nel caso in cui tale campo non sia stato definito dall'utente
                                if (template.Colore != "N/A")
                                {
                                    if (auto.Colore != template.Colore)
                                    {
                                        idoneo = false;
                                        break;
                                    }
                                }
                                if (template.TipoCarburante != "N/A")
                                {
                                    if (auto.TipoCarburante != template.TipoCarburante)
                                    {
                                        idoneo = false;
                                        break;
                                    }
                                }
                                if (template.Potenza != 0)
                                {
                                    if (auto.Potenza != template.Potenza)
                                    {
                                        idoneo = false;
                                        break;
                                    }
                                }
                                if (template.Cilindrata != 0)
                                {
                                    if (auto.Cilindrata != template.Cilindrata)
                                    {
                                        idoneo = false;
                                        break;
                                    }
                                }
                                if (template.Nuova != true && template.KmPercorsi != 0)
                                {
                                    if (auto.KmPercorsi != template.KmPercorsi)
                                    {
                                        idoneo = false;
                                        break;
                                    }
                                }

                                if (idoneo == true)//se l'oggetto è idonei si visualizzano i suoi dati, altrimenti no
                                {
                                    Console.WriteLine("Codice ID dell'auto: {0}\n" +
                                        "Nuova: {1}\n" +
                                        "Marca: {2}\n" +
                                        "Modello: {3}\n" +
                                        "Colore: {4}\n" +
                                        "Motore: {5}\n" +
                                        "Tipo di carburante utilizzato: {6}\n" +
                                        "Potenza: {7} cavalli\n" +
                                        "Cilindrata: {6} cc\n" +
                                        "Km percorsi: {9}\n" +
                                        "Prezzo: {10}\n" +
                                        "--------------------------------------------------\n",
                                        auto.CodiceID, auto.Nuova, auto.Marca, auto.Modello, auto.Colore, auto.Motore, auto.TipoCarburante, auto.Potenza, auto.Cilindrata, auto.KmPercorsi, auto.Prezzo);
                                    risultati++;
                                }
                            }
                            if (risultati == 0)//se non ci sono oggetti idonei si visualizza un messaggio
                            {
                                Console.WriteLine("Nessun auto travata.");
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Errore: opzione invalida, scegliere un' opzione valida.\n");
                            break;
                        }
                }

            }
            public void RimuovereAuto()
            {
                Console.WriteLine("Indicare come cercare l'auto che desideri rimuovere:\n" +
                    "1-Cercare col codice ID\n" +
                    "2-Inserire i dati dell'auto\n");
                string scelta = Console.ReadLine();
                switch (scelta)
                {
                    case "1":
                        {
                            Console.WriteLine("Indicare il codice ID dell`auto.");
                            string codiceIDConfronto = Console.ReadLine();

                            if (leAuto.Exists(auto => auto.CodiceID == codiceIDConfronto) == true)//nel caso in cui esista un tale oggetto nel list "leAuto"
                            {
                                leAuto.Remove(new Auto { CodiceID = codiceIDConfronto });//si rimuove l'oggetto dal list "leAuto"
                                Console.WriteLine("E' stata rimossa l'auto con ID = {0}.", codiceIDConfronto);//e si visualizza un messaggio;
                                autoPresenti--;
                                autoVendute++;
                            }
                            else//altrimenti si visualizza un altro messaggio
                            {
                                Console.WriteLine("Nessun auto travata.");
                            }
                            break;
                        }
                    case "2":
                        {
                            Auto template = new Auto();//si genera il template come oggetto di confronto durante la ricerca
                            GenerazioneTemplate(ref template);
                            List<Auto> autoIdonei = new List<Auto>();//list degli oggetti che soddisfano i requisiti dell'utente

                            foreach (Auto auto in leAuto)
                            {
                                bool idoneo = true;
                                if (auto.Prezzo != template.Prezzo || auto.Marca != template.Marca || auto.Modello != template.Modello || auto.Motore != template.Motore)
                                {
                                    idoneo = false;
                                    break;
                                }
                                if (template.Colore != "N/A")
                                {
                                    if (auto.Colore != template.Colore)
                                    {
                                        idoneo = false;
                                        break;
                                    }
                                }
                                if (template.TipoCarburante != "N/A")
                                {
                                    if (auto.TipoCarburante != template.TipoCarburante)
                                    {
                                        idoneo = false;
                                        break;
                                    }
                                }
                                if (template.Potenza != 0)
                                {
                                    if (auto.Potenza != template.Potenza)
                                    {
                                        idoneo = false;
                                        break;
                                    }
                                }
                                if (template.Cilindrata != 0)
                                {
                                    if (auto.Cilindrata != template.Cilindrata)
                                    {
                                        idoneo = false;
                                        break;
                                    }
                                }
                                if (template.Nuova != true && template.KmPercorsi != 0)
                                {
                                    if (auto.KmPercorsi != template.KmPercorsi)
                                    {
                                        idoneo = false;
                                        break;
                                    }
                                }
                                if (idoneo == true)//se l'oggetto è idonei lo si aggiunge nel list "autoIdonei"
                                {
                                    autoIdonei.Add(auto);
                                }
                            }

                            if (autoIdonei.Count != 0)//se il numero degli elementi presenti in "autoIdonei" è diverso da 0, si visualizzano i loro dati
                            {
                                Console.WriteLine("I risultati che soddisfano tali requisiti sono:\n");
                                foreach (Auto auto in autoIdonei)
                                {
                                    Console.WriteLine("Codice ID dell'auto: {0}\n" +
                                        "Nuova: {1}\n" +
                                        "Marca: {2}\n" +
                                        "Modello: {3}\n" +
                                        "Colore: {4}\n" +
                                        "Motore: {5}\n" +
                                        "Tipo di carburante utilizzato: {6}\n" +
                                        "Potenza: {7} cavalli\n" +
                                        "Cilindrata: {6} cc\n" +
                                        "Km percorsi: {9}\n" +
                                        "Prezzo: {10}\n" +
                                        "--------------------------------------------------\n",
                                        auto.CodiceID, auto.Nuova, auto.Marca, auto.Modello, auto.Colore, auto.Motore, auto.TipoCarburante, auto.Potenza, auto.Cilindrata, auto.KmPercorsi, auto.Prezzo);
                                }
                                Console.WriteLine("Indicare in numero intero l'ordine dell'auto che desideri rimuovere.");
                                int IndiceAutoRimossa = Int32.Parse(Console.ReadLine()) - 1;
                                if (IndiceAutoRimossa >= 0 && IndiceAutoRimossa < autoIdonei.Count)//si controlla che l'indice sia entro i limiti
                                {
                                    leAuto.RemoveAt(IndiceAutoRimossa);
                                    Console.WriteLine("E' stata rimossa l'auto");
                                    autoPresenti--;
                                    autoVendute++;
                                }
                                else//se lindice fuoriesce dai limiti si visualizza un messaggio
                                {
                                    Console.WriteLine("Errore, auto inesistente.");
                                }
                            }

                            else//se il numero è 0 si visualizza un messaggio
                            {
                                Console.WriteLine("Nessun auto travata.");
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Errore: opzione invalida, scegliere un' opzione valida.\n");
                            break;
                        }
                }
            }
            private void GenerazioneTemplate(ref Auto template)//il template è una variabile tipo Auto, che può diventare l'elemento inserito nella list, oppure essere l'oggetto di confronto durante la ricerca
            {
                bool end = false;//bool per ripetere l'inserimento dei dati, o per dare la possibilità di scegliere l'opzione coretta: l'altro bool endSwitch ha funzioni simili
                string scelta;//string per memorizzare l'opzione scelta

                do//si chiede lo stato dell'auto
                {
                    Console.WriteLine("L'auto registrata e' :\n" +
                        "1-Nuova\n" +
                        "2-Usata\n");
                    scelta = Console.ReadLine();
                    switch (scelta)
                    {
                        case "1":
                            {
                                template.Nuova = true;
                                end = true;
                                break;
                            }
                        case "2":
                            {
                                template.Nuova = false;
                                end = true;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Errore: opzione imvalida, reinserire correttamente.");
                                break;
                            }
                    }
                } while (end == false);

                end = false;//si ripristina il valore
                do//ciclo per inserire i dati dell'auto
                {
                    Console.WriteLine("Digitare il numero per iniziare ad inserire i dati. Attenzione: i campi contrassegnati con * sono obbligatori.\n" +
                        "1-Prezzo*\n" +
                        "2-Marca*\n" +
                        "3-Colore\n" +
                        "4-Modello*\n" +
                        "5-Motore*\n" +
                        "6-Tipo di carburante utilizzato\n" +
                        "7-Potenza\n" +
                        "8-Cilindrata\n" +
                        "9-Km percorsi (*) (solo se auto usata)\n" +
                        "10-Reinserire un dato\n" +
                        "11-Fine inserimento\n");
                    scelta = Console.ReadLine();
                puntoReinserireDato://questa etichetta è il punto in cui si salta dal case 10
                    switch (scelta)
                    {
                        case "1":
                            {
                                Console.WriteLine("Inserire il prezzo del veicolo.\n");
                                template.Prezzo = Int32.Parse(Console.ReadLine());
                                break;
                            }
                        case "2":
                            {
                                bool endSwitch = false;
                                do
                                {
                                    Console.WriteLine("Scegliere la marca dell'auto.\n" +
                                        "1-BMW\n" +
                                        "2-Audi\n" +
                                        "3-Citroen\n" +
                                        "4-Ferrari\n" +
                                        "5-Fiat\n" +
                                        "6-Ford\n" +
                                        "7-Honda\n" +
                                        "8-Hyundai\n" +
                                        "9-Mercedes\n" +
                                        "10-Mini\n" +
                                        "11-Mitsubishi\n" +
                                        "12-Kia\n" +
                                        "13-Nissan\n" +
                                        "14-Peugeot\n" +
                                        "15-Suzuki\n" +
                                        "16-Tesla\n" +
                                        "17-Toyota\n" +
                                        "18-Volvo\n" +
                                        "19-Lamborghini\n" +
                                        "20-Altro");
                                    scelta = Console.ReadLine();
                                    switch (scelta)
                                    {
                                        case "1":
                                            {
                                                template.Marca = "BMW";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "2":
                                            {
                                                template.Marca = "Audi";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "3":
                                            {
                                                template.Marca = "Citroen";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "4":
                                            {
                                                template.Marca = "Ferrari";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "5":
                                            {
                                                template.Marca = "Fiat";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "6":
                                            {
                                                template.Marca = "Ford";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "7":
                                            {
                                                template.Marca = "Honda";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "8":
                                            {
                                                template.Marca = "Hyundai";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "9":
                                            {
                                                template.Marca = "Mercedes";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "10":
                                            {
                                                template.Marca = "Mini";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "11":
                                            {
                                                template.Marca = "Mitsubishi";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "12":
                                            {
                                                template.Marca = "Kia";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "13":
                                            {
                                                template.Marca = "Nissan";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "14":
                                            {
                                                template.Marca = "Peugeot";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "15":
                                            {
                                                template.Marca = "Suzuki";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "16":
                                            {
                                                template.Marca = "Tesla";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "17":
                                            {
                                                template.Marca = "Toyota";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "18":
                                            {
                                                template.Marca = "Volvo";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "19":
                                            {
                                                template.Marca = "Lamborghini";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "20":
                                            {
                                                template.Marca = "Altro";
                                                endSwitch = true;
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Errore: opzione invalida, reinserire correttamente.\n");
                                                break;
                                            }
                                    }
                                } while (endSwitch == false);
                                break;
                            }
                        case "3":
                            {
                                bool endSwitch = false;
                                do
                                {
                                    Console.WriteLine("Scegliere il colore dell'auto.\n" +
                                        "1-Bianco\n" +
                                        "2-Nero\n" +
                                        "3-Grigio chiaro\n" +
                                        "4-Grigio\n" +
                                        "5-Grigio scuro\n" +
                                        "6-Giallo melone\n" +
                                        "7-Giallo\n" +
                                        "8-Giallo chiaro\n" +
                                        "9-Arancione\n" +
                                        "10-Rosso\n" +
                                        "11-Rosso scuro\n" +
                                        "12-Verde chiaro\n" +
                                        "13-Verde scuro\n" +
                                        "14-Verde bandiera\n" +
                                        "15-Turchese blu\n" +
                                        "16-Menta\n" +
                                        "17-Azzurro cielo\n" +
                                        "18-Azzurro\n" +
                                        "19-Blu oceano\n" +
                                        "20-Blu cobalto\n" +
                                        "21-Blu notte\n" +
                                        "22-Viola\n" +
                                        "23-Magenta\n" +
                                        "24-Rosa\n" +
                                        "25-Marrone\n" +
                                        "26-Altro\n");
                                    scelta = Console.ReadLine();
                                    switch (scelta)
                                    {
                                        case "1":
                                            {
                                                template.Colore = "Bianco";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "2":
                                            {
                                                template.Colore = "Nero";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "3":
                                            {
                                                template.Colore = "Grigio chiaro";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "4":
                                            {
                                                template.Colore = "Grigio";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "5":
                                            {
                                                template.Colore = "Grigio scuro";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "6":
                                            {
                                                template.Colore = "Giallo melone";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "7":
                                            {
                                                template.Colore = "Giallo";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "8":
                                            {
                                                template.Colore = "Giallo chiaro";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "9":
                                            {
                                                template.Colore = "Arancione";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "10":
                                            {
                                                template.Colore = "Rosso";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "11":
                                            {
                                                template.Colore = "Rosso scuro";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "12":
                                            {
                                                template.Colore = "Verde chiaro";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "13":
                                            {
                                                template.Colore = "Verde scuro";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "14":
                                            {
                                                template.Colore = "Verde bandiera";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "15":
                                            {
                                                template.Colore = "Turchese blu";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "16":
                                            {
                                                template.Colore = "Menta";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "17":
                                            {
                                                template.Colore = "Azzurro cielo";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "18":
                                            {
                                                template.Colore = "Azzurro";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "19":
                                            {
                                                template.Colore = "Blu oceano";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "20":
                                            {
                                                template.Colore = "Blu cobalto";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "21":
                                            {
                                                template.Colore = "Blu notte";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "22":
                                            {
                                                template.Colore = "Viola";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "23":
                                            {
                                                template.Colore = "Magenta";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "24":
                                            {
                                                template.Colore = "Rosa";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "25":
                                            {
                                                template.Colore = "Marrone";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "26":
                                            {
                                                template.Colore = "Altro";
                                                endSwitch = true;
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Errore: opzione invalida, reinserire correttamente.\n");
                                                break;
                                            }
                                    }
                                } while (endSwitch == false);
                                break;
                            }
                        case "4":
                            {
                                Console.WriteLine("Indicare il modello del veicolo:\n");
                                template.Modello = Console.ReadLine();
                                break;
                            }
                        case "5":
                            {
                                bool endSwitch = false;
                                do
                                {
                                    Console.WriteLine("Scegliere il tipo del motore dell'auto.\n" +
                                        "1-Motore a combustione interna\n" +
                                        "2-Motore elettrico\n" +
                                        "3-Motore diesel\n" +
                                        "4-Motore ibrido\n");
                                    scelta = Console.ReadLine();
                                    switch (scelta)
                                    {
                                        case "1":
                                            {
                                                template.Motore = "Motore a combustione interna";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "2":
                                            {
                                                template.Motore = "Motore elettrico";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "3":
                                            {
                                                template.Motore = "Motore diesel";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "4":
                                            {
                                                template.Motore = "Motore ibrido";
                                                endSwitch = true;
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Errore: opzione invalida, reinserire correttamente.\n");
                                                break;
                                            }
                                    }
                                } while (endSwitch == false);
                                break;
                            }
                        case "6":
                            {
                                bool endSwitch = false;
                                do
                                {
                                    Console.WriteLine("Scegliere il tipo di carburante utilizzato dell'auto.\n" +
                                        "1-Benzina\n" +
                                        "2-Diesel\n" +
                                        "3-Metano\n" +
                                        "4-GPL (gas di petrolio liquefatto)\n" +
                                        "5-Idrogeno\n" +
                                        "6-GNL (gas naturale liquefatto)\n" +
                                        "7-Elettricità\n");
                                    scelta = Console.ReadLine();
                                    switch (scelta)
                                    {
                                        case "1":
                                            {
                                                template.TipoCarburante = "Benzina";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "2":
                                            {
                                                template.TipoCarburante = "Diesel";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "3":
                                            {
                                                template.TipoCarburante = "Metano";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "4":
                                            {
                                                template.TipoCarburante = "GPL(gas di petrolio liquefatto)";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "5":
                                            {
                                                template.TipoCarburante = "Idrogeno";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "6":
                                            {
                                                template.TipoCarburante = "GNL(gas naturale liquefatto)";
                                                endSwitch = true;
                                                break;
                                            }
                                        case "7":
                                            {
                                                template.TipoCarburante = "Elettricità";
                                                endSwitch = true;
                                                break;
                                            }
                                        default:
                                            {
                                                Console.WriteLine("Errore: opzione invalida, reinserire correttamente.\n");
                                                break;
                                            }
                                    }
                                } while (endSwitch == false);
                                break;
                            }
                        case "7":
                            {
                                Console.WriteLine("Indicare la potenza dell'auto.\n");
                                template.Potenza = Int32.Parse(Console.ReadLine());
                                break;
                            }
                        case "8":
                            {
                                Console.WriteLine("Indicare la cilindrata dell'auto in cc.\n");
                                template.Cilindrata = Int32.Parse(Console.ReadLine());
                                break;
                            }
                        case "9":
                            {
                                //solo alle auto usate si chiedono i km percorsi
                                if (template.Nuova == true)
                                {
                                    Console.WriteLine("Errore: il veicolo che stai registrando è nuovo.");
                                }
                                else
                                {
                                    Console.WriteLine("Indicare i Km percorsi dell'auto.\n");
                                    template.KmPercorsi = Int32.Parse(Console.ReadLine());
                                }
                                break;
                            }
                        case "10":
                            {

                                Console.WriteLine("Scegliere il dato che vuoi reinserire\n" +
                                    "1-Prezzo*\n" +
                                    "2-Marca*\n" +
                                    "3-Colore\n" +
                                    "4-Modello*\n" +
                                    "5-Motore*\n" +
                                    "6-Tipo di carburante utilizzato\n" +
                                    "7-Potenza\n" +
                                    "8-Cilindrata\n" +
                                    "9-Km percorsi (*) (solo se auto usata)\n");
                                scelta = Console.ReadLine();
                                if (scelta == "10")//controllo per evitare un loop inutile
                                {
                                    Console.WriteLine("Errore: opzione invalida!");
                                    break;
                                }
                                else
                                {
                                    goto puntoReinserireDato;//si salta all'etichetta "puntoReinserireDato"
                                }
                            }
                        case "11":
                            {
                                if (template.Nuova == true)//si controlla se tutti gli attributi obbligatori sono stati definiti o meno
                                {
                                    if (template.Prezzo != 0 && template.Marca != null && template.Motore != null && template.Modello != null)
                                    {
                                        //si controllano se gli altri attributi di tipo string sono stati definiti; in caso di assenza vengono definiti con N/A (not available), cioè "dato non disponibile"
                                        if (template.Colore == null)
                                        {
                                            template.Colore = "N/A";
                                        }
                                        if (template.TipoCarburante == null)
                                        {
                                            template.TipoCarburante = "N/A";
                                        }
                                        end = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Errore: ci ancora dati da inserire.\n");
                                    }
                                }
                                else
                                {
                                    if (template.Prezzo != 0 && template.Marca != null && template.Motore != null && template.Modello != null && template.KmPercorsi != 0)
                                    {
                                        if (template.Colore == null)
                                        {
                                            template.Colore = "N/A";
                                        }
                                        if (template.TipoCarburante == null)
                                        {
                                            template.TipoCarburante = "N/A";
                                        }
                                        end = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Errore: ci ancora dati da inserire.\n");
                                    }
                                }
                                break;
                            }
                        default://controllo per avvertire l'utente che ha sbagliato
                            {
                                Console.WriteLine("Errore: opzione invalida, scegliere un' opzione valida.\n");
                                break;
                            }
                    }
                }
                while (end == false);
            }
        }
        public class Auto
        {
            protected int prezzo, potenza, cilindrata, kmPercorsi;
            protected bool nuova;
            protected string marca, colore, modello, tipoCarburante, motore, codiceID;
            public int Prezzo
            {
                set { prezzo = value; }
                get { return prezzo; }
            }
            public int Potenza
            {
                set { potenza = value; }
                get { return potenza; }
            }
            public int Cilindrata
            {
                set { cilindrata = value; }
                get { return cilindrata; }
            }
            public int KmPercorsi
            {
                set { kmPercorsi = value; }
                get { return kmPercorsi; }
            }
            public bool Nuova
            {
                set { nuova = value; }
                get { return nuova; }
            }
            public string Marca
            {
                set { marca = value; }
                get { return marca; }
            }
            public string Colore
            {
                set { colore = value; }
                get { return colore; }
            }
            public string Modello
            {
                set { modello = value; }
                get { return modello; }
            }
            public string TipoCarburante
            {
                set { tipoCarburante = value; }
                get { return tipoCarburante; }
            }
            public string Motore
            {
                set { motore = value; }
                get { return motore; }
            }
            public string CodiceID
            {
                set { codiceID = value; }
                get { return codiceID; }
            }
            private string GenCodice()
            {
                //codice iden= 3 caratt marca, 2 caratt colore, 2 caratt cilindr, 2 caratt modello, 2 caratt carb, 2 caratt potenza +2 random == 15 caratt tot

                string CarMarca = Marca.Substring(0, 3);
                string CarColore = Colore.Substring(0, 2);
                string CilindrataString = Cilindrata.ToString();   //portando int in string per prenderne i caratteri
                string CarCilindrata = CilindrataString.Substring(0, 2);
                string CarModello = Modello.Substring(0, 2);
                string CarCarburante = TipoCarburante.Substring(0, 2);
                string PotenzaString = Potenza.ToString();         //portando int in string per prenderne i caratteri
                string CarPotenza = PotenzaString.Substring(0, 2);

                Random RandomID = new Random();
                int RandomIDInt = RandomID.Next(10, 99);
                string RandomIDString = RandomIDInt.ToString();

                string CodiceFin = CarMarca + CarColore + CarCilindrata + CarModello + CarCarburante + CarPotenza + RandomIDString;

                return CodiceFin;

            }

        }
    }
}
