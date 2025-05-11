using Godot;
using Godot.Collections;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class storyline : Node
{
	public int currentLine = 0;

	private class Line
	{
		public string Speaker;
		public string Text;
	}

	private List<Line> lines = new();

	public void Load()
	{
		string file = FileAccess.GetFileAsString("res://json/storyline.json");
		
		var json = new Json();
		var error = json.Parse(file);

		if (error != Error.Ok)
		{
			GD.PrintErr("Failed to parse JSON!");
			return;
		}

		var root = json.Data.As<Dictionary>();
		if (!root.ContainsKey("storyline"))
		{
			GD.PrintErr("No 'storyline' key found.");
			return;
		}

		var array = root["storyline"].As<Godot.Collections.Array>();

		foreach (var entry in array)
		{
			var dict = entry.As<Dictionary>();
			if (dict != null && dict.ContainsKey("speaker") && dict.ContainsKey("line"))
			{
				var line = new Line
				{
					Speaker = dict["speaker"].ToString(),
					Text = dict["line"].ToString()
				};
				lines.Add(line);
			}
		}
	}

	public (string, string) Play(int index)
	{
		if (index < lines.Count)
		{
			var line = lines[index];
			return (line.Speaker, line.Text);
		}
		else
		{
			GD.Print("End of storyline.");
		}

		return ("", "");
	}

	public override void _Ready()
	{
		Load();
	}

}
