using Rubik_s_cube_solver;
using System.Diagnostics;


//int mins;
//if (args.Length == 0) mins = 30;
//else mins = int.Parse(args.First());
static void Evaluate()
{
    double sommeTemps = 0;
    int cpt = 0;
    int maxLength = 0;
    double tempsMax = 0;
    int sommeLength = 0;
    Stopwatch benchCube;
    List<byte> cheminFinal;
    while (true)
    {
        cpt++;
        Cube c1 = new(500);
        Cube cubeDeSecurite = c1.Clone();
        Console.WriteLine("Le cube mélangé : ");
        Cube.PrintWithColors(c1.PrintCubeColors());
        benchCube = new Stopwatch();
        benchCube.Start();
        cheminFinal = Cube.LightOptimization(Cube.FastBeginnerMethod(c1));
        benchCube.Stop();
        //IEnumerable<byte> cheminFinal = Cube.FastMethodeDebutantOptim(c1);
        double finalTime = benchCube.Elapsed.TotalSeconds;
        string finalStringPath = Move.GetStringPath(cheminFinal);
        int length = cheminFinal.Count;
        Console.WriteLine("Longueur de la résolution en terme de mouvements : " + length);
        Console.WriteLine("Temps " + finalTime + "s");
        cubeDeSecurite.ExecuterAlgorithme(cheminFinal);
        Console.WriteLine("Tout s'est bien passé : " + cubeDeSecurite.IsSolved);
        if (!cubeDeSecurite.IsSolved) return;
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(finalStringPath);
        Console.ResetColor();
        if (length > maxLength) maxLength = length;
        sommeLength += length;
        double temps = finalTime;
        Console.WriteLine("Temps " + temps + "s");
        sommeTemps += temps;
        if (temps > tempsMax) tempsMax = temps;
        Console.WriteLine("Longueur max " + maxLength);
        Console.WriteLine("Longueur moyenne " + (sommeLength / cpt));
        Console.WriteLine("Temps max " + tempsMax + "s");
        Console.WriteLine("Temps moyen : " + (sommeTemps / cpt) + "s");
        Console.WriteLine("Nombre d'essais : " + cpt);
    }
}
static void LightOptimizationEvaluation()
{
    int i = 1;
    double somme = 0;
    double somme2 = 0;
    double secondSomme = 0;
    while (true)
    {
        Cube c1 = new();
        Cube c2 = new();
        Cube c3 = new();
        string path = Move.GetStringPath(c3.Scramble(100).Select(x => x));
        List<byte> res = Move.GetAlgoFromStringEnum(Move.StringPathToEnum(path));
        Stopwatch timer = new();
        timer.Start();
        List<byte> optRes = Cube.LightOptimization([.. Cube.LightOptimization([.. res])]);
        double time = timer.Elapsed.TotalSeconds;
        c1.ExecuterAlgorithme(res);
        c2.ExecuterAlgorithme(optRes);
        if (c1.ToString() != c2.ToString())
        {
            Console.WriteLine(Move.GetStringPath(res) + "\n");
            Console.WriteLine(Move.GetStringPath(optRes));
            Console.WriteLine(i);
            return;
        }
        else
        {
            Console.WriteLine(Move.GetStringPath(res));
            Console.WriteLine(Move.GetStringPath(optRes) + "\n");
            somme += res.Count;
            somme2 += optRes.Count;
            secondSomme += time;
            Console.WriteLine((somme / i) + " " + (somme2 / i) + " " + (secondSomme / i) + "\n");
        }
        i++;
    }
}

//LightOptimizationEvaluation();
//Evaluate();

//Cube randomCube = new(500);
//Cube cubeDeSecurite = randomCube.Clone();
//Cube cubeDeSecurite2 = randomCube.Clone();
//Console.WriteLine("Cube aléatoire : \n");
//Cube.PrintWithColors(randomCube.PrintCubeColors());
//List<byte> resolution = Cube.FastBeginnerMethod(randomCube);
//string mouvements = Moves.GetStringPath(resolution);
//List<byte> resLightOptim = Cube.LightOptimization(resolution);
//string lightOptimPath = Moves.GetStringPath(resLightOptim);
//byte[] optimRes = Cube.OptimizePath(resolution);
//string optimizedPath = Moves.GetStringPath(optimRes);
//Console.WriteLine("Résolution : " + mouvements + "\n\n");
//Console.WriteLine("Optimized Résolution : " + optimizedPath + "\n\n");
//Console.WriteLine("Light Optimized Résolution : " + lightOptimPath + "\n\n");
//Console.WriteLine("Résolution inverse : " + Moves.GetStringPath(Cube.GetReversalPath(resolution.Reverse<byte>())) + "\n");
//Console.WriteLine("Longueur de la résolution : " + resolution.Count);
//Console.WriteLine("Longueur de la résolution optimisée (light) : " + resLightOptim.Count);
//Console.WriteLine("Longueur de la résolution optimisée : " + optimRes.Length);
//cubeDeSecurite.ExecuterAlgorithme(optimRes);
//randomCube.ExecuterAlgorithme(resolution);
//cubeDeSecurite2.ExecuterAlgorithme(resLightOptim);
//bool isOkay = cubeDeSecurite.IsSolved && randomCube.IsSolved && cubeDeSecurite2.IsSolved;
//Console.WriteLine("Tout s'est bien passé : " + isOkay);

/*Cube ct = new(500);
Cube secu = ct.Clone();
List<byte> sol = Cube.LightOptimization(Cube.TabuSearch(ct));
Console.WriteLine("Path Length : " + sol.Count);
secu.ExecuterAlgorithme(sol);
Console.WriteLine("Cube est résolu : " + secu.IsSolved);
Console.WriteLine("Résolution : " + Move.GetStringPath(sol) + "\n");
return;*/

Cube ct = new(500);
Cube.PrintWithColors(ct.PrintCubeColors());
Cube secu = ct.Clone();
Stopwatch sw = new();
sw.Start();
byte[] sol = Cube.BFSWithMITM(ct);
sw.Stop();
Console.WriteLine("Path Length : " + sol.Length);
secu.ExecuterAlgorithme(sol);
Console.WriteLine("Cube est résolu : " + secu.IsSolved + " En " + sw.ElapsedMilliseconds/1000 + "s");
Console.WriteLine("Résolution : " + Move.GetStringPath(sol) + "\n");
Console.WriteLine("Résolution inverse: " + Move.GetStringPath(Move.GetReversalPath(sol.Reverse<byte>())));
return;
while (true)
{
    Console.WriteLine("Enter the scramble : 1 - Random, 2 - User defined, 3 - Enter a Cube manually");
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
        string resolution = Move.GetStringPath(Cube.LightOptimization([.. Cube.FastBeginnerMethod(randomCube)]));
        Console.WriteLine("Resolution : " + resolution + "\n");
    }
    else if (n == 2)
    {
        Console.WriteLine("Enter the moves of your scramble (one per line) (type 0 to end)");
        Cube c = new();
        List<string> fullAlgo = [];
        while (true)
        {
            List<byte> algo;
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
            str = Console.ReadLine().ToUpper();
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.
            if (str == "0" || str == string.Empty) break;
            try
            {
                algo = Move.GetAlgoFromStringEnum([str]);
                fullAlgo.Add(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                break;
            }
            c.ExecuterAlgorithme(algo);
            Cube.PrintWithColors(c.PrintCubeColors());
        }
        //Cube.PrintWithColors(c.PrintCubeColors());
        Console.WriteLine("Scramble : " + Move.GetStringPath(Move.GetAlgoFromStringEnum(fullAlgo)));
        string resolution = Move.GetStringPath(Cube.LightOptimization(Cube.FastBeginnerMethod(c)));
        Console.WriteLine("Resolution : " + resolution + "\n");
    }
    else if (n == 3)
    {
        Console.WriteLine("Enter a cube :");
#pragma warning disable CS8602 // Déréférencement d'une éventuelle référence null.
        str = Console.ReadLine().ToUpper();
#pragma warning restore CS8602 // Déréférencement d'une éventuelle référence null.
        Cube c;
        try
        {
            c = new(str);
            string resolution = Move.GetStringPath(Cube.LightOptimization(Cube.FastBeginnerMethod(c)));
            Console.WriteLine("Resolution : " + resolution + "\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}