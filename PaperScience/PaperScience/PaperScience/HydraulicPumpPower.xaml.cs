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
    public partial class HydraulicPumpPower : ContentPage
    {
        public HydraulicPumpPower()
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
                s.Text = "SI";
                label1.Text = "Differential pressure (kPa)";
                label2.Text = "Flow (l/min)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Differential pressure (psig)";
                label2.Text = "Flow (gpm)";
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
            if (string.IsNullOrWhiteSpace(H.Text) || string.IsNullOrWhiteSpace(Q.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(H.Text, out num) && Double.TryParse(Q.Text, out num))
            {
                var pressure = double.Parse(H.Text, CultureInfo.InvariantCulture);
                var flow = double.Parse(Q.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var power = pressure * flow / 1714.0;
                    label3.Text = "Hydraulic pump power: " + String.Format("{0:n4}", power) + " HP";
                }
                else
                {
                    var power = pressure * flow / 60000.0;
                    label3.Text = "Hydraulic pump power: " + String.Format("{0:n4}", power) + " kW";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}