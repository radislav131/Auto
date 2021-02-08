using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Auto
{
    public partial class Form1 : Form
    {
        private int _width = 840;
        private int _height = 650;
        Rectangle invalidrect;
        public int set=0;
        
        public Bitmap acar = Resource1.carr;
        public Bitmap gif = Resource1.transparent_sprite_zombie_6;
        public bool t = true;
        public int xx = 180;
        public int yy = 600;
        public PointF rect;
        public List<PointF> list;
        public PointF _x;
        public bool x = true;


        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            
            this.Width = _width;
            this.Height = _height;
            this.Text = "Auto";
            _x = new PointF(xx, yy);
            RandGo();
            timer1.Interval = 20;
            timer1.Tick += new EventHandler(update);
            timer1.Start();
            this.KeyDown += new KeyEventHandler(OKP);
            timer3.Interval = 1;
            timer3.Tick += new EventHandler(Find);
            timer3.Start();


            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            
            invalidrect = new Rectangle(invalidrect.X, invalidrect.Y = 280, 100, 100);




        }
        //Отслеживание столкновений и счетчик удачных переходов через дорогу
        private void Find(object sender, EventArgs e)
        {
            if (Math.Abs(invalidrect.Y - rect.Y) <= 50)
            {
                if (Math.Abs(invalidrect.X - rect.X) <= 50)
                {
                    MessageBox.Show("Collide!!!!");
                    DestroyHandle();
                }
            }
            if (invalidrect.X > 800)
            {
                invalidrect.X = -50;
                set += 1;
                label1.Text = set.ToString();
                

            }
        }
        //Появление и движение машины
        private void update(object sender, EventArgs e)
        {
            

            rect.Y -= 10;
            if (rect.Y < -100)
            {
                
                rect = list[new Random().Next(0, list.Count)];
                rect.Y = 600;
            }
            Invalidate();
        }
        //Создание списка PoinF для рандомного появления машины 
        public void RandGo()
        {
            
            
            list = new List<PointF>();
            
            if (t == true)
            {
                t = false;
                for (int i = 0; i < 4; i++)
                {
                    list.Add(_x);
                    xx += 125;
                    _x = new PointF(xx, yy);

                }
            }
            
            
            
            
        }
        
        private void OnFrameChanged(object o,EventArgs e)
        {
            this.Invalidate(invalidrect);
        }
        //Анимация
        private void Form1_Load(object sender, EventArgs e)
            {
            if (ImageAnimator.CanAnimate(gif))
            {
                ImageAnimator.Animate(gif, new EventHandler(this.OnFrameChanged));
            }
            }
        //Прорисовка
        public void  Form1_Paint(object sender, PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                
                g.DrawImage(acar, new Rectangle((int)rect.X,(int)rect.Y,100,100));
                ImageAnimator.UpdateFrames(gif);
                g.DrawImage(gif, invalidrect);
        }

        private void timer1_Tick(object sender, EventArgs e)
            {
                this.Refresh();
            }
       
   //Управление 
        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    {
                        invalidrect.X += 10;
                }


                    break;
                case "Left":
                    {

                        invalidrect.X -= 10;
                      
                    }
                    break;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }

    
}