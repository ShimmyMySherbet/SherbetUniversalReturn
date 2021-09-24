using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SherbetUniversalReturn.Models
{
    public class LocationHistory : MonoBehaviour
    {
        public List<LocNode> Nodes = new List<LocNode>();
        public LocNode LastNode
        {
            get
            {
                if (Nodes.Count != 0)
                {
                    return Nodes.Last();
                } else
                {
                    return LocNode.Empty;
                }
            }
        }

        public void AddNew(Vector3 Pos, float Yaw)
        {
            if (Nodes.Count >= 2)
            {
                Nodes.RemoveAt(0);
            }
            Nodes.Add(new LocNode(Pos, Yaw));
        }

    }
}
