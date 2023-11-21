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
                if (face.ColorFace == 'W') WhiteFace = face;
                else if (face.ColorFace == 'Y') YellowFace = face;
                else if (face.ColorFace == 'R') RedFace = face;
                else if (face.ColorFace == 'G') GreenFace = face;
                else if (face.ColorFace == 'B') BlueFace = face;
                else if (face.ColorFace == 'O') OrangeFace = face;
                else throw new Exception("La couleur " + face.ColorFace + " n'existe pas");
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
                if (face.ColorFace == 'W') WhiteFace = face;
                else if (face.ColorFace == 'Y') YellowFace = face;
                else if (face.ColorFace == 'R') RedFace = face;
                else if (face.ColorFace == 'G') GreenFace = face;
                else if (face.ColorFace == 'B') BlueFace = face;
                else if (face.ColorFace == 'O') OrangeFace = face;
                else throw new Exception("La couleur " + face.ColorFace + " n'existe pas");
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
            if (color == 'W') return (char[,])WhiteFace.Pieces.Clone();
            else if (color == 'Y') return (char[,])YellowFace.Pieces.Clone();
            else if (color == 'R') return (char[,])RedFace.Pieces.Clone();
            else if (color == 'G') return (char[,])GreenFace.Pieces.Clone();
            else if (color == 'B') return (char[,])BlueFace.Pieces.Clone();
            else if (color == 'O') return (char[,])OrangeFace.Pieces.Clone();
            else throw new Exception("La couleur " + color + " n'existe pas");
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
                if (randInt == 1) F();
                else if (randInt == 2) U();
                else if (randInt == 3) B();
                else if (randInt == 4) L();
                else if (randInt == 5) D();
                else if (randInt == 6) R();
                else if (randInt == 7) Fprime();
                else if (randInt == 8) Uprime();
                else if (randInt == 9) Bprime();
                else if (randInt == 10) Lprime();
                else if (randInt == 11) Dprime();
                else if (randInt == 12) Rprime();
                else if (randInt == 13) F2();
                else if (randInt == 14) U2();
                else if (randInt == 15) B2();
                else if (randInt == 16) L2();
                else if (randInt == 17) D2();
                else if (randInt == 18) R2();
                else Debug.Assert(false);
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
                if (randInt == 0) F();
                else if (randInt == 1) U();
                else if (randInt == 2) B();
                else if (randInt == 3) L();
                else if (randInt == 4) D();
                else if (randInt == 5) R();
                else if (randInt == 6) Fprime();
                else if (randInt == 7) Uprime();
                else if (randInt == 8) Bprime();
                else if (randInt == 9) Lprime();
                else if (randInt == 10) Dprime();
                else if (randInt == 11) Rprime();
                else if (randInt == 12) F2();
                else if (randInt == 13) U2();
                else if (randInt == 14) B2();
                else if (randInt == 15) L2();
                else if (randInt == 16) D2();
                else if (randInt == 17) R2();
                else Debug.Assert(false);
            }
            return randPath;
        }



        public static byte[] GetReversalPath(IEnumerable<byte> path)
        {
            byte[] newPath = new byte[path.Count()];
            int i = 0;
            foreach (byte item in path)
            {
                if (item == 0) newPath[i] = 6;
                else if (item == 1) newPath[i] = 7;
                else if (item == 2) newPath[i] = 8;
                else if (item == 3) newPath[i] = 9;
                else if (item == 4) newPath[i] = 10;
                else if (item == 5) newPath[i] = 11;
                else if (item == 6) newPath[i] = 0;
                else if (item == 7) newPath[i] = 1;
                else if (item == 8) newPath[i] = 2;
                else if (item == 9) newPath[i] = 3;
                else if (item == 10) newPath[i] = 4;
                else if (item == 11) newPath[i] = 5;
                else newPath[i] = item;
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
            if (move <= 5) return (byte)(move + 6);
            else if (move <= 11) return (byte)(move - 6);
            else return move;
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
                if (item == 0) response.Append('F');
                else if (item == 1) response.Append('U');
                else if (item == 2) response.Append('B');
                else if (item == 3) response.Append('L');
                else if (item == 4) response.Append('D');
                else if (item == 5) response.Append('R');
                else if (item == 6) response.Append("F'");
                else if (item == 7) response.Append("U'");
                else if (item == 8) response.Append("B'");
                else if (item == 9) response.Append("L'");
                else if (item == 10) response.Append("D'");
                else if (item == 11) response.Append("R'");
                else if (item == 12) response.Append("F2");
                else if (item == 13) response.Append("U2");
                else if (item == 14) response.Append("B2");
                else if (item == 15) response.Append("L2");
                else if (item == 16) response.Append("D2");
                else if (item == 17) response.Append("R2");
            }
            return response.ToString();
        }

        public static IEnumerable<string> StringPathToEnum(string path)
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

        public List<List<((int, int, int, int, int, int), byte[])>> GenererArbre(int nombreGen)
        {
            List<List<((int, int, int, int, int, int), byte[])>> listeCubes = [];
            HashSet<(int, int, int, int, int, int)> hashCubes = [];
            int methodsCount = 18;
            (int, int, int, int, int, int) intThisCube = StringCubeToInt(ToString());
            listeCubes.Add([(intThisCube, new byte[1])]);
            hashCubes.Add(intThisCube);
            for (int i = 1; i < nombreGen; i++)
            {
                List<((int, int, int, int, int, int), byte[])> newCubes = [];
                foreach (((int, int, int, int, int, int), byte[]) cube in listeCubes[i - 1])
                {
                    Cube c1 = new(IntToCubeString(cube.Item1));
                    for (byte j = 0; j < methodsCount; j++)
                    {
                        Cube c = c1.Clone();
                        c.DoMove(j);
                        (int, int, int, int, int, int) intCube = StringCubeToInt(c.ToString());
                        if (hashCubes.Add(intCube))
                            newCubes.Add((intCube, cube.Item2.Append(j).ToArray()));
                    }
                }
                listeCubes.Add(newCubes);
            }
            return listeCubes;
        }

        public static void OriginalNextTreeBranch(List<List<((int, int, int, int, int, int), byte[])>> gen, HashSet<(int, int, int, int, int, int)> h)
        {
            int methodsCount = 18;
            List<((int, int, int, int, int, int), byte[])> newCubes = [];
            foreach (((int, int, int, int, int, int), byte[]) cube in gen[^1])
            {
                Cube c1 = new(IntToCubeString(cube.Item1));
                for (byte j = 0; j < methodsCount; j++)
                {
                    Cube c = c1.Clone();
                    c.DoMove(j);
                    (int, int, int, int, int, int) intCube = StringCubeToInt(c.ToString());
                    if (h.Add(intCube))
                        newCubes.Add((intCube, cube.Item2.Append(j).ToArray()));
                }
            }
            gen.Add(newCubes);
        }


        public static void NextTreeBranchForMITMv2(Dictionary<(ulong, ulong), byte[]> dico)
        {
            int methodsCount = 18;
            List<((ulong, ulong), byte[])> newCubes = [];
            foreach (KeyValuePair<(ulong, ulong), byte[]> cube in dico)
            {
                Cube c1 = new(DecompressState(cube.Key));
                for (byte j = 0; j < methodsCount; j++)
                {
                    Cube c = c1.Clone();
                    c.DoMove(j);
                    (ulong, ulong) intCube = CompressState(c.ToString());
                    if (!dico.ContainsKey(intCube))
                        newCubes.Add((intCube, cube.Value.Append(j).ToArray()));
                }
            }
            foreach (((ulong, ulong), byte[]) cube in newCubes)
            {
                dico.TryAdd(cube.Item1, cube.Item2);
            }
        }

        public static void NextTreeBranchForMITMv3(List<Dictionary<(ulong, ulong), byte[]>> listDico)
        {
            int methodsCount = 18;
            Dictionary<(ulong, ulong), byte[]> newCubes = [];
            foreach (KeyValuePair<(ulong, ulong), byte[]> cube in listDico[^1])
            {
                Cube c1 = new(DecompressState(cube.Key));
                for (byte j = 0; j < methodsCount; j++)
                {
                    //Cube c = c1.Clone();
                    c1.DoMove(j);
                    (ulong, ulong) intCube = CompressState(c1.ToString());
                    bool isContained = false;
                    foreach (Dictionary<(ulong, ulong), byte[]> item in listDico)
                    {
                        if (item.ContainsKey(intCube))
                        {
                            isContained = true;
                            break;
                        }
                    }
                    if (!isContained)
                        newCubes.TryAdd(intCube, [.. cube.Value, j]);
                    if (j != 17)
                        c1.DoMove(GetReversalMove(j));
                }
            }
            listDico.Add(newCubes);
        }

        public static (Cube, byte[])? GetNextBranchWithStrongCompressionOfCubes(List<Dictionary<(ulong, ulong), byte[]>> listDico, Func<Cube, bool> f)
        {
            int methodsCount = 18;
            Dictionary<(ulong, ulong), byte[]> newCubes = [];
            foreach (KeyValuePair<(ulong, ulong), byte[]> cube in listDico[^1])
            {
                Cube c1 = new(DecompressState(cube.Key));
                for (byte j = 0; j < methodsCount; j++)
                {
                    c1.DoMove(j);
                    (ulong, ulong) intCube = CompressState(c1.ToString());
                    bool isContained = false;
                    foreach (Dictionary<(ulong, ulong), byte[]> item in listDico)
                    {
                        if (item.ContainsKey(intCube))
                        {
                            isContained = true;
                            break;
                        }
                    }
                    if (!isContained)
                        newCubes.TryAdd(intCube, [.. cube.Value, j]);
                    if (f(c1))
                        return (c1, newCubes[intCube]);
                    if (j != 17)
                        c1.DoMove(GetReversalMove(j));
                }
            }
            listDico.Add(newCubes);
            return null;
        }

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

        public static void NextTreeBranch2(List<Dictionary<Cube, byte[]>> listDico)
        {
            Dictionary<Cube, byte[]> newCubes = [];
            foreach (KeyValuePair<Cube, byte[]> cube in listDico[^1])
            {
                Cube c1 = cube.Key;
                for (byte j = 0; j < 18; j++)
                {
                    c1.DoMove(j);
                    //string str = c1.ToString();
                    bool isContained = false;
                    foreach (Dictionary<Cube, byte[]> item in listDico)
                    {
                        if (item.ContainsKey(c1))
                        {
                            isContained = true;
                            break;
                        }
                    }
                    if (!isContained)
                        newCubes.TryAdd(c1, [.. cube.Value, j]);
                    if (j != 17)
                        c1.DoMove(GetReversalMove(j));
                }
            }
            listDico.Add(newCubes);
        }


        public static void NextTreeBranchForMITM(List<Dictionary<(ulong, ulong), byte>> listDico)
        {
            int methodsCount = 18;
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
                if (item == "F") data.Add(0);
                else if (item == "U") data.Add(1);
                else if (item == "B") data.Add(2);
                else if (item == "L") data.Add(3);
                else if (item == "D") data.Add(4);
                else if (item == "R") data.Add(5);
                else if (item == "F'") data.Add(6);
                else if (item == "U'") data.Add(7);
                else if (item == "B'") data.Add(8);
                else if (item == "L'") data.Add(9);
                else if (item == "D'") data.Add(10);
                else if (item == "R'") data.Add(11);
                else if (item == "F2") data.Add(12);
                else if (item == "U2") data.Add(13);
                else if (item == "B2") data.Add(14);
                else if (item == "L2") data.Add(15);
                else if (item == "D2") data.Add(16);
                else if (item == "R2") data.Add(17);
                else throw new Exception("Le mouvement n'existe pas " + item);
            }
            return data;
        }

        public static string[] GetListStringAlgoFromByte(IEnumerable<byte> algorithme)
        {
            List<string> data = [];
            foreach (byte item in algorithme)
            {
                if (item == 0) data.Add("F");
                else if (item == 1) data.Add("U");
                else if (item == 2) data.Add("B");
                else if (item == 3) data.Add("L");
                else if (item == 4) data.Add("D");
                else if (item == 5) data.Add("R");
                else if (item == 6) data.Add("F'");
                else if (item == 7) data.Add("U'");
                else if (item == 8) data.Add("B'");
                else if (item == 9) data.Add("L'");
                else if (item == 10) data.Add("D'");
                else if (item == 11) data.Add("R'");
                else if (item == 12) data.Add("F2");
                else if (item == 13) data.Add("U2");
                else if (item == 14) data.Add("B2");
                else if (item == 15) data.Add("L2");
                else if (item == 16) data.Add("D2");
                else if (item == 17) data.Add("R2");
                else throw new Exception("Le mouvement n'existe pas");
            }
            return [.. data];
        }
        public void ExecuterAlgorithme(IEnumerable<string> algorithme)
        {
            foreach (string item in algorithme)
            {
                if (item == "F") DoMove(0);
                else if (item == "U") DoMove(1);
                else if (item == "B") DoMove(2);
                else if (item == "L") DoMove(3);
                else if (item == "D") DoMove(4);
                else if (item == "R") DoMove(5);
                else if (item == "F'") DoMove(6);
                else if (item == "U'") DoMove(7);
                else if (item == "B'") DoMove(8);
                else if (item == "L'") DoMove(9);
                else if (item == "D'") DoMove(10);
                else if (item == "R'") DoMove(11);
                else if (item == "F2") DoMove(12);
                else if (item == "U2") DoMove(13);
                else if (item == "B2") DoMove(14);
                else if (item == "L2") DoMove(15);
                else if (item == "D2") DoMove(16);
                else if (item == "R2") DoMove(17);
                else throw new Exception("Le mouvement n'existe pas");
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
            int i = 2;
            HashSet<(int, int, int, int, int, int)> h1 = [];
            HashSet<(int, int, int, int, int, int)> h2 = [];
            h1.Add(StringCubeToInt(finalCube.ToString()));
            h2.Add(StringCubeToInt(initialCube.ToString()));
            List<List<((int, int, int, int, int, int), byte[])>> arbreFinal = finalCube.GenererArbre(1);
            List<List<((int, int, int, int, int, int), byte[])>> arbreInitial = initialCube.GenererArbre(1);
            List<byte> solutionFromRandom = [];
            while (!isSolved)
            {
                if (i == deepMax) return [];
                Parallel.Invoke(
                    () => OriginalNextTreeBranch(arbreFinal, h1),
                    () => OriginalNextTreeBranch(arbreInitial, h2)
                    );
                ParallelQuery<((int, int, int, int, int, int), byte[])> arbreFinalManySelected = arbreFinal.AsParallel().SelectMany(x => x);
                ParallelQuery<((int, int, int, int, int, int), byte[])> arbreInitialManySelected = arbreInitial.AsParallel().SelectMany(x => x);
                ParallelQuery<(int, int, int, int, int, int)> intersect = arbreFinalManySelected.Select(x => x.Item1).Intersect(arbreInitialManySelected.Select(x => x.Item1));
                if (intersect.Any())
                {
                    isSolved = true;
                    Dictionary<(int, int, int, int, int, int), byte[]> dicoPos = [];
                    foreach (((int, int, int, int, int, int), byte[]) item in arbreInitialManySelected)
                    {
                        if (h1.Contains(item.Item1) && h2.Contains(item.Item1))
                            dicoPos.Add(item.Item1, item.Item2.Skip(1).ToArray());
                    }
                    h1.Clear();
                    h2.Clear();
                    foreach (((int, int, int, int, int, int), byte[]) item in arbreFinalManySelected)
                    {
                        if (dicoPos.TryGetValue(item.Item1, out byte[]? value))
                        {
                            dicoPos[item.Item1] = [.. value, .. GetReversalPath(item.Item2.Skip(1).Reverse())];
                        } // Le reverse c'est pour avoir la seconde partie du chemin
                    }
                    int minLength = dicoPos.Select(x => x.Value.Length).Min();
                    KeyValuePair<(int, int, int, int, int, int), byte[]> firstItem = dicoPos.Where(x => x.Value.Length == minLength).First();
                    solutionFromRandom = [.. firstItem.Value];
                }
                else
                {
                    if (i >= 5)
                    {
                        arbreFinal.RemoveRange(0, arbreFinal.Count - 1);
                        arbreInitial.RemoveRange(0, arbreInitial.Count - 1);
                    }
                }
                i += 1;
            }
            initialCube.ExecuterAlgorithme(solutionFromRandom);
            return [.. solutionFromRandom];
        }

        public static byte[] MeetInTheMiddle2(Cube initialCube, Cube? finalCube = null, int? deepMax = null)
        {
            finalCube ??= new();
            bool isSolved = false;
            int i = 2;
            Dictionary<(ulong, ulong), byte[]> dico1 = new()
            {
                { Cube.CompressState(initialCube.ToString()), new byte[1] { 0 } }
            };
            Dictionary<(ulong, ulong), byte[]> dico2 = new()
            {
                { Cube.CompressState(finalCube.ToString()), new byte[1] { 0 } }
            };
            List<byte> solutionFromRandom = [];
            while (!isSolved)
            {
                if (i == deepMax) return [];
                Parallel.Invoke(
                    () => NextTreeBranchForMITMv2(dico1),
                    () => NextTreeBranchForMITMv2(dico2)
                    );
                bool hasCommonElements = dico1.Keys.Intersect(dico2.Keys).Any();
                if (hasCommonElements)
                {
                    isSolved = true;
                    Dictionary<(ulong, ulong), byte[]> dicoPos = [];
                    foreach (KeyValuePair<(ulong, ulong), byte[]> item in dico1)
                    {
                        if (dico2.TryGetValue(item.Key, out byte[]? value))
                        {
                            dicoPos.Add(item.Key, item.Value.Skip(1).Concat(GetReversalPath(value.Skip(1).Reverse())).ToArray());
                        }
                    }
                    int minLength = dicoPos.Select(x => x.Value.Length).Min();
                    KeyValuePair<(ulong, ulong), byte[]> firstItem = dicoPos.Where(x => x.Value.Length == minLength).First();
                    solutionFromRandom = [.. firstItem.Value];
                }
                i += 1;
            }
            initialCube.ExecuterAlgorithme(solutionFromRandom);
            return [.. solutionFromRandom];
        }

        public static byte[] MeetInTheMiddle3(Cube initialCube, Cube? finalCube = null, int? deepMax = null)
        {
            finalCube ??= new();
            bool isSolved = false;
            int i = 2;
            Dictionary<(ulong, ulong), byte[]> dico1 = new()
            {
                { CompressState(initialCube.ToString()), new byte[1] { 0 } }
            };
            Dictionary<(ulong, ulong), byte[]> dico2 = new()
            {
                { CompressState(finalCube.ToString()), new byte[1] { 0 } }
            };
            List<Dictionary<(ulong, ulong), byte[]>> arbreInitial = [];
            List<Dictionary<(ulong, ulong), byte[]>> arbreFinal = [];
            arbreInitial.Add(dico1);
            arbreFinal.Add(dico2);
            List<byte> solutionFromRandom = [];
            while (!isSolved)
            {
                //Console.WriteLine(i);
                if (i == deepMax) return [];
                Parallel.Invoke(
                    () => NextTreeBranchForMITMv3(arbreInitial),
                    () => NextTreeBranchForMITMv3(arbreFinal)
                    );
                IEnumerable<KeyValuePair<(ulong, ulong), byte[]>> arbreFinalManySelected = arbreFinal.SelectMany(x => x);
                IEnumerable<KeyValuePair<(ulong, ulong), byte[]>> arbreInitialManySelected = arbreInitial.SelectMany(x => x);
                HashSet<(ulong, ulong)> hasCommonElements = arbreInitialManySelected.Select(x => x.Key)
                    .Intersect(arbreFinalManySelected.Select(x => x.Key)).ToHashSet();
                if (hasCommonElements.Count != 0)
                {
                    isSolved = true;
                    Dictionary<(ulong, ulong), byte[]> dicoPos = [];
                    foreach (KeyValuePair<(ulong, ulong), byte[]> item in arbreInitialManySelected)
                    {
                        if (hasCommonElements.Contains(item.Key))
                            dicoPos.Add(item.Key, item.Value.Skip(1).ToArray());
                    }
                    foreach (KeyValuePair<(ulong, ulong), byte[]> item in arbreFinalManySelected)
                    {
                        if (dicoPos.TryGetValue(item.Key, out byte[]? value))
                        {
                            dicoPos[item.Key] = [.. value, .. GetReversalPath(item.Value.Skip(1).Reverse().ToArray())];
                        } // Le reverse c'est pour avoir la seconde partie du chemin
                    }

                    int minLength = dicoPos.Select(x => x.Value.Length).Min();
                    KeyValuePair<(ulong, ulong), byte[]> firstItem = dicoPos.Where(x => x.Value.Length == minLength).First();
                    solutionFromRandom = [.. firstItem.Value];
                }
                i += 1;
            }
            initialCube.ExecuterAlgorithme(solutionFromRandom);
            return [.. solutionFromRandom];
        }

        public static byte[] MeetInTheMiddle5(Cube initialCube, Cube? finalCube = null, int? deepMax = null)
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
                if (i == deepMax) return [];
                Parallel.Invoke(
                    () => NextTreeBranchForMITM(arbreInitial),
                    () => NextTreeBranchForMITM(arbreFinal)
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
            if (move == 0) return 12;
            else if (move == 1) return 13;
            else if (move == 2) return 14;
            else if (move == 3) return 15;
            else if (move == 4) return 16;
            else if (move == 5) return 17;
            else if (move == 6) return 12;
            else if (move == 7) return 13;
            else if (move == 8) return 14;
            else if (move == 9) return 15;
            else if (move == 10) return 16;
            else if (move == 11) return 17;
            else throw new Exception("Mauvais numéro de move");
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

        public static byte[] OptimizePath(IEnumerable<byte> path, int chunkSize = 2, int sizeMax = 4, bool shifting = true)
        {
            if (!path.Any()) return path.ToArray();
            List<byte> finalFlatPath = path.ToList();
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
                        byte[] minPath = MeetInTheMiddle5(c1.Clone(), c2.Clone(), maxDeep);
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
                    byte[] minPath = MeetInTheMiddle5(c1.Clone(), c2.Clone(), maxDeep);
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
            };
            arbre1.Add(dico);
            isPlaced = crossAndEdges(c1)
                        && firstCornerPlaced(c1) && secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1);
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, (c1) => crossAndEdges(c1)
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
            while (!edgeIsPlaced())
            {
                if (c1.RedFace.Pieces[2, 1] == 'R' && c1.BlueFace.Pieces[2, 1] == 'B')
                {
                    List<string> redF = ["D2", "F", "D", "F'", "D", "F", "D2", "F'", "D'"];
                    path.AddRange(GetAlgoFromStringEnum(redF));
                    c1.ExecuterAlgorithme(redF);
                }
                else if (c1.BlueFace.Pieces[2, 1] == 'B' && c1.OrangeFace.Pieces[2, 1] == 'O')
                {
                    List<string> BlueF = ["D", "F", "D", "F'", "D", "F", "D2", "F'"];
                    c1.ExecuterAlgorithme(BlueF);
                    path.AddRange(GetAlgoFromStringEnum(BlueF));
                }
                else if (c1.GreenFace.Pieces[2, 1] == 'G' && c1.OrangeFace.Pieces[2, 1] == 'O')
                {
                    List<string> GreenF = ["F", "D", "F'", "D", "F", "D2", "F'", "D"];
                    c1.ExecuterAlgorithme(GreenF);
                    path.AddRange(GetAlgoFromStringEnum(GreenF));
                }
                else if (c1.GreenFace.Pieces[2, 1] == 'G' && c1.RedFace.Pieces[2, 1] == 'R')
                {
                    List<string> GreenF2 = ["D'", "F", "D", "F'", "D", "F", "D2", "F'", "D2"];
                    c1.ExecuterAlgorithme(GreenF2);
                    path.AddRange(GetAlgoFromStringEnum(GreenF2));
                }
                else if (c1.RedFace.Pieces[2, 1] == 'R' && c1.OrangeFace.Pieces[2, 1] == 'O')
                {
                    List<string> RedF2 = ["F", "D", "F'", "D", "F", "D2", "F'"];
                    c1.ExecuterAlgorithme(RedF2);
                    path.AddRange(GetAlgoFromStringEnum(RedF2));
                }
                else if (c1.BlueFace.Pieces[2, 1] == 'B' && c1.GreenFace.Pieces[2, 1] == 'G')
                {
                    List<string> BlueF2 = ["D", "F", "D", "F'", "D", "F", "D2", "F'", "D'"];
                    c1.ExecuterAlgorithme(BlueF2);
                    path.AddRange(GetAlgoFromStringEnum(BlueF2));
                }
                else
                {
                    c1.D();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "D" }));

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
                { c.ToString(), new byte[1] { 0 } }
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
                (Cube, byte[])? newC = NextTreeBranch(arbre, oneEdgeIsPlaced);
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
                { c.ToString(), new byte[1] { 0 } }
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
                (Cube, byte[])? newC = NextTreeBranch(arbre, (c) => (firstEdge(c) && secondEdge(c))
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
                { c.ToString(), new byte[1] { 0 } }
            };
            arbre.Add(dico);
            isPlaced = ((firstEdge(c) && secondEdge(c) && thirdEdge(c))
                        || (firstEdge(c) && thirdEdge(c) && fourthEdge(c))
                        || (firstEdge(c) && fourthEdge(c) && secondEdge(c))
                        || (secondEdge(c) && thirdEdge(c) && fourthEdge(c)));
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, byte[])? newC = NextTreeBranch(arbre, (c) => ((firstEdge(c) && secondEdge(c) && thirdEdge(c))
                        || (firstEdge(c) && thirdEdge(c) && fourthEdge(c))
                        || (firstEdge(c) && fourthEdge(c) && secondEdge(c))
                        || (secondEdge(c) && thirdEdge(c) && fourthEdge(c))));
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
                { c.ToString(), new byte[1] { 0 } }
            };
            arbre.Add(dico);
            isPlaced = firstEdge(c) && secondEdge(c) && thirdEdge(c) && fourthEdge(c);
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, byte[])? newC = NextTreeBranch(arbre, (c) => firstEdge(c) && secondEdge(c) && thirdEdge(c) && fourthEdge(c));
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
                { c.ToString(), new byte[1] { 0 } }
            };
            arbre.Add(dico);
            bool isPlaced = crossAndEdges(c) && oneCornerIsPlaced(c);
            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, byte[])? newC = NextTreeBranch(arbre, (c) => crossAndEdges(c) && oneCornerIsPlaced(c));
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
                { c.ToString(), new byte[1] { 0 } }
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
                (Cube, byte[])? newC = NextTreeBranch(arbre, (c) => crossAndEdges(c)
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
                { c.ToString(), new byte[1] { 0 } }
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
                (Cube, byte[])? newC = NextTreeBranch(arbre, (c) => crossAndEdges(c)
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
                { c.ToString(), new byte[1] { 0 } }
            };
            arbre.Add(dico);
            isPlaced = crossAndEdges(c)
                        && allCornersIsPlaced(c);

            while (!isPlaced)
            {
                if (path.Count > 1000 || arbre.Count >= 6) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                (Cube, byte[])? newC = NextTreeBranch(arbre, (c) => crossAndEdges(c)
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
            bool firstCornerOriented(Cube c) => c.BlueFace.Pieces[0, 1] == 'B'
                && c.RedFace.Pieces[0, 2] == 'R'
                && c.WhiteFace.Pieces[2, 2] == 'W';
            bool secondCornerIsOriented(Cube c) => c.RedFace.Pieces[0, 0] == 'R'
                && c.WhiteFace.Pieces[2, 0] == 'W'
                && c.GreenFace.Pieces[0, 2] == 'G';
            bool thirdCornerIsOriented(Cube c) => c.OrangeFace.Pieces[0, 1] == 'O'
                && c.BlueFace.Pieces[0, 2] == 'B'
                && c.WhiteFace.Pieces[0, 2] == 'W';
            bool fourthCornerIsOriented(Cube c) => c.WhiteFace.Pieces[0, 0] == 'W'
                && c.GreenFace.Pieces[0, 0] == 'G'
                && c.OrangeFace.Pieces[0, 2] == 'O';
            bool allCornersIsOriented(Cube c) => firstCornerOriented(c) && secondCornerIsOriented(c) && thirdCornerIsOriented(c)
                && fourthCornerIsOriented(c);
            List<string> inverseSexyMove = ["F", "D", "F'", "D'"];
            List<byte> inverseByteAlgo = GetAlgoFromStringEnum(inverseSexyMove);
            IEnumerable<string> doubleInverseSexyMove = inverseSexyMove.Concat(inverseSexyMove);
            IEnumerable<string> quadInverseSexyMove = doubleInverseSexyMove.Concat(doubleInverseSexyMove);
            IEnumerable<byte> doubleInverseByteAlgo = inverseByteAlgo.Concat(inverseByteAlgo);
            IEnumerable<byte> quadInverseByteAlgo = doubleInverseByteAlgo.Concat(doubleInverseByteAlgo);
            List<byte> uPrimeAlgo = GetAlgoFromStringEnum(new List<string>() { "U'" });
            while (!allCornersIsOriented(c))
            {
                if (path.Count > 1000) throw new ArgumentException("Le cube n'est pas résoluble, vérifiez l'entrée");
                if (c.RedFace.Pieces[0, 0] == 'W')
                {
                    c.ExecuterAlgorithme(quadInverseSexyMove);
                    path.AddRange(quadInverseByteAlgo);
                    if (allCornersIsOriented(c)) break;
                    c.Uprime();
                    path.AddRange(uPrimeAlgo);
                }
                else if (c.GreenFace.Pieces[0, 2] == 'W')
                {
                    c.ExecuterAlgorithme(doubleInverseSexyMove);
                    path.AddRange(doubleInverseByteAlgo);
                    if (allCornersIsOriented(c)) break;
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
            bool crossAndEdges(Cube c) => c.WhiteFace.Pieces[0, 1] == 'W' && c.WhiteFace.Pieces[1, 0] == 'W'
            && c.WhiteFace.Pieces[1, 2] == 'W' && c.WhiteFace.Pieces[2, 1] == 'W'
            && c.RedFace.Pieces[0, 1] == 'R' && c.BlueFace.Pieces[0, 1] == 'B'
            && c.GreenFace.Pieces[0, 1] == 'G' && c.OrangeFace.Pieces[0, 1] == 'O';
            List<string> secondLayerLeftRedFace = ["D'", "B'", "D", "B", "D", "R", "D'", "R'"];
            List<string> secondLayerRightRedFace = ["D", "F", "D'", "F'", "D'", "R'", "D", "R"];

            List<string> secondLayerLeftBlueFace = ["D'", "L'", "D", "L", "D", "B", "D'", "B'"];
            List<string> secondLayerRightBlueFace = ["D", "R", "D", "R'", "D'", "B'", "D'", "B"];

            List<string> secondLayerLeftOrangeFace = ["D'", "F'", "D", "F", "D", "L", "D'", "L'"];
            List<string> secondLayerRightOrangeFace = ["D", "B", "D'", "B'", "D'", "L'", "D", "L"];

            List<string> secondLayerLeftGreenFace = ["D'", "R'", "D", "R", "D", "F", "D'", "F'"];
            List<string> secondLayerRightGreenFace = ["D", "L", "D'", "L'", "D'", "F'", "D", "F"];

            bool isSecondLayerDone(Cube c) => crossAndEdges(c)
                    && c.WhiteFace.IsUniform && c.RedFace.Pieces[0, 1] == 'R'
                    && c.BlueFace.Pieces[0, 1] == 'B' && c.GreenFace.Pieces[0, 1] == 'G'
                    && c.OrangeFace.Pieces[0, 1] == 'O'
                    && c.RedFace.Pieces[1, 0] == 'R' && c.RedFace.Pieces[1, 2] == 'R'
                    && c.OrangeFace.Pieces[1, 0] == 'O' && c.OrangeFace.Pieces[1, 2] == 'O'
                    && c.BlueFace.Pieces[1, 0] == 'B' && c.BlueFace.Pieces[1, 2] == 'B'
                    && c.GreenFace.Pieces[1, 0] == 'G' && c.GreenFace.Pieces[1, 2] == 'G';
            int i = 0;
            while (!isSecondLayerDone(c))
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
            List<string> algoCrossPattern = ["R", "D", "F", "D'", "F'", "R'"];
            List<string> algoCrossPattern2 = ["R", "F", "D", "F'", "D'", "R'"];

            static bool yellowCrossIsDone(Cube c) => c.YellowFace.Pieces[0, 1] == 'Y' && c.YellowFace.Pieces[1, 0] == 'Y'
                                     && c.YellowFace.Pieces[1, 2] == 'Y' && c.YellowFace.Pieces[2, 1] == 'Y';
            while (!yellowCrossIsDone(c))
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
            List<string> redF = ["D2", "F", "D", "F'", "D", "F", "D2", "F'", "D'"];
            List<string> blueF = ["D", "F", "D", "F'", "D", "F", "D2", "F'"];
            List<string> greenF = ["F", "D", "F'", "D", "F", "D2", "F'", "D"];
            List<string> greenF2 = ["D'", "F", "D", "F'", "D", "F", "D2", "F'", "D2"];
            List<string> redF2 = ["F", "D", "F'", "D", "F", "D2", "F'"];
            List<string> blueF2 = ["D", "F", "D", "F'", "D", "F", "D2", "F'", "D'"];

            static bool edgeIsPlaced(Cube c) => c.RedFace.Pieces[2, 1] == 'R' && c.GreenFace.Pieces[2, 1] == 'G'
                    && c.OrangeFace.Pieces[2, 1] == 'O' && c.BlueFace.Pieces[2, 1] == 'B';
            while (!edgeIsPlaced(c))
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
            static bool cornersIsPlaced(Cube c) => ((c.RedFace.Pieces[2, 2] == 'R' && c.BlueFace.Pieces[2, 0] == 'B')
                || (c.RedFace.Pieces[2, 2] == 'B' && c.BlueFace.Pieces[2, 0] == 'Y' && c.YellowFace.Pieces[0, 2] == 'R')
                || (c.RedFace.Pieces[2, 2] == 'Y' && c.BlueFace.Pieces[2, 0] == 'R' && c.YellowFace.Pieces[0, 2] == 'B'))
                && ((c.RedFace.Pieces[2, 0] == 'R' && c.GreenFace.Pieces[2, 2] == 'G')
                || (c.RedFace.Pieces[2, 0] == 'G' && c.GreenFace.Pieces[2, 2] == 'Y' && c.YellowFace.Pieces[0, 0] == 'R')
                || (c.RedFace.Pieces[2, 0] == 'Y' && c.GreenFace.Pieces[2, 2] == 'R' && c.YellowFace.Pieces[0, 0] == 'G'))
                && ((c.BlueFace.Pieces[2, 2] == 'B' && c.OrangeFace.Pieces[2, 0] == 'O')
                || (c.BlueFace.Pieces[2, 2] == 'O' && c.OrangeFace.Pieces[2, 0] == 'Y' && c.YellowFace.Pieces[2, 2] == 'B')
                || (c.BlueFace.Pieces[2, 2] == 'Y' && c.OrangeFace.Pieces[2, 0] == 'B' && c.YellowFace.Pieces[2, 2] == 'O'));

            List<string> cornerAlignementAlgo = ["D'", "B'", "D", "F", "D'", "B", "D", "F'"];
            List<string> cornerAlignementAlgo2 = ["B'", "D", "F", "D'", "B", "D", "F'", "D'"];
            List<string> cornerAlignementAlgo3 = ["D2", "B'", "D", "F", "D'", "B", "D", "F'", "D"];
            List<string> cornerAlignementAlgo4 = ["D", "B'", "D", "F", "D'", "B", "D", "F'", "D2"];

            List<string> cornerAlignementOptim = ["F", "D'", "B'", "D", "F'", "D'", "B", "D"];
            List<string> cornerAlignementOptim2 = ["L", "D'", "R'", "D", "L'", "D'", "R", "D"];
            List<string> cornerAlignementOptim3 = ["R", "D'", "L'", "D", "R'", "D'", "L", "D"];
            List<string> cornerAlignementOptim4 = ["B", "D'", "F'", "D", "B'", "D'", "F", "D"];

            while (!cornersIsPlaced(c))
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
            List<string> sexyMove = ["R", "U", "R'", "U'"];
            IEnumerable<string> doubleSM = sexyMove.Concat(sexyMove);
            IEnumerable<string> quadSM = doubleSM.Concat(doubleSM);
            List<byte> byteAlgo = GetAlgoFromStringEnum(sexyMove);
            IEnumerable<byte> doubleByteAlgo = byteAlgo.Concat(byteAlgo);
            IEnumerable<byte> quadByteAlgo = doubleByteAlgo.Concat(doubleByteAlgo);
            List<byte> dPrimeAlgo = GetAlgoFromStringEnum(new List<string>() { "D'" });

            static bool isSolvedOptim(Cube c) => c.RedFace.IsUniform && c.WhiteFace.IsUniform && c.YellowFace.IsUniform;
            while (!isSolvedOptim(c))
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

        // Same method but cubes are stored in a Dictionary<(int, int, int, int, int, int), byte[]>
        // That require less memory but more computations (StringCubeToInt(c.ToString()) called each times 
        // but only c.ToString() in FastBeginnerMethod, I don't know if it's faster, probably approximatively same time.
        public static List<byte> FastBeginnerMethod2(Cube c1)
        {
            List<Dictionary<(int, int, int, int, int, int), IEnumerable<byte>>> arbre1 = [];
            Dictionary<(int, int, int, int, int, int), IEnumerable<byte>> dico = new()
            {
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
            };
            arbre1.Add(dico);
            bool firstEdge(Cube c) => c.WhiteFace.Pieces[2, 1] == 'W';
            bool secondEdge(Cube c) => c.WhiteFace.Pieces[1, 2] == 'W';
            bool thirdEdge(Cube c) => c.WhiteFace.Pieces[1, 0] == 'W';
            bool fourthEdge(Cube c) => c.WhiteFace.Pieces[0, 1] == 'W';
            bool oneEdgeIsPlaced(Cube c) => firstEdge(c) || secondEdge(c) || thirdEdge(c) || fourthEdge(c);
            bool isPlaced = oneEdgeIsPlaced(c1);
            List<byte> path = [];
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, oneEdgeIsPlaced);
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
            };
            arbre1.Add(dico);
            isPlaced = (firstEdge(c1) && secondEdge(c1))
                        || (firstEdge(c1) && thirdEdge(c1))
                        || (firstEdge(c1) && fourthEdge(c1))
                        || (secondEdge(c1) && thirdEdge(c1))
                        || (thirdEdge(c1) && fourthEdge(c1))
                        || (secondEdge(c1) && fourthEdge(c1));
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, (c1) => (firstEdge(c1) && secondEdge(c1))
                        || (firstEdge(c1) && thirdEdge(c1))
                        || (firstEdge(c1) && fourthEdge(c1))
                        || (secondEdge(c1) && thirdEdge(c1))
                        || (thirdEdge(c1) && fourthEdge(c1))
                        || (secondEdge(c1) && fourthEdge(c1)));
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
            };
            arbre1.Add(dico);
            isPlaced = ((firstEdge(c1) && secondEdge(c1) && thirdEdge(c1))
                        || (firstEdge(c1) && thirdEdge(c1) && fourthEdge(c1))
                        || (firstEdge(c1) && fourthEdge(c1) && secondEdge(c1))
                        || (secondEdge(c1) && thirdEdge(c1) && fourthEdge(c1)));
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, (c1) => ((firstEdge(c1) && secondEdge(c1) && thirdEdge(c1))
                        || (firstEdge(c1) && thirdEdge(c1) && fourthEdge(c1))
                        || (firstEdge(c1) && fourthEdge(c1) && secondEdge(c1))
                        || (secondEdge(c1) && thirdEdge(c1) && fourthEdge(c1))));
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
            };
            arbre1.Add(dico);
            isPlaced = firstEdge(c1) && secondEdge(c1) && thirdEdge(c1) && fourthEdge(c1);
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, (c1) => firstEdge(c1) && secondEdge(c1) && thirdEdge(c1) && fourthEdge(c1));
                if (newC != null)
                {
                    c1 = newC.Value.Item1.Clone();
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            bool crossAndEdges(Cube c) => c.WhiteFace.Pieces[0, 1] == 'W' && c.WhiteFace.Pieces[1, 0] == 'W'
            && c.WhiteFace.Pieces[1, 2] == 'W' && c.WhiteFace.Pieces[2, 1] == 'W'
            && c.RedFace.Pieces[0, 1] == 'R' && c.BlueFace.Pieces[0, 1] == 'B'
            && c.GreenFace.Pieces[0, 1] == 'G' && c.OrangeFace.Pieces[0, 1] == 'O';

            List<string> placeEdges = ["B", "U", "B'", "U", "B", "U2", "B'", "U"];
            List<string> placeEdges2 = ["U'", "B", "U", "B'", "U", "B", "U2", "B'", "U2"];
            List<string> placeEdges3 = ["U2", "B", "U", "B'", "U", "B", "U2", "B'", "U'"];
            List<string> placeEdges4 = ["U", "B", "U", "B'", "U", "B", "U2", "B'"];
            List<string> placeEdges5 = ["B2", "F2", "D2", "B2", "F2"];
            List<string> placeEdges6 = ["L2", "R2", "D2", "L2", "R2"];
            while (!crossAndEdges(c1))
            {
                if (c1.OrangeFace.Pieces[0, 1] == 'O' && c1.BlueFace.Pieces[0, 1] == 'B')
                {
                    c1.ExecuterAlgorithme(placeEdges);
                    path.AddRange(GetAlgoFromStringEnum(placeEdges));
                }
                else if (c1.RedFace.Pieces[0, 1] == 'R' && c1.BlueFace.Pieces[0, 1] == 'B')
                {
                    c1.ExecuterAlgorithme(placeEdges2);
                    path.AddRange(GetAlgoFromStringEnum(placeEdges2));
                }
                else if (c1.RedFace.Pieces[0, 1] == 'R' && c1.GreenFace.Pieces[0, 1] == 'G')
                {
                    c1.ExecuterAlgorithme(placeEdges3);
                    path.AddRange(GetAlgoFromStringEnum(placeEdges3));
                }
                else if (c1.OrangeFace.Pieces[0, 1] == 'O' && c1.GreenFace.Pieces[0, 1] == 'G')
                {
                    c1.ExecuterAlgorithme(placeEdges4);
                    path.AddRange(GetAlgoFromStringEnum(placeEdges4));
                }
                else if (c1.OrangeFace.Pieces[0, 1] == 'O' && c1.RedFace.Pieces[0, 1] == 'R')
                {
                    c1.ExecuterAlgorithme(placeEdges5);
                    path.AddRange(GetAlgoFromStringEnum(placeEdges5));
                }
                else if (c1.BlueFace.Pieces[0, 1] == 'B' && c1.GreenFace.Pieces[0, 1] == 'G')
                {
                    c1.ExecuterAlgorithme(placeEdges6);
                    path.AddRange(GetAlgoFromStringEnum(placeEdges6));
                }
                else
                {
                    c1.U();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "U" }));
                }
            }

            bool firstCornerOriented(Cube c) => c.BlueFace.Pieces[0, 1] == 'B'
                && c.RedFace.Pieces[0, 2] == 'R'
                && c.WhiteFace.Pieces[2, 2] == 'W';
            bool secondCornerIsOriented(Cube c) => c.RedFace.Pieces[0, 0] == 'R'
                && c.WhiteFace.Pieces[2, 0] == 'W'
                && c.GreenFace.Pieces[0, 2] == 'G';
            bool thirdCornerIsOriented(Cube c) => c.OrangeFace.Pieces[0, 1] == 'O'
                && c.BlueFace.Pieces[0, 2] == 'B'
                && c.WhiteFace.Pieces[0, 2] == 'W';
            bool fourthCornerIsOriented(Cube c) => c.WhiteFace.Pieces[0, 0] == 'W'
                && c.GreenFace.Pieces[0, 0] == 'G'
                && c.OrangeFace.Pieces[0, 2] == 'O';

            bool allCornersIsOriented(Cube c) => firstCornerOriented(c) && secondCornerIsOriented(c) && thirdCornerIsOriented(c)
                && fourthCornerIsOriented(c);

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

            arbre1 = [];
            dico = new()
            {
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
            };
            arbre1.Add(dico);
            isPlaced = crossAndEdges(c1)
                        && ((firstCornerIsPlaced(c1) && secondCornerIsPlaced(c1))
                        || (firstCornerIsPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (firstCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))
                        || (secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))
                        || (secondCornerIsPlaced(c1) && fourthCornerIsPlaced(c1)));
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, (c1) => crossAndEdges(c1)
                        && ((firstCornerIsPlaced(c1) && secondCornerIsPlaced(c1))
                        || (firstCornerIsPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (firstCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
            };
            arbre1.Add(dico);
            isPlaced = crossAndEdges(c1)
                        && ((firstCornerIsPlaced(c1) && secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (firstCornerIsPlaced(c1) && thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))
                        || (firstCornerIsPlaced(c1) && fourthCornerIsPlaced(c1) && secondCornerIsPlaced(c1))
                        || (secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1)));
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, (c1) => crossAndEdges(c1)
                        && ((firstCornerIsPlaced(c1) && secondCornerIsPlaced(c1) && thirdCornerIsPlaced(c1))
                        || (firstCornerIsPlaced(c1) && thirdCornerIsPlaced(c1) && fourthCornerIsPlaced(c1))
                        || (firstCornerIsPlaced(c1) && fourthCornerIsPlaced(c1) && secondCornerIsPlaced(c1))
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
                { StringCubeToInt(c1.ToString()), new byte[1] { 0 } }
            };
            arbre1.Add(dico);
            isPlaced = crossAndEdges(c1)
                        && allCornersIsPlaced(c1);
            while (!isPlaced)
            {
                (Cube, IEnumerable<byte>)? newC = OldGetNextBranch(arbre1, (c1) => crossAndEdges(c1)
                        && allCornersIsPlaced(c1));
                if (newC != null)
                {
                    c1 = newC.Value.Item1.Clone();
                    path.AddRange(newC.Value.Item2.Skip(1));
                    isPlaced = true;
                }
            }
            List<string> inverseSexyMove = ["F", "D", "F'", "D'"];
            List<byte> inverseByteAlgo = GetAlgoFromStringEnum(inverseSexyMove);
            while (!allCornersIsOriented(c1))
            {
                if (c1.RedFace.Pieces[0, 0] == 'W')
                {
                    c1.ExecuterAlgorithme(inverseSexyMove);
                    c1.ExecuterAlgorithme(inverseSexyMove);
                    c1.ExecuterAlgorithme(inverseSexyMove);
                    c1.ExecuterAlgorithme(inverseSexyMove);
                    path.AddRange(inverseByteAlgo);
                    path.AddRange(inverseByteAlgo);
                    path.AddRange(inverseByteAlgo);
                    path.AddRange(inverseByteAlgo);
                    if (allCornersIsOriented(c1)) break;
                    c1.Uprime();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "U'" }));
                }
                else if (c1.GreenFace.Pieces[0, 2] == 'W')
                {
                    c1.ExecuterAlgorithme(inverseSexyMove);
                    c1.ExecuterAlgorithme(inverseSexyMove);
                    path.AddRange(inverseByteAlgo);
                    path.AddRange(inverseByteAlgo);
                    if (allCornersIsOriented(c1)) break;
                    c1.Uprime();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "U'" }));

                }
                else
                {
                    c1.Uprime();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "U'" }));
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
            bool yellowCrossIsDone() => c1.YellowFace.Pieces[0, 1] == 'Y' && c1.YellowFace.Pieces[1, 0] == 'Y'
                                     && c1.YellowFace.Pieces[1, 2] == 'Y' && c1.YellowFace.Pieces[2, 1] == 'Y';
            while (!yellowCrossIsDone())
            {
                if (c1.YellowFace.Pieces[0, 1] != 'Y' && c1.YellowFace.Pieces[1, 0] != 'Y' && c1.YellowFace.Pieces[1, 2] != 'Y' &&
                c1.YellowFace.Pieces[2, 1] != 'Y')
                {
                    c1.ExecuterAlgorithme(algoCrossPattern);
                    path.AddRange(GetAlgoFromStringEnum(algoCrossPattern));
                    c1.D();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "D" }));
                    c1.ExecuterAlgorithme(algoCrossPattern2);
                    path.AddRange(GetAlgoFromStringEnum(algoCrossPattern2));
                    break;
                }
                else if (c1.YellowFace.Pieces[0, 1] == 'Y' && c1.YellowFace.Pieces[2, 1] == 'Y')
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
            while (!edgeIsPlaced())
            {
                if (c1.RedFace.Pieces[2, 1] == 'R' && c1.BlueFace.Pieces[2, 1] == 'B')
                {
                    List<string> redF = ["D2", "F", "D", "F'", "D", "F", "D2", "F'", "D'"];
                    path.AddRange(GetAlgoFromStringEnum(redF));
                    c1.ExecuterAlgorithme(redF);
                }
                else if (c1.BlueFace.Pieces[2, 1] == 'B' && c1.OrangeFace.Pieces[2, 1] == 'O')
                {
                    List<string> BlueF = ["D", "F", "D", "F'", "D", "F", "D2", "F'"];
                    c1.ExecuterAlgorithme(BlueF);
                    path.AddRange(GetAlgoFromStringEnum(BlueF));
                }
                else if (c1.GreenFace.Pieces[2, 1] == 'G' && c1.OrangeFace.Pieces[2, 1] == 'O')
                {
                    List<string> GreenF = ["F", "D", "F'", "D", "F", "D2", "F'", "D"];
                    c1.ExecuterAlgorithme(GreenF);
                    path.AddRange(GetAlgoFromStringEnum(GreenF));
                }
                else if (c1.GreenFace.Pieces[2, 1] == 'G' && c1.RedFace.Pieces[2, 1] == 'R')
                {
                    List<string> GreenF2 = ["D'", "F", "D", "F'", "D", "F", "D2", "F'", "D2"];
                    c1.ExecuterAlgorithme(GreenF2);
                    path.AddRange(GetAlgoFromStringEnum(GreenF2));
                }
                else if (c1.RedFace.Pieces[2, 1] == 'R' && c1.OrangeFace.Pieces[2, 1] == 'O')
                {
                    List<string> RedF2 = ["F", "D", "F'", "D", "F", "D2", "F'"];
                    c1.ExecuterAlgorithme(RedF2);
                    path.AddRange(GetAlgoFromStringEnum(RedF2));
                }
                else if (c1.BlueFace.Pieces[2, 1] == 'B' && c1.GreenFace.Pieces[2, 1] == 'G')
                {
                    List<string> BlueF2 = ["D", "F", "D", "F'", "D", "F", "D2", "F'", "D'"];
                    c1.ExecuterAlgorithme(BlueF2);
                    path.AddRange(GetAlgoFromStringEnum(BlueF2));
                }
                else
                {
                    c1.D();
                    path.AddRange(GetAlgoFromStringEnum(new List<string>() { "D" }));

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
    }
}

