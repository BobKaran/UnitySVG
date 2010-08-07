using UnityEngine;
using System;

public class SVGGraphicsStroke : uISVGPathDraw {
  private SVGGraphics    _graphics;
  private SVGBasicDraw    _basicDraw;
  private float _width;
  private bool isUseWidth = false;

  //================================================================================
  public SVGGraphicsStroke(SVGGraphics graphics) {
    this._graphics = graphics;

    //Basic Draw
    this._basicDraw = new SVGBasicDraw();
    this._basicDraw.SetPixelMethod = new SetPixelDelegate(SetPixel);
  }
  //================================================================================
  //--------------------------------------------------------------------------------
  //SetPixel
  //--------------------------------------------------------------------------------
  private void SetPixel(int x, int y) {
    this._graphics.SetPixel(x, y);

  }
  //--------------------------------------------------------------------------------
  //Method: StrokeLineCapLeft
  //Ve Line Cap, dau cuoi Left
  //--------------------------------------------------------------------------------
  private void StrokeLineCapLeft(SVGPoint p1, SVGPoint p2, float width) {
    if((int)width == 1)return;
    if((this._graphics.strokeLineCap == SVGStrokeLineCapMethod.Unknown)||
     (this._graphics.strokeLineCap == SVGStrokeLineCapMethod.Butt))return;
    if(((p1.x - p2.x)*(p1.x - p2.x)+(p1.y - p2.y)*(p1.y - p2.y)) <= 4f)return;
    if(this._graphics.strokeLineCap == SVGStrokeLineCapMethod.Round) {
      this._graphics.FillCircle(p1, width/2f);
      return;
    }

    SVGPoint _p1 = new SVGPoint(0f, 0f);
    SVGPoint _p2 = new SVGPoint(0f, 0f);
    SVGPoint _p3 = new SVGPoint(0f, 0f);
    SVGPoint _p4 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(p1, p2, width,
          ref _p1, ref _p2, ref _p3, ref _p4);

    SVGPoint t_p1 = new SVGPoint(0f, 0f);
    SVGPoint t_p2 = new SVGPoint(0f, 0f);
    SVGPoint t_p3 = new SVGPoint(0f, 0f);
    SVGPoint t_p4 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(_p2, _p1, width,
          ref t_p1, ref t_p2, ref t_p3, ref t_p4);

    SVGPoint[] points = new SVGPoint[4];
    points[0] = t_p1;
    points[1] = _p2;
    points[2] = _p1;
    points[3] = t_p3;
    this._graphics.FillPolygon(points);
  }

  //--------------------------------------------------------------------------------
  //Method: StrokeLineCapRight
  //Ve Line Cap, dau cuoi Right
  //--------------------------------------------------------------------------------
  private void StrokeLineCapRight(SVGPoint p1, SVGPoint p2, float width) {
    if((int)width == 1)return;
    if((this._graphics.strokeLineCap == SVGStrokeLineCapMethod.Unknown)||
     (this._graphics.strokeLineCap == SVGStrokeLineCapMethod.Butt))return;

    if(((p1.x - p2.x)*(p1.x - p2.x)+(p1.y - p2.y)*(p1.y - p2.y)) <= 4f)return;
    if(this._graphics.strokeLineCap == SVGStrokeLineCapMethod.Round) {
      this._graphics.FillCircle(p2, width/2f);
      return;
    }

    SVGPoint _p1 = new SVGPoint(0f, 0f);
    SVGPoint _p2 = new SVGPoint(0f, 0f);
    SVGPoint _p3 = new SVGPoint(0f, 0f);
    SVGPoint _p4 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(p1, p2, width,
          ref _p1, ref _p2, ref _p3, ref _p4);

    SVGPoint t_p1 = new SVGPoint(0f, 0f);
    SVGPoint t_p2 = new SVGPoint(0f, 0f);
    SVGPoint t_p3 = new SVGPoint(0f, 0f);
    SVGPoint t_p4 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(_p4, _p3, width,
          ref t_p1, ref t_p2, ref t_p3, ref t_p4);

    SVGPoint[] points = new SVGPoint[4];
    points[0] = _p4;
    points[1] = t_p2;
    points[2] = t_p4;
    points[3] = _p3;
    this._graphics.FillPolygon(points);
  }
  //--------------------------------------------------------------------------------
  //Method: StrokeLineJoin
  //Ve LineJoin
  //--------------------------------------------------------------------------------
  private void StrokeLineJoin(SVGPoint p1, SVGPoint p2, SVGPoint p3, float width) {
    if((int)width == 1)return;
    if(((p1.x - p2.x)*(p1.x - p2.x)+(p1.y - p2.y)*(p1.y - p2.y)) <= 4f)return;
    if(this._graphics.strokeLineJoin == SVGStrokeLineJoinMethod.Round) {
      this._graphics.FillCircle(p2, width/2f);
      return;
    }

    if((this._graphics.strokeLineJoin == SVGStrokeLineJoinMethod.Miter)||
   (this._graphics.strokeLineJoin == SVGStrokeLineJoinMethod.Unknown)) {
      SVGPoint _p1 = new SVGPoint(0f, 0f);
      SVGPoint _p2 = new SVGPoint(0f, 0f);
      SVGPoint _p3 = new SVGPoint(0f, 0f);
      SVGPoint _p4 = new SVGPoint(0f, 0f);

      this._graphics.GetThickLine(p1, p2, width,
          ref _p1, ref _p2, ref _p3, ref _p4);


      SVGPoint _p5 = new SVGPoint(0f, 0f);
      SVGPoint _p6 = new SVGPoint(0f, 0f);
      SVGPoint _p7 = new SVGPoint(0f, 0f);
      SVGPoint _p8 = new SVGPoint(0f, 0f);

      this._graphics.GetThickLine(p2, p3, width,
          ref _p5, ref _p6, ref _p7, ref _p8);

      SVGPoint _cp1, _cp2;
      _cp1 = this._graphics.GetCrossPoint(_p1, _p3, _p5, _p7);
      _cp2 = this._graphics.GetCrossPoint(_p2, _p4, _p6, _p8);


      SVGPoint[] points = new SVGPoint[8];
      points[0] = p2;
      points[1] = _p3;
      points[2] = _cp1;
      points[3] = _p5;

      points[4] = p2;
      points[5] = _p6;
      points[6] = _cp2;
      points[7] = _p4;
      this._graphics.FillPolygon(points);
      return;
    }
    if(this._graphics.strokeLineJoin == SVGStrokeLineJoinMethod.Bevel) {
      SVGPoint _p1 = new SVGPoint(0f, 0f);
      SVGPoint _p2 = new SVGPoint(0f, 0f);
      SVGPoint _p3 = new SVGPoint(0f, 0f);
      SVGPoint _p4 = new SVGPoint(0f, 0f);

      this._graphics.GetThickLine(p1, p2, width,
          ref _p1, ref _p2, ref _p3, ref _p4);


      SVGPoint _p5 = new SVGPoint(0f, 0f);
      SVGPoint _p6 = new SVGPoint(0f, 0f);
      SVGPoint _p7 = new SVGPoint(0f, 0f);
      SVGPoint _p8 = new SVGPoint(0f, 0f);

      this._graphics.GetThickLine(p2, p3, width,
          ref _p5, ref _p6, ref _p7, ref _p8);

      SVGPoint[] points = new SVGPoint[6];
      points[0] = p2;
      points[1] = _p3;
      points[2] = _p5;

      points[3] = p2;
      points[4] = _p6;
      points[5] = _p4;
      this._graphics.FillPolygon(points);
      return;
    }
  }
  //================================================================================
  //--------------------------------------------------------------------------------
  //Methods: MoveTo
  //--------------------------------------------------------------------------------
  public void MoveTo(SVGPoint p) {
    this._basicDraw.MoveTo(p);
  }
  //--------------------------------------------------------------------------------
  //Methods: CircleTo
  //--------------------------------------------------------------------------------
  public void CircleTo(SVGPoint p, float r) {
    if((this.isUseWidth)&&((int)this._width > 1)) {
      CircleTo(p, r, this._width);
      return;
    }
    Circle(p, r);
  }
  //-----
  public void CircleTo(SVGPoint p, float r, float width) {
    Circle(p, r, width);
  }
  //--------------------------------------------------------------------------------
  //Methods: EllipseTo
  //--------------------------------------------------------------------------------
  public void EllipseTo(SVGPoint p, float r1, float r2, float angle) {
    if((this.isUseWidth)&&((int)this._width > 1)) {
      EllipseTo(p, r1, r2, this._width);
      return;
    }
    Ellipse(p, r1, r2, angle);
  }
  //-----
  public void EllipseTo(SVGPoint p, float r1, float r2, float angle, float width) {
    Ellipse(p, r1, r2, angle, width);
  }
  //--------------------------------------------------------------------------------
  //Methods: ArcTo
  //--------------------------------------------------------------------------------
  public void ArcTo(float r1, float r2, float angle,
        bool largeArcFlag, bool sweepFlag, SVGPoint p) {
    if((this.isUseWidth)&&((int)this._width > 1))
      ArcTo(r1, r2, angle, largeArcFlag, sweepFlag, p, this._width);
    else
      this._basicDraw.ArcTo(r1, r2, angle, largeArcFlag, sweepFlag, p);
  }
  //-----
  public void ArcTo(float r1, float r2, float angle,
        bool largeArcFlag, bool sweepFlag, SVGPoint p, float width) {
    float tx, ty, rx, ry;
    rx = r1;
    ry = r2;
    SVGPoint p1 = new SVGPoint(0f, 0f);
    SVGPoint p2 = new SVGPoint(0f, 0f);
    p1.SetValue(this._basicDraw.currentPoint);
    p2.SetValue(p);

    double trx2, try2, tx2, ty2;
    float temp1, temp2;
    float _radian = (angle * Mathf.PI / 180.0f);
    float _CosRadian = (float)Math.Cos(_radian);
    float _SinRadian = (float)Math.Sin(_radian);
    temp1 = (p1.x - p2.x)/2.0f;
    temp2 = (p1.y - p2.y)/2.0f;
    tx = ( _CosRadian * temp1)+(_SinRadian * temp2);
    ty = (-_SinRadian * temp1)+(_CosRadian * temp2);

    trx2 = rx * rx;
    try2 = ry * ry;
    tx2 = tx * tx;
    ty2 = ty * ty;


    double radiiCheck = tx2/trx2 + ty2/try2;
    if(radiiCheck > 1) {
    rx = (float)Math.Sqrt((float)radiiCheck)* rx;
    ry = (float)Math.Sqrt((float)radiiCheck)* ry;
    trx2 = rx * rx;
    try2 = ry * ry;
    }

    double tm1;
    tm1 = (trx2*try2 - trx2*ty2 - try2*tx2)/(trx2*ty2 + try2*tx2);
    tm1 = (tm1 < 0) ? 0 : tm1;

    float tm2;
    tm2 = (largeArcFlag == sweepFlag) ? -(float)Math.Sqrt((float)tm1) : (float)Math.Sqrt((float)tm1);

    float tcx, tcy;
    tcx = tm2 *((rx * ty)/ ry);
    tcy = tm2 *(-(ry * tx)/ rx);

    float cx, cy;
    cx = _CosRadian * tcx - _SinRadian * tcy +((p1.x + p2.x)/ 2.0f);
    cy = _SinRadian * tcx + _CosRadian * tcy +((p1.y + p2.y)/ 2.0f);

    float ux = (tx - tcx)/rx;
    float uy = (ty - tcy)/ry;
    float vx = (-tx - tcx)/rx;
    float vy = (-ty - tcy)/ry;
    float _angle, _delta;

    float tp, n;
    n = (float)Math.Sqrt((ux * ux)+(uy * uy));
    tp = ux;
    _angle = (uy < 0) ? -(float)Math.Acos(tp/n) : (float)Math.Acos(tp/n);
    _angle = _angle * 180.0f / Mathf.PI;
    _angle %= 360f;

    n = (float)Math.Sqrt((ux * ux + uy * uy)*(vx * vx + vy * vy));
    tp = ux * vx + uy * vy;
    _delta = (ux * vy - uy * vx < 0) ? -(float)Math.Acos(tp/n) : (float)Math.Acos(tp/n);
    _delta = _delta * 180.0f / Mathf.PI;

    if(!sweepFlag && _delta > 0) {
      _delta -= 360f;
    } else if(sweepFlag && _delta < 0) {
      _delta += 360f;
    }

    _delta %= 360f;

    int number = 50;
    float deltaT = _delta  / number;
    //---Get Control Point
    SVGPoint _controlPoint1 = new SVGPoint(0f, 0f);
    SVGPoint _controlPoint2 = new SVGPoint(0f, 0f);

    for(int i = 0; i <= number; i++) {
      float t_angle = (deltaT * i + _angle)* Mathf.PI / 180.0f;
      _controlPoint1.x = _CosRadian * rx *(float)Math.Cos(t_angle)-
            _SinRadian * ry *(float)Math.Sin(t_angle)
            + cx;
      _controlPoint1.y = _SinRadian * rx *(float)Math.Cos(t_angle)+
            _CosRadian * ry *(float)Math.Sin(t_angle)
          + cy;
      if((_controlPoint1.x != p1.x)&&(_controlPoint1.y != p1.y)) {
        i = number + 1;
      }
    }


    for(int i = number; i >= 0; i--) {
      float t_angle = (deltaT * i + _angle)* Mathf.PI / 180.0f;
      _controlPoint2.x = _CosRadian * rx *(float)Math.Cos(t_angle)-
            _SinRadian * ry *(float)Math.Sin(t_angle)
            + cx;
      _controlPoint2.y = _SinRadian * rx *(float)Math.Cos(t_angle)+
            _CosRadian * ry *(float)Math.Sin(t_angle)
          + cy;
      if((_controlPoint2.x != p2.x)&&(_controlPoint2.y != p2.y)) {
        i = -1;
      }
    }
    //-----
    SVGPoint _p1 = new SVGPoint(0f, 0f);
    SVGPoint _p2 = new SVGPoint(0f, 0f);
    SVGPoint _p3 = new SVGPoint(0f, 0f);
    SVGPoint _p4 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(p1, _controlPoint1, width,
        ref _p1, ref _p2, ref _p3, ref _p4);

    SVGPoint _p5 = new SVGPoint(0f, 0f);
    SVGPoint _p6 = new SVGPoint(0f, 0f);
    SVGPoint _p7 = new SVGPoint(0f, 0f);
    SVGPoint _p8 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(_controlPoint2, p2, width,
          ref _p5, ref _p6, ref _p7, ref _p8);

    float _half, _ihalf1, _ihalf2;
    _half = width / 2f;
    _ihalf1 = _half;
    _ihalf2 = width - _ihalf1 + 0.5f;
    //-----

    float t_len1, t_len2;
    t_len1 = (_p1.x - cx)*(_p1.x - cx)+(_p1.y - cy)*(_p1.y - cy);
    t_len2 = (_p2.x - cx)*(_p2.x - cx)+(_p2.y - cy)*(_p2.y - cy);

    SVGPoint tempPoint = new SVGPoint(0f, 0f);
    if(t_len1 > t_len2) {
      tempPoint.SetValue(_p1);
      _p1.SetValue(_p2);
      _p2.SetValue(tempPoint);
    }

    t_len1 = (_p7.x - cx)*(_p7.x - cx)+(_p7.y - cy)*(_p7.y - cy);
    t_len2 = (_p8.x - cx)*(_p8.x - cx)+(_p8.y - cy)*(_p8.y - cy);

    if(t_len1 > t_len2) {
      tempPoint.SetValue(_p7);
      _p7.SetValue(_p8);
      _p8.SetValue(tempPoint);
    }

    SVGGraphicsPath _graphicsPath = new SVGGraphicsPath();
    _graphicsPath.AddMoveTo(_p2);
    _graphicsPath.AddArcTo(r1 + _ihalf1, r2 + _ihalf1, angle, largeArcFlag, sweepFlag, _p8);
    _graphicsPath.AddLineTo(_p7);
    _graphicsPath.AddArcTo(r1 - _ihalf2, r2 - _ihalf2, angle, largeArcFlag, !sweepFlag, _p1);
    _graphicsPath.AddLineTo(_p2);
    this._graphics.FillPath(_graphicsPath);

    MoveTo(p);
  }
  //--------------------------------------------------------------------------------
  //Methods: CubicCurveTo
  //--------------------------------------------------------------------------------
  public void CubicCurveTo(SVGPoint p1, SVGPoint p2, SVGPoint p) {
    if((this.isUseWidth)&&((int)this._width > 1)) {
      CubicCurveTo(p1, p2, p, this._width);
      return;
    }
    this._basicDraw.CubicCurveTo(p1, p2, p);
  }
  //-----
  public void CubicCurveTo(SVGPoint p1, SVGPoint p2, SVGPoint p, float width) {
    SVGPoint _point = new SVGPoint(0f, 0f);
    _point.SetValue(this._basicDraw.currentPoint);

    SVGPoint _p1 = new SVGPoint(0f, 0f);
    SVGPoint _p2 = new SVGPoint(0f, 0f);
    SVGPoint _p3 = new SVGPoint(0f, 0f);
    SVGPoint _p4 = new SVGPoint(0f, 0f);

    bool temp;
    temp = this._graphics.GetThickLine(_point, p1, width,
        ref _p1, ref _p2, ref _p3, ref _p4);
    if(temp == false) {
      QuadraticCurveTo(p2, p, width);
      return;
    }

    SVGPoint _p5 = new SVGPoint(0f, 0f);
    SVGPoint _p6 = new SVGPoint(0f, 0f);
    SVGPoint _p7 = new SVGPoint(0f, 0f);
    SVGPoint _p8 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(p1, p2, width,
          ref _p5, ref _p6, ref _p7, ref _p8);

    SVGPoint _p9 = new SVGPoint(0f, 0f);
    SVGPoint _p10 = new SVGPoint(0f, 0f);
    SVGPoint _p11 = new SVGPoint(0f, 0f);
    SVGPoint _p12 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(p2, p, width,
          ref _p9, ref _p10, ref _p11, ref _p12);

    SVGPoint _cp1, _cp2, _cp3, _cp4;
    _cp1 = this._graphics.GetCrossPoint(_p1, _p3, _p5, _p7);
    _cp2 = this._graphics.GetCrossPoint(_p2, _p4, _p6, _p8);
    _cp3 = this._graphics.GetCrossPoint(_p5, _p7, _p9, _p11);
    _cp4 = this._graphics.GetCrossPoint(_p6, _p8, _p10, _p12);


    this._basicDraw.MoveTo(_point);
    this._basicDraw.CubicCurveTo(p1, p2, p);

    SVGGraphicsPath _graphicsPath = new SVGGraphicsPath();
    _graphicsPath.AddMoveTo(_p2);
    _graphicsPath.AddCubicCurveTo(_cp2, _cp4, _p12);
    _graphicsPath.AddLineTo(_p11);
    _graphicsPath.AddCubicCurveTo(_cp3, _cp1, _p1);
    _graphicsPath.AddLineTo(_p2);
    this._graphics.FillPath(_graphicsPath);

    MoveTo(p);

  }
  //--------------------------------------------------------------------------------
  //Methods: QuadraticCurveTo
  //--------------------------------------------------------------------------------
  public void QuadraticCurveTo(SVGPoint p1, SVGPoint p) {
    if((this.isUseWidth)&&((int)this._width > 1)) {
      QuadraticCurveTo(p1, p, this._width);
      return;
    }
    this._basicDraw.QuadraticCurveTo(p1, p);
  }
  //-----
  public void QuadraticCurveTo(SVGPoint p1, SVGPoint p, float width) {
    SVGPoint _point = new SVGPoint(0f, 0f);
    _point.SetValue(this._basicDraw.currentPoint);

    SVGPoint _p1 = new SVGPoint(0f, 0f);
    SVGPoint _p2 = new SVGPoint(0f, 0f);
    SVGPoint _p3 = new SVGPoint(0f, 0f);
    SVGPoint _p4 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(_point, p1, width,
        ref _p1, ref _p2, ref _p3, ref _p4);

    SVGPoint _p5 = new SVGPoint(0f, 0f);
    SVGPoint _p6 = new SVGPoint(0f, 0f);
    SVGPoint _p7 = new SVGPoint(0f, 0f);
    SVGPoint _p8 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(p1, p, width,
          ref _p5, ref _p6, ref _p7, ref _p8);

    SVGPoint _cp1, _cp2;
    _cp1 = this._graphics.GetCrossPoint(_p1, _p3, _p5, _p7);
    _cp2 = this._graphics.GetCrossPoint(_p2, _p4, _p6, _p8);

    SVGGraphicsPath _graphicsPath = new SVGGraphicsPath();
    _graphicsPath.AddMoveTo(_p2);
    _graphicsPath.AddQuadraticCurveTo(_cp2, _p8);
    _graphicsPath.AddLineTo(_p7);
    _graphicsPath.AddQuadraticCurveTo(_cp1, _p1);
    _graphicsPath.AddLineTo(_p2);
    this._graphics.FillPath(_graphicsPath);

    MoveTo(p);
  }
  //--------------------------------------------------------------------------------
  //Methods: LineTo
  //--------------------------------------------------------------------------------
  public void LineTo(SVGPoint p) {
    if((this.isUseWidth)&&((int)this._width > 1)) {
      LineTo(p, this._width);
      return;
    }
    this._basicDraw.LineTo(p);
  }
  //-----
  public void LineTo(SVGPoint p, float width) {
    SVGPoint _point = new SVGPoint(0f, 0f);
    _point.SetValue(this._basicDraw.currentPoint);
    Line(_point, p, width);
    MoveTo(p);
  }
  //--------------------------------------------------------------------------------
  //Methods: Line
  //--------------------------------------------------------------------------------
  public void Line(SVGPoint p1, SVGPoint p2) {
    if((this.isUseWidth)&&((int)this._width > 1)) {
      Line(p1, p2, this._width);
      return;
    }
    this._basicDraw.Line(p1, p2);
  }
  //-----
  public void Line(SVGPoint p1, SVGPoint p2, float width) {
    if((int)width == 1) {
      Line(p1, p2);
    } else {
      if(((p1.x - p2.x)*(p1.x - p2.x)+(p1.y - p2.y)*(p1.y - p2.y)) <= 4f)
        return;
      StrokeLineCapLeft(p1, p2, width);
      StrokeLineCapRight(p1, p2, width);
      SVGPoint _p1 = new SVGPoint(0f, 0f);
      SVGPoint _p2 = new SVGPoint(0f, 0f);
      SVGPoint _p3 = new SVGPoint(0f, 0f);
      SVGPoint _p4 = new SVGPoint(0f, 0f);

      this._graphics.GetThickLine(p1, p2, width,
          ref _p1, ref _p2, ref _p3, ref _p4);

      SVGPoint[] points = new SVGPoint[4];
      points[0] = _p1;
      points[1] = _p3;
      points[2] = _p4;
      points[3] = _p2;
      this._graphics.FillPolygon(points);
    }
  }
  //--------------------------------------------------------------------------------
  //Method: Rect
  //--------------------------------------------------------------------------------
  public void Rect(SVGPoint p1, SVGPoint p2, SVGPoint p3, SVGPoint p4) {
    if((this.isUseWidth)&&((int)this._width > 1)) {
      Rect(p1, p2, p3, p4, this._width);
      return;
    }
    this._basicDraw.Rect(p1, p2, p3, p4);
  }
  //-----
  public void Rect(SVGPoint p1, SVGPoint p2, SVGPoint p3, SVGPoint p4, float width) {
    if((int)width == 1) {
      Rect(p1, p2, p3, p4);
    }
    SVGPoint[] points = new SVGPoint[4];
    points[0] = p1;
    points[1] = p2;
    points[2] = p3;
    points[3] = p4;
    Polygon(points, width);
  }
  //--------------------------------------------------------------------------------
  //Methods: Rounded Rect
  //--------------------------------------------------------------------------------
  public void RoundedRect(SVGPoint p1, SVGPoint p2, SVGPoint p3, SVGPoint p4,
      SVGPoint p5, SVGPoint p6, SVGPoint p7, SVGPoint p8,
      float r1, float r2, float angle) {

    if((this.isUseWidth)&&((int)this._width > 1)) {
      RoundedRect(p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, angle, this._width);
      return;
    }
    this._basicDraw.MoveTo(p1);
    this._basicDraw.LineTo(p2);
    this._basicDraw.ArcTo(r1, r2, angle, false, true, p3);

    this._basicDraw.MoveTo(p3);
    this._basicDraw.LineTo(p4);
    this._basicDraw.ArcTo(r1, r2, angle, false, true, p5);

    this._basicDraw.MoveTo(p5);
    this._basicDraw.LineTo(p6);
    this._basicDraw.ArcTo(r1, r2, angle, false, true, p7);

    this._basicDraw.MoveTo(p7);
    this._basicDraw.LineTo(p8);
    this._basicDraw.ArcTo(r1, r2, angle, false, true, p1);
  }
  //-----
  public void RoundedRect(SVGPoint p1, SVGPoint p2, SVGPoint p3, SVGPoint p4,
      SVGPoint p5, SVGPoint p6, SVGPoint p7, SVGPoint p8,
      float r1, float r2, float angle, float width) {

    if((int)width == 1) {
      RoundedRect(p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, angle);
      return;
    }

    Line(p1, p2, width); Line(p3, p4, width); Line(p5, p6, width); Line(p7, p8, width);
    SVGPoint _p1 = new SVGPoint(0f, 0f);
    SVGPoint _p2 = new SVGPoint(0f, 0f);
    SVGPoint _p3 = new SVGPoint(0f, 0f);
    SVGPoint _p4 = new SVGPoint(0f, 0f);

    this._graphics.GetThickLine(p1, p2, width,
          ref _p1, ref _p2, ref _p3, ref _p4);

    SVGPoint _p5 = new SVGPoint(0f, 0f);
    SVGPoint _p6 = new SVGPoint(0f, 0f);
    SVGPoint _p7 = new SVGPoint(0f, 0f);
    SVGPoint _p8 = new SVGPoint(0f, 0f);

    //-------
    this._graphics.GetThickLine(p3, p4, width,
          ref _p5, ref _p6, ref _p7, ref _p8);

    SVGGraphicsPath _graphicsPath = new SVGGraphicsPath();

    _graphicsPath.AddMoveTo(_p4);
    _graphicsPath.AddArcTo(r1 +(width/2f), r2 +(width/2f),
                  angle, false, true, _p6);
    _graphicsPath.AddLineTo(_p5);
    _graphicsPath.AddArcTo(r1 -(width/2f), r2 -(width/2f),
                  angle, false, false, _p3);
    _graphicsPath.AddLineTo(_p4);


    this._graphics.FillPath(_graphicsPath);

    //-------
    this._graphics.GetThickLine(p5, p6, width,
          ref _p1, ref _p2, ref _p3, ref _p4);

    _graphicsPath.Reset();
    _graphicsPath.AddMoveTo(_p8);
    _graphicsPath.AddArcTo(r1 +(width/2f), r2 +(width/2f),
                  angle, false, true, _p2);
    _graphicsPath.AddLineTo(_p1);
    _graphicsPath.AddArcTo(r1 -(width/2f), r2 -(width/2f),
                  angle, false, false, _p7);
    _graphicsPath.AddLineTo(_p8);

    this._graphics.FillPath(_graphicsPath);

    //----------
    this._graphics.GetThickLine(p7, p8, width,
          ref _p5, ref _p6, ref _p7, ref _p8);

    _graphicsPath.Reset();
    _graphicsPath.AddMoveTo(_p4);
    _graphicsPath.AddArcTo(r1 +(width/2f), r2 +(width/2f),
                  angle, false, true, _p6);
    _graphicsPath.AddLineTo(_p5);
    _graphicsPath.AddArcTo(r1 -(width/2f), r2 -(width/2f),
                  angle, false, false, _p3);
    _graphicsPath.AddLineTo(_p4);

    this._graphics.FillPath(_graphicsPath);

    //-------
    this._graphics.GetThickLine(p1, p2, width,
          ref _p1, ref _p2, ref _p3, ref _p4);

    _graphicsPath.Reset();
    _graphicsPath.AddMoveTo(_p8);
    _graphicsPath.AddArcTo(r1 +(width/2f), r2 +(width/2f),
                  angle, false, true, _p2);
    _graphicsPath.AddLineTo(_p1);
    _graphicsPath.AddArcTo(r1 -(width/2f), r2 -(width/2f),
                  angle, false, false, _p7);
    _graphicsPath.AddLineTo(_p8);

    this._graphics.FillPath(_graphicsPath);
  }
  //--------------------------------------------------------------------------------
  //Methods: Circle
  //--------------------------------------------------------------------------------
  public void Circle(SVGPoint p, float r) {
    if((this.isUseWidth)&&((int)this._width > 1)) {
      Circle(p, r, this._width);
      return;
    }
    this._basicDraw.Circle(p, r);
  }
  //-----
  public void Circle(SVGPoint p, float r, float width) {
    if((int)width == 1) {
      Circle(p, r);
    } else {
      int r1 = (int)(width / 2f);
      int r2 = (int)width - r1;

      SVGPoint[] _points = new SVGPoint[1];
      _points[0] = new SVGPoint(p.x, p.y);

      SVGGraphicsPath _graphicsPath = new SVGGraphicsPath();
      _graphicsPath.AddCircleTo(p, r + r1);
      _graphicsPath.AddCircleTo(p, r - r2);

      this._graphics.FillPath(_graphicsPath, _points);
    }
  }
  //--------------------------------------------------------------------------------
  //Methods: Ellipse
  //--------------------------------------------------------------------------------
  public void Ellipse(SVGPoint p, float rx, float ry, float angle) {
    if((this.isUseWidth)&&((int)this._width > 1)) {
      Ellipse(p, rx, ry, angle, this._width);
      return;
    }
    this._basicDraw.Ellipse(p, rx, ry, angle);

  }
  //-----
  public void Ellipse(SVGPoint p, float rx, float ry, float angle, float width) {
    if((int)width == 1) {
      Ellipse(p, rx, ry, angle);
    } else {
      int r1 = (int)(width / 2f);
      int r2 = (int)width - r1;

      SVGPoint[] _points = new SVGPoint[1];
      _points[0] = new SVGPoint(p.x, p.y);

      SVGGraphicsPath _graphicsPath = new SVGGraphicsPath();
      _graphicsPath.AddEllipseTo(p, rx + r1, ry + r1, angle);
      _graphicsPath.AddEllipseTo(p, rx - r2, ry - r2, angle);

      this._graphics.FillPath(_graphicsPath, _points);
    }
  }
  //--------------------------------------------------------------------------------
  //Method: Polygon
  //--------------------------------------------------------------------------------
  public void Polygon(SVGPoint[] points) {

    if((this.isUseWidth)&&((int)this._width > 1)) {
      Polygon(points, this._width);
      return;
    }

    int _length = points.GetLength(0);
    if(_length > 1) {
      this._basicDraw.MoveTo(points[0]);
      for(int i = 1; i < _length; i++ ) {
        this._basicDraw.LineTo(points[i]);
      }
      this._basicDraw.LineTo(points[0]);
    }
  }
  //-----
  public void Polygon(SVGPoint[] points, float width) {
    if((int)width == 1) {
      Polygon(points);
      return;
    }
    int _length = points.GetLength(0);
    if(_length > 1) {
      if(_length == 2) {
        Line(points[0], points[1], width);
        StrokeLineCapLeft(points[0], points[1], width);
        StrokeLineCapRight(points[0], points[1], width);
      } else if(_length > 2) {
        StrokeLineJoin(points[_length - 1], points[0], points[1], width);
        Line(points[0], points[1], width);

        StrokeLineJoin(points[_length - 2], points[_length - 1], points[0], width);
        Line(points[_length - 1], points[0], width);
        for(int i = 1; i < _length - 1; i++) {
          StrokeLineJoin(points[i-1], points[i], points[i+1], width);
          Line(points[i], points[i+1], width);
        }
      }
    }
  }
  //--------------------------------------------------------------------------------
  //Methods: Polyline
  //--------------------------------------------------------------------------------
  public void Polyline(SVGPoint[] points) {

    if((this.isUseWidth)&&((int)this._width > 1)) {
      Polyline(points, this._width);
      return;
    }

    int _length = points.GetLength(0);
    if(_length > 1) {
      this._basicDraw.MoveTo(points[0]);
      for(int i = 1; i < _length; i++ ) {
        this._basicDraw.LineTo(points[i]);
      }
    }
  }
  //-----
  public void Polyline(SVGPoint[] points, float width) {
    if((int)width == 1) {
      Polygon(points);
      return;
    }
    int _length = points.GetLength(0);
    if(_length > 1) {
      if(_length >= 2) {
        Line(points[0], points[1], width);
        StrokeLineCapLeft(points[0], points[1], width);
        StrokeLineCapRight(points[_length - 2], points[_length - 1], width);

        for(int i = 1; i < _length - 1; i++) {
          StrokeLineJoin(points[i-1], points[i], points[i+1], width);
          Line(points[i], points[i+1], width);
        }
      }
    }
  }
  //================================================================================
  //--------------------------------------------------------------------------------
  //Methods: DrawPath
  //--------------------------------------------------------------------------------
  public void DrawPath(SVGGraphicsPath graphicsPath) {
    graphicsPath.RenderPath(this, false);
  }
  //-----
  //Fill co Stroke trong do luon
  public void DrawPath(SVGGraphicsPath graphicsPath, float width) {
    this._width = width;
    this.isUseWidth = true;
    graphicsPath.RenderPath(this, false);
    this.isUseWidth = false;
  }
}