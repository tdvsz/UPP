using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfApp4
{
    public partial class MainWindow : Window
    {
        private Thread rotationThread;
        private bool isRotating = false;

        public MainWindow()
        {
            InitializeComponent();

            var currentMargin = lbl.Margin;
            var targetMargin = new Thickness(currentMargin.Left + 20, currentMargin.Top + 20, currentMargin.Right, currentMargin.Bottom);

            ThicknessAnimation anim = new ThicknessAnimation
            {
                From = currentMargin,
                To = targetMargin,
                Duration = TimeSpan.FromSeconds(1),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            lbl.BeginAnimation(Label.MarginProperty, anim);

            currentMargin = model.Margin;
            targetMargin = new Thickness(currentMargin.Left + 20, currentMargin.Top + 20, currentMargin.Right, currentMargin.Bottom);
            ThicknessAnimation modelAnim = new ThicknessAnimation
            {
                From = currentMargin,
                To = targetMargin,
                Duration = TimeSpan.FromSeconds(1),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            model.BeginAnimation(Viewport3D.MarginProperty, modelAnim);
        }

        private void ToggleRotationButton_Click(object sender, RoutedEventArgs e)
        {
            if (isRotating)
            {
                StopRotation();
            }
            else
            {
                StartRotation();
            }
        }

        private void StartRotation()
        {
            isRotating = true;
            rotationThread = new Thread(RandomAngle)
            {
                IsBackground = true
            };
            rotationThread.Start();
        }

        private void StopRotation()
        {
            isRotating = false;
            rotationThread = null;
        }

        private void RandomAngle()
        {
            Random random = new Random();

            while (isRotating)
            {
                int xAngle = random.Next(0, 360);
                int yAngle = random.Next(0, 360);
                int zAngle = random.Next(0, 360);

                Dispatcher.Invoke(() =>
                {
                    XangleSlider.Value = xAngle;
                    YangleSlider.Value = yAngle;
                    ZangleSlider.Value = zAngle;
                });

                Thread.Sleep(100);
            }
        }
    }
}
