﻿using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Blox_Saber_Editor.Gui
{
	class GuiLabel : Gui
	{
		public string Text;
		public int FontSize = 24;
		public bool Centered = false;

		public Color Color = Color.White;

		public GuiLabel(float x, float y, string text) : base(x, y, 0, 0)
		{
			Text = text;
		}

		public override void Render(float delta, float mouseX, float mouseY)
		{
			GL.Color4(Color);

			var fr = EditorWindow.Instance.FontRenderer;

			if (Centered)
			{
				var w = fr.GetWidth(Text, FontSize);
				var h = fr.GetHeight(FontSize);

				fr.Render(Text, (int)(ClientRectangle.X - w / 2f), (int)(ClientRectangle.Y - h / 2f), FontSize);
			}
			else
				fr.Render(Text, (int)ClientRectangle.X, (int)ClientRectangle.Y, FontSize);
		}
	}
}
