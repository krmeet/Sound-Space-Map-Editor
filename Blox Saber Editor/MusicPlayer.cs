﻿using System;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
using System.IO;
using Blox_Saber_Editor.SoundTouch;
using NAudio.Wave;
>>>>>>> parent of 2cde2dc (fixes)
=======
using System.IO;
using Blox_Saber_Editor.SoundTouch;
using NAudio.Wave;
>>>>>>> parent of 2cde2dc (fixes)
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Fx;
=======
using Blox_Saber_Editor.SoundTouch;
using NAudio.Wave;
>>>>>>> parent of eea8ff6 (Bass.NET instead of NAudio and OpenAL)

namespace Blox_Saber_Editor
{
	class MusicPlayer : IDisposable
	{
		private WaveStream _music;
		private WaveChannel32 _volumeStream;
		private WaveOutEvent _player;
		private VarispeedSampleProvider _speedControl;
		
		private readonly BetterTimer _time;

		private object locker = new object();

		public MusicPlayer()
		{
			_player = new WaveOutEvent();
			_time = new BetterTimer();
		}

		public void Load(string file)
		{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
			var stream = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_STREAM_PRESCAN | BASSFlag.BASS_FX_FREESOURCE);
			var volume = Volume;
			var tempo = Tempo;
=======
			_music?.Dispose();
			_volumeStream?.Dispose();
			_player?.Dispose();
			_speedControl?.Dispose();
>>>>>>> parent of eea8ff6 (Bass.NET instead of NAudio and OpenAL)

			var reader = new AudioFileReader(file);
			_music = reader;
			_volumeStream = new WaveChannel32(_music, Volume, 0);
			_player = new WaveOutEvent();

<<<<<<< HEAD
			Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_BUFFER, 250);
			Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_UPDATEPERIOD, 5);

			streamID = BassFx.BASS_FX_TempoCreate(stream, BASSFlag.BASS_STREAM_PRESCAN);
=======
			_speedControl = new VarispeedSampleProvider(reader, 150, new SoundTouchProfile(true, true));
>>>>>>> parent of eea8ff6 (Bass.NET instead of NAudio and OpenAL)
=======
			var stream = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_FX_FREESOURCE);
			var volume = Volume;
			var tempo = Tempo;
=======
			var stream = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_FX_FREESOURCE);
			var volume = Volume;
			var tempo = Tempo;

			Bass.BASS_StreamFree(streamID);

			streamID = BassFx.BASS_FX_TempoCreate(stream, BASSFlag.BASS_DEFAULT);
>>>>>>> parent of 2cde2dc (fixes)

			Bass.BASS_StreamFree(streamID);

			streamID = BassFx.BASS_FX_TempoCreate(stream, BASSFlag.BASS_DEFAULT);
>>>>>>> parent of 2cde2dc (fixes)

			Init();

			Reset();
		}

		public void Init() => _player.Init(_speedControl);
		public void Play()
		{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
			Bass.BASS_ChannelPlay(streamID, false);
=======
			lock (locker)
			{
				var time = CurrentTime;
=======
=======
>>>>>>> parent of 2cde2dc (fixes)
			//lock (locker)
			{
				Bass.BASS_ChannelPlay(streamID, false);
			}
		}
>>>>>>> parent of 2cde2dc (fixes)

				if (Progress >= 0.999998)
				{
					time = TimeSpan.Zero;
				}

				Stop();

				CurrentTime = time;

				_player.Play();
				_time.Start();
			}
>>>>>>> parent of eea8ff6 (Bass.NET instead of NAudio and OpenAL)
		}
		public void Pause()
		{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
			var pos = Bass.BASS_ChannelGetPosition(streamID, BASSMode.BASS_POS_BYTES);

			Bass.BASS_ChannelPause(streamID);

			Bass.BASS_ChannelSetPosition(streamID, pos, BASSMode.BASS_POS_BYTES);
=======
			lock (locker)
			{
				_time.Stop();
				_player.Pause();
			}
>>>>>>> parent of eea8ff6 (Bass.NET instead of NAudio and OpenAL)
=======
=======
>>>>>>> parent of 2cde2dc (fixes)
			//lock (locker)
			{
				Stop();
			//	Bass.BASS_ChannelPause(streamID);
			}
<<<<<<< HEAD
>>>>>>> parent of 2cde2dc (fixes)
=======
>>>>>>> parent of 2cde2dc (fixes)
		}
		public void Stop()
		{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
			Bass.BASS_ChannelStop(streamID);

			Bass.BASS_ChannelSetPosition(streamID, 0, BASSMode.BASS_POS_BYTES);
=======
			lock (locker)
			{
				_time.Reset();
				_player.Stop();
			}
>>>>>>> parent of eea8ff6 (Bass.NET instead of NAudio and OpenAL)
=======
			var pos = Bass.BASS_ChannelGetPosition(streamID, BASSMode.BASS_POS_BYTES);

			Bass.BASS_ChannelStop(streamID);

			Bass.BASS_ChannelSetPosition(streamID, pos, BASSMode.BASS_POS_BYTES);
>>>>>>> parent of 86a6c55 (optimizations, redesign, autoplay)
=======
=======
>>>>>>> parent of 2cde2dc (fixes)
			//lock (locker)
			{
				Bass.BASS_ChannelStop(streamID);
			}
<<<<<<< HEAD
>>>>>>> parent of 2cde2dc (fixes)
=======
>>>>>>> parent of 2cde2dc (fixes)
		}

		public float Speed
		{
			get => _speedControl?.PlaybackRate ?? 1;

			set
			{
				lock (locker)
				{
					var wasPlaying = IsPlaying;

					Pause();
					_time.SetSpeed(value);
					var time = _time.Elapsed;
					Stop();

					_speedControl.PlaybackRate = value;

					CurrentTime = time;

					Init();

					if (wasPlaying)
						Play();
				}
			}
		}

		public float Volume
		{
			get => _player.Volume;

			set => _player.Volume = value;
		}

		public void Reset()
		{
			Stop();

			_music.CurrentTime = TimeSpan.Zero;
		}
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
		
=======

>>>>>>> parent of 2cde2dc (fixes)
=======

>>>>>>> parent of 2cde2dc (fixes)
		public bool IsPlaying => Bass.BASS_ChannelIsActive(streamID) == BASSActive.BASS_ACTIVE_PLAYING;
		public bool IsPaused => Bass.BASS_ChannelIsActive(streamID) == BASSActive.BASS_ACTIVE_PAUSED;
=======

		public bool IsPlaying => _player.PlaybackState == PlaybackState.Playing;
		public bool IsPaused => _player.PlaybackState == PlaybackState.Paused;
>>>>>>> parent of eea8ff6 (Bass.NET instead of NAudio and OpenAL)

		public TimeSpan TotalTime => _music?.TotalTime ?? TimeSpan.Zero;

		public TimeSpan CurrentTime
		{
			get
			{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
				var pos = Bass.BASS_ChannelGetPosition(streamID, BASSMode.BASS_POS_BYTES);
				var length = Bass.BASS_ChannelGetLength(streamID, BASSMode.BASS_POS_BYTES);

				return TimeSpan.FromTicks((long)(TotalTime.Ticks * pos / (decimal)length));
=======
				lock (locker)
				{
					if (_music == null)
						return TimeSpan.Zero;

					var time = _time.Elapsed;

					time = time > _music.TotalTime ? TotalTime : time;

					return time;
				}
>>>>>>> parent of eea8ff6 (Bass.NET instead of NAudio and OpenAL)
=======
				long pos = Bass.BASS_ChannelGetPosition(streamID, BASSMode.BASS_POS_BYTES);
				var time = TimeSpan.FromSeconds(Bass.BASS_ChannelBytes2Seconds(streamID, pos));

				return time;
>>>>>>> parent of 2cde2dc (fixes)
=======
				long pos = Bass.BASS_ChannelGetPosition(streamID, BASSMode.BASS_POS_BYTES);
				var time = TimeSpan.FromSeconds(Bass.BASS_ChannelBytes2Seconds(streamID, pos));

				return time;
>>>>>>> parent of 2cde2dc (fixes)
			}
			set
			{
				lock (locker)
				{
					if (_music == null)
						return;

					Stop();

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
		public decimal Progress
=======
		public double Progress
>>>>>>> parent of 2cde2dc (fixes)
=======
		public double Progress
>>>>>>> parent of 2cde2dc (fixes)
		{
			get
			{
				var pos = Bass.BASS_ChannelGetPosition(streamID, BASSMode.BASS_POS_BYTES);
				var length = Bass.BASS_ChannelGetLength(streamID, BASSMode.BASS_POS_BYTES);

<<<<<<< HEAD
<<<<<<< HEAD
				return pos / (decimal)length;
=======
					_music.CurrentTime = value;
					_time.Elapsed = value;

					_speedControl.Reposition();

					Pause();
				}
>>>>>>> parent of eea8ff6 (Bass.NET instead of NAudio and OpenAL)
=======
				return (double)(pos / (decimal)length);
>>>>>>> parent of 2cde2dc (fixes)
=======
				return (double)(pos / (decimal)length);
>>>>>>> parent of 2cde2dc (fixes)
			}
		}

		public double Progress => TotalTime == TimeSpan.Zero ? 0 : Math.Min(1, CurrentTime.TotalMilliseconds / TotalTime.TotalMilliseconds);

		public void Dispose()
		{
			_player?.Dispose();
			_speedControl?.Dispose();
			_music?.Dispose();
			_volumeStream?.Dispose();
		}
	}
}