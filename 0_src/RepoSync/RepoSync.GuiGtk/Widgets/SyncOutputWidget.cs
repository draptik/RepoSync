using System;
using Gtk;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class SyncOutputWidget : Gtk.Bin
	{
		private Gtk.TextView textView;

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
//			textView.Buffer.Text = string.Empty;
			model.Clear ();
		}

//		public void Content (string content)
//		{
//			textView.Buffer.Text += content;
//		}

		public void Content (bool success, string title, string content)
		{
			Gtk.TreeIter iter;

			iter = model.AppendValues (GetSuccessIcon (success), title);
			model.AppendValues (iter, content);

//			treeView.Model += model;
			treeView.Model = model;
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

		private void Init ()
		{
//			textView = new Gtk.TextView();
//
//			// we need some more height...
//			textView.HeightRequest = 400;
//
//			var scrolledWindow = new Gtk.ScrolledWindow(null, null);
//			scrolledWindow.Add (textView);
//
//			var vbox = new VBox();
//			vbox.PackStart(scrolledWindow, true, true, 0);
//			this.Add (vbox);

			// Model:
			model = new Gtk.TreeStore (typeof (Gdk.Pixbuf), typeof (string));

			// View:
			treeView = new Gtk.TreeView ();
			treeView.HeightRequest = 400;
			treeView.AppendColumn ("", new Gtk.CellRendererPixbuf (), "pixbuf", 0);  
			treeView.AppendColumn ("Result", new Gtk.CellRendererText (), "text", 1);

			treeView.Model = model;
			var vbox = new VBox ();
			vbox.PackStart(treeView, true, true, 0);
			this.Add (vbox);
		}

	}
}

