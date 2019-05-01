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
    public partial class Torque : ContentPage
    {
        public Torque()
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
                label1.Text = "Force (N)";
                label2.Text = "Radius (m)";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Force (lbf)";
                label2.Text = "Radius (in.)";
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
            if (string.IsNullOrWhiteSpace(f.Text) || string.IsNullOrWhiteSpace(r.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(f.Text, out num) && Double.TryParse(r.Text, out num))
            {
                var force = double.Parse(f.Text, CultureInfo.InvariantCulture);
                var radius = double.Parse(r.Text, CultureInfo.InvariantCulture);

                var torque = force * radius;
                if (!App.switched)
                {
                    label3.Text = "Torque: " + String.Format("{0:n4}", torque) + " lbf*in.";
                }
                else
                {
                    label3.Text = "Torque: " + String.Format("{0:n4}", torque) + " N*m";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}