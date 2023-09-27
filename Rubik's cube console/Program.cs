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
//while (true)
//{
//    cpt++;
//    var benchCube = new Stopwatch();
//    benchCube.Start();
//    Cube c1 = new();
//    c1.Shuffle(500);
//    var cubeDeSecurite = c1.Clone();
//    Console.WriteLine("Le cube mélangé : ");
//    Console.WriteLine(c1.PrintCube());
//    var cheminFinal = Cube.FastMethodeDebutant(c1);
//    Console.WriteLine("Résolution : " + Cube.GetStringPath(cheminFinal));
//    Console.WriteLine("Longueur de la résolution en terme de mouvements : " + cheminFinal.Length);
//    Console.WriteLine("Résolution inverse : " + Cube.GetStringPath(Cube.GetReversalPath(cheminFinal.Reverse().ToArray())));
//    Console.WriteLine("Temps " + benchCube.Elapsed.TotalSeconds + "s");
//    var optimizedPath = Cube.OptimizePath(cheminFinal, 3, 4, false);
//    cubeDeSecurite.ExecuterAlgorithme(optimizedPath);
//    Console.WriteLine("Tout s'est bien passé : " + cubeDeSecurite.IsSolved);
//    if (!cubeDeSecurite.IsSolved) return;
//    var test = Cube.GetStringPath(optimizedPath);
//    Console.WriteLine("");
//    Console.ForegroundColor = ConsoleColor.Red;
//    Console.WriteLine(test);
//    Console.ResetColor();
//    Console.WriteLine("Longueur de la résolution optimisée en terme de mouvements : " + optimizedPath.Length);
//    if (optimizedPath.Length > maxLength) maxLength = optimizedPath.Length;
//    sommeLength += optimizedPath.Length;
//    double temps = benchCube.Elapsed.TotalSeconds;
//    Console.WriteLine("Temps " + temps + "s");
//    sommeTemps += temps;
//    if (temps > tempsMax) tempsMax = temps;
//    Console.WriteLine("Temps moyen : " + sommeTemps / cpt);
//    Console.WriteLine("Longueur max " + maxLength);
//    Console.WriteLine("Longueur moyenne " + sommeLength / cpt);
//    Console.WriteLine("Temps max " + tempsMax);
//    benchCube.Stop();
//}

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
        var cheminFinal = Cube.LightOptimization(Cube.FastMethodeDebutantOptim(c1));
        var finalTime = benchCube.Elapsed.TotalSeconds;
        var finalStringPath = Cube.GetStringPath(cheminFinal);
        //Console.WriteLine("Résolution : " + finalStringPath);
        var length = cheminFinal.Count();
        Console.WriteLine("Longueur de la résolution en terme de mouvements : " + length);
        //Console.WriteLine("Résolution inverse : " + Cube.GetStringPath(Cube.GetReversalPath(cheminFinal.Reverse().ToArray())));
        Console.WriteLine("Temps " + finalTime + "s");
        cubeDeSecurite.ExecuterAlgorithme(cheminFinal);
        Console.WriteLine("Tout s'est bien passé : " + cubeDeSecurite.IsSolved);
        if (!cubeDeSecurite.IsSolved) return;
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(finalStringPath);
        Console.ResetColor();
        //Console.WriteLine("Longueur de la résolution optimisée en terme de mouvements : " + length);
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
        //string path = "R2L'FUR'U'BD'B'LFD2F'LD'L'RD2R'U'U'U'FDF'D'FDF'D'U'DFD'F'D'R'DRDDDB'DBDRD'R'DDDRDR'D'B'D'BDLD'L'D'F'DFDDD'L'DLDBD'B'RDFD'F'R'DFDF'DFD2F'D'FDF'DFD2F'D2BD'F'DB'D'FDRUR'U'RUR'U'D'RUR'U'RUR'U'D'RUR'U'RUR'U'D'D'";
        Cube c3 = new();
        var path = Cube.GetStringPath(c3.Scramble(100).Select(x => (byte)x));
        var res = Cube.GetAlgoFromStringEnum(Cube.StringPathToEnum(path));
        //var optRes = Cube.OptimizePath(Cube.LightOptimization(res.ToList()), 2, 6);
        //var optRes = Cube.OptimizePath(res.ToList(), 2, 6);
        var timer = new Stopwatch();
        timer.Start();
        var optRes = Cube.LightOptimization(Cube.LightOptimization(res.ToList()).ToList());
        //var optRes = Cube.OptimizePath(Cube.LightOptimization(res.ToList()), 2, 6);
        //var optRes = Cube.OptimizePath(res.ToList());
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
//var benchCube = new Stopwatch();
//benchCube.Start();
//Cube c = new(500);
//Cube c1 = c.Clone();
//Cube init = new();
//Console.WriteLine(c.PrintCube());
//byte[] test;
//do
//{
//    init.Shuffle(14);
//    test = Cube.MeetInTheMiddle5(c, init, 7);

//} while (test.Length == 0);
//c1.ExecuterAlgorithme(test);
//Console.WriteLine("Tout s'est bien passé : " + c1.IsSolved);
//Console.WriteLine(Cube.GetStringPath(test));
//Console.WriteLine("Temps " + benchCube.Elapsed.TotalSeconds + "s");
//benchCube.Stop();



//PrintPercentage();
//if (args.Length > 0)
//{
//var strCube = args.First();
//int cptW = 0;
//int cptY = 0;
//int cptR = 0;
//int cptG = 0;
//int cptB = 0;
//int cptO = 0;
//foreach (var color in strCube)
//{
//    if (color == 'W') cptW++;
//    else if (color == 'Y') cptY++;
//    else if (color == 'R') cptR++;
//    else if (color == 'G') cptG++;
//    else if (color == 'B') cptB++;
//    else if (color == 'O') cptO++;
//    else throw new Exception(color + " n'est pas une couleur");
//}

//if (cptW != 9) throw new Exception("Il y a " + cptW + " blancs à la place de 9 blancs");
//else if (cptY != 9) throw new Exception("Il y a " + cptY + " jaunes à la place de 9 jaunes");
//else if (cptR != 9) throw new Exception("Il y a " + cptR + " rouges à la place de 9 rouges");
//else if (cptG != 9) throw new Exception("Il y a " + cptG + " verts à la place de 9 verts");
//else if (cptB != 9) throw new Exception("Il y a " + cptB + " bleus à la place de 9 bleus");
//else if (cptO != 9) throw new Exception("Il y a " + cptO + " oranges à la place de 9 oranges");
//Cube cube = new(strCube);
//var r = Cube.FastMethodeDebutant(cube);
//Console.WriteLine(Cube.GetStringPath(r));
//Console.WriteLine(Cube.GetStringPath(Cube.OptimizePath(r)));
//}
//Cube cube = new Cube(500);
//Console.WriteLine(cube.ToString());

