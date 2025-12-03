using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace GraniteAddition.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class GraniteSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.Spears[Item.type] = true; // This allows the game to recognize our new item as a spear.
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true;
        }

        // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.GraniteAddition.hjson' file.

        public override void SetDefaults()
		{

			Item.DefaultToSpear(ModContent.ProjectileType<Projectiles.GraniteSpearProjectile>(), 10, 22);
			Item.useAnimation = 92;
			Item.useTime = 92;
            Item.damage = 18;
			Item.crit = 5;
			Item.ArmorPenetration = 10;
            Item.knockBack = 6f;
			Item.value = Item.sellPrice(3200);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item15;
            Item.autoReuse = true;
		}
        public override bool CanUseItem(Player player) //copied
        {
            // Ensures no more than one spear can be thrown out. 
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override bool? UseItem(Player player) // copied
        {
            // Because we're skipping sound playback on use animation start, we have to play it ourselves whenever the item is actually used.
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
