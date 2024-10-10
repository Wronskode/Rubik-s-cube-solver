using System.Diagnostics;
using CubeLibrary;

while (true)
{
    Console.WriteLine("1 - Random cube, 2 - User defined, 3 - Enter a Cube manually");
    string? str = Console.ReadLine();
    bool ok = int.TryParse(str, out int n);
    if (!ok) return;
    if (n == 1)
    {
        Console.WriteLine("Enter the number of moves of the scramble");
        str = Console.ReadLine();
        ok = int.TryParse(str, out n);
        if (!ok) return;
        if (n < 0) return;
        Cube randomCube = new();
        string randomPath = Move.GetStringPath(randomCube.Scramble(n));
        Console.WriteLine("Random scramble : " + randomPath + "\n");
        Cube.PrintWithColors(randomCube.PrintCubeColors());
        string? resolution = null;
        long duration = 0;
        do
        {
            Console.WriteLine("1 - Solve using beginner method, 2 - Solve using Kociemba algorithm (this may take time, avg : <120s)");
            str = Console.ReadLine();
            bool v = int.TryParse(str, out int result);
            while (!v)
            {
                str = Console.ReadLine();
                v = int.TryParse(str, out result);
            }
            if (result == 1)
            {
                Stopwatch sw = new();
                sw.Start();
                resolution = Move.GetStringPath(Cube.LightOptimization([.. Cube.FastBeginnerMethod(randomCube)]));
                sw.Stop();
                duration = sw.ElapsedMilliseconds / 1000;
            }
            else if (result == 2)
            {
                Console.WriteLine("Solving...");
                Stopwatch sw = new();
                sw.Start();
                resolution = Move.GetStringPath(randomCube.Kociemba());
                sw.Stop();
                duration = sw.ElapsedMilliseconds / 1000;
                
            }
        } while (resolution is null);
        Console.WriteLine("Resolution : " + resolution + "\n");
        Console.WriteLine("Duration : " + duration + "s");
    }
    else if (n == 2)
    {
        Console.WriteLine("Enter the scramble (e.g. : URU'R'F2B'R)");
        Cube c = new();
        IEnumerable<string> fullAlgo;
        List<byte> algo;
        str = Console.ReadLine();
        if (str != null) str = str.ToUpper();
        if (str == "0" || str == string.Empty || str == null) break;
        try
        {
            fullAlgo = Move.StringPathToEnum(str);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            break;
        }
        c.ExecuterAlgorithme(fullAlgo);
        Cube.PrintWithColors(c.PrintCubeColors());
        Console.WriteLine("Scramble : " + Move.GetStringPath(Move.GetAlgoFromStringEnum(fullAlgo)));
        string? resolution = null;
        long duration = 0;
        do
        {
            Console.WriteLine("1 - Solve using beginner method, 2 - Solve using Kociemba algorithm (this may take time, avg : <120s)");
            str = Console.ReadLine();
            bool v = int.TryParse(str, out int result);
            while (!v)
            {
                str = Console.ReadLine();
                v = int.TryParse(str, out result);
            }
            if (result == 1)
            {
                Stopwatch sw = new();
                sw.Start();
                resolution = Move.GetStringPath(Cube.LightOptimization([.. Cube.FastBeginnerMethod(c)]));
                sw.Stop();
                duration = sw.ElapsedMilliseconds / 1000;
            }
            else if (result == 2)
            {
                Console.WriteLine("Solving...");
                Stopwatch sw = new();
                sw.Start();
                resolution = Move.GetStringPath(c.Kociemba());
                sw.Stop();
                duration = sw.ElapsedMilliseconds / 1000;
            }
        } while (resolution is null);
        Console.WriteLine("Resolution : " + resolution + "\n");
        Console.WriteLine("Duration : " + duration + "s");
    }
    else if (n == 3)
    {
        Console.WriteLine("Enter a cube :");
        str = Console.ReadLine();
        if (str != null) str = str.ToUpper();
        else continue;
        Cube c;
        try
        {
            c = new(str);
            string? resolution = null;
            long duration = 0;
            do
            {
                Console.WriteLine("1 - Solve using beginner method, 2 - Solve using Kociemba algorithm (this may take time, avg : <120s)");
                str = Console.ReadLine();
                bool v = int.TryParse(str, out int result);
                while (!v)
                {
                    str = Console.ReadLine();
                    v = int.TryParse(str, out result);
                }
                
                if (result == 1)
                {
                    Stopwatch sw = new();
                    sw.Start();
                    resolution = Move.GetStringPath(Cube.LightOptimization([.. Cube.FastBeginnerMethod(c)]));
                    sw.Stop();
                    duration = sw.ElapsedMilliseconds / 1000;
                }
                else if (result == 2)
                {
                    Console.WriteLine("Solving...");
                    Stopwatch sw = new();
                    sw.Start();
                    resolution = Move.GetStringPath(c.Kociemba());
                    sw.Stop();
                    duration = sw.ElapsedMilliseconds / 1000;
                }
            } while (resolution == null);
            Console.WriteLine("Resolution : " + resolution + "\n");
            Console.WriteLine("Duration : " + duration + "s");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}