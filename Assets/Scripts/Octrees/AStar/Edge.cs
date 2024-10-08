namespace Octrees
{
    public class Edge
    {
        public readonly Node a, b;

        public Edge(Node a, Node b)
        {
            this.a = a;
            this.b = b;
        }

        public override bool Equals(object obj)
        {
            return obj is Edge other && ((a == other.a && b == other.b) || (a == other.b && b == other.a));
        }

        public override int GetHashCode() => a.GetHashCode() ^ b.GetHashCode();
    }
}
