# Клеточный автомат муравей Лэнгтона.

![image](https://user-images.githubusercontent.com/52111046/144684250-f0e8dd55-85a4-4d45-853f-60546e30b9d6.png)

Правила в изначальном виде змучат так:

- На чёрном квадрате — повернуть на 90° влево, изменить цвет квадрата на белый, сделать шаг вперед на следующую клетку.
- На белом квадрате — повернуть на 90° вправо, изменить цвет квадрата на чёрный, сделать шаг вперед на следующую клетку.

Однако данная цвет не привязана к конкретному классу муравья и при каждом старте цвет для отдельного экземпляра задаётся случайно.
```C#
  color = new Color();

  Colors = new List<Brush>();
  Ants = new List<Ant>();

  for (int i = 0; i < Density; i++)
  }
      Ants.Add(new Ant(pictureBox1.Height / Resolution, pictureBox1.Width / Resolution, StartLocation));

      color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
      Colors.Add(new SolidBrush(color));
  }
```
Связь цвета и экземпляра класса муравья происходит по порядковому номеру в списках Colors и Ants.

За движение муравья и отрисовку отвечает следущяя функция, запускающаяся в отдельном потоке, для увеличения производительности программы.

```C#
 public void AtntStep()
        {
            while (flag)
            {
                for (int i = 0; i < Ants.Count; i++)
                {
                    Ants[i].NewPos();
                    Ants[i].NextStep();
                }
                GameDraw();
            }
        }
```
Рассмотрим её подробнее. В цикле `for` для каждого экземпляра муравья из списка применяются следущие методы:
## NewPos()
  ```C#
  public Vector NewPos()
          {
              while (i < queue.Count)
              {
                  if (!Fild[AntPos.X, AntPos.Y])
                  {
                      i++;
                      if (i > 3) { i = 0; }

                      Increment = new Vector(queue[i].X, queue[i].Y);

                      return Increment;
                  }
                  if (Fild[AntPos.X, AntPos.Y])
                  {
                      i--;
                      if (i == -1) { i = 3; }

                      Increment = new Vector(queue[i].X, queue[i].Y);

                      if (i > 3) { i = 0; }

                      return Increment;
                  }
              }
              return Increment;
          }
  ```
Метод вычисляет приращение для каждого муравья на следущий ход. Происходит это путём вращения списка queue, содержащего набор приращений, согласно которым муравей будет двигаться только по белым или только по чёрным клеткам.
```C#
        private List<Vector> queue = new List<Vector>(4);
        private Vector Right = new Vector(1, 0);
        private Vector Up = new Vector(0, 1);
        private Vector Left = new Vector(-1, 0);
        private Vector Down = new Vector(0, -1);
```
Таким образом, для вычисления приращения нам необходимо знать откудо пришел муравей, переменная `i` по сути задаёт первое приращение. Это нужно потому что в изначальный момент времени муравей приходит из ниоткуда.

## NextStep()
```C#
public void NextStep()
        {
            AntPos = AntPos + Increment;

            if (AntPos.X < 0)
            {
                AntPos.X = (cols - 1);
            }
            if (AntPos.Y < 0)
            {
                AntPos.Y = (rows - 1);
            }
            if (AntPos.X > (cols - 1))
            {
                AntPos.X = 0;
            }
            if (AntPos.Y > (rows - 1))
            {
                AntPos.Y = 0;
            }

            Fild[OldAntPos.X, OldAntPos.Y] = !Fild[OldAntPos.X, OldAntPos.Y];
            OldAntPos = AntPos;
        }
```
Тут всё понятно к текущим координатам прибавляется приращение. Условия служат для проверки выхода за пределы массива.

![image](https://user-images.githubusercontent.com/52111046/144684250-f0e8dd55-85a4-4d45-853f-60546e30b9d6.png)
![image](https://user-images.githubusercontent.com/52111046/144709310-fa75aead-9074-42d1-b84b-e2eb8d280ccf.png)


