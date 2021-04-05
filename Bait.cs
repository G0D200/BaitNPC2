
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BaitNPC2
{
	[AutoloadHead]
	class Bait : ModNPC
	{
		// Time of day for traveller to leave (6PM)

		// the time of day the traveler will spawn (double.MaxValue for no spawn)
		// saved and loaded with the world in ExampleWorld
		public static double spawnTime = 0;

		// The list of items in the traveler's shop. Saved with the world and reset when a new traveler spawns
		public static List<Item> shopItems = new List<Item>();

		public static NPC FindNPC(int npcType) => Main.npc.FirstOrDefault(npc => npc.type == npcType && npc.active);

		public static void UpdateTravelingMerchant()
		{
			NPC bait = FindNPC(ModContent.NPCType<Bait>()); // Find an Explorer if there's one spawned in the world

			// Main.time is set to 0 each morning, and only for one update. Sundialling will never skip past time 0 so this is the place for 'on new day' code
			if (Main.dayTime && Main.time == 0)
			{
				// insert code here to change the spawn chance based on other conditions (say, npcs which have arrived, or milestones the player has passed)
				// You can also add a day counter here to prevent the merchant from possibly spawning multiple days in a row.

				// NPC won't spawn today if it stayed all night
				if (bait == null && Main.rand.NextBool(4))
				{ // 4 = 25% Chance
				  // Here we can make it so the NPC doesnt spawn at the EXACT same time every time it does spawn
					spawnTime = GetRandomSpawnTime(0, 18000); // minTime = 6:00am, maxTime = 7:30am
				}
				else
				{
					spawnTime = double.MaxValue; // no spawn today
				}
			}

			// Spawn the traveler if the spawn conditions are met (time of day, no events, no sundial)
			if (bait == null && CanSpawnNow())
			{
				int newTraveler = NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, ModContent.NPCType<Bait>(), 1); // Spawning at the world spawn
				bait.homeless = false;
				bait.direction = Main.spawnTileX >= WorldGen.bestX ? -1 : 1;
				bait.netUpdate = true;

				// Prevents the traveler from spawning again the same day

				// Annouce that the traveler has spawned in!
				if (Main.netMode == NetmodeID.SinglePlayer) Main.NewText(Language.GetTextValue("Announcement.HasArrived", bait.FullName), 50, 125, 255);
				else NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasArrived", bait.GetFullNetName()), new Color(50, 125, 255));
			}
		}

		private static bool CanSpawnNow()
		{
			// can't spawn if any events are running
			if (Main.eclipse || Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0)
				return false;

			// can't spawn if the sundial is active
			if (Main.fastForwardTime)
				return true;

			// can spawn if daytime, and between the spawn and despawn times
			return Main.dayTime;
		}

		public static double GetRandomSpawnTime(double minTime, double maxTime)
		{
			// A simple formula to get a random time between two chosen times
			return (maxTime - minTime) * Main.rand.NextDouble() + minTime;
		}

		public override void SetupShop(Chest shop, ref int nextSlot)       //Allows you to add items to this town NPC's shop. Add an item by setting the defaults of shop.item[nextSlot] then incrementing nextSlot.
		{
			shop.item[nextSlot].SetDefaults(ItemID.MonarchButterfly);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Snail);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Scorpion);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.SulphurButterfly);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Grasshopper);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.ApprenticeBait);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.BlackScorpion);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.ZebraSwallowtailButterfly);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.GlowingSnail);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Grubby);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.BlueJellyfish);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.GreenJellyfish);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.PinkJellyfish);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Firefly);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.UlyssesButterfly);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.JuliaButterfly);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Sluggy);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Worm);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.JourneymanBait);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.RedAdmiralButterfly);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.PurpleEmperorButterfly);
			nextSlot++;
			if (Main.hardMode == true)
            {
				shop.item[nextSlot].SetDefaults(ItemID.LightningBug);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(ItemID.EnchantedNightcrawler);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.Buggy);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.MasterBait);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.TreeNymphButterfly);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.GoldButterfly);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.GoldWorm);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.GoldGrasshopper);
			nextSlot++;
			if (NPC.downedMechBoss2 == true && NPC.downedMechBoss1 == true && NPC.downedMechBoss3 == true)
            {
				shop.item[nextSlot].SetDefaults(ItemID.TruffleWorm);
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(mod.ItemType("CustomSword"));  //this is an example of how to add a modded item
			nextSlot++;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bait Merchant");
			Main.npcFrameCount[npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 700;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 90;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true; // This will be changed once the NPC is spawned
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Guide;
		}

		public static TagCompound Save()
		{
			return new TagCompound
			{
				["spawnTime"] = spawnTime,
				["shopItems"] = shopItems
			};
		}

		public static void Load(TagCompound tag)
		{
			spawnTime = tag.GetDouble("spawnTime");
			shopItems = tag.Get<List<Item>>("shopItems");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			int num = npc.life > 0 ? 1 : 5;
			for (int k = 0; k < num; k++)
			{
				
			}
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return true; // This should always be false, because we spawn in the Travleing Merchant manually
		}

		public override string TownNPCName()
		{
			switch (Main.rand.Next(4))
			{
				case 0:
					return "Jack";
				case 1:
					return "James";
				case 2:
					return "Buggy";
				default:
					return "Kevin";
			}
		}

		public override string GetChat()
		{
			switch (Main.rand.Next(5))
			{
				case 0:
					return "Do you know how hard catching bait is? It's a nightmare, they always run away right before you catch them!";
				case 1:
					return "You ever heard of the rare and elusive Gold Butterfly? Oh, you haven't? Well, we have them for sale at the low, low price of 50 Gold!";
				case 2:
					return "You ever heard of the rare and elusive Gold Grasshopper? Oh, you haven't? Well, we have them for sale at the low, low price of 50 Gold!";
				case 3:
					return "You ever heard of the rare and elusive Gold Worm? Oh, you haven't? Well, we have them for sale at the low, low price of 50 Gold!";
				/*case 2:
					{
						// Main.npcChatCornerItem shows a single item in the corner, like the Angler Quest chat.
						Main.npcChatCornerItem = ItemID.HiveBackpack;
						return $"Hey, if you find a [i:{ItemID.HiveBackpack}], my cousin can upgrade it for you.";
					}*/
				default:
					return $"Hey, want some bait?";
			}
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
		}
		public override void AI()
		{
			npc.homeless = false; // Make sure it stays homeless
		}

		public override void NPCLoot()
		{
			
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 30;
			randExtraCooldown = 30;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
		{
			projType = ProjectileID.WoodenArrowFriendly;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
		{
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}
