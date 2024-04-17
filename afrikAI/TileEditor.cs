using System.Diagnostics;

namespace afrikAI
{
	public class TileEditor
	{
		private InputHandler inputHandler;
		private const ConsoleColor SELECTEDCOLOR = ConsoleColor.Green;
		private TileManager tileManager;
		private int width,height;
		private string name;
		private bool isZebraOnMap;
		private int x { get => Console.CursorLeft/2-1; }
		private int y { get => Console.CursorTop; }
        public TileEditor(string filePath) {
			name = filePath;
			Console.ResetColor();
			inputHandler = new InputHandler(this);
			tileManager = new TileManager(name, ref width, ref height);
			try
			{
				_ = tileManager.GetZebra();
				isZebraOnMap = true;
			}
			catch (Exception) {isZebraOnMap = false;}
			tileManager.DrawTiles();
			inputHandler.HandleEditorInput();
			
			
		}
		public void MoveCursor(char dir)
		{
			int[] moveMatrix = Statics.moveMatrixes[dir];
			tileManager.DrawTile(x, y); // reset last tile
			tileManager.DrawTile(Math.Max(Math.Min(x + moveMatrix[0], width-1), 0), Math.Max(Math.Min(y + moveMatrix[1], height-1), 0), SELECTEDCOLOR);
		}
		public void EditTile(string tileTpye)
		{
			if (isZebraOnMap && tileTpye == "zebra") return;
			tileManager.SetTileTpye(x, y, tileTpye);
		}
		public void Save()
		{
			if(isValidMap()) tileManager.SaveTiles(name);
		}
		public void ChangeTypeUp()
		{
			string currType = tileManager.GetTileType(x, y);
			if (isZebraOnMap && currType == "lion") tileManager.AddToTile(x, y, 2);
			else
			{
				if (currType == "zebra") isZebraOnMap = false;
				else if (currType == "lion" && !isZebraOnMap) isZebraOnMap = true;
				tileManager.AddToTile(x, y, 1); 
			}
			tileManager.DrawTile(x,y);
		}
		public void ChangeTypeDown()
		{
			string currType = tileManager.GetTileType(x, y);
			if (isZebraOnMap && currType == "ground") tileManager.AddToTile(x, y, -2);
			else
			{
				if (currType == "zebra") isZebraOnMap = false;
				else if(currType == "ground") isZebraOnMap = true;
				tileManager.AddToTile(x, y, -1);
			}
			tileManager.DrawTile(x, y);
		}
		public void Next()
		{
			TileManager lastSaved = new TileManager(name);
			if(isValidMap()) 
			{ 	
				Menu menu = new Menu(); 
			}
		}
		public void Quit() => Environment.Exit(0);
		private bool isValidMap()
		{
			TileGeneratorData tileData = tileManager.GetTilesData();
			if (!isZebraOnMap)
			{
				writeError("Cannot save because no zebra is on the map.");
				return false;
			}
			if (tileData.Water < 0)
			{
				writeError("Cannot save because no water is on the map.");
				return false;
			}
			return true;
		}
		private void writeError(string errorMessage)
		{
			Console.SetCursorPosition(0, height + 2);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(errorMessage);
			Console.ResetColor();
		}
	}
}
