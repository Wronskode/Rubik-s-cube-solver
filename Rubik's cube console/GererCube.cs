using Rubik_s_cube_solver;

namespace Rubik_s_cube_console
{
    public class GererCube
    {
        public static string SolveCube(string strCube)
        {
            int cptW = 0;
            int cptY = 0;
            int cptR = 0;
            int cptG = 0;
            int cptB = 0;
            int cptO = 0;
            foreach (var color in strCube)
            {
                if (color == 'W') cptW++;
                else if (color == 'Y') cptY++;
                else if (color == 'R') cptR++;
                else if (color == 'G') cptG++;
                else if (color == 'B') cptB++;
                else if (color == 'O') cptO++;
                else throw new Exception(color + " n'est pas une couleur");
            }

            if (cptW != 9) throw new Exception("Il y a " + cptW + " blancs à la place de 9 blancs");
            else if (cptY != 9) throw new Exception("Il y a " + cptY + " jaunes à la place de 9 jaunes");
            else if (cptR != 9) throw new Exception("Il y a " + cptR + " rouges à la place de 9 rouges");
            else if (cptG != 9) throw new Exception("Il y a " + cptG + " verts à la place de 9 verts");
            else if (cptB != 9) throw new Exception("Il y a " + cptB + " bleus à la place de 9 bleus");
            else if (cptO != 9) throw new Exception("Il y a " + cptO + " oranges à la place de 9 oranges");
            Cube cube = new(strCube);
            return Cube.GetStringPath(Cube.LightOptimization(Cube.FastMethodeDebutantOptim(cube)));
        }
    }
}
