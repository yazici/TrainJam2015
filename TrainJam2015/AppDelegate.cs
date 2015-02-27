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

namespace TrainJam2015
{
	class AppDelegate : CCApplicationDelegate
	{
		public AppDelegate ()
		{
			#if DEBUG
			CCLog.CustomCCLog = new LogWrapper (Console.WriteLine);
			#endif
		}

		public override void ApplicationDidFinishLaunching (CCApplication application, CCWindow mainWindow)
		{
			// Mac apps don't place items into subdirectory when they come from a shproj
			#if !MAC
			application.ContentRootDirectory = "Content";
			#endif

			var resolution = new CCSize (
				application.MainWindow.WindowSizeInPixels.Width,
				application.MainWindow.WindowSizeInPixels.Height
			);

			var scene = new CCScene (mainWindow);
			scene.AddChild (new CloudChamber (resolution));

			mainWindow.RunWithScene (scene);
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

