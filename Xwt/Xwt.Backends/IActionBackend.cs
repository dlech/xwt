using System;

namespace Xwt.Backends
{
	public interface IActionBackend
	{
		IActionBackend(string name, string label);

		string Name { get; }

		string Label { get; set; }

		Accelerator Accelerator { get; set; }

		string ToolTip { get; set; }

		string ToolbarLabel { get; set; }

		bool Visible { get; set; }

		bool Sensitive { get; set; }
	}

	public interface IActionEventSink
	{
		void OnActivated ();
	}
	
	public enum ActionEvent
	{
		Activated = 1
	}
}

