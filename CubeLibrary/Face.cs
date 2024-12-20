﻿using System.Text;

namespace CubeLibrary
{
    public class Face : IEquatable<Face?>
    {
        public char[,] Pieces { get; set; }
        private char[,] nouvelleFace;
        public char ColorFace
        {
            get
            {
                return Pieces[1, 1];
            }
        }

        public bool IsUniform
        {
            get
            {
                return Pieces.Cast<char>().All(piece => piece == ColorFace);
            }
        }
        public Face(char[] pieces)
        {
            if (pieces.Length != 9) throw new Exception("Pas le bon nombre de piece sur cette face");
            Pieces = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                Pieces[0, i] = pieces[i];
            }
            for (int i = 3; i < 6; i++)
            {
                Pieces[1, i] = pieces[i];
            }
            for (int i = 6; i < 9; i++)
            {
                Pieces[2, i] = pieces[i];
            }
        }

        public Face(char couleur)
        {
            Pieces = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Pieces[i, j] = couleur;
                }
            }
        }

        public Face(char[,] pieces)
        {
            Pieces = pieces;
        }

        public Face Clone()
        {
            return new Face((char[,])Pieces.Clone());
        }

        public void Rotate90Left()
        {
            nouvelleFace = new char[3, 3];
            nouvelleFace[0, 0] = Pieces[0, 2];
            nouvelleFace[0, 1] = Pieces[1, 2];
            nouvelleFace[0, 2] = Pieces[2, 2];
            nouvelleFace[1, 0] = Pieces[0, 1];
            nouvelleFace[1, 2] = Pieces[2, 1];
            nouvelleFace[2, 0] = Pieces[0, 0];
            nouvelleFace[2, 1] = Pieces[1, 0];
            nouvelleFace[2, 2] = Pieces[2, 0];
            nouvelleFace[1, 1] = Pieces[1, 1];
            Pieces = nouvelleFace;
        }

        public void Rotate90Right()
        {
            nouvelleFace = new char[3, 3];
            nouvelleFace[0, 0] = Pieces[2, 0];
            nouvelleFace[0, 1] = Pieces[1, 0];
            nouvelleFace[0, 2] = Pieces[0, 0];
            nouvelleFace[1, 0] = Pieces[2, 1];
            nouvelleFace[1, 2] = Pieces[0, 1];
            nouvelleFace[2, 0] = Pieces[2, 2];
            nouvelleFace[2, 1] = Pieces[1, 2];
            nouvelleFace[2, 2] = Pieces[0, 2];
            nouvelleFace[1, 1] = Pieces[1, 1];
            Pieces = nouvelleFace;
        }

        public void Rotate180()
        {
            nouvelleFace = new char[3, 3];
            nouvelleFace[0, 0] = Pieces[2, 2];
            nouvelleFace[0, 1] = Pieces[2, 1];
            nouvelleFace[0, 2] = Pieces[2, 0];
            nouvelleFace[1, 0] = Pieces[1, 2];
            nouvelleFace[1, 2] = Pieces[1, 0];
            nouvelleFace[2, 0] = Pieces[0, 2];
            nouvelleFace[2, 1] = Pieces[0, 1];
            nouvelleFace[2, 2] = Pieces[0, 0];
            nouvelleFace[1, 1] = Pieces[1, 1];
            Pieces = nouvelleFace;
        }

        public override string ToString()
        {
            StringBuilder sb = new(9);
            foreach (char piece in Pieces)
            {
                sb.Append(piece);
            }
            return sb.ToString();
        }

        public string PrintFace()
        {
            StringBuilder sb = new();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 2)
                    {
                        sb.Append(Pieces[i, j]);
                        sb.AppendLine();
                    }
                    else
                    {
                        sb.Append(Pieces[i, j]);
                        sb.Append(' ');
                    }
                }
            }
            return sb.ToString();
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Face);
        }

        public bool Equals(Face? other)
        {
            if (other is null) return false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Pieces[i, j] != other.Pieces[i, j]) return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Pieces);
        }
    }
}
