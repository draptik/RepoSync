using System;
using Gtk;
using System.Collections.Generic;
using RepoSync.Service;

namespace RepoSync.GuiGtk
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class MenuBarWidget : Gtk.Bin
	{
		public MenuBarWidget ()
		{
			this.Build ();

			Init ();

			this.ShowAll ();
		}

		private void Init ()
		{
			Menu helpMenu = new Menu ();

			MenuItem aboutItem = new MenuItem ("About");
			aboutItem.Activated += HandleAboutActivated;

			helpMenu.Append (aboutItem);
			MenuItem helpItem = new MenuItem ("Help");

			helpItem.Submenu = helpMenu;

			MenuBar menuBar = new MenuBar ();
			menuBar.Append (helpItem);

			var vbox = new Gtk.VBox ();
			vbox.PackStart (menuBar, false, false, 0);
			this.Add (vbox);
		}

		private void HandleAboutActivated (object sender, EventArgs e)
		{
			AboutDialog about = new AboutDialog {
				ProgramName = AppInfos.AppName,
				Version = AppInfos.Version, 
				Website = AppInfos.WebSite, 
				Comments = AppInfos.Comments
			};

			about.Run ();
			about.Destroy ();
		}
	}
}

