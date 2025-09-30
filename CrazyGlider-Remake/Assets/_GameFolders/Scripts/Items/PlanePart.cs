using System.Collections.Generic;
using UnityEngine;

namespace _GameFolders.Scripts.Items
{
    public class PlanePart : MonoBehaviour
    {
        public bool IsPurchased;
        public List<PlanePart> requiredPartsToUnlock;
    }
}