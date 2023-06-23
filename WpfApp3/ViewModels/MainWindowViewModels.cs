using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp3.Commands;
using WpfApp3.Models;
using WpfApp3.Repository;

namespace WpfApp3.ViewModels
{
    public class MainWindowViewModels : BaseViewModel
    {
        public DataSet AutorSet { get; set; } = new DataSet();

        public Repo AuthorsRepo { get; set; }
        public RelayCommand InsertCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand ShowAllCommand { get; set; }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string firstName;

        public string Firstname
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged(); }
        }

        private string lastName;

        public string Lastname
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged(); }
        }


        public MainWindowViewModels()
        {
            AuthorsRepo = new Repo();
            AutorSet = AuthorsRepo.GetAll();
            

            InsertCommand = new RelayCommand((obj) =>
            {
                AuthorsRepo.Insert(Id, Firstname, Lastname);
            });

        }
    }
}
