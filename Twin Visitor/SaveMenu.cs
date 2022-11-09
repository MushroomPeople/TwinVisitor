using Godot;
using System;

public class SaveMenu : Control
{
	public override void _Ready()
	{
		var file1 = Load.GetData("file1.sav");
		var file2 = Load.GetData("file2.sav");
		var file3 = Load.GetData("file3.sav");
	}
}
