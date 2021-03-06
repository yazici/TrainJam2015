﻿//
// Splash.cs
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
	public class SplashLayer : CCLayer
	{
		public SplashLayer (CCSize size) : base (size)
		{
		}

		protected override void AddedToScene ()
		{
			base.AddedToScene ();

			var screenSize = Window.WindowSizeInPixels;
			var background = new CCSprite("splash");

			background.Scale = 0.8f * screenSize.Height / background.ContentSize.Height;
			background.Position = new CCPoint(screenSize.Width / 2, screenSize.Height / 2);
			AddChild(background);

			AddEventListener (new CCEventListenerKeyboard {
				OnKeyPressed = OnKeyEvent,
			});
		}

		void OnKeyEvent (CCEventKeyboard obj)
		{
			switch (obj.Keys) {
			case CCKeys.Space:
				Director.ReplaceScene (new MainScene (Window));
				break;
			case CCKeys.Escape:
				AppDelegate.Application.ExitGame ();
				break;
			case CCKeys.C:
				Director.ReplaceScene (new CreditsScene (Window));
				break;
			}
		}
	}
}

