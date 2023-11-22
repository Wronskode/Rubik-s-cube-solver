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
        string finalStringPath = Cube.GetStringPath(cheminFinal);
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
        string path = Cube.GetStringPath(c3.Scramble(100).Select(x => x));
        List<byte> res = Cube.GetAlgoFromStringEnum(Cube.StringPathToEnum(path));
        Stopwatch timer = new();
        timer.Start();
        List<byte> optRes = Cube.LightOptimization([.. Cube.LightOptimization([.. res])]);
        double time = timer.Elapsed.TotalSeconds;
        c1.ExecuterAlgorithme(res);
        c2.ExecuterAlgorithme(optRes);
        if (c1.ToString() != c2.ToString())
        {
            Console.WriteLine(Cube.GetStringPath(res) + "\n");
            Console.WriteLine(Cube.GetStringPath(optRes));
            Console.WriteLine(i);
            return;
        }
        else
        {
            Console.WriteLine(Cube.GetStringPath(res));
            Console.WriteLine(Cube.GetStringPath(optRes) + "\n");
            somme += res.Count;
            somme2 += optRes.Count;
            secondSomme += time;
            Console.WriteLine((somme / i) + " " + (somme2 / i) + " " + (secondSomme / i) + "\n");
        }
        i++;
    }
}

//LightOptimizationEvaluation();
Evaluate();

Cube randomCube = new(500);
Cube cubeDeSecurite = randomCube.Clone();
Cube cubeDeSecurite2 = randomCube.Clone();
Console.WriteLine("Cube aléatoire : \n");
Cube.PrintWithColors(randomCube.PrintCubeColors());
List<byte> resolution = Cube.FastBeginnerMethod(randomCube);
string mouvements = Cube.GetStringPath(resolution);
List<byte> resLightOptim = Cube.LightOptimization(resolution);
string lightOptimPath = Cube.GetStringPath(resLightOptim);
byte[] optimRes = Cube.OptimizePath(resolution);
string optimizedPath = Cube.GetStringPath(optimRes);
Console.WriteLine("Résolution : " + mouvements + "\n\n");
Console.WriteLine("Optimized Résolution : " + optimizedPath + "\n\n");
Console.WriteLine("Light Optimized Résolution : " + lightOptimPath + "\n\n");
Console.WriteLine("Résolution inverse : " + Cube.GetStringPath(Cube.GetReversalPath(resolution.Reverse<byte>())) + "\n");
Console.WriteLine("Longueur de la résolution : " + resolution.Count);
Console.WriteLine("Longueur de la résolution optimisée (light) : " + resLightOptim.Count);
Console.WriteLine("Longueur de la résolution optimisée : " + optimRes.Length);
cubeDeSecurite.ExecuterAlgorithme(optimRes);
randomCube.ExecuterAlgorithme(resolution);
cubeDeSecurite2.ExecuterAlgorithme(resLightOptim);
bool isOkay = cubeDeSecurite.IsSolved && randomCube.IsSolved && cubeDeSecurite2.IsSolved;
Console.WriteLine("Tout s'est bien passé : " + isOkay);