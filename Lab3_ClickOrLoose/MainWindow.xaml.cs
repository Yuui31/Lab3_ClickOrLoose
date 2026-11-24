using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Lab3_ClickOrLoose
{
    public partial class MainWindow : Window
    {
        private CController controller;
        private CPlayer player; // Добавили игрока
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

            player = new CPlayer(0.3); // Перезарядка 0.3 секунды (300 мс)
            gameTime = 0;
            isGameRunning = true;
            timer.Start();
            // Отписываемся от старых, если игра запускается повторно
            if (controller != null)
            {
                controller.addObject -= AddObjectInScene;
                controller.removeObject -= RemoveObjectFromScene;
                controller.removeObject -= LogMessage;
            }

            // Подписываемся
            controller.addObject += AddObjectInScene;
            controller.removeObject += RemoveObjectFromScene;
            controller.removeObject += LogMessage;

        }

        private void Timer_Tick(object? sender, EventArgs e)
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

            player.Update(delta); // Обновляем таймер перезарядки
            controller.Update(delta);
            UpdateVisuals();
        }

        private void GameCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!isGameRunning) return;


            // Клик идет сначала в Игрока, а он решает, можно ли бить
            player.MouseClick(e.GetPosition(GameCanvas), controller);
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            ScoreText.Text = controller.GetPoints().ToString("F1");
            TimeText.Text = $"{gameTime:F1}s / {gameDuration}s";
        }
        public void AddObjectInScene(object sender, CControllerEventArgs e)
        {
            GameCanvas.Children.Add(e.sprite);
        }

        public void RemoveObjectFromScene(object sender, CControllerEventArgs e)
        {
            GameCanvas.Children.Remove(e.sprite);
        }

        public void LogMessage(object sender, CControllerEventArgs e)
        {
            LogListBox.Items.Insert(0, "Объект удален!");
        }
    }
}