// Copyright (c) 2017 Leacme (http://leac.me). View LICENSE.md for more information.
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class Main : Spatial {

	public AudioStreamPlayer Audio { get; } = new AudioStreamPlayer();

	private PackedScene bubs = GD.Load<PackedScene>("res://scenes/Bubble.tscn");

	private void InitSound() {
		if (!Lib.Node.SoundEnabled) {
			AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), true);
		}
	}

	public override void _Notification(int what) {
		if (what is MainLoop.NotificationWmGoBackRequest) {
			GetTree().ChangeScene("res://scenes/Menu.tscn");
		}
	}

	public override void _Ready() {
		var env = GetNode<WorldEnvironment>("sky").Environment;
		env.BackgroundColor = new Color(Lib.Node.BackgroundColorHtmlCode);
		env.GlowEnabled = true;
		env.GlowIntensity = 20;
		env.GlowStrength = 1.1f;
		env.GlowBicubicUpscale = true;
		env.DofBlurFarEnabled = true;
		env.DofBlurFarDistance = 0.01f;
		env.DofBlurFarTransition = 0.01f;
		env.DofBlurFarAmount = 0.13f;
		PhysicsServer.AreaSetParam(GetWorld().Space, PhysicsServer.AreaParameter.GravityVector, new Vector3(0, 0.1f, 0));

		InitSound();
		AddChild(Audio);

		var timers = new List<System.Timers.Timer>(Enumerable.Range(1, 10).Select(z => {
			var timer = new System.Timers.Timer() { Enabled = true, AutoReset = true, Interval = (float)GD.RandRange(1, 5) * 1000 };
			timer.Elapsed += (zz, zzz) => {
				AddChild(bubs.Instance());
				timer.Interval = (float)GD.RandRange(1, 5) * 1000;
			};
			return timer;
		}));

	}

}
