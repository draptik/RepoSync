using System;
using Gtk;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SyncOutputWidget : Gtk.Bin
	{
		private Gtk.TreeView treeView;
		private Gtk.TreeStore model;

		public SyncOutputWidget ()
		{
			this.Build ();
			
			Init ();

			this.ShowAll ();
		}

		public void ClearContent ()
		{
			model.Clear ();
		}

		public void Content (bool success, string title, string content)
		{
			Gtk.TreeIter iter;

			// Controller:
			iter = model.AppendValues (GetSuccessIcon (success), title);
			model.AppendValues (iter, null, content);

			treeView.Model = model;
		}

		
		private void Init ()
		{
			// Model:
			model = new Gtk.TreeStore (typeof (Gdk.Pixbuf), typeof (string));

			// View:
			treeView = new Gtk.TreeView ();
			treeView.HeightRequest = 400;
			treeView.AppendColumn ("", new Gtk.CellRendererPixbuf (), "pixbuf", 0);  
			treeView.AppendColumn ("Result", new Gtk.CellRendererText (), "text", 1);

			treeView.Model = model; // empty model

			var vbox = new VBox ();
			vbox.PackStart(treeView, true, true, 0);
			this.Add (vbox);
		}

		private Gdk.Pixbuf GetSuccessIcon (bool success)
		{
			var iconSuccess = ConvertStockItemToPixBuf (Stock.Apply);
			var iconFailure = ConvertStockItemToPixBuf (Stock.No);

			return success ? iconSuccess : iconFailure;
		}

		private Gdk.Pixbuf ConvertStockItemToPixBuf (string stockItem, IconSize size = IconSize.Menu)
		{
			return this.RenderIcon (stockItem, size, null);
		}
	}
}

