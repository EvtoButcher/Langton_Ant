
namespace Langton__s_Ant
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y; 
        }

        public Vector(Vector V1)
        {
            X = V1.X;
            Y = V1.Y;
        } 

        public static Vector operator +(Vector V1, Vector V2)
        {
            Vector V = new Vector((V1.X) + (V2.X), (V1.Y) + (V2.Y));
            return V;
        }
    }

}
