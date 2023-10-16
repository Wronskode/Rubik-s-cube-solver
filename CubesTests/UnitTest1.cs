using Rubik_s_cube_solver;

namespace CubesTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FastMethodSolves()
        {
            Cube c = new(500);
            var res = Cube.FastBeginnerMethod(c);
            c.ExecuterAlgorithme(res);
            Assert.IsTrue(c.IsSolved);
        }

        [TestMethod]
        public void ScrambleWorks()
        {
            Cube c = new();
            var scramble = c.Scramble(500).Select(x => (byte) x);
            c.ExecuterAlgorithme(Cube.GetReversalPath(scramble.Reverse()));
            Console.WriteLine(c.PrintCube());
            Assert.IsTrue(c.IsSolved);
        }

        [TestMethod]
        public void LightReductionWorks()
        {
            Cube c = new(500);
            Cube c1 = c.Clone();
            var res = Cube.FastBeginnerMethod(c);
            var res2 = Cube.LightOptimization(res);
            c.ExecuterAlgorithme(res);
            c1.ExecuterAlgorithme(res2);
            Assert.IsTrue(c.IsSolved && c1.IsSolved && res2.Count() <= res.Count);
        }
    }
}