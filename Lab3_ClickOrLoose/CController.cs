using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab3_ClickOrLoose
{
    public class CController
    {
        private List<CObject> objects;
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
            objects = new List<CObject>();
            this.spawnRate = spawnRate;
            this.sceneSize = sceneSize;
            points = 0;
        }

        public double GetPoints() { return points; }
        public List<CObject> GetObjects() { return objects; }

        public void SpawnObject()
        {
            double randomSize = rng.Next((int)minSize, (int)maxSize);
            double x = rng.Next(0, (int)(sceneSize.Width - randomSize));
            double y = rng.Next(0, (int)(sceneSize.Height - randomSize));
            double life = minLifetime + rng.NextDouble() * (maxLifetime - minLifetime);

            objects.Add(new CObject(new Point(x, y), randomSize, life));
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

        public void MouseClick(Point mousePosition)
        {
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                if (objects[i].IsMouseOnObject(mousePosition))
                {
                    points += objects[i].GetPointsValue();
                    objects.RemoveAt(i);
                    break;
                }
            }
        }
    }
}