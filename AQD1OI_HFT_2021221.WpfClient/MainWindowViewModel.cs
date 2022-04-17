using AQD1OI_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AQD1OI_HFT_2021221.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public ICommand CreateBikeCommand { get; set; }
        public ICommand DeleteBikeCommand { get; set; }
        public ICommand UpdateBikeCommand { get; set; }
        public RestCollection<Bike> Bikes { get; set; }
        private Bike selectedBike;

        public static bool IsInDesignMode
        { 
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public Bike SelectedBike
        {
            get { return selectedBike; }
            set
            {
                if (value != null)
                {
                    selectedBike = new Bike()
                    {
                        Model = value.Model,
                        ID = value.ID,
                        BrandID = value.BrandID
                    };
                    OnPropertyChanged();
                    (DeleteBikeCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Bikes = new RestCollection<Bike>("http://localhost:7293/", "bike", "hub");

                CreateBikeCommand = new RelayCommand(() =>
                {
                    //Bikes.Add(new Bike() { BrandID = 4, Price = 25000, Model = "Cross Avalon" });
                    Bikes.Add(new Bike() { Model = SelectedBike.Model, BrandID = 1 });
                });


                UpdateBikeCommand = new RelayCommand(() =>
                {
                    Bikes.Update(SelectedBike);
                });

                DeleteBikeCommand = new RelayCommand(() =>
                {
                    Bikes.Delete(SelectedBike.ID);
                },
                () =>
                {
                    return SelectedBike != null;
                });
            SelectedBike = new Bike();
            }
        }
    }
}
