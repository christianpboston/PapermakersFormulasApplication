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
    public partial class Power : ContentPage
    {
        public Power()
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
                label1.Text = "Torque (N * m)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Torque (lbf * in.)";
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
            if (string.IsNullOrWhiteSpace(T.Text) || string.IsNullOrWhiteSpace(N.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(T.Text, out num) && Double.TryParse(N.Text, out num))
            {
                var torque = double.Parse(T.Text, CultureInfo.InvariantCulture);
                var speed = double.Parse(N.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var horsepower = (torque * speed) / 63025.0;
                    label3.Text = "Horsepower: " + String.Format("{0:n4}", horsepower) + " HP";
                }
                else
                {
                    var power = 0.1047 * torque * speed;
                    label3.Text = "Power: " + String.Format("{0:n4}", power) + " W";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}