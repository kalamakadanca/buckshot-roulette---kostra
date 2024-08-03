using System;
using System.Net.NetworkInformation;

class Program
{
    static void Main(string[] args)
    {
        bool prvni_zapnuti = true; //prvni zapnuti hry
        bool play_again = true; //chcete hrat znovu?
        bool aktivni_hra = false; //probihajici hra
        bool kolo = true; //true = hraje hrac, false = hraje dealer
        bool plny_zasobnik = false; //jestli je plny zasobnik
        String rozhodnuti; //chcete hrat znovu? <prechodna promenna>
        int penizky = 0; //udrzovany pocet penez
        int vyhra = 0; //random pocet pri vyhre
        bool vyber; //vyber dealera jestli strelit sebe ci hrace <true = hrac, false = dealer>
        bool moron = true; //kdyby nekdo nezadal spravne ano/ne
        //chce se mi brecet pokazdy kdyz narazim na tuto cast kodu  /*
        bool dalsi_hra = false; //zobrazi se naboje pri dalsi hre <prechodna promenna>
        bool streleno_tupym = false; //jestli se clovek streli tupym, nezobrazi se zivoty <prechodna promenna>
        //                                                          */

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
        while (play_again)
        {
            //vsechno kolem naboju /*
            pocet_naboju = random.Next(3, 8);
            kontrola_naboju = 0;
            pocet_tupych = 0;
            pocet_pravych = 0;
            prave_tupe.Clear();
            kolo = true;

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

            //CO HRAC UVIDI /*
            if (prvni_zapnuti)
            {
                prvni_zapnuti = false;
                pravidla();
                Console.WriteLine("Stiskni libovolnou klávesu pro start");
                Console.ReadKey(true);
                Console.WriteLine();
            }
            if (aktivni_hra == false || dalsi_hra || plny_zasobnik == false)
            {
                if (aktivni_hra == true)
                {
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("NÁBOJE");
                Console.WriteLine("Pravých nábojů je: " + pocet_pravych);
                Console.WriteLine("Tupých nábojů je: " + pocet_tupych);
                Console.ResetColor();
                Console.WriteLine();
                aktivni_hra = true;
                dalsi_hra = false;
            }
            //              */

            plny_zasobnik = true;

            //hlavni hra    /*
            while (plny_zasobnik)
            {


                //smrt hrace/dealera    /*
                if (srdce_hrace == 0)
                {
                    plny_zasobnik = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Byl jsi zabit");
                    Console.ResetColor();
                    moron = true;
                }
                else if (srdce_dealera == 0)
                {
                    plny_zasobnik = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine("Vyhrál jsi!!!");
                    Console.ResetColor();
                    vyhra = random.Next(5, 16);
                    penizky += vyhra;
                    Console.WriteLine("Tvoje vyhra: " + vyhra);
                    moron = true;
                }

                if (srdce_hrace == 0 || srdce_dealera == 0)
                {
                    while (moron)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Chcete hrát znovu? (ano/ne)");
                        Console.ResetColor();
                        rozhodnuti = Console.ReadLine().ToLower().Trim();
                        if (rozhodnuti == "ano")
                        {
                            srdce_hrace = 3;
                            srdce_dealera = 3;
                            dalsi_hra = true;
                            break; // Ukončení smyčky plny_zasobnik pro restart hry
                        }
                        else if (rozhodnuti == "ne")
                        {
                            play_again = false; // Ukončení hlavní smyčky pro ukončení programu
                            break;
                        }
                    }
                    break;
                }
                //                     */

                int vyber_naboje = random.Next(prave_tupe.Count);

                if (prave_tupe.Count != 0)
                {
                    //hracovo kolo  /*
                    if (kolo)
                    {
                        Console.WriteLine("-----------------------------");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        if (streleno_tupym == false)
                        {
                            Console.WriteLine("Počet životů hráče: " + srdce_hrace);
                            Console.WriteLine("Počet životů dealera: " + srdce_dealera);
                            Console.ResetColor();
                            Console.WriteLine("-----------------------------");
                        }
                        else
                        {
                            streleno_tupym = false;
                        }
                        Console.WriteLine("Chcete střelit sebe či dealera?");
                        Console.ForegroundColor= ConsoleColor.DarkGray;
                        String koho_strelit = Console.ReadLine().ToLower().Trim();
                        Console.ResetColor();
                        switch (koho_strelit)
                        {
                            case "sebe":
                                if (prave_tupe[vyber_naboje] == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine("tupý náboj");
                                    Console.ResetColor();
                                    streleno_tupym = true;
                                    pocet_tupych--;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Ostrý náboj");
                                    Console.ResetColor();
                                    srdce_hrace--;
                                    pocet_pravych--;
                                    kolo = false;
                                }
                                prave_tupe.RemoveAt(vyber_naboje);
                                break;
                            case "dealera":
                                if (prave_tupe[vyber_naboje] == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine("Tupý náboj");
                                    Console.ResetColor();
                                    pocet_tupych--;
                                    kolo = false;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Ostrý náboj");
                                    Console.ResetColor();
                                    srdce_dealera--;
                                    pocet_pravych--;
                                    kolo = false;
                                }
                                prave_tupe.RemoveAt(vyber_naboje);
                                break;
                        }
                    }
                    //              */

                    //dealerovo kolo    /*
                    else
                    {
                        Console.WriteLine();
                        if (pocet_pravych == pocet_naboju)
                        {
                            Console.WriteLine("Dealer střelil tebe");
                            srdce_hrace--;
                            prave_tupe.RemoveAt(vyber_naboje);
                            pocet_pravych--;
                            kolo = true;
                        }
                        else if (pocet_tupych == pocet_naboju)
                        {
                            Console.WriteLine("Dealer střelil sebe");
                            prave_tupe.RemoveAt(vyber_naboje);
                            pocet_tupych--;
                            kolo = true;
                        }
                        else
                        {
                            vyber = random.Next(2) == 0;
                            switch (vyber)
                            {
                                case false:
                                    Console.WriteLine("Dealer střelil sebe");
                                    if (prave_tupe[vyber_naboje] == false)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.WriteLine("Tupý náboj");
                                        Console.ResetColor();
                                        pocet_tupych--;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor= ConsoleColor.DarkRed;
                                        Console.WriteLine("Ostrý náboj");
                                        Console.ResetColor();
                                        pocet_pravych--;
                                        srdce_dealera--;
                                        kolo = true;
                                    }
                                    prave_tupe.RemoveAt(vyber_naboje);
                                    break;
                                case true:
                                    Console.WriteLine("Dealer střelil tebe");
                                    if (prave_tupe[vyber_naboje] == false)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        Console.WriteLine("Tupý náboj");
                                        Console.ResetColor();
                                        pocet_tupych--;
                                        kolo = true;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("Ostrý náboj");
                                        Console.ResetColor();
                                        pocet_pravych--;
                                        srdce_hrace--;
                                        kolo = true;
                                    }
                                    prave_tupe.RemoveAt(vyber_naboje);
                                    break;
                            }
                        }
                    }
                    //                  */
                }
                else
                {
                    plny_zasobnik = false;
                }
            }//             */
        }   //
        static void pravidla()
        {
            Console.WriteLine("***PRAVIDLA***");
            //nastaveni barvicky
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Vítej ve hře \"Buckshot Roullete\".");
            Console.WriteLine("Tvým úkolem je zabít dealera.");
            Console.WriteLine("Po vyprázdnění brokovnice ti bude zobrazen počet pravých a tupých nábojů. Začínáš ty a vybereš si, na koho chceš střílet.\nHraje se do smrti jednoho z vás.");
            //reset barvicky
            Console.ResetColor();
        }

    }
}