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
    public partial class ApproximateCriticalSpeedOfRoll : ContentPage
    {
        public ApproximateCriticalSpeedOfRoll()
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
                label1.Text = "Outside diameter of roll (m)";
                label2.Text = "Roll deflection over face (m)";
                label3.Text = "Critical speed (m/min)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Outside diameter of roll (in.)";
                label2.Text = "Roll deflection over face (in.)";
                label3.Text = "Critical speed (ft/min)";
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
            if (string.IsNullOrWhiteSpace(Do.Text) || string.IsNullOrWhiteSpace(d9.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(Do.Text, out num) && Double.TryParse(d9.Text, out num))
            {
                var outsideDiameter = double.Parse(Do.Text, CultureInfo.InvariantCulture);
                var rollDeflection = double.Parse(d9.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var criticalSpeed = (49.12 * outsideDiameter) / Math.Sqrt(rollDeflection);
                    label3.Text = "Critical speed: " + String.Format("{0:n4}", criticalSpeed) + " ft/min";
                }
                else
                {
                    var criticalSpeed = (93.96 * outsideDiameter) / Math.Sqrt(rollDeflection);
                    label3.Text = "Critical speed: " + String.Format("{0:n4}", criticalSpeed) + " m/min";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}