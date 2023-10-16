using Microsoft.AspNetCore.Mvc;
using Rubik_s_cube_solver;

namespace MySite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiRubiksCube : ControllerBase
    {
        //private readonly ILogger<ApiRubiksCube> _logger;

        //public ApiRubiksCube(ILogger<ApiRubiksCube> logger)
        //{
        //    _logger = logger;
        //}

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    });
        //}

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
            return Cube.GetStringPath(Cube.LightOptimization(Cube.FastBeginnerMethod(cube)));
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
                return Ok("Le cube n'est pas résoluble, vérifiez l'entrée");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost("ReverseResolution")]
        public ActionResult<string> ReversePath([FromBody] string path)
        {

            try
            {
                return Ok(Cube.GetStringPath(Cube.GetReversalPath(Cube.GetAlgoFromStringEnum(
                            Cube.StringPathToEnum(path.Replace(" ", string.Empty).ToUpper()).Reverse()))));
            }
            catch
            {
                return Ok("Erreur");
            }
        }

        [HttpPost("OptimizeAlgo")]
        public ActionResult<string> Optimize([FromBody] string path)
        {
            try
            {
                return Ok(Cube.GetStringPath(Cube.OptimizePath(Cube.GetAlgoFromStringEnum(Cube.StringPathToEnum(path.Replace(" ", string.Empty).ToUpper())))));
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
                return Ok(Cube.Periodicity(Cube.StringPathToEnum(algorithm.Replace(" ", string.Empty).ToUpper())));
            }
            catch
            {
                return Ok("Erreur");
            }
        }
    }
}