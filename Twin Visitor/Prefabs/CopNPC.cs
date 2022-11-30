using Godot;
using System;


public class CopNPC : Area
{
	private RichTextLabel dialogText;
	public DialogBox dialogBox;
	private GameControl gc;
	private bool once = true;
	public bool delay = false;
	
	public override void _Ready()
	{
		dialogBox = GetNode<DialogBox>("DialogBox");
		dialogText = GetNode<RichTextLabel>("DialogBox/DialogText");
		gc = GetNode<GameControl>("/root/GameControl");
	}

	private void _on_Item_body_entered(Node body)
	{
		if (delay)
		{
			delay = false;
		}
		else
		{
			dialogText.Visible = true;
			if (dialogBox.finished == false)
			{
				if (dialogBox.Visible == false)
					dialogBox.Visible = true;
				dialogBox.Call("AdvanceDialogue");
			}
			else
			{
				if (once)
				{
					GetNode<Keycard>("../Keycard").spawn = true;
					once = false;
				}
			
				dialogBox.Visible = false;
				dialogBox.finished = false;
				dialogBox.dialogueIndex = 0;
				dialogText.Visible = false;
				gc.interacting = false;
			}
		}
				
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
	}
}

