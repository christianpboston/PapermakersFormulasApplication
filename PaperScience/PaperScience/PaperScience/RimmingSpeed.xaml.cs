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
    public partial class RimmingSpeed : ContentPage
    {
        public RimmingSpeed()
        {
            InitializeComponent();
            
        }

        /*
         * Calculate button pressed
         */
        private void Button_Clicked(object sender, EventArgs e)
        {
            answer.IsVisible = true;
            double num;
            if (string.IsNullOrWhiteSpace(D.Text) || string.IsNullOrWhiteSpace(L.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(D.Text, out num) && Double.TryParse(L.Text, out num))
            {
                var diameter = double.Parse(D.Text, CultureInfo.InvariantCulture);
                var condensate = double.Parse(L.Text, CultureInfo.InvariantCulture);

                var speed = (5720.0 - (2160.0 / diameter)) * Math.Pow(condensate, 1.0 / 3.0);
                label3.Text = "Rimming speed: " + String.Format("{0:n4}", speed) + " ft/min\r\nRimming speed: " + String.Format("{0:n4}", speed * 0.3048) + " m/min";
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}