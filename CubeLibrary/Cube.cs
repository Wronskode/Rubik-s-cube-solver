﻿using System.Collections.Generic;
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

        private static readonly string[] secondLayerLeftRedFace = ["D'", "B'", "D", "B", "D", "R", "D'", "R'"];
        private static readonly string[] secondLayerRightRedFace = ["D", "F", "D'", "F'", "D'", "R'", "D", "R"];

        private static readonly string[] secondLayerLeftBlueFace = ["D'", "L'", "D", "L", "D", "B", "D'", "B'"];
        private static readonly string[] secondLayerRightBlueFace = ["D", "R", "D", "R'", "D'", "B'", "D'", "B"];

        private static readonly string[] secondLayerLeftOrangeFace = ["D'", "F'", "D", "F", "D", "L", "D'", "L'"];
        private static readonly string[] secondLayerRightOrangeFace = ["D", "B", "D'", "B'", "D'", "L'", "D", "L"];

        private static readonly string[] secondLayerLeftGreenFace = ["D'", "R'", "D", "R", "D", "F", "D'", "F'"];
        private static readonly string[] secondLayerRightGreenFace = ["D", "L", "D'", "L'", "D'", "F'", "D", "F"];

        private static readonly string[] algoCrossPattern = ["R", "D", "F", "D'", "F'", "R'"];
        private static readonly string[] algoCrossPattern2 = ["R", "F", "D", "F'", "D'", "R'"];

        private static readonly string[] redF = ["D2", "F", "D", "F'", "D", "F", "D2", "F'", "D'"];
        private static readonly string[] blueF = ["D", "F", "D", "F'", "D", "F", "D2", "F'"];
        private static readonly string[] greenF = ["F", "D", "F'", "D", "F", "D2", "F'", "D"];
        private static readonly string[] greenF2 = ["D'", "F", "D", "F'", "D", "F", "D2", "F'", "D2"];
        private static readonly string[] redF2 = ["F", "D", "F'", "D", "F", "D2", "F'"];
        private static readonly string[] blueF2 = ["D", "F", "D", "F'", "D", "F", "D2", "F'", "D'"];

        private static readonly string[] cornerAlignementAlgo = ["D'", "B'", "D", "F", "D'", "B", "D", "F'"];
        private static readonly string[] cornerAlignementAlgo2 = ["B'", "D", "F", "D'", "B", "D", "F'", "D'"];
        private static readonly string[] cornerAlignementAlgo3 = ["D2", "B'", "D", "F", "D'", "B", "D", "F'", "D"];
        private static readonly string[] cornerAlignementAlgo4 = ["D", "B'", "D", "F", "D'", "B", "D", "F'", "D2"];

        private static readonly string[] cornerAlignementOptim = ["F", "D'", "B'", "D", "F'", "D'", "B", "D"];
        private static readonly string[] cornerAlignementOptim2 = ["L", "D'", "R'", "D", "L'", "D'", "R", "D"];
        private static readonly string[] cornerAlignementOptim3 = ["R", "D'", "L'", "D", "R'", "D'", "L", "D"];
        private static readonly string[] cornerAlignementOptim4 = ["B", "D'", "F'", "D", "B'", "D'", "F", "D"];

        private static readonly string[] sexyMove = ["R", "U", "R'", "U'"];
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

        public static byte[] GetReversalPath(IEnumerable<byte> path)
        {
            byte[] newPath = new byte[path.Count()];
            int i = 0;
            foreach (byte item in path)
            {
                newPath[i] = item switch
                {
                    0 => 6,
                    1 => 7,
                    2 => 8,
                    3 => 9,
                    4 => 10,
                    5 => 11,
                    6 => 0,
                    7 => 1,
                    8 => 2,
                    9 => 3,
                    10 => 4,
                    11 => 5,
                    _ => item,
                };
                i++;
            }
            return newPath;
        }

        public static byte GetReversalMove(byte move)
        {
            //if (move == 0) return 6;
            //else if (move == 1) return 7;
            //else if (move == 2) return 8;
            //else if (move == 3) return 9;
            //else if (move == 4) return 10;
            //else if (move == 5) return 11;
            //else if (move == 6) return 0;
            //else if (move == 7) return 1;
            //else if (move == 8) return 2;
            //else if (move == 9) return 3;
            //else if (move == 10) return 4;
            //else if (move == 11) return 5;
            //else return move;
            return move switch
            {
                <= 5 => (byte)(move + 6),
                <= 11 => (byte)(move - 6),
                _ => move,
            };
        }

        public static (int, int, int, int, int, int) StringCubeToInt(string cubeRepresentation)
        {
            int[] result = new int[6];
            int idx = 0;
            for (int face = 0; face < 6; face++)
            {
                int n = 0;
                for (int i = 0; i < 9; i++)
                {
                    char c = cubeRepresentation[idx++];
                    int code = c switch
                    {
                        'W' => 1,
                        'Y' => 2,
                        'R' => 3,
                        'G' => 4,
                        'B' => 5,
                        _ => 6,
                    };
                    n = (n << 3) | code;
                }
                result[face] = n;
            }
            return (result[0], result[1], result[2], result[3], result[4], result[5]);
        }
        public static void FaceToString(int n, StringBuilder sb)
        {
            const string colors = "*WYRGBO";
            sb.Append(colors[(n >> 24) & 7]);
            sb.Append(colors[(n >> 21) & 7]);
            sb.Append(colors[(n >> 18) & 7]);
            sb.Append(colors[(n >> 15) & 7]);
            sb.Append(colors[(n >> 12) & 7]);
            sb.Append(colors[(n >> 9) & 7]);
            sb.Append(colors[(n >> 6) & 7]);
            sb.Append(colors[(n >> 3) & 7]);
            sb.Append(colors[n & 7]);
        }
        public static string IntToCubeString((int, int, int, int, int, int) intCube)
        {
            StringBuilder sb = new();
            FaceToString(intCube.Item1, sb);
            FaceToString(intCube.Item2, sb);
            FaceToString(intCube.Item3, sb);
            FaceToString(intCube.Item4, sb);
            FaceToString(intCube.Item5, sb);
            FaceToString(intCube.Item6, sb);
            return sb.ToString();
        }

        public static string GetStringPath(IEnumerable<byte> path)
        {
            StringBuilder response = new();
            foreach (byte item in path)
            {
                switch (item)
                {
                    case 0:
                        response.Append('F');
                        break;
                    case 1:
                        response.Append('U');
                        break;
                    case 2:
                        response.Append('B');
                        break;
                    case 3:
                        response.Append('L');
                        break;
                    case 4:
                        response.Append('D');
                        break;
                    case 5:
                        response.Append('R');
                        break;
                    case 6:
                        response.Append("F'");
                        break;
                    case 7:
                        response.Append("U'");
                        break;
                    case 8:
                        response.Append("B'");
                        break;
                    case 9:
                        response.Append("L'");
                        break;
                    case 10:
                        response.Append("D'");
                        break;
                    case 11:
                        response.Append("R'");
                        break;
                    case 12:
                        response.Append("F2");
                        break;
                    case 13:
                        response.Append("U2");
                        break;
                    case 14:
                        response.Append("B2");
                        break;
                    case 15:
                        response.Append("L2");
                        break;
                    case 16:
                        response.Append("D2");
                        break;
                    case 17:
                        response.Append("R2");
                        break;
                }
            }
            return response.ToString();
        }

        public static List<string> StringPathToEnum(string path)
        {
            List<string> newPath = [];
            for (int i = 0; i < path.Length - 1; i++)
            {
                if (path[i + 1] == '\'' || path[i + 1] == '2')
                {
                    newPath.Add(path[i].ToString() + path[i + 1].ToString());
                    if (i == path.Length - 3) newPath.Add(path[i + 2].ToString());
                    i++;
                }
                else
                {
                    newPath.Add(path[i].ToString());
                    if (i == path.Length - 2) newPath.Add(path[i + 1].ToString());
                }
            }
            if (path.Length == 1) newPath.Add(path[0].ToString());
            return newPath;
        }

        // Storing only tuple of int representing each face of the cube
        public static (Cube, IEnumerable<byte>)? OldGetNextBranch(List<Dictionary<(int, int, int, int, int, int), IEnumerable<byte>>> listDico, Func<Cube, bool> f)
        {
            Dictionary<(int, int, int, int, int, int), IEnumerable<byte>> newCubes = [];
            bool isContained;
            foreach (KeyValuePair<(int, int, int, int, int, int), IEnumerable<byte>> cube in listDico[^1])
            {
                Cube c1 = new(IntToCubeString(cube.Key));
                for (byte j = 0; j < 18; j++)
                {
                    c1.DoMove(j);
                    (int, int, int, int, int, int) intCube = StringCubeToInt(c1.ToString());
                    isContained = false;
                    foreach (Dictionary<(int, int, int, int, int, int), IEnumerable<byte>> item in listDico)
                    {
                        if (item.ContainsKey(intCube))
                        {
                            isContained = true;
                            break;
                        }
                    }
                    if (!isContained)
                        newCubes.TryAdd(intCube, cube.Value.Append(j));
                    if (f(c1))
                        return (c1, newCubes[intCube]);
                    if (j != 17)
                        c1.DoMove(GetReversalMove(j));
                }
            }
            listDico.Add(newCubes);
            return null;
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
                        c1.DoMove(GetReversalMove(j));
                }
            }
            listDico.Add(newCubes);
            return null;
        }

        private static int Heuristique(string s)
        {
            var solution = (new Cube()).ToString();
            int k = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (solution[i] == s[i]) k++;
            }
            return k;
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
                    string c1String = c1.ToString();
                    (ulong, ulong) intCube = CompressState(c1String);
                    bool isContained = false;
                    foreach (Dictionary<(ulong, ulong), byte> item in listDico)
                    {
                        if (item.ContainsKey(intCube))
                        {
                            isContained = true;
                            break;
                        }
                    }
                    if (!isContained && (listDico.Count < 6 || !heuristique || Heuristique(c1String) > 15+(listDico.Count-5)*3))
                        newCubes.TryAdd(intCube, j);
                    if (j != 17)
                        c1.DoMove(GetReversalMove(j));
                }
            }
            listDico.Add(newCubes);
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

        public static List<byte> GetAlgoFromStringEnum(IEnumerable<string> algorithme)
        {
            List<byte> data = [];
            foreach (string item in algorithme)
            {
                switch (item)
                {
                    case "F":
                        data.Add(0);
                        break;
                    case "U":
                        data.Add(1);
                        break;
                    case "B":
                        data.Add(2);
                        break;
                    case "L":
                        data.Add(3);
                        break;
                    case "D":
                        data.Add(4);
                        break;
                    case "R":
                        data.Add(5);
                        break;
                    case "F'":
                        data.Add(6);
                        break;
                    case "U'":
                        data.Add(7);
                        break;
                    case "B'":
                        data.Add(8);
                        break;
                    case "L'":
                        data.Add(9);
                        break;
                    case "D'":
                        data.Add(10);
                        break;
                    case "R'":
                        data.Add(11);
                        break;
                    case "F2":
                        data.Add(12);
                        break;
                    case "U2":
                        data.Add(13);
                        break;
                    case "B2":
                        data.Add(14);
                        break;
                    case "L2":
                        data.Add(15);
                        break;
                    case "D2":
                        data.Add(16);
                        break;
                    case "R2":
                        data.Add(17);
                        break;
                    default:
                        throw new Exception("Le mouvement n'existe pas " + item);
                }
            }
            return data;
        }

        public static string[] GetListStringAlgoFromByte(IEnumerable<byte> algorithme)
        {
            List<string> data = [];
            foreach (byte item in algorithme)
            {
                switch (item)
                {
                    case 0:
                        data.Add("F");
                        break;
                    case 1:
                        data.Add("U");
                        break;
                    case 2:
                        data.Add("B");
                        break;
                    case 3:
                        data.Add("L");
                        break;
                    case 4:
                        data.Add("D");
                        break;
                    case 5:
                        data.Add("R");
                        break;
                    case 6:
                        data.Add("F'");
                        break;
                    case 7:
                        data.Add("U'");
                        break;
                    case 8:
                        data.Add("B'");
                        break;
                    case 9:
                        data.Add("L'");
                        break;
                    case 10:
                        data.Add("D'");
                        break;
                    case 11:
                        data.Add("R'");
                        break;
                    case 12:
                        data.Add("F2");
                        break;
                    case 13:
                        data.Add("U2");
                        break;
                    case 14:
                        data.Add("B2");
                        break;
                    case 15:
                        data.Add("L2");
                        break;
                    case 16:
                        data.Add("D2");
                        break;
                    case 17:
                        data.Add("R2");
                        break;
                    default:
                        throw new Exception("Le mouvement n'existe pas");
                }
            }
            return [.. data];
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
            int i = 1;
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
                Console.WriteLine(i);
                if (i == deepMax) return [];
                Parallel.Invoke(
                    () => NextTreeBranchForMITM(arbreInitial, true),
                    () => NextTreeBranchForMITM(arbreFinal, false)
                    );
                ParallelQuery<KeyValuePair<(ulong, ulong), byte>> arbreFinalManySelected = arbreFinal.AsParallel().SelectMany(x => x);
                ParallelQuery<KeyValuePair<(ulong, ulong), byte>> arbreInitialManySelected = arbreInitial.AsParallel().SelectMany(x => x);
                ParallelQuery<(ulong, ulong)> hasCommonElements = arbreInitialManySelected.Select(x => x.Key)
                    .Intersect(arbreFinalManySelected.Select(x => x.Key));
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
                                newCube.ExecuterAlgorithme(GetReversalPath(path.TakeLast(1)));
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
                                newCube2.ExecuterAlgorithme(GetReversalPath(path2.TakeLast(1)));
                            }
                        }
                        IEnumerable<byte> solutionFromRandom = GetReversalPath(path.Reverse<byte>()).Concat(path2).Reverse();
                        int count = solutionFromRandom.Count();
                        if (count < min)
                        {
                            solution = solutionFromRandom;
                            min = count;
                        }
                    }
                }
                i += 1;
            }
            return solution.ToArray();
        }

        private static byte GetDoubleMove(byte move)
        {
            return move switch
            {
                0 => 12,
                1 => 13,
                2 => 14,
                3 => 15,
                4 => 16,
                5 => 17,
                6 => 12,
                7 => 13,
                8 => 14,
                9 => 15,
                10 => 16,
                11 => 17,
                _ => throw new Exception("Mauvais numéro de move")
            };
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
                        newPath.Add(GetReversalMove(path[i]));
                        return newPath;
                    }
                    newPath.Add(GetReversalMove(path[i]));
                    i += 2;
                }
                else if (path[i] == GetReversalMove(path[i + 1]))
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
                        newPath.Add(GetDoubleMove(path[i]));
                        i++;
                    }
                }
                else
                {
                    if (path[i] < 12 && GetDoubleMove(path[i]) == path[i + 1])
                    {

                        newPath.Add(GetReversalMove(path[i]));
                        i++;
                    }
                    else if (path[i + 1] < 12 && GetDoubleMove(path[i + 1]) == path[i])
                    {
                        newPath.Add(GetReversalMove(path[i + 1]));
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
                    newPath[^1] = GetDoubleMove(newPath[^1]);
                    return newPath;
                }
                if (newPath[^1] == GetReversalMove(path[^1]) || newPath[^1] == path[^1])
                {
                    return newPath.SkipLast(1).ToList();
                }
                if (newPath.Count > 1)
                {
                    if (newPath[^1] == GetReversalMove(newPath[^2]))
                    {
                        return newPath.SkipLast(2).Append(path[^1]).ToList();
                    }
                    if (newPath[^1] == newPath[^2] && newPath[^1] < 12)
                    {
                        newPath[^2] = GetDoubleMove(newPath[^1]);
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

        public static byte[] BeginnerMethod(Cube c1)
        {
            List<Dictionary<(int, int, int, int, int, int), IEnumerable<byte>>> arbre1 = [];
            Dictionary<(int, int, int, int, int, int), IEnumerable<byte>> dico = new()
            {
                { StringCubeToInt(c1.ToString()), [0] }
            };
            arbre1.Add(dico);
            List<byte> path = [];
            bool thereAreAWhiteCross(Cube c) => (c.WhiteFace.Pieces[1, 2] == 'W' && c.WhiteFace.Pieces[2, 1] == 'W'
                && c.RedFace.Pieces[0, 1] == 'R' && c.BlueFace.Pieces[0, 1] == 'B') ||
                (c.WhiteFace.Pieces[0, 1] == 'W' && c.WhiteFace.Pieces[1, 0] == 'W'
                && c.GreenFace.Pieces[0, 1] == 'G' && c.OrangeFace.Pieces[0, 1] == 'O')
                || (c.WhiteFace.Pieces[1, 2] == 'W' && c.WhiteFace.Pieces[1, 0] == 'W'
                && c.BlueFace.Pieces[0, 1] == 'B' && c.GreenFace.Pieces[0, 1] == 'G')
                || (c.WhiteFace.Pieces[0, 1] == 'W' && c.WhiteFace.Pieces[2, 1] == 'W'
                && c.RedFace.Pieces[0, 1] == 'R' && c.OrangeFace.Pieces[0, 1] == 'O')
                || (c.WhiteFace.Pieces[1, 2] == 'W' && c.WhiteFace.Pieces[0, 1] == 'W'
                && c.BlueFace.Pieces[0, 1] == 'B' && c.OrangeFace.Pieces[0, 1] == 'O')
                || (c.WhiteFace.Pieces[1, 0] == 'W' && c.WhiteFace.Pieces[2, 1] == 'W'
                && c.GreenFace.Pieces[0, 1] == 'G' && c.RedFace.Pieces[0, 1] == 'R');
            bool isPlaced = thereAreAWhiteCross(c1);
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, thereAreAWhiteCross);
                if (newC != null)
                {
                    c1 = newC.Value.Item1.Clone();
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            arbre1 = [];
            dico = new()
            {
                { StringCubeToInt(c1.ToString()), [0] }
            };
            arbre1.Add(dico);
            bool crossAndEdges(Cube c) => c.WhiteFace.Pieces[0, 1] == 'W' && c.WhiteFace.Pieces[1, 0] == 'W'
            && c.WhiteFace.Pieces[1, 2] == 'W' && c.WhiteFace.Pieces[2, 1] == 'W'
            && c.RedFace.Pieces[0, 1] == 'R' && c.BlueFace.Pieces[0, 1] == 'B'
            && c.GreenFace.Pieces[0, 1] == 'G' && c.OrangeFace.Pieces[0, 1] == 'O';
            isPlaced = crossAndEdges(c1);
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, crossAndEdges);
                if (newC != null)
                {
                    c1 = newC.Value.Item1.Clone();
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            bool firstCornerPlaced(Cube c) => c.BlueFace.Pieces[0, 1] == 'B'
                && c.RedFace.Pieces[0, 2] == 'R'
                && c.WhiteFace.Pieces[2, 2] == 'W';
            bool secondCornerIsPlaced(Cube c) => c.RedFace.Pieces[0, 0] == 'R'
                && c.WhiteFace.Pieces[2, 0] == 'W'
                && c.GreenFace.Pieces[0, 2] == 'G';
            bool thirdCornerIsPlaced(Cube c) => c.OrangeFace.Pieces[0, 1] == 'O'
                && c.BlueFace.Pieces[0, 2] == 'B'
                && c.WhiteFace.Pieces[0, 2] == 'W';
            bool fourthCornerIsPlaced(Cube c) => c.WhiteFace.Pieces[0, 0] == 'W'
                && c.GreenFace.Pieces[0, 0] == 'G'
                && c.OrangeFace.Pieces[0, 2] == 'O';

            bool oneCornerIsPlaced(Cube c) => firstCornerPlaced(c) || secondCornerIsPlaced(c) || thirdCornerIsPlaced(c)
                || fourthCornerIsPlaced(c);
            arbre1 = [];
            dico = new()
            {
                { StringCubeToInt(c1.ToString()), [0] }
            };
            arbre1.Add(dico);
            isPlaced = crossAndEdges(c1) && oneCornerIsPlaced(c1);
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, (c1) => crossAndEdges(c1) && oneCornerIsPlaced(c1));
                if (newC != null)
                {
                    c1 = newC.Value.Item1.Clone();
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            arbre1 = [];
            dico = new()
            {
                { StringCubeToInt(c1.ToString()), [0] }
            };
            arbre1.Add(dico);
            isPlaced = crossAndEdges(c1)
                        && ((firstCornerPlaced(c1) && secondCornerIsPlaced(c1))
                        || (firstCornerPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (firstCornerPlaced(c1) && fourthCornerIsPlaced(c1))
                        || (secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))
                        || (secondCornerIsPlaced(c1) && fourthCornerIsPlaced(c1)));
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, (c1) => crossAndEdges(c1)
                        && ((firstCornerPlaced(c1) && secondCornerIsPlaced(c1))
                        || (firstCornerPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (firstCornerPlaced(c1) && fourthCornerIsPlaced(c1))
                        || (secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))
                        || (secondCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))));
                if (newC != null)
                {
                    c1 = newC.Value.Item1.Clone();
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            arbre1 = [];
            dico = new()
            {
                { StringCubeToInt(c1.ToString()), [0] }
            };
            arbre1.Add(dico);
            isPlaced = crossAndEdges(c1)
                        && ((firstCornerPlaced(c1) && secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (firstCornerPlaced(c1) && thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))
                        || (firstCornerPlaced(c1) && fourthCornerIsPlaced(c1) && secondCornerIsPlaced(c1))
                        || (secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1)));
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, (c1) => crossAndEdges(c1)
                        && ((firstCornerPlaced(c1) && secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (firstCornerPlaced(c1) && thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))
                        || (firstCornerPlaced(c1) && fourthCornerIsPlaced(c1) && secondCornerIsPlaced(c1))
                        || (secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))));
                if (newC != null)
                {
                    c1 = newC.Value.Item1.Clone();
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            arbre1 = [];
            dico = new()
            {
                { StringCubeToInt(c1.ToString()), [0] }
            };
            arbre1.Add(dico);
            isPlaced = crossAndEdges(c1)
                        && firstCornerPlaced(c1) && secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1);
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC  = OldGetNextBranch(arbre1, (c1) => crossAndEdges(c1)
                        && firstCornerPlaced(c1) && secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1));
                if (newC != null)
                {
                    c1 = newC.Value.Item1.Clone();
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            List<string> secondLayerLeftRedFace = ["D'", "B'", "D", "B", "D", "R", "D'", "R'"];
            List<string> secondLayerRightRedFace = ["D", "F", "D'", "F'", "D'", "R'", "D", "R"];

            List<string> secondLayerLeftBlueFace = ["D'", "L'", "D", "L", "D", "B", "D'", "B'"];
            List<string> secondLayerRightBlueFace = ["D", "R", "D", "R'", "D'", "B'", "D'", "B"];

            List<string> secondLayerLeftOrangeFace = ["D'", "F'", "D", "F", "D", "L", "D'", "L'"];
            List<string> secondLayerRightOrangeFace = ["D", "B", "D'", "B'", "D'", "L'", "D", "L"];

            List<string> secondLayerLeftGreenFace = ["D'", "R'", "D", "R", "D", "F", "D'", "F'"];
            List<string> secondLayerRightGreenFace = ["D", "L", "D'", "L'", "D'", "F'", "D", "F"];

            bool isSecondLayerDone() => crossAndEdges(c1)
                    && c1.WhiteFace.IsUniform && c1.RedFace.Pieces[0, 1] == 'R'
                    && c1.BlueFace.Pieces[0, 1] == 'B' && c1.GreenFace.Pieces[0, 1] == 'G'
                    && c1.OrangeFace.Pieces[0, 1] == 'O'
                    && c1.RedFace.Pieces[1, 0] == 'R' && c1.RedFace.Pieces[1, 2] == 'R'
                    && c1.OrangeFace.Pieces[1, 0] == 'O' && c1.OrangeFace.Pieces[1, 2] == 'O'
                    && c1.BlueFace.Pieces[1, 0] == 'B' && c1.BlueFace.Pieces[1, 2] == 'B'
                    && c1.GreenFace.Pieces[1, 0] == 'G' && c1.GreenFace.Pieces[1, 2] == 'G';
            int i = 0;
            while (!isSecondLayerDone())
            {
                if (c1.RedFace.Pieces[2, 1] == 'R' && c1.YellowFace.Pieces[0, 1] != 'Y')
                {
                    if (c1.YellowFace.Pieces[0, 1] == 'B')
                    {
                        c1.ExecuterAlgorithme(secondLayerLeftRedFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftRedFace));
                        i = 0;
                    }
                    else
                    {
                        c1.ExecuterAlgorithme(secondLayerRightRedFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightRedFace));
                        i = 0;
                    }
                }

                else if (c1.BlueFace.Pieces[2, 1] == 'B' && c1.YellowFace.Pieces[1, 2] != 'Y')
                {
                    if (c1.YellowFace.Pieces[1, 2] == 'O')
                    {
                        c1.ExecuterAlgorithme(secondLayerLeftBlueFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftBlueFace));
                        i = 0;
                    }
                    else
                    {
                        c1.ExecuterAlgorithme(secondLayerRightBlueFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightBlueFace));
                        i = 0;
                    }
                }

                else if (c1.GreenFace.Pieces[2, 1] == 'G' && c1.YellowFace.Pieces[1, 0] != 'Y')
                {
                    if (c1.YellowFace.Pieces[1, 0] == 'R')
                    {
                        c1.ExecuterAlgorithme(secondLayerLeftGreenFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftGreenFace));
                        i = 0;
                    }
                    else
                    {
                        c1.ExecuterAlgorithme(secondLayerRightGreenFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightGreenFace));
                        i = 0;
                    }
                }

                else if (c1.OrangeFace.Pieces[2, 1] == 'O' && c1.YellowFace.Pieces[2, 1] != 'Y')
                {
                    if (c1.YellowFace.Pieces[2, 1] == 'G')
                    {
                        c1.ExecuterAlgorithme(secondLayerLeftOrangeFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftOrangeFace));
                        i = 0;
                    }
                    else
                    {
                        c1.ExecuterAlgorithme(secondLayerRightOrangeFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightOrangeFace));
                        i = 0;
                    }
                }
                else
                {
                    c1.D();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "D" }));
                }

                if (i >= 3)
                {
                    if (c1.RedFace.Pieces[1, 0] != 'R')
                    {
                        c1.ExecuterAlgorithme(secondLayerRightRedFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightRedFace.Skip(1)));
                    }
                    else if (c1.RedFace.Pieces[1, 2] != 'R')
                    {
                        c1.ExecuterAlgorithme(secondLayerLeftRedFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftRedFace.Skip(1)));
                    }

                    else if (c1.BlueFace.Pieces[1, 0] != 'B')
                    {
                        c1.ExecuterAlgorithme(secondLayerRightBlueFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightBlueFace.Skip(1)));
                    }
                    else if (c1.BlueFace.Pieces[1, 2] != 'B')
                    {
                        c1.ExecuterAlgorithme(secondLayerLeftBlueFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftBlueFace.Skip(1)));
                    }

                    else if (c1.GreenFace.Pieces[1, 0] != 'G')
                    {
                        c1.ExecuterAlgorithme(secondLayerRightGreenFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightGreenFace.Skip(1)));
                    }
                    else if (c1.GreenFace.Pieces[1, 2] != 'G')
                    {
                        c1.ExecuterAlgorithme(secondLayerLeftGreenFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftGreenFace.Skip(1)));
                    }

                    else if (c1.OrangeFace.Pieces[1, 0] != 'O')
                    {
                        c1.ExecuterAlgorithme(secondLayerRightOrangeFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightOrangeFace.Skip(1)));
                    }
                    else if (c1.OrangeFace.Pieces[1, 2] != 'O')
                    {
                        c1.ExecuterAlgorithme(secondLayerLeftOrangeFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftOrangeFace.Skip(1)));
                    }
                    i = 0;
                }
                i++;
            }
            List<string> algoCrossPattern = ["R", "D", "F", "D'", "F'", "R'"];
            List<string> algoCrossPattern2 = ["R", "F", "D", "F'", "D'", "R'"];
            if (c1.YellowFace.Pieces[0, 1] != 'Y' && c1.YellowFace.Pieces[1, 0] != 'Y' && c1.YellowFace.Pieces[1, 2] != 'Y' &&
                c1.YellowFace.Pieces[2, 1] != 'Y')
            {
                c1.ExecuterAlgorithme(algoCrossPattern);
                path.AddRange(GetAlgoFromStringEnum(algoCrossPattern));
                c1.D();
                path.AddRange(GetAlgoFromStringEnum(new List<string>() { "D" }));
                c1.ExecuterAlgorithme(algoCrossPattern2);
                path.AddRange(GetAlgoFromStringEnum(algoCrossPattern2));
            }
            bool yellowCrossIsDone() => c1.YellowFace.Pieces[0, 1] == 'Y' && c1.YellowFace.Pieces[1, 0] == 'Y'
                                     && c1.YellowFace.Pieces[1, 2] == 'Y' && c1.YellowFace.Pieces[2, 1] == 'Y';
            while (!yellowCrossIsDone())
            {
                if (c1.YellowFace.Pieces[0, 1] == 'Y' && c1.YellowFace.Pieces[2, 1] == 'Y')
                {
                    c1.D();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "D" }));
                    c1.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(GetAlgoFromStringEnum(algoCrossPattern2));
                }
                else if (c1.YellowFace.Pieces[1, 0] == 'Y' && c1.YellowFace.Pieces[1, 2] == 'Y')
                {
                    c1.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(GetAlgoFromStringEnum(algoCrossPattern2));
                }
                else if (c1.YellowFace.Pieces[1, 2] == 'Y' && c1.YellowFace.Pieces[2, 1] == 'Y')
                {
                    c1.ExecuterAlgorithme(algoCrossPattern);
                    path.AddRange(GetAlgoFromStringEnum(algoCrossPattern));
                }
                else
                {
                    c1.D();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "D" }));
                }
            }
            bool edgeIsPlaced() => c1.RedFace.Pieces[2, 1] == 'R' && c1.GreenFace.Pieces[2, 1] == 'G'
                    && c1.OrangeFace.Pieces[2, 1] == 'O' && c1.BlueFace.Pieces[2, 1] == 'B';
            List<string> redF = ["D2", "F", "D", "F'", "D", "F", "D2", "F'", "D'"];
            List<string> BlueF = ["D", "F", "D", "F'", "D", "F", "D2", "F'"];
            List<string> GreenF = ["F", "D", "F'", "D", "F", "D2", "F'", "D"];
            List<string> GreenF2 = ["D'", "F", "D", "F'", "D", "F", "D2", "F'", "D2"];
            List<string> RedF2 = ["F", "D", "F'", "D", "F", "D2", "F'"];
            List<string> BlueF2 = ["D", "F", "D", "F'", "D", "F", "D2", "F'", "D'"];
            while (!edgeIsPlaced())
            {
                if (c1.RedFace.Pieces[2, 1] == 'R' && c1.BlueFace.Pieces[2, 1] == 'B')
                {
                    path.AddRange(GetAlgoFromStringEnum(redF));
                    c1.ExecuterAlgorithme(redF);
                }
                else if (c1.BlueFace.Pieces[2, 1] == 'B' && c1.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c1.ExecuterAlgorithme(BlueF);
                    path.AddRange(GetAlgoFromStringEnum(BlueF));
                }
                else if (c1.GreenFace.Pieces[2, 1] == 'G' && c1.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c1.ExecuterAlgorithme(GreenF);
                    path.AddRange(GetAlgoFromStringEnum(GreenF));
                }
                else if (c1.GreenFace.Pieces[2, 1] == 'G' && c1.RedFace.Pieces[2, 1] == 'R')
                {
                    c1.ExecuterAlgorithme(GreenF2);
                    path.AddRange(GetAlgoFromStringEnum(GreenF2));
                }
                else if (c1.RedFace.Pieces[2, 1] == 'R' && c1.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c1.ExecuterAlgorithme(RedF2);
                    path.AddRange(GetAlgoFromStringEnum(RedF2));
                }
                else if (c1.BlueFace.Pieces[2, 1] == 'B' && c1.GreenFace.Pieces[2, 1] == 'G')
                {
                    c1.ExecuterAlgorithme(BlueF2);
                    path.AddRange(GetAlgoFromStringEnum(BlueF2));
                }
                else
                {
                    c1.D();
                    path.AddRange(GetAlgoFromStringEnum(["D"]));
                }
            }
            bool cornersIsPlaced() => ((c1.RedFace.Pieces[2, 2] == 'R' && c1.BlueFace.Pieces[2, 0] == 'B')
                || (c1.RedFace.Pieces[2, 2] == 'B' && c1.BlueFace.Pieces[2, 0] == 'Y' && c1.YellowFace.Pieces[0, 2] == 'R')
                || (c1.RedFace.Pieces[2, 2] == 'Y' && c1.BlueFace.Pieces[2, 0] == 'R' && c1.YellowFace.Pieces[0, 2] == 'B'))
                && ((c1.RedFace.Pieces[2, 0] == 'R' && c1.GreenFace.Pieces[2, 2] == 'G')
                || (c1.RedFace.Pieces[2, 0] == 'G' && c1.GreenFace.Pieces[2, 2] == 'Y' && c1.YellowFace.Pieces[0, 0] == 'R')
                || (c1.RedFace.Pieces[2, 0] == 'Y' && c1.GreenFace.Pieces[2, 2] == 'R' && c1.YellowFace.Pieces[0, 0] == 'G'))
                && ((c1.BlueFace.Pieces[2, 2] == 'B' && c1.OrangeFace.Pieces[2, 0] == 'O')
                || (c1.BlueFace.Pieces[2, 2] == 'O' && c1.OrangeFace.Pieces[2, 0] == 'Y' && c1.YellowFace.Pieces[2, 2] == 'B')
                || (c1.BlueFace.Pieces[2, 2] == 'Y' && c1.OrangeFace.Pieces[2, 0] == 'B' && c1.YellowFace.Pieces[2, 2] == 'O'));

            List<string> cornerAlignementAlgo = ["D'", "B'", "D", "F", "D'", "B", "D", "F'"];
            List<string> cornerAlignementAlgo2 = ["B'", "D", "F", "D'", "B", "D", "F'", "D'"];
            List<string> cornerAlignementAlgo3 = ["D2", "B'", "D", "F", "D'", "B", "D", "F'", "D"];
            List<string> cornerAlignementAlgo4 = ["D", "B'", "D", "F", "D'", "B", "D", "F'", "D2"];

            List<string> cornerAlignementOptim = ["F", "D'", "B'", "D", "F'", "D'", "B", "D"];
            List<string> cornerAlignementOptim2 = ["L", "D'", "R'", "D", "L'", "D'", "R", "D"];
            List<string> cornerAlignementOptim3 = ["R", "D'", "L'", "D", "R'", "D'", "L", "D"];
            List<string> cornerAlignementOptim4 = ["B", "D'", "F'", "D", "B'", "D'", "F", "D"];

            while (!cornersIsPlaced())
            {
                if ((c1.RedFace.Pieces[2, 2] == 'R' && c1.BlueFace.Pieces[2, 0] == 'B')
                    || (c1.RedFace.Pieces[2, 2] == 'B' && c1.BlueFace.Pieces[2, 0] == 'Y' && c1.YellowFace.Pieces[0, 2] == 'R')
                    || (c1.RedFace.Pieces[2, 2] == 'Y' && c1.BlueFace.Pieces[2, 0] == 'R' && c1.YellowFace.Pieces[0, 2] == 'B'))
                {
                    if (c1.BlueFace.Pieces[2, 2] == 'B' || c1.BlueFace.Pieces[2, 2] == 'O'
                        || c1.OrangeFace.Pieces[2, 0] == 'O' || c1.OrangeFace.Pieces[2, 0] == 'B' ||
                        c1.YellowFace.Pieces[2, 2] == 'B' || c1.YellowFace.Pieces[2, 2] == 'O')
                    {
                        c1.ExecuterAlgorithme(cornerAlignementAlgo);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementAlgo));
                    }
                    else
                    {
                        c1.ExecuterAlgorithme(cornerAlignementOptim);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementOptim));
                    }
                }
                else if ((c1.RedFace.Pieces[2, 0] == 'R' && c1.GreenFace.Pieces[2, 2] == 'G')
                        || (c1.RedFace.Pieces[2, 0] == 'G' && c1.GreenFace.Pieces[2, 2] == 'Y' && c1.YellowFace.Pieces[0, 0] == 'R')
                        || (c1.RedFace.Pieces[2, 0] == 'Y' && c1.GreenFace.Pieces[2, 2] == 'R' && c1.YellowFace.Pieces[0, 0] == 'G'))
                {
                    if (c1.RedFace.Pieces[2, 2] == 'R' || c1.RedFace.Pieces[2, 2] == 'B' ||
                        c1.BlueFace.Pieces[2, 0] == 'B' || c1.BlueFace.Pieces[2, 0] == 'R' ||
                        c1.YellowFace.Pieces[0, 2] == 'R' || c1.YellowFace.Pieces[0, 2] == 'B')
                    {
                        c1.ExecuterAlgorithme(cornerAlignementAlgo2);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementAlgo2));
                    }
                    else
                    {
                        c1.ExecuterAlgorithme(cornerAlignementOptim2);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementOptim2));
                    }

                }
                else if ((c1.BlueFace.Pieces[2, 2] == 'B' && c1.OrangeFace.Pieces[2, 0] == 'O')
                        || (c1.BlueFace.Pieces[2, 2] == 'O' && c1.OrangeFace.Pieces[2, 0] == 'Y' && c1.YellowFace.Pieces[2, 2] == 'B')
                        || (c1.BlueFace.Pieces[2, 2] == 'Y' && c1.OrangeFace.Pieces[2, 0] == 'B' && c1.YellowFace.Pieces[2, 2] == 'O'))
                {
                    if (c1.OrangeFace.Pieces[2, 2] == 'O' || c1.OrangeFace.Pieces[2, 2] == 'G' ||
                        c1.GreenFace.Pieces[2, 0] == 'G' || c1.GreenFace.Pieces[2, 0] == 'O' ||
                        c1.YellowFace.Pieces[2, 0] == 'G' || c1.YellowFace.Pieces[2, 0] == 'O')
                    {
                        c1.ExecuterAlgorithme(cornerAlignementAlgo3);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementAlgo3));
                    }
                    else
                    {
                        c1.ExecuterAlgorithme(cornerAlignementOptim3);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementOptim3));
                    }
                }
                else
                {
                    if (c1.GreenFace.Pieces[2, 2] == 'R' || c1.GreenFace.Pieces[2, 2] == 'G' ||
                        c1.RedFace.Pieces[2, 0] == 'R' || c1.RedFace.Pieces[2, 0] == 'G' ||
                        c1.YellowFace.Pieces[0, 0] == 'G' || c1.YellowFace.Pieces[0, 0] == 'R')
                    {
                        c1.ExecuterAlgorithme(cornerAlignementAlgo4);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementAlgo4));
                    }
                    else
                    {
                        c1.ExecuterAlgorithme(cornerAlignementOptim4);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementOptim4));
                    }
                }
            }
            List<string> sexyMove = ["R", "U", "R'", "U'"];
            List<byte> byteAlgo = GetAlgoFromStringEnum(sexyMove);
            while (!c1.IsSolved)
            {
                if (c1.GreenFace.Pieces[2, 2] == 'Y')
                {
                    c1.ExecuterAlgorithme(sexyMove);
                    c1.ExecuterAlgorithme(sexyMove);
                    c1.ExecuterAlgorithme(sexyMove);
                    c1.ExecuterAlgorithme(sexyMove);
                    path.AddRange(byteAlgo);
                    path.AddRange(byteAlgo);
                    path.AddRange(byteAlgo);
                    path.AddRange(byteAlgo);
                    if (c1.IsSolved) break;
                    c1.Dprime();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "D'" }));
                }
                else if (c1.RedFace.Pieces[2, 0] == 'Y')
                {
                    c1.ExecuterAlgorithme(sexyMove);
                    c1.ExecuterAlgorithme(sexyMove);
                    path.AddRange(byteAlgo);
                    path.AddRange(byteAlgo);
                    if (c1.IsSolved) break;
                    c1.Dprime();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "D'" }));

                }
                else
                {
                    c1.Dprime();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "D'" }));
                }
            }
            return [.. path];
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
            List<string> inverseSexyMove = ["F", "D", "F'", "D'"];
            List<byte> inverseByteAlgo = GetAlgoFromStringEnum(inverseSexyMove);
            IEnumerable<string> doubleInverseSexyMove = inverseSexyMove.Concat(inverseSexyMove);
            IEnumerable<string> quadInverseSexyMove = doubleInverseSexyMove.Concat(doubleInverseSexyMove);
            IEnumerable<byte> doubleInverseByteAlgo = inverseByteAlgo.Concat(inverseByteAlgo);
            IEnumerable<byte> quadInverseByteAlgo = doubleInverseByteAlgo.Concat(doubleInverseByteAlgo);
            List<byte> uPrimeAlgo = GetAlgoFromStringEnum(["U'"]);
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
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftRedFace));
                        i = 0;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightRedFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightRedFace));
                        i = 0;
                    }
                }

                else if (c.BlueFace.Pieces[2, 1] == 'B' && c.YellowFace.Pieces[1, 2] != 'Y')
                {
                    if (c.YellowFace.Pieces[1, 2] == 'O')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftBlueFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftBlueFace));
                        i = 0;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightBlueFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightBlueFace));
                        i = 0;
                    }
                }

                else if (c.GreenFace.Pieces[2, 1] == 'G' && c.YellowFace.Pieces[1, 0] != 'Y')
                {
                    if (c.YellowFace.Pieces[1, 0] == 'R')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftGreenFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftGreenFace));
                        i = 0;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightGreenFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightGreenFace));
                        i = 0;
                    }
                }

                else if (c.OrangeFace.Pieces[2, 1] == 'O' && c.YellowFace.Pieces[2, 1] != 'Y')
                {
                    if (c.YellowFace.Pieces[2, 1] == 'G')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftOrangeFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftOrangeFace));
                        i = 0;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(secondLayerRightOrangeFace);
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightOrangeFace));
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
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightRedFace.Skip(1)));
                    }
                    else if (c.RedFace.Pieces[1, 2] != 'R')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftRedFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftRedFace.Skip(1)));
                    }

                    else if (c.BlueFace.Pieces[1, 0] != 'B')
                    {
                        c.ExecuterAlgorithme(secondLayerRightBlueFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightBlueFace.Skip(1)));
                    }
                    else if (c.BlueFace.Pieces[1, 2] != 'B')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftBlueFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftBlueFace.Skip(1)));
                    }

                    else if (c.GreenFace.Pieces[1, 0] != 'G')
                    {
                        c.ExecuterAlgorithme(secondLayerRightGreenFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightGreenFace.Skip(1)));
                    }
                    else if (c.GreenFace.Pieces[1, 2] != 'G')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftGreenFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftGreenFace.Skip(1)));
                    }

                    else if (c.OrangeFace.Pieces[1, 0] != 'O')
                    {
                        c.ExecuterAlgorithme(secondLayerRightOrangeFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerRightOrangeFace.Skip(1)));
                    }
                    else if (c.OrangeFace.Pieces[1, 2] != 'O')
                    {
                        c.ExecuterAlgorithme(secondLayerLeftOrangeFace.Skip(1));
                        path.AddRange(GetAlgoFromStringEnum(secondLayerLeftOrangeFace.Skip(1)));
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
                    path.AddRange(GetAlgoFromStringEnum(algoCrossPattern));
                    c.D();
                    path.AddRange(dAlgo);
                    c.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(GetAlgoFromStringEnum(algoCrossPattern2));
                    break;
                }
                else if (c.YellowFace.Pieces[0, 1] == 'Y' && c.YellowFace.Pieces[2, 1] == 'Y')
                {
                    c.D();
                    path.AddRange(dAlgo);
                    c.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(GetAlgoFromStringEnum(algoCrossPattern2));
                    break;
                }
                else if (c.YellowFace.Pieces[1, 0] == 'Y' && c.YellowFace.Pieces[1, 2] == 'Y')
                {
                    c.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(GetAlgoFromStringEnum(algoCrossPattern2));
                    break;
                }
                else if (c.YellowFace.Pieces[1, 2] == 'Y' && c.YellowFace.Pieces[2, 1] == 'Y')
                {
                    c.ExecuterAlgorithme(algoCrossPattern);
                    path.AddRange(GetAlgoFromStringEnum(algoCrossPattern));
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
                    path.AddRange(GetAlgoFromStringEnum(redF));
                    c.ExecuterAlgorithme(redF);
                    break;
                }
                else if (c.BlueFace.Pieces[2, 1] == 'B' && c.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c.ExecuterAlgorithme(blueF);
                    path.AddRange(GetAlgoFromStringEnum(blueF));
                    break;
                }
                else if (c.GreenFace.Pieces[2, 1] == 'G' && c.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c.ExecuterAlgorithme(greenF);
                    path.AddRange(GetAlgoFromStringEnum(greenF));
                    break;
                }
                else if (c.GreenFace.Pieces[2, 1] == 'G' && c.RedFace.Pieces[2, 1] == 'R')
                {
                    c.ExecuterAlgorithme(greenF2);
                    path.AddRange(GetAlgoFromStringEnum(greenF2));
                    break;
                }
                else if (c.RedFace.Pieces[2, 1] == 'R' && c.OrangeFace.Pieces[2, 1] == 'O')
                {
                    c.ExecuterAlgorithme(redF2);
                    path.AddRange(GetAlgoFromStringEnum(redF2));
                }
                else if (c.BlueFace.Pieces[2, 1] == 'B' && c.GreenFace.Pieces[2, 1] == 'G')
                {
                    c.ExecuterAlgorithme(blueF2);
                    path.AddRange(GetAlgoFromStringEnum(blueF2));
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
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementAlgo));
                        break;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(cornerAlignementOptim);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementOptim));
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
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementAlgo2));
                        break;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(cornerAlignementOptim2);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementOptim2));
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
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementAlgo3));
                        break;
                    }
                    else
                    {
                        c.ExecuterAlgorithme(cornerAlignementOptim3);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementOptim3));
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
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementAlgo4));
                    }
                    else
                    {
                        c.ExecuterAlgorithme(cornerAlignementOptim4);
                        path.AddRange(GetAlgoFromStringEnum(cornerAlignementOptim4));
                    }
                }
            }
            return c;
        }
        private static void OrientLastCornersOptim(Cube c, List<byte> path)
        {
            IEnumerable<string> doubleSM = sexyMove.Concat(sexyMove);
            IEnumerable<string> quadSM = doubleSM.Concat(doubleSM);
            List<byte> byteAlgo = GetAlgoFromStringEnum(sexyMove);
            IEnumerable<byte> doubleByteAlgo = byteAlgo.Concat(byteAlgo);
            IEnumerable<byte> quadByteAlgo = doubleByteAlgo.Concat(doubleByteAlgo);
            List<byte> dPrimeAlgo = GetAlgoFromStringEnum(["D'"]);
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
            List<byte> dAlgo = GetAlgoFromStringEnum(["D"]);
            c = SecondLayer(c, path, dAlgo);
            c = YellowCross(c, path, dAlgo);
            c = OrientEdges(c, path, dAlgo);
            c = PlaceSecondCorners(c, path);
            OrientLastCornersOptim(c, path);
            return path;
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