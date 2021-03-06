﻿/**
 * WiredIn - Visual Reminder of Suspended Tasks
 *
 * The MIT License (MIT)
 * Copyright (c) 2012 Yikun Liu, https://github.com/yikliu
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of
 * this software and associated documentation files (the "Software"), to deal in the
 * Software without restriction, including without limitation the rights to use, copy,
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
/// <summary>
/// The View namespace.
/// </summary>
namespace WiredIn.Visualization.View
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Class ClockView.
    /// </summary>
    class ClockView : AbstractView
    {
        #region Fields

        /// <summary>
        /// The hour color
        /// </summary>
        private Color hourColor = Color.Green;
        /// <summary>
        /// The minute color
        /// </summary>
        private Color minuteColor = Color.Green;
        /// <summary>
        /// The second color
        /// </summary>
        private Color secondColor = Color.Green;
        
        private System.DateTime clockTime = DateTime.Today;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClockView"/> class.
        /// </summary>
        public ClockView()
        {
            //InitializeComponent();
            //Sets the rendering mode of the control to double buffer to stop flickering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            MakeRound();
            viewName = "clock";
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the color of the hour.
        /// </summary>
        /// <value>The color of the hour.</value>
        public Color HourColor
        {
            get
            {
                return hourColor;
            }
            set
            {
                if (hourColor != value)
                {
                    hourColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the minute.
        /// </summary>
        /// <value>The color of the minute.</value>
        public Color MinuteColor
        {
            get
            {
                return minuteColor;
            }
            set
            {
                if (minuteColor != value)
                {
                    minuteColor = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the second.
        /// </summary>
        /// <value>The color of the second.</value>
        public Color SecondColor
        {
            get
            {
                return secondColor;
            }
            set
            {
                if (secondColor != value)
                {
                    secondColor = value;
                    Invalidate();
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Activates a specified control.
        /// </summary>
        /// <param name="active">The <see cref="T:System.Windows.Forms.Control" /> being activated.</param>
        /// <returns>true if the control is successfully activated; otherwise, false.</returns>
        /// <exception cref="System.Exception">The method or operation is not implemented.</exception>
        public bool ActivateControl(System.Windows.Forms.Control active)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Gets the count of steps.
        /// </summary>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.Exception">The method or operation is not implemented.</exception>
        public override int GetCountOfSteps()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <returns>System.Int32.</returns>
        /// <exception cref="System.Exception">The method or operation is not implemented.</exception>
        public override int GetScore()
        {
            return 0;
        }

        /// <summary>
        /// Moves the view.
        /// </summary>
        /// <param name="goToGood">if set to <c>true</c> [go to good].</param>
        /// <exception cref="System.Exception">The method or operation is not implemented.</exception>
        public override void MoveView(bool goToGood)
        {
            if (goToGood)
            {
                clockTime = DateTime.Today; //roll back to 0:00;
            }
            else
            {
                clockTime = clockTime.AddSeconds(1.0); //increment one second
            }
            Invalidate();
        }

        /// <summary>
        /// Sets up.
        /// </summary>
        /// <exception cref="System.Exception">The method or operation is not implemented.</exception>
        public override void SetUp()
        {
            clockTime = DateTime.Today;
        }

        /// <summary>
        /// Tears down.
        /// </summary>
        /// <exception cref="System.Exception">The method or operation is not implemented.</exception>
        public override void TearDown()
        {
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            SolidBrush blueBrush = new SolidBrush(Color.White);
            e.Graphics.FillRegion(blueBrush, this.Region);
            
            //Smooths out the appearance of the control
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            //The center of the control, that is used as center for the clock
            PointF center = new PointF(this.Width / 2, this.Height / 2);
            //The distace of the text from the center
            float textRadius = (Math.Min(Width, Height) - Font.Height) / 2;
            //The distance of the margin points from the center
            float outerRadius = Math.Min(Width, Height) / 2 - Font.Height;
            //The length of the hour line
            float hourRadius = outerRadius * 6 / 9;
            //The length of the minute line
            float minuteRadius = outerRadius * 7 / 9;
            //The length of the second line
            float secondRadius = outerRadius * 8 / 9;
            for (int i = 1; i <= 60; i++)
            {
                //Gets the angle of the outer dot
                float angle = GetAngle(i / 5f, 12);
                //Gets the location of the outer dot
                PointF dotPoint = GetPoint(center, outerRadius, angle);
                //Indicates the size of the point
                int pointSize = 2;

                //Is true when a large dot needs to be rendered
                if (i % 5 == 0)
                {
                    //Sets the size of the point to make it bigger
                    pointSize = 4;
                    //The hour number
                    string text = (i / 5).ToString();
                    SizeF sz = e.Graphics.MeasureString(text, Font);
                    //The point where the text should be rendered
                    PointF textPoint = GetPoint(center, textRadius, angle);
                    //Offsets the text location so it is centered in that point.
                    textPoint.X -= sz.Width / 2;
                    textPoint.Y -= sz.Height / 2;
                    //Draws the hour number
                    e.Graphics.DrawString(text, Font, new SolidBrush(Color.Red), textPoint);
                }

                Pen pen = new Pen(new SolidBrush(Color.Black), 1);
                //Draws the outer dot of the clock
                e.Graphics.DrawEllipse(pen, dotPoint.X - pointSize / 2, dotPoint.Y - pointSize / 2, pointSize, pointSize);
                pen.Dispose();
            }

            //Calculates the hour offset from the large outer dot
            float min = ((float)clockTime.Minute) / 60;

            float secondfraction = (float) clockTime.Second / 60;
            //Calculates the angle of the hour line
            float hourAngle = GetAngle(clockTime.Hour + min, 12);
            //Calculates the angle of the minute line
            float minuteAngle = GetAngle(clockTime.Minute + secondfraction, 60);
            //Calculates the angle of the second line
            float secondAngle = GetAngle(clockTime.Second, 60);
            //Draws the clock lines
            DrawLine(e.Graphics, this.secondColor, 1, center, secondRadius, secondAngle);
            DrawLine(e.Graphics, this.minuteColor, 2, center, minuteRadius, minuteAngle);
            DrawLine(e.Graphics, this.hourColor, 3, center, hourRadius, hourAngle);

            e.Graphics.ResetTransform();
        }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            this.BackColor = System.Drawing.Color.Red;
        }

        /// <summary>
        /// Handles the Paint event of the Clock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void Clock_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// Handles the SizeChanged event of the Clock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Clock_SizeChanged(object sender, EventArgs e)
        {
            MakeRound();
            Invalidate();
        }

        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="color">The color.</param>
        /// <param name="penWidth">Width of the pen.</param>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="angle">The angle.</param>
        private void DrawLine(Graphics g, Color color, int penWidth, PointF center, float radius, float angle)
        {
            //Calculates the end point of the line
            PointF endPoint = GetPoint(center, radius, angle);
            //Creates the pen used to render the line
            Pen pen = new Pen(new SolidBrush(color), penWidth);
            //Renders the line
            g.DrawLine(pen, center, endPoint);
            pen.Dispose();
        }

        /// <summary>
        /// Gets the angle.
        /// </summary>
        /// <param name="clockValue">The clock value.</param>
        /// <param name="divisions">The divisions.</param>
        /// <returns>System.Single.</returns>
        private float GetAngle(float clockValue, float divisions)
        {
            //Calculates the angle
            return 360 - (360 * (clockValue) / divisions) + 90;
        }

        /// <summary>
        /// Gets the point.
        /// </summary>
        /// <param name="center">The center.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="angle">The angle.</param>
        /// <returns>PointF.</returns>
        private PointF GetPoint(PointF center, float radius, float angle)
        {
            //Calculates the X coordinate of the point
            float x = (float)Math.Cos(2 * Math.PI * angle / 360) * radius + center.X;
            //Calculates the Y coordinate of the point
            float y = -(float)Math.Sin(2 * Math.PI * angle / 360) * radius + center.Y;
            return new PointF(x, y);
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.BackColor = System.Drawing.Color.White;
            this.HourColor = System.Drawing.Color.Green;
            this.Location = new System.Drawing.Point(67, 49);
            this.MinuteColor = System.Drawing.Color.Green;
            this.Name = "cloc1";
            this.SecondColor = System.Drawing.Color.Green;
            this.Size = new System.Drawing.Size(150, 150);
            
            //
            // Clock
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Clock";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Clock_Paint);
            this.SizeChanged += new System.EventHandler(this.Clock_SizeChanged);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Makes the round.
        /// </summary>
        private void MakeRound()
        {
            GraphicsPath gp = new GraphicsPath();
            float min = Math.Min(Width, Height);
            //Creates the ellipse shape
            gp.AddEllipse((Width - min) / 2, (Height - min) / 2, min, min);
            //Creates the ellipse region
            Region rgn = new Region(gp);

            //Sets the ellipse region to the control
            this.Region = rgn;
        }

        #endregion Methods
    }
}