using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LAB5.ViewModels;
using Avalonia.Interactivity;
using System;
using System.IO;

namespace LAB5.Views
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
            InitializeComponent();

			this.FindControl<Button>("OpenFileDialog").Click += async delegate
			{
				var taskPath = new OpenFileDialog()
				{ 
					Title="Open File", 
					Filters = null 
				}.ShowAsync((Window)this.VisualRoot);

				string[]? path = await taskPath;

				if (path != null)
				{
					var context = this.DataContext as MainWindowViewModel;
					var pathStr = string.Join(@"\", path);
					context.Input = "";
					StreamReader reader = File.OpenText(pathStr);
					string str;
					while ((str = reader.ReadLine()) != null)
						context.Input += str + "\n";

					reader.Close();
				}
			};

			this.FindControl<Button>("SetRegexDialog").Click += async delegate
			{
				var context = this.DataContext as MainWindowViewModel;
				string? regex = await new NoMainWindow(context.MyRegex).ShowDialog<string>((Window)this.VisualRoot);
				context.MyRegex = regex;

				context.Input = context.Input;
			};

			this.FindControl<Button>("SaveFileDialog").Click += async delegate
			{
				var taskPath = new SaveFileDialog()
				{
					Title = "Save File",
					Filters = null
				}.ShowAsync((Window)this.VisualRoot);
				
				string? path = await taskPath;

				if (path != null)
				{
					var context = this.DataContext as MainWindowViewModel;
					var pathStr = string.Join(@"\", path);
					StreamWriter writer = new StreamWriter(pathStr);
					writer.WriteLine(context.Output);
					writer.Close();
				}
			};
		}
	}
}


			