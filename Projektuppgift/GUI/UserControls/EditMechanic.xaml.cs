﻿using System.Windows.Controls;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using static Logic.Services.AddMechanicService;
using Logic.Entities;
using System.Collections.Generic;
using static GUI.UserControls.MechanicHome;
using System;
using System.Windows;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for EditMechanic.xaml
    /// </summary>
    public partial class EditMechanic : UserControl
    {
        private const string EditMessage = "You have edited a mechanic!";
        public EditMechanic()
        {
            InitializeComponent();
            //Läser från JSON.
            string jsonFromFile;
            using (var reader = new StreamReader(mechpath))
            {
                jsonFromFile = reader.ReadToEnd();
            }
            var readFromJson = JsonConvert.DeserializeObject<List<Mechanic>>(jsonFromFile);
            //// Lägger till i listan.
            //mechanics.AddRange(readFromJson);



        }

        private void EditMechanicButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Mechanic selectedMechanic = _selectedMechanic;

            Guid CurrentMechID = selectedMechanic.MechID;

            mechanics.Remove(selectedMechanic);

            string firstName = this.tbFirstName2.Text;
            string surName = this.tbSurName2.Text;
            string dateOfBirth = this.tbDateOfBirth2.Text;
            string dateOfEmployment = this.tbDateOfEmployment2.Text;
            string employmentEnds = this.tbEmploymentEnds2.Text;
            string MotorIsChecked = ((bool)cbMotorYes2.IsChecked) ? "Yes" : "No";
            string TiresAreChecked = ((bool)cbTiresYes2.IsChecked) ? "Yes" : "No";
            string BrakesAreChecked = ((bool)cbBrakesYes2.IsChecked) ? "Yes" : "No";
            string WindshieldsAreChecked = ((bool)cbWindshieldsYes2.IsChecked) ? "Yes" : "No";
            string VehicleBodyIsChecked = ((bool)cbVehicleBodyYes2.IsChecked) ? "Yes" : "No";



            Mechanic mechanic = new Mechanic
            {
                FirstName = firstName,
                SurName = surName,
                DateOfBirth = dateOfBirth,
                DateOfEmployment = dateOfEmployment,
                EndDate = employmentEnds,
                MechID = CurrentMechID,
                CanFixMotor = MotorIsChecked,
                CanFixTires = TiresAreChecked,
                CanFixBrakes = BrakesAreChecked,
                CanFixWindshield = WindshieldsAreChecked,
                CanFixVehicleBody = VehicleBodyIsChecked

            };

            //READ
            if (mechanics.Count >= 1)
            {
                string jsonFromFile;
                using (var reader = new StreamReader(mechpath))
                {
                    jsonFromFile = reader.ReadToEnd();
                }
                var readFromJson = JsonConvert.DeserializeObject<List<Mechanic>>(jsonFromFile);
                mechanics.Add(mechanic);
                var jsonToWrite = JsonConvert.SerializeObject(mechanics, Formatting.Indented);
                using (var writer = new StreamWriter(mechpath))
                {
                    writer.Write(jsonToWrite);

                }
            }
            //ADD
            else
            {
                mechanics.Add(mechanic);
                var jsonToWrite = JsonConvert.SerializeObject(mechanics, Formatting.Indented);
                var fs = File.OpenWrite(mechpath);
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(jsonToWrite);

                }

            }
            EditMechanicView.Children.Clear();
            EditMechanicView.Children.Add(new EditMechanic());

            MessageBox.Show(EditMessage);
        }

        private void EditGoBackButton_Click(object sender, RoutedEventArgs e)
        {
            EditMechanicView.Children.Clear();
            EditMechanicView.Children.Add(new MechanicHome());
        }
    }
}