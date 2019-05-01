using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperScience
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FourdrinierFormingLengthGuidelines : ContentPage
    {
        public FourdrinierFormingLengthGuidelines()
        {
            InitializeComponent();
            if (App.switched)
            {
                units.IsToggled = true;
            }
            else
            {
                units.IsToggled = false;
            }
        }

        /*
        * Switch changed
        */
        private void Units_Toggled(object sender, ToggledEventArgs e)
        {
            bool isToggled = e.Value;

            if (isToggled)
            {
                label1.Text = "Forming length (m)";
                label2.Text = "Wire speed (m/min)";
                s.Text = "SI";
                var speedList = new List<string>();
                speedList.Add("< 366 m/min");
                speedList.Add("> 366 m/min");
                speedList.Add("205 g/m\u00B2");
                speedList.Add("Foodboard");
                speed.ItemsSource = speedList;
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "Forming length (ft)";
                label2.Text = "Wire speed (ft/min)";
                s.Text = "US";
                var speedList = new List<string>();
                speedList.Add("< 1200 ft/min");
                speedList.Add("> 1200 ft/min");
                speedList.Add("42 lb/1000ft\u00B2");
                speedList.Add("Foodboard");
                speed.ItemsSource = speedList;
                App.switched = false;
                answer.IsVisible = false;
            }
        }

         /*
         * Calculate button pressed
         */
        private void Button_Clicked(object sender, EventArgs e)
        {
            answer.IsVisible = true;
            double num;
            if (string.IsNullOrWhiteSpace(e1.Text) || speed.SelectedIndex == -1 || dwell.SelectedIndex == -1)
            {
                label4.Text = "Please enter all values.";
            }
            else if (Double.TryParse(e1.Text, out num))
            {
                var units = "";
                if (App.switched)
                {
                    units = " m/min";
                }
                else
                {
                    units = " ft/min";
                }

                var formingLength = double.Parse(e1.Text, CultureInfo.InvariantCulture);
                if (speed.SelectedIndex == 0)
                {
                    if (dwell.SelectedIndex == 0)
                    {
                        var machineSpeed = formingLength * 40.0;
                        label4.Text = "Supported machine speed: " + String.Format("{0:n4}", machineSpeed) + units;
                    }
                    else
                    {
                        var machineSpeed = formingLength * 30.0;
                        label4.Text = "Supported machine speed: " + String.Format("{0:n4}", machineSpeed) + units;
                    }
                }
                else if (speed.SelectedIndex == 1)
                {
                    var machineSpeed = formingLength * 60.0;
                    label4.Text = "Supported machine speed: " + String.Format("{0:n4}", machineSpeed) + units;
                }
                else if (speed.SelectedIndex == 2)
                {
                    var machineSpeed = formingLength * 48.0;
                    label4.Text = "Supported machine speed: " + String.Format("{0:n4}", machineSpeed) + units;
                }
                else if (speed.SelectedIndex == 3)
                {
                    var machineSpeed = formingLength * 30.0;
                    label4.Text = "Supported machine speed: " + String.Format("{0:n4}", machineSpeed) + units;
                }

            }
            else
            {
                label4.Text = "Please enter a valid number.";
            }
        }

         /**
         * Sets dwell time items based on which wire speed was chosen.
         */
        private void Speed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (speed.SelectedIndex == 0)
            {
                var list_items = new List<string>();
                list_items.Add("1.5");
                list_items.Add("2.0");
                dwell.ItemsSource = list_items;
            }
            else if (speed.SelectedIndex == 1)
            {
                var list_items = new List<string>();
                list_items.Add("1.0");
                dwell.ItemsSource = list_items;
            }
            else if (speed.SelectedIndex == 2)
            {
                var list_items = new List<string>();
                list_items.Add("1.25");
                dwell.ItemsSource = list_items;
            }
            else
            {
                var list_items = new List<string>();
                list_items.Add("2.0");
                dwell.ItemsSource = list_items;
            }
        }
    }
}