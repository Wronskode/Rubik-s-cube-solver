using Rubik_s_cube_solver;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
using System.Security;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;

//int mins;
//if (args.Length == 0) mins = 30;
//else mins = int.Parse(args.First());
double sommeTemps = 0;
int cpt = 0;
int maxLength = 0;
double tempsMax = 0;
int sommeLength = 0;

void Evaluate()
{
    while (true)
    {
        cpt++;
        Cube c1 = new();
        c1.Shuffle(500);
        Cube cubeDeSecurite = c1.Clone();
        Console.WriteLine("Le cube mélangé : ");
        Console.WriteLine(c1.PrintCube());
        var benchCube = new Stopwatch();
        benchCube.Start();
        IEnumerable<byte> cheminFinal = Cube.LightOptimization(Cube.FastMethodeDebutantOptim(c1));
        //IEnumerable<byte> cheminFinal = Cube.FastMethodeDebutantOptim(c1);
        var finalTime = benchCube.Elapsed.TotalSeconds;
        var finalStringPath = Cube.GetStringPath(cheminFinal);
        var length = cheminFinal.Count();
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
        Console.WriteLine("Longueur moyenne " + sommeLength / cpt);
        Console.WriteLine("Temps max " + tempsMax + "s");
        Console.WriteLine("Temps moyen : " + sommeTemps / cpt + "s");
        Console.WriteLine("Nombre d'essais : " + cpt);
        benchCube.Stop();
    }
}
//Evaluate();

void LightOptimizationEvaluation()
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
        var path = Cube.GetStringPath(c3.Scramble(100).Select(x => (byte)x));
        var res = Cube.GetAlgoFromStringEnum(Cube.StringPathToEnum(path));
        var timer = new Stopwatch();
        timer.Start();
        var optRes = Cube.LightOptimization(Cube.LightOptimization(res.ToList()).ToList());
        var time = timer.Elapsed.TotalSeconds;
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
            //return;
            somme += res.Count();
            somme2 += optRes.Count();
            secondSomme += time;
            Console.WriteLine(somme / i + " " + somme2 / i + " " + secondSomme / i + "\n");
        }
        i++;
    }
}

//LightOptimizationEvaluation();
Evaluate();

//Cube randomCube = new(500);
//Console.WriteLine("Cube aléatoire : \n" + randomCube.PrintCube());
//var resolution = Cube.FastMethodeDebutantOptim(randomCube);
//string mouvements = Cube.GetStringPath(resolution);
//Console.WriteLine("Résolution : " + mouvements + "\n\n");
//Console.WriteLine("Résolution inverse : " + Cube.GetStringPath(Cube.GetReversalPath(resolution.Reverse<byte>()))+"\n");

//Console.WriteLine("Longueur de la résolution : " + resolution.Count);
