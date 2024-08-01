using System;
using System.Net.NetworkInformation;

class Program
{
    private static bool running = true;

    static void Main(string[] args)
    {
        bool prvni_zapnuti = true; //prvni zapnuti hry
        bool play_again = true; //chcete hrat znovu?
        bool aktivni_hra = false; //probihajici hra
        bool kolo = true; //true = hraje hrac, false = hraje dealer
        bool plny_zasobnik = true; //jestli je plny zasobnik

        //volne k pouziti
        Random random = new Random();

        int pocet_naboju;
        List<bool> prave_tupe = new List<bool>();
        int kontrola_naboju;
        int pocet_tupych;
        int pocet_pravych;


        //srdicko /*
        int srdce_hrace = 3;
        int srdce_dealera = 3;
        //        */
        while (running)
        {
            //vsechno kolem naboju /*
            pocet_naboju = random.Next(3, 9);
            kontrola_naboju = 0;
            pocet_tupych = 0;
            pocet_pravych = 0;

            for (int i = 0; i < pocet_naboju; i++)
            {
                bool rozhodovani_naboju = random.Next(2) == 0;
                prave_tupe.Add(rozhodovani_naboju);
                if (prave_tupe[i] == false)
                {
                    kontrola_naboju++;
                }
            }

            prave_tupe.Sort();
            //kontrola naboju     **
            if (kontrola_naboju == pocet_naboju)
            {
                prave_tupe[pocet_naboju - 1] = true;
            }
            else if (kontrola_naboju == 0)
            {
                prave_tupe[0] = false;
            }

            for (int i = 0; i < prave_tupe.Count; i++)
            {
                if (prave_tupe[i] == false)
                {
                    pocet_tupych++;
                }
                else
                {
                    pocet_pravych++;
                }
            }
            //                    */

            plny_zasobnik = true;

            //CO HRAC UVIDI /*
            if (prvni_zapnuti)
            {
                prvni_zapnuti = false;
                pravidla();
                Console.WriteLine("Stiskni libovolnou klávesu pro pokračování");
                Console.ReadKey(true);
                Console.WriteLine();
            }
            if (aktivni_hra == false)
            {
                Console.WriteLine("NÁBOJE");
                Console.WriteLine("Pravých nábojů je: " + pocet_pravych);
                Console.WriteLine("Tupých nábojů je: " + pocet_tupych);
                Console.WriteLine("ŽIVOTY");
                aktivni_hra = true;
            }
            //              */

            //hlavni hra    /*
            while (plny_zasobnik)
            {
                Console.WriteLine("Počet životů hráče: " + srdce_hrace);
                Console.WriteLine("Počet životů dealera: " + srdce_dealera);
                int vyber_naboje = random.Next(prave_tupe.Count);

                if (prave_tupe.Count != 0)
                {

                    if (kolo)
                    {
                        Console.WriteLine("Chcete střelit sebe či dealera?");
                        String koho_strelit = Console.ReadLine().ToLower().Trim();
                        switch (koho_strelit)
                        {
                            case "sebe":
                                if (prave_tupe[vyber_naboje] == false)
                                {
                                    Console.WriteLine("tupý náboj");
                                    prave_tupe.Remove(false);
                                }
                                else
                                {
                                    Console.WriteLine("Ostrý náboj");
                                    srdce_hrace--;
                                    prave_tupe.Remove(true);
                                }
                                break;
                            case "dealera":
                                if (prave_tupe[vyber_naboje] == false)
                                {
                                    Console.WriteLine("tupý náboj");
                                    prave_tupe.Remove(false);
                                }
                                else
                                {
                                    Console.WriteLine("Ostrý náboj");
                                    srdce_dealera--;
                                    prave_tupe.Remove(true);
                                }
                                break;
                        }
                        kolo = false;
                    }
                    else
                    {
                        kolo = true;
                    }
                }
                else
                {
                    plny_zasobnik = false;
                }

            }
        }
        static void pravidla()
        {
            Console.WriteLine("***PRAVIDLA***");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Vítej ve hře \"Buckshot Roullete\".");
            Console.WriteLine("Tvým úkolem je zabít dealera. Na začátku hry ti bude zobrazen počet pravých/tupých nábojů a životů obou z vás");
            Console.WriteLine("Začínáš ty a vybereš si, na koho chceš střílet. Po vyprázdnění brokovnice se opět naplní.\nHraje se do smrti jednoho z vás");
            Console.ResetColor();
        }
        static void KeyListener()
        {
            while (running)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.C)
                    {
                        pravidla();
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}