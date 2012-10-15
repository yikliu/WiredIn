﻿using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Wired_In.View
{
    class ImageView : AbstractView
    {
        private int numOfPics = 0;
        private int currentID = 0;        

        public ImageView()
        {            
            countNumberOfFiles();
            currentID = numOfPics - 1; //THe last picture is best one
        }
    
        public Image getImageByID(int id){                       
            string path = Application.StartupPath + "//more_pics//" + id + ".jpg";
            return Image.FromFile(path);
        }
               
        /*
        public int getCurrentID(double score){
             return numOfPics + 1 - (int)Math.Ceiling(score * numOfPics);
        }
        */

        public void disposeImage(Bitmap p)
        {
            if (p != null)
            {
                p.Dispose();
            }
        }

        public static Bitmap CreateFastBitmap(Image src)
        {
            Bitmap dest = new Bitmap(src.Width, src.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.DrawImage(src, 0, 0, src.Width, src.Height);
            }           
            return dest;
        }


        private delegate void RefreshViewDelegate();

        public void RefreshView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new RefreshViewDelegate(RefreshView));
            }
            else
            {
                disposeImage(this.content);
                this.content = CreateFastBitmap(getImageByID(currentID));
                this.Refresh();
            }
        }

        public override void setUp() { }

        public override void tearDown() { }

        public override void pause() { }

        
        public override void updateView(bool goToGood)
        {            
            if (!goToGood && currentID != 0)
            {
                currentID--;
                RefreshView();
            }else if(goToGood && (currentID != numOfPics - 1)){
                currentID++;
                RefreshView();
            }
        }

        //Image image;

        private void countNumberOfFiles()
        {
            String path = Application.StartupPath + "//more_pics//";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            numOfPics = dir.GetFiles().Length;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.Height = this.content.Height;
            this.Width = this.content.Width;
            e.Graphics.Clear(this.BackColor);
            if (this.content != null)
            {
                e.Graphics.DrawImage(this.content, 0, 0,this.content.Width,this.content.Height);
            }
            String str = /*"Score:" + score +*/ "Pic:" + currentID;
            using (Font myFont = new Font("Arial", 13))
            {
                e.Graphics.DrawString(str, myFont, Brushes.Yellow, new Point(2, 2));
            }    
            
        }

        
        
    }
}