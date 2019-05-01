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
    public partial class DryerFeltTension : ContentPage
    {
        public DryerFeltTension()
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
                label1.Text = "Mass (kg)";
                label2.Text = "Small chainwheel diam. (m)";
                label3.Text = "Large chainwheel diam. (m)";
                label4.Text = "Felt width (m)";
                label5.Text = "Felt tension (kN/m)";
                s.Text = "SI";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "Weight (lbf)";
                label2.Text = "Small chainwheel diam. (in.)";
                label3.Text = "Large chainwheel diam. (in.)";
                label4.Text = "Felt width (in.)";
                label5.Text = "Felt tension (lbf/in.)";
                s.Text = "US";
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
            if (string.IsNullOrWhiteSpace(W.Text) || string.IsNullOrWhiteSpace(D1.Text) || string.IsNullOrWhiteSpace(D2.Text) || string.IsNullOrWhiteSpace(FW.Text))
            {
                label5.Text = "Please enter all values.";
            }
            else if (Double.TryParse(W.Text, out num) && Double.TryParse(D1.Text, out num) && Double.TryParse(D2.Text, out num) && Double.TryParse(FW.Text, out num))
            {
                var weight = double.Parse(W.Text, CultureInfo.InvariantCulture);
                var diameterSmall = double.Parse(D1.Text, CultureInfo.InvariantCulture);
                var diameterLarge = double.Parse(D2.Text, CultureInfo.InvariantCulture);
                var feltWidth = double.Parse(FW.Text, CultureInfo.InvariantCulture);

                var top = weight * diameterLarge;
                var bottom = 2.0 * feltWidth * diameterSmall;

                if (!App.switched)
                {
                    var tension = top / bottom;
                    label5.Text = "Felt tension: " + String.Format("{0:n4}", tension) + " lbf/in.";
                }
                else
                {
                    var tension = (0.00981 * top) / bottom;
                    label5.Text = "Felt tension: " + String.Format("{0:n4}", tension) + " kN/m";
                }
            }
            else
            {
                label5.Text = "Please enter a valid number.";
            }
        }
    }
}