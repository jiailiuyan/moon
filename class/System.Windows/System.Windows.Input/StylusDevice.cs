//
// System.Windows.Input.StylusDevice.cs
//
// Contact:
//   Moonlight List (moonlight-list@lists.ximian.com)
//
// Copyright 2008-2009 Novell, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using Mono;

namespace System.Windows.Input
{
	public sealed partial class StylusDevice
	{
		private MouseEventArgs mouse_event_args;
		private StylusInfo stylus;
		
		internal StylusDevice (MouseEventArgs args)
		{
			mouse_event_args = args;
		}
		
		public TabletDeviceType DeviceType {
			get { return Stylus.DeviceType; }
		}
		
		public bool Inverted {
			get { return Stylus.IsInverted; }
		}

		private StylusInfo Stylus {
			get {
				if (stylus == null)
					stylus = StylusInfo.FromIntPtr (mouse_event_args.NativeHandle);
				return stylus;
			}
		}
		
		public StylusPointCollection GetStylusPoints (UIElement relativeTo)
		{
			IntPtr col = NativeMethods.mouse_event_args_get_stylus_points (mouse_event_args.NativeHandle, relativeTo == null ? IntPtr.Zero : relativeTo.native);
			var ret = (StylusPointCollection) NativeDependencyObjectHelper.Lookup (col);
			NativeMethods.event_object_unref (col);
			return ret;
		}
	}
}
