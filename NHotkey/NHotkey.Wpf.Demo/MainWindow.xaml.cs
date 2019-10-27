﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace NHotkey.Wpf.Demo
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : INotifyPropertyChanged
	{
		public MainWindow()
		{
			HotkeyManager.HotkeyAlreadyRegistered += HotkeyManager_HotkeyAlreadyRegistered;

			HotkeyManager.Current.AddOrReplace("Increment", Key.Add, ModifierKeys.Control | ModifierKeys.Alt, OnIncrement);
			HotkeyManager.Current.AddOrReplace("Decrement", Key.Subtract, ModifierKeys.Control | ModifierKeys.Alt, OnDecrement);

			InitializeComponent();
			DataContext = this;
		}

		private void HotkeyManager_HotkeyAlreadyRegistered(object sender, HotkeyAlreadyRegisteredEventArgs e)
		{
			MessageBox.Show(string.Format("The hotkey {0} is already registered by another application", e.Name));
		}

		private void OnIncrement(object sender, HotkeyEventArgs e)
		{
			Value++;
			e.Handled = true;
		}

		private void OnDecrement(object sender, HotkeyEventArgs e)
		{
			Value--;
			e.Handled = true;
		}

		private int _value;
		public int Value
		{
			get { return _value; }
			set
			{
				_value = value;
				OnPropertyChanged();
			}
		}

		private DelegateCommand _negateCommand;
		public ICommand NegateCommand
		{
			get { return _negateCommand ?? (_negateCommand = new DelegateCommand(Negate)); }
		}

		private DelegateCommand _testCommand;
		public ICommand TestCommand
		{
			get { return _testCommand ?? (_testCommand = new DelegateCommand(Test)); }
		}

		private void Test()
		{
			MessageBox.Show("Test");
		}


		private void Negate()
		{
			Value = -Value;
		}


		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	class DelegateCommand : ICommand
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public DelegateCommand(Action execute, Func<bool> canExecute = null)
		{
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute();
		}

		public void Execute(object parameter)
		{
			_execute();
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
	}
}
