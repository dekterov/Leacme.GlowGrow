using Godot;
using System;

public class Bubble : Spatial {

	public override void _Ready() {

		Translate(new Vector3((float)GD.RandRange(5, 10), 0, (float)GD.RandRange(0, 10)));
		var bub = GetNode<MeshInstance>("BubblePhysics/BubbleCollision/Bubble");
		var mat = new SpatialMaterial();
		mat.AlbedoColor = Color.FromHsv(GD.Randf(), 1, 1);
		mat.EmissionEnabled = true;
		mat.Emission = Color.FromHsv(GD.Randf(), 1, 1);
		mat.EmissionEnergy = 1;
		bub.MaterialOverride = mat;
	}

	public override void _Process(float delta) {
		if (GetNode<RigidBody>("BubblePhysics").GlobalTransform.origin.y > 20) {
			QueueFree();
		}
	}
}
