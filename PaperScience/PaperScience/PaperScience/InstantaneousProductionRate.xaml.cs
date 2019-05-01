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
    public partial class InstantaneousProductionRate : ContentPage
    {
        public InstantaneousProductionRate()
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
                label1.Text = "Speed (m/min)";
                label2.Text = "Basis weight (g/m\u00B2)";
                label3.Text = "Reel trim (m)";
                label6.Text = "Note: Multiply the result by 0.024 to convert to t/d.";
                label4.IsVisible = false;
                R.IsVisible = false;
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Speed (ft/min)";
                label2.Text = "Basis weight (lb/ream)";
                label3.Text = "Reel trim (in.)";
                label4.Text = "Ream size (ft\u00B2)";
                label6.Text = "Note: Multiply the result by 0.012 to convert to ton/d.";
                label4.IsVisible = true;
                R.IsVisible = true;
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
            if (string.IsNullOrWhiteSpace(S.Text) || string.IsNullOrWhiteSpace(BW.Text) || string.IsNullOrWhiteSpace(T.Text) || string.IsNullOrWhiteSpace(R.Text))
            {
                label5.Text = "Please enter all values.";
            }
            else if (Double.TryParse(S.Text, out num) && Double.TryParse(BW.Text, out num) && Double.TryParse(T.Text, out num) && Double.TryParse(R.Text, out num))
            {
                var speed = double.Parse(S.Text, CultureInfo.InvariantCulture);
                var basisWeight = double.Parse(BW.Text, CultureInfo.InvariantCulture);
                var reelTrim = double.Parse(T.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var reamSize = double.Parse(R.Text, CultureInfo.InvariantCulture);
                    var production = speed * basisWeight * reelTrim * (5.0 / reamSize);
                    label5.Text = "Production: " + String.Format("{0:n4}", production) + " lb/h";
                }
                else
                {
                    var production = speed * basisWeight * reelTrim * 0.06;
                    label5.Text = "Production: " + String.Format("{0:n4}", production) + " kg/h";
                }
            }
            else
            {
                label5.Text = "Please enter a valid number.";
            }
        }
    }
}