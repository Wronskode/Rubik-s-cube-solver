using System.Diagnostics;
using System.Text;

namespace Rubik_s_cube_solver
{
    public class Cube : IEquatable<Cube?>
    {
        private readonly Face WhiteFace;
        private readonly Face YellowFace;
        private readonly Face RedFace;
        private readonly Face GreenFace;
        private readonly Face BlueFace;
        private readonly Face OrangeFace;

        private static readonly List<byte> secondLayerLeftRedFace = Move.GetAlgoFromStringEnum(["D'", "B'", "D", "B", "D", "R", "D'", "R'"]);
        private static readonly List<byte> secondLayerRightRedFace = Move.GetAlgoFromStringEnum(["D", "F", "D'", "F'", "D'", "R'", "D", "R"]);

        private static readonly List<byte> secondLayerLeftBlueFace = Move.GetAlgoFromStringEnum(["D'", "L'", "D", "L", "D", "B", "D'", "B'"]);
        private static readonly List<byte> secondLayerRightBlueFace = Move.GetAlgoFromStringEnum(["D", "R", "D", "R'", "D'", "B'", "D'", "B"]);

        private static readonly List<byte> secondLayerLeftOrangeFace = Move.GetAlgoFromStringEnum(["D'", "F'", "D", "F", "D", "L", "D'", "L'"]);
        private static readonly List<byte> secondLayerRightOrangeFace = Move.GetAlgoFromStringEnum(["D", "B", "D'", "B'", "D'", "L'", "D", "L"]);

        private static readonly List<byte> secondLayerLeftGreenFace = Move.GetAlgoFromStringEnum(["D'", "R'", "D", "R", "D", "F", "D'", "F'"]);
        private static readonly List<byte> secondLayerRightGreenFace = Move.GetAlgoFromStringEnum(["D", "L", "D'", "L'", "D'", "F'", "D", "F"]);

        private static readonly List<byte> algoCrossPattern = Move.GetAlgoFromStringEnum(["R", "D", "F", "D'", "F'", "R'"]);
        private static readonly List<byte> algoCrossPattern2 = Move.GetAlgoFromStringEnum(["R", "F", "D", "F'", "D'", "R'"]);

        private static readonly List<byte> redF = Move.GetAlgoFromStringEnum(["D2", "F", "D", "F'", "D", "F", "D2", "F'", "D'"]);
        private static readonly List<byte> blueF = Move.GetAlgoFromStringEnum(["D", "F", "D", "F'", "D", "F", "D2", "F'"]);
        private static readonly List<byte> greenF = Move.GetAlgoFromStringEnum(["F", "D", "F'", "D", "F", "D2", "F'", "D"]);
        private static readonly List<byte> greenF2 = Move.GetAlgoFromStringEnum(["D'", "F", "D", "F'", "D", "F", "D2", "F'", "D2"]);
        private static readonly List<byte> redF2 = Move.GetAlgoFromStringEnum(["F", "D", "F'", "D", "F", "D2", "F'"]);
        private static readonly List<byte> blueF2 = Move.GetAlgoFromStringEnum(["D", "F", "D", "F'", "D", "F", "D2", "F'", "D'"]);

        private static readonly List<byte> cornerAlignementAlgo = Move.GetAlgoFromStringEnum(["D'", "B'", "D", "F", "D'", "B", "D", "F'"]);
        private static readonly List<byte> cornerAlignementAlgo2 = Move.GetAlgoFromStringEnum(["B'", "D", "F", "D'", "B", "D", "F'", "D'"]);
        private static readonly List<byte> cornerAlignementAlgo3 = Move.GetAlgoFromStringEnum(["D2", "B'", "D", "F", "D'", "B", "D", "F'", "D"]);
        private static readonly List<byte> cornerAlignementAlgo4 = Move.GetAlgoFromStringEnum(["D", "B'", "D", "F", "D'", "B", "D", "F'", "D2"]);

        private static readonly List<byte> cornerAlignementOptim = Move.GetAlgoFromStringEnum(["F", "D'", "B'", "D", "F'", "D'", "B", "D"]);
        private static readonly List<byte> cornerAlignementOptim2 = Move.GetAlgoFromStringEnum(["L", "D'", "R'", "D", "L'", "D'", "R", "D"]);
        private static readonly List<byte> cornerAlignementOptim3 = Move.GetAlgoFromStringEnum(["R", "D'", "L'", "D", "R'", "D'", "L", "D"]);
        private static readonly List<byte> cornerAlignementOptim4 = Move.GetAlgoFromStringEnum(["B", "D'", "F'", "D", "B'", "D'", "F", "D"]);

        private static readonly List<byte> sexyMove = Move.GetAlgoFromStringEnum(["R", "U", "R'", "U'"]);
        private static readonly List<byte> inverseSexyMove = Move.GetAlgoFromStringEnum(["F", "D", "F'", "D'"]);
        private static readonly IEnumerable<byte> doubleInverseSexyMove = inverseSexyMove.Concat(inverseSexyMove);
        private static readonly IEnumerable<byte> quadInverseSexyMove = doubleInverseSexyMove.Concat(doubleInverseSexyMove);
        private static readonly IEnumerable<byte> doubleInverseByteAlgo = inverseSexyMove.Concat(inverseSexyMove);
        private static readonly IEnumerable<byte> quadInverseByteAlgo = doubleInverseByteAlgo.Concat(doubleInverseByteAlgo);
        private static readonly List<byte> uPrimeAlgo = Move.GetAlgoFromStringEnum(["U'"]);

        private static readonly IEnumerable<byte> doubleSM = sexyMove.Concat(sexyMove);
        private static readonly IEnumerable<byte> quadSM = doubleSM.Concat(doubleSM);
        private static readonly List<byte> byteAlgo = sexyMove;
        private static readonly IEnumerable<byte> doubleByteAlgo = byteAlgo.Concat(byteAlgo);
        private static readonly IEnumerable<byte> quadByteAlgo = doubleByteAlgo.Concat(doubleByteAlgo);
        private static readonly List<byte> dPrimeAlgo = Move.GetAlgoFromStringEnum(["D'"]);
        private static readonly List<byte> dAlgo = Move.GetAlgoFromStringEnum(["D"]);
        public bool IsSolved
        {
            get
            {
                return WhiteFace.IsUniform && YellowFace.IsUniform && RedFace.IsUniform && GreenFace.IsUniform &&
                        BlueFace.IsUniform && OrangeFace.IsUniform;
            }
        }
        public Cube(List<Face> faces)
        {
            foreach (Face face in faces)
            {
                switch (face.ColorFace)
                {
                    case 'W':
                        WhiteFace = face;
                        break;
                    case 'Y':
                        YellowFace = face;
                        break;
                    case 'R':
                        RedFace = face;
                        break;
                    case 'G':
                        GreenFace = face;
                        break;
                    case 'B':
                        BlueFace = face;
                        break;
                    case 'O':
                        OrangeFace = face;
                        break;
                    default:
                        throw new Exception("La couleur " + face.ColorFace + " n'existe pas");
                }
            }
            Debug.Assert(WhiteFace != null);
            Debug.Assert(YellowFace != null);
            Debug.Assert(RedFace != null);
            Debug.Assert(GreenFace != null);
            Debug.Assert(BlueFace != null);
            Debug.Assert(OrangeFace != null);
        }

        public Cube()
        {
            WhiteFace = new Face('W');
            YellowFace = new Face('Y');
            RedFace = new Face('R');
            GreenFace = new Face('G');
            BlueFace = new Face('B');
            OrangeFace = new Face('O');
        }

        public Cube(string str)
        {
            //if (str.Length != 54) throw new Exception("Pas le bon nombre de couleurs");
            int idx = 0;
            List<Face> newFaces = new(6);
            for (int i = 0; i < 6; i++)
            {
                char[,] array = new char[3, 3];
                for (int p = 0; p < 9; p++)
                {
                    array[p / 3, p % 3] = str[idx++];
                }
                Face f = new(array);
                newFaces.Add(f);
            }
            foreach (Face face in newFaces)
            {
                switch (face.ColorFace)
                {
                    case 'W':
                        WhiteFace = face;
                        break;
                    case 'Y':
                        YellowFace = face;
                        break;
                    case 'R':
                        RedFace = face;
                        break;
                    case 'G':
                        GreenFace = face;
                        break;
                    case 'B':
                        BlueFace = face;
                        break;
                    case 'O':
                        OrangeFace = face;
                        break;
                    default:
                        throw new Exception("La couleur " + face.ColorFace + " n'existe pas");
                }
            }
            Debug.Assert(WhiteFace != null);
            Debug.Assert(YellowFace != null);
            Debug.Assert(RedFace != null);
            Debug.Assert(GreenFace != null);
            Debug.Assert(BlueFace != null);
            Debug.Assert(OrangeFace != null);
        }

        public Cube(int n)
        {
            WhiteFace = new Face('W');
            YellowFace = new Face('Y');
            RedFace = new Face('R');
            GreenFace = new Face('G');
            BlueFace = new Face('B');
            OrangeFace = new Face('O');
            this.Shuffle(n);
        }

        public char[,] CopyFace(char color)
        {
            return color switch
            {
                'W' => (char[,])WhiteFace.Pieces.Clone(),
                'Y' => (char[,])YellowFace.Pieces.Clone(),
                'R' => (char[,])RedFace.Pieces.Clone(),
                'G' => (char[,])GreenFace.Pieces.Clone(),
                'B' => (char[,])BlueFace.Pieces.Clone(),
                'O' => (char[,])OrangeFace.Pieces.Clone(),
                _ => throw new Exception("La couleur " + color + " n'existe pas")
            };
        }
        public void U()
        {
            char[] newRed = [BlueFace.Pieces[0, 0], BlueFace.Pieces[0, 1], BlueFace.Pieces[0, 2]];
            char[] newGreen = [RedFace.Pieces[0, 0], RedFace.Pieces[0, 1], RedFace.Pieces[0, 2]];
            char[] newBlue = [OrangeFace.Pieces[0, 0], OrangeFace.Pieces[0, 1], OrangeFace.Pieces[0, 2]];
            char[] newOrange = [GreenFace.Pieces[0, 0], GreenFace.Pieces[0, 1], GreenFace.Pieces[0, 2]];
            for (int i = 0; i < 3; i++)
            {
                RedFace.Pieces[0, i] = newRed[i];
                GreenFace.Pieces[0, i] = newGreen[i];
                BlueFace.Pieces[0, i] = newBlue[i];
                OrangeFace.Pieces[0, i] = newOrange[i];
            }
            WhiteFace.Rotate90Right();
        }

        public void Uprime()
        {
            char[] newRed = [GreenFace.Pieces[0, 0], GreenFace.Pieces[0, 1], GreenFace.Pieces[0, 2]];
            char[] newGreen = [OrangeFace.Pieces[0, 0], OrangeFace.Pieces[0, 1], OrangeFace.Pieces[0, 2]];
            char[] newBlue = [RedFace.Pieces[0, 0], RedFace.Pieces[0, 1], RedFace.Pieces[0, 2]];
            char[] newOrange = [BlueFace.Pieces[0, 0], BlueFace.Pieces[0, 1], BlueFace.Pieces[0, 2]];
            for (int i = 0; i < 3; i++)
            {
                RedFace.Pieces[0, i] = newRed[i];
                GreenFace.Pieces[0, i] = newGreen[i];
                BlueFace.Pieces[0, i] = newBlue[i];
                OrangeFace.Pieces[0, i] = newOrange[i];
            }
            WhiteFace.Rotate90Left();
        }

        public void R()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newBlueFace = CopyFace('B');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newGreenFace = CopyFace('G');

            newWhiteFace[2, 0] = GreenFace.Pieces[2, 2];
            newWhiteFace[2, 1] = GreenFace.Pieces[1, 2];
            newWhiteFace[2, 2] = GreenFace.Pieces[0, 2];

            newBlueFace[0, 0] = WhiteFace.Pieces[2, 0];
            newBlueFace[1, 0] = WhiteFace.Pieces[2, 1];
            newBlueFace[2, 0] = WhiteFace.Pieces[2, 2];

            newYellowFace[0, 0] = BlueFace.Pieces[2, 0];
            newYellowFace[0, 1] = BlueFace.Pieces[1, 0];
            newYellowFace[0, 2] = BlueFace.Pieces[0, 0];

            newGreenFace[0, 2] = YellowFace.Pieces[0, 0];
            newGreenFace[1, 2] = YellowFace.Pieces[0, 1];
            newGreenFace[2, 2] = YellowFace.Pieces[0, 2];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
            GreenFace.Pieces = newGreenFace;
            RedFace.Rotate90Right();
        }

        public void Rprime()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newBlueFace = CopyFace('B');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newGreenFace = CopyFace('G');

            newWhiteFace[2, 0] = BlueFace.Pieces[0, 0];
            newWhiteFace[2, 1] = BlueFace.Pieces[1, 0];
            newWhiteFace[2, 2] = BlueFace.Pieces[2, 0];

            newBlueFace[0, 0] = YellowFace.Pieces[0, 2];
            newBlueFace[1, 0] = YellowFace.Pieces[0, 1];
            newBlueFace[2, 0] = YellowFace.Pieces[0, 0];

            newYellowFace[0, 0] = GreenFace.Pieces[0, 2];
            newYellowFace[0, 1] = GreenFace.Pieces[1, 2];
            newYellowFace[0, 2] = GreenFace.Pieces[2, 2];

            newGreenFace[0, 2] = WhiteFace.Pieces[2, 2];
            newGreenFace[1, 2] = WhiteFace.Pieces[2, 1];
            newGreenFace[2, 2] = WhiteFace.Pieces[2, 0];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
            GreenFace.Pieces = newGreenFace;
            RedFace.Rotate90Left();

        }

        public void D()
        {
            char[] newRed = [GreenFace.Pieces[2, 0], GreenFace.Pieces[2, 1], GreenFace.Pieces[2, 2]];
            char[] newGreen = [OrangeFace.Pieces[2, 0], OrangeFace.Pieces[2, 1], OrangeFace.Pieces[2, 2]];
            char[] newBlue = [RedFace.Pieces[2, 0], RedFace.Pieces[2, 1], RedFace.Pieces[2, 2]];
            char[] newOrange = [BlueFace.Pieces[2, 0], BlueFace.Pieces[2, 1], BlueFace.Pieces[2, 2]];
            for (int i = 0; i < 3; i++)
            {
                RedFace.Pieces[2, i] = newRed[i];
                GreenFace.Pieces[2, i] = newGreen[i];
                BlueFace.Pieces[2, i] = newBlue[i];
                OrangeFace.Pieces[2, i] = newOrange[i];
            }
            YellowFace.Rotate90Right();
        }

        public void Dprime()
        {
            char[] newRed = [BlueFace.Pieces[2, 0], BlueFace.Pieces[2, 1], BlueFace.Pieces[2, 2]];
            char[] newGreen = [RedFace.Pieces[2, 0], RedFace.Pieces[2, 1], RedFace.Pieces[2, 2]];
            char[] newBlue = [OrangeFace.Pieces[2, 0], OrangeFace.Pieces[2, 1], OrangeFace.Pieces[2, 2]];
            char[] newOrange = [GreenFace.Pieces[2, 0], GreenFace.Pieces[2, 1], GreenFace.Pieces[2, 2]];
            for (int i = 0; i < 3; i++)
            {
                RedFace.Pieces[2, i] = newRed[i];
                GreenFace.Pieces[2, i] = newGreen[i];
                BlueFace.Pieces[2, i] = newBlue[i];
                OrangeFace.Pieces[2, i] = newOrange[i];
            }
            YellowFace.Rotate90Left();
        }


        public void L()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newBlueFace = CopyFace('B');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newGreenFace = CopyFace('G');

            newWhiteFace[0, 0] = BlueFace.Pieces[0, 2];
            newWhiteFace[0, 1] = BlueFace.Pieces[1, 2];
            newWhiteFace[0, 2] = BlueFace.Pieces[2, 2];

            newBlueFace[0, 2] = YellowFace.Pieces[2, 2];
            newBlueFace[1, 2] = YellowFace.Pieces[2, 1];
            newBlueFace[2, 2] = YellowFace.Pieces[2, 0];

            newYellowFace[2, 0] = GreenFace.Pieces[0, 0];
            newYellowFace[2, 1] = GreenFace.Pieces[1, 0];
            newYellowFace[2, 2] = GreenFace.Pieces[2, 0];

            newGreenFace[0, 0] = WhiteFace.Pieces[0, 2];
            newGreenFace[1, 0] = WhiteFace.Pieces[0, 1];
            newGreenFace[2, 0] = WhiteFace.Pieces[0, 0];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
            GreenFace.Pieces = newGreenFace;
            OrangeFace.Rotate90Right();
        }

        public void Lprime()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newBlueFace = CopyFace('B');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newGreenFace = CopyFace('G');

            newWhiteFace[0, 0] = GreenFace.Pieces[2, 0];
            newWhiteFace[0, 1] = GreenFace.Pieces[1, 0];
            newWhiteFace[0, 2] = GreenFace.Pieces[0, 0];

            newBlueFace[0, 2] = WhiteFace.Pieces[0, 0];
            newBlueFace[1, 2] = WhiteFace.Pieces[0, 1];
            newBlueFace[2, 2] = WhiteFace.Pieces[0, 2];

            newYellowFace[2, 0] = BlueFace.Pieces[2, 2];
            newYellowFace[2, 1] = BlueFace.Pieces[1, 2];
            newYellowFace[2, 2] = BlueFace.Pieces[0, 2];

            newGreenFace[0, 0] = YellowFace.Pieces[2, 0];
            newGreenFace[1, 0] = YellowFace.Pieces[2, 1];
            newGreenFace[2, 0] = YellowFace.Pieces[2, 2];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
            GreenFace.Pieces = newGreenFace;
            OrangeFace.Rotate90Left();
        }

        public void F()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newRedFace = CopyFace('R');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newOrangeFace = CopyFace('O');

            newWhiteFace[0, 0] = OrangeFace.Pieces[2, 2];
            newWhiteFace[1, 0] = OrangeFace.Pieces[1, 2];
            newWhiteFace[2, 0] = OrangeFace.Pieces[0, 2];

            newOrangeFace[0, 2] = YellowFace.Pieces[2, 0];
            newOrangeFace[1, 2] = YellowFace.Pieces[1, 0];
            newOrangeFace[2, 2] = YellowFace.Pieces[0, 0];

            newYellowFace[0, 0] = RedFace.Pieces[0, 0];
            newYellowFace[1, 0] = RedFace.Pieces[1, 0];
            newYellowFace[2, 0] = RedFace.Pieces[2, 0];

            newRedFace[0, 0] = WhiteFace.Pieces[0, 0];
            newRedFace[1, 0] = WhiteFace.Pieces[1, 0];
            newRedFace[2, 0] = WhiteFace.Pieces[2, 0];

            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            RedFace.Pieces = newRedFace;
            GreenFace.Rotate90Right();
        }

        public void Fprime()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newRedFace = CopyFace('R');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newOrangeFace = CopyFace('O');

            newWhiteFace[0, 0] = RedFace.Pieces[0, 0];
            newWhiteFace[1, 0] = RedFace.Pieces[1, 0];
            newWhiteFace[2, 0] = RedFace.Pieces[2, 0];

            newOrangeFace[0, 2] = WhiteFace.Pieces[2, 0];
            newOrangeFace[1, 2] = WhiteFace.Pieces[1, 0];
            newOrangeFace[2, 2] = WhiteFace.Pieces[0, 0];

            newYellowFace[0, 0] = OrangeFace.Pieces[2, 2];
            newYellowFace[1, 0] = OrangeFace.Pieces[1, 2];
            newYellowFace[2, 0] = OrangeFace.Pieces[0, 2];

            newRedFace[0, 0] = YellowFace.Pieces[0, 0];
            newRedFace[1, 0] = YellowFace.Pieces[1, 0];
            newRedFace[2, 0] = YellowFace.Pieces[2, 0];


            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            RedFace.Pieces = newRedFace;
            GreenFace.Rotate90Left();
        }

        public void B()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newRedFace = CopyFace('R');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newOrangeFace = CopyFace('O');

            newWhiteFace[0, 2] = RedFace.Pieces[0, 2];
            newWhiteFace[1, 2] = RedFace.Pieces[1, 2];
            newWhiteFace[2, 2] = RedFace.Pieces[2, 2];

            newOrangeFace[0, 0] = WhiteFace.Pieces[2, 2];
            newOrangeFace[1, 0] = WhiteFace.Pieces[1, 2];
            newOrangeFace[2, 0] = WhiteFace.Pieces[0, 2];

            newYellowFace[0, 2] = OrangeFace.Pieces[2, 0];
            newYellowFace[1, 2] = OrangeFace.Pieces[1, 0];
            newYellowFace[2, 2] = OrangeFace.Pieces[0, 0];

            newRedFace[0, 2] = YellowFace.Pieces[0, 2];
            newRedFace[1, 2] = YellowFace.Pieces[1, 2];
            newRedFace[2, 2] = YellowFace.Pieces[2, 2];


            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            RedFace.Pieces = newRedFace;
            BlueFace.Rotate90Right();
        }

        public void Bprime()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newRedFace = CopyFace('R');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newOrangeFace = CopyFace('O');

            newWhiteFace[0, 2] = OrangeFace.Pieces[2, 0];
            newWhiteFace[1, 2] = OrangeFace.Pieces[1, 0];
            newWhiteFace[2, 2] = OrangeFace.Pieces[0, 0];

            newOrangeFace[0, 0] = YellowFace.Pieces[2, 2];
            newOrangeFace[1, 0] = YellowFace.Pieces[1, 2];
            newOrangeFace[2, 0] = YellowFace.Pieces[0, 2];

            newYellowFace[0, 2] = RedFace.Pieces[0, 2];
            newYellowFace[1, 2] = RedFace.Pieces[1, 2];
            newYellowFace[2, 2] = RedFace.Pieces[2, 2];

            newRedFace[0, 2] = WhiteFace.Pieces[0, 2];
            newRedFace[1, 2] = WhiteFace.Pieces[1, 2];
            newRedFace[2, 2] = WhiteFace.Pieces[2, 2];


            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            RedFace.Pieces = newRedFace;
            BlueFace.Rotate90Left();
        }

        public void F2()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newRedFace = CopyFace('R');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newOrangeFace = CopyFace('O');

            newWhiteFace[0, 0] = YellowFace.Pieces[0, 0];
            newWhiteFace[1, 0] = YellowFace.Pieces[1, 0];
            newWhiteFace[2, 0] = YellowFace.Pieces[2, 0];

            newOrangeFace[0, 2] = RedFace.Pieces[2, 0];
            newOrangeFace[1, 2] = RedFace.Pieces[1, 0];
            newOrangeFace[2, 2] = RedFace.Pieces[0, 0];

            newYellowFace[0, 0] = WhiteFace.Pieces[0, 0];
            newYellowFace[1, 0] = WhiteFace.Pieces[1, 0];
            newYellowFace[2, 0] = WhiteFace.Pieces[2, 0];

            newRedFace[0, 0] = OrangeFace.Pieces[2, 2];
            newRedFace[1, 0] = OrangeFace.Pieces[1, 2];
            newRedFace[2, 0] = OrangeFace.Pieces[0, 2];

            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            RedFace.Pieces = newRedFace;
            GreenFace.Rotate180();
        }

        public void U2()
        {
            char[] newRed = [OrangeFace.Pieces[0, 0], OrangeFace.Pieces[0, 1], OrangeFace.Pieces[0, 2]];
            char[] newGreen = [BlueFace.Pieces[0, 0], BlueFace.Pieces[0, 1], BlueFace.Pieces[0, 2]];
            char[] newBlue = [GreenFace.Pieces[0, 0], GreenFace.Pieces[0, 1], GreenFace.Pieces[0, 2]];
            char[] newOrange = [RedFace.Pieces[0, 0], RedFace.Pieces[0, 1], RedFace.Pieces[0, 2]];
            for (int i = 0; i < 3; i++)
            {
                RedFace.Pieces[0, i] = newRed[i];
                GreenFace.Pieces[0, i] = newGreen[i];
                BlueFace.Pieces[0, i] = newBlue[i];
                OrangeFace.Pieces[0, i] = newOrange[i];
            }
            WhiteFace.Rotate180();
        }
        public void B2()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newRedFace = CopyFace('R');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newOrangeFace = CopyFace('O');

            newWhiteFace[0, 2] = YellowFace.Pieces[0, 2];
            newWhiteFace[1, 2] = YellowFace.Pieces[1, 2];
            newWhiteFace[2, 2] = YellowFace.Pieces[2, 2];

            newOrangeFace[0, 0] = RedFace.Pieces[2, 2];
            newOrangeFace[1, 0] = RedFace.Pieces[1, 2];
            newOrangeFace[2, 0] = RedFace.Pieces[0, 2];

            newYellowFace[0, 2] = WhiteFace.Pieces[0, 2];
            newYellowFace[1, 2] = WhiteFace.Pieces[1, 2];
            newYellowFace[2, 2] = WhiteFace.Pieces[2, 2];

            newRedFace[0, 2] = OrangeFace.Pieces[2, 0];
            newRedFace[1, 2] = OrangeFace.Pieces[1, 0];
            newRedFace[2, 2] = OrangeFace.Pieces[0, 0];


            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            RedFace.Pieces = newRedFace;
            BlueFace.Rotate180();
        }
        public void L2()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newBlueFace = CopyFace('B');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newGreenFace = CopyFace('G');

            newWhiteFace[0, 0] = YellowFace.Pieces[2, 2];
            newWhiteFace[0, 1] = YellowFace.Pieces[2, 1];
            newWhiteFace[0, 2] = YellowFace.Pieces[2, 0];

            newBlueFace[0, 2] = GreenFace.Pieces[2, 0];
            newBlueFace[1, 2] = GreenFace.Pieces[1, 0];
            newBlueFace[2, 2] = GreenFace.Pieces[0, 0];

            newYellowFace[2, 0] = WhiteFace.Pieces[0, 2];
            newYellowFace[2, 1] = WhiteFace.Pieces[0, 1];
            newYellowFace[2, 2] = WhiteFace.Pieces[0, 0];

            newGreenFace[0, 0] = BlueFace.Pieces[2, 2];
            newGreenFace[1, 0] = BlueFace.Pieces[1, 2];
            newGreenFace[2, 0] = BlueFace.Pieces[0, 2];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
            GreenFace.Pieces = newGreenFace;
            OrangeFace.Rotate180();
        }
        public void D2()
        {
            char[] newRed = [OrangeFace.Pieces[2, 0], OrangeFace.Pieces[2, 1], OrangeFace.Pieces[2, 2]];
            char[] newGreen = [BlueFace.Pieces[2, 0], BlueFace.Pieces[2, 1], BlueFace.Pieces[2, 2]];
            char[] newBlue = [GreenFace.Pieces[2, 0], GreenFace.Pieces[2, 1], GreenFace.Pieces[2, 2]];
            char[] newOrange = [RedFace.Pieces[2, 0], RedFace.Pieces[2, 1], RedFace.Pieces[2, 2]];
            for (int i = 0; i < 3; i++)
            {
                RedFace.Pieces[2, i] = newRed[i];
                GreenFace.Pieces[2, i] = newGreen[i];
                BlueFace.Pieces[2, i] = newBlue[i];
                OrangeFace.Pieces[2, i] = newOrange[i];
            }
            YellowFace.Rotate180();
        }
        public void R2()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newBlueFace = CopyFace('B');

            char[,] newYellowFace = CopyFace('Y');

            char[,] newGreenFace = CopyFace('G');

            newWhiteFace[2, 0] = YellowFace.Pieces[0, 2];
            newWhiteFace[2, 1] = YellowFace.Pieces[0, 1];
            newWhiteFace[2, 2] = YellowFace.Pieces[0, 0];

            newBlueFace[0, 0] = GreenFace.Pieces[2, 2];
            newBlueFace[1, 0] = GreenFace.Pieces[1, 2];
            newBlueFace[2, 0] = GreenFace.Pieces[0, 2];

            newYellowFace[0, 0] = WhiteFace.Pieces[2, 2];
            newYellowFace[0, 1] = WhiteFace.Pieces[2, 1];
            newYellowFace[0, 2] = WhiteFace.Pieces[2, 0];

            newGreenFace[0, 2] = BlueFace.Pieces[2, 0];
            newGreenFace[1, 2] = BlueFace.Pieces[1, 0];
            newGreenFace[2, 2] = BlueFace.Pieces[0, 0];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
            GreenFace.Pieces = newGreenFace;
            RedFace.Rotate180();
        }

        public override string ToString()
        {
            StringBuilder sb = new(54);
            sb.Append(WhiteFace);
            sb.Append(YellowFace);
            sb.Append(RedFace);
            sb.Append(GreenFace);
            sb.Append(BlueFace);
            sb.Append(OrangeFace);
            return sb.ToString();
        }

        public Cube Clone()
        {
            return new Cube(ToString());
            //return new Cube(new List<Face>(6) { WhiteFace.Clone(), YellowFace.Clone(), RedFace.Clone(), GreenFace.Clone(), BlueFace.Clone(), OrangeFace.Clone() });
        }

        public void Shuffle(int n = 20, int? seed = null)
        {
            Random rnd;
            if (seed == null)
                rnd = new(); // 2023
            else
                rnd = new(seed.GetValueOrDefault());
            for (int i = 0; i < n; i++)
            {
                int randInt = rnd.Next(1, 19);
                switch (randInt)
                {
                    case 1:
                        F();
                        break;
                    case 2:
                        U();
                        break;
                    case 3:
                        B();
                        break;
                    case 4:
                        L();
                        break;
                    case 5:
                        D();
                        break;
                    case 6:
                        R();
                        break;
                    case 7:
                        Fprime();
                        break;
                    case 8:
                        Uprime();
                        break;
                    case 9:
                        Bprime();
                        break;
                    case 10:
                        Lprime();
                        break;
                    case 11:
                        Dprime();
                        break;
                    case 12:
                        Rprime();
                        break;
                    case 13:
                        F2();
                        break;
                    case 14:
                        U2();
                        break;
                    case 15:
                        B2();
                        break;
                    case 16:
                        L2();
                        break;
                    case 17:
                        D2();
                        break;
                    case 18:
                        R2();
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }
        }
        public IEnumerable<byte> Scramble(int n = 20, int? seed = null)
        {
            Random rnd;
            List<byte> randPath = new(n);
            if (seed == null)
                rnd = new();
            else
                rnd = new(seed.GetValueOrDefault());
            for (int i = 0; i < n; i++)
            {
                byte randInt = (byte)rnd.Next(0, 18);
                randPath.Add(randInt);
                switch (randInt)
                {
                    case 0:
                        F();
                        break;
                    case 1:
                        U();
                        break;
                    case 2:
                        B();
                        break;
                    case 3:
                        L();
                        break;
                    case 4:
                        D();
                        break;
                    case 5:
                        R();
                        break;
                    case 6:
                        Fprime();
                        break;
                    case 7:
                        Uprime();
                        break;
                    case 8:
                        Bprime();
                        break;
                    case 9:
                        Lprime();
                        break;
                    case 10:
                        Dprime();
                        break;
                    case 11:
                        Rprime();
                        break;
                    case 12:
                        F2();
                        break;
                    case 13:
                        U2();
                        break;
                    case 14:
                        B2();
                        break;
                    case 15:
                        L2();
                        break;
                    case 16:
                        D2();
                        break;
                    case 17:
                        R2();
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            }
            return randPath;
        }

        // Storing only string of cube
        public static (Cube, byte[])? NextTreeBranch(List<Dictionary<string, byte[]>> listDico, Func<Cube, bool> f)
        {
            Dictionary<string, byte[]> newCubes = [];
            foreach (KeyValuePair<string, byte[]> cube in listDico[^1])
            {
                Cube c1 = new(cube.Key);
                for (byte j = 0; j < 18; j++)
                {
                    c1.DoMove(j);
                    string str = c1.ToString();
                    bool isContained = false;
                    foreach (Dictionary<string, byte[]> item in listDico)
                    {
                        if (item.ContainsKey(str))
                        {
                            isContained = true;
                            break;
                        }
                    }
                    if (!isContained)
                        newCubes.TryAdd(str, [.. cube.Value, j]);
                    if (f(c1))
                        return (c1, newCubes[str]);
                    if (j != 17)
                        c1.DoMove(Move.GetReversalMove(j));
                }
            }
            listDico.Add(newCubes);
            return null;
        }

        public static void NextTreeBranchForMITM(List<Dictionary<(ulong, ulong), byte>> listDico, bool heuristique)
        {
            byte methodsCount = 18; // Nombre de mouvements possibles
            Dictionary<(ulong, ulong), byte> newCubes = [];
            foreach (KeyValuePair<(ulong, ulong), byte> cube in listDico[^1])
            {
                Cube c1 = new(DecompressState(cube.Key));
                for (byte j = 0; j < methodsCount; j++)
                {
                    //Cube c = c1.Clone();
                    c1.DoMove(j);
                    (ulong, ulong) intCube = CompressState(c1.ToString());
                    bool isContained = false;
                    foreach (Dictionary<(ulong, ulong), byte> item in listDico)
                    {
                        if (item.ContainsKey(intCube))
                        {
                            isContained = true;
                            break;
                        }
                    }
                    if (!isContained)
                        newCubes.TryAdd(intCube, j);
                    if (j != 17)
                        c1.DoMove(Move.GetReversalMove(j));
                }
            }
            if (heuristique && listDico.Count >= 6)
            {
                HashSet<(ulong, ulong)> sortedCubes = newCubes.Select(x => x.Key).OrderBy(x => Conflits(new Cube(DecompressState(x)))).Take(1000000).ToHashSet();
                listDico.Add(newCubes.Where(x => sortedCubes.Contains(x.Key)).ToDictionary());
            }
            else
            {
                listDico.Add(newCubes);
            }
        }

        public static void NextTreeBranchForMITM(List<Dictionary<string, (byte, byte)>> listDico, bool heuristique)
        {
            byte methodsCount = 18; // Nombre de mouvements possibles
            Dictionary<string, (byte, byte)> newCubes = [];
            foreach (KeyValuePair<string, (byte, byte)> cube in listDico[^1])
            {
                Cube c1 = new(cube.Key);
                for (byte j = 0; j < methodsCount; j++)
                {
                    //Cube c = c1.Clone();
                    c1.DoMove(j);
                    string c1String = c1.ToString();
                    bool isContained = false;
                    foreach (Dictionary<string, (byte, byte)> item in listDico)
                    {
                        if (item.ContainsKey(c1String))
                        {
                            isContained = true;
                            break;
                        }
                    }
                    if (!isContained)
                        newCubes.TryAdd(c1String, (j, Conflits(c1)));
                    if (j != 17)
                        c1.DoMove(Move.GetReversalMove(j));
                }
            }
            if (heuristique && listDico.Count >= 5)
            {
                Dictionary<string, (byte, byte)> sortedCubes = newCubes.OrderBy(x => x.Value.Item2).Take(100000).ToDictionary();
                listDico.Add(sortedCubes);
            }
            else
            {
                listDico.Add(newCubes);
            }
        }

        public static byte[] BFSWithMITM(Cube initialCube)
        {
            List<byte> path = [];
            Cube copy = initialCube.Clone();
            Dictionary<string, (byte, byte)> dico = new()
            {
                { copy.ToString(), (255, Conflits(copy))}
            };
            List<Dictionary<string, (byte, byte)>> arbre = [dico];
            byte minEval = byte.MaxValue;
            while (true)
            {
                List<Task<(byte[], bool)>> tasks = [];
                foreach (KeyValuePair<string, (byte, byte)> element in arbre[^1])
                {
                    string state = element.Key;
                    Cube nc = new(element.Key);
                    byte eval = element.Value.Item2;
                    if (eval < minEval)
                    {
                        minEval = eval;
                        Console.WriteLine("Min eval : " + eval);
                    }
                    if (eval <= 4)
                    {
                        tasks.Add(Task.Run((byte[], bool) () =>
                        {
                            Cube c1 = nc.Clone();
                            Cube secu = c1.Clone();
                            byte[] sol = MeetInTheMiddle(c1, new Cube(), 6);
                            secu.ExecuterAlgorithme(sol);
                            Console.WriteLine("Solved : " + secu.IsSolved);
                            if (secu.IsSolved)
                            {
                                foreach (Dictionary<string, (byte, byte)> layer in arbre.Skip(1).Reverse<Dictionary<string, (byte, byte)>>())
                                {
                                    foreach (KeyValuePair<string, (byte, byte)> cube in layer)
                                    {
                                        if (cube.Key == state)
                                        {
                                            path.Add(cube.Value.Item1);
                                            nc.DoMove(Move.GetReversalMove(cube.Value.Item1));
                                            state = nc.ToString();
                                            break;
                                        }
                                    }
                                }
                                path.Reverse();
                                return (LightOptimization(path.Concat(sol).ToList()).ToArray(), true);
                            }
                            return ([], false);
                        }));
                    }
                }
                while (tasks.Count > 0)
                {
                    var taskIndex = Task.WaitAny(tasks.ToArray());
                    if (tasks[taskIndex].Result.Item2)
                    {
                        return tasks[taskIndex].Result.Item1;
                    }
                    tasks.RemoveAt(taskIndex);
                }
                NextTreeBranchForMITM(arbre, true);
            }
        }

        public void DoMove(byte j)
        {
            switch (j)
            {
                case 0:
                    F();
                    break;
                case 1:
                    U();
                    break;
                case 2:
                    B();
                    break;
                case 3:
                    L();
                    break;
                case 4:
                    D();
                    break;
                case 5:
                    R();
                    break;
                case 6:
                    Fprime();
                    break;
                case 7:
                    Uprime();
                    break;
                case 8:
                    Bprime();
                    break;
                case 9:
                    Lprime();
                    break;
                case 10:
                    Dprime();
                    break;
                case 11:
                    Rprime();
                    break;
                case 12:
                    F2();
                    break;
                case 13:
                    U2();
                    break;
                case 14:
                    B2();
                    break;
                case 15:
                    L2();
                    break;
                case 16:
                    D2();
                    break;
                case 17:
                    R2();
                    break;
            }
        }

        public string PrintCube()
        {
            StringBuilder sb = new();
            sb.AppendLine(WhiteFace.PrintFace());
            sb.AppendLine(YellowFace.PrintFace());
            sb.AppendLine(RedFace.PrintFace());
            sb.AppendLine(GreenFace.PrintFace());
            sb.AppendLine(BlueFace.PrintFace());
            sb.AppendLine(OrangeFace.PrintFace());
            return sb.ToString();
        }

        public List<string> PrintCubeColors()
        {
            return
            [
                WhiteFace.PrintFace(),
                YellowFace.PrintFace(),
                RedFace.PrintFace(),
                GreenFace.PrintFace(),
                BlueFace.PrintFace(),
                OrangeFace.PrintFace()
            ];
        }

        public void ExecuterAlgorithme(IEnumerable<string> algorithme)
        {
            foreach (string item in algorithme)
            {
                switch (item)
                {
                    case "F":
                        DoMove(0);
                        break;
                    case "U":
                        DoMove(1);
                        break;
                    case "B":
                        DoMove(2);
                        break;
                    case "L":
                        DoMove(3);
                        break;
                    case "D":
                        DoMove(4);
                        break;
                    case "R":
                        DoMove(5);
                        break;
                    case "F'":
                        DoMove(6);
                        break;
                    case "U'":
                        DoMove(7);
                        break;
                    case "B'":
                        DoMove(8);
                        break;
                    case "L'":
                        DoMove(9);
                        break;
                    case "D'":
                        DoMove(10);
                        break;
                    case "R'":
                        DoMove(11);
                        break;
                    case "F2":
                        DoMove(12);
                        break;
                    case "U2":
                        DoMove(13);
                        break;
                    case "B2":
                        DoMove(14);
                        break;
                    case "L2":
                        DoMove(15);
                        break;
                    case "D2":
                        DoMove(16);
                        break;
                    case "R2":
                        DoMove(17);
                        break;
                    default:
                        throw new Exception("Le mouvement n'existe pas");
                }
            }
        }

        public void ExecuterAlgorithme(IEnumerable<byte> algorithme)
        {
            foreach (byte move in algorithme)
            {
                DoMove(move);
            }
        }

        private static readonly char[] IntegerToColor =
        [
            'W',
            'Y',
            'R',
            'G',
            'B',
            'O'
        ];
        public static (ulong, ulong) CompressState(string state)
        {
            ulong fst = 0;
            ulong snd = 0;

            for (int i = 0; i < 27; i++)
            {
                if (i % 9 != 4)
                {
                    fst *= 6;
                    fst += state[i] switch
                    {
                        'W' => 0,
                        'Y' => 1,
                        'R' => 2,
                        'G' => 3,
                        'B' => 4,
                        _ => 5,
                    };
                }
            }

            for (int i = 27; i < 54; i++)
            {
                if (i % 9 != 4)
                {
                    snd *= 6;
                    snd += state[i] switch
                    {
                        'W' => 0,
                        'Y' => 1,
                        'R' => 2,
                        'G' => 3,
                        'B' => 4,
                        _ => 5,
                    };
                }
            }
            return (fst, snd);
        }
        public static string DecompressState((ulong, ulong) compressedState)
        {
            ulong fst = compressedState.Item1;
            ulong snd = compressedState.Item2;

            StringBuilder sb = new(54);

            for (int i = 23; i >= 0; i--)
            {
                sb.Insert(0, IntegerToColor[fst % 6]);
                fst /= 6;
            }

            for (int i = 23; i >= 0; i--)
            {
                sb.Insert(24, IntegerToColor[snd % 6]);
                snd /= 6;
            }

            sb.Insert(4, 'W');
            sb.Insert(13, 'Y');
            sb.Insert(22, 'R');
            sb.Insert(31, 'G');
            sb.Insert(40, 'B');
            sb.Insert(49, 'O');
            return sb.ToString();
        }

        public static byte[] MeetInTheMiddle(Cube initialCube, Cube? finalCube = null, int? deepMax = null)
        {
            finalCube ??= new();
            bool isSolved = false;
            int i = 0;
            Dictionary<(ulong, ulong), byte> dico1 = new()
            {
                { CompressState(initialCube.ToString()), 255}
            };
            Dictionary<(ulong, ulong), byte> dico2 = new()
            {
                { CompressState(finalCube.ToString()), 255}
            };
            List<Dictionary<(ulong, ulong), byte>> arbreInitial = [];
            List<Dictionary<(ulong, ulong), byte>> arbreFinal = [];
            arbreInitial.Add(dico1);
            arbreFinal.Add(dico2);
            IEnumerable<byte> solution = new List<byte>();
            while (!isSolved)
            {
                Console.WriteLine(i + " " + arbreInitial[^1].Count);
                if (i == deepMax) return [];
                Parallel.Invoke(
                    () => NextTreeBranchForMITM(arbreInitial, false),
                    () => NextTreeBranchForMITM(arbreFinal, false)
                    );
                IEnumerable<(ulong, ulong)> commonLastsElements = arbreInitial.TakeLast(2).SelectMany(x => x.Keys)
                    .Intersect(arbreFinal.TakeLast(2).SelectMany(x => x.Keys));
                if (!commonLastsElements.Any())
                {
                    i++;
                    continue;
                }
                IEnumerable<(ulong, ulong)> arbreFinalManySelected = arbreFinal.SelectMany(x => x.Keys);
                IEnumerable<(ulong, ulong)> arbreInitialManySelected = arbreInitial.SelectMany(x => x.Keys);
                IEnumerable<(ulong, ulong)> hasCommonElements = arbreInitialManySelected.Intersect(arbreFinalManySelected);
                if (hasCommonElements.Any())
                {
                    isSolved = true;
                    int min = int.MaxValue;
                    foreach ((ulong, ulong) element in hasCommonElements)
                    {
                        List<byte> path = [];
                        Cube newCube = new(DecompressState(element));
                        for (int j = 1; j <= arbreFinal.Count; j++)
                        {
                            (ulong, ulong) elementEtapeAvant = CompressState(newCube.ToString());
                            if (arbreFinal[^j].ContainsKey(elementEtapeAvant))
                            {
                                if (arbreFinal[^j][elementEtapeAvant] == 255) break;
                                path.Add(arbreFinal[^j][elementEtapeAvant]);
                                newCube.ExecuterAlgorithme(Move.GetReversalPath(path.TakeLast(1)));
                                // On exécute le dernier mouvement mais à l'envers
                            }
                        }
                        List<byte> path2 = [];
                        Cube newCube2 = new(DecompressState(element));
                        for (int j = 1; j <= arbreInitial.Count; j++)
                        {
                            (ulong, ulong) elementEtapeAvant = CompressState(newCube2.ToString());
                            if (arbreInitial[^j].ContainsKey(elementEtapeAvant))
                            {
                                if (arbreInitial[^j][elementEtapeAvant] == 255) break;
                                path2.Add(arbreInitial[^j][elementEtapeAvant]);
                                newCube2.ExecuterAlgorithme(Move.GetReversalPath(path2.TakeLast(1)));
                            }
                        }
                        IEnumerable<byte> solutionFromRandom = Move.GetReversalPath(path.Reverse<byte>()).Concat(path2).Reverse();
                        int count = solutionFromRandom.Count();
                        if (count < min)
                        {
                            solution = solutionFromRandom;
                            min = count;
                        }
                    }
                }
                i++;
            }
            return solution.ToArray();
        }

        public static List<byte> LightOptimization(List<byte> path)
        {
            if (path.Count == 0) return [];
            List<byte> newPath = [];
            for (int i = 0; i < path.Count - 1; i++)
            {
                if (i >= path.Count - 2)
                {
                    newPath.Add(path[^2]);
                    break;
                }
                if (path[i] == path[i + 1] && path[i] == path[i + 2])
                {
                    if (i == path.Count - 3)
                    {
                        newPath.Add(Move.GetReversalMove(path[i]));
                        return newPath;
                    }
                    newPath.Add(Move.GetReversalMove(path[i]));
                    i += 2;
                }
                else if (path[i] == Move.GetReversalMove(path[i + 1]))
                {
                    i++;
                }
                else if (path[i] == path[i + 1])
                {
                    if (path[i] >= 12)
                    {
                        i++;
                    }
                    else
                    {
                        newPath.Add(Move.GetDoubleMove(path[i]));
                        i++;
                    }
                }
                else
                {
                    if (path[i] < 12 && Move.GetDoubleMove(path[i]) == path[i + 1])
                    {

                        newPath.Add(Move.GetReversalMove(path[i]));
                        i++;
                    }
                    else if (path[i + 1] < 12 && Move.GetDoubleMove(path[i + 1]) == path[i])
                    {
                        newPath.Add(Move.GetReversalMove(path[i + 1]));
                        i++;
                    }
                    else
                        newPath.Add(path[i]);
                }
            }
            if (newPath.Count != 0)
            {
                if (newPath[^1] == path[^1] && newPath[^1] < 12)
                {
                    newPath[^1] = Move.GetDoubleMove(newPath[^1]);
                    return newPath;
                }
                if (newPath[^1] == Move.GetReversalMove(path[^1]) || newPath[^1] == path[^1])
                {
                    return newPath.SkipLast(1).ToList();
                }
                if (newPath.Count > 1)
                {
                    if (newPath[^1] == Move.GetReversalMove(newPath[^2]))
                    {
                        return newPath.SkipLast(2).Append(path[^1]).ToList();
                    }
                    if (newPath[^1] == newPath[^2] && newPath[^1] < 12)
                    {
                        newPath[^2] = Move.GetDoubleMove(newPath[^1]);
                        return newPath.SkipLast(1).Append(path[^1]).ToList();
                    }
                }
            }
            newPath.Add(path[^1]);
            return newPath;
        }

        public static byte[] OptimizePath(List<byte> path, int chunkSize = 2, int sizeMax = 4, bool shifting = true)
        {
            if (path.Count == 0) return [.. path];
            List<byte> finalFlatPath = path;
            const int maxDeep = 8;
            for (int k = chunkSize; k <= sizeMax; k++)
            {
                int longueurPred;
                IEnumerable<byte[]> chunks = path.Chunk(k);
                do
                {
                    longueurPred = finalFlatPath.Count;
                    List<byte[]> finalPath = [];
                    Cube c1 = new();
                    Cube c2 = new();
                    foreach (byte[] item in chunks)
                    {
                        c2.ExecuterAlgorithme(item);
                        byte[] minPath = MeetInTheMiddle(c1.Clone(), c2.Clone(), maxDeep);
                        finalPath.Add(minPath);
                        c1.ExecuterAlgorithme(item);
                    }
                    finalFlatPath = finalPath.SelectMany(x => x).ToList();
                } while (longueurPred > finalFlatPath.Count);
            }
            if (!shifting) return [.. finalFlatPath];
            int i = 4;
            for (int j = 2; j <= i; j++)
            {
                List<byte[]> finalPath = [];
                List<byte[]> chunks = finalFlatPath.Skip(j).Chunk(i).ToList();
                IEnumerable<byte> begining = finalFlatPath.Take(j);
                chunks.Insert(0, begining.ToArray());
                Cube c1 = new();
                Cube c2 = new();
                foreach (byte[]? item in chunks)
                {
                    c2.ExecuterAlgorithme(item);
                    byte[] minPath = MeetInTheMiddle(c1.Clone(), c2.Clone(), maxDeep);
                    finalPath.Add(minPath);
                    c1.ExecuterAlgorithme(item);
                }
                finalFlatPath = finalPath.SelectMany(x => x).ToList();
            }
            return [.. finalFlatPath];
        }

        public static int Periodicity(IEnumerable<string> algorithm)
        {
            int cpt = 0;
            Cube solvedCube = new();
            do
            {
                solvedCube.ExecuterAlgorithme(algorithm);
                cpt++;
            } while (!solvedCube.IsSolved);
            return cpt;
        }

        public static int Periodicity(IEnumerable<byte> algorithm)
        {
            int cpt = 0;
            Cube solvedCube = new();
            do
            {
                solvedCube.ExecuterAlgorithme(algorithm);
                cpt++;
            } while (!solvedCube.IsSolved);
            return cpt;
        }

        private static Cube PlaceEdges(Cube c, List<byte> path)
        {
            List<Dictionary<string, byte[]>> arbre = [];
            Dictionary<string, byte[]> dico = new()
            {
                { c.ToString(), [0] }
            };
            arbre.Add(dico);
            bool firstEdge(Cube c) => c.WhiteFace.Pieces[2, 1] == 'W' && c.RedFace.Pieces[0, 1] == 'R';
            bool secondEdge(Cube c) => c.WhiteFace.Pieces[1, 2] == 'W' && c.BlueFace.Pieces[0, 1] == 'B';
            bool thirdEdge(Cube c) => c.WhiteFace.Pieces[1, 0] == 'W' && c.GreenFace.Pieces[0, 1] == 'G';
            bool fourthEdge(Cube c) => c.WhiteFace.Pieces[0, 1] == 'W' && c.OrangeFace.Pieces[0, 1] == 'O';
            bool oneEdgeIsPlaced(Cube c) => firstEdge(c) || secondEdge(c) || thirdEdge(c) || fourthEdge(c);
            bool isPlaced = oneEdgeIsPlaced(c);
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, oneEdgeIsPlaced);
                if (newC is not null)
                {
                    c = newC.Value.Item1;
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            arbre = [];
            dico = new()
            {
                { c.ToString(), [0] }
            };
            arbre.Add(dico);
            isPlaced = (firstEdge(c) && secondEdge(c))
                        || (firstEdge(c) && thirdEdge(c))
                        || (firstEdge(c) && fourthEdge(c))
                        || (secondEdge(c) && thirdEdge(c))
                        || (thirdEdge(c) && fourthEdge(c))
                        || (secondEdge(c) && fourthEdge(c));
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, (c) => (firstEdge(c) && secondEdge(c))
                        || (firstEdge(c) && thirdEdge(c))
                        || (firstEdge(c) && fourthEdge(c))
                        || (secondEdge(c) && thirdEdge(c))
                        || (thirdEdge(c) && fourthEdge(c))
                        || (secondEdge(c) && fourthEdge(c)));
                if (newC != null)
                {
                    c = newC.Value.Item1;
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            arbre = [];
            dico = new()
            {
                { c.ToString(), [0] }
            };
            arbre.Add(dico);
            isPlaced = (firstEdge(c) && secondEdge(c) && thirdEdge(c))
                        || (firstEdge(c) && thirdEdge(c) && fourthEdge(c))
                        || (firstEdge(c) && fourthEdge(c) && secondEdge(c))
                        || (secondEdge(c) && thirdEdge(c) && fourthEdge(c));
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, (c) => (firstEdge(c) && secondEdge(c) && thirdEdge(c))
                        || (firstEdge(c) && thirdEdge(c) && fourthEdge(c))
                        || (firstEdge(c) && fourthEdge(c) && secondEdge(c))
                        || (secondEdge(c) && thirdEdge(c) && fourthEdge(c)));
                if (newC is not null)
                {
                    c = newC.Value.Item1;
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }

            arbre = [];
            dico = new()
            {
                { c.ToString(), [0] }
            };
            arbre.Add(dico);
            isPlaced = firstEdge(c) && secondEdge(c) && thirdEdge(c) && fourthEdge(c);
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, (c) => firstEdge(c) && secondEdge(c) && thirdEdge(c) && fourthEdge(c));
                if (newC is not null)
                {
                    c = newC.Value.Item1;
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            return c;
        }

        private static Cube PlaceCorners(Cube c, List<byte> path)
        {
            bool crossAndEdges(Cube c) => c.WhiteFace.Pieces[0, 1] == 'W' && c.WhiteFace.Pieces[1, 0] == 'W'
            && c.WhiteFace.Pieces[1, 2] == 'W' && c.WhiteFace.Pieces[2, 1] == 'W'
            && c.RedFace.Pieces[0, 1] == 'R' && c.BlueFace.Pieces[0, 1] == 'B'
            && c.GreenFace.Pieces[0, 1] == 'G' && c.OrangeFace.Pieces[0, 1] == 'O';

            bool firstCornerIsPlaced(Cube c) =>
                (c.BlueFace.Pieces[0, 0] == 'B' && c.WhiteFace.Pieces[2, 2] == 'W' && c.RedFace.Pieces[0, 2] == 'R')
                || (c.BlueFace.Pieces[0, 0] == 'R' && c.WhiteFace.Pieces[2, 2] == 'B' && c.RedFace.Pieces[0, 2] == 'W')
                || (c.BlueFace.Pieces[0, 0] == 'W' && c.WhiteFace.Pieces[2, 2] == 'R' && c.RedFace.Pieces[0, 2] == 'B');

            bool secondCornerIsPlaced(Cube c) =>
                (c.RedFace.Pieces[0, 0] == 'R' && c.WhiteFace.Pieces[2, 0] == 'W' && c.GreenFace.Pieces[0, 2] == 'G')
                || (c.RedFace.Pieces[0, 0] == 'G' && c.WhiteFace.Pieces[2, 0] == 'R' && c.GreenFace.Pieces[0, 2] == 'W')
                || (c.RedFace.Pieces[0, 0] == 'W' && c.WhiteFace.Pieces[2, 0] == 'G' && c.GreenFace.Pieces[0, 2] == 'R');

            bool thirdCornerIsPlaced(Cube c) =>
                (c.OrangeFace.Pieces[0, 0] == 'O' && c.BlueFace.Pieces[0, 2] == 'B' && c.WhiteFace.Pieces[0, 2] == 'W')
                || (c.OrangeFace.Pieces[0, 0] == 'B' && c.BlueFace.Pieces[0, 2] == 'W' && c.WhiteFace.Pieces[0, 2] == 'O')
                || (c.OrangeFace.Pieces[0, 0] == 'W' && c.BlueFace.Pieces[0, 2] == 'O' && c.WhiteFace.Pieces[0, 2] == 'B');

            bool fourthCornerIsPlaced(Cube c) =>
            (c.WhiteFace.Pieces[0, 0] == 'W' && c.GreenFace.Pieces[0, 0] == 'G' && c.OrangeFace.Pieces[0, 2] == 'O')
            || (c.WhiteFace.Pieces[0, 0] == 'G' && c.GreenFace.Pieces[0, 0] == 'O' && c.OrangeFace.Pieces[0, 2] == 'W')
            || (c.WhiteFace.Pieces[0, 0] == 'O' && c.GreenFace.Pieces[0, 0] == 'W' && c.OrangeFace.Pieces[0, 2] == 'G');

            bool oneCornerIsPlaced(Cube c) => firstCornerIsPlaced(c) || secondCornerIsPlaced(c) || thirdCornerIsPlaced(c)
                || fourthCornerIsPlaced(c);
            bool allCornersIsPlaced(Cube c) => firstCornerIsPlaced(c) && secondCornerIsPlaced(c) && thirdCornerIsPlaced(c)
            && fourthCornerIsPlaced(c);
            List<Dictionary<string, byte[]>> arbre = [];
            Dictionary<string, byte[]> dico = new()
            {
                { c.ToString(), [0] }
            };
            arbre.Add(dico);
            bool isPlaced = crossAndEdges(c) && oneCornerIsPlaced(c);
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, (c) => crossAndEdges(c) && oneCornerIsPlaced(c));
                if (newC is not null)
                {
                    c = newC.Value.Item1;
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            arbre = [];
            dico = new()
            {
                { c.ToString(), [0] }
            };
            arbre.Add(dico);
            isPlaced = crossAndEdges(c)
                        && ((firstCornerIsPlaced(c) && secondCornerIsPlaced(c))
                        || (firstCornerIsPlaced(c) && thirdCornerIsPlaced(c))
                        || (firstCornerIsPlaced(c) && fourthCornerIsPlaced(c))
                        || (secondCornerIsPlaced(c) && thirdCornerIsPlaced(c))
                        || (thirdCornerIsPlaced(c) && fourthCornerIsPlaced(c))
                        || (secondCornerIsPlaced(c) && fourthCornerIsPlaced(c)));
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, (c) => crossAndEdges(c)
                        && ((firstCornerIsPlaced(c) && secondCornerIsPlaced(c))
                        || (firstCornerIsPlaced(c) && thirdCornerIsPlaced(c))
                        || (firstCornerIsPlaced(c) && fourthCornerIsPlaced(c))
                        || (secondCornerIsPlaced(c) && thirdCornerIsPlaced(c))
                        || (thirdCornerIsPlaced(c) && fourthCornerIsPlaced(c))
                        || (secondCornerIsPlaced(c) && fourthCornerIsPlaced(c))));
                if (newC is not null)
                {
                    c = newC.Value.Item1;
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            arbre = [];
            dico = new()
            {
                { c.ToString(), [0] }
            };
            arbre.Add(dico);
            isPlaced = crossAndEdges(c)
                        && ((firstCornerIsPlaced(c) && secondCornerIsPlaced(c) && thirdCornerIsPlaced(c))
                        || (firstCornerIsPlaced(c) && thirdCornerIsPlaced(c) && fourthCornerIsPlaced(c))
                        || (firstCornerIsPlaced(c) && fourthCornerIsPlaced(c) && secondCornerIsPlaced(c))
                        || (secondCornerIsPlaced(c) && thirdCornerIsPlaced(c) && fourthCornerIsPlaced(c)));
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, (c) => crossAndEdges(c)
                        && ((firstCornerIsPlaced(c) && secondCornerIsPlaced(c) && thirdCornerIsPlaced(c))
                        || (firstCornerIsPlaced(c) && thirdCornerIsPlaced(c) && fourthCornerIsPlaced(c))
                        || (firstCornerIsPlaced(c) && fourthCornerIsPlaced(c) && secondCornerIsPlaced(c))
                        || (secondCornerIsPlaced(c) && thirdCornerIsPlaced(c) && fourthCornerIsPlaced(c))));
                if (newC is not null)
                {
                    c = newC.Value.Item1;
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            arbre = [];
            dico = new()
            {
                { c.ToString(), [0] }
            };
            arbre.Add(dico);
            isPlaced = crossAndEdges(c)
                        && allCornersIsPlaced(c);

            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, (c) => crossAndEdges(c)
                        && allCornersIsPlaced(c));
                if (newC is not null)
                {
                    c = newC.Value.Item1;
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            return c;
        }
        private static Cube OrientCorners(Cube c, List<byte> path)
        {
            bool firstCornerOriented() => c.BlueFace.Pieces[0, 1] == 'B'
                && c.RedFace.Pieces[0, 2] == 'R'
                && c.WhiteFace.Pieces[2, 2] == 'W';
            bool secondCornerIsOriented() => c.RedFace.Pieces[0, 0] == 'R'
                && c.WhiteFace.Pieces[2, 0] == 'W'
                && c.GreenFace.Pieces[0, 2] == 'G';
            bool thirdCornerIsOriented() => c.OrangeFace.Pieces[0, 1] == 'O'
                && c.BlueFace.Pieces[0, 2] == 'B'
                && c.WhiteFace.Pieces[0, 2] == 'W';
            bool fourthCornerIsOriented() => c.WhiteFace.Pieces[0, 0] == 'W'
                && c.GreenFace.Pieces[0, 0] == 'G'
                && c.OrangeFace.Pieces[0, 2] == 'O';
            bool allCornersIsOriented() => firstCornerOriented() && secondCornerIsOriented() && thirdCornerIsOriented()
                && fourthCornerIsOriented();
            while (!allCornersIsOriented())
            {
                if (path.Count > 1000) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                if (c.RedFace.Pieces[0, 0] == 'W')
                {
                    c.ExecuterAlgorithme(quadInverseSexyMove);
                    path.AddRange(quadInverseByteAlgo);
                    if (allCornersIsOriented()) break;
                    c.Uprime();
                    path.AddRange(uPrimeAlgo);
                }
                else if (c.GreenFace.Pieces[0, 2] == 'W')
                {
                    c.ExecuterAlgorithme(doubleInverseSexyMove);
                    path.AddRange(doubleInverseByteAlgo);
                    if (allCornersIsOriented()) break;
                    c.Uprime();
                    path.AddRange(uPrimeAlgo);
                }
                else
                {
                    c.Uprime();
                    path.AddRange(uPrimeAlgo);
                }
            }
            return c;
        }
        private static Cube SecondLayer(Cube c, List<byte> path, IEnumerable<byte> dAlgo)
        {
            bool crossAndEdges() => c.WhiteFace.Pieces[0, 1] == 'W' && c.WhiteFace.Pieces[1, 0] == 'W'
            && c.WhiteFace.Pieces[1, 2] == 'W' && c.WhiteFace.Pieces[2, 1] == 'W'
            && c.RedFace.Pieces[0, 1] == 'R' && c.BlueFace.Pieces[0, 1] == 'B'
            && c.GreenFace.Pieces[0, 1] == 'G' && c.OrangeFace.Pieces[0, 1] == 'O';
            bool isSecondLayerDone() => crossAndEdges()
                    && c.WhiteFace.IsUniform && c.RedFace.Pieces[0, 1] == 'R'
                    && c.BlueFace.Pieces[0, 1] == 'B' && c.GreenFace.Pieces[0, 1] == 'G'
                    && c.OrangeFace.Pieces[0, 1] == 'O'
                    && c.RedFace.Pieces[1, 0] == 'R' && c.RedFace.Pieces[1, 2] == 'R'
                    && c.OrangeFace.Pieces[1, 0] == 'O' && c.OrangeFace.Pieces[1, 2] == 'O'
                    && c.BlueFace.Pieces[1, 0] == 'B' && c.BlueFace.Pieces[1, 2] == 'B'
                    && c.GreenFace.Pieces[1, 0] == 'G' && c.GreenFace.Pieces[1, 2] == 'G';
            int i = 0;
            while (!isSecondLayerDone())
            {
                if (path.Count > 1000) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                if (c.RedFace.Pieces[2, 1] == 'R' && c.YellowFace.Pieces[0, 1] != 'Y')
                {
                    if (c.YellowFace.Pieces[0, 1] == 'B')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftRedFace);
                        path.AddRange(secondLayerLeftRedFace);
                        i = 0;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightRedFace);
                        path.AddRange(secondLayerRightRedFace);
                        i = 0;
                    }
                }

                else if (c.BlueFace.Pieces[2, 1] == 'B' && c.YellowFace.Pieces[1, 2] != 'Y')
                {
                    if (c.YellowFace.Pieces[1, 2] == 'O')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftBlueFace);
                        path.AddRange(secondLayerLeftBlueFace);
                        i = 0;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightBlueFace);
                        path.AddRange(secondLayerRightBlueFace);
                        i = 0;
                    }
                }

                else if (c.GreenFace.Pieces[2, 1] == 'G' && c.YellowFace.Pieces[1, 0] != 'Y')
                {
                    if (c.YellowFace.Pieces[1, 0] == 'R')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftGreenFace);
                        path.AddRange(secondLayerLeftGreenFace);
                        i = 0;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightGreenFace);
                        path.AddRange(secondLayerRightGreenFace);
                        i = 0;
                    }
                }

                else if (c.OrangeFace.Pieces[2, 1] == 'O' && c.YellowFace.Pieces[2, 1] != 'Y')
                {
                    if (c.YellowFace.Pieces[2, 1] == 'G')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftOrangeFace);
                        path.AddRange(secondLayerLeftOrangeFace);
                        i = 0;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightOrangeFace);
                        path.AddRange(secondLayerRightOrangeFace);
                        i = 0;
                    }
                }
                else
                {
                    c.D();
                    path.AddRange(dAlgo);
                }

                if (i >= 3)
                {
                    if (c.RedFace.Pieces[1, 0] != 'R')
                    {
                        c.ExecuterAlgorithme(secondLayerRightRedFace.Skip(1));
                        path.AddRange(secondLayerRightRedFace.Skip(1));
                    }
                    else if (c.RedFace.Pieces[1, 2] != 'R')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftRedFace.Skip(1));
                        path.AddRange(secondLayerLeftRedFace.Skip(1));
                    }

                    else if (c.BlueFace.Pieces[1, 0] != 'B')
                    {
                        c.ExecuterAlgorithme(secondLayerRightBlueFace.Skip(1));
                        path.AddRange(secondLayerRightBlueFace.Skip(1));
                    }
                    else if (c.BlueFace.Pieces[1, 2] != 'B')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftBlueFace.Skip(1));
                        path.AddRange(secondLayerLeftBlueFace.Skip(1));
                    }

                    else if (c.GreenFace.Pieces[1, 0] != 'G')
                    {
                        c.ExecuterAlgorithme(secondLayerRightGreenFace.Skip(1));
                        path.AddRange(secondLayerRightGreenFace.Skip(1));
                    }
                    else if (c.GreenFace.Pieces[1, 2] != 'G')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftGreenFace.Skip(1));
                        path.AddRange(secondLayerLeftGreenFace.Skip(1));
                    }

                    else if (c.OrangeFace.Pieces[1, 0] != 'O')
                    {
                        c.ExecuterAlgorithme(secondLayerRightOrangeFace.Skip(1));
                        path.AddRange(secondLayerRightOrangeFace.Skip(1));
                    }
                    else if (c.OrangeFace.Pieces[1, 2] != 'O')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftOrangeFace.Skip(1));
                        path.AddRange(secondLayerLeftOrangeFace.Skip(1));
                    }
                    i = 0;
                }
                i++;
            }
            return c;
        }
        private static Cube YellowCross(Cube c, List<byte> path, IEnumerable<byte> dAlgo)
        {
            bool yellowCrossIsDone() => c.YellowFace.Pieces[0, 1] == 'Y' && c.YellowFace.Pieces[1, 0] == 'Y'
                                     && c.YellowFace.Pieces[1, 2] == 'Y' && c.YellowFace.Pieces[2, 1] == 'Y';
            while (!yellowCrossIsDone())
            {
                if (path.Count > 1000) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                if (c.YellowFace.Pieces[0, 1] != 'Y' && c.YellowFace.Pieces[1, 0] != 'Y' && c.YellowFace.Pieces[1, 2] != 'Y' &&
                c.YellowFace.Pieces[2, 1] != 'Y')
                {
                    c.ExecuterAlgorithme(algoCrossPattern);
                    path.AddRange(algoCrossPattern);
                    c.D();
                    path.AddRange(dAlgo);
                    c.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(algoCrossPattern2);
                    break;
                }
                else if (c.YellowFace.Pieces[0, 1] == 'Y' && c.YellowFace.Pieces[2, 1] == 'Y')
                {
                    c.D();
                    path.AddRange(dAlgo);
                    c.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(algoCrossPattern2);
                    break;
                }
                else if (c.YellowFace.Pieces[1, 0] == 'Y' && c.YellowFace.Pieces[1, 2] == 'Y')
                {
                    c.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(algoCrossPattern2);
                    break;
                }
                else if (c.YellowFace.Pieces[1, 2] == 'Y' && c.YellowFace.Pieces[2, 1] == 'Y')
                {
                    c.ExecuterAlgorithme(algoCrossPattern);
                    path.AddRange(algoCrossPattern);
                    break;
                }
                else
                {
                    c.D();
                    path.AddRange(dAlgo);
                }
            }
            return c;
        }
        private static Cube OrientEdges(Cube c, List<byte> path, IEnumerable<byte> dAlgo)
        {
            bool edgeIsPlaced() => c.RedFace.Pieces[2, 1] == 'R' && c.GreenFace.Pieces[2, 1] == 'G'
                    && c.OrangeFace.Pieces[2, 1] == 'O' && c.BlueFace.Pieces[2, 1] == 'B';
            while (!edgeIsPlaced())
            {
                if (path.Count > 1000) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                if (c.RedFace.Pieces[2, 1] == 'R' && c.BlueFace.Pieces[2, 1] == 'B')
                {
                    path.AddRange(redF);
                    c.ExecuterAlgorithme(redF);
                    break;
                }
                else if (c.BlueFace.Pieces[2, 1] == 'B' && c.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c.ExecuterAlgorithme(blueF);
                    path.AddRange(blueF);
                    break;
                }
                else if (c.GreenFace.Pieces[2, 1] == 'G' && c.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c.ExecuterAlgorithme(greenF);
                    path.AddRange(greenF);
                    break;
                }
                else if (c.GreenFace.Pieces[2, 1] == 'G' && c.RedFace.Pieces[2, 1] == 'R')
                {
                    c.ExecuterAlgorithme(greenF2);
                    path.AddRange(greenF2);
                    break;
                }
                else if (c.RedFace.Pieces[2, 1] == 'R' && c.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c.ExecuterAlgorithme(redF2);
                    path.AddRange(redF2);
                }
                else if (c.BlueFace.Pieces[2, 1] == 'B' && c.GreenFace.Pieces[2, 1] == 'G')
                {
                    c.ExecuterAlgorithme(blueF2);
                    path.AddRange(blueF2);
                }
                else
                {
                    c.D();
                    path.AddRange(dAlgo);
                }
            }
            return c;
        }
        private static Cube PlaceSecondCorners(Cube c, List<byte> path)
        {
            bool cornersIsPlaced() => ((c.RedFace.Pieces[2, 2] == 'R' && c.BlueFace.Pieces[2, 0] == 'B')
                || (c.RedFace.Pieces[2, 2] == 'B' && c.BlueFace.Pieces[2, 0] == 'Y' && c.YellowFace.Pieces[0, 2] == 'R')
                || (c.RedFace.Pieces[2, 2] == 'Y' && c.BlueFace.Pieces[2, 0] == 'R' && c.YellowFace.Pieces[0, 2] == 'B'))
                && ((c.RedFace.Pieces[2, 0] == 'R' && c.GreenFace.Pieces[2, 2] == 'G')
                || (c.RedFace.Pieces[2, 0] == 'G' && c.GreenFace.Pieces[2, 2] == 'Y' && c.YellowFace.Pieces[0, 0] == 'R')
                || (c.RedFace.Pieces[2, 0] == 'Y' && c.GreenFace.Pieces[2, 2] == 'R' && c.YellowFace.Pieces[0, 0] == 'G'))
                && ((c.BlueFace.Pieces[2, 2] == 'B' && c.OrangeFace.Pieces[2, 0] == 'O')
                || (c.BlueFace.Pieces[2, 2] == 'O' && c.OrangeFace.Pieces[2, 0] == 'Y' && c.YellowFace.Pieces[2, 2] == 'B')
                || (c.BlueFace.Pieces[2, 2] == 'Y' && c.OrangeFace.Pieces[2, 0] == 'B' && c.YellowFace.Pieces[2, 2] == 'O'));

            while (!cornersIsPlaced())
            {
                if (path.Count > 1000) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                if ((c.RedFace.Pieces[2, 2] == 'R' && c.BlueFace.Pieces[2, 0] == 'B')
                    || (c.RedFace.Pieces[2, 2] == 'B' && c.BlueFace.Pieces[2, 0] == 'Y' && c.YellowFace.Pieces[0, 2] == 'R')
                    || (c.RedFace.Pieces[2, 2] == 'Y' && c.BlueFace.Pieces[2, 0] == 'R' && c.YellowFace.Pieces[0, 2] == 'B'))
                {
                    if (c.BlueFace.Pieces[2, 2] == 'B' || c.BlueFace.Pieces[2, 2] == 'O'
                        || c.OrangeFace.Pieces[2, 0] == 'O' || c.OrangeFace.Pieces[2, 0] == 'B' ||
                        c.YellowFace.Pieces[2, 2] == 'B' || c.YellowFace.Pieces[2, 2] == 'O')
                    {
                        c.ExecuterAlgorithme(cornerAlignementAlgo);
                        path.AddRange(cornerAlignementAlgo);
                        break;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(cornerAlignementOptim);
                        path.AddRange(cornerAlignementOptim);
                        break;
                    }
                }
                else if ((c.RedFace.Pieces[2, 0] == 'R' && c.GreenFace.Pieces[2, 2] == 'G')
                        || (c.RedFace.Pieces[2, 0] == 'G' && c.GreenFace.Pieces[2, 2] == 'Y' && c.YellowFace.Pieces[0, 0] == 'R')
                        || (c.RedFace.Pieces[2, 0] == 'Y' && c.GreenFace.Pieces[2, 2] == 'R' && c.YellowFace.Pieces[0, 0] == 'G'))
                {
                    if (c.RedFace.Pieces[2, 2] == 'R' || c.RedFace.Pieces[2, 2] == 'B' ||
                        c.BlueFace.Pieces[2, 0] == 'B' || c.BlueFace.Pieces[2, 0] == 'R' ||
                        c.YellowFace.Pieces[0, 2] == 'R' || c.YellowFace.Pieces[0, 2] == 'B')
                    {
                        c.ExecuterAlgorithme(cornerAlignementAlgo2);
                        path.AddRange(cornerAlignementAlgo2);
                        break;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(cornerAlignementOptim2);
                        path.AddRange(cornerAlignementOptim2);
                        break;
                    }
                }
                else if ((c.BlueFace.Pieces[2, 2] == 'B' && c.OrangeFace.Pieces[2, 0] == 'O')
                        || (c.BlueFace.Pieces[2, 2] == 'O' && c.OrangeFace.Pieces[2, 0] == 'Y' && c.YellowFace.Pieces[2, 2] == 'B')
                        || (c.BlueFace.Pieces[2, 2] == 'Y' && c.OrangeFace.Pieces[2, 0] == 'B' && c.YellowFace.Pieces[2, 2] == 'O'))
                {
                    if (c.OrangeFace.Pieces[2, 2] == 'O' || c.OrangeFace.Pieces[2, 2] == 'G' ||
                        c.GreenFace.Pieces[2, 0] == 'G' || c.GreenFace.Pieces[2, 0] == 'O' ||
                        c.YellowFace.Pieces[2, 0] == 'G' || c.YellowFace.Pieces[2, 0] == 'O')
                    {
                        c.ExecuterAlgorithme(cornerAlignementAlgo3);
                        path.AddRange(cornerAlignementAlgo3);
                        break;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(cornerAlignementOptim3);
                        path.AddRange(cornerAlignementOptim3);
                        break;
                    }
                }
                else
                {
                    if (c.GreenFace.Pieces[2, 2] == 'R' || c.GreenFace.Pieces[2, 2] == 'G' ||
                        c.RedFace.Pieces[2, 0] == 'R' || c.RedFace.Pieces[2, 0] == 'G' ||
                        c.YellowFace.Pieces[0, 0] == 'G' || c.YellowFace.Pieces[0, 0] == 'R')
                    {
                        c.ExecuterAlgorithme(cornerAlignementAlgo4);
                        path.AddRange(cornerAlignementAlgo4);
                    }
                    else
                    {
                        c.ExecuterAlgorithme(cornerAlignementOptim4);
                        path.AddRange(cornerAlignementOptim4);
                    }
                }
            }
            return c;
        }
        private static void OrientLastCornersOptim(Cube c, List<byte> path)
        {
            bool isSolvedOptim() => c.RedFace.IsUniform && c.WhiteFace.IsUniform && c.YellowFace.IsUniform;
            while (!isSolvedOptim())
            {
                if (path.Count > 1000) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                if (c.GreenFace.Pieces[2, 2] == 'Y')
                {
                    c.ExecuterAlgorithme(quadSM);
                    path.AddRange(quadByteAlgo);
                    c.Dprime();
                    path.AddRange(dPrimeAlgo);
                }
                else if (c.RedFace.Pieces[2, 0] == 'Y')
                {
                    c.ExecuterAlgorithme(doubleSM);
                    path.AddRange(doubleByteAlgo);
                    c.Dprime();
                    path.AddRange(dPrimeAlgo);
                }
                else
                {
                    c.Dprime();
                    path.AddRange(dPrimeAlgo);
                }
            }
        }
        public static List<byte> FastBeginnerMethod(Cube c)
        {
            List<byte> path = [];
            c = PlaceEdges(c, path);
            c = PlaceCorners(c, path);
            c = OrientCorners(c, path);
            c = SecondLayer(c, path, dAlgo);
            c = YellowCross(c, path, dAlgo);
            c = OrientEdges(c, path, dAlgo);
            c = PlaceSecondCorners(c, path);
            OrientLastCornersOptim(c, path);
            return path;
        }

        public static int Conflits(string s, string solution)
        {
            int k = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (solution[i] != s[i]) k++;
            }
            return k;
        }

        public static byte Conflits(Cube c)
        {
            byte conflits = 0;
            // Corners
            if (c.RedFace.Pieces[0, 0] != 'R' || c.GreenFace.Pieces[0, 2] != 'G' || c.WhiteFace.Pieces[2, 0] != 'W')
                conflits++;
            if (c.RedFace.Pieces[0, 2] != 'R' || c.BlueFace.Pieces[0, 0] != 'B' || c.WhiteFace.Pieces[2, 2] != 'W')
                conflits++;
            if (c.BlueFace.Pieces[0, 2] != 'B' || c.OrangeFace.Pieces[0, 0] != 'O' || c.WhiteFace.Pieces[0, 2] != 'W')
                conflits++;
            if (c.OrangeFace.Pieces[0, 2] != 'O' || c.GreenFace.Pieces[0, 0] != 'G' || c.WhiteFace.Pieces[0, 0] != 'W')
                conflits++;
            if (c.RedFace.Pieces[2, 0] != 'R' || c.GreenFace.Pieces[2, 2] != 'G' || c.YellowFace.Pieces[0, 0] != 'Y')
                conflits++;
            if (c.RedFace.Pieces[2, 2] != 'R' || c.BlueFace.Pieces[2, 0] != 'B' || c.YellowFace.Pieces[0, 2] != 'Y')
                conflits++;
            if (c.BlueFace.Pieces[2, 2] != 'B' || c.OrangeFace.Pieces[2, 0] != 'O' || c.YellowFace.Pieces[2, 2] != 'Y')
                conflits++;
            if (c.OrangeFace.Pieces[2, 2] != 'O' || c.GreenFace.Pieces[2, 0] != 'G' || c.YellowFace.Pieces[2, 0] != 'Y')
                conflits++;
            // Edges
            if (c.WhiteFace.Pieces[0, 1] != 'W' || c.OrangeFace.Pieces[0, 1] != 'O')
                conflits++;
            if (c.WhiteFace.Pieces[1, 0] != 'W' || c.GreenFace.Pieces[0, 1] != 'G')
                conflits++;
            if (c.WhiteFace.Pieces[1, 2] != 'W' || c.BlueFace.Pieces[0, 1] != 'B')
                conflits++;
            if (c.WhiteFace.Pieces[2, 1] != 'W' || c.RedFace.Pieces[0, 1] != 'R')
                conflits++;
            if (c.RedFace.Pieces[1, 0] != 'R' || c.GreenFace.Pieces[1, 2] != 'G')
                conflits++;
            if (c.RedFace.Pieces[1, 2] != 'R' || c.BlueFace.Pieces[1, 0] != 'B')
                conflits++;
            if (c.RedFace.Pieces[2, 1] != 'R' || c.YellowFace.Pieces[0, 1] != 'Y')
                conflits++;
            if (c.BlueFace.Pieces[2, 1] != 'B' || c.YellowFace.Pieces[1, 2] != 'Y')
                conflits++;
            if (c.BlueFace.Pieces[1, 2] != 'B' || c.OrangeFace.Pieces[1, 0] != 'O')
                conflits++;
            if (c.OrangeFace.Pieces[1, 1] != 'O' || c.GreenFace.Pieces[1, 0] != 'G')
                conflits++;
            if (c.OrangeFace.Pieces[2, 1] != 'O' || c.YellowFace.Pieces[2, 1] != 'Y')
                conflits++;
            if (c.GreenFace.Pieces[2, 1] != 'G' || c.YellowFace.Pieces[1, 0] != 'Y')
                conflits++;
            return conflits;

        }

        public static List<byte> TabuSearch(Cube c)
        {
            //Random rand = new();
            Cube init = c.Clone();
            string s = new Cube().ToString();
            List<byte> path = [];
            List<byte> bestPath = [];
            int evaluation = Conflits(c);
            int best = evaluation;
            HashSet<string> hs = [c.ToString()];
            Console.WriteLine(best);
            PrintWithColors(c.PrintCubeColors());
            Stopwatch sw = new();
            Stopwatch sw2 = new();
            sw.Start();
            sw2.Start();
            while (evaluation > 0)
            {
                evaluation = Conflits(c);
                for (byte i = 0; i < 18; i++)
                {
                    byte rev = Move.GetReversalMove(i);
                    for (byte j = 0; j < 18; j++)
                    {
                        if (j != i && j != rev)
                        {
                            c.DoMove(j);
                        }
                        c.DoMove(i);
                        string cString = c.ToString();
                        //(ulong, ulong) cCompressed = CompressState(cString);
                        int newEval = Conflits(c);
                        if (newEval < evaluation && hs.Add(cString))
                        {
                            if (j != i && j != rev)
                            {
                                path.Add(j);
                            }
                            path.Add(i);
                            evaluation = newEval;
                            if (evaluation < best)
                            {
                                best = evaluation;
                                Console.WriteLine(best);
                                bestPath = bestPath.Concat(path).Select(x => x).ToList();
                                path.Clear();
                                PrintWithColors(c.PrintCubeColors());
                            }
                            goto breaked;
                        }
                        c.DoMove(Move.GetReversalMove(i));
                        if (j != i && j != rev)
                        {
                            c.DoMove(Move.GetReversalMove(j));
                        }
                    }
                }
                /* byte move = (byte)rand.Next(18);
                 c.DoMove(move);
                 path.Add(move);
                 hs.Add(c.ToString());*/
                int minEval = int.MaxValue;
                byte move = 0;
                string cs = "";
                for (byte j = 0; j < 18; j++)
                {
                    c.DoMove(j);
                    cs = c.ToString();
                    int eval = Conflits(c);
                    if (eval < minEval && hs.Add(cs))
                    {
                        minEval = eval;
                        move = j;
                    }
                    c.DoMove(Move.GetReversalMove(j));
                }
                c.DoMove(move);
                path.Add(move);
                hs.Add(cs);
            breaked:;
                if (sw2.ElapsedMilliseconds >= 600 * 1000) break;
                if (sw.ElapsedMilliseconds >= 180 * 1000)
                {
                    hs.Clear();
                    sw.Restart();
                }
            }
            init.ExecuterAlgorithme(bestPath);
            return bestPath.Concat(MeetInTheMiddle(init)).ToList();
            //return path;
        }

        public static List<byte> TabuSearchExp(Cube c)
        {
            Random rand = new();
            Cube init = c.Clone();
            Cube copy = c.Clone();
            string s = new Cube().ToString();
            List<byte> path = new();
            const int algoSize = 18;
            for (int i = 0; i < algoSize; i++)
            {
                path.Add((byte)rand.Next(18));
            }
            List<byte> bestPath = path.Select(x => x).ToList();
            copy.ExecuterAlgorithme(bestPath);
            int evaluation = Conflits(copy);
            int best = evaluation;
            HashSet<string> hs = [Move.GetStringPath(bestPath)];
            Console.WriteLine(best);
            PrintWithColors(c.PrintCubeColors());
            Stopwatch sw = new();
            Stopwatch sw2 = new();
            sw.Start();
            sw2.Start();
            while (evaluation > 3)
            {
                evaluation = Conflits(copy);
                for (byte i = 0; i < algoSize; i++)
                {
                    byte k = path[i];
                    for (byte j = 0; j < 18; j++)
                    {
                        if (j == Move.GetReversalMove(path[i]) || (path[i] <= 9 && j == Move.GetDoubleMove(path[i])) || j == path[i]) continue;
                        if (i < algoSize - 1 && (path[i + 1] == j || path[i + 1] == Move.GetReversalMove(j))) continue;
                        path[i] = j;
                        //(ulong, ulong) cCompressed = CompressState(cString);
                        copy = init.Clone();
                        copy.ExecuterAlgorithme(path);
                        string cString = copy.ToString();
                        int newEval = Conflits(copy);
                        if (newEval < evaluation && hs.Add(cString))
                        {
                            evaluation = newEval;
                            if (evaluation < best)
                            {
                                best = evaluation;
                                Console.WriteLine(best);
                                bestPath = path.Select(x => x).ToList();
                                PrintWithColors(copy.PrintCubeColors());
                                Console.WriteLine(Move.GetStringPath(bestPath));
                            }
                            goto breaked;
                        }
                        path[i] = k;
                    }
                }
                int r = rand.Next(algoSize-1);
                byte l = (byte)rand.Next(18);
                path[r] = l;
                copy = init.Clone();
                copy.ExecuterAlgorithme(path);
                hs.Add(copy.ToString());
            breaked:;
                if (sw2.ElapsedMilliseconds >= 600 * 1000) break;
                if (sw.ElapsedMilliseconds >= 180 * 1000)
                {
                    hs.Clear();
                    sw.Restart();
                }
            }
            init.ExecuterAlgorithme(bestPath);
            return bestPath.Concat(MeetInTheMiddle(init)).ToList();
        }


        public bool Equals(Cube? other)
        {
            return other is not null &&
                    WhiteFace.Equals(other.WhiteFace) &&
                    RedFace.Equals(other.RedFace) &&
                    GreenFace.Equals(other.GreenFace) &&
                    BlueFace.Equals(other.BlueFace) &&
                    YellowFace.Equals(other.YellowFace) &&
                    OrangeFace.Equals(other.OrangeFace);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Cube);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(WhiteFace, YellowFace, RedFace, GreenFace, BlueFace, OrangeFace);
        }

        public static void PrintWithColors(List<string> ls)
        {
            foreach (string line in ls)
            {
                foreach (char item in line)
                {
                    if (item == 'Y')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('Y');
                    }
                    else if (item == 'R')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('R');
                    }
                    else if (item == 'B')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('B');
                    }
                    else if (item == 'W')
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write('W');
                    }
                    else if (item == 'G')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('G');
                    }
                    else if (item == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write('O');
                    }
                    else
                    {
                        Console.Write(item);
                    }
                }
                Console.WriteLine(' ');
            }
            Console.ResetColor();
        }
    }
}