using Rubik_s_cube_solver;

namespace CubesTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void FastMethodSolves()
        {
            for (int i = 0; i < 10; i++)
            {
                Cube c = new(500);
                Cube c2 = c.Clone();
                List<byte> res = Cube.FastBeginnerMethod(c);
                Assert.IsTrue(c2.ToString().Equals(c.ToString()));
                c.ExecuterAlgorithme(res);
                Assert.IsTrue(c.IsSolved);
            }
        }

        [TestMethod]
        public void ScrambleWorks()
        {
            for (int i = 0; i < 10; i++)
            {
                Cube c = new();
                IEnumerable<byte> scramble = c.Scramble(500);
                c.ExecuterAlgorithme(Move.GetReversalPath(scramble.Reverse()));
                Assert.IsTrue(c.IsSolved);
            }
        }

        [TestMethod]
        public void LightReductionWorks()
        {
            for (int i = 0; i < 1000; i++)
            {
                Cube c = new(500);
                Cube c1 = c.Clone();
                List<byte> res = Cube.FastBeginnerMethod(c);
                List<byte> res2 = Cube.LightOptimization(res);
                c.ExecuterAlgorithme(res);
                c1.ExecuterAlgorithme(res2);
                Assert.IsTrue(c.IsSolved && c1.IsSolved && res2.Count <= res.Count);
            }
        }

        [TestMethod]
        public void LightReductionEdgeCases()
        {
            List<byte> res = [1, 1, 1];
            List<byte> optimized = Cube.LightOptimization(res);
            Assert.AreEqual(1, optimized.Count);

            res = [1, Move.GetReversalMove(1)];
            optimized = Cube.LightOptimization(res);
            Assert.AreEqual(0, optimized.Count);

            res = [1, 1, 2, 2, 3, 3];
            optimized = Cube.LightOptimization(res);
            Assert.AreEqual(3, optimized.Count);

            res = [0, 12];
            optimized = Cube.LightOptimization(res);
            Assert.AreEqual(1, optimized.Count);

            res = [12, 0];
            optimized = Cube.LightOptimization(res);
            Assert.AreEqual(1, optimized.Count);
        }

        [TestMethod]
        public void PeriodicityWorks()
        {
            for (int j = 0; j < 10; j++)
            {
                Cube c = new(500);
                Cube c1 = c.Clone();
                List<byte> res = Cube.FastBeginnerMethod(c);
                int periodicity = Cube.Periodicity(res);
                Cube solvedCube = new();
                for (int i = 0; i < periodicity; i++)
                {
                    solvedCube.ExecuterAlgorithme(res);
                    c.ExecuterAlgorithme(res);
                }
                Assert.IsTrue(solvedCube.IsSolved && c.Equals(c1));
            }
        }

        [TestMethod]
        public void KociembaSolves()
        {
            Cube c = new(500);
            Cube c2 = c.Clone();
            byte[] res = c.Kociemba();
            Assert.IsTrue(c2.ToString().Equals(c.ToString()));
            c.ExecuterAlgorithme(res);
            Assert.IsTrue(c.IsSolved);
        }

        [TestMethod]
        public void HeuristicWorks()
        {
            for (int i = 0; i < 100; i++)
            {
                Cube c = new();
                Assert.IsTrue(c.Conflicts() == 0);
                c.Scramble(1);
                Assert.IsTrue(c.Conflicts() == 8);
            }
        }
    }
}