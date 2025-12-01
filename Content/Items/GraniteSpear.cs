using Terraria;
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

			Item.DefaultToSpear(ModContent.ProjectileType<Projectiles.GraniteSpearProjectile>(), 22, 22);
            Item.damage = 22;
			Item.crit = 5;
			Item.knockBack = 6;
			Item.value = Item.sellPrice(3200);
			Item.rare = ItemRarityID.Green;
			//Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
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
