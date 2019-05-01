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
    public partial class RollRotationalSpeed : ContentPage
    {
        public RollRotationalSpeed()
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
                label2.Text = "Roll outside diameter (m)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Speed (ft/min)";
                label2.Text = "Roll outside diameter (in.)";
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
            if (string.IsNullOrWhiteSpace(V.Text) || string.IsNullOrWhiteSpace(D.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(V.Text, out num) && Double.TryParse(D.Text, out num))
            {
                var speed = double.Parse(V.Text, CultureInfo.InvariantCulture);
                var diameter = double.Parse(D.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var revolutions = 3.82 * speed / diameter;
                    label3.Text = "Rotational speed: " + String.Format("{0:n4}", revolutions) + " RPM";
                }
                else
                {
                    var revolutions = 0.3183 * speed / diameter;
                    label3.Text = "Rotational speed: " + String.Format("{0:n4}", revolutions) + " RPM";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}