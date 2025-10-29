using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using Microsoft.Win32;



namespace TextEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string? Filepath { get; set; }
    public bool IsModified { get; private set; } = false;
    
    public MainWindow()
    {
        InitializeComponent();
        FontSizeBox.Text = Editor.FontSize.ToString(CultureInfo.InvariantCulture);
        var panel = new StackPanel();
        panel.Children.Add(new Button { Content = "OK" });
        panel.Children.Add(new Button { Content = "Cancel" });
    
        StartTimer(DateBlock);
    }

    private static void StartTimer(TextBlock t)
    {
        var timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        timer.Tick += (s, e) => { t.Text = DateTime.Now.ToString("t"); };
        timer.Start();
    }


    private void OpenFile_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog()
        {
            Title = "Open a file",
            Filter = "Rich Text Format (*.rtf)|*.rtf|XAML Document (*.xaml)|*.xaml|Plain Text (*.txt)|*.txt"
        };

        if (dialog.ShowDialog() != true) return;
        OpenFileFromComputer(dialog.FileName);
    }

    private void OpenFileFromComputer(string? path)
    {
        if (string.IsNullOrEmpty(path)) { return;}
        Filepath = path;
        this.Title = $"Text Editor: {System.IO.Path.GetFileName(Filepath)}";
        using var file = new StreamReader(Filepath);
        TextRange range = new(Editor.Document.ContentStart, Editor.Document.ContentEnd)
        {
            Text = file.ReadToEnd()
        };
    }

    private void SaveFile_OnClick(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(Filepath)) { return;}

        if (!System.IO.Path.Exists(Filepath))
        {
            using var fileNew = File.Create(Filepath);
        }
        TextRange range = new(Editor.Document.ContentStart, Editor.Document.ContentEnd);
        using var file = new FileStream(Filepath, FileMode.Open, FileAccess.ReadWrite);
        var extension = Path.GetExtension(Filepath);
        switch (extension)
        {
            case ".rtf":
                range.Save(file, DataFormats.Rtf);
                break;
            case ".xaml": range.Save(file, DataFormats.Xaml);
                break;
            default: range.Save(file, DataFormats.Text);
                break;
        }
        MessageBox.Show($"Successfully saved {Path.GetFileName(Filepath)}");
        IsModified = false;
    }

    private void NewFile_OnClick(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(Filepath))
        {
            using var file = new StreamReader(Filepath);
            if (IsModified)
                MessageBox.Show("Save your file first");
            else
            {
                OpenFileFromComputer(CreateNewFile());
            }
        }
        else
        {
                OpenFileFromComputer(CreateNewFile());
        }
    }

    private string? CreateNewFile()
    {
        var dialog = new SaveFileDialog()
        {
            Title = "Save your file",
            Filter = "Rich Text Format (*.rtf)|*.rtf|XAML Document (*.xaml)|*.xaml|Plain Text (*.txt)|*.txt",
            FileName = "untitled.txt"
            
        };
        if (dialog.ShowDialog() != true) return null;
        using var file = File.Create(dialog.FileName);
        return dialog.FileName;
    }

    private void FontSizeBox_OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        double.TryParse(FontSizeBox.Text, out double fontsize);
        Editor.FontSize = fontsize != 0 ? fontsize : 1;
    }

    private void Editor_OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        IsModified = true;
    }
}