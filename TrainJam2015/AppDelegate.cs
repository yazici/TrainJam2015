//
// Program.cs
//
// Author:
//       Michael Hutchinson <m.j.hutchinson@gmail.com>
//
// Copyright (c) 2015 Michael Hutchinson
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using CocosSharp;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection;
using System.Linq;

namespace TrainJam2015
{
	class AppDelegate : CCApplicationDelegate
	{
		public static CCApplication Application { get; private set; }

		public AppDelegate ()
		{
			#if DEBUG
			CCLog.CustomCCLog = new LogWrapper (Console.WriteLine);
			#endif
		}

		public override void ApplicationDidFinishLaunching (CCApplication application, CCWindow mainWindow)
		{
			#if DEBUG
			mainWindow.DisplayStats = true;
			#endif

			Application = application;

			SoundPlayer.PreloadSounds ();

			// Currently build tasks don't place items into subdirectory when they come from a shproj
			#if !MAC
			application.ContentRootDirectory = "Content";
			#endif

			var scene = new CCScene (mainWindow);
			var layer = new SplashLayer (mainWindow.WindowSizeInPixels);
			scene.AddChild (layer);
			mainWindow.RunWithScene (scene);

			//HACK: partly work around MonoGame fullscreen issues. still comes up behind taskbar
			#if WINDOWS
			var screen = System.Windows.Forms.Screen.AllScreens.First(e => e.Primary);
			var window = (GameWindow) mainWindow.GetType ().GetProperty ("XnaWindow", BindingFlags.Instance | BindingFlags.NonPublic).GetValue (mainWindow);
			window.IsBorderless = true;
			window.Position = new Point(screen.Bounds.X, screen.Bounds.Y);
			var form = ((System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle (window.Handle));
			form.TopMost = true;
			form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			#endif
		}
	}

	class LogWrapper : ICCLog
	{
		readonly Action<string> write;

		public LogWrapper (Action<string> write)
		{
			this.write = write;
		}

		public void Log (string message)
		{
			write (message);
		}

		public void Log (string format, params object[] args)
		{
			write (string.Format (format, args));
		}
	}
}

