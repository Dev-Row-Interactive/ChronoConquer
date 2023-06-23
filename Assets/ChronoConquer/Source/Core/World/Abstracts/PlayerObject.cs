using UnityEngine;

namespace DevRowInteractive.ChronoConquer.Source.Core.World.Abstracts
{
    public abstract class PlayerObject : WorldObject
    {
        [HideInInspector] public int CurrentHitPoints;
        public int InitialHitpoints;
        public int AttackDamage;

        public override void Awake()
        {
            base.Awake();
            CurrentHitPoints = InitialHitpoints;
        }
    }
}