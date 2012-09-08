using System;

namespace RepoSync.GuiGtk.Widgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ChooseConfigWidget : Gtk.Bin
	{
		public ChooseConfigWidget ()
		{
			this.Build ();

//			var dummyLabel = new Gtk.Label();
//			dummyLabel.Text = "HALLO";
//			this.Add(dummyLabel);

			Init ();

			this.ShowAll();
		}

		private void Init()
		{
			var btnFileChooser = new Gtk.FileChooserButton("Please choose a file!", Gtk.FileChooserAction.Open);
			this.Add (btnFileChooser);
		}
	}
}

