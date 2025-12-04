// using ExampleMod.Content.Dusts; This is required for dusts which are currently not within the scope of where I am at in the project
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GraniteAddition.Content.Projectiles
{
	public class GraniteSpearProjectile : ModProjectile
	{
		// Define the range of the Spear Projectile. These are overridable properties
		protected virtual float HoldoutRangeMin => 35f;
		protected virtual float HoldoutRangeMax => 110f;

		public override void SetDefaults() {
			Projectile.CloneDefaults(ProjectileID.Spear); // Clone the default values for a vanilla spear. Spear specific values set for width, height, aiStyle, friendly, penetrate, tileCollide, scale, hide, ownerHitCheck, and melee.
			Projectile.scale = 1.3f; // Change the scale of the projectile to 1f (100% size)
        }

		public override bool PreAI() {
			Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
			int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

			player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

			// Reset projectile time left if necessary
			if (Projectile.timeLeft > duration) {
				Projectile.timeLeft = duration;
			}

			Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

			float halfDuration = duration * 0.5f;
			float progress;

			// Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
			if (Projectile.timeLeft < halfDuration) {
				progress = Projectile.timeLeft / halfDuration;
			}
			else {
				progress = (duration - Projectile.timeLeft) / halfDuration;
			}

			// Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin , Projectile.velocity * HoldoutRangeMax, progress);

            // Apply proper rotation to the sprite.
            if (Projectile.spriteDirection == -1) {
				// If sprite is facing left, rotate 45 degrees
				Projectile.rotation += MathHelper.ToRadians(45f);
			}
			else {
				// If sprite is facing right, rotate 135 degrees	
				Projectile.rotation += MathHelper.ToRadians(135f);
			}

			

            /*
			 * Dust spawning code. Currently unused. Left for possible future use. Turned into comment to remove errors.
			 * 
			// Avoid spawning dusts on dedicated servers
			if (!Main.dedServ) {
				// These dusts are added later, for the 'ExampleMod' effect
				if (Main.rand.NextBool(3)) {
					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Sparkle>(), Projectile.velocity.X * 2f, Projectile.velocity.Y * 2f, Alpha: 128, Scale: 1.2f);
				}

				if (Main.rand.NextBool(4)) {
					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Sparkle>(), Alpha: 128, Scale: 0.3f);
				}
			}
			*/

            return false; // Don't execute vanilla AI.
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }


    }
}
