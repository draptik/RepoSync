using System;
using Gtk;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SyncOutputWidget : Gtk.Bin
	{
		private Gtk.TextView textView;

		public SyncOutputWidget ()
		{
			this.Build ();
			
			Init ();

			this.ShowAll ();
		}

		public void Content (string content)
		{
			textView.Buffer.Text = content;
		}

		private void Init ()
		{
		
			textView = new Gtk.TextView();

			var vbox = new VBox();
			vbox.PackStart(textView, true, true, 0);
			this.Add (vbox);
		}

	}
}

