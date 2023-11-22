using Microsoft.ML;
using Microsoft.ML.OnnxRuntime;
using Rubik_s_cube_solver;
using System.Diagnostics;
using System.Text;

List<int> cubeStringToInt(string cubeName)
{
    List<int> entiers = new(54);
    foreach (char item in cubeName)
    {
        if (item == 'W') entiers.Add(0);
        else if (item == 'Y') entiers.Add(1);
        else if (item == 'R') entiers.Add(2);
        else if (item == 'G') entiers.Add(3);
        else if (item == 'B') entiers.Add(4);
        else entiers.Add(5);
    }
    return entiers;
}

void GenererCSV(StreamWriter w)
{
    Cube c = new(500);
    byte[] cheminFinal = Cube.BeginnerMethod(c);
    List<List<int>> csvDico = [];
    for (int i = 0; i < cheminFinal.Length; i++)
    {
        List<int> ligne = cubeStringToInt(c.ToString());
        ligne.Add(cheminFinal[i]);
        csvDico.Add(ligne);
        if (i != cheminFinal.Length - 1)
            c.DoMove(cheminFinal[i]);
        w.WriteLine(string.Join(',', csvDico[i]));
    }
}
void GenererSecondCSV(StreamWriter w)
{
    Cube c = new(500);
    List<byte> cheminFinal = Cube.FastBeginnerMethod(c);
    List<int> ligne = cubeStringToInt(c.ToString());
    ligne.Add(cheminFinal[0]);
    w.WriteLine(string.Join(',', ligne));
}

StringBuilder GenererThirdCSV()
{
    Cube c = new(500);
    StringBuilder txt = new();
    //var cheminFinal = Cube.OptimizePath(Cube.FastMethodeDebutant(c), 3, 4, false);
    List<byte> cheminFinal = Cube.FastBeginnerMethod(c);
    List<List<int>> csvDico = [];
    for (int i = 0; i < cheminFinal.Count; i++)
    {
        List<int> ligne = cubeStringToInt(c.ToString());
        ligne.Add(cheminFinal[i]);
        ligne.Add(i);
        if (i <= 0) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 1]);
        if (i <= 1) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 2]);
        if (i <= 2) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 3]);
        csvDico.Add(ligne);
        if (i != cheminFinal.Count - 1)
            c.DoMove(cheminFinal[i]);
        //w.WriteLine(string.Join(',', csvDico[i]));
        txt.AppendLine(string.Join(',', csvDico[i]));
    }
    return txt;
}

//using StreamWriter w1 = File.AppendText("cubes.csv");
//using StreamWriter w2 = File.AppendText("cubes2.csv");



//using StreamWriter w3 = File.AppendText("cubesopti.csv");

//while (w2.BaseStream.Length < 10.2 * Math.Pow(10, 6))
//{
//    for (int i = 0; i < 100; i++)
//    {
//        GenererSecondCSV(w2);
//        Console.WriteLine(i);
//    }
//    w2.Flush();
//}

void AddDataOnFile(string appendFileName)
{
    Stopwatch timer = new();
    timer.Start();
    StreamWriter cubeopti = new(appendFileName, true);
    while (cubeopti.BaseStream.Length < Math.Pow(10, 6)) // 2 * Math.Pow(10, 6)
    {
        StringBuilder texte = new();
        for (int i = 0; i < 50; i++)
        {
            Parallel.Invoke(
                () => texte.Append(GenererThirdCSV()),
                () => texte.Append(GenererThirdCSV()),
                () => texte.Append(GenererThirdCSV()),
                () => texte.Append(GenererThirdCSV())
                );
            Console.WriteLine(i);
        }
        cubeopti.Write(texte.ToString());
    }
    cubeopti.Close();
    Console.WriteLine(timer.Elapsed.TotalSeconds);
}
//GenererEntete("cubesopti3.csv", "move", "num", "move-1", "move-2", "move-3");
//AddDataOnFile("cubesopti3.csv");

string GenererRandomCubesCSV(int shuffling)
{
    Cube c = new();
    IEnumerable<byte> randPath = c.Scramble(shuffling);
    List<int> ligne = cubeStringToInt(c.ToString());
    foreach (byte item in Cube.GetReversalPath(randPath.Reverse()))
    {
        ligne.Add(item);
    }
    return string.Join(',', ligne);
}

void AddRandomDataOnFile(string appendFileName, int shuffling)
{
    StreamWriter cubeopti = new(appendFileName, true);
    while (cubeopti.BaseStream.Length < 1.25 * Math.Pow(10, 9))
    {
        StringBuilder concatenatedContent = new();
        for (int i = 0; i < 1000; i++)
        {
            concatenatedContent.AppendLine(GenererRandomCubesCSV(shuffling));
        }
        cubeopti.Write(concatenatedContent);
    }
    cubeopti.Close();
}

int shuffling = 5;
string[] movesArray = new string[shuffling];
for (int i = 0; i < shuffling; i++)
{
    movesArray[i] = "move" + (i + 1).ToString();
}
//GenererEntete("randomCubes2.csv", movesArray);
//AddRandomDataOnFile("randomCubes2.csv", shuffling);
void GenererEntete(string appendFileName, params string[] values)
{
    StreamWriter file = new(appendFileName, false);
    List<string> t = new(54 + values.Length);
    for (int i = 1; i <= 54; i++)
    {
        t.Add("c" + i);
    }
    foreach (string otherColumnsNames in values)
    {
        t.Add(otherColumnsNames);
    }
    file.Write(string.Join(',', t) + "\n");
    file.Close();
}
//GenererEntete("cubesopti2.csv", "move", "num", "move-1", "move-2", "move-3");

//var cube2 = "0,3,2,4,0,0,3,5,5,3,0,3,2,1,3,5,2,2,1,4,0,5,2,3,0,3,2,4,1,5,1,3,0,4,1,5,4,4,1,5,4,0,1,2,3,4,1,2,2,5,5,0,4,1,0".Split(',').Select(x => float.Parse(x)).ToArray();
//Console.WriteLine(cube2.Length);
//return;

void PrintPercentage()
{
    MLContext mlContext = new();
    InferenceSession session = new("modelko2.onnx");
    double sizeM = 0.0;
    int[] dims = [1, 63];
    double cptTotal = 0.0;
    double cpt = 0.0;
    while (true)
    {
        bool solved = false;
        List<byte> moves = [];
        Cube cube1 = new(500);
        //var timer = new Stopwatch();
        //timer.Start();
        int i = 0;
        List<float> cube = cubeStringToInt(cube1.ToString()).Select(x => (float)x).ToList();
        cube.AddRange(new float[9] { i, -1, -1, -1, -1, -1, -1, -1, -1 });
        while (i <= 100)
        {
            Microsoft.ML.OnnxRuntime.Tensors.DenseTensor<float> tensor = new(cube.ToArray(), dims);
            List<NamedOnnxValue> xs = new() { NamedOnnxValue.CreateFromTensor<float>("normalization_input", tensor) };
            using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = session.Run(xs);
            DisposableNamedOnnxValue? output = results.FirstOrDefault();
            Debug.Assert(output is not null);
            Microsoft.ML.OnnxRuntime.Tensors.DenseTensor<float>? resultTensor = output.Value as Microsoft.ML.OnnxRuntime.Tensors.DenseTensor<float>;
            Debug.Assert(resultTensor is not null);
            float[] tabProba = resultTensor.ToArray();

            //var moves2 = new int[18];
            //for (int j = 0; j < 18; j++)
            //    moves2[j] = j;
            //Array.Sort(tabProba, moves2);
            float probaMax = tabProba.Max();
            int leMove = Array.IndexOf(tabProba, probaMax);
            //Console.WriteLine(leMove);
            moves.Add((byte)leMove);
            cube1.DoMove((byte)leMove);
            //Console.WriteLine(moves2[17]);
            i++;
            cube = cubeStringToInt(cube1.ToString()).Select(x => (float)x).Append(i).ToList();
            if (i <= 0) cube.Add(-1);
            else cube.Add(moves[i - 1]);
            if (i <= 1) cube.Add(-1);
            else cube.Add(moves[i - 2]);
            if (i <= 2) cube.Add(-1);
            else cube.Add(moves[i - 3]);
            if (i <= 3) cube.Add(-1);
            else cube.Add(moves[i - 4]);
            if (i <= 4) cube.Add(-1);
            else cube.Add(moves[i - 5]);
            if (i <= 5) cube.Add(-1);
            else cube.Add(moves[i - 6]);
            if (i <= 6) cube.Add(-1);
            else cube.Add(moves[i - 7]);
            if (i <= 7) cube.Add(-1);
            else cube.Add(moves[i - 8]);
            if (cube1.IsSolved)
            {
                sizeM += i;
                solved = true;
                break;
            }
        }
        cptTotal++;
        if (solved)
        {
            //timer.Stop();
            //m += timer.Elapsed.TotalSeconds;
            cpt++;
            Console.WriteLine((cpt / cptTotal) + " " + cptTotal + "\n nombre moyen de moves : " + (sizeM / cpt));
        }
        //else
        //{
        //    Console.WriteLine("Echec");
        //}
    }
}

List<List<byte>> GetKociembaResolution(string fileName)
{
    List<List<byte>> allRes = [];
    using (StreamReader reader = new(fileName))
    {
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            List<byte> res = [];
            foreach (string item in line.Split(' '))
            {
                res.Add(byte.Parse(item.ToString()));
            }
            allRes.Add(res);
        }
    }
    return allRes;
}

StringBuilder GenererKociembaCSV(List<byte> cheminFinal)
{
    StringBuilder txt = new();
    Cube c = new();
    c.ExecuterAlgorithme(Cube.GetReversalPath(cheminFinal.Reverse<byte>()));
    List<List<int>> csvDico = [];
    for (int i = 0; i < cheminFinal.Count; i++)
    {
        List<int> ligne = cubeStringToInt(c.ToString());
        ligne.Add(cheminFinal[i]);
        ligne.Add(i);
        if (i <= 0) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 1]);
        if (i <= 1) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 2]);
        if (i <= 2) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 3]);
        if (i <= 3) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 4]);
        if (i <= 4) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 5]);
        if (i <= 5) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 6]);
        if (i <= 6) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 7]);
        if (i <= 7) ligne.Add(-1);
        else ligne.Add(cheminFinal[i - 8]);
        csvDico.Add(ligne);
        if (i != cheminFinal.Count - 1)
            c.DoMove(cheminFinal[i]);
        txt.AppendLine(string.Join(',', csvDico[i]));
    }
    return txt;
}
void KociembaAddDataOnFile(string input, string appendFileName)
{
    List<List<byte>> allRes = GetKociembaResolution(input);
    File.Delete(input);
    StreamWriter cubeopti = new(appendFileName, true);
    foreach (List<byte> item in allRes)
    {
        cubeopti.Write(GenererKociembaCSV(item).ToString());
    }
    cubeopti.Close();
}
void KociembaParser(string input, string output)
{
    List<List<byte>> moves = new();
    using (StreamReader reader = new(input))
    {
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            StringBuilder newLine = new();
            foreach (string item in line.Split(" "))
            {
                if (item != "") newLine.Append(item + " ");
                else break;
            }
            moves.Add([.. Cube.GetAlgoFromStringEnum(newLine.ToString().Trim().Split(' '))]);
        }
    }
    using StreamWriter writer = new(output);
    foreach (List<byte> item in moves)
    {
        writer.WriteLine(string.Join(' ', item));
    }
}
PrintPercentage();
//KociembaParser("cubes.txt", "result.txt");
//GenererEntete("cubesoptiKociemba.csv", "move", "num", "move-1", "move-2", "move-3");
//KociembaAddDataOnFile("result.txt", "cubesoptiKociemba.csv");

//KociembaParser("cubesQuarter.txt", "result.txt");
//GenererEntete("cubesQuarteroptiKociemba2.csv", "move", "num", "move-1", "move-2", "move-3", "move-4", "move-5", "move-6", "move-7", "move-8");
//KociembaAddDataOnFile("result.txt", "cubesQuarteroptiKociemba2.csv");
