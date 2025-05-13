using Godot;
using Godot.Collections;
using System.Collections.Generic;

public partial class Storyline : Node
{
	// Variables + Classes
	public int currentLine = 0;

	private class Line
	{
		public string Classification;
		public string Speaker;
		public string Text;
	}

	private List<Line> Lines = new();

	public void Load()
	//	Loads the .JSON storyline, and parses it into the Lines
	//	variable (which is a list of story-lines).
	{
		string file = FileAccess.GetFileAsString("res://json/storyline.json");
		
		var json = new Json();
		var error = json.Parse(file);

		if (error != Error.Ok)
		{
			GD.PrintErr("Failed to parse JSON!");
			return;
		}

		var root = json.Data.As<Dictionary>(); // Set to dictionary to get keys and values
		if (!root.ContainsKey("storyline")) // Ensure the key
		{
			GD.PrintErr("No 'storyline' key found.");
			return;
		}

		var array = root["storyline"].As<Array>(); // Array of entries

		foreach (var entry in array)
		{
			var dict = entry.As<Dictionary>(); // Each entry has a key value pair
			if (dict != null && dict.ContainsKey("classification") && dict.ContainsKey("speaker") && dict.ContainsKey("line")) // Check keys
			{
				var line = new Line
				{
					Classification = dict["classification"].ToString(),
					Speaker = dict["speaker"].ToString(),
					Text = dict["line"].ToString()
				};
				Lines.Add(line);
			}
		}
	}

	public (string, string, string) Play(string classification, int index)
	//	After the lines are loaded, we can filter based on the ID/classification
	//	and increment within that ID/classification a/k/a the scene.
	{
		var filteredLines = Lines.FindAll(line => line.Classification == classification);

		if (index < filteredLines.Count)
		{
			var line = filteredLines[index];
			return (line.Classification, line.Speaker, line.Text);
		}
		else
		{
			GD.Print($"End of storyline for classification: {classification}.");
		}

		return ("", "", "");
	}

	public override void _Ready()
	{
		Load();
	}

}
