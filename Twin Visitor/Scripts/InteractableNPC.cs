using Godot;
using System;


public class InteractableNPC : Area
{
	private RichTextLabel dialogText;
	public DialogBox dialogBox;
	
	public override void _Ready()
	{
		dialogBox = GetNode<DialogBox>("DialogBox");
		dialogText = GetNode<RichTextLabel>("DialogBox/DialogText");
	}

	private void _on_Item_body_entered(Node body)
	{
		dialogText.Visible = true;
		if (dialogBox.finished == false)
		{
			if (dialogBox.Visible == false)
			{
				dialogBox.Visible = true;
				dialogBox.Call("AdvanceDialogue");
			}
			else
				dialogBox.Call("AdvanceDialogue");
		}
		else
		{
			dialogBox.Visible = false;
			dialogBox.finished = false;
			dialogBox.dialogueIndex = 0;
			dialogText.Visible = false;
		}
		
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
	}
}

