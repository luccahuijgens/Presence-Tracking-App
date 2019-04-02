using System;
using System.IO;
using Xamarin.Forms;


namespace Notes
{
    public partial class MainPage : ContentPage
    {
        string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notes.txt");

        public MainPage()
        {
            InitializeComponent();


        }
        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            result.Text = "" + (Convert.ToInt64(editor.Text) * 2);
            editor.Text = result.Text;

        }

    }
}