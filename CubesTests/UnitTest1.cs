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
            Cube c2 = c.Clone();
            var res = Cube.FastBeginnerMethod(c);
            Assert.IsTrue(c2.ToString().Equals(c.ToString()));
            c.ExecuterAlgorithme(res);
            Assert.IsTrue(c.IsSolved);
        }

        [TestMethod]
        public void ScrambleWorks()
        {
            Cube c = new();
            IEnumerable<byte> scramble = c.Scramble(500);
            c.ExecuterAlgorithme(Cube.GetReversalPath(scramble.Reverse()));
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

        [TestMethod]
        public void OptimizePathWorks()
        {
            Cube c = new(500);
            Cube c1 = c.Clone();
            var res = Cube.FastBeginnerMethod(c);
            var res2 = Cube.OptimizePath(res);
            c.ExecuterAlgorithme(res);
            c1.ExecuterAlgorithme(res2);
            Assert.IsTrue(c.IsSolved && c1.IsSolved && res2.Length <= res.Count);
        }

        [TestMethod]
        public void PeriodicityWorks()
        {
            Cube c = new(500);
            Cube c1 = c.Clone();
            var res = Cube.FastBeginnerMethod(c);
            var periodicity = Cube.Periodicity(res);
            Cube solvedCube = new();
            for (int i = 0; i < periodicity; i++)
            {
                solvedCube.ExecuterAlgorithme(res);
                c.ExecuterAlgorithme(res);
            }
            Assert.IsTrue(solvedCube.IsSolved && c.Equals(c1));
        }
    }
}