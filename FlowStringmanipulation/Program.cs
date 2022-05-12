using System;
using System.Collections.Generic;

namespace FlowStringmanipulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool run = true; // Loop-brytare.
            do
            {
                // Eftersom menyn endast används här behöver vi inte skapa en metod för att skapa den.
                Console.Clear();
                Console.WriteLine("Välkommen till BIO");
                Console.WriteLine("0. Stäng av programmet");
                Console.WriteLine("1. Köp enskild biljett");
                // Jag hade haft ETT menyval för biljettköp, om man vill köpa en biljett kan man välja 1 biljett helt enkelt.
                Console.WriteLine("1b. Köp biljetter för sällskap");
                // Lite malicious compliance här.
                // Enligt specifikation ville ni att case 2 skulle vara upprepning så då får sällskapsbiljettköpet hamna osmidigt.
                // I vanliga fall pratar vi med klienten om detta.
                Console.WriteLine("2. Upprepningar");
                Console.WriteLine("3. Splitta sträng");
                Console.WriteLine();
                Console.Write("Menyval: "); //Jag gillar att berätta för användaren vad vi håller på med i inputbuffern.

                string huvudmenyVal = Console.ReadLine();
                switch (huvudmenyVal)
                {
                    case "0":
                        run = false; //Loop-brytare, stänger av programmet.
                        break;

                    case "1":
                        Console.Clear();
                        KöpBiljett(); //Egna metoderna finns efter Main-metoden.
                        VäntaPåAnvändare();
                        break;

                    case "1b": // Specifikation sa både att vi skulle skapa en case för detta samt att case 2 skulle vara något
                        // helt annat så vi kompromissar med att döpa detta val till 1b.
                        Console.Clear();
                        Console.WriteLine("Köp biljetter för sällskap");
                        Sällskap(ValideraUINT("Antal besökare: "));
                        VäntaPåAnvändare();
                        break;

                    case "2":
                        Console.WriteLine("Skriv in något du vill upprepa 10 gånger");
                        Console.Write("Upprepa: ");
                        string upprepa = Console.ReadLine();
                        uint upprepningar = 0;
                        while (upprepningar < 10)
                        //En loop som lägger till ett heltal i variabeln varje varv tills vi når 9
                        //(börjar räkna från 0 så det blir 10 loopar)
                        {
                            upprepningar++;
                            Console.WriteLine($"{upprepningar}. {upprepa}");
                        }
                        VäntaPåAnvändare();
                        break;

                    case "3":
                        Console.WriteLine("Skriv en mening med minst tre ord så visar vi dig det tredje ordet");
                        do
                        {
                            Console.Write("Mening: ");
                            string mening = Console.ReadLine();
                            string[] ord = mening.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                            // Vi använder TrimEntries och RemoveEmptyEntries tillsammans för att ta bort whitespace. Tack IntelliSense.
                            if (ord.Length < 3)
                            {
                                Console.WriteLine("Du måste skriva minst tre ord.");
                            }
                            else
                            {
                                Console.WriteLine($"Det tredje ordet är: {ord[2]}");
                                break;
                            }
                        } while (true);
                        VäntaPåAnvändare();
                        break;

                    default:
                        break;
                }
            } while (run);
        }

        static uint KöpBiljett()
        //En metod för att köpa biljetter. Den returnerar kostnaden för biljetten så att vi kan använda den
        //som input i en annan metod, metoden Sällskap där vi sammanslår priset för flera biljetter.
        {
            //Eftersom dessa värden inte behöver vara ändringsbara/variabla i programmet kan vi deklarera dem som konstanter.
            const uint ungdomsPris = 80;
            const uint pensionärsPris = 90;
            const uint standardPris = 120;
            const uint barnPris = 0;
            const uint näraDödenPris = 0; // Vi skulle kunna slå ihop denna med barnPris och
                                          // använda || i if-satsen men detta känns tydligare.
            do
            {
                uint ålderUint = ValideraUINT("Ålder på besökare: "); //Vi ser först till att användarens input är ett positiv heltal.
                if (ålderUint < 5) //Sen matchar vi åldern med if-satser och returnerar priset från konstanterna ovan.
                {
                    Console.WriteLine($"Barn under 5 år går gratis");
                    return barnPris;
                }
                else if (ålderUint > 100)
                {
                    Console.WriteLine($"Pensionärer över 100 år går gratis");
                    return näraDödenPris;
                }
                else if (ålderUint < 20)
                {
                    Console.WriteLine($"Ungdomspris: {ungdomsPris}kr");
                    return ungdomsPris;
                }
                else if (ålderUint > 64)
                {
                    Console.WriteLine($"Pensionärspris: {pensionärsPris}kr");
                    return pensionärsPris;
                }
                else
                {
                    Console.WriteLine($"Standardpris: {standardPris}kr");
                    return standardPris;
                }
            } while (true);
        }

        static void Sällskap(uint input)
        //Metoden tar emot hur många biljetter som ska köpas, visar detta för kunden tillsammans med kostnaden för alla biljetterna.
        {
            uint summa = 0;
            //Vi kan jobba direkt i en heltalsvariabel eftersom vi inte har användning för en
            // array eller lista enligt spec.
            for (int i = 0; i < input; i++) 
            {
                summa += KöpBiljett();
            }
            Console.WriteLine($"\nAntal personer i sällskapet: {input}");
            Console.WriteLine($"Total kostnad för sällskapet: {summa}kr");
        }

        static uint ValideraUINT(string inputBuffer)
        {
            do
            {
                Console.Write(inputBuffer);
                if (uint.TryParse(Console.ReadLine(), out uint output))
                //TryParse returerar en sann boolean om dess input är ett positiv heltal
                {
                    return output;
                }
                else
                //Om input inte är ett positivt heltal så kör vi loopen igen och meddelar användaren om problemet.
                {
                    Console.WriteLine("Endast positiva heltal är tillåtna.\n");
                }
            } while (true);
        }

        static void VäntaPåAnvändare()
        //En enkel metod så att vi kan stanna upp programmet så att användaren hinner konfimera vad som händer.
        {
            Console.WriteLine("\nTryck på Enter för att återgå till huvudmenyn.");
            Console.ReadLine();
        }
    }

}
