using Godot;
using System;


public class DialogBox : Control
{
	[Export]
	string[] dialogue = {"Hey, this is the default dialogue.",
						  "This shouldn't be in the final game. If you see this, make note of where you found it.",
						  "Thank you!"};
	bool finished = false;
	int dialogueIndex = 0;
	private RichTextLabel text;

	public override void _Ready()
	{
		text = GetNode<RichTextLabel>("RichTextLabel");
		LoadDialogue();
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("Interact") && finished == false)
		{
			LoadDialogue();
		}
	}
	
	private void LoadDialogue()
	{
		if (dialogueIndex < dialogue.Length)
		{
			text.BbcodeText = dialogue[dialogueIndex];
			dialogueIndex++;
			if (dialogueIndex == dialogue.Length)
			{
				finished = true;
			}
		}
	}
}

