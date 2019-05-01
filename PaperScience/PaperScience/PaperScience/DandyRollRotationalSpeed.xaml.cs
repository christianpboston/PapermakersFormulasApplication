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
    public partial class DandyRollRotationalSpeed : ContentPage
    {
        public DandyRollRotationalSpeed()
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
                label1.Text = "Wire speed (m/min)";
                label2.Text = "Diameter (m)";
                s.Text = "SI";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "Wire speed (ft/min)";
                label2.Text = "Diameter (ft)";
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
            if (string.IsNullOrWhiteSpace(ws.Text) || string.IsNullOrWhiteSpace(d.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(ws.Text, out num) && Double.TryParse(d.Text, out num))
            {
                var speed = double.Parse(ws.Text, CultureInfo.InvariantCulture);
                var diameter = double.Parse(d.Text, CultureInfo.InvariantCulture);
                var rotationalSpeed = speed / (3.142 * diameter);
                label3.Text = "Rotational speed: " + String.Format("{0:n4}", rotationalSpeed) + " rev/min";
            }   
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}