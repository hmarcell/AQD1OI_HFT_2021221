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
        public ICommand CreateBrandCommand { get; set; }
        public ICommand DeleteBrandCommand { get; set; }
        public ICommand UpdateBrandCommand { get; set; }
        public ICommand CreateRentalCommand { get; set; }
        public ICommand DeleteRentalCommand { get; set; }
        public ICommand UpdateRentalCommand { get; set; }
        public RestCollection<Bike> Bikes { get; set; }
        public RestCollection<Brand> Brands { get; set; }
        public RestCollection<Rental> Rentals { get; set; }
        private Bike selectedBike;
        private Brand selectedBrand;
        private Rental selectedRental;

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
                        BrandID = value.BrandID,
                        Price = value.Price
                    };
                    OnPropertyChanged();
                    (DeleteBikeCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public Brand SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                if (value != null)
                {
                    selectedBrand = new Brand()
                    {
                        ID = value.ID,
                        Name = value.Name
                    };
                    OnPropertyChanged();
                    (DeleteBrandCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public Rental SelectedRental
        {
            get { return selectedRental; }
            set
            {
                if (value != null)
                {
                    selectedRental = new Rental()
                    {
                        ID = value.ID,
                        Date = value.Date,
                        Renter = value.Renter,
                        BikeID = value.BikeID
                        
                    };
                    OnPropertyChanged();
                    (DeleteRentalCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public void SetupCollections()
        {
            Bikes = new RestCollection<Bike>("http://localhost:7293/", "bike", "hub");
            Brands = new RestCollection<Brand>("http://localhost:7293/", "brand", "hub");
            Rentals = new RestCollection<Rental>("http://localhost:7293/", "rental", "hub");
        }
        public void SetupCommands()
        {
            CreateBikeCommand = new RelayCommand(() =>
            {
                Bikes.Add(new Bike() { Model = SelectedBike.Model, BrandID = SelectedBike.BrandID, Price = SelectedBike.Price });
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

            CreateBrandCommand = new RelayCommand(() =>
            {
                Brands.Add(new Brand() { Name = SelectedBrand.Name });
            });
            UpdateBrandCommand = new RelayCommand(() =>
            {
                Brands.Update(SelectedBrand);
            });
            DeleteBrandCommand = new RelayCommand(() =>
            {
                Brands.Delete(SelectedBrand.ID);
            },
            () =>
            {
                return SelectedBrand != null;
            });

            CreateRentalCommand = new RelayCommand(() =>
            {
                Rentals.Add(new Rental() { Renter = SelectedRental.Renter, Date = SelectedRental.Date, BikeID = SelectedRental.BikeID });
            });
            UpdateRentalCommand = new RelayCommand(() =>
            {
                Rentals.Update(SelectedRental);
            });
            DeleteRentalCommand = new RelayCommand(() =>
            {
                Rentals.Delete(SelectedRental.ID);
            },
            () =>
            {
                return SelectedRental != null;
            });

        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                SetupCollections();
                SetupCommands();

                SelectedBrand = new Brand();
                SelectedBike = new Bike();
                SelectedRental = new Rental();
            }
        }
    }
}
