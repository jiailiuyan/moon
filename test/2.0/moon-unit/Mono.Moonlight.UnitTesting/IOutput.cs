﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mono.Moonlight.UnitTesting
{
	public interface IOutput
	{
		void StartReport ();
		void Report (TestInfo result);
		void EndReport ();
	}
}
