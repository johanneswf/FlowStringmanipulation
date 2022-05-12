using System;
using System.Collections.Generic;

namespace FlowStringmanipulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Välkommen till BIO");
                Console.WriteLine("0. Stäng av programmet");
                Console.WriteLine("1. Köp enskild biljett");
                // Jag hade haft ETT menyval för biljettköp, om man vill köpa en biljett kan man välja 1 biljett helt enkelt.
                Console.WriteLine("1b. Köp biljetter för sällskap (malicious compliance)");
                // Enligt specifikation ville ni att case 2 skulle vara upprepning så då får sällskapsbiljettköpet hamna osmidigt.
                // I vanliga fall pratar vi med klienten om detta.
                Console.WriteLine("2. Upprepningar");
                Console.WriteLine("3. Splitta sträng");
                Console.WriteLine();
                Console.Write("Val: ");

                string huvudmenyVal = Console.ReadLine();
                switch (huvudmenyVal)
                {
                    case "0":
                        run = false;
                        break;
                    case "1":
                        Console.Clear();
                        KöpBiljett();
                        VäntaPåAnvändare();
                        break;
                    case "1b":
                        Console.Clear();
                        Console.WriteLine("Köp biljetter för sällskap");
                        Console.Write("Antal besökare: ");
                        Sällskap(ValideraUINT());
                        VäntaPåAnvändare();
                        break;
                    case "2":
                        Console.WriteLine("Skriv in något du vill upprepa 10 gånger");
                        Console.Write("Upprepa: ");
                        string upprepa = Console.ReadLine();
                        uint upprepningar = 0;
                        while (upprepningar < 10)
                        {
                            upprepningar++;
                            Console.WriteLine($"{upprepningar}. {upprepa}");
                        }
                        VäntaPåAnvändare();
                        break;
                    case "3":
                        Console.WriteLine("Skriv en mening med minst tre ord så visar vi dig det tredje ordet");
                        Console.Write("Mening: ");
                        string mening = Console.ReadLine();
                        string[] ord = mening.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                        // Vi använder TrimEntries och RemoveEmptyEntries tillsammans för att ta bort whitespace. Tack IntelliSense.
                        Console.WriteLine($"Det tredje ordet är: {ord[2]}");
                        VäntaPåAnvändare();
                        break;
                }
            } while (run);
        }

        static uint KöpBiljett()
        {
            const uint ungdomsPris = 80;
            const uint pensionärsPris = 90;
            const uint standardPris = 120;
            const uint barnPris = 0;
            const uint näraDödenPris = 0;
            do
            {
                Console.Write("Ålder på besökare: ");
                uint ålderUint = ValideraUINT();
                if (ålderUint < 5)
                {
                    Console.WriteLine($"Barn under 5 år går gratis");
                    return barnPris;
                }
                if (ålderUint > 100)
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
        {
            uint summa = 0;
            for (int i = 0; i < input; i++)
            {
                summa = summa + KöpBiljett();
            }
            Console.WriteLine($"Antal personer i sällskapet: {input}");
            Console.WriteLine($"Total kostnad för sällskapet: {summa}kr");
        }

        static uint ValideraUINT()
        {
            do
            {
                if (uint.TryParse(Console.ReadLine(), out uint output))
                {
                    return output;
                }
                else
                {
                    Console.WriteLine("Endast positiva heltal är tillåtna.");
                }
            } while (true);
        }

        static void VäntaPåAnvändare()
        {
            Console.WriteLine("Tryck på Enter för att återgå till huvudmenyn.");
            Console.ReadLine();
        }
    }

}
