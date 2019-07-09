using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Demo
{
    public class Patient : Person, INotifyPropertyChanged
    {
        public Patient() {
            isNew = true;
            BloodSugar = 4.9f;
            History = new List<string>();
        }

        public string FirstName { get; set;}

        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public int HeartBeatRate { get; set; }

        public bool isNew { get; set; }

        public float BloodSugar { get; set; }

        public List<string> History { get; set; }

        //事件
        public event EventHandler<EventArgs> PatientSlept;
        

        public void OnPatientSleep() {
            PatientSlept?.Invoke(this, EventArgs.Empty);
        }
        public void Sleep() {
            OnPatientSleep();
        }


        public void IncreaseHeartBeatRate() {
            HeartBeatRate = CalculateHeartBeatRate() + 2;
            OnPropertyChanged(nameof(HeartBeatRate));
        }

        private int CalculateHeartBeatRate() {
            var rand = new Random();
            return rand.Next(1, 100);
        }

        public void NotAllowed() {
            throw new InvalidOperationException("Not able to create");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        }
            
         
    }
}
