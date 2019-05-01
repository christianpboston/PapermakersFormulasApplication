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
    public partial class DeflectionOfRollOverFace : ContentPage
    {
        public DeflectionOfRollOverFace()
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
                label1.Text = "Resultant unit load (kN/m)";
                label2.Text = "Shell face (m)";
                label3.Text = "Center to center bearings (m)";
                label4.Text = "Modulus of elasticity (kN/m\u00B2)";
                label5.Text = "Outside diameter (m)";
                label6.Text = "Inside diameter (m)";
                s.Text = "SI";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "Resultant unit load (lbf/in.)";
                label2.Text = "Shell face (in.)";
                label3.Text = "Center to center bearings (in.)";
                label4.Text = "Modulus of elasticity (lbf/in\u00B2)";
                label5.Text = "Outside diameter (in.)";
                label6.Text = "Inside diameter (in.)";
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
            if (string.IsNullOrWhiteSpace(w.Text) || string.IsNullOrWhiteSpace(F.Text) || string.IsNullOrWhiteSpace(B.Text) || string.IsNullOrWhiteSpace(E.Text) || string.IsNullOrWhiteSpace(D0.Text) || string.IsNullOrWhiteSpace(D1.Text))
            {
                label7.Text = "Please enter all values.";
            }
            else if (Double.TryParse(w.Text, out num) && Double.TryParse(F.Text, out num) && Double.TryParse(B.Text, out num) && Double.TryParse(E.Text, out num) && Double.TryParse(D0.Text, out num) && Double.TryParse(D1.Text, out num))
            {
                var resultant = double.Parse(w.Text, CultureInfo.InvariantCulture);
                var shellFace = double.Parse(F.Text, CultureInfo.InvariantCulture);
                var centerLine = double.Parse(B.Text, CultureInfo.InvariantCulture);
                var modulus = double.Parse(E.Text, CultureInfo.InvariantCulture);
                var outside = double.Parse(D0.Text, CultureInfo.InvariantCulture);
                var inside = double.Parse(D1.Text, CultureInfo.InvariantCulture);
                var momentOfInertia = 0.0491 * (Math.Pow(outside, 4) - Math.Pow(inside, 4));

                var top = resultant * Math.Pow(shellFace, 3) * ((12.0 * centerLine) - (7.0 * shellFace));
                var bottom = 384.0 * modulus * momentOfInertia;

                var deflection = top / bottom;
                if (!App.switched)
                {
                    label7.Text = "Deflection over face: " + String.Format("{0:n4}", deflection) + " in.";
                }
                else
                {
                    label7.Text = "Deflection over face: " + String.Format("{0:n4}", deflection) + " m";
                }  
            }
            else
            {
                label7.Text = "Please enter a valid number.";
            }
        }
    }
}