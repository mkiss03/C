using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.ComTypes;

Exception? kivetel = null;
Irany? irany = null;
bool BezarasKerese = false;
Random random = new Random();
int szelesseg = Console.WindowWidth;
int magassag = Console.WindowHeight;
Queue<(int x, int y)> kigyo = new();
(int x, int y) = (szelesseg / 2, magassag / 2);
Mezo[,] terkep = new Mezo[szelesseg, magassag];
char[] IranyKarakterek = new char[] { '^', 'v', '>', '<' };
TimeSpan kivaras = TimeSpan.FromMilliseconds(100);


try
{
    //Ha minden ok
    Console.CursorVisible = false;
    Console.Clear();
    kigyo.Enqueue((x, y));
    terkep[x, y] = Mezo.Kigyo;
    Console.SetCursorPosition(x, y);
    Console.Write('@');
    KajaHelye();
    while (!irany.HasValue && !BezarasKerese)
    {
        IranyMegadasa();
    }
    while (!BezarasKerese)
    {
        Console.Clear();
        switch (irany)
        {
            case Irany.Fel:
                y--;
                break;
            case Irany.Le:
                y++;
                break;
            case Irany.Jobbra:
                x++;
                break;
            case Irany.Balra:
                x--;
                break;
        }
        if (x < 0 || x >= szelesseg || y < 0 || y >= magassag || terkep[x, y] == Mezo.Kigyo)
        {
            Console.Clear();
            Console.Write("A játéknak vége!" + (kigyo.Count() - 1) + ".");
            return;

        }
        Console.SetCursorPosition(x, y);
        Console.Write(IranyKarakterek[(int)irany!]);
        kigyo.Enqueue((x, y));
        if (terkep[x, y] == Mezo.Kaja)
        {
            KajaHelye();
        }
        else
        {
            (int X, int Y) = kigyo.Dequeue();
            terkep[x, y] = Mezo.Ures;
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }
        terkep[x, y] = Mezo.Kigyo;
        if (Console.KeyAvailable)
        {
            IranyMegadasa();
        }
        System.Threading.Thread.Sleep(kivaras);
    }
}

catch (Exception e)
{
    //Ha hiba van
    kivetel = e;
    throw;
}

finally
{
    //Ha minden esetben lefut
    if (kivetel != null)
    {
        Console.WriteLine("Hiba történt: {0}", kivetel.Message);
    }
}

void IranyMegadasa()
{
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.UpArrow:
            irany = Irany.Fel;
            break;
        case ConsoleKey.DownArrow:
            irany = Irany.Le;
            break;
        case ConsoleKey.RightArrow:
            irany = Irany.Jobbra;
            break;
        case ConsoleKey.LeftArrow:
            irany = Irany.Balra;
            break;
        case ConsoleKey.Escape:
            BezarasKerese = true;
            break;
    }
}

void KajaHelye()
{
    List<(int x, int y)> LehetsegesHelyek = new List<(int x, int y)>();
    for (int i = 0; i < szelesseg; i++)
    {
        for (int j = 0; j < magassag; j++)
        {
            if (terkep[i, j] == Mezo.Ures)
            {
                LehetsegesHelyek.Add((i, j));
            }
        }
    }

    int index = random.Next(LehetsegesHelyek.Count());
    (int x, int y) = LehetsegesHelyek[index];
    terkep[x, y] = Mezo.Kaja;
    Console.SetCursorPosition(x, y);
    Console.Write('+');
}

enum Irany
{
    Fel = 0,
    Le = 1,
    Jobbra = 2,
    Balra = 3
}

enum Mezo
{
    Ures = 0,
    Kigyo,
    Kaja
}

