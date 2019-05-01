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
    public partial class ApproximateSpoutingVelocity : ContentPage
    {
        public ApproximateSpoutingVelocity()
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
                label1.Text = "Head (m of H20)";
                s.Text = "SI";
                label2.IsVisible = false;
                head2.IsVisible = false;
                label3.IsVisible = false;
                head3.IsVisible = false;
                label4.IsVisible = false;
                head4.IsVisible = false;
                App.switched = true;
                answer.IsVisible = false;
                answer.IsVisible = false;
                or1.IsVisible = false;
                or2.IsVisible = false;
                or3.IsVisible = false;
            }
            else
            {
                label1.Text = "Head (in. of H2O)";
                label2.Text = "Head (ft. of H2O)";
                label3.Text = "Head (in. of Hg)";
                label4.Text = "Head (psig)";
                s.Text = "US";
                label2.IsVisible = true;
                head2.IsVisible = true;
                label3.IsVisible = true;
                head3.IsVisible = true;
                label4.IsVisible = true;
                head4.IsVisible = true;
                App.switched = false;
                answer.IsVisible = false;
                answer.IsVisible = false;
                or1.IsVisible = true;
                or2.IsVisible = true;
                or3.IsVisible = true;
            }
        }

        /*
         * Calculate button pressed
         */
        private void Button_Clicked(object sender, EventArgs e)
        {
            answer.IsVisible = true;
            double num;
            if (string.IsNullOrWhiteSpace(head1.Text) && string.IsNullOrWhiteSpace(head2.Text) && string.IsNullOrWhiteSpace(head3.Text) && string.IsNullOrWhiteSpace(head4.Text))
            {
                unit.Text = "Please enter at least one value.";
            }
            else if (Double.TryParse(head1.Text, out num) || Double.TryParse(head2.Text, out num) || Double.TryParse(head3.Text, out num) || Double.TryParse(head4.Text, out num))
            {
                if (!App.switched)
                {
                    if (head1.Text.Length > 0)
                    {
                        if (Double.TryParse(head2.Text, out num) || Double.TryParse(head3.Text, out num) || Double.TryParse(head4.Text, out num))
                        {
                            unit.Text = "Please enter ONLY ONE value.";
                        }
                        else
                        {
                            var H = double.Parse(head1.Text, CultureInfo.InvariantCulture);
                            var velocity = 139.2 * Math.Sqrt(H);
                            unit.Text = "Approx. Spouting Velocity: " + String.Format("{0:n4}", velocity) + " ft/min";
                        }
                    }
                    else if (head2.Text.Length > 0)
                    {
                        if (Double.TryParse(head1.Text, out num) || Double.TryParse(head3.Text, out num) || Double.TryParse(head4.Text, out num))
                        {
                            unit.Text = "Please enter ONLY ONE value.";
                        }
                        else
                        {
                            var H = double.Parse(head2.Text, CultureInfo.InvariantCulture);
                            var velocity = 481.5 * Math.Sqrt(H);
                            unit.Text = "Approx. Spouting Velocity: " + String.Format("{0:n4}", velocity) + " ft/min";
                        }
                    }
                    else if (head3.Text.Length > 0)
                    {
                        if (Double.TryParse(head1.Text, out num) || Double.TryParse(head2.Text, out num) || Double.TryParse(head4.Text, out num))
                        {
                            unit.Text = "Please enter ONLY ONE value.";
                        }
                        else
                        {
                            var H = double.Parse(head3.Text, CultureInfo.InvariantCulture);
                            var velocity = 513.3 * Math.Sqrt(H);
                            unit.Text = "Approx. Spouting Velocity: " + String.Format("{0:n4}", velocity) + " ft/min";
                        }
                    }
                    else if (head4.Text.Length > 0)
                    {
                        if (Double.TryParse(head1.Text, out num) || Double.TryParse(head2.Text, out num) || Double.TryParse(head3.Text, out num))
                        {
                            unit.Text = "Please enter ONLY ONE value.";
                        }
                        else
                        {
                            var H = double.Parse(head4.Text, CultureInfo.InvariantCulture);
                            var velocity = 732.3 * Math.Sqrt(H);
                            unit.Text = "Approx. Spouting Velocity: " + String.Format("{0:n4}", velocity) + " ft/min";
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(head1.Text))
                    {
                        unit.Text = "Please enter a value.";
                    }
                    else if (!Double.TryParse(head1.Text, out num))
                    {
                        unit.Text = "Please enter a valid number.";
                    } 
                    else
                    {
                        var K = 265.7;
                        var H = double.Parse(head1.Text, CultureInfo.InvariantCulture);
                        var velocity = K * Math.Sqrt(H);
                        unit.Text = "Approx. Spouting Velocity: " + String.Format("{0:n4}", velocity) + " m/min";
                    }
                }
            }
            else
            {
                unit.Text = "Please enter a valid number.";
            }
        }
    }
}