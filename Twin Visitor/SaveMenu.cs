using Godot;
using System;

public class SaveMenu : Control
{
	private Button file1Button;
	private Button file2Button;
	private Button file3Button;
	
	public override void _Ready()
	{
		var file1 = Load.GetData("file1.sav");
		var file2 = Load.GetData("file2.sav");
		var file3 = Load.GetData("file3.sav");
		
		file1Button = GetNode<Button>("File1Button");
		file2Button = GetNode<Button>("File2Button");
		file3Button = GetNode<Button>("File3Button");
		
		// track playtime
		
		file1Button.Text = $"File 1 - {file1.currentSceneName} - 00:00";
		file2Button.Text = $"File 2 - {file2.currentSceneName} - 00:00";
		file3Button.Text = $"File 3 - {file3.currentSceneName} - 00:00";
	}
}
