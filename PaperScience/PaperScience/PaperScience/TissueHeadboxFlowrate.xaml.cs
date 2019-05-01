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
    public partial class TissueHeadboxFlowRate : ContentPage
    {
        public TissueHeadboxFlowRate()
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
                label1.Text = "Throat opening (mm)";
                label2.Text = "Spouting velocity (m/min)";
                label4.Text = "Note: assumes contraction (orifice) coefficient = 1.0";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                s.Text = "US";
                label1.Text = "Throat opening (in.)";
                label2.Text = "Spouting velocity (ft/min)";
                label4.Text = "Note: assumes contraction (orifice) coefficient = 1.0";
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
            if (string.IsNullOrWhiteSpace(TO.Text) || string.IsNullOrWhiteSpace(V.Text))
            {
                label3.Text = "Please enter all values.";
            }
            else if (Double.TryParse(TO.Text, out num) && Double.TryParse(V.Text, out num))
            {
                var opening = double.Parse(TO.Text, CultureInfo.InvariantCulture);
                var velocity = double.Parse(V.Text, CultureInfo.InvariantCulture);

                if (!App.switched)
                {
                    var f = opening * velocity / 19.25;
                    label3.Text = "Headbox Flow Rate: " + String.Format("{0:n4}", f) + " GPM/in.";
                }
                else
                {
                    var f = opening * velocity;
                    label3.Text = "Headbox Flow Rate: " + String.Format("{0:n4}", f) + " LPM/m";
                }
            }
            else
            {
                label3.Text = "Please enter a valid number.";
            }
        }
    }
}