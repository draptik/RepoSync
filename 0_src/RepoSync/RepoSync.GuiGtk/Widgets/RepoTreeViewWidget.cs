using System;

namespace RepoSync.GuiGtk.Widgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class RepoTreeViewWidget : Gtk.Bin
	{
		public RepoTreeViewWidget ()
		{
			this.Build ();

			Init ();

			this.ShowAll ();
		}

		private void Init ()
		{
			var tv = new Gtk.TreeView();
			this.Add (tv);
		}

	}
}

