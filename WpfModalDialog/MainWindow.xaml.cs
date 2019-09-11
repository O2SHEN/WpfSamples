using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Technewlogic.Samples.WpfModalDialog
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			ModalDialog.SetParent(ModalDialogParent);
		}

		private void ShowModalDialog_Click(object sender, RoutedEventArgs e)
		{
			var res = ModalDialog.ShowHandlerDialog(MessageTextBox.Text);
			var resultMessagePrefix = "Result: ";
			if (res)
				ResultText.Text = resultMessagePrefix + "Ok";
			else
				ResultText.Text = resultMessagePrefix + "Cancel";
		}
	}
}
