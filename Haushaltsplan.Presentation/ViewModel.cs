namespace Haushaltsplan.Presentation
{
    using Haushaltsplan.Business.Enums;
    using Haushaltsplan.Business.Manager;
    using Haushaltsplan.Business.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;

    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel(HaushaltsplanManager manager)
        {
            Manager = manager;

            AlleJahre = manager.GetAlleJahre();

            ResetDateCommand = new RelayCommand(ResetDate);
            NextJahrCommand = new RelayCommand(NextJahr, () => AlleJahre.Contains(SelectedJahr + 1));
            PrevJahrCommand = new RelayCommand(PrevJahr, () => AlleJahre.Contains(SelectedJahr - 1));
            NextMonatCommand = new RelayCommand(NextMonat, () => SelectedMonat != Monate.Dezember || AlleJahre.Contains(SelectedJahr + 1));
            PrevMonatCommand = new RelayCommand(PrevMonat, () => SelectedMonat != Monate.Januar || AlleJahre.Contains(SelectedJahr - 1));
        }

        private HaushaltsplanManager Manager { get; }

        private Plan Plan { get; set; }

        private IEnumerable<Int32> AlleJahre { get; }

        public Int32 SelectedJahr { get; set; } = DateTime.Now.Year;
        public Monate SelectedMonat { get; set; } = DateTime.Now.GetMonat();

        public IEnumerable<Transaktion> Einnahmen { get; private set; }
        public IEnumerable<Transaktion> Ausgaben { get; private set; }

        private Boolean _einnahmenFixChecked = true;
        public Boolean EinnahmenFixChecked 
        {
            get => _einnahmenFixChecked; 
            set
            {
                _einnahmenFixChecked = value;
                ApplyEinnahmenFilter();
            }
        }

        private Boolean _einnahmenVariabelChecked = true;
        public Boolean EinnahmenVariabelChecked
        {
            get => _einnahmenVariabelChecked;
            set
            {
                _einnahmenVariabelChecked = value;
                ApplyEinnahmenFilter();
            }
        }

        private Boolean _ausgabenFixChecked = true;
        public Boolean AusgabenFixChecked
        {
            get => _ausgabenFixChecked;
            set
            {
                _ausgabenFixChecked = value;
                ApplyAusgabenFilter();
            }
        }

        private Boolean _ausgabenVariabelChecked = true;
        public Boolean AusgabenVariabelChecked
        {
            get => _ausgabenVariabelChecked;
            set
            {
                _ausgabenVariabelChecked = value;
                ApplyAusgabenFilter();
            }
        }

        private Boolean _fixTransaktionChecked;
        public Boolean FixTransaktionChecked
        {
            get => _fixTransaktionChecked;
            set
            {
                if (!_fixTransaktionChecked && value)
                {
                    _fixTransaktionChecked = true;
                    _neueTransaktionChecked = false;
                }

                OnPropertyChanged(nameof(NeueTransaktionChecked));
                OnPropertyChanged(nameof(FixTransaktionChecked));
            }
        }

        private Boolean _neueTransaktionChecked = true;
        public Boolean NeueTransaktionChecked
        {
            get => _neueTransaktionChecked;
            set
            {
                if (!_neueTransaktionChecked && value)
                {
                    _neueTransaktionChecked = true;
                    _fixTransaktionChecked = false;
                }

                OnPropertyChanged(nameof(NeueTransaktionChecked));
                OnPropertyChanged(nameof(FixTransaktionChecked));
            }
        }

        
        public ICommand ResetDateCommand { get; }
        public ICommand NextJahrCommand { get; }
        public ICommand PrevJahrCommand { get; }
        public ICommand NextMonatCommand { get; }
        public ICommand PrevMonatCommand { get; }

        private void ResetDate()
        {
            SelectedJahr = DateTime.Now.Year;
            SelectedMonat = DateTime.Now.GetMonat();
            UpdateView();
        }

        private void NextJahr()
        {
            SelectedJahr++;
            UpdateView();
        }

        private void PrevJahr()
        {
            SelectedJahr--;
            UpdateView();
        }

        private void NextMonat()
        {
            var prev = (Int32)SelectedMonat + 1;

            if (prev > 12)
            {
                prev = 1;
                SelectedJahr++;
            }

            SelectedMonat = (Monate)prev;

            UpdateView();
        }

        private void PrevMonat()
        {
            var prev = (Int32)SelectedMonat - 1;

            if (prev < 1)
            {
                prev = 12;
                SelectedJahr--;
            }

            SelectedMonat = (Monate)prev;

            UpdateView();
        }

        private void UpdateView()
        {
            Einnahmen = Plan.Jahre[SelectedJahr].Monate[SelectedMonat].Einnahmen;
            Ausgaben = Plan.Jahre[SelectedJahr].Monate[SelectedMonat].Ausgaben;

            OnPropertyChanged(nameof(SelectedJahr));
            OnPropertyChanged(nameof(SelectedMonat));
            OnPropertyChanged(nameof(Einnahmen));
            OnPropertyChanged(nameof(Ausgaben));
        }

        private void ApplyEinnahmenFilter()
        {
        }

        private void ApplyAusgabenFilter()
        {
        }

        public void LoadData()
        {
            Plan = Manager.LadePlan();
            ResetDate();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
