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
	private GameControl gc;

	public override void _Ready()
	{
		text = GetNode<RichTextLabel>("DialogText");
		gc = GetNode<GameControl>("/root/GameControl");
	}
	
	public void AdvanceDialogue()
	{
		if (dialogueIndex < dialogue.Length)
		{
			gc.interacting = true;
			text.BbcodeText = dialogue[dialogueIndex];
			dialogueIndex++;
			if (dialogueIndex == dialogue.Length)
			{
				finished = true;
			}
		}
	}
}

