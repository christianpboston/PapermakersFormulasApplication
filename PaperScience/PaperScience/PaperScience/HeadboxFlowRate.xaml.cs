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
    public partial class HeadboxFlowRate : ContentPage
    {
        public HeadboxFlowRate()
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
                label1.Text = "Spouting velocity (m/min)";
                label2.Text = "Slice opening (mm)";
                label4.Text = "Headbox Flow Rate (LPM/m)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Spouting velocity (ft/min)";
                label2.Text = "Slice opening (in.)";
                label4.Text = "Headbox Flow Rate (GPM/in.)";
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
            if (string.IsNullOrWhiteSpace(V.Text) || string.IsNullOrWhiteSpace(SO.Text) || contraction.SelectedItem == null)
            {
                label4.Text = "Please enter all values.";
            }
            else if (Double.TryParse(V.Text, out num) && Double.TryParse(SO.Text, out num))
            {
                var velocity = double.Parse(V.Text, CultureInfo.InvariantCulture);
                var opening = double.Parse(SO.Text, CultureInfo.InvariantCulture);
                var coefficient = 0.0;

                if (contraction.SelectedIndex == 0)
                {
                    coefficient = 0.95;
                }
                else if (contraction.SelectedIndex == 1)
                {
                    coefficient = 0.75;
                }
                else if (contraction.SelectedIndex == 2)
                {
                    coefficient = 0.70;
                }
                else if (contraction.SelectedIndex == 3)
                {
                    coefficient = 0.60;
                }

                if (!App.switched)
                {
                    var rate = opening * velocity * 0.052 * coefficient;
                    label4.Text = "Headbox Flow Rate: " + String.Format("{0:n4}", rate) + " GPM/in.";
                }
                else
                {
                    var rate = opening * velocity * coefficient;
                    label4.Text = "Headbox Flow Rate: " + String.Format("{0:n4}", rate) + " LPM/m";
                }
            }
            else
            {
                label4.Text = "Please enter a valid number.";
            }
        }
    }
}