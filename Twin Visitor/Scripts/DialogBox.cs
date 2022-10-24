using Godot;
using System;


public class DialogBox : Control
{
	// Get array of strings
	string[] dialogue = {"Hey, this is the default dialogue.",
								"This shouldn't be in the final game. If you see this, make note of where you found it.",
								"Thank you!"};
	bool finished = false;
	int dialogueIndex = 0;
	private RichTextLabel text;

	public override void _Ready()
	{
		text = GetNode<RichTextLabel>("RichTextLabel");
		load_dialogue();
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("Interact"))
		{
			load_dialogue();
		}
	}
	
	private void load_dialogue()
	{
		if (dialogueIndex < dialogue.Length)
		{
			text.bbcode_text = dialogue[dialogueIndex];
			dialogueIndex++;
		}
	}
}

