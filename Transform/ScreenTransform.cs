using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Projection;

namespace Transform
{
    public class ScreenTransform
    {
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }

        public ScreenTransform( double minX, double maxX, double minY, double maxY )
        {
            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        public void ZoomIn( int percent )
        {
            ApplyZoom( Math.Min( percent, 50 ) );
        }

        public void ZoomOut( int percent )
        {
            ApplyZoom( -1 * percent );
        }
        
        public PointF ToScreenPoint( RectangleF rect, PointD point )
        {
            double scale = GetScale( rect );
            float screenX = (float)((point.X - (MinX + MaxX) / 2) * scale + rect.Width / 2);
            float screenY = (float)((point.Y - (MinY + MaxY) / 2) * scale + rect.Height / 2);
            return new PointF( screenX, screenY );
        }

        public PointD ToCoordinatePoint( RectangleF rect, PointF point)
        {
            double scale = GetScale( rect );
            double coordX = ((point.X - rect.Width / 2) / scale + (MinX + MaxX) / 2);
            double coordY = ((point.Y - rect.Height / 2) / scale + (MinY + MaxY) / 2);
            return new PointD( coordX, coordY );            
        }

        public void ShiftByScreenDelta( RectangleF rect, int deltaX, int deltaY)
        {
            var scale = GetScale( rect );
            MinX += (float)(deltaX / scale);
            MaxX += (float)(deltaX / scale);
            MinY += (float)(deltaY / scale);
            MaxY += (float)(deltaY / scale);
        }

        private void ApplyZoom( int percent )
        {
            double deltaX = (MaxX - MinX) * percent / 200;
            double deltaY = (MaxY - MinY) * percent / 200;
            MinX += deltaX;
            MaxX -= deltaX;
            MinY += deltaY;
            MaxY -= deltaY;
        }

        public double GetScale( RectangleF rect )
        {
            double scaleX = Math.Abs(rect.Width / (MaxX - MinX));
            double scaleY = Math.Abs(rect.Height / (MaxY - MinY));
            return Math.Min( scaleX, scaleY );
        }

        public static double DistanceBetweenPointAndSegment( PointD point, PointD segmentStart, PointD segmentEnd )
        {
            Vector AB = new Vector( segmentStart, segmentEnd );
            Vector BA = new Vector( segmentEnd, segmentStart );
            Vector AC = new Vector( segmentStart, point );
            Vector BC = new Vector( segmentEnd, point );
            if( AB * BC > 0 )
                return BC.Magnitude;
            if( BA * AC > 0 )
                return AC.Magnitude;
            return Math.Abs((AB ^ BC) / AB.Magnitude);
        }
    }
}
