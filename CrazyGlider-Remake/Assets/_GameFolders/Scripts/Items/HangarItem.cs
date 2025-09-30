using System.Collections.Generic;
using UnityEngine;

namespace _GameFolders.Scripts.Items
{
    public class HangarItem : MonoBehaviour
    {
        [SerializeField] private PlanePart motor;
        [SerializeField] private PlanePart wings;
        [SerializeField] private PlanePart wheels;
        [SerializeField] private PlanePart cover;
        [SerializeField] private PlanePart tail;
        
        public PlanePart Motor => motor;
        public PlanePart Wings => wings;
        public PlanePart Wheels => wheels;
        public PlanePart Cover => cover;
        public PlanePart Tail => tail;

        public List<PlanePart> AllParts()
        {
            List<PlanePart> allParts = new List<PlanePart>();
            allParts.Add(motor);
            allParts.Add(wings);
            allParts.Add(wheels);
            allParts.Add(cover);
            allParts.Add(tail);
            return allParts;
        }
    }
}