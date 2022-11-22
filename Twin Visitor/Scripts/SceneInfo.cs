using Godot;
using System;

public class SceneInfo : Spatial
{
	[Export]
	public string sceneName = "";
	[Export]
	public string scenePath = "";
	[Export]
	public Transform activeSpawn = DataTools.ArrayToTransform(new float[] {1, 0, 0, 0, 1, 0, 0, 0, 1, 4, 1.5f, 0});
	[Export]
	public Transform inactiveSpawn = DataTools.ArrayToTransform(new float[] {1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1.5f, 0});

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<GameControl>("/root/GameControl").currentSceneName = sceneName;
		//GetNode<GameControl>("/root/GameControl").currentScene = sceneToLoad;
		
		if (GetNode<PlayerA>("/root/GameControl/PlayerA").active)
		{
			GetNode<PlayerA>("/root/GameControl/PlayerA").GlobalTransform = activeSpawn;
			GetNode<PlayerB>("/root/GameControl/PlayerB").GlobalTransform = inactiveSpawn;
		}
		else
		{
			GetNode<PlayerB>("/root/GameControl/PlayerB").GlobalTransform = activeSpawn;
			GetNode<PlayerA>("/root/GameControl/PlayerA").GlobalTransform = inactiveSpawn;
		}
	}
}
