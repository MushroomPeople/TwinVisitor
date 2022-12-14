using Godot;
using System;


public class InteractableNPC : Area
{
	private RichTextLabel dialogText;
	public DialogBox dialogBox;
	private GameControl gc;
	private AudioStreamPlayer asp;
	
	public override void _Ready()
	{
		dialogBox = GetNode<DialogBox>("DialogBox");
		dialogText = GetNode<RichTextLabel>("DialogBox/DialogText");
		gc = GetNode<GameControl>("/root/GameControl");
		asp = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}

	private void _on_Item_body_entered(Node body)
	{
		dialogText.Visible = true;
		if (dialogBox.finished == false)
		{
			if (dialogBox.Visible == false)
				dialogBox.Visible = true;
			asp.Play();
			dialogBox.Call("AdvanceDialogue");
		}
		else
		{
			dialogBox.Visible = false;
			dialogBox.finished = false;
			dialogBox.dialogueIndex = 0;
			dialogText.Visible = false;
			gc.interacting = false;
		}
		
		body.GetNode<CollisionShape>("InteractHitBox").Disabled = true;
	}
}

