using System.Linq;
using System.Text;

namespace Rubik_s_cube_solver
{
    public class Move
    {
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

        public static byte GetDoubleMove(byte move)
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
            return move switch
            {
                <= 5 => (byte)(move + 6),
                <= 11 => (byte)(move - 6),
                _ => move,
            };
        }
    }
}
