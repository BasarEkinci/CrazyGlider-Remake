using System.Collections.Generic;
using UnityEngine;

namespace _GameFolders.Scripts.Items
{
    public class PlanePartHolder : MonoBehaviour
    {
        [SerializeField] private List<PlanePart> planeParts;
        public List<PlanePart> PlaneParts => planeParts;
    }
}