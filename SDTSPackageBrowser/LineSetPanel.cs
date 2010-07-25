using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDTSLib.ISO8211Data;
using SDTSLib.SDTSData;
using Transform;
using Projection;

namespace STDSPackageBrowser
{
    public partial class LineSetPanel : UserControl
    {
        private bool _useProjection;
        public bool UseProjection 
        {
            get
            {
                return _useProjection;
            }
            set
            {
                if (value || _projection != null)
                {
                    _useProjection = value;
                    _transform = null;
                }
            }
        }
        private const int ScreenSnapDistance = 2;

        private RectangleF _boundingRect;
        private LineSet _lineSet;
        private CatalogDirectory _package;
        private ScreenTransform _transform;
        private ScreenTransform Transform
        {
            get
            {
                if( _transform == null )
                {
                    var xVals = _lineSet.Lines.SelectMany( line => line.Points ).Select( point => point.X );
                    var yVals = _lineSet.Lines.SelectMany( line => line.Points ).Select( point => point.Y );

                    var xMin = xVals.Min();
                    var xMax = xVals.Max();
                    var yMin = yVals.Min();
                    var yMax = yVals.Max();
                    xMin -= (xMax - xMin) / 20;
                    xMax += (xMax - xMin) / 20;
                    yMin -= (yMax - yMin) / 20;
                    yMax += (yMax - yMin) / 20; 
                    if( UseProjection )
                    {
                        _transform = new ScreenTransform( xMin, xMax, yMin, yMax );
                    }
                    else
                    {
                        PointD ulCorner = _projection.FromProjection( new PointD { X = xMin, Y = yMax } );
                        PointD lrCorner = _projection.FromProjection( new PointD { X = xMax, Y = yMin } );
                        _transform = new ScreenTransform( ulCorner.X, lrCorner.X, ulCorner.Y, lrCorner.Y );
                    }
                }
                return _transform;
            }
        }

        private IProjection _projection;

        private Point _lastMousePoint;

        private bool _dragging;
        private List<Line> _selectedLines;
        private ToolStripDropDown _popup;

        public LineSetPanel()
        {
            InitializeComponent();
            MouseWheel += LineSetPanel_MouseWheel;
            _popup = new ToolStripDropDown();
            _selectedLines = new List<Line>();
            UseProjection = true;
        }


        public void LoadLineSet( DataDefinitionFile ddf )
        {
            _lineSet = new LineSet( ddf );

        }

        public void LoadPackage( CatalogDirectory package )
        {
            LoadLineSet( package.Entries.First( entry => entry.Contents.ToUpper() == "LINE" ).File );
            _package = package;
            _projection = package.Projection;
        }

        private void LineSetPanel_Paint( object sender, PaintEventArgs e )
        {
            if( _boundingRect == null )
                _boundingRect = e.Graphics.ClipBounds;
            e.Graphics.FillRectangle( Brushes.Aquamarine, e.Graphics.ClipBounds );

            foreach( var line in _lineSet.Lines )
            {
                Pen pen = Pens.Black;
                if( _selectedLines.Contains(line) )
                    pen = Pens.Red;
                DrawLine( line, pen, e.Graphics );
            }
        }

        private void DrawLine( Line line, Pen pen, Graphics graphics )
        {
            for( int i = 1; i < line.Points.Count; i++ )
            {
                if( UseProjection )
                {
                    graphics.DrawLine( pen,
                        Transform.ToScreenPoint( graphics.ClipBounds, line.Points[ i - 1 ] ),
                        Transform.ToScreenPoint( graphics.ClipBounds, line.Points[ i ] ) );
                }
                else
                {
                    graphics.DrawLine( pen,
                        Transform.ToScreenPoint( graphics.ClipBounds, _projection.FromProjection( line.Points[ i - 1 ] ) ),
                        Transform.ToScreenPoint( graphics.ClipBounds, _projection.FromProjection( line.Points[ i ] ) ) );
                }
            }
        }

        private void LineSetPanel_MouseDown( object sender, MouseEventArgs e )
        {
            _lastMousePoint = e.Location;
            _dragging = true;
        }

        private void LineSetPanel_MouseMove( object sender, MouseEventArgs e )
        {
            if( _dragging )
            {
                Transform.ShiftByScreenDelta( _boundingRect, _lastMousePoint.X - e.X, _lastMousePoint.Y - e.Y );
                _lastMousePoint = e.Location;
                Refresh();
            }
        }

        private void LineSetPanel_MouseUp( object sender, MouseEventArgs e )
        {
            _dragging = false;
        }

        void LineSetPanel_MouseWheel( object sender, MouseEventArgs e )
        {
            if( e.Delta > 0 )
                Transform.ZoomIn( Math.Abs(e.Delta / 10) );
            else
                Transform.ZoomOut( Math.Abs( e.Delta / 10 ) );
            Refresh();
        }

        private void LineSetPanel_MouseClick( object sender, MouseEventArgs e )
        {
            if( e.Button == MouseButtons.Left )
            {                
                Line closestLine = GetClosestLine( e.Location ); ;
                if( closestLine != null )
                {
                    if( Control.ModifierKeys == Keys.Control )
                    {
                        if( _selectedLines.Contains( closestLine ) )
                            _selectedLines.Remove( closestLine );
                        else
                            _selectedLines.Add( closestLine );
                    }
                    else
                    {
                        _selectedLines.Clear();
                        _selectedLines.Add( closestLine );
                    }
                    Refresh();
                }
            }
            else if( e.Button == MouseButtons.Right && _package != null )
            {
                Line closestLine = GetClosestLine( e.Location ); ;
                if (closestLine != null)
                {
                    _popup.Items.Clear();
                    _popup.Items.Add( new ToolStripControlHost( new AttributeDisplayer( closestLine, _package ) ) );
                    _popup.Show( this, _lastMousePoint );
                }
            }
        }

        private Line GetClosestLine( Point location )
        {
            double? minDistance = null;
            Line closestLine = null;
            PointD coord = Transform.ToCoordinatePoint( _boundingRect, location );
            foreach( var line in _lineSet.Lines )
            {
                for( int i = 1; i < line.Points.Count; i++ )
                {
                    PointD startPoint = line.Points[ i - 1 ];
                    PointD endPoint = line.Points[ i ];
                    if( !UseProjection )
                    {
                        startPoint = _projection.FromProjection( startPoint );
                        endPoint = _projection.FromProjection( endPoint );
                    }
                    var distance = ScreenTransform.DistanceBetweenPointAndSegment( coord, startPoint, endPoint );
                    if( !minDistance.HasValue || distance < minDistance )
                    {
                        closestLine = line;
                        minDistance = distance;
                    }
                }
            }
            if( Math.Abs( minDistance.Value * Transform.GetScale( _boundingRect ) ) <= ScreenSnapDistance )
                return closestLine;
            return null;
        }

        private void LineSetPanel_Resize( object sender, EventArgs e )
        {
            _boundingRect = this.ClientRectangle;
            Refresh();
        }
    }
}
