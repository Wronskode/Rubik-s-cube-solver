using CubeLibrary;
using Microsoft.AspNetCore.Mvc;

namespace MySite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiRubiksCube : ControllerBase
    {
        public static string SolveCube(string strCube)
        {
            int cptW = 0;
            int cptY = 0;
            int cptR = 0;
            int cptG = 0;
            int cptB = 0;
            int cptO = 0;
            foreach (char color in strCube)
            {
                if (color == 'W') cptW++;
                else if (color == 'Y') cptY++;
                else if (color == 'R') cptR++;
                else if (color == 'G') cptG++;
                else if (color == 'B') cptB++;
                else if (color == 'O') cptO++;
                else throw new Exception(color + " n'est pas une couleur");
            }

            if (cptW != 9) throw new Exception("Il y a " + cptW + " blancs � la place de 9 blancs");
            if (cptY != 9) throw new Exception("Il y a " + cptY + " jaunes � la place de 9 jaunes");
            if (cptR != 9) throw new Exception("Il y a " + cptR + " rouges � la place de 9 rouges");
            if (cptG != 9) throw new Exception("Il y a " + cptG + " verts � la place de 9 verts");
            if (cptB != 9) throw new Exception("Il y a " + cptB + " bleus � la place de 9 bleus");
            if (cptO != 9) throw new Exception("Il y a " + cptO + " oranges � la place de 9 oranges");

            Cube cube = new(strCube);
            return Move.GetStringPath(Cube.LightOptimization(Cube.FastBeginnerMethod(cube)));
        }

        [HttpPost("SolveCube")]
        public ActionResult<string> Solve([FromBody] string strCube = "")
        {
            if (strCube == string.Empty)
            {
                Cube cube = new(500);
                strCube = cube.ToString();
                return Ok(SolveCube(strCube));
            }
            try
            {
                return SolveCube(strCube.ToUpper());
            }
            catch (Exception ex) when (ex is ArgumentException || ex is IndexOutOfRangeException)
            {
                return Ok("Le cube n'est pas r�soluble, v�rifiez l'entr�e");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        /*[HttpPost("Kociemba")]
        public ActionResult<string> Kociemba([FromBody] string strCube = "")
        {
            if (strCube == string.Empty)
            {
                Cube cube = new(500);
                return Ok(Move.GetStringPath(cube.Kociemba()));
            }
            try
            {
                SolveCube(strCube.ToUpper());
                return Move.GetStringPath(new Cube(strCube.ToUpper()).Kociemba());
            }
            catch (Exception ex) when (ex is ArgumentException || ex is IndexOutOfRangeException)
            {
                return Ok("Le cube n'est pas r�soluble, v�rifiez l'entr�e");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }*/

        [HttpPost("ReverseResolution")]
        public ActionResult<string> ReversePath([FromBody] string path)
        {

            try
            {
                return Ok(Move.GetStringPath(Move.GetReversalPath(Move.GetAlgoFromStringEnum(
                            Move.StringPathToEnum(path.Replace(" ", string.Empty).ToUpper()).Reverse()))));
            }
            catch
            {
                return Ok("Erreur");
            }
        }

        [HttpPost("Periodicity")]
        public ActionResult<int> Periodicity([FromBody] string algorithm)
        {
            try
            {
                return Ok(Cube.Periodicity(Move.StringPathToEnum(algorithm.Replace(" ", string.Empty).ToUpper())));
            }
            catch
            {
                return Ok("Erreur");
            }
        }
    }
}