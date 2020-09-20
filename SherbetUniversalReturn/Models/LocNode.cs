using UnityEngine;

namespace SherbetUniversalReturn.Models
{
    public class LocNode
    {
        public static LocNode Empty => new LocNode(new Vector3(), 0);
        public float Yaw;
        public Vector3 Position;

        public static bool operator ==(LocNode L, LocNode R)
        {
            return (L.Position == R.Position && L.Yaw == R.Yaw);
        }

        public static bool operator !=(LocNode L, LocNode R)
        {
            return !(L.Position == R.Position && L.Yaw == R.Yaw);
        }

        public LocNode(Vector3 Pos, float Yaw)
        {
            Position = Pos;
            this.Yaw = Yaw;
        }

        public override bool Equals(object obj)
        {
            if (obj is LocNode node)
            {
                return (this.Position == node.Position && this.Yaw == node.Yaw);
            }
            else
            {
                return false;
            }
        }

        // Just to get rid of annoying message
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}