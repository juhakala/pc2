using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using projectWpf.Sources.pages.menu;
using projectWpf.Sources.pages.streamMem;

namespace projectWpf
{
	class MainWindowViewModel
	{
		private MenuViewModel _menuViewModel;
		public MenuViewModel MenuViewModel { get { return _menuViewModel; } set { _menuViewModel = value; } }
		private StreamMemViewModel _streamMemViewModel;
		public StreamMemViewModel StreamMemViewModel { get { return _streamMemViewModel; } set { _streamMemViewModel = value; } }

		public MainWindowViewModel()
		{
			MenuViewModel = new MenuViewModel();
			StreamMemViewModel = new StreamMemViewModel();
		}
		public void setMenuPage(MainWindow MainW)
		{
			Page page = new MenuView();
			page.DataContext = MenuViewModel;
			MainW.menu_frame.Navigate(page);
		}
		public void setStreamMemPage(MainWindow MainW)
		{
			Page page = new StreamMemView();
			page.DataContext = StreamMemViewModel;
			MainW.content_frame.Navigate(page);
		}
	}
}
