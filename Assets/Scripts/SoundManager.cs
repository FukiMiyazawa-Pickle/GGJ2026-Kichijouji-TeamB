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
	[SerializeField] private AudioSource bgmSource;
	[SerializeField] private AudioSource seSource;

	[Header("Sound Data")]
	[SerializeField] private SoundData[] bgmList;
	[SerializeField] private SoundData[] seList;

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

		foreach (var data in bgmList)
		{
			if (!bgmDict.ContainsKey(data.tag))
				bgmDict.Add(data.tag, data.clip);
			else
				Debug.LogWarning($"BGMタグ重複: {data.tag}");
		}

		foreach (var data in seList)
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

		if (bgmSource.clip == clip) return;

		bgmSource.clip = clip;
		bgmSource.loop = loop;
		bgmSource.Play();
	}

	// ===== SE =====
	public void PlaySE(string tag)
	{
		if (!seDict.TryGetValue(tag, out var clip))
		{
			Debug.LogWarning($"SEタグが存在しません: {tag}");
			return;
		}

		seSource.PlayOneShot(clip);
	}
}
