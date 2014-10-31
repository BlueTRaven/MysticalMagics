using TAPI;
using Terraria;

namespace MysticalMagics
{
    class MWorld : ModWorld
    {
        public int version = 1;
        public static bool SarcophogusBeaten = false;

        public override void WorldGenModifyTaskList(System.Collections.Generic.List<WorldGenTask> list)
        {
            list.Add(new WorldGenTask.Action("MysticalMagics:Flint", () =>
            {
                Main.statusText = "Adding Flint Ore...";
                int SpawnRate = 100 + (int)(Main.maxTilesY / 5);
                for (int i = 0; i < SpawnRate; i++)
                    AddOre();
            }));
        }

        public void AddOre()
        {
            int LeftX = 100;
            int RightX = Main.maxTilesX - 100;
            int TopY = (int)Main.rockLayer;
            int BottomY = Main.maxTilesY - 50;

            int X = WorldGen.genRand.Next(LeftX, RightX);
            int Y = WorldGen.genRand.Next(TopY, BottomY);
            
            int OreMinimumSpread = 6;
            int OreMaximumSpread = 12;

            int OreMinimumFrequency = 6;
            int OreMaximumFrequency = 12;

            int OreSpread = WorldGen.genRand.Next(OreMinimumSpread, OreMaximumSpread + 1);
            int OreFrequency = WorldGen.genRand.Next(OreMinimumFrequency, OreMaximumFrequency + 1);

            WorldGen.OreRunner(X, Y, (double)OreSpread, OreFrequency, TileDef.byName["MysticalMagics:Flint"]);
        }

        //<summary>
        //loads/saves stuff
        //</summary>
        //does that even do anything :I

        public void ClearFlags()
        {
            SarcophogusBeaten = false;
        }

        public override void Save(BinBuffer bb)
        {
            bb.Write(version);
            bb.Write(SarcophogusBeaten);
        }

        public override void Load(BinBuffer bb)
        {
            ClearFlags();
            int v = bb.ReadInt();
            if (v >= 1)
            {
                SarcophogusBeaten = bb.ReadBool();
            }
        }

        public override void WorldGenPostGen()
        {
            ClearFlags();
        }
    }
}
