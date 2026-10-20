using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Lab3_ClickOrLoose
{
    public partial class MainWindow : Window
    {
        private CController controller;
        private DispatcherTimer timer;
        private double gameTime = 0;
        private double gameDuration = 10.0;
        private bool isGameRunning = false;

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (isGameRunning) return;
            GameCanvas.Children.Clear();
            controller = new CController(0.5, new Size(GameCanvas.ActualWidth, GameCanvas.ActualHeight));
            gameTime = 0;
            isGameRunning = true;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double delta = 0.1;
            gameTime += delta;

            if (gameTime >= gameDuration)
            {
                timer.Stop();
                isGameRunning = false;
                MessageBox.Show($"Игра окончена! Очки: {controller.GetPoints():F1}");
                return;
            }

            controller.Update(delta);

            // Обновляем картинку по таймеру
            UpdateVisuals();
        }

        private void GameCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isGameRunning) return;

            controller.MouseClick(e.GetPosition(GameCanvas));

            // ВОТ ОНО! Обновляем картинку сразу же после клика
            UpdateVisuals();
        }

        // Новый метод для отрисовки
        private void UpdateVisuals()
        {
            ScoreText.Text = controller.GetPoints().ToString("F1");
            TimeText.Text = $"{gameTime:F1}s / {gameDuration}s";

            GameCanvas.Children.Clear();
            foreach (var obj in controller.GetObjects())
            {
                GameCanvas.Children.Add(obj.GetSprite());
            }
        }
    }
}