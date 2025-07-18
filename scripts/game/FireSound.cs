using Godot;

public partial class FireSound : AudioStreamPlayer {
	[Export] private AudioStream[] m_dryFireSounds = new AudioStream[0];
	[Export] private AudioStream[] m_fireSounds = new AudioStream[0];

	public void PlayDryFire() => Play(m_dryFireSounds);
	public void PlayFire() => Play(m_fireSounds);

	private void Play(AudioStream[] sounds) {
		Stream = sounds[GD.RandRange(0, sounds.Length - 1)];
		PitchScale = (float)GD.RandRange(0.8f, 1.2f);
		Play();
	}
}
