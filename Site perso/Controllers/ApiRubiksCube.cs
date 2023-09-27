using Microsoft.AspNetCore.Mvc;
using Rubik_s_cube_console;
using Rubik_s_cube_solver;
using System.Net;

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

        [HttpPost("SolveCube")]
        public ActionResult<string> Solve([FromBody] string strCube = "")
        {
            if (strCube == string.Empty)
            {
                Cube cube = new(500);
                strCube = cube.ToString();
                return Ok(GererCube.SolveCube(strCube));
            }
            try
            {
                return GererCube.SolveCube(strCube.ToUpper());
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