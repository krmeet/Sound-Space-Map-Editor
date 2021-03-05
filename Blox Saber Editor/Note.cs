﻿using System;
using System.Drawing;

namespace Blox_Saber_Editor
{
	[Serializable]
	class Note
	{
		public int X;
		public int Y;
		public long Ms;
		public long DragStartMs;

		public Color Color;

		public Note(int x, int y, long ms)
		{
			X = x;
			Y = y;

			Ms = ms;
		}

		public Note Clone()
		{
			return new Note(X, Y, Ms) { Color = Color };
		}
	}
}