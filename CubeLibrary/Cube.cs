using System.Diagnostics;
using System.Text;

namespace CubeLibrary
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

        public static List<(int, int)> GetCorners
        {
            get
            {
                return [(0, 0), (0, 2), (2, 0), (2, 2)];
            }
        }

        public static List<(int, int)> GetEdges
        {
            get
            {
                return [(0, 1), (1, 0), (1, 2), (2, 1)];
            }
        }

        public List<Face> GetAllFaces
        {
            get
            {
                return [WhiteFace, RedFace, BlueFace, YellowFace, GreenFace, OrangeFace];
            }
        }

        private static string Up = "U";
        private static string Down = "D";
        private static string Right = "R2";
        private static string Left = "L2";
        private static string Front = "F2";
        private static string Back = "B2";
        public Cube(IEnumerable<Face> faces)
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
            Debug.Assert(WhiteFace is not null);
            Debug.Assert(YellowFace is not null);
            Debug.Assert(RedFace is not null);
            Debug.Assert(GreenFace is not null);
            Debug.Assert(BlueFace is not null);
            Debug.Assert(OrangeFace is not null);
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
            for (int i = 0; i < 6; i++)
            {
                char[,] array = new char[3, 3];
                for (int p = 0; p < 9; p++)
                {
                    array[p / 3, p % 3] = str[idx++];
                }
                Face face = new(array);
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
            Debug.Assert(WhiteFace is not null);
            Debug.Assert(YellowFace is not null);
            Debug.Assert(RedFace is not null);
            Debug.Assert(GreenFace is not null);
            Debug.Assert(BlueFace is not null);
            Debug.Assert(OrangeFace is not null);
        }

        public Cube(int n)
        {
            WhiteFace = new Face('W');
            YellowFace = new Face('Y');
            RedFace = new Face('R');
            GreenFace = new Face('G');
            BlueFace = new Face('B');
            OrangeFace = new Face('O');
            Shuffle(n);
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

            newWhiteFace[2, 0] = GreenFace.Pieces[2, 2];
            newWhiteFace[2, 1] = GreenFace.Pieces[1, 2];
            newWhiteFace[2, 2] = GreenFace.Pieces[0, 2];

            newBlueFace[0, 0] = WhiteFace.Pieces[2, 0];
            newBlueFace[1, 0] = WhiteFace.Pieces[2, 1];
            newBlueFace[2, 0] = WhiteFace.Pieces[2, 2];

            newYellowFace[0, 0] = BlueFace.Pieces[2, 0];
            newYellowFace[0, 1] = BlueFace.Pieces[1, 0];
            newYellowFace[0, 2] = BlueFace.Pieces[0, 0];

            GreenFace.Pieces[0, 2] = YellowFace.Pieces[0, 0];
            GreenFace.Pieces[1, 2] = YellowFace.Pieces[0, 1];
            GreenFace.Pieces[2, 2] = YellowFace.Pieces[0, 2];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
            RedFace.Rotate90Right();
        }

        public void Rprime()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newBlueFace = CopyFace('B');

            char[,] newYellowFace = CopyFace('Y');

            newWhiteFace[2, 0] = BlueFace.Pieces[0, 0];
            newWhiteFace[2, 1] = BlueFace.Pieces[1, 0];
            newWhiteFace[2, 2] = BlueFace.Pieces[2, 0];

            newBlueFace[0, 0] = YellowFace.Pieces[0, 2];
            newBlueFace[1, 0] = YellowFace.Pieces[0, 1];
            newBlueFace[2, 0] = YellowFace.Pieces[0, 0];

            newYellowFace[0, 0] = GreenFace.Pieces[0, 2];
            newYellowFace[0, 1] = GreenFace.Pieces[1, 2];
            newYellowFace[0, 2] = GreenFace.Pieces[2, 2];

            GreenFace.Pieces[0, 2] = WhiteFace.Pieces[2, 2];
            GreenFace.Pieces[1, 2] = WhiteFace.Pieces[2, 1];
            GreenFace.Pieces[2, 2] = WhiteFace.Pieces[2, 0];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
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


            newWhiteFace[0, 0] = BlueFace.Pieces[0, 2];
            newWhiteFace[0, 1] = BlueFace.Pieces[1, 2];
            newWhiteFace[0, 2] = BlueFace.Pieces[2, 2];

            newBlueFace[0, 2] = YellowFace.Pieces[2, 2];
            newBlueFace[1, 2] = YellowFace.Pieces[2, 1];
            newBlueFace[2, 2] = YellowFace.Pieces[2, 0];

            newYellowFace[2, 0] = GreenFace.Pieces[0, 0];
            newYellowFace[2, 1] = GreenFace.Pieces[1, 0];
            newYellowFace[2, 2] = GreenFace.Pieces[2, 0];

            GreenFace.Pieces[0, 0] = WhiteFace.Pieces[0, 2];
            GreenFace.Pieces[1, 0] = WhiteFace.Pieces[0, 1];
            GreenFace.Pieces[2, 0] = WhiteFace.Pieces[0, 0];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
            OrangeFace.Rotate90Right();
        }

        public void Lprime()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newBlueFace = CopyFace('B');

            char[,] newYellowFace = CopyFace('Y');

            newWhiteFace[0, 0] = GreenFace.Pieces[2, 0];
            newWhiteFace[0, 1] = GreenFace.Pieces[1, 0];
            newWhiteFace[0, 2] = GreenFace.Pieces[0, 0];

            newBlueFace[0, 2] = WhiteFace.Pieces[0, 0];
            newBlueFace[1, 2] = WhiteFace.Pieces[0, 1];
            newBlueFace[2, 2] = WhiteFace.Pieces[0, 2];

            newYellowFace[2, 0] = BlueFace.Pieces[2, 2];
            newYellowFace[2, 1] = BlueFace.Pieces[1, 2];
            newYellowFace[2, 2] = BlueFace.Pieces[0, 2];

            GreenFace.Pieces[0, 0] = YellowFace.Pieces[2, 0];
            GreenFace.Pieces[1, 0] = YellowFace.Pieces[2, 1];
            GreenFace.Pieces[2, 0] = YellowFace.Pieces[2, 2];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
            OrangeFace.Rotate90Left();
        }

        public void F()
        {
            char[,] newWhiteFace = CopyFace('W');

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

            RedFace.Pieces[0, 0] = WhiteFace.Pieces[0, 0];
            RedFace.Pieces[1, 0] = WhiteFace.Pieces[1, 0];
            RedFace.Pieces[2, 0] = WhiteFace.Pieces[2, 0];

            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            GreenFace.Rotate90Right();
        }

        public void Fprime()
        {
            char[,] newWhiteFace = CopyFace('W');

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

            RedFace.Pieces[0, 0] = YellowFace.Pieces[0, 0];
            RedFace.Pieces[1, 0] = YellowFace.Pieces[1, 0];
            RedFace.Pieces[2, 0] = YellowFace.Pieces[2, 0];


            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            GreenFace.Rotate90Left();
        }

        public void B()
        {
            char[,] newWhiteFace = CopyFace('W');

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

            RedFace.Pieces[0, 2] = YellowFace.Pieces[0, 2];
            RedFace.Pieces[1, 2] = YellowFace.Pieces[1, 2];
            RedFace.Pieces[2, 2] = YellowFace.Pieces[2, 2];


            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            BlueFace.Rotate90Right();
        }

        public void Bprime()
        {
            char[,] newWhiteFace = CopyFace('W');

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

            RedFace.Pieces[0, 2] = WhiteFace.Pieces[0, 2];
            RedFace.Pieces[1, 2] = WhiteFace.Pieces[1, 2];
            RedFace.Pieces[2, 2] = WhiteFace.Pieces[2, 2];


            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            BlueFace.Rotate90Left();
        }

        public void F2()
        {
            char[,] newWhiteFace = CopyFace('W');

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

            RedFace.Pieces[0, 0] = OrangeFace.Pieces[2, 2];
            RedFace.Pieces[1, 0] = OrangeFace.Pieces[1, 2];
            RedFace.Pieces[2, 0] = OrangeFace.Pieces[0, 2];

            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
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

            RedFace.Pieces[0, 2] = OrangeFace.Pieces[2, 0];
            RedFace.Pieces[1, 2] = OrangeFace.Pieces[1, 0];
            RedFace.Pieces[2, 2] = OrangeFace.Pieces[0, 0];

            WhiteFace.Pieces = newWhiteFace;
            OrangeFace.Pieces = newOrangeFace;
            YellowFace.Pieces = newYellowFace;
            BlueFace.Rotate180();
        }
        public void L2()
        {
            char[,] newWhiteFace = CopyFace('W');

            char[,] newBlueFace = CopyFace('B');

            char[,] newYellowFace = CopyFace('Y');

            newWhiteFace[0, 0] = YellowFace.Pieces[2, 2];
            newWhiteFace[0, 1] = YellowFace.Pieces[2, 1];
            newWhiteFace[0, 2] = YellowFace.Pieces[2, 0];

            newBlueFace[0, 2] = GreenFace.Pieces[2, 0];
            newBlueFace[1, 2] = GreenFace.Pieces[1, 0];
            newBlueFace[2, 2] = GreenFace.Pieces[0, 0];

            newYellowFace[2, 0] = WhiteFace.Pieces[0, 2];
            newYellowFace[2, 1] = WhiteFace.Pieces[0, 1];
            newYellowFace[2, 2] = WhiteFace.Pieces[0, 0];

            GreenFace.Pieces[0, 0] = BlueFace.Pieces[2, 2];
            GreenFace.Pieces[1, 0] = BlueFace.Pieces[1, 2];
            GreenFace.Pieces[2, 0] = BlueFace.Pieces[0, 2];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
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

            newWhiteFace[2, 0] = YellowFace.Pieces[0, 2];
            newWhiteFace[2, 1] = YellowFace.Pieces[0, 1];
            newWhiteFace[2, 2] = YellowFace.Pieces[0, 0];

            newBlueFace[0, 0] = GreenFace.Pieces[2, 2];
            newBlueFace[1, 0] = GreenFace.Pieces[1, 2];
            newBlueFace[2, 0] = GreenFace.Pieces[0, 2];

            newYellowFace[0, 0] = WhiteFace.Pieces[2, 2];
            newYellowFace[0, 1] = WhiteFace.Pieces[2, 1];
            newYellowFace[0, 2] = WhiteFace.Pieces[2, 0];

            GreenFace.Pieces[0, 2] = BlueFace.Pieces[2, 0];
            GreenFace.Pieces[1, 2] = BlueFace.Pieces[1, 0];
            GreenFace.Pieces[2, 2] = BlueFace.Pieces[0, 0];


            WhiteFace.Pieces = newWhiteFace;
            BlueFace.Pieces = newBlueFace;
            YellowFace.Pieces = newYellowFace;
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
            return new Cube([WhiteFace.Clone(), YellowFace.Clone(), RedFace.Clone(), GreenFace.Clone(), BlueFace.Clone(), OrangeFace.Clone()]);
        }

        public void Shuffle(int n = 20, int? seed = null)
        {
            Random rnd = seed == null ? new Random() : new(seed.GetValueOrDefault());
            for (int i = 0; i < n; i++)
            {
                byte randInt = (byte)rnd.Next(0, 18);
                DoMove(randInt);
            }
        }
        public IEnumerable<byte> Scramble(int n = 20, int? seed = null)
        {
            Random rnd = seed == null ? new Random() : new(seed.GetValueOrDefault());
            List<byte> randPath = new(n);
            for (int i = 0; i < n; i++)
            {
                byte randInt = (byte)rnd.Next(0, 18);
                randPath.Add(randInt);
                DoMove(randInt);
            }
            return randPath;
        }

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
                    bool isContained = listDico.Any(item => item.ContainsKey(str));
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
            Dictionary<(ulong, ulong), byte> newCubes = [];
            foreach (var c1 in listDico[^1].Select(cube => new Cube(DecompressState(cube.Key))))
            {
                for (byte j = 0; j < 18; j++)
                {
                    c1.DoMove(j);
                    (ulong, ulong) intCube = CompressState(c1.ToString());
                    bool isContained = listDico.Any(item => item.ContainsKey(intCube));
                    if (!isContained)
                        newCubes.TryAdd(intCube, j);
                    if (j != 17)
                        c1.DoMove(Move.GetReversalMove(j));
                }
            }
            if (heuristique && listDico.Count >= 6)
            {
                Dictionary<(ulong, ulong), byte> sortedCubes = newCubes.OrderBy(x => new Cube(DecompressState(x.Key)).Conflicts()).Take(100000).ToDictionary();
                listDico.Add(sortedCubes);
            }
            else
            {
                listDico.Add(newCubes);
            }
        }

        public static void NextTreeBranchForMITM(List<Dictionary<string, (byte, byte)>> listDico, bool heuristique)
        {
            Dictionary<string, (byte, byte)> newCubes = [];
            foreach (var c1 in listDico[^1].Select(cube => new Cube(cube.Key)))
            {
                for (byte j = 0; j < 18; j++)
                {
                    c1.DoMove(j);
                    string c1String = c1.ToString();
                    bool isContained = listDico.Any(item => item.ContainsKey(c1String));
                    if (!isContained)
                        newCubes.TryAdd(c1String, (j, c1.Conflicts()));
                    if (j != 17)
                        c1.DoMove(Move.GetReversalMove(j));
                }
            }
            if (heuristique && listDico.Count >= 5)
            {
                IEnumerable<KeyValuePair<string, (byte, byte)>> ordered = newCubes.OrderBy(x => x.Value.Item2).Take(100000);
                listDico.Add(ordered.ToDictionary());
            }
            else
            {
                listDico.Add(newCubes);
            }
        }

        public static void NextTreeBranchForKociemba(List<Dictionary<(ulong, ulong), byte>> listDico)
        {
            List<byte> allowedMoves = 
                Move.GetAlgoFromStringEnum(
                    [Up, Down, Right, Left, Front, Back, 
                    Move.GetStringPath([Move.GetReversalMove(Move.GetAlgoFromStringEnum([Up]).First())]), 
                    Move.GetStringPath([Move.GetReversalMove(Move.GetAlgoFromStringEnum([Down]).First())])]);
            Dictionary<(ulong, ulong), byte> newCubes = [];
            foreach (KeyValuePair<(ulong, ulong), byte> cube in listDico[^1])
            {
                Cube c1 = new(DecompressState(cube.Key));
                foreach (byte j in allowedMoves)
                {
                    c1.DoMove(j);
                    string c1String = c1.ToString();
                    (ulong, ulong) compressedCube = CompressState(c1String);
                    bool isContained = listDico.Any(item => item.ContainsKey(compressedCube));
                    if (!isContained)
                        newCubes.TryAdd(compressedCube, j);
                    if (j != allowedMoves.Last())
                        c1.DoMove(Move.GetReversalMove(j));
                }
            }
            listDico.Add(newCubes);
        }

        public static void NextTreeBranchForPhase1Compressed(List<Dictionary<(ulong, ulong), (byte, byte)>> listDico, bool heuristique)
        {
            Dictionary<(ulong, ulong), (byte, byte)> newCubes = [];
            foreach (var c1 in listDico[^1].Select(cube => new Cube(DecompressState(cube.Key))))
            {
                for (byte j = 0; j < 18; j++)
                {
                    c1.DoMove(j);
                    (ulong, ulong) c1Compressed = CompressState(c1.ToString());
                    bool isContained = listDico.Any(item => item.ContainsKey(c1Compressed));
                    if (!isContained)
                        newCubes.TryAdd(c1Compressed, (j, c1.EdgeConflicts()));
                    if (j != 17)
                        c1.DoMove(Move.GetReversalMove(j));
                }
            }
            if (heuristique && listDico.Count >= 6)
            {
                IEnumerable<KeyValuePair<(ulong, ulong), (byte, byte)>> ordered = newCubes.OrderBy(x => x.Value.Item2).Take(100000);
                listDico.Add(ordered.ToDictionary());
            }
            else
            {
                listDico.Add(newCubes);
            }
        }

        public static void NextTreeBranchForPhase1Compressed(List<Dictionary<(ulong, ulong), (byte, byte)>> listDico, bool heuristique, List<byte> allowedMoves)
        {
            Dictionary<(ulong, ulong), (byte, byte)> newCubes = [];
            foreach (KeyValuePair<(ulong, ulong), (byte, byte)> cube in listDico[^1])
            {
                Cube c1 = new(DecompressState(cube.Key));
                foreach (byte j in allowedMoves)
                {
                    c1.DoMove(j);
                    (ulong, ulong) c1Compressed = CompressState(c1.ToString());
                    bool isContained = listDico.Any(item => item.ContainsKey(c1Compressed));
                    if (!isContained)
                        newCubes.TryAdd(c1Compressed, (j, c1.Conflicts()));
                    if (j != allowedMoves.Last())
                        c1.DoMove(Move.GetReversalMove(j));
                }
            }
            if (heuristique && listDico.Count >= 7)
            {
                IEnumerable<KeyValuePair<(ulong, ulong), (byte, byte)>> ordered = newCubes.OrderBy(x => x.Value.Item2).Take(100000);
                listDico.Add(ordered.ToDictionary());
            }
            else
            {
                listDico.Add(newCubes);
            }
        }

        private static (byte[], bool) EvaluateTask(Cube nc, Cube emptyCube, List<Dictionary<string, (byte, byte)>> arbre, string state)
        {
            List<byte> path = [];
            Cube secu = nc.Clone();
            byte[] sol = secu.IsInDominoGroup() ? MeetInTheMiddleForKociemba(nc) : MeetInTheMiddle(nc, emptyCube, 6);
            secu.ExecuterAlgorithme(sol);
            if (!secu.IsSolved)
            {
                return ([], false);
            }

            foreach (Dictionary<string, (byte move, byte eval)> layer in arbre.Skip(1).Reverse())
            {
                if (!layer.TryGetValue(state, out (byte, byte) value))
                {
                    continue;
                }

                path.Add(value.Item1);
                nc.DoMove(Move.GetReversalMove(value.Item1));
                state = nc.ToString();
            }
            return (path.Reverse<byte>().Concat(sol).ToArray(), true);
        }

        public static byte[] BFSWithMITM(Cube initialCube)
        {
            Cube emptyCube = new();
            Dictionary<string, (byte, byte)> dico = new()
            {
                { initialCube.ToString(), (255, initialCube.Conflicts())}
            };
            List<Dictionary<string, (byte, byte)>> arbre = [dico];
            byte minEval = byte.MaxValue;
            while (true)
            {
                List<Task<(byte[], bool)>> tasks = [];
                foreach (KeyValuePair<string, (byte move, byte eval)> element in arbre[^1])
                {
                    byte eval = element.Value.eval;
                    if (eval < minEval)
                    {
                        minEval = eval;
                    }

                    if (eval > 4)
                    {
                        continue;
                    }

                    string state = element.Key;
                    Cube nc = new(state);
                    tasks.Add(new Task<(byte[], bool)>(() => EvaluateTask(nc.Clone(), emptyCube, arbre, state)));
                }
                while (tasks.Count > 0)
                {
                    Task[] newTasks = tasks.Take(4).ToArray<Task>();
                    foreach(var t in newTasks)
                    {
                        t.Start();
                    }
                    Task.WaitAll(newTasks);
                    foreach (Task task1 in newTasks)
                    {
                        var task = (Task<(byte[], bool)>)task1;
                        if (task.Result.Item2)
                        {
                            return task.Result.Item1;
                        }
                    }
                    tasks = tasks.Skip(4).ToList();
                }
                NextTreeBranchForMITM(arbre, true);
            }
        }

        public byte[] Kociemba()
        {
            List<byte> path = [];
            Cube secu = Clone();
            Dictionary<(ulong, ulong), (byte, byte)> dico = new()
            {
                { CompressState(ToString()), (255, 255)}
            };
            List<Dictionary<(ulong, ulong), (byte, byte)>> arbre = [dico];
            while (true)
            {
                foreach (KeyValuePair<(ulong, ulong), (byte, byte)> element in arbre[^1])
                {
                    Cube nc = new(DecompressState(element.Key));
                    if (!nc.EdgeReduction())
                    {
                        continue;
                    }

                    string state = nc.ToString();
                    (ulong, ulong) compressedState = CompressState(state);
                    foreach (Dictionary<(ulong, ulong), (byte, byte)> layer in arbre.Skip(1).Reverse())
                    {
                        if (!layer.TryGetValue(compressedState, out (byte, byte) value))
                        {
                            continue;
                        }

                        path.Add(value.Item1);
                        nc.DoMove(Move.GetReversalMove(value.Item1));
                        state = nc.ToString();
                        compressedState = CompressState(state);
                    }
                    path.Reverse();
                    secu.ExecuterAlgorithme(path);
                    dico = new()
                    {
                        { CompressState(secu.ToString()), (255, 255)}
                    };
                    arbre = [dico];

                    while (true)
                    {
                        foreach (var nc2 in arbre[^1].Select(element2 => new Cube(DecompressState(element2.Key))).Where(nc2 => nc2.IsInDominoGroup()))
                        {
                            state = nc2.ToString();
                            compressedState = CompressState(state);
                            List<byte> path2 = [];
                            foreach (Dictionary<(ulong, ulong), (byte, byte)> layer in arbre.Skip(1).Reverse())
                            {
                                if (!layer.TryGetValue(compressedState, out (byte, byte) value))
                                {
                                    continue;
                                }

                                path2.Add(value.Item1);
                                nc2.DoMove(Move.GetReversalMove(value.Item1));
                                state = nc2.ToString();
                                compressedState = CompressState(state);
                            }
                            path2.Reverse();
                            secu.ExecuterAlgorithme(path2);
                            byte[] phase2 = MeetInTheMiddleForKociemba(secu);
                            List<byte> solution = path.Concat(path2).Concat(phase2).ToList();
                            List<byte> optimisedSolution = solution;
                            int l;
                            int k = 0;
                            do
                            {
                                l = optimisedSolution.Count;
                                optimisedSolution = LightOptimization(optimisedSolution);
                                k++;
                                if (k == 1000) return optimisedSolution.ToArray();
                            } while (l != optimisedSolution.Count);
                            return optimisedSolution.ToArray();
                        }

                        NextTreeBranchForPhase1Compressed(arbre, true, Move.GetAlgoFromStringEnum(
                        [Up, Down, Right[0].ToString(), Left[0].ToString(), Front, Back,
                            Move.GetStringPath([Move.GetReversalMove(Move.GetAlgoFromStringEnum([Up]).First())]),
                            Move.GetStringPath([Move.GetReversalMove(Move.GetAlgoFromStringEnum([Down]).First())]),
                            Move.GetStringPath([Move.GetReversalMove(Move.GetAlgoFromStringEnum([Right[0].ToString()]).First())]),
                            Move.GetStringPath([Move.GetReversalMove(Move.GetAlgoFromStringEnum([Left[0].ToString()]).First())])]));
                    }
                }
                NextTreeBranchForPhase1Compressed(arbre, true);
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
                if (i % 9 == 4)
                {
                    continue;
                }

                fst *= 6;
                fst += state[i] switch
                {
                    'W' => (ulong) 0,
                    'Y' => 1,
                    'R' => 2,
                    'G' => 3,
                    'B' => 4,
                    _ => 5
                };
            }

            for (int i = 27; i < 54; i++)
            {
                if (i % 9 == 4)
                {
                    continue;
                }

                snd *= 6;
                snd += state[i] switch
                {
                    'W' => (ulong) 0,
                    'Y' => 1,
                    'R' => 2,
                    'G' => 3,
                    'B' => 4,
                    _ => 5
                };
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

        public static byte[] MeetInTheMiddle(Cube initialCube, Cube? finalCube = null, int? deepMax = null, bool heuristique = false)
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
            byte[] solution = [];
            while (!isSolved)
            {
                i++;
                if (i == deepMax) return [];
                Parallel.Invoke(
                    () => NextTreeBranchForMITM(arbreInitial, heuristique),
                    () => NextTreeBranchForMITM(arbreFinal, heuristique)
                    );
                IEnumerable<(ulong, ulong)> takedLastInitial = arbreInitial.TakeLast(2).SelectMany(x => x.Keys);
                IEnumerable<(ulong, ulong)> takedLastFinal = arbreFinal.TakeLast(2).SelectMany(x => x.Keys);
                List<(ulong, ulong)> hasCommonElements = takedLastInitial
                    .Intersect(takedLastFinal).ToList();
                if (hasCommonElements.Count <= 0)
                {
                    continue;
                }

                isSolved = true;
                int min = int.MaxValue;
                foreach ((ulong, ulong) element in hasCommonElements)
                {
                    List<byte> path = [];
                    Cube newCube = new(DecompressState(element));
                    for (int j = 1; j <= arbreFinal.Count; j++)
                    {
                        (ulong, ulong) elementEtapeAvant = CompressState(newCube.ToString());
                        if (!arbreFinal[^j].TryGetValue(elementEtapeAvant, out byte value))
                        {
                            continue;
                        }

                        if (value == 255) break;
                        path.Add(value);
                        newCube.ExecuterAlgorithme(Move.GetReversalPath(path.TakeLast(1)));
                    }
                    List<byte> path2 = [];
                    Cube newCube2 = new(DecompressState(element));
                    for (int j = 1; j <= arbreInitial.Count; j++)
                    {
                        (ulong, ulong) elementEtapeAvant = CompressState(newCube2.ToString());
                        if (!arbreInitial[^j].TryGetValue(elementEtapeAvant, out byte value))
                        {
                            continue;
                        }

                        if (value == 255) break;
                        path2.Add(value);
                        newCube2.ExecuterAlgorithme(Move.GetReversalPath(path2.TakeLast(1)));
                    }
                    List<byte> solutionFromRandom = Move.GetReversalPath(path.Reverse<byte>()).Concat(path2).ToList();
                    int count = solutionFromRandom.Count;
                    if (count >= min)
                    {
                        continue;
                    }

                    solution = solutionFromRandom.Reverse<byte>().ToArray();
                    min = count;
                }
            }
            return solution;
        }

        public static byte[] MeetInTheMiddleForKociemba(Cube initialCube, Cube? finalCube = null, int? deepMax = null)
        {
            finalCube ??= new();
            bool isSolved = false;
            int i = 0;
            Cube secu = initialCube.Clone();
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
            byte[] solution = [];
            while (!isSolved)
            {
                i++;
                if (i == deepMax) return [];
                Parallel.Invoke(
                    () => NextTreeBranchForKociemba(arbreInitial),
                    () => NextTreeBranchForKociemba(arbreFinal)
                    );
                IEnumerable<(ulong, ulong)> takedLastInitial = arbreInitial.TakeLast(2).SelectMany(x => x.Keys);
                IEnumerable<(ulong, ulong)> takedLastFinal = arbreFinal.TakeLast(2).SelectMany(x => x.Keys);
                List<(ulong, ulong)> hasCommonElements = takedLastInitial
                    .Intersect(takedLastFinal).ToList();
                if (hasCommonElements.Count <= 0)
                {
                    continue;
                }

                isSolved = true;
                int min = int.MaxValue;
                foreach ((ulong, ulong) element in hasCommonElements)
                {
                    List<byte> path = [];
                    Cube newCube = new(DecompressState(element));
                    for (int j = 1; j <= arbreFinal.Count; j++)
                    {
                        (ulong, ulong) elementEtapeAvant = CompressState(newCube.ToString());
                        if (arbreFinal[^j].TryGetValue(elementEtapeAvant, out byte value))
                        {
                            if (value == 255) break;
                            path.Add(value);
                            newCube.ExecuterAlgorithme(Move.GetReversalPath(path.TakeLast(1)));
                        }
                    }
                    List<byte> path2 = [];
                    Cube newCube2 = new(DecompressState(element));
                    for (int j = 1; j <= arbreInitial.Count; j++)
                    {
                        (ulong, ulong) elementEtapeAvant = CompressState(newCube2.ToString());
                        if (!arbreInitial[^j].TryGetValue(elementEtapeAvant, out byte value))
                        {
                            continue;
                        }

                        if (value == 255) break;
                        path2.Add(value);
                        newCube2.ExecuterAlgorithme(Move.GetReversalPath(path2.TakeLast(1)));
                    }
                    List<byte> solutionFromRandom = Move.GetReversalPath(path.Reverse<byte>()).Concat(path2).ToList();
                    int count = solutionFromRandom.Count;
                    if (count >= min)
                    {
                        continue;
                    }

                    solution = solutionFromRandom.Reverse<byte>().ToArray();
                    min = count;
                }
            }
            secu.ExecuterAlgorithme(solution);
            return solution;
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
                    if (path[i] < 12)
                    {
                        newPath.Add(Move.GetDoubleMove(path[i]));
                    }
                    i++;
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
            newPath.Add(path[^1]);
            if (newPath.Count <= 1)
            {
                return newPath;
            }

            if (newPath[^1] == Move.GetReversalMove(newPath[^2]))
            {
                return newPath.SkipLast(2).ToList();
            }
            if (newPath[^1] == newPath[^2] && newPath[^1] < 12)
            {
                newPath[^2] = Move.GetDoubleMove(newPath[^2]);
                return newPath.SkipLast(1).ToList();
            }
            if (newPath[^2] < 12 && newPath[^1] == Move.GetDoubleMove(newPath[^2]))
            {
                newPath[^2] = Move.GetReversalMove(newPath[^2]);
                return newPath.SkipLast(1).ToList();
            }

            if (newPath[^1] >= 12 || Move.GetDoubleMove(newPath[^1]) != newPath[^2])
            {
                return newPath;
            }

            newPath[^2] = Move.GetReversalMove(newPath[^1]);
            return newPath.SkipLast(1).ToList();
        }

        public static int Periodicity(IEnumerable<string> algorithm)
        {
            int cpt = 0;
            Cube solvedCube = new();
            List<string> algo = algorithm.ToList();
            do
            {
                solvedCube.ExecuterAlgorithme(algo);
                cpt++;
            } while (!solvedCube.IsSolved);
            return cpt;
        }

        public static int Periodicity(IEnumerable<byte> algorithm)
        {
            int cpt = 0;
            Cube solvedCube = new();
            List<byte> algo = algorithm.ToList();
            do
            {
                solvedCube.ExecuterAlgorithme(algo);
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
            bool firstEdge(Cube cube) => cube.WhiteFace.Pieces[2, 1] == 'W' && cube.RedFace.Pieces[0, 1] == 'R';
            bool secondEdge(Cube cube) => cube.WhiteFace.Pieces[1, 2] == 'W' && cube.BlueFace.Pieces[0, 1] == 'B';
            bool thirdEdge(Cube cube) => cube.WhiteFace.Pieces[1, 0] == 'W' && cube.GreenFace.Pieces[0, 1] == 'G';
            bool fourthEdge(Cube cube) => cube.WhiteFace.Pieces[0, 1] == 'W' && cube.OrangeFace.Pieces[0, 1] == 'O';
            bool oneEdgeIsPlaced(Cube cube) => firstEdge(cube) || secondEdge(cube) || thirdEdge(cube) || fourthEdge(cube);
            bool isPlaced = oneEdgeIsPlaced(c);
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, oneEdgeIsPlaced);
                if (newC is null)
                {
                    continue;
                }

                c = newC.Value.Item1;
                path.AddRange(newC.Value.Item2.Skip(1));
                isPlaced = true;
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
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, cube => (firstEdge(cube) && secondEdge(cube))
                        || (firstEdge(cube) && thirdEdge(cube))
                        || (firstEdge(cube) && fourthEdge(cube))
                        || (secondEdge(cube) && thirdEdge(cube))
                        || (thirdEdge(cube) && fourthEdge(cube))
                        || (secondEdge(cube) && fourthEdge(cube)));
                if (newC == null)
                {
                    continue;
                }

                c = newC.Value.Item1;
                path.AddRange(newC.Value.Item2.Skip(1));
                isPlaced = true;
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
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, cube => (firstEdge(cube) && secondEdge(cube) && thirdEdge(cube))
                        || (firstEdge(cube) && thirdEdge(cube) && fourthEdge(cube))
                        || (firstEdge(cube) && fourthEdge(cube) && secondEdge(cube))
                        || (secondEdge(cube) && thirdEdge(cube) && fourthEdge(cube)));
                if (newC is null)
                {
                    continue;
                }

                c = newC.Value.Item1;
                path.AddRange(newC.Value.Item2.Skip(1));
                isPlaced = true;
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
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, cube => firstEdge(cube) && secondEdge(cube) && thirdEdge(cube) && fourthEdge(cube));
                if (newC is null)
                {
                    continue;
                }

                c = newC.Value.Item1;
                path.AddRange(newC.Value.Item2.Skip(1));
                isPlaced = true;
            }
            return c;
        }

        private static Cube PlaceCorners(Cube c, List<byte> path)
        {
            bool crossAndEdges(Cube cube) => cube.WhiteFace.Pieces[0, 1] == 'W' && cube.WhiteFace.Pieces[1, 0] == 'W'
            && cube.WhiteFace.Pieces[1, 2] == 'W' && cube.WhiteFace.Pieces[2, 1] == 'W'
            && cube.RedFace.Pieces[0, 1] == 'R' && cube.BlueFace.Pieces[0, 1] == 'B'
            && cube.GreenFace.Pieces[0, 1] == 'G' && cube.OrangeFace.Pieces[0, 1] == 'O';

            bool firstCornerIsPlaced(Cube cube) =>
                (cube.BlueFace.Pieces[0, 0] == 'B' && cube.WhiteFace.Pieces[2, 2] == 'W' && cube.RedFace.Pieces[0, 2] == 'R')
                || (cube.BlueFace.Pieces[0, 0] == 'R' && cube.WhiteFace.Pieces[2, 2] == 'B' && cube.RedFace.Pieces[0, 2] == 'W')
                || (cube.BlueFace.Pieces[0, 0] == 'W' && cube.WhiteFace.Pieces[2, 2] == 'R' && cube.RedFace.Pieces[0, 2] == 'B');

            bool secondCornerIsPlaced(Cube cube) =>
                (cube.RedFace.Pieces[0, 0] == 'R' && cube.WhiteFace.Pieces[2, 0] == 'W' && cube.GreenFace.Pieces[0, 2] == 'G')
                || (cube.RedFace.Pieces[0, 0] == 'G' && cube.WhiteFace.Pieces[2, 0] == 'R' && cube.GreenFace.Pieces[0, 2] == 'W')
                || (cube.RedFace.Pieces[0, 0] == 'W' && cube.WhiteFace.Pieces[2, 0] == 'G' && cube.GreenFace.Pieces[0, 2] == 'R');

            bool thirdCornerIsPlaced(Cube cube) =>
                (cube.OrangeFace.Pieces[0, 0] == 'O' && cube.BlueFace.Pieces[0, 2] == 'B' && cube.WhiteFace.Pieces[0, 2] == 'W')
                || (cube.OrangeFace.Pieces[0, 0] == 'B' && cube.BlueFace.Pieces[0, 2] == 'W' && cube.WhiteFace.Pieces[0, 2] == 'O')
                || (cube.OrangeFace.Pieces[0, 0] == 'W' && cube.BlueFace.Pieces[0, 2] == 'O' && cube.WhiteFace.Pieces[0, 2] == 'B');

            bool fourthCornerIsPlaced(Cube cube) =>
            (cube.WhiteFace.Pieces[0, 0] == 'W' && cube.GreenFace.Pieces[0, 0] == 'G' && cube.OrangeFace.Pieces[0, 2] == 'O')
            || (cube.WhiteFace.Pieces[0, 0] == 'G' && cube.GreenFace.Pieces[0, 0] == 'O' && cube.OrangeFace.Pieces[0, 2] == 'W')
            || (cube.WhiteFace.Pieces[0, 0] == 'O' && cube.GreenFace.Pieces[0, 0] == 'W' && cube.OrangeFace.Pieces[0, 2] == 'G');

            bool oneCornerIsPlaced(Cube cube) => firstCornerIsPlaced(cube) || secondCornerIsPlaced(cube) || thirdCornerIsPlaced(cube)
                || fourthCornerIsPlaced(cube);
            bool allCornersIsPlaced(Cube cube) => firstCornerIsPlaced(cube) && secondCornerIsPlaced(cube) && thirdCornerIsPlaced(cube)
            && fourthCornerIsPlaced(cube);
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
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, cube => crossAndEdges(cube) && oneCornerIsPlaced(cube));
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
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, cube => crossAndEdges(cube)
                        && ((firstCornerIsPlaced(cube) && secondCornerIsPlaced(cube))
                        || (firstCornerIsPlaced(cube) && thirdCornerIsPlaced(cube))
                        || (firstCornerIsPlaced(cube) && fourthCornerIsPlaced(cube))
                        || (secondCornerIsPlaced(cube) && thirdCornerIsPlaced(cube))
                        || (thirdCornerIsPlaced(cube) && fourthCornerIsPlaced(cube))
                        || (secondCornerIsPlaced(cube) && fourthCornerIsPlaced(cube))));
                if (newC is null)
                {
                    continue;
                }

                c = newC.Value.Item1;
                path.AddRange(newC.Value.Item2.Skip(1));
                isPlaced = true;
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
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, cube => crossAndEdges(cube)
                        && ((firstCornerIsPlaced(cube) && secondCornerIsPlaced(cube) && thirdCornerIsPlaced(cube))
                        || (firstCornerIsPlaced(cube) && thirdCornerIsPlaced(cube) && fourthCornerIsPlaced(cube))
                        || (firstCornerIsPlaced(cube) && fourthCornerIsPlaced(cube) && secondCornerIsPlaced(cube))
                        || (secondCornerIsPlaced(cube) && thirdCornerIsPlaced(cube) && fourthCornerIsPlaced(cube))));
                if (newC is null)
                {
                    continue;
                }

                c = newC.Value.Item1;
                path.AddRange(newC.Value.Item2.Skip(1));
                isPlaced = true;
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
                (Cube, IEnumerable<byte>)? newC = NextTreeBranch(arbre, cube => crossAndEdges(cube)
                        && allCornersIsPlaced(cube));
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
                }
                else if (c.GreenFace.Pieces[0, 2] == 'W')
                {
                    c.ExecuterAlgorithme(doubleInverseSexyMove);
                    path.AddRange(doubleInverseByteAlgo);
                    if (allCornersIsOriented()) break;
                }

                c.Uprime();
                path.AddRange(uPrimeAlgo);
            }
            return c;
        }
        private static Cube SecondLayer(Cube c, List<byte> path)
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
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightRedFace);
                        path.AddRange(secondLayerRightRedFace);
                    }
                    i = 0;
                }

                else if (c.BlueFace.Pieces[2, 1] == 'B' && c.YellowFace.Pieces[1, 2] != 'Y')
                {
                    if (c.YellowFace.Pieces[1, 2] == 'O')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftBlueFace);
                        path.AddRange(secondLayerLeftBlueFace);
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightBlueFace);
                        path.AddRange(secondLayerRightBlueFace);
                    }
                    i = 0;
                }

                else if (c.GreenFace.Pieces[2, 1] == 'G' && c.YellowFace.Pieces[1, 0] != 'Y')
                {
                    if (c.YellowFace.Pieces[1, 0] == 'R')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftGreenFace);
                        path.AddRange(secondLayerLeftGreenFace);
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightGreenFace);
                        path.AddRange(secondLayerRightGreenFace);
                    }
                    i = 0;
                }

                else if (c.OrangeFace.Pieces[2, 1] == 'O' && c.YellowFace.Pieces[2, 1] != 'Y')
                {
                    if (c.YellowFace.Pieces[2, 1] == 'G')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftOrangeFace);
                        path.AddRange(secondLayerLeftOrangeFace);
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightOrangeFace);
                        path.AddRange(secondLayerRightOrangeFace);
                    }
                    i = 0;
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
        private static Cube YellowCross(Cube c, List<byte> path)
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
                if (c.YellowFace.Pieces[0, 1] == 'Y' && c.YellowFace.Pieces[2, 1] == 'Y')
                {
                    c.D();
                    path.AddRange(dAlgo);
                    c.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(algoCrossPattern2);
                    break;
                }
                if (c.YellowFace.Pieces[1, 0] == 'Y' && c.YellowFace.Pieces[1, 2] == 'Y')
                {
                    c.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(algoCrossPattern2);
                    break;
                }
                if (c.YellowFace.Pieces[1, 2] == 'Y' && c.YellowFace.Pieces[2, 1] == 'Y')
                {
                    c.ExecuterAlgorithme(algoCrossPattern);
                    path.AddRange(algoCrossPattern);
                    break;
                }
                c.D();
                path.AddRange(dAlgo);
            }
            return c;
        }
        private static Cube OrientEdges(Cube c, List<byte> path)
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

                if (c.BlueFace.Pieces[2, 1] == 'B' && c.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c.ExecuterAlgorithme(blueF);
                    path.AddRange(blueF);
                    break;
                }
                if (c.GreenFace.Pieces[2, 1] == 'G' && c.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c.ExecuterAlgorithme(greenF);
                    path.AddRange(greenF);
                    break;
                }
                if (c.GreenFace.Pieces[2, 1] == 'G' && c.RedFace.Pieces[2, 1] == 'R')
                {
                    c.ExecuterAlgorithme(greenF2);
                    path.AddRange(greenF2);
                    break;
                }
                if (c.RedFace.Pieces[2, 1] == 'R' && c.OrangeFace.Pieces[2, 1] == 'O')
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
                    }
                    else
                    {
                        c.ExecuterAlgorithme(cornerAlignementOptim);
                        path.AddRange(cornerAlignementOptim);
                    }

                    break;
                }

                if ((c.RedFace.Pieces[2, 0] == 'R' && c.GreenFace.Pieces[2, 2] == 'G')
                    || (c.RedFace.Pieces[2, 0] == 'G' && c.GreenFace.Pieces[2, 2] == 'Y' && c.YellowFace.Pieces[0, 0] == 'R')
                    || (c.RedFace.Pieces[2, 0] == 'Y' && c.GreenFace.Pieces[2, 2] == 'R' && c.YellowFace.Pieces[0, 0] == 'G'))
                {
                    if (c.RedFace.Pieces[2, 2] == 'R' || c.RedFace.Pieces[2, 2] == 'B' ||
                        c.BlueFace.Pieces[2, 0] == 'B' || c.BlueFace.Pieces[2, 0] == 'R' ||
                        c.YellowFace.Pieces[0, 2] == 'R' || c.YellowFace.Pieces[0, 2] == 'B')
                    {
                        c.ExecuterAlgorithme(cornerAlignementAlgo2);
                        path.AddRange(cornerAlignementAlgo2);
                    }
                    else
                    {
                        c.ExecuterAlgorithme(cornerAlignementOptim2);
                        path.AddRange(cornerAlignementOptim2);
                    }

                    break;
                }
                if ((c.BlueFace.Pieces[2, 2] == 'B' && c.OrangeFace.Pieces[2, 0] == 'O')
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

                    c.ExecuterAlgorithme(cornerAlignementOptim3);
                    path.AddRange(cornerAlignementOptim3);
                    break;
                }

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
                }
                else if (c.RedFace.Pieces[2, 0] == 'Y')
                {
                    c.ExecuterAlgorithme(doubleSM);
                    path.AddRange(doubleByteAlgo);
                }

                c.Dprime();
                path.AddRange(dPrimeAlgo);
            }
        }
        public static List<byte> FastBeginnerMethod(Cube c)
        {
            List<byte> path = [];
            c = PlaceEdges(c, path);
            c = PlaceCorners(c, path);
            c = OrientCorners(c, path);
            c = SecondLayer(c, path);
            c = YellowCross(c, path);
            c = OrientEdges(c, path);
            c = PlaceSecondCorners(c, path);
            OrientLastCornersOptim(c, path);
            return path;
        }

        private byte EdgeConflicts()
        {
            byte conflicts = 0;
            if (WhiteFace.Pieces[0, 1] != 'W' || OrangeFace.Pieces[0, 1] != 'O')
                conflicts++;
            if (WhiteFace.Pieces[1, 0] != 'W' || GreenFace.Pieces[0, 1] != 'G')
                conflicts++;
            if (WhiteFace.Pieces[1, 2] != 'W' || BlueFace.Pieces[0, 1] != 'B')
                conflicts++;
            if (WhiteFace.Pieces[2, 1] != 'W' || RedFace.Pieces[0, 1] != 'R')
                conflicts++;
            if (RedFace.Pieces[1, 0] != 'R' || GreenFace.Pieces[1, 2] != 'G')
                conflicts++;
            if (RedFace.Pieces[1, 2] != 'R' || BlueFace.Pieces[1, 0] != 'B')
                conflicts++;
            if (RedFace.Pieces[2, 1] != 'R' || YellowFace.Pieces[0, 1] != 'Y')
                conflicts++;
            if (BlueFace.Pieces[2, 1] != 'B' || YellowFace.Pieces[1, 2] != 'Y')
                conflicts++;
            if (BlueFace.Pieces[1, 2] != 'B' || OrangeFace.Pieces[1, 0] != 'O')
                conflicts++;
            if (OrangeFace.Pieces[1, 2] != 'O' || GreenFace.Pieces[1, 0] != 'G')
                conflicts++;
            if (OrangeFace.Pieces[2, 1] != 'O' || YellowFace.Pieces[2, 1] != 'Y')
                conflicts++;
            if (GreenFace.Pieces[2, 1] != 'G' || YellowFace.Pieces[1, 0] != 'Y')
                conflicts++;
            return conflicts;
        }

        private byte CornersConflicts()
        {
            byte conflicts = 0;
            if (RedFace.Pieces[0, 0] != 'R' || GreenFace.Pieces[0, 2] != 'G' || WhiteFace.Pieces[2, 0] != 'W')
                conflicts++;
            if (RedFace.Pieces[0, 2] != 'R' || BlueFace.Pieces[0, 0] != 'B' || WhiteFace.Pieces[2, 2] != 'W')
                conflicts++;
            if (BlueFace.Pieces[0, 2] != 'B' || OrangeFace.Pieces[0, 0] != 'O' || WhiteFace.Pieces[0, 2] != 'W')
                conflicts++;
            if (OrangeFace.Pieces[0, 2] != 'O' || GreenFace.Pieces[0, 0] != 'G' || WhiteFace.Pieces[0, 0] != 'W')
                conflicts++;
            if (RedFace.Pieces[2, 0] != 'R' || GreenFace.Pieces[2, 2] != 'G' || YellowFace.Pieces[0, 0] != 'Y')
                conflicts++;
            if (RedFace.Pieces[2, 2] != 'R' || BlueFace.Pieces[2, 0] != 'B' || YellowFace.Pieces[0, 2] != 'Y')
                conflicts++;
            if (BlueFace.Pieces[2, 2] != 'B' || OrangeFace.Pieces[2, 0] != 'O' || YellowFace.Pieces[2, 2] != 'Y')
                conflicts++;
            if (OrangeFace.Pieces[2, 2] != 'O' || GreenFace.Pieces[2, 0] != 'G' || YellowFace.Pieces[2, 0] != 'Y')
                conflicts++;
            return conflicts;
        }

        public byte Conflicts()
        {
            return (byte)(CornersConflicts()+EdgeConflicts());
        }

        public bool IsInDominoGroup()
        {
            return EdgeReduction() && CornerReduction();
        }

        public bool SecondLayerEdgeReductionHorizontal()
        {
            return (OrangeFace.Pieces[1, 0] == OrangeFace.ColorFace || OrangeFace.Pieces[1, 0] == GetOppositeColor(OrangeFace.ColorFace))
                   && (OrangeFace.Pieces[1, 2] == OrangeFace.ColorFace || OrangeFace.Pieces[1, 2] == GetOppositeColor(OrangeFace.ColorFace))
                   && (RedFace.Pieces[1, 0] == RedFace.ColorFace || RedFace.Pieces[1, 0] == GetOppositeColor(RedFace.ColorFace))
                   && (RedFace.Pieces[1, 2] == RedFace.ColorFace || RedFace.Pieces[1, 2] == GetOppositeColor(RedFace.ColorFace))
                   && (BlueFace.Pieces[1, 0] == BlueFace.ColorFace || BlueFace.Pieces[1, 0] == GetOppositeColor(BlueFace.ColorFace))
                   && (BlueFace.Pieces[1, 2] == BlueFace.ColorFace || BlueFace.Pieces[1, 2] == GetOppositeColor(BlueFace.ColorFace))
                   && (GreenFace.Pieces[1, 0] == GreenFace.ColorFace || GreenFace.Pieces[1, 0] == GetOppositeColor(GreenFace.ColorFace))
                   && (GreenFace.Pieces[1, 2] == GreenFace.ColorFace || GreenFace.Pieces[1, 2] == GetOppositeColor(GreenFace.ColorFace));
        }

        public bool SecondLayerEdgeReductionVertical1()
        {
            return (OrangeFace.Pieces[0, 1] == OrangeFace.ColorFace || OrangeFace.Pieces[0, 1] == GetOppositeColor(OrangeFace.ColorFace))
                   && (OrangeFace.Pieces[2, 1] == OrangeFace.ColorFace || OrangeFace.Pieces[2, 1] == GetOppositeColor(OrangeFace.ColorFace))
                   && (RedFace.Pieces[0, 1] == RedFace.ColorFace || RedFace.Pieces[0, 1] == GetOppositeColor(RedFace.ColorFace))
                   && (RedFace.Pieces[2, 1] == RedFace.ColorFace || RedFace.Pieces[2, 1] == GetOppositeColor(RedFace.ColorFace))
                   && (WhiteFace.Pieces[0, 1] == WhiteFace.ColorFace || WhiteFace.Pieces[0, 1] == GetOppositeColor(WhiteFace.ColorFace))
                   && (WhiteFace.Pieces[2, 1] == WhiteFace.ColorFace || WhiteFace.Pieces[2, 1] == GetOppositeColor(WhiteFace.ColorFace))
                   && (YellowFace.Pieces[0, 1] == YellowFace.ColorFace || YellowFace.Pieces[0, 1] == GetOppositeColor(YellowFace.ColorFace))
                   && (YellowFace.Pieces[2, 1] == YellowFace.ColorFace || YellowFace.Pieces[2, 1] == GetOppositeColor(YellowFace.ColorFace));
        }

        public bool SecondLayerEdgeReductionVertical2()
        {
            return (WhiteFace.Pieces[1, 0] == WhiteFace.ColorFace || WhiteFace.Pieces[1, 0] == GetOppositeColor(WhiteFace.ColorFace))
                   && (WhiteFace.Pieces[1, 2] == WhiteFace.ColorFace || WhiteFace.Pieces[1, 2] == GetOppositeColor(WhiteFace.ColorFace))
                   && (YellowFace.Pieces[1, 0] == YellowFace.ColorFace || YellowFace.Pieces[1, 0] == GetOppositeColor(YellowFace.ColorFace))
                   && (YellowFace.Pieces[1, 2] == YellowFace.ColorFace || YellowFace.Pieces[1, 2] == GetOppositeColor(YellowFace.ColorFace))
                   && (GreenFace.Pieces[0, 1] == GreenFace.ColorFace || GreenFace.Pieces[0, 1] == GetOppositeColor(GreenFace.ColorFace))
                   && (GreenFace.Pieces[2, 1] == GreenFace.ColorFace || GreenFace.Pieces[2, 1] == GetOppositeColor(GreenFace.ColorFace))
                   && (BlueFace.Pieces[0, 1] == BlueFace.ColorFace || WhiteFace.Pieces[0, 1] == GetOppositeColor(BlueFace.ColorFace))
                   && (BlueFace.Pieces[2, 1] == BlueFace.ColorFace || BlueFace.Pieces[2, 1] == GetOppositeColor(BlueFace.ColorFace));
        }

        public bool EdgeReduction()
        {
            foreach (Face face in new List<Face>([WhiteFace, YellowFace]))
            {
                foreach ((int, int) p in GetEdges)
                {
                    if (face.Pieces[p.Item1, p.Item2] != face.ColorFace && face.Pieces[p.Item1, p.Item2] != GetOppositeColor(face.ColorFace))
                    {
                        goto breaked1;
                    }
                }
            }
            bool whiteYellow = SecondLayerEdgeReductionHorizontal();
            if (whiteYellow)
            {
                Up = "U";
                Down = "D";
                Right = "R2";
                Left = "L2";
                Front = "F2";
                Back = "B2";
                return true;
            }
            breaked1:
            foreach (Face face in new List<Face>([BlueFace, GreenFace]))
            {
                foreach ((int, int) p in GetEdges)
                {
                    if (face.Pieces[p.Item1, p.Item2] != face.ColorFace && face.Pieces[p.Item1, p.Item2] != GetOppositeColor(face.ColorFace))
                    {
                        goto breaked2;
                    }
                }
            }
            bool blueGreen = SecondLayerEdgeReductionVertical1();
            if (blueGreen)
            {
                Up = "F";
                Down = "B";
                Right = "R2";
                Left = "L2";
                Front = "U2";
                Back = "D2";
                return true;
            }
            breaked2:
            if ((from face in new List<Face>([RedFace, OrangeFace]) from p in GetEdges where face.Pieces[p.Item1, p.Item2] != face.ColorFace && face.Pieces[p.Item1, p.Item2] != GetOppositeColor(face.ColorFace) select face).Any())
            {
                return false;
            }
            bool redOrange = SecondLayerEdgeReductionVertical2();
            if (redOrange)
            {
                Up = "L";
                Down = "R";
                Right = "U2";
                Left = "D2";
                Front = "F2";
                Back = "B2";
                return true;
            }
            return false;
        }

        public bool CornerReduction()
        {
            Face face1 = Up switch
            {
                "U" => WhiteFace,
                "F" => BlueFace,
                "L" => RedFace,
                _ => WhiteFace
            };
            Face face2 = Up switch
            {
                "U" => YellowFace,
                "F" => GreenFace,
                "L" => OrangeFace,
                _ => WhiteFace
            };
            return !(from face in new List<Face>([face1, face2]) from p in GetCorners where face.Pieces[p.Item1, p.Item2] != face.ColorFace && face.Pieces[p.Item1, p.Item2] != GetOppositeColor(face.ColorFace) select face).Any();
        }

        public static char GetOppositeColor(char color)
        {
            return color switch
            {
                'W' => 'Y',
                'G' => 'B',
                'Y' => 'W',
                'B' => 'G',
                'R' => 'O',
                'O' => 'R',
                _  => throw new Exception("Pas de telle couleur")
            };
        }

        public static List<byte> TabuSearch(Cube c)
        {
            Cube init = c.Clone();
            List<byte> path = [];
            List<byte> bestPath = [];
            int evaluation = c.Conflicts();
            int best = evaluation;
            HashSet<string> hs = [c.ToString()];
            Stopwatch sw = new();
            Stopwatch sw2 = new();
            sw.Start();
            sw2.Start();
            while (evaluation > 0)
            {
                evaluation = c.Conflicts();
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
                        int newEval = c.Conflicts();
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
                                bestPath = bestPath.Concat(path).Select(x => x).ToList();
                                path.Clear();
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
                int minEval = int.MaxValue;
                byte move = 0;
                string cs = "";
                for (byte j = 0; j < 18; j++)
                {
                    c.DoMove(j);
                    cs = c.ToString();
                    int eval = c.Conflicts();
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
                    switch (item)
                    {
                        case 'Y':
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write('Y');
                            break;
                        case 'R':
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write('R');
                            break;
                        case 'B':
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write('B');
                            break;
                        case 'W':
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write('W');
                            break;
                        case 'G':
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write('G');
                            break;
                        case 'O':
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write('O');
                            break;
                        default:
                            Console.Write(item);
                            break;
                    }
                }
                Console.WriteLine(' ');
            }
            Console.ResetColor();
        }
    }
}