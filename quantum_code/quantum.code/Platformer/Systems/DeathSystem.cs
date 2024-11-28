using Photon.Deterministic;


namespace Quantum.Platformer.Systems
{
    public unsafe class DeathSystem : SystemMainThreadFilter<DeathSystem.Filter>
    {

        public struct Filter
        {
            public EntityRef Entity;
            public Health* Health;

        }

        public override void Update(Frame f, ref Filter filter)
        {
            if (filter.Health->Value > 0)
                return;

            f.Destroy(filter.Entity);
        }

    }
}
