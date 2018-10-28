using Kelompok14.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kelompok14.View
{
    public class HalamanHapusData : ContentPage
    {
        private ListView _listView;
        private Button _simpan;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db4");

        DataMahasiswa datamahasiswa = new DataMahasiswa();

        public HalamanHapusData()
        {
            this.Title = "Hapus Data Mahasiswa";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<DataMahasiswa>().OrderBy(x => x.Nama).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _simpan = new Button();
            _simpan.Text = "Hapus Data";
            _simpan.Clicked += _hapus_Clicked;
            stackLayout.Children.Add(_simpan);

            Content = stackLayout;
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            datamahasiswa = (DataMahasiswa)e.SelectedItem;
        }

        private async void _hapus_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.Table<DataMahasiswa>().Delete(x => x.Id == datamahasiswa.Id);

            await DisplayAlert(null, "Data " + datamahasiswa.Nama + " Berhasil Dihapus", "Ok");
            await Navigation.PopAsync();
            db.Delete(datamahasiswa);
        }
    }
}