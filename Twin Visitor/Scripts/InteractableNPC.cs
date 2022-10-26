using Godot;
using System;


public class InteractableNPC : Area
{
	private RichTextLabel dialogText;
	//public PopupPanel popupPanel;
	public DialogBox dialogBox;
	//public PopupPanel dialogBox;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//popupPanel = GetNode<PopupPanel>("DialogBox");
		dialogBox = GetNode<DialogBox>("DialogBox");
		//dialogBox = GetNode<PopupPanel>("DialogBox");
		dialogText = GetNode<RichTextLabel>("DialogBox/DialogText");
		//dialogText.Visible = false;
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
			{
				dialogBox.Call("AdvanceDialogue");
			}
		}
		else
		{
			dialogBox.Visible = false;
			dialogBox.finished = false;
			dialogBox.dialogueIndex = 0;
			dialogText.Visible = false;
			//popupPanel.Visible = false;
		}
		
		//if (dialogBox.finished == true)
		//{
		//	if (dialogBox.Visible == true)
		//	{
		//		dialogBox.Visible = false;
		//	}
		//}
		
		//if (dialogBox.Call("HandleDialog") == true)
		//{
		//	dialogBox.Call("AdvanceDialog");
		//}
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
	}
}

