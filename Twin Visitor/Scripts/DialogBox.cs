using Godot;
using System;


public class DialogBox : Control
{
	[Export]
	public string[] dialogue = {"Hey, this is the default dialogue.",
						  "This shouldn't be in the final game. If you see this, make note of where you found it.",
						  "Thank you!"};
	public bool finished = false;
	public int dialogueIndex = 0;
	private RichTextLabel text;

	public override void _Ready()
	{
		text = GetNode<RichTextLabel>("DialogText");
		//LoadDialogue();
	}

	public override void _Process(float delta)
	{
		//if (Input.IsActionJustPressed("Interact"))
		//{
		//	
		//}
	}
	
	public void AdvanceDialogue()
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

