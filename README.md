# Клеточный автомат муравей Лэнгтона.
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

![image](https://user-images.githubusercontent.com/52111046/144684250-f0e8dd55-85a4-4d45-853f-60546e30b9d6.png)
![image](https://user-images.githubusercontent.com/52111046/144709310-fa75aead-9074-42d1-b84b-e2eb8d280ccf.png)


