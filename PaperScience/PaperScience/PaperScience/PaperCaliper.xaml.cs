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
    public partial class PaperCaliper : ContentPage
    {
        public PaperCaliper()
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
                label1.Text = "Basis weight (g/m\u00B2)";
                label3.Text = "Density (kg/m\u00B3)";
                label2.IsVisible = false;
                R.IsVisible = false;
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Basis weight (lbs/ft\u00B2)";
                label2.Text = "Ream (ft\u00B2)\nShould equal area from above";
                label3.Text = "Density (lb/in\u00B3)";
                label2.IsVisible = true;
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
            if (!App.switched)
            {
                if (string.IsNullOrWhiteSpace(BW.Text) || string.IsNullOrWhiteSpace(R.Text) || string.IsNullOrWhiteSpace(D.Text))
                {
                    label4.Text = "Please enter all values.";
                }
                else if (Double.TryParse(BW.Text, out num) && Double.TryParse(R.Text, out num) && Double.TryParse(D.Text, out num))
                {
                    var basisWeight = double.Parse(BW.Text, CultureInfo.InvariantCulture);
                    var density = double.Parse(D.Text, CultureInfo.InvariantCulture);
                    var ream = double.Parse(R.Text, CultureInfo.InvariantCulture);
                    var caliper = basisWeight / (ream * 144.0 * density);
                    label4.Text = "Caliper: " + String.Format("{0:n4}", caliper) + " in.";
                }
                else
                {
                    label4.Text = "Please enter a valid number.";
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(BW.Text) || string.IsNullOrWhiteSpace(D.Text))
                {
                    label4.Text = "Please enter all values.";
                }
                else if (Double.TryParse(BW.Text, out num) && Double.TryParse(D.Text, out num))
                {
                    var basisWeight = double.Parse(BW.Text, CultureInfo.InvariantCulture);
                    var density = double.Parse(D.Text, CultureInfo.InvariantCulture);
                    var caliper = basisWeight / density;
                    label4.Text = "Caliper: " + String.Format("{0:n4}", caliper) + " mm"; 
                }
                else
                {
                    label4.Text = "Please enter a valid number.";
                }
            }
        }
    }
}