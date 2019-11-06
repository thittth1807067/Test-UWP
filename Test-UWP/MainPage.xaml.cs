using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Test_UWP.entity;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Test_UWP.Models;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Test_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Contact> listContact;
        private ContactModel _model;
        public MainPage()
        {
            this.InitializeComponent();
            this._model = new ContactModel();
        }

        private void AddContact_OnClick(object sender, RoutedEventArgs e)
        {

            _model.Insert(new Contact
            {
                Name = this.Name.Text,
                PhoneNumber = long.Parse(this.PhoneNumber.Text)
            });
            ViewListContact();
        }

        private void SearchContact_OnClick(object sender, RoutedEventArgs e)
        {
            var list = _model.GetContactByName(this.KeyWord.Text);
            listContact = new ObservableCollection<Contact>();
            if (list == null || list.Count == 0)
            {
                this.StatusSearch.Text = "No contact found";
                return;
            }

            listContact.Add(list[0]);
            ListViewContact.ItemsSource = listContact;
        }

        private void ViewListContact()
        {
            var list = _model.GetListContacts();
            listContact = new ObservableCollection<Contact>();
            if (list == null || list.Count == 0)
            {
                return;
            }

            listContact = list;
            ListViewContact.ItemsSource = listContact;
        }
    }
}
