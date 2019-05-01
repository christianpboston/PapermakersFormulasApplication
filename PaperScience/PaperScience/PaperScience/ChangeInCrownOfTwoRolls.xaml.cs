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
    public partial class ChangeInCrownOfTwoRolls : ContentPage
    {
        public ChangeInCrownOfTwoRolls()
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
                label1.Text = "Nip width at ends (mm)";
                label2.Text = "Nip width at center (mm)";
                label3.Text = "Top roll diameter (mm)";
                label4.Text = "Bottom roll diameter (mm)";
                label5.Text = "Change in total crown";
                s.Text = "SI";
                App.switched = true;
                answer.IsVisible = false;
            }
            else
            {
                label1.Text = "Nip width at ends (in.)";
                label2.Text = "Nip width at center (in.)";
                label3.Text = "Top roll diameter (in.)";
                label4.Text = "Bottom roll diameter (in.)";
                label5.Text = "Change in total crown";
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
            if (string.IsNullOrWhiteSpace(ew.Text) || string.IsNullOrWhiteSpace(cw.Text) || string.IsNullOrWhiteSpace(td.Text) || string.IsNullOrWhiteSpace(bd.Text))
            {
                label5.Text = "Please enter all values.";
            }
            else if (Double.TryParse(ew.Text, out num) && Double.TryParse(cw.Text, out num) && Double.TryParse(td.Text, out num) && Double.TryParse(bd.Text, out num))
            {
                var endWidth = double.Parse(ew.Text, CultureInfo.InvariantCulture);
                var centerWidth = double.Parse(cw.Text, CultureInfo.InvariantCulture);
                var topDiameter = double.Parse(td.Text, CultureInfo.InvariantCulture);
                var bottomDiameter = double.Parse(bd.Text, CultureInfo.InvariantCulture);

                var top = (Math.Pow(endWidth, 2) - Math.Pow(centerWidth, 2)) * (topDiameter + bottomDiameter);
                var bottom = 2.0 * topDiameter * bottomDiameter;

                var totalChange = top / bottom;

                if (!App.switched)
                {
                    label5.Text = "Change in total crown: " + String.Format("{0:n4}", totalChange) + " in.";
                }
                else
                {
                    label5.Text = "Change in total crown: " + String.Format("{0:n4}", totalChange) + " mm";
                }
                
            }
            else
            {
                label5.Text = "Please enter a valid number.";
            }
        }
    }
}