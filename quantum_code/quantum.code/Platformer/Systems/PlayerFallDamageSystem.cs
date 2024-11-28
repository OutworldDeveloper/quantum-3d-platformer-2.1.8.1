using Photon.Deterministic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.Platformer.Systems
{
    public unsafe class PlayerFallDamageSystem : SystemMainThreadFilter<PlayerFallDamageSystem.Filter>
    {

        public const int MAX_SAFE_FALL_DISTANCE = 4;

        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform3D;
            public CharacterController3D* CharacterController;
            public PlayerFallingController* FallingController;
            public Health* Health;

        }

        public override void Update(Frame f, ref Filter filter)
        {
            bool isFalling = filter.FallingController->Falling;
            bool isGrounded = filter.CharacterController->Grounded;

            if (isFalling)
            {
                // Set peak altitude
                filter.FallingController->PeakY = 
                    FPMath.Max(filter.FallingController->PeakY, filter.Transform3D->Position.Y);

                if (isGrounded)
                {
                    filter.FallingController->Falling = false;

                    //
                    FP startY = filter.FallingController->PeakY;
                    FP finalY = filter.Transform3D->Position.Y;

                    FP fallDistance = startY - finalY;
                    bool applyDamage = fallDistance > MAX_SAFE_FALL_DISTANCE;

                    if (applyDamage)
                    {
                        FP fallDamage = fallDistance; // Fancy damage formula goes here
                        filter.Health->Value -= fallDamage;
                        f.Events.Damaged(fallDamage);
                    }
                }
            }
            else
            {
                if (!isGrounded)
                {
                    filter.FallingController->Falling = true;
                }
            }
        }

    }
}
