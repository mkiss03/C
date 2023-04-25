using System;

Random random = new();

int gyoztes = 0;
int vesztes = 0;
int dontetlen = 0;

while(true)
{ 
    Console.Clear();
    Console.WriteLine("Kő, Papír, Olló");
    Console.WriteLine();
Bevitel:
    Console.Write("Válasszon: [k]ő, [p]apír, [o]lló vagy [v]ége:");
    Tesz jatekosTesz;
    switch (Console.ReadLine()!.ToLower())
    {
    case "ko" or "k": jatekosTesz = Tesz.Ko; break;
    case "papir" or "p": jatekosTesz = Tesz.Papir; break;
    case "ollo" or "o": jatekosTesz = Tesz.Ollo; break;
    case "vege" or "v": Console.Clear(); return;
    default:
        Console.WriteLine("Hibás beírás. \nPróbája újra!"); goto Bevitel;
}

Tesz gepTesz = (Tesz)random.Next(3);
Console.WriteLine($"A gép választása {gepTesz}");

switch (jatekosTesz, gepTesz)
    {
        case (Tesz.Ko, Tesz.Papir):
        case (Tesz.Papir, Tesz.Ollo):
        case (Tesz.Ollo, Tesz.Ko):
            Console.WriteLine("Vesztettél!");
            vesztes++;
            break;
        case (Tesz.Ko, Tesz.Ollo):
        case (Tesz.Papir, Tesz.Ko):
        case (Tesz.Ollo, Tesz.Papir):
            Console.WriteLine("Nyertél!");
            gyoztes++;
            break;
        default:
            Console.WriteLine("Ez a forduló döntetlen!");
            dontetlen++;
            break;

    }
    Console.WriteLine($"Pontok: \n Győztesből: {gyoztes} \n Vesztesből: {vesztes} \n Döntetlenből: {dontetlen}");
    Console.WriteLine("Nyomja meg az Enter-t a folytatáshoz.");
    Console.ReadLine();



}


enum Tesz
    {
        Ko = 0,
        Papir = 1,
        Ollo = 2
    }