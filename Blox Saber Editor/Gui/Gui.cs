﻿using System.Drawing;

namespace Blox_Saber_Editor.Gui
{
	class Gui
	{
		public RectangleF ClientRectangle;

		protected Gui(float x, float y, float sx, float sy)
		{
			ClientRectangle = new RectangleF(x, y, sx, sy);
		}

		public virtual void Render(float delta, float mouseX, float mouseY)
		{

		}

		public virtual void OnResize(Size size)
		{

		}
	}
}