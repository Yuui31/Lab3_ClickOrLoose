using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab3_ClickOrLoose
{
    public class CController
    {
        // Храним список именно РОДИТЕЛЕЙ, но класть туда будем НАСЛЕДНИКОВ
        private List<CCollectable> objects;
        private double spawnRate;
        private double timeSinceLastSpawn;
        private Random rng;

        private double minLifetime = 1.0;
        private double maxLifetime = 5.0;
        private double minSize = 20.0;
        private double maxSize = 50.0;

        private Size sceneSize;
        private double points;

        public CController(double spawnRate, Size sceneSize)
        {
            rng = new Random();
            objects = new List<CCollectable>();
            this.spawnRate = spawnRate;
            this.sceneSize = sceneSize;
            points = 0;
        }

        public double GetPoints() { return points; }
        public List<CCollectable> GetObjects() { return objects; }

        public void SpawnObject()
        {
            double randomSize = rng.Next((int)minSize, (int)maxSize);
            double x = rng.Next(0, (int)(sceneSize.Width - randomSize));
            double y = rng.Next(0, (int)(sceneSize.Height - randomSize));
            double life = minLifetime + rng.NextDouble() * (maxLifetime - minLifetime);
            Point pos = new Point(x, y);

            // Случайно выбираем, кто родится
            int type = rng.Next(0, 10);
            CCollectable newObj;

            if (type < 5) newObj = new CPointGiver(pos, randomSize, life);        // 50%
            else if (type < 7) newObj = new CClickSpeedUp(pos, randomSize, life); // 20%
            else if (type < 9) newObj = new CLifetimeModifier(pos, randomSize, life); // 20%
            else newObj = new CSpamRateChanger(pos, randomSize, life);            // 10%

            objects.Add(newObj);
        }

        public void Update(double delta)
        {
            timeSinceLastSpawn += delta;
            if (timeSinceLastSpawn >= spawnRate)
            {
                SpawnObject();
                timeSinceLastSpawn = 0;
            }

            for (int i = objects.Count - 1; i >= 0; i--)
            {
                if (objects[i].UpdateLifetime(delta))
                    objects.RemoveAt(i);
            }
        }

        // Вызывается из CPlayer
        public bool HandleClick(CPlayer player, Point mousePosition)
        {
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                // ВЫЗОВ АБСТРАКТНОГО МЕТОДА. Сработает версия нужного наследника!
                if (objects[i].OnClick(player, this, mousePosition))
                {
                    objects.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        // Вспомогательные методы для наследников
        public void PointsIncrease(double amount) { points += amount; }

        public void DecreaseSpawnRate(double amount)
        {
            spawnRate -= amount;
            if (spawnRate < 0.1) spawnRate = 0.1;
        }

        public void IncreaseAllLifetimes(double amount)
        {
            foreach (var obj in objects)
                obj.AddLifetime(amount);
        }
    }
}