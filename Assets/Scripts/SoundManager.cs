using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SoundData
{
	public string tag;
	public AudioClip clip;
}


public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	[Header("Audio Sources")]
	[SerializeField] private AudioSource _bgmSource;
	[SerializeField] private AudioSource _seSource;

	[Header("Sound Data")]
	[SerializeField] private SoundData[] _bgmList;
	[SerializeField] private SoundData[] _seList;

	private Dictionary<string, AudioClip> bgmDict;
	private Dictionary<string, AudioClip> seDict;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);

		InitializeDictionary();
	}

	private void InitializeDictionary()
	{
		bgmDict = new Dictionary<string, AudioClip>();
		seDict = new Dictionary<string, AudioClip>();

		foreach (var data in _bgmList)
		{
			if (!bgmDict.ContainsKey(data.tag))
				bgmDict.Add(data.tag, data.clip);
			else
				Debug.LogWarning($"BGMタグ重複: {data.tag}");
		}

		foreach (var data in _seList)
		{
			if (!seDict.ContainsKey(data.tag))
				seDict.Add(data.tag, data.clip);
			else
				Debug.LogWarning($"SEタグ重複: {data.tag}");
		}
	}

	// ===== BGM =====
	public void PlayBGM(string tag, bool loop = true)
	{
		if (!bgmDict.TryGetValue(tag, out var clip))
		{
			Debug.LogWarning($"BGMタグが存在しません: {tag}");
			return;
		}

		if (_bgmSource.clip == clip) return;

		_bgmSource.clip = clip;
		_bgmSource.loop = loop;
		_bgmSource.Play();
	}

	// ===== SE =====
	public void PlaySE(string tag)
	{
		if (!seDict.TryGetValue(tag, out var clip))
		{
			Debug.LogWarning($"SEタグが存在しません: {tag}");
			return;
		}

		_seSource.PlayOneShot(clip);
	}
}
