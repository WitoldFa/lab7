using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace lab7.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _inputSequence = "";
        private string _result = "";

        public string InputSequence
        {
            get => _inputSequence;
            set
            {
                _inputSequence = value;
                OnPropertyChanged();
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public ICommand CountCommand { get; }

        public MainViewModel()
        {
            CountCommand = new RelayCommand(CountSequences);
        }

        private void CountSequences()
        {
            string dna = InputSequence.ToUpper().Replace(" ", "").Replace("\n", "").Replace("\r", "");
            Dictionary<string, int> counts = new();

            for (int i = 0; i <= dna.Length - 4; i++)
            {
                string sub = dna.Substring(i, 4);
                if (!IsValidDna(sub)) continue;

                if (counts.ContainsKey(sub))
                    counts[sub]++;
                else
                    counts[sub] = 1;
            }

            StringBuilder sb = new();
            foreach (var pair in counts)
            {
                sb.AppendLine($"{pair.Key}: {pair.Value}");
            }

            Result = sb.ToString();
        }

        private bool IsValidDna(string seq)
        {
            foreach (char c in seq)
            {
                if (!"ACGT".Contains(c)) return false;
            }
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
