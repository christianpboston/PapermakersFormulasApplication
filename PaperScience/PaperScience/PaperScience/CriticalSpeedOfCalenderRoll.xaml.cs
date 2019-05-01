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
    public partial class CriticalSpeedOfCalenderRoll : ContentPage
    {
        public CriticalSpeedOfCalenderRoll()
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
                label1.Text = "Outside radius (m)";
                label2.Text = "Inside radius (m)";
                label3.Text = "Center to center bearing (m)";
                label4.Text = "Critical speed (m/min)";
                label5.Text = "Note: Assumes L = face + 1m.";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "Outside radius (in.)";
                label2.Text = "Inside radius (in.)";
                label3.Text = "Center to center bearing (in.)";
                label4.Text = "Critical speed (ft/min)";
                label5.Text = "Note: Assumes L = face + 40 in.";
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
            if (string.IsNullOrWhiteSpace(Ro.Text) || string.IsNullOrWhiteSpace(Ri.Text) || string.IsNullOrWhiteSpace(L.Text))
            {
                label4.Text = "Please enter all values.";
            }
            else if (Double.TryParse(Ro.Text, out num) && Double.TryParse(Ri.Text, out num) && Double.TryParse(L.Text, out num))
            {
                var outsideRadius = double.Parse(Ro.Text, CultureInfo.InvariantCulture);
                var insideRadius = double.Parse(Ri.Text, CultureInfo.InvariantCulture);
                var centerLine = double.Parse(L.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var criticalSpeed = 4.12 * Math.Pow(10, 6) * (outsideRadius / Math.Pow(centerLine, 2)) * Math.Sqrt(Math.Pow(outsideRadius, 2) + Math.Pow(insideRadius, 2));
                    label4.Text = "Critical speed: " + String.Format("{0:n4}", criticalSpeed) + " ft/min";
                }
                else
                {
                    var criticalSpeed = 1.26 * Math.Pow(10, 6) * (outsideRadius / Math.Pow(centerLine, 2)) * Math.Sqrt(Math.Pow(outsideRadius, 2) + Math.Pow(insideRadius, 2));
                    label4.Text = "Critical speed: " + String.Format("{0:n4}", criticalSpeed) + " m/min";
                }
            }
            else
            {
                label4.Text = "Please enter a valid number.";
            }
        }
    }
}