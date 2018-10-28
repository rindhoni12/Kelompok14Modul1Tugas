﻿using Kelompok14.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kelompok14.View
{
    public class HalamanEditData : ContentPage
    {
        private ListView _listView;
        private Entry _ID, _nama, _jurusan;
        private Button _simpan;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db4");

        DataMahasiswa datamahasiswa = new DataMahasiswa();

        public HalamanEditData()
        {
            this.Title = "Edit Data Mahasiswa";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<DataMahasiswa>().OrderBy(x => x.Nama).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _ID = new Entry();
            _ID.Placeholder = "ID";
            _ID.IsVisible = false;
            stackLayout.Children.Add(_ID);

            _nama = new Entry();
            _nama.Keyboard = Keyboard.Text;
            _nama.Placeholder = "Nama Mahasiswa";
            stackLayout.Children.Add(_nama);

            _jurusan = new Entry();
            _jurusan.Keyboard = Keyboard.Text;
            _jurusan.Placeholder = "Jurusan";
            stackLayout.Children.Add(_jurusan);

            _simpan = new Button();
            _simpan.Text = "Edit Data";
            _simpan.Clicked += _simpan_Clicked;
            stackLayout.Children.Add(_simpan);

            Content = stackLayout;
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            datamahasiswa = (DataMahasiswa)e.SelectedItem;
            _ID.Text = datamahasiswa.Id.ToString();
            _nama.Text = datamahasiswa.Nama.ToString();
            _jurusan.Text = datamahasiswa.Jurusan.ToString();
        }

        private async void _simpan_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            DataMahasiswa datamahasiswa = new DataMahasiswa()
            {
                Id = Convert.ToInt32(_ID.Text),
                Nama = _nama.Text,
                Jurusan = _jurusan.Text
            };
            db.Update(datamahasiswa);
            await DisplayAlert(null, "Data " + datamahasiswa.Nama + " Berhasil Diedit", "OK");
            await Navigation.PopAsync();
        }
    }
}